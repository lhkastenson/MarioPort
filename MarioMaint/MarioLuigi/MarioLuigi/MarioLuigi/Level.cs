using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using System.IO;

namespace MarioLuigi
{
   /// <summary>
   /// A uniform grid of tiles with collections of collectables and enemies.
   /// The level owns the player and controls the game's win and lose
   /// conditions as well as scoring.
   /// </summary>
   class Level : IDisposable
   {
      private Menu menu;
      private Platform main;
      private KeyboardState curKeyState, prevKeyState;

      // Physical structure of the level.
      private Tile[,] tiles;
      private List<Tile[,]> layers = new List<Tile[,]>();

      // The background image of the level
      public ParallaxBackground Background;

      // Entities in the level.
      public Player Player
      {
         get { return player; }
      }
      Player player;

      private List<Collectable> collectables = new List<Collectable>();
      private List<Enemy> enemies = new List<Enemy>();
      private Random random = new Random(34687);

      private List<NoteBox> notes = new List<NoteBox>();
      private List<Animation> animations = new List<Animation>();
      private List<Vector2> locations = new List<Vector2>();
      private List<AnimationPlayer> sprites = new List<AnimationPlayer>();

      // Key locations in the level.        
      private Vector2 start;
      private Point exit = InvalidPosition;
      private List<Point> exits = new List<Point>();
      private List<Point> ports = new List<Point>();
      private static readonly Point InvalidPosition = new Point(-1, -1);

      private float cameraPosition;

      public int LevelNumber
      {
         get { return levelNumber; }
      }
      int levelNumber;

      public bool ReachedExit
      {
         get { return reachedExit; }
      }
      bool reachedExit;

      // Level content.        
      public ContentManager Content
      {
         get { return content; }
      }
      ContentManager content;

      #region Loading

      /// <summary>
      /// Constructs a new level.
      /// </summary>
      /// <param name="serviceProvider">
      /// The service provider that will be used to construct a ContentManager.
      /// </param>
      /// <param name="layers">
      /// The absolute path to the level file to be loaded.
      /// </param>
      public Level(IServiceProvider serviceProvider, string[] layers, int levelNumber, Platform main)
      {
         // Create a new content manager to load content used just by this level.
         content = new ContentManager(serviceProvider, "Content");

         this.main = main;

         this.levelNumber = levelNumber;

         // Create the menu screen
         if (LevelNumber == 0)
            menu = new Menu(content, 640, 364);

         // Create the background of this level
         switch (LevelNumber)
         {
            case 0:
               Background = new ParallaxBackground(Content, "Backgrounds/SKY1", 0.0f);
               break;
            case 1:
               Background = new ParallaxBackground(Content, "Backgrounds/SKY1", 0.1f);
               break;
            case 2:
               Background = new ParallaxBackground(Content, "Backgrounds/BRICK2", 0.1f);
               break;
            case 3:
               Background = new ParallaxBackground(Content, "Backgrounds/SKY3", 0.1f);
               break;
            case 4:
               Background = new ParallaxBackground(Content, "Backgrounds/SKY4", 0.1f);
               break;
            case 5:
               Background = new ParallaxBackground(Content, "Backgrounds/SKY5", 0.1f);
               break;
            case 6:
               Background = new ParallaxBackground(Content, "Backgrounds/PANE6", 0.1f);
               break;
         }  
         // Load the world one tile at a time, one layer at a time
         for (int i = 0; i < layers.Length; i++)
            LoadTiles(layers[i]);

         if (LevelNumber > 0)
         {
            // Verify that the level has a beginning and an end.
            if (Player == null)
               throw new NotSupportedException("A level must have a starting point.");
            if (exit == InvalidPosition)
               throw new NotSupportedException("A level must have an exit.");
         }

         // Start the animation of each animated tile
         for (int i = 0; i < animations.Count; i++)
            sprites[i].PlayAnimation(animations[i]);
      }

