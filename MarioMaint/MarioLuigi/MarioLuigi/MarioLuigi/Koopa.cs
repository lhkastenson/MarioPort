using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioLuigi
{
   class Koopa : Enemy
   {
      private Animation spinningAnimation;
      private float downTime = 0;
      private const float DOWN_TIME = 5.0f;

      public Koopa(Level level, Vector2 position, SpriteEffects effects)
         : base(level, position, effects) { }

      public override void LoadContent()
      {
         string spritePath = "Sprites/Koopa/";
         Texture2D a = null, b = null, c = null, d = null;

         a = Level.Content.Load<Texture2D>(spritePath + "RDKOOPA000");
         b = Level.Content.Load<Texture2D>(spritePath + "RDKOOPA001");
         c = Level.Content.Load<Texture2D>(spritePath + "RDKP000");
         d = Level.Content.Load<Texture2D>(spritePath + "RDKP001");

         // Load animations.
         runAnimation = new Animation(a, 0.15f, true, a.Width / Tile.Width);
         idleAnimation = new Animation(b, 0.15f, true, b.Width / Tile.Width);
         spinningAnimation = new Animation(c, 0.15f, true, c.Width / Tile.Width);
         dyingAnimation = new Animation(d, 0.15f, true, d.Width / Tile.Width);

         sprite.PlayAnimation(idleAnimation);

         base.LoadContent();
      }

      public override void Update(GameTime gameTime)
      {
         float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

         if (state == State.Dying)
         {
            downTime += elapsed;
            if (downTime > DOWN_TIME)
            {
               state = State.Alive;
               velocity = MoveSpeed;
               downTime = 0;
            }
         }

         if (state == State.Held && !(Level.Player.Direction == SpriteEffects.FlipHorizontally))
            Position = new Vector2(Level.Player.BoundingRectangle.X - ((Level.Player.BoundingRectangle.Width / 8)), Level.Player.BoundingRectangle.Y + (3*(Level.Player.BoundingRectangle.Height / 4)));
         else if (state == State.Held)
            Position = new Vector2(Level.Player.BoundingRectangle.X + (6*(Level.Player.BoundingRectangle.Width)/5), Level.Player.BoundingRectangle.Y + (3 * (Level.Player.BoundingRectangle.Height / 4)));

         if (wasHit)
         {
            hitTimer += elapsed;
            if (hitTimer > HIT_TIMER && !Level.Player.IsHolding)
            {
               wasHit = false;
               hitTimer = 0;
            }
         }
         base.Update(gameTime);
      }

      public override void OnCollision(Player collideWith)
      {
         if(state == State.Dying && Level.Player.IsRunning && !Level.Player.IsHolding) //pickup shell
         {
            Console.WriteLine("Running: " + Level.Player.IsRunning);
            state = State.Held;
            wasHit = true;
            Level.Player.IsHolding = true;
            if (!(Level.Player.Direction == SpriteEffects.FlipHorizontally))
               Position = new Vector2(Level.Player.BoundingRectangle.X - (Level.Player.BoundingRectangle.Width / 8), Level.Player.BoundingRectangle.Y + (3 * (Level.Player.BoundingRectangle.Height / 4)));
            else
               Position = new Vector2(Level.Player.BoundingRectangle.X + (6*(Level.Player.BoundingRectangle.Width)/5), Level.Player.BoundingRectangle.Y + (3 * (Level.Player.BoundingRectangle.Height / 4)));
         }
         else if (state == State.Dying && !wasHit
                  || state == State.Held && !Level.Player.IsRunning)
         {
            if (state == State.Held)
            {
               Level.Player.IsHolding = false;
               velocity = 0;
               if (!(Level.Player.Direction == SpriteEffects.FlipHorizontally))
               {
                  Position = new Vector2(Level.Player.BoundingRectangle.X - Level.Player.BoundingRectangle.Width, Level.Player.BoundingRectangle.Y + Level.Player.BoundingRectangle.Height);
                  collideWith.Velocity -= Vector2.Multiply(new Vector2(0, -1500f), 0.0167f);
                 // Direction = FaceDirection.Right;
               }
               else
               {
                  Position = new Vector2(Level.Player.BoundingRectangle.X + 2 * Level.Player.BoundingRectangle.Width, Level.Player.BoundingRectangle.Y + Level.Player.BoundingRectangle.Height);
                  collideWith.Velocity += Vector2.Multiply(new Vector2(0, -1500f), 0.0167f);
                 /// Direction = FaceDirection.Left;
               }
               
               state = State.Spinning;
               velocity = MoveSpeed * 5f;
               if (!(Level.Player.Direction == SpriteEffects.FlipHorizontally))
                  Direction = FaceDirection.Left;
               else
                  Direction = FaceDirection.Right;
               wasHit = true;
               collideWith.BounceJump = true;
               collideWith.FirstBounce = true;
            }
            else
            {
               collideWith.Velocity += Vector2.Multiply(new Vector2(0, -1500f), 0.0167f);
               state = State.Spinning;
               velocity = MoveSpeed * 5f;
               wasHit = true;
               collideWith.BounceJump = true;
               collideWith.FirstBounce = true;
            }
         }
         else if((collideWith.BoundingRectangle.Bottom < this.BoundingRectangle.Center.Y
            && state == State.Dying && !wasHit)
            || (collideWith.BoundingRectangle.Bottom < this.BoundingRectangle.Center.Y
            && state == State.Alive && !wasHit)
            || (collideWith.BoundingRectangle.Bottom < this.BoundingRectangle.Center.Y
            && state == State.Spinning && !wasHit))
         {
            collideWith.Velocity += Vector2.Multiply(new Vector2(0, -1500f), 0.0167f);
            state = State.Dying;
            collideWith.Score += ENEMY_VALUE;
            velocity = 0;
            wasHit = true;
            collideWith.BounceJump = true;
            collideWith.FirstBounce = true;
         }
         else if(state == State.Alive || state == State.Spinning)
         {
            if(collideWith.Invinsible)
               state = State.Dead;
            else if(!collideWith.Invinsible && !collideWith.WasHit)
            {
               collideWith.Size--;
               if(collideWith.Size >= 0)
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

         base.OnCollision(collideWith);
      }

      public override void OnCollision(Enemy collideWith)
      {
         if (state == State.Spinning || state == State.DeadSpinning)
         {
            Level.Player.Score += ENEMY_VALUE;
            collideWith.State = State.Dead;
            state = State.DeadSpinning;
         }
      }
      public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
      {
         if (state == State.Alive)
            sprite.PlayAnimation(runAnimation);
         else if (state == State.Spinning)
            sprite.PlayAnimation(spinningAnimation);
         else if (state == State.Dying)
            sprite.PlayAnimation(dyingAnimation);

         base.Draw(gameTime, spriteBatch);
      }
   }
}
