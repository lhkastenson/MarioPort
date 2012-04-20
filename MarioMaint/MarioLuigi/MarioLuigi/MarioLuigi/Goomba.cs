using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioLuigi
{
   class Goomba : Enemy
   {
      private const float KILL_TIMER = 0.25f;
      private float killTimer = 0f;

      public Goomba(Level level, Vector2 position, SpriteEffects effects)
         : base(level, position, effects) { }

      public override void LoadContent()
      {
         string spritePath = "Sprites/Goomba/";
         Texture2D a = null, b = null;

         a = Level.Content.Load<Texture2D>(spritePath + "CHIBIBO000");
         b = Level.Content.Load<Texture2D>(spritePath + "CHIBIBO001");

         // Load animations.
         idleAnimation = new Animation(a, 0.15f, true, a.Width / Tile.Width);
         dyingAnimation = new Animation(b, 0.15f, true, b.Width / Tile.Width);

         sprite.PlayAnimation(idleAnimation);

         base.LoadContent();
      }

      public override void Update(GameTime gameTime)
      {
         elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

         if (wasHit)
         {
            hitTimer += elapsed;
            if (hitTimer > HIT_TIMER)
            {
               wasHit = false;
               hitTimer = 0;
            }
         }

         if (state == State.Dying)
         {
            killTimer += elapsed;
            if (killTimer > KILL_TIMER)
            {
               state = State.Dead;
               killTimer = 0;
            }
         }

         base.Update(gameTime);
      }

      public override void OnCollision(Player collideWith)
      {

         if (collideWith.BoundingRectangle.Bottom < this.BoundingRectangle.Center.Y
            && state == State.Alive && !wasHit)
         {
            state = State.Dying;
            collideWith.Score += ENEMY_VALUE;
            velocity = 0;
            wasHit = true;
            collideWith.BounceJump = true;
            collideWith.FirstBounce = true;
         }
         else
         {
            if (state == State.Alive)
            {
               if (collideWith.Invinsible)
                  state = State.Dying;
               else if (!collideWith.Invinsible && !collideWith.WasHit)
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

         base.OnCollision(collideWith);
      }

      public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
      {
         if (state == State.Alive)
            sprite.PlayAnimation(idleAnimation);
         else if (state == State.Dying)
            sprite.PlayAnimation(dyingAnimation);

         sprite.Draw(gameTime, spriteBatch, Position, SpriteEffects.None, false);
      }
   }
}