      /// <summary>
      /// Iterates over every tile in the structure file and loads its
      /// appearance and behavior. This method also validates that the
      /// file is well-formed with a player start point, exit, etc.
      /// </summary>
      /// <param name="path">
      /// The absolute path to the level file to be loaded.
      /// </param>
      private void LoadTiles(string path)
      {
         // Load the level and ensure all of the lines are the same length.
         int width;
         List<string> lines = new List<string>();
         using (StreamReader reader = new StreamReader(path))
         {
            string line = reader.ReadLine();
            width = line.Length;
            while (line != null)
            {
               lines.Add(line);
               if (line.Length != width)
                  throw new Exception(String.Format("The length of line {0} is different from all preceeding lines.", lines.Count));
               line = reader.ReadLine();
            }
         }

         // Allocate the tile grid.
         tiles = new Tile[width, lines.Count];

         // Loop over every tile position,
         for (int y = 0; y < Height; ++y)
         {
               for (int x = 0; x < Width; ++x)
               {
                  // to load each tile.
                  char tileType = lines[y][x];
                  tiles[x, y] = LoadTile(tileType, x, y);
               }
         }
         layers.Add(tiles);
      }

      /// <summary>
      /// Loads an individual tile's appearance and behavior.
      /// </summary>
      /// <param name="tileType">
      /// The character loaded from the structure file which
      /// indicates what should be loaded.
      /// </param>
      /// <param name="x">
      /// The X location of this tile in tile space.
      /// </param>
      /// <param name="y">
      /// The Y location of this tile in tile space.
      /// </param>
      /// <returns>The loaded tile.</returns>
      private Tile LoadTile(char tileType, int x, int y)
      {
         SpriteEffects effects = SpriteEffects.None;
         switch (tileType)
         {
            case '.': // Blank space
               return new Tile(null, TileCollision.Passable, effects, false); // working

            case '0':
               return LoadTile("PIPE000", TileCollision.Impassable, effects); // working

            case '1':
               return LoadTile("PIPE001", TileCollision.Impassable, effects); // working
            
            case '2':
               return LoadTile("PIPE002", TileCollision.Impassable, effects); // working

            case '3':
               return LoadTile("PIPE003", TileCollision.Impassable, effects); // working

            case '4':
               return LoadAnimationTile(x, y, "FALL000"); // working

            case '5':
               return LoadTile("SAND014", TileCollision.Passable, effects); // working

            case '6':
               return LoadAnimationTile(x, y, "LAVA000");

            case '7':
               return LoadTile("TREE001", TileCollision.Passable, effects); // working

            case '8':
               return LoadTile("TREE003", TileCollision.Passable, effects); // working

            case '9':
               return LoadAnimationTile(x, y, "LAVA2001");

            case 'a':
               return LoadEnemyTile(x, y, "fireball");

            case 'b':
               return LoadTile("QUEST002", TileCollision.Hidden, effects);

            case 'c':
               return LoadAnimationTile(x, y, "FALL001");

            case 'd':
               return LoadTile("PIN001", TileCollision.Death, effects); // working

            case 'e':
               return LoadAnimationTile(x, y, "LAVA001");

            case 'f':
               return LoadTile("TREE000", TileCollision.Passable, effects); // working

            case 'g':
               return LoadTile("TREE002", TileCollision.Passable, effects); // working

            case 'h':
               return LoadTile("LAVA2000", TileCollision.Passable, effects);

            case 'i':
               return LoadCollectableTile(x, y, "coin"); // coin

            case 'j':
               return LoadCollectableTile(x, y, "coin_question"); // working

            case 'k':
               return LoadCollectableTile(x, y, "power_question"); // working

            case 'l':
               return LoadEnemyTile(x, y, "koopa");

            case 'm':
               return LoadCollectableTile(x, y, "poison_question"); // working

            case 'n':
               return LoadCollectableTile(x, y, "star_question"); // working

            case 'o':
               return LoadTile("BROWN010", TileCollision.Platform, effects); // working

            case 'p':
               return LoadTile("GREEN010", TileCollision.Platform, effects); // working

            case 'q':
               return LoadEnemyTile(x, y, "goomba");

            case 'r':
               return LoadTile("GREEN012", TileCollision.Passable, effects); // working

            case 's':
               return LoadTile("PIN000", TileCollision.Death, effects); // working

            case 't':
               return LoadTile("DONUT000", TileCollision.Impassable, effects); // working

            case 'u':
               return LoadAnimationTile(x, y, "GRASS1000"); // working

            case '|':
               return LoadAnimationTile(x, y, "GRASS2000"); // working

            case 'v':
               return LoadAnimationTile(x, y, "GRASS3000"); // working

            case 'w':
               return LoadTile("BROWN012", TileCollision.Passable, effects); // working

            case 'x':
               return LoadEnemyTile(x, y, "plant");

            case 'y':
               return LoadEnemyTile(x, y, "REDPLANT");

            case 'z':
               return LoadEnemyTile(x, y, "red");

            case 'A':
               return LoadEnemyTile(x, y, "REDKOOPAACTIVE");

            case 'B':
               return LoadTile("SAND010", TileCollision.Platform, effects); // working

            case 'C':
               return LoadTile("BROWN000", TileCollision.Platform, effects); // working

            case 'D':
               return LoadTile("BROWN001", TileCollision.Platform, effects); // working

            case 'E':
               return LoadTile("BROWN002", TileCollision.Passable, effects); // working

            case 'F':
               return LoadTile("BROWN003", TileCollision.Passable, effects); // working

            case 'G':
               return LoadTile("BROWN004", TileCollision.Passable, effects); // working

            case 'H':
               return LoadTile("BRICK1000", TileCollision.Impassable, effects); // working

            case 'I':
               return LoadTile("BRICK1001", TileCollision.Impassable, effects); // working

            case 'J':
               return LoadTile("BRICK1002", TileCollision.Impassable, effects); // working

            case 'K':
               return LoadTile("SAND000", TileCollision.Platform, effects); // working

            case 'L':
               return LoadTile("SAND001", TileCollision.Platform, effects); // working

            case 'M':
               return LoadTile("SAND002", TileCollision.Platform, effects); // working

            case 'N':
               return LoadTile("SAND003", TileCollision.Passable, effects); // working

            case 'O':
               return LoadTile("SAND004", TileCollision.Passable, effects); // working

            case 'P':
               return LoadTile("GREEN000", TileCollision.Platform, effects); // working

            case 'Q':
               return LoadTile("GREEN001", TileCollision.Platform, effects); // working

            case 'R':
               return LoadTile("GREEN002", TileCollision.Passable, effects); // working

            case 'S':
               return LoadTile("GREEN003", TileCollision.Passable, effects); // working

            case 'T':
               return LoadTile("GREEN004", TileCollision.Passable, effects); // working

            case 'U':
               return LoadCollectableTile(x, y, "life_question"); // life

            case 'V':
               return LoadCollectableTile(x, y, "champ"); // champ

            case 'W':
               return LoadCollectableTile(x, y, "flower"); // flower

            case 'X':
               return LoadCollectableTile(x, y, "star"); // star

            case 'Y':
               return LoadTile("BROWN012", TileCollision.Passable, effects); // working

            case 'Z':
               return LoadExitTile(x, y); // working

            case '!':
               return LoadTile("SMTREE000", TileCollision.Passable, effects); // working

            case '@':
               return LoadTile("SMTREE001", TileCollision.Passable, effects); // working

            case '#':
               return LoadTile("BLOCK000", TileCollision.Impassable, effects); // working

            case '$':
               return LoadCollectableTile(x, y, "poison"); // poison

            case '%':
               return LoadCollectableTile(x, y, "block"); //LoadTile("BLOCK001", TileCollision.Impassable, effects); // working

            case '^':
               return LoadNoteTile(x, y, "NOTE000");

            case '&':
               return LoadAnimationTile(x, y, "PALM1000");

            case '*':
               return LoadTile("WPALM000", TileCollision.Passable, effects); // working

            case '-':
               return LoadAnimationTile(x, y, "PALM3000");

            case '(':
               return LoadTile("EXIT000", TileCollision.Passable, effects); // working

            case ')':
               return LoadTile("EXIT001", TileCollision.Passable, effects); // working

            case '=':
               return LoadAnimationTile(x, y, "PALM0000");

            case '_':
               return LoadAnimationTile(x, y, "PALM2000");

            case '+':
               return LoadTile("WOOD000", TileCollision.Impassable, effects); // working

            case ',':
               return LoadTile("XBLOCK000", TileCollision.Impassable, effects); // working

            case '<':
               return LoadEnemyTile(x, y, "fish");

            case '{':
               return LoadTile("INTRO000", TileCollision.Impassable, effects); // working

            case '`':
               return LoadTile("INTRO001", TileCollision.Impassable, effects); // working

            case '}':
               return LoadTile("INTRO002", TileCollision.Impassable, effects); // working

            case '~': // mario starting position
               return LoadStartTile(x, y); // working

            default: // Unknown tile type character
               throw new NotSupportedException(String.Format("Unsupported tile type character '{0}' at position {1}, {2}.", tileType, x, y));
         }
      }

