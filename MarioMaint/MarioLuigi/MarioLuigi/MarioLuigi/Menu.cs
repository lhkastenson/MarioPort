using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MarioLuigi
{
   class Menu
   {
      #region Variables

      private const int IMAGE_WIDTH = 40;
      private const int IMAGE_HEIGHT = 24;
      private const string START = "START";
      private const string OPTIONS = "OPTIONS";
      private const string END = "END";
      private const string SOUND_ON = "SOUND ON";
      private const string STATUS_ON = "STATUSLINE ON";
      private const string SOUND_OFF = "SOUND OFF";
      private const string STATUS_OFF = "STATUSLINE OFF";
      private const string NO_SAVE = "NO SAVE";
      private const string SAVE_SELECT = "GAME SELECT";
      private const string ERASE = "ERASE";
      private const string ONE_PLAYER = "ONE PLAYER";
      private const string TWO_PLAYER = "TWO PLAYERS";
      private string SAVE_SLOT_ONE = "GAME #1 - EMPTY";
      private string SAVE_SLOT_TWO = "GAME #2 - EMPTY";
      private string SAVE_SLOT_THREE = "GAME #3 - EMPTY";

      private enum MenuScreen { Main, Options, GameSelect, Players, SaveSelect }
      private enum ArrowLocation { Top, Center, Bottom }

      private ArrowLocation arrowLocation;
      private MenuScreen menuScreen;
      private SpriteFont spriteFont;
      private ContentManager content;
      private KeyboardState currentKeyboardState, previousKeyboardState;

      private int stageWidth, stageHeight;

      #endregion Variables

      /// <summary>
      /// 
      /// </summary>
      /// <param name="content"></param>
      /// <param name="stageWidth"></param>
      /// <param name="stageHeight"></param>
      /// <param name="scale"></param>

      public Menu(ContentManager content, int stageWidth, int stageHeight)
      {
         this.stageHeight = stageHeight;
         this.stageWidth = stageWidth;
         this.content = content;
         spriteFont = this.content.Load<SpriteFont>("MenuFont");
         menuScreen = MenuScreen.Main;
         arrowLocation = ArrowLocation.Top;
         currentKeyboardState = previousKeyboardState = Keyboard.GetState();
      }

      /// <summary>
      /// 
      /// </summary>
      public void Reset()
      {
         menuScreen = MenuScreen.Main;
         arrowLocation = ArrowLocation.Top;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="gameTime"></param>
      /// <param name="currentKeyboardState"></param>
      /// <param name="previousKeyboardState"></param>
      public void Update(GameTime gameTime, ref bool start)
      {
         previousKeyboardState = currentKeyboardState;
         currentKeyboardState = Keyboard.GetState();
         MenuSelect(gameTime, currentKeyboardState, previousKeyboardState, ref start);
         MenuControl(gameTime, currentKeyboardState, previousKeyboardState);
      }

      /// <summary>
      /// Draws everything in the figure list and the text on the screen.
      /// </summary>
      /// <param name="gameTime"></param>
      /// <param name="spriteBatch"></param>
      public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
      {
         switch (menuScreen)
         {
            case MenuScreen.Main:
               DrawArrow(spriteBatch, 6.0f);
               DrawLine(spriteBatch, Color.White, START, 7.0f, 6.0f);
               DrawLine(spriteBatch, Color.White, OPTIONS, 7.0f, 7.0f);
               DrawLine(spriteBatch, Color.White, END, 7.0f, 8.0f);
               break;

            case MenuScreen.Options:
               DrawArrow(spriteBatch, 4.0f);
               if (Options.Sound)
                  DrawLine(spriteBatch, Color.White, SOUND_ON, 5.0f, 6.0f);
               else
                  DrawLine(spriteBatch, Color.White, SOUND_OFF, 5.0f, 6.0f);
               if (Options.Text)
                  DrawLine(spriteBatch, Color.White, STATUS_ON, 5.0f, 7.0f);
               else
                  DrawLine(spriteBatch, Color.White, STATUS_OFF, 5.0f, 7.0f);
               break;

            case MenuScreen.SaveSelect:
               DrawArrow(spriteBatch, 5.0f);
               DrawLine(spriteBatch, Color.White, NO_SAVE, 6.0f, 6.0f);
               DrawLine(spriteBatch, Color.White, SAVE_SELECT, 6.0f, 7.0f);
               DrawLine(spriteBatch, Color.White, ERASE, 6.0f, 8.0f);
               break;

            case MenuScreen.Players:
               DrawArrow(spriteBatch, 5.0f);
               DrawLine(spriteBatch, Color.White, ONE_PLAYER, 6.0f, 6.0f);
               DrawLine(spriteBatch, Color.White, TWO_PLAYER, 6.0f, 7.0f);
               break;

            case MenuScreen.GameSelect:
               DrawArrow(spriteBatch, 4.0f);
               DrawLine(spriteBatch, Color.White, SAVE_SLOT_ONE, 5.0f, 6.0f);
               DrawLine(spriteBatch, Color.White, SAVE_SLOT_TWO, 5.0f, 7.0f);
               DrawLine(spriteBatch, Color.White, SAVE_SLOT_THREE, 5.0f, 8.0f);
               break;
         }
      }

      /// <summary>
      /// Navigates the red arrow on the screen and changes the menu screen.
      /// </summary>
      /// <param name="gameTime"></param>
      /// <param name="curKeyPress"></param>
      /// <param name="lastKeyPress"></param>
      private void MenuSelect(GameTime gameTime, KeyboardState curKeyPress, KeyboardState lastKeyPress, ref bool start)
      {
         if (curKeyPress.IsKeyDown(Keys.Enter) && !lastKeyPress.IsKeyDown(Keys.Enter))
         {
            if (arrowLocation == ArrowLocation.Top)
            {
               if (menuScreen == MenuScreen.Main)
               {
                  arrowLocation = ArrowLocation.Top;
                  menuScreen = MenuScreen.SaveSelect; // start game
               }
               else if (menuScreen == MenuScreen.Options)
                  Options.Sound = !Options.Sound; // handle sound on/off
               else if (menuScreen == MenuScreen.SaveSelect)
               {
                  arrowLocation = ArrowLocation.Top;
                  menuScreen = MenuScreen.Players; // number of players
               }
               else if (menuScreen == MenuScreen.GameSelect)
                  LoadGame(); // load a saved game
               else if (menuScreen == MenuScreen.Players)
               {
                  Options.Players = 1;
                  start = true; // will have to change this later, 1 player
               }
            }
            else if (arrowLocation == ArrowLocation.Center)
            {
               if (menuScreen == MenuScreen.Main)
               {
                  arrowLocation = ArrowLocation.Top;
                  menuScreen = MenuScreen.Options; // select options
               }
               else if (menuScreen == MenuScreen.SaveSelect)
               {
                  arrowLocation = ArrowLocation.Top;
                  menuScreen = MenuScreen.GameSelect; // select save
               }
               else if (menuScreen == MenuScreen.Options)
                  Options.Text = !Options.Text; // handle status line on/off
               else if (menuScreen == MenuScreen.GameSelect)
                  LoadGame(); // load a saved game
               else if (menuScreen == MenuScreen.Players)
               {
                  Options.Players = 2;
                  start = true; // will have to change this later, 2 players
               }
            }
            else if (arrowLocation == ArrowLocation.Bottom)
            {
               if (menuScreen == MenuScreen.Main)
                  Platform.ExitGame = true; // exit game
               else if (menuScreen == MenuScreen.SaveSelect)
               {
                  arrowLocation = ArrowLocation.Top;
                  menuScreen = MenuScreen.GameSelect; // select a game
               }
               else if (menuScreen == MenuScreen.GameSelect)
                  LoadGame(); // load a saved game
            }
         }
         else if (curKeyPress.IsKeyDown(Keys.Escape) && !lastKeyPress.IsKeyDown(Keys.Escape))
         {
            if (menuScreen == MenuScreen.Main)
               Platform.ExitGame = true; // exit the game
            else if (menuScreen == MenuScreen.SaveSelect || menuScreen == MenuScreen.Options)
               menuScreen = MenuScreen.Main; // return to main
            else if (menuScreen == MenuScreen.Players || menuScreen == MenuScreen.GameSelect)
               menuScreen = MenuScreen.SaveSelect; // return to save selections
            arrowLocation = ArrowLocation.Top;
         }
      }

      /// <summary>
      /// These are the actions that take place for each menu entry.
      /// </summary>
      /// <param name="gameTime"></param>
      /// <param name="curKeyPress"></param>
      /// <param name="lastKeyPress"></param>
      /// <param name="numItems"></param>
      private void MenuControl(GameTime gameTime, KeyboardState curKeyPress, KeyboardState lastKeyPress)
      {
         if (menuScreen == MenuScreen.Options || menuScreen == MenuScreen.Players)
         {
            if (arrowLocation == ArrowLocation.Top && curKeyPress.IsKeyDown(Keys.Down) && !lastKeyPress.IsKeyDown(Keys.Down))
               arrowLocation = ArrowLocation.Center;

            else if (arrowLocation == ArrowLocation.Center && curKeyPress.IsKeyDown(Keys.Up) && !lastKeyPress.IsKeyDown(Keys.Up))
               arrowLocation = ArrowLocation.Top;
         }
         else
         {
            if (arrowLocation == ArrowLocation.Center && curKeyPress.IsKeyDown(Keys.Up) && !lastKeyPress.IsKeyDown(Keys.Up))
               arrowLocation = ArrowLocation.Top;

            else if (arrowLocation == ArrowLocation.Bottom && curKeyPress.IsKeyDown(Keys.Up) && !lastKeyPress.IsKeyDown(Keys.Up))
               arrowLocation = ArrowLocation.Center;

            else if (arrowLocation == ArrowLocation.Center && curKeyPress.IsKeyDown(Keys.Down) && !lastKeyPress.IsKeyDown(Keys.Down))
               arrowLocation = ArrowLocation.Bottom;

            else if (arrowLocation == ArrowLocation.Top && curKeyPress.IsKeyDown(Keys.Down) && !lastKeyPress.IsKeyDown(Keys.Down))
               arrowLocation = ArrowLocation.Center;
         }
      }

      /// <summary>
      /// Draws the red arrow on the screen.
      /// </summary>
      /// <param name="spriteBatch"></param>
      /// <param name="X"></param>
      private void DrawArrow(SpriteBatch spriteBatch, float X)
      {
         switch (arrowLocation)
         {
            case ArrowLocation.Top:
               DrawLine(spriteBatch, Color.Red, "->", X, 6.0f);
               break;

            case ArrowLocation.Center:
               DrawLine(spriteBatch, Color.Red, "->", X, 7.0f);
               break;

            case ArrowLocation.Bottom:
               DrawLine(spriteBatch, Color.Red, "->", X, 8.0f);
               break;
         }
      }

      /// <summary>
      /// Draws a line of text on the screen.
      /// </summary>
      /// <param name="spriteBatch"></param>
      /// <param name="color"></param>
      /// <param name="text"></param>
      /// <param name="X"></param>
      /// <param name="Y"></param>
      private void DrawLine(SpriteBatch spriteBatch, Color color, string text, float X, float Y)
      {
         spriteBatch.DrawString(spriteFont, text, new Vector2((IMAGE_WIDTH * X), (IMAGE_HEIGHT * Y)),
                  color, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
      }

      private void LoadGame()
      {
      }

      private void EraseGame()
      {
      }
   }
}
