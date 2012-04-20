using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace MarioLuigi
{
   class StarBox : Collectable
   {
      public StarBox(Level level, Vector2 position, int x, int y)
         : base(level, position, x, y) { }

      public override void LoadContent()
      {
         texture = Level.Content.Load<Texture2D>("Tiles/QUEST002");

         Texture2D t = Level.Content.Load<Texture2D>("Sprites/Collectables/QUEST000");
         animation = new Animation(t, .25f, true, t.Width / Tile.Width);

         sprite.PlayAnimation(animation);

         base.LoadContent();
      }

      public override void OnCollected(Player collectedBy)
      {
         // make sure box is active and player is below it
         if(isActive && collectedBy.Position.Y > this.Position.Y + texture.Height
             && collectedBy.Position.X + Tile.Width / 2 < this.Position.X + Tile.Width
             && collectedBy.Position.X + Tile.Width / 2 > this.Position.X)
         {
            isActive = false;
            Level.AddCollectable(new Star(Level, new Vector2(Position.X - origin.X, Position.Y - origin.Y), X, Y));
            base.OnCollected(collectedBy);
         }
      }

      public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
      {
         if (!isActive)
            base.Draw(gameTime, spriteBatch);
         else
            sprite.Draw(gameTime, spriteBatch, new Vector2(Position.X, Position.Y + origin.Y), SpriteEffects.None, false);
      }
   }
}