      /// <summary>
      /// Creates a new tile. The other tile loading methods typically chain to this
      /// method after performing their special logic.
      /// </summary>
      /// <param name="name">
      /// Path to a tile texture relative to the Content/Tiles directory.
      /// </param>
      /// <param name="collision">
      /// The tile collision type for the new tile.
      /// </param>
      /// <returns>The new tile.</returns>
      private Tile LoadTile(string name, TileCollision collision, SpriteEffects effect)
      {
         return new Tile(Content.Load<Texture2D>("Tiles/" + name), collision, effect, true);
      }

      /// <summary>
      /// Instantiates a player, puts him in the level, and remembers where to put him when he is resurrected.
      /// </summary>
      private Tile LoadStartTile(int x, int y)
      {
         if (Player != null)
               throw new NotSupportedException("A level may only have one starting point.");

         start = RectangleExtensions.GetBottomCenter(GetBounds(x, y));
         player = new Player(this, start);

         return new Tile(null, TileCollision.Passable, SpriteEffects.None, false);
      }

      /// <summary>
      /// Remembers the location of the level's exit.
      /// </summary>
      private Tile LoadExitTile(int x, int y)
      {
         exit = GetBounds(x, y).Center;
         exits.Add(exit);

         return new Tile(null, TileCollision.Passable, SpriteEffects.None, false);
      }

