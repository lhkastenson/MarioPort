using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace MarioLuigi
{
   class Coin : Collectable
   {
      public Coin(Level level, Vector2 position, int x, int y)
         : base(level, position, x, y) { }

      public override void LoadContent()
      {
         texture = Level.Content.Load<Texture2D>("Sprites/Collectables/COIN000");
         animation = new Animation(texture, .15f, true, texture.Width / Tile.Width);
         sprite.PlayAnimation(animation);
      }

      public override void OnCollected(Player collectedBy)
      {
         collectedBy.Score += COIN_VALUE;
         collectedBy.Coins++;
         base.OnCollected(collectedBy);
      }

      public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
      {
         sprite.Draw(gameTime, spriteBatch, Position, SpriteEffects.None, false);
      }
   }
}
