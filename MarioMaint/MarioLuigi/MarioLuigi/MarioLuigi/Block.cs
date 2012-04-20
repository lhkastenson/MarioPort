using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace MarioLuigi
{
   class Block : Collectable
   {
      private bool bounce = false;
      private float resetBounce = 0;
      private const float RESET_BOUNCE = 0.25f;

      public Block(Level level, Vector2 position, int x, int y)
         : base(level, position, x, y) { }

      public override void LoadContent()
      {
         texture = Level.Content.Load<Texture2D>("Tiles/BLOCK001");

         Texture2D t = Level.Content.Load<Texture2D>("Sprites/Animations/BRICK002");
         animation = new Animation(t, .015f, false, t.Width / Tile.Width);

         base.LoadContent();
      }

      public override void Update(GameTime gameTime)
      {
         if (bounce)
         {
            resetBounce += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (resetBounce > RESET_BOUNCE)
            {
               bounce = false;
               resetBounce = 0;
               LoadContent();
            }
         }
      }

      public override void OnCollected(Player collectedBy)
      {
         // make sure box is active and player is below it
         if(isActive && collectedBy.Position.Y > this.Position.Y + texture.Height
             && collectedBy.Position.X + Tile.Width / 2 < this.Position.X + Tile.Width
             && collectedBy.Position.X + Tile.Width / 2 > this.Position.X)
         {
            if (collectedBy.Size > 0)
            {
               collectedBy.Score += BLOCK_VALUE;
               isActive = false;
            }
            else if(!bounce)
            {
               bounce = true;
               sprite.PlayAnimation(animation);
            }
            base.OnCollected(collectedBy);
         }
      }

      public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
      {
         if (bounce)
         {
            sprite.Draw(gameTime, spriteBatch,
               new Vector2(Position.X, Position.Y + origin.Y), SpriteEffects.None, false);
         }
         else
            base.Draw(gameTime, spriteBatch);
      }
   }
}