      private Tile LoadPortTile(int x, int y)
      {
         ports.Add(GetBounds(x, y).Center);

         return new Tile(null, TileCollision.Passable, SpriteEffects.None, false);
      }

      private Tile LoadAnimationTile(int x, int y, string spriteSet)
      {
         Vector2 position = RectangleExtensions.GetBottomCenter(GetBounds(x, y));
         Texture2D t = (Content.Load<Texture2D>("Sprites/Animations/" + spriteSet));
         Animation animation = new Animation(t, .45f, true, t.Width / Tile.Width);
         AnimationPlayer sprite = new AnimationPlayer();

         animations.Add(animation);
         locations.Add(position);
         sprites.Add(sprite);

         return new Tile(null, TileCollision.Passable, SpriteEffects.None, false);
      }

      /// <summary>
      /// Instantiates an enemy and puts him in the level.
      /// </summary>
      private Tile LoadEnemyTile(int x, int y, string sprite)
      {
         Vector2 position = RectangleExtensions.GetBottomCenter(GetBounds(x, y));
         SpriteEffects effects = SpriteEffects.None;
         switch (sprite)
         {
            case "koopa":
               enemies.Add(new Koopa(this, position, effects));
               return new Tile(null, TileCollision.Passable, SpriteEffects.None, false);

            case "goomba":
               enemies.Add(new Goomba(this, position, effects));
               return new Tile(null, TileCollision.Passable, SpriteEffects.None, false);

            case "red":
               enemies.Add(new Red(this, position, effects));
               return new Tile(null, TileCollision.Passable, SpriteEffects.None, false);

            case "plant":
               enemies.Add(new PiranhaPlant(this, position, effects, random.Next(5000)));
               return new Tile(null, TileCollision.Passable, SpriteEffects.None, false);

            case "fish":
               enemies.Add(new Fish(this, position, effects, random.Next(5000)));
               return new Tile(null, TileCollision.Passable, SpriteEffects.None, false);

            case "fireball":
               enemies.Add(new LavaBall(this, position, effects, random.Next(5000)));
               return new Tile(null, TileCollision.Passable, SpriteEffects.None, false);

            default:
               return new Tile(null, TileCollision.Passable, SpriteEffects.None, false);
         }
      }

