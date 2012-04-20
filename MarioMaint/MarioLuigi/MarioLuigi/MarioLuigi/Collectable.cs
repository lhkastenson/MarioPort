using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace MarioLuigi
{
   /// <summary>
   /// A valuable item the player can collect.
   /// </summary>
   abstract class Collectable
   {
      protected Texture2D texture;
      protected Animation animation;
      protected AnimationPlayer sprite;
      protected Vector2 origin;
      protected bool isActive;
      // Allow aother objects to know if this collectable can be aquired
      public bool IsActive
      {
         get { return isActive; }
      }

      protected readonly Vector2 upVelocity = new Vector2(0, -40f);

      protected readonly Color Color = Color.White;
      protected const int COIN_VALUE = 50;
      protected const int BLOCK_VALUE = 10;
      protected const int POWER_VALUE = 1000;
      protected const float MoveAcceleration = 5000.0f;
      protected const float MaxFallSpeed = 250.0f;
      protected const float MaxMoveSpeed = 150.0f;
      protected const float GravityAcceleration = 5000.0f;
      protected float movement = 0;
      protected bool isOnGround;
      protected float previousBottom;

      /// <summary>
      /// 
      /// </summary>
      public Level Level
      {
         get { return level; }
      }
      Level level;

      /// <summary>
      /// Gets the current position of this collectable in world space.
      /// </summary>
      public Vector2 Position
      {
         get { return position; }
         protected set { position = value; }
      }
      Vector2 position;

      /// <summary>
      /// Gets the current velocity for the collectable
      /// </summary>
      public Vector2 Velocity
      {
         get { return velocity; }
         protected set { velocity = value; }
      }
      Vector2 velocity = new Vector2(40f, 0);

      /// <summary>
      /// Gets a circle which bounds this collectable in world space.
      /// </summary>
      public Circle BoundingCircle
      {
         get
         {
            return new Circle(Position, Tile.Width / 3f);
         }
      }

      public Rectangle BoundingRectangle
      {
         get
         {
            return new Rectangle((int)position.X, (int)position.Y,
               texture.Width, texture.Height);
         }
      }

      public int X
      {
         get { return x; }
      }
      int x;

      public int Y
      {
         get { return y; }
      }
      int y;

      /// <summary>
      /// Constructs a new collectable.
      /// </summary>
      public Collectable(Level level, Vector2 position, int x, int y)
      {
         this.level = level;
         this.position = position;
         this.x = x;
         this.y = y;
         isActive = true;
         sprite = new AnimationPlayer();
         LoadContent();
      }

      /// <summary>
      /// Loads the collectable texture and collected sound.
      /// </summary>
      public virtual void LoadContent()
      {
         origin = new Vector2(texture.Width / 2.0f, texture.Height / 2.0f);
      }

      /// <summary>
      /// Bounces up and down in the air to entice players to collect them.
      /// </summary>
      public virtual void Update(GameTime gameTime) { }

      /// <summary>
      /// Updates the player's velocity and position based on input, gravity, etc.
      /// </summary>
      public void ApplyPhysics(GameTime gameTime)
      {
         float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

         Vector2 previousPosition = Position;

         // Base velocity is a combination of horizontal movement control and
         // acceleration downward due to gravity.
         velocity.X += movement * MoveAcceleration * elapsed;

         velocity.Y = MathHelper.Clamp(velocity.Y + GravityAcceleration * elapsed, -MaxFallSpeed, MaxFallSpeed);

         // Prevent the player from running faster than his top speed.
         velocity.X = MathHelper.Clamp(velocity.X, -MaxMoveSpeed, MaxMoveSpeed);

         // Apply velocity.
         Position += velocity * elapsed;
         Position = new Vector2((float)Math.Round(Position.X), (float)Math.Round(Position.Y));

         // If the player is now colliding with the level, separate them.
         HandleCollisions();

         // If the collision stopped us from moving, reset the velocity to zero.
         if (Position.X == previousPosition.X)
            velocity.X = 0;

         if (Position.Y == previousPosition.Y)
            velocity.Y = 0;
      }

      /// <summary>
      /// Detects and resolves all collisions between the collectable and the neighboring
      /// tiles. When a collision is detected, the collectable is pushed away along one
      /// axis to prevent overlapping. There is some special logic for the Y axis to
      /// handle platforms which behave differently depending on direction of movement.
      /// </summary>
      private void HandleCollisions()
      {
         // Get the collectable's bounding rectangle and find neighboring tiles.
         Rectangle bounds = BoundingRectangle;
         int leftTile = (int)Math.Floor((float)bounds.Left / Tile.Width);
         int rightTile = (int)Math.Ceiling(((float)bounds.Right / Tile.Width)) - 1;
         int topTile = (int)Math.Floor((float)bounds.Top / Tile.Height);
         int bottomTile = (int)Math.Ceiling(((float)bounds.Bottom / Tile.Height)) - 1;

         // Reset flag to search for ground collision.
         isOnGround = false;

         // For each potentially colliding tile,
         for (int y = topTile; y <= bottomTile; ++y)
         {
            for (int x = leftTile; x <= rightTile; ++x)
            {
               // If this tile is collidable,
               TileCollision collision = Level.GetCollision(x, y);
               if (collision != TileCollision.Passable)
               {
                  // Determine collision depth (with direction) and magnitude.
                  Rectangle tileBounds = Level.GetBounds(x, y);
                  Vector2 depth = RectangleExtensions.GetIntersectionDepth(bounds, tileBounds);
                  if (depth != Vector2.Zero)
                  {
                     float absDepthX = Math.Abs(depth.X);
                     float absDepthY = Math.Abs(depth.Y);

                     // Resolve the collision along the shallow axis.
                     if (absDepthY < absDepthX || collision == TileCollision.Platform)
                     {
                        // If we crossed the top of a tile, we are on the ground.
                        if (previousBottom <= tileBounds.Top)
                           isOnGround = true;

                        // Ignore platforms, unless we are on the ground.
                        if (collision == TileCollision.Impassable || isOnGround)
                        {
                           // Resolve the collision along the Y axis.
                           Position = new Vector2(Position.X, Position.Y + depth.Y);

                           // Perform further collisions with the new bounds.
                           bounds = BoundingRectangle;
                        }
                     }
                     else if (collision == TileCollision.Impassable) // Ignore platforms.
                     {
                        // Resolve the collision along the X axis.
                        Position = new Vector2(Position.X + depth.X, Position.Y);
                        movement = - movement;

                        // Perform further collisions with the new bounds.
                        bounds = BoundingRectangle;
                     }
                  }
               }
            }
         }

         // Save the new bounds bottom.
         previousBottom = bounds.Bottom;
      }

      /// <summary>
      /// Called when this collectable has been collected by a player and removed from the level.
      /// </summary>
      /// <param name="collectedBy">
      /// The player who collected this collectable. Although currently not used, this parameter would be
      /// useful for creating special powerup collectables. For example, a collectable could make the player invincible.
      /// </param>
      public virtual void OnCollected(Player collectedBy) { }

      /// <summary>
      /// Draws a collectable in the appropriate color.
      /// </summary>
      public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
      {
         spriteBatch.Draw(texture, Position, null, Color, 0.0f, origin, 1.0f, SpriteEffects.None, 0.0f);
      }
   }
}
