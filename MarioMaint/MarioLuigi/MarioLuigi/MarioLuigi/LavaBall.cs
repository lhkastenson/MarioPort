using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioLuigi
{
   class LavaBall : Enemy
   {
      private new float waitTime = 0.0f;
      private float wait;
      private bool active = false;
      private const float WAIT_TIME = 0.75f;
      private Vector2 Velocity = Vector2.Zero;
      private new const float MoveSpeed = 400.0f;
      private bool top = false;

      public LavaBall(Level level, Vector2 position, SpriteEffects effects, int wait)
         : base(level, position, effects) 
      {
         this.wait = wait;
      }

      public override void LoadContent()
      {
         string spritePath = "Sprites/Fire/";
         Texture2D a = null;

         // Load animations.
         a = Level.Content.Load<Texture2D>(spritePath + "WHFIRE000");
         idleAnimation = new Animation(a, 0.15f, true, a.Width / Tile.Width);

         sprite.PlayAnimation(idleAnimation);

         base.LoadContent();
      }

      public override void Update(GameTime gameTime)
      {
         float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

         if (!active)
         {
            wait -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (wait < 0)
               active = true;
         }
         else
         {
            waitTime += elapsed;
            if (waitTime > WAIT_TIME)
            {
               if (Velocity == Vector2.Zero && !top)
               {
                  Velocity = new Vector2(0.0f, -MoveSpeed);
                  waitTime = 0.0f;
                  top = false;
               }
               else if (Velocity.Y < 0 && !top)
               {
                  Velocity = new Vector2(0.0f, MoveSpeed);
                  waitTime = 0.0f;
                  top = true;
               }
               else if (Velocity.Y > 0 && top)
               {
                  Velocity = Vector2.Zero;
                  waitTime = 0.0f;
                  top = false;
               }
            }

            Position = new Vector2(Position.X, Position.Y + Velocity.Y * elapsed);
         }
      }

      public override void OnCollision(Player collideWith)
      {
         if (state == State.Alive)
         {
            if (collideWith.Invinsible)
               state = State.Dead;
            if (!collideWith.Invinsible && !collideWith.WasHit)
            {
               collideWith.Size--;
               if (collideWith.Size >= 0)
               {
                  collideWith.LoadContent();
                  collideWith.WasHit = true;
               }
               else
               {
                  collideWith.Size++;
                  collideWith.LoadContent();
                  collideWith.OnKilled(this);
               }
            }
         }
      }

      public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
      {
         sprite.Draw(gameTime, spriteBatch, new Vector2(Position.X + (Tile.Width / 2.0f), Position.Y),
            SpriteEffects.None, false);
      }
   }
}
