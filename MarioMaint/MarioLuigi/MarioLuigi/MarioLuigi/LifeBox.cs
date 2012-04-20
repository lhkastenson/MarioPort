using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace MarioLuigi
{
   class LifeBox : Collectable
   {
      private Animation bounceAnimation;
      private bool bounce = false;
      private float resetBounce = 0;
      private const float RESET_BOUNCE = 0.25f;

      public LifeBox(Level level, Vector2 position, int x, int y)
         : base(level, position, x, y) { }

      public override void LoadContent()
      {
         texture = Level.Content.Load<Texture2D>("Tiles/QUEST002");

         Texture2D t = Level.Content.Load<Texture2D>("Sprites/Collectables/QUEST000");
         animation = new Animation(t, .25f, true, t.Width / Tile.Width);

         Texture2D tex = Level.Content.Load<Texture2D>("Sprites/Animations/QUEST002");
         bounceAnimation = new Animation(tex, .015f, false, tex.Width / Tile.Width);

         sprite.PlayAnimation(animation);

         base.LoadContent();
      }

      public override void Update(GameTime gameTime)
      {
         float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

         if (bounce)
         {
            resetBounce += elapsed;
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
         if(collectedBy.Position.Y > this.Position.Y + texture.Height
             && collectedBy.Position.X + Tile.Width / 2 < this.Position.X + Tile.Width
             && collectedBy.Position.X + Tile.Width / 2 > this.Position.X)
         {
            if (isActive)
            {
               isActive = false;
               Level.AddCollectable(new Life(Level, new Vector2(Position.X - origin.X, Position.Y - origin.Y), X, Y));
               base.OnCollected(collectedBy);
            }
            else if (!bounce)
            {
               bounce = true;
               sprite.PlayAnimation(bounceAnimation);
            }
         }
      }

      public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
      {
         if (!isActive)
         {
            if (bounce)
               sprite.Draw(gameTime, spriteBatch, new Vector2(Position.X, Position.Y + origin.Y), SpriteEffects.None, false);
            else
               base.Draw(gameTime, spriteBatch);
         }
         else
            sprite.Draw(gameTime, spriteBatch, new Vector2(Position.X, Position.Y + origin.Y),
               SpriteEffects.None, false);
      }
   }
}