      private Tile LoadNoteTile(int x, int y, string spritePath)
      {
         Point position = GetBounds(x, y).Center;
         notes.Add(new NoteBox(this, new Vector2(position.X, position.Y)));
         return new Tile(null, TileCollision.Impassable, SpriteEffects.None, true);
      }

      /// <summary>
      /// Instantiates a collectable and puts it in the level.
      /// </summary>
      private Tile LoadCollectableTile(int x, int y, string type)
      {
         Point position = GetBounds(x, y).Center;
         switch (type)
         {
            case "coin":
               collectables.Add(new Coin(this, new Vector2(position.X, position.Y), x, y));
               return new Tile(null, TileCollision.Passable, SpriteEffects.None, true);

            case "coin_question":
               collectables.Add(new CoinBox(this, new Vector2(position.X, position.Y), x, y));
               return new Tile(null, TileCollision.Impassable, SpriteEffects.None, true);

            case "power_question":
               collectables.Add(new PowerBox(this, new Vector2(position.X, position.Y), x, y));
               return new Tile(null, TileCollision.Impassable, SpriteEffects.None, true);

            case "life_question":
               collectables.Add(new LifeBox(this, new Vector2(position.X, position.Y), x, y));
               return new Tile(null, TileCollision.Impassable, SpriteEffects.None, false);

            case "poison_question":
               collectables.Add(new PoisonBox(this, new Vector2(position.X, position.Y), x, y));
               return new Tile(null, TileCollision.Impassable, SpriteEffects.None, true);

            case "star_question":
               collectables.Add(new StarBox(this, new Vector2(position.X, position.Y), x, y));
               return new Tile(null, TileCollision.Impassable, SpriteEffects.None, true);

            case "life":
               collectables.Add(new Life(this, new Vector2(position.X, position.Y), x, y));
               return new Tile(null, TileCollision.Passable, SpriteEffects.None, true);

            case "champ":
               collectables.Add(new Champ(this, new Vector2(position.X, position.Y), x, y));
               return new Tile(null, TileCollision.Passable, SpriteEffects.None, true);

            case "flower":
               collectables.Add(new Flower(this, new Vector2(position.X, position.Y), x, y));
               return new Tile(null, TileCollision.Passable, SpriteEffects.None, true);

            case "star":
               collectables.Add(new Star(this, new Vector2(position.X, position.Y), x, y));
               return new Tile(null, TileCollision.Passable, SpriteEffects.None, true);

            case "poison":
               collectables.Add(new Poison(this, new Vector2(position.X, position.Y), x, y));
               return new Tile(null, TileCollision.Passable, SpriteEffects.None, true);
            
            case "block":
               collectables.Add(new Block(this, new Vector2(position.X, position.Y), x, y));
               return new Tile(null, TileCollision.Impassable, SpriteEffects.None, true);

            default:
               return new Tile(null, TileCollision.Passable, SpriteEffects.None, false);
         }
      }

