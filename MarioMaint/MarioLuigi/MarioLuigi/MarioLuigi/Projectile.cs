using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MarioLuigi
{
   class Projectile
   {
      private Animation animation;
      private Animation explosion;
      private AnimationPlayer sprite = new AnimationPlayer();
      private Level Level;      

      // Position of the Projectile relative to the upper left side of the screen
      public Vector2 Position { get; set; }
      private SpriteEffects flip = SpriteEffects.None;

      // Determines how fast the projectile moves
      private Vector2 velocity;
      private float speed = 450.0f;

      private Rectangle localBounds;
      /// <summary>
      /// Gets a rectangle which bounds this projectile in world space.
      /// </summary>
      public Rectangle BoundingRectangle
      {
         get
         {
            int left = (int)Math.Round(Position.X - sprite.Origin.X) + localBounds.X;
            int top = (int)Math.Round(Position.Y - sprite.Origin.Y) + localBounds.Y;

            return new Rectangle(left, top, localBounds.Width, localBounds.Height);
         }
      }

      private bool isOnGround = false;

      /// <summary>
      /// Is this projectile still active
      /// </summary>
      private bool active = true;

      public bool Done { get; private set; }
      private const float REMOVE_TIME = 0.1f;
      private float removeTime = 0.0f;

      private const int KILLED_ENEMY = 100;
      private float previousBottom;
      private float previousTop;

      /// <summary>
      /// Creates a new projectile in world space
      /// </summary>
      /// <param name="content"></param>
      /// <param name="position"></param>
      /// <param name="spritePath"></param>
      public Projectile(Player player)
      {
         this.Level = player.Level;

         // Fire the projectile based on which direction the player is facing
         if (player.Direction == SpriteEffects.FlipHorizontally)
         {
            speed *= 1.0f;
            Position = new Vector2(player.Position.X + Tile.Width / 2.0f, player.Position.Y - Tile.Width / 2.0f);
            flip = SpriteEffects.FlipHorizontally;
         }
         else
         {
            speed *= -1.0f;
            Position = new Vector2(player.Position.X, player.Position.Y - Tile.Width / 2.0f);
            flip = SpriteEffects.None;
         }

         velocity = new Vector2(speed, 0 );

         LoadContent(player.Level.Content);
      }

      /// <summary>
      /// Loads and plays the animations
      /// </summary>
      /// <param name="content"></param>
      private void LoadContent(ContentManager content)
      {
         Texture2D t = content.Load<Texture2D>("Sprites/Animations/FIRE000");
         animation = new Animation(t, .15f, true, 2);

         Texture2D te = content.Load<Texture2D>("Sprites/Animations/F000");
         explosion = new Animation(te, .05f, false, 4);

         sprite.PlayAnimation(animation);

         // Calculate bounds within texture size.            
         int width = (int)(animation.FrameWidth * 1.25f);
         int left = (animation.FrameWidth - width) / 2;
         int height = (int)(animation.FrameHeight * 1.25f);
         int top = animation.FrameHeight - height;
         localBounds = new Rectangle(left, top, width, height);
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="gameTime"></param>
      /// <param name="movement"></param>
      public void Update(GameTime gameTime)
      {
         if (!active)
         {
            removeTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (removeTime > REMOVE_TIME)
               Done = true;
            sprite.PlayAnimation(explosion);
         }

         if (velocity.Y < 0)
         {
             velocity.Y += 10;
         }
         else
         {
             velocity.Y += 10;
         }

         Position += Vector2.Multiply(velocity, (float)gameTime.ElapsedGameTime.TotalSeconds);
         HandleCollisions();
      }

      public void OnCollision(Enemy enemy)
      {
         active = false;
         Level.Player.Score += KILLED_ENEMY;
         velocity = Vector2.Zero;
         enemy.State = State.Dead;
      }

      /// <summary>
      /// Detects and resolves all collisions between the player and his neighboring
      /// tiles. When a collision is detected, the player is pushed away along one
      /// axis to prevent overlapping. There is some special logic for the Y axis to
      /// handle platforms which behave differently depending on direction of movement.
      /// </summary>
      private void HandleCollisions()
      {
         // Get the player's bounding rectangle and find neighboring tiles.
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
                         if (previousBottom <= tileBounds.Top || previousTop <= tileBounds.Top)
                             isOnGround = true;
                        // Ignore platforms, unless we are on the ground.
                        if (collision == TileCollision.Impassable || isOnGround)
                        {
                           //active = false;
                           //velocity = Vector2.Zero;
                           if(velocity.Y < 0)
                           {
                            Position = new Vector2(Position.X, tileBounds.Top);
                           }
                               velocity.Y = -velocity.Y;
                           // Perform further collisions with the new bounds.
                           bounds = BoundingRectangle;
                        }
                     }
                     else if (collision == TileCollision.Impassable) // Ignore platforms.
                     {
                        active = false;
                        velocity = Vector2.Zero;

                        // Perform further collisions with the new bounds.
                        bounds = BoundingRectangle;
                     }
                  }
               }
            }
         }
         previousBottom = bounds.Bottom;
         previousTop = bounds.Top;
      }

      /// <summary>
      /// Draws the projectile animation at the given coordinates
      /// </summary>
      /// <param name="gameTime"></param>
      /// <param name="spriteBatch"></param>
      public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
      {
         sprite.Draw(gameTime, spriteBatch, Position, flip, false);
      }
   }
}
