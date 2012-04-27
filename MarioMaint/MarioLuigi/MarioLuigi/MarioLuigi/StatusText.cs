using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MarioLuigi
{
   class StatusText
   {
      private SpriteFont spriteFont;
      private ContentManager content;
      private Animation animation;
      private AnimationPlayer sprite = new AnimationPlayer();

      private int stageWidth, stageHeight;

      private const int IMAGE_WIDTH = 40;
      private const int IMAGE_HEIGHT = 28;
      private const float COIN_SCALE = 0.9f;
      private const float HEIGHT_SCALE = 4f;

      /// <summary>
      /// Initializes the status text bar.
      /// </summary>
      /// <param name="content">content manager to load resources</param>
      /// <param name="stageWidth">width of the screen</param>
      /// <param name="stageHeight">height of the screen</param>
      /// <param name="scale">scale of everything drawn</param>
      public StatusText(ContentManager content, int stageWidth, int stageHeight) 
      {
         this.stageHeight = stageHeight;
         this.stageWidth = stageWidth;
         this.content = content;
         spriteFont = this.content.Load<SpriteFont>("StatusFont");
         Texture2D t  = this.content.Load<Texture2D>("Sprites/Animations/COINICON");
         animation = new Animation(t, .25f, true, t.Width / Tile.Width);
         sprite.PlayAnimation(animation);
      }

      /// <summary>
      /// Draws the status line on the top of the screen if it is enabled.
      /// </summary>
      /// <param name="spriteBatch">spritebatch used to draw the current frame</param>
      public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
      {
         if (Options.Text)
         {
            spriteBatch.Begin();
            // lives
            spriteBatch.DrawString(spriteFont, "MARIO x " + Options.Lives, new Vector2(IMAGE_WIDTH / 2f,
               IMAGE_HEIGHT / HEIGHT_SCALE), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            // score
            spriteBatch.DrawString(spriteFont, Options.Score.ToString("0000000"), new Vector2(IMAGE_WIDTH * 4f,
               IMAGE_HEIGHT / HEIGHT_SCALE), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            // coin texture
            sprite.Draw(gameTime, spriteBatch, new Vector2(IMAGE_WIDTH * 7.5f, IMAGE_HEIGHT * 1.20f), SpriteEffects.None, false);
            // number of coins
            spriteBatch.DrawString(spriteFont, "x " + Options.Coins, new Vector2(IMAGE_WIDTH * 8f,
               IMAGE_HEIGHT / HEIGHT_SCALE), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            // current level number
            spriteBatch.DrawString(spriteFont, "LEVEL " + Options.Level, new Vector2(IMAGE_WIDTH * 10f,
               IMAGE_HEIGHT / HEIGHT_SCALE), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            //Timer
            spriteBatch.DrawString(spriteFont, "TIME: " + Level.Time().ToString(), new Vector2(IMAGE_WIDTH * 13f,
               IMAGE_HEIGHT / HEIGHT_SCALE), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

            spriteBatch.End();
         }
      }
   }
}