      /// <summary>
      /// Unloads the level content.
      /// </summary>
      public void Dispose()
      {
         Content.Unload();
      }

      #endregion

      #region Bounds and collision

      /// <summary>
      /// Gets the collision mode of the tile at a particular location.
      /// This method handles tiles outside of the levels boundries by making it
      /// impossible to escape past the left or right edges, but allowing things
      /// to jump beyond the top of the level and fall off the bottom.
      /// </summary>
      public TileCollision GetCollision(int x, int y)
      {
         // Prevent escaping past the level ends.
         if (x < 0 || x >= Width)
            return TileCollision.Impassable;
         // Allow jumping past the level top and falling through the bottom.
         if (y < 0 || y >= Height)
            return TileCollision.Passable;

         return layers[0][x, y].Collision;
      }

      /// <summary>
      /// Gets the bounding rectangle of a tile in world space.
      /// </summary>        
      public Rectangle GetBounds(int x, int y)
      {
         return new Rectangle(x * Tile.Width, y * Tile.Height, Tile.Width, Tile.Height);
      }

      /// <summary>
      /// Width of level measured in tiles.
      /// </summary>
      public int Width
      {
         get { return tiles.GetLength(0); }
      }

      /// <summary>
      /// Height of the level measured in tiles.
      /// </summary>
      public int Height
      {
         get { return tiles.GetLength(1); }
      }

      #endregion

      #region Update

      /// <summary>
      /// Updates all objects in the world, performs collision between them,
      /// and handles the time limit with scoring.
      /// </summary>
      public void Update(GameTime gameTime)
      {
         // Get the keys currently and previosly being pressed
         prevKeyState = curKeyState;
         curKeyState = Keyboard.GetState();

         // Toggle the status text
         if (curKeyState.IsKeyDown(Keys.S) && !prevKeyState.IsKeyDown(Keys.S))
            Options.Text = !Options.Text;

         // Pause while the player is dead or time is expired.
         if (!Player.IsAlive)
         {
            // Still want to perform physics on the player.
            Player.ApplyPhysics(gameTime);
         }
         else if (ReachedExit)
         {
            LoadNewLevel();
         }
         else
         {
            if (Player.Coins == 100)
            {
               Player.Lives++;
               Player.Coins = 0;
            }

            if (LevelNumber == 0)
               menu.Update(gameTime, ref reachedExit);
            else
            {
               Player.Update(gameTime);

               UpdateCollectables(gameTime);

               UpdateNotes(gameTime);

               // Falling off the bottom of the level kills the player.
               if (Player.BoundingRectangle.Top >= Height * Tile.Height)
                  OnPlayerKilled(null);

               UpdateEnemies(gameTime);

               // Check to see if player is standing on one of the exit points
               bool onExit = false;
               foreach (Point point in exits)
                  if (Player.BoundingRectangle.Contains(point))
                     onExit = true;

               // The player has reached the exit if they are standing on the ground and
               // his bounding rectangle contains the center of the exit tile. They can only
               // exit when they have collected all of the collectables.
               if (Player.IsAlive && Player.IsOnGround 
                  && onExit && curKeyState.IsKeyDown(Keys.Down)
                  && !prevKeyState.IsKeyDown(Keys.Down))
                     OnExitReached();
            }
         }
      }

