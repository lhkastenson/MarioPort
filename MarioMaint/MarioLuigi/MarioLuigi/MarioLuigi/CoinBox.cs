using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace MarioLuigi
{
    class CoinBox : Collectable
    {
       private Animation coinAnimation;
       private Animation bounceAnimation;
       private AnimationPlayer bounceSprite = new AnimationPlayer();
       
       private bool drawing = false;
       private bool bounce = false;
       private float animationTime = 0;
       private float resetBounce = 0;
       private const float RESET_BOUNCE = 0.25f;

       public CoinBox(Level level, Vector2 position, int x, int y)
           : base(level, position, x, y) { }

       public override void LoadContent()
       {
          texture = Level.Content.Load<Texture2D>("Tiles/QUEST002");

          Texture2D t = Level.Content.Load<Texture2D>("Sprites/Animations/COIN000");
          coinAnimation = new Animation(t, .0165f, false, t.Width / Tile.Width);

          Texture2D te = Level.Content.Load<Texture2D>("Sprites/Collectables/QUEST000");
          animation = new Animation(te, .25f, true, te.Width / Tile.Width);

          Texture2D tex = Level.Content.Load<Texture2D>("Sprites/Animations/QUEST002");
          bounceAnimation = new Animation(tex, .015f, false, tex.Width / Tile.Width);

          sprite.PlayAnimation(animation);

          base.LoadContent();
       }

       public override void Update(GameTime gameTime)
       {
          float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

          if (drawing)
          {
             animationTime += elapsed;
             if (animationTime > 0.265)
                drawing = false;
          }

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
                collectedBy.Score += COIN_VALUE;
                collectedBy.Coins++;
                sprite.PlayAnimation(coinAnimation);
                drawing = true;
                isActive = false;
                base.OnCollected(collectedBy);
             }
             else if (!bounce)
             {
                bounce = true;
                bounceSprite.PlayAnimation(bounceAnimation);
             }
          }
       }

       public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
       {
          if (!isActive)
          {
             if (drawing)
                sprite.Draw(gameTime, spriteBatch, new Vector2(Position.X, Position.Y + origin.Y),
                   SpriteEffects.None, false);

             if (bounce)
                bounceSprite.Draw(gameTime, spriteBatch, new Vector2(Position.X, Position.Y + origin.Y),
                   SpriteEffects.None, false);
             else
                base.Draw(gameTime, spriteBatch);
          }
          else
             sprite.Draw(gameTime, spriteBatch, new Vector2(Position.X, Position.Y + origin.Y),
                SpriteEffects.None, false);
       }
    }
}
