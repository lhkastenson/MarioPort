using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioLuigi
{
   class Red : Enemy
   {

      public Red(Level level, Vector2 position, SpriteEffects effects)
         : base(level, position, effects) { }

      public override void LoadContent()
      {
         string spritePath = "Sprites/Red/";
         Texture2D a = null;

         // Load animations.
         a = Level.Content.Load<Texture2D>(spritePath + "RED000");
         idleAnimation = new Animation(a, 0.15f, true, a.Width / Tile.Width);

         sprite.PlayAnimation(idleAnimation);

         base.LoadContent();
      }

      public override void OnCollision(Player collideWith)
      {
         if (state == State.Alive)
         {
            if (collideWith.Invinsible)
               state = State.Dead;
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

      public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
      {
         if (state == State.Alive)
            sprite.PlayAnimation(idleAnimation);

         base.Draw(gameTime, spriteBatch);
      }
   }
}