      private void LoadNewLevel()
      {
         switch (LevelNumber)
         {
            case -1:
               main.LoadNextLevel(0);
               Options.Level = 0;
               break;

            case 0:
               main.LoadNextLevel(1);
               Options.Level = 1;
               break;

            case 1:
               main.LoadNextLevel(2);
               Options.Level = 2;
               break;

            case 2:
               main.LoadNextLevel(3);
               Options.Level = 3;
               break;

            case 3:
               main.LoadNextLevel(4);
               Options.Level = 4;
               break;

            case 4:
               main.LoadNextLevel(5);
               Options.Level = 5;
               break;

            case 5:
               main.LoadNextLevel(6);
               Options.Level = 6;
               break;
         }
      }

      private void UpdateNotes(GameTime gameTime)
      {
         foreach (NoteBox note in notes)
            if (Player.BoundingRectangle.Intersects(note.BoundingRectangle))
               note.OnCollision(Player);
      }

      /// <summary>
      /// Animates each collectable and checks to allows the player to collect them.
      /// </summary>
      private void UpdateCollectables(GameTime gameTime)
      {
         for (int i = 0; i < collectables.Count; ++i)
         {
            Collectable collectable = collectables[i];

            collectable.Update(gameTime);

            if (collectable is CoinBox || collectable is PowerBox 
               || collectable is PoisonBox || collectable is StarBox 
               || collectable is LifeBox)
            {
               if (collectable.BoundingRectangle.Intersects(Player.BoundingRectangle))
                  OnCollectableCollected(collectable, Player);
            }
            else if (collectable is Block)
            {
               if (collectable.BoundingRectangle.Intersects(Player.BoundingRectangle))
               {
                  OnCollectableCollected(collectable, Player);
                  if (Player.Size > 0)
                  {
                     layers[0][collectable.X, collectable.Y].Collision = TileCollision.Passable;
                     layers[0][collectable.X, collectable.Y].Visible = false;
                     collectables.RemoveAt(i--);
                  }
               }
            }
            else
            {
               if (collectable.BoundingCircle.Intersects(Player.BoundingRectangle) && collectable.IsActive)
               {
                  collectables.RemoveAt(i--);
                  OnCollectableCollected(collectable, Player);
               }
            }
         }
      }

      /// <summary>
      /// Animates each enemy and allow them to kill the player.
      /// </summary>
      private void UpdateEnemies(GameTime gameTime)
      {
         for(int i = 0; i < enemies.Count; ++i)
         {
            Enemy enemy = enemies[i];

            enemy.Update(gameTime);

            foreach (Projectile fireball in Player.FireBalls)
               if (fireball.BoundingRectangle.Intersects(enemy.BoundingRectangle))
                  fireball.OnCollision(enemy);

            if (enemy.State == State.Dead)
               enemies.RemoveAt(i--);

            if (enemy.BoundingRectangle.Intersects(Player.BoundingRectangle))
               enemy.OnCollision(Player);
         }
      }

      /// <summary>
      /// Adds a collectable to the level at the given coordinates
      /// </summary>
      /// <param name="collectable"></param>
      /// <param name="x"></param>
      /// <param name="y"></param>
      public void AddCollectable(Collectable collectable)
      {
         // Add to front of list so it is drawn first
         collectables.Insert(0, collectable);
      }

      /// <summary>
      /// Called when a collectable is collected.
      /// </summary>
      /// <param name="collectable">The collectable that was collected.</param>
      /// <param name="collectedBy">The player who collected this collectable.</param>
      private void OnCollectableCollected(Collectable collectable, Player collectedBy)
      {
         Player.PreviousSize = Player.Size;
         collectable.OnCollected(collectedBy);
         if (Player.PreviousSize != Player.Size)
            Player.LoadContent();
      }

      /// <summary>
      /// Called when the player is killed.
      /// </summary>
      /// <param name="killedBy">
      /// The enemy who killed the player. This is null if the player was not killed by an
      /// enemy, such as when a player falls into a hole.
      /// </param>
      private void OnPlayerKilled(Enemy killedBy)
      {
         Player.OnKilled(killedBy);
      }

