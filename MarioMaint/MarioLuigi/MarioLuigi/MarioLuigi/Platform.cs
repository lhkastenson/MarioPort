using System;
using System.IO;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Media;

namespace MarioLuigi
{
   /// <summary>
   /// This is the main type for your game
   /// </summary>
   public class Platform : Game
   {
      private GraphicsDeviceManager graphics;
      private SpriteBatch spriteBatch;

      private Level level;
      private Texture2D loading;
      private StatusText statusText;

      private const int BackBufferWidth = 640;
      private const int BackBufferHeight = 364;
      private const int LAYERS = 2;

      private const float LOADING_TIME = 2;
      private float loadingTime = 0;
      private bool isLoading = false;

      public static bool ExitGame = false;

      public Platform()
      {
         graphics = new GraphicsDeviceManager(this);
         graphics.PreferredBackBufferWidth = BackBufferWidth;
         graphics.PreferredBackBufferHeight = BackBufferHeight;
         Content.RootDirectory = "Content";
         Options.Level = 0;
      }

      /// <summary>
      /// Allows the game to perform any initialization it needs to before starting to run.
      /// This is where it can query for any required services and load any non-graphic
      /// related content.  Calling base.Initialize will enumerate through any components
      /// and initialize them as well.
      /// </summary>
      protected override void Initialize()
      {
         base.Initialize();
      }

      /// <summary>
      /// LoadContent will be called once per game and is the place to load
      /// all of your content.
      /// </summary>
      protected override void LoadContent()
      {
         // Create a new SpriteBatch, which can be used to draw textures.
         spriteBatch = new SpriteBatch(GraphicsDevice);

         loading = Content.Load<Texture2D>("Backgrounds/START000");

         LoadNextLevel(Options.Level);

         statusText = new StatusText(Content, graphics.PreferredBackBufferWidth,
            graphics.PreferredBackBufferHeight);
      }

      /// <summary>
      /// UnloadContent will be called once per game and is the place to unload
      /// all content.
      /// </summary>
      protected override void UnloadContent()
      {
         level.Dispose();
      }

      /// <summary>
      /// Allows the game to run logic such as updating the world,
      /// checking for collisions, gathering input, and playing audio.
      /// </summary>
      /// <param name="gameTime">Provides a snapshot of timing values.</param>
      protected override void Update(GameTime gameTime)
      {
         if (ExitGame || Options.Lives == 0)
         {
            //this.Exit();
            Options.Level = 0;
            Options.Lives = 3;
            LoadNextLevel(Options.Level);
         }

         if (!level.Player.IsAlive)
         {
            level.StartNewLife();
            ReloadCurrentLevel();
         }

         if (!isLoading || Options.Level == 0)
            level.Update(gameTime);

         Options.Score = level.Player.Score;
         Options.Coins = level.Player.Coins;
         Options.Lives = level.Player.Lives;
         Options.Size = level.Player.Size;
         Options.Level = level.LevelNumber;

         base.Update(gameTime);
      }

      public void LoadNextLevel(int levelIndex)
      {
         // Unloads the content for the current level before loading the next one.
         if (level != null)
            level.Dispose();

         string[] layers = new string[LAYERS];
         for (int i = 0; i < LAYERS; i++)
         {
            // Load the level.
            string levelPath = string.Format("Content/Levels/{0}{1}.txt", levelIndex, i);
            layers[i] = Path.GetFullPath(levelPath);
         }
         level = new Level(Services, layers, levelIndex, this);
         isLoading = true;
      }

      private void ReloadCurrentLevel()
      {
         LoadNextLevel(Options.Level);
      }

      /// <summary>
      /// This is called when the game should draw itself.
      /// </summary>
      /// <param name="gameTime">Provides a snapshot of timing values.</param>
      protected override void Draw(GameTime gameTime)
      {
         GraphicsDevice.Clear(Color.CornflowerBlue);

         if (isLoading && Options.Level > 0)
         {
            DrawLoadingScreen();
            loadingTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (loadingTime > LOADING_TIME)
            {
               isLoading = false;
               loadingTime = 0;
            }
         }
         else
         {

            level.Draw(gameTime, spriteBatch);

            if (Options.Level > 0)
               statusText.Draw(gameTime, spriteBatch);
         }

         base.Draw(gameTime);
      }
      
      private void DrawLoadingScreen()
      {
         Vector2 position = new Vector2(spriteBatch.GraphicsDevice.Viewport.Width / 2.0f,
            spriteBatch.GraphicsDevice.Viewport.Height / 2.0f);
         Vector2 origin = new Vector2(loading.Width / 2.0f, loading.Height / 2.0f);

         GraphicsDevice.Clear(Color.Black);
         
         spriteBatch.Begin();
         spriteBatch.Draw(loading, position, null, Color.White, 0.0f, origin, 1.0f, SpriteEffects.None, 0.0f);
         spriteBatch.End();
      }
   }
}
