using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioLuigi
{
   /// <summary>
   /// Facing direction along the X axis.
   /// </summary>
   enum FaceDirection
   {
      Left = -1,
      Right = 1,
   }

   enum State
   {
      Alive,
      Dying,
      Spinning,
      Dead,
   }

   /// <summary>
   /// A monster who is impeding the progress of our fearless adventurer.
   /// </summary>
   abstract class Enemy
   {
      public Level Level
      {
         get { return level; }
         protected set { level = value; }
      }
      Level level;

      /// <summary>
      /// Position in world space of the bottom center of this enemy.
      /// </summary>
      public Vector2 Position
      {
         get { return position; }
         protected set { position = value; }
      }
      Vector2 position;

      protected Rectangle localBounds;
      /// <summary>
      /// Gets a rectangle which bounds this enemy in world space.
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

      // Animations
      protected Animation runAnimation;
      protected Animation idleAnimation;
      protected Animation dyingAnimation;
      protected AnimationPlayer sprite = new AnimationPlayer();
      protected SpriteEffects effects = SpriteEffects.None;

      /// <summary>
      /// The direction this enemy is facing and moving along the X axis.
      /// </summary>
      protected FaceDirection direction = FaceDirection.Left;

      /// <summary>
      /// The current state of the enemy in the game world
      /// </summary>
      public State State
      {
         get { return state; }
         set { state = value; }
      }
      protected State state = State.Alive;

      /// <summary>
      /// How long this enemy has been waiting before turning around.
      /// </summary>
      protected float waitTime;

      protected float elapsed;

      /// <summary>
      /// How long to wait before turning around.
      /// </summary>
      protected const float MaxWaitTime = 0.005f;

      /// <summary>
      /// The speed at which this enemy moves along the X axis.
      /// </summary>
      protected const float MoveSpeed = 100.0f;

      protected float velocity = MoveSpeed;

      protected bool wasHit = false;
      protected float hitTimer = 0;
      protected const float HIT_TIMER = 0.5f;

      protected const int ENEMY_VALUE = 100;

      /// <summary>
      /// Constructs a new Enemy.
      /// </summary>
      public Enemy(Level level, Vector2 position, SpriteEffects effects)
      {
         this.level = level;
         this.position = position;
         this.effects = effects;

         LoadContent();
      }

      /// <summary>
      /// Loads a particular enemy sprite sheet and sounds.
      /// </summary>
      public virtual void LoadContent()
      {
         // Calculate bounds within texture size.
         int width = (int)(idleAnimation.FrameWidth * .85f);
         int left = (idleAnimation.FrameWidth - width) / 2;
         int height = (int)(idleAnimation.FrameHeight * .75);
         int top = idleAnimation.FrameHeight - height;
         localBounds = new Rectangle(left, top, width, height);
      }

      public virtual void OnCollision(Player collideWith) { }

      /// <summary>
      /// Paces back and forth along a platform, waiting at either end.
      /// </summary>
      public virtual void Update(GameTime gameTime)
      {
         float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

         // Calculate tile position based on the side we are walking towards.
         float posX = Position.X + localBounds.Width / 2 * (int)direction;
         int tileX = (int)Math.Floor(posX / Tile.Width) - (int)direction;
         int tileY = (int)Math.Floor(Position.Y / Tile.Height);

         if (waitTime > 0)
         {
            // Wait for some amount of time.
            waitTime = Math.Max(0.0f, waitTime - (float)gameTime.ElapsedGameTime.TotalSeconds);
            if (waitTime <= 0.0f)
            {
               // Then turn around.
               direction = (FaceDirection)(-(int)direction);
            }
         }
         else
         {
            // If we are about to run into a wall or off a cliff, start waiting.
            if (Level.GetCollision(tileX + (int)direction, tileY - 1) == TileCollision.Impassable ||
               Level.GetCollision(tileX + (int)direction, tileY) == TileCollision.Passable)
            {
               waitTime = MaxWaitTime;
            }
            else
            {
               // Move in the current direction.
               Vector2 Velocity = new Vector2((int)direction * velocity * elapsed, 0.0f);
               position += Velocity;
            }
         }
      }

      /// <summary>
      /// Draws the animated enemy.
      /// </summary>
      public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
      {
         // Draw facing the way the enemy is moving.
         SpriteEffects flip = direction > 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
         sprite.Draw(gameTime, spriteBatch, Position, flip, false);
      }
   }
}