      /// <summary>
      /// Called when the player reaches the level's exit.
      /// </summary>
      private void OnExitReached()
      {
         Player.OnReachedExit();
         reachedExit = true;
      }

      /// <summary>
      /// Restores the player to the starting point to try the level again.
      /// </summary>
      public void StartNewLife()
      {
         Player.Reset(start);
      }

      #endregion

      #region Draw

      /// <summary>
      /// Draw everything in the level from background to foreground.
      /// </summary>
      public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
      {
         spriteBatch.Begin();
         Background.Draw(spriteBatch, cameraPosition);
         spriteBatch.End();

         ScrollCamera(spriteBatch.GraphicsDevice.Viewport);
         Matrix cameraTransform = Matrix.CreateTranslation(-cameraPosition, 0.0f, 0.0f);
         spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, 
            DepthStencilState.Default, RasterizerState.CullCounterClockwise, null, cameraTransform);

         foreach (Enemy enemy in enemies)
            if (enemy is PiranhaPlant || enemy is Fish || enemy is LavaBall)
               enemy.Draw(gameTime, spriteBatch);

         DrawTiles(spriteBatch, gameTime);

         for (int i = 0; i < animations.Count; i++)
            sprites[i].Draw(gameTime, spriteBatch, locations[i], SpriteEffects.None, false);

         if (LevelNumber == 0)
            menu.Draw(gameTime, spriteBatch);

         foreach (NoteBox note in notes)
            note.Draw(gameTime, spriteBatch);

         foreach (Collectable collectable in collectables)
               collectable.Draw(gameTime, spriteBatch);

         foreach (Enemy enemy in enemies)
            if (!(enemy is PiranhaPlant) && !(enemy is Fish) && !(enemy is LavaBall))
               enemy.Draw(gameTime, spriteBatch);

         Player.Draw(gameTime, spriteBatch);

         spriteBatch.End();
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="viewport"></param>
      private void ScrollCamera(Viewport viewport)
      {
         const float ViewMargin = 0.33f;

         // Calculate the edges of the screen.
         float marginWidth = viewport.Width * ViewMargin;
         float marginLeft = cameraPosition + marginWidth;
         float marginRight = cameraPosition + viewport.Width - marginWidth;

         // Calculate how far to scroll when the player is near the edges of the screen.
         float cameraMovement = 0.0f;
         if (Player.Position.X < marginLeft)
            cameraMovement = Player.Position.X - marginLeft;
         else if (Player.Position.X > marginRight)
            cameraMovement = Player.Position.X - marginRight;

         // Update the camera position, but prevent scrolling off the ends of the level.
         float maxCameraPosition = Tile.Width * Width - viewport.Width;
         cameraPosition = MathHelper.Clamp(cameraPosition + cameraMovement, 0.0f, maxCameraPosition);
      }

      /// <summary>
      /// Draws each tile in the level.
      /// </summary>
      private void DrawTiles(SpriteBatch spriteBatch, GameTime gameTime)
      {
         // Calculate the visible range of tiles.
         int left = (int)Math.Floor(cameraPosition / Tile.Width);
         int right = left + spriteBatch.GraphicsDevice.Viewport.Width / Tile.Width;
         right = Math.Min(right, Width - 1);

         // For each layer
         foreach (var layer in layers)
         {
            // For each tile position
            for (int y = 0; y < Height; ++y)
            {
               for (int x = left; x < right + 1; ++x)
               {
                  // If there is a visible tile in that position
                  Texture2D texture = layer[x, y].Texture;
                  if (texture != null && layer[x, y].Visible)
                  {
                     // Draw it in screen space.
                     Vector2 position = new Vector2(x, y) * Tile.Size;
                     SpriteEffects effects = layer[x, y].Effects;
                     spriteBatch.Draw(texture, position, null, Color.White, 0f, Vector2.Zero, 1f, effects, 0f); 
                  }
               }
            }
         }
      }
      
      #endregion
   }
}
