using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MarioLuigi
{
   /// <summary>
   /// 
   /// </summary>
   class Player
   {
      // Current and previous state of the keyboard
      private KeyboardState curKeyState, prevKeyState;

      // Animations
      private Animation idleAnimation;
      private Animation runAnimation;
      private Animation jumpAnimation;
      //private Animation dieAnimation;
      private SpriteEffects flip = SpriteEffects.FlipHorizontally;
      private AnimationPlayer sprite = new AnimationPlayer();
   //{
   //   get { return 
   //}
      private List<Projectile> fireballs = new List<Projectile>();
      // Let other objects see the players projectiles
      public List<Projectile> FireBalls
      {
         get { return fireballs; }
      }

      // Sounds
      //private SoundEffect killedSound;
      //private SoundEffect jumpSound;

      public SpriteEffects Direction
      {
         get { return flip; }
      }

      public Level Level
      {
         get { return level; }
      }
      Level level;

      public bool IsAlive
      {
         get { return isAlive; }
         set { isAlive = value; }
      }
      bool isAlive;

      public int Size
      {
         get { return size; }
         set { size = value; }
      }
      int size = Options.Size;

      public const int MAX_SIZE = 2;

      public int PreviousSize
      {
         get { return prevSize; }
         set { prevSize = value; }
      }
      int prevSize;

      public int Lives
      {
         get { return lives; }
         set { lives = value; }
      }
      int lives = Options.Lives;

      public int Score
      {
         get { return score; }
         set { score = value; }
      }
      int score = Options.Score;

      public int Coins
      {
         get { return coins; }
         set { coins = value; }
      }
      int coins = Options.Coins;

      // Physics state
      public Vector2 Position
      {
         get { return position; }
         set { position = value; }
      }
      Vector2 position;

      private float previousBottom;

      public Vector2 Velocity
      {
         get { return velocity; }
         set { velocity = value; }
      }
      Vector2 velocity;

      // Constants for controling horizontal movement
      private const float MoveAcceleration = 10000.0f;
      private const float TurboAcceleration = 15000.0f;
      private const float MaxMoveSpeed = 500.0f;
      private const float GroundDragFactor = 0.58f;
      private const float AirDragFactor = 0.65f;

      // Constants for controlling vertical movement
      private const float MaxJumpTime = 0.42f;
      private const float JumpLaunchVelocity = -3000.0f;
      private const float GravityAcceleration = 2800.0f;
      private const float MaxFallSpeed = 500.0f;
      private const float JumpControlPower = 0.14f;

      /// <summary>
      /// Gets whether or not the player's feet are on the ground.
      /// </summary>
      public bool FirstBounce
      {
         set { firstBounce = value; }
      }
      bool firstBounce = false;

      /// <summary>
      /// Gets whether or not the player's feet are on the ground.
      /// </summary>
      public int NoteBounceTicks
      {
         set { noteBounceTicks = value; }
      }
      int noteBounceTicks = 0;

      /// <summary>
      /// Gets whether or not the player's feet are on the ground.
      /// </summary>
      public bool NoteBounce
      {
         set { noteBounce = value; }
      }
      bool noteBounce = false;

      /// <summary>
      /// Gets whether or not the player's feet are on the ground.
      /// </summary>
      public bool IsOnGround
      {
         get { return isOnGround; }
      }
      bool isOnGround;

      /// <summary>
      /// Gets or sets if the player is invinsible.
      /// </summary>
      public bool Invinsible
      {
         get { return invinsible; }
         set { invinsible = value; }
      }
      bool invinsible = false;

      private const float STAR_TIMER = 10f;
      private float starTimer = 0f;

      /// <summary>
      /// Gets or sets if the player is invinsible.
      /// </summary>
      public bool WasHit
      {
         get { return wasHit; }
         set { wasHit = value; }
      }
      bool wasHit = false;

      private const float HIT_TIMER = 2.0f;
      private float hitTimer = 0f;

      /// <summary>
      /// Current user movement input.
      /// </summary>
      private float movement;

      public bool IsRunning
      {
         get { return isRunning; }
         set { isRunning = value; }
      }
      bool isRunning = false;

      public bool IsHolding
      {
         get { return isHolding; }
         set { isHolding = value; }
      }
      bool isHolding = false;
      // Jumping state
      private bool isJumping;
      private bool wasJumping;
      private float jumpTime;
      public bool BounceJump
      {
         set { bounceJump = value; }
      }
      bool bounceJump = false;

      private Rectangle localBounds;
      /// <summary>
      /// Gets a rectangle which bounds this player in world space.
      /// </summary>
      public Rectangle BoundingRectangle
      {
         get
         {
            int left = (int)Math.Round(Position.X - sprite.Origin.X) + localBounds.X;
            int top = (int)Math.Round(Position.Y - sprite.Origin.Y) + localBounds.Y;

            return new Rectangle(left, top, localBounds.Width, localBounds.Height);
         }
      }

      /// <summary>
      /// Constructors a new player.
      /// </summary>
      public Player(Level level, Vector2 position)
      {
         this.level = level;
         Reset(position);
      }

      /// <summary>
      /// Loads the player sprite sheet and sounds.
      /// </summary>
      public void LoadContent()
      {
         Texture2D a = null, b = null, c = null;
         if (Size == 0)
         {
            a = Level.Content.Load<Texture2D>("Sprites/Mario/SMALLIDLEMARIO");
            b = Level.Content.Load<Texture2D>("Sprites/Mario/SMALLWALKINGMARIO");
            c = Level.Content.Load<Texture2D>("Sprites/Mario/SMALLJUMPINGMARIO");
         }
         else if (Size == 1)
         {
            a = Level.Content.Load<Texture2D>("Sprites/Mario/LARGEIDLEMARIO");
            b = Level.Content.Load<Texture2D>("Sprites/Mario/LARGEWALKINGMARIO");
            c = Level.Content.Load<Texture2D>("Sprites/Mario/LARGEJUMPINGMARIO");
         }
         else if (Size == 2)
         {
            a = Level.Content.Load<Texture2D>("Sprites/Mario/FIREIDLEMARIO");
            b = Level.Content.Load<Texture2D>("Sprites/Mario/FIREWALKINGMARIO");
            c = Level.Content.Load<Texture2D>("Sprites/Mario/FIREJUMPINGMARIO");
         }
         if (a != null && b != null && c != null)
         {
            // Load animated textures.
            idleAnimation = new Animation(a, 0.1f, true, a.Width / Tile.Width);
            runAnimation = new Animation(b, 0.1f, true, b.Width / Tile.Width);
            jumpAnimation = new Animation(c, 0.1f, false, c.Width / Tile.Width);
            //dieAnimation = new Animation(Level.Content.Load<Texture2D>("Sprites/Mario/Die"), 0.1f, false);
         }
         else
            throw new NullReferenceException("Failed to load player animations.");

         // Calculate bounds within texture size.            
         int width = (int)(idleAnimation.FrameWidth * .85);
         int left = (idleAnimation.FrameWidth - width) / 2;
         int height = (int)(idleAnimation.FrameHeight * .75f);
         int top = idleAnimation.FrameHeight - height;
         localBounds = new Rectangle(left, top, width, height);

         // Load sounds.            
         //killedSound = Level.Content.Load<SoundEffect>("Sounds/PlayerKilled");
         //jumpSound = Level.Content.Load<SoundEffect>("Sounds/PlayerJump");
      }

      /// <summary>
      /// Resets the player to life.
      /// </summary>
      /// <param name="position">The position to come to life at.</param>
      public void Reset(Vector2 position)
      {
         LoadContent();
         Position = position;
         Velocity = Vector2.Zero;
         isAlive = true;
         isRunning = false;
         sprite.PlayAnimation(idleAnimation);
         curKeyState = prevKeyState = Keyboard.GetState();
      }

      /// <summary>
      /// Handles input, performs physics, and animates the player sprite.
      /// </summary>
      public void Update(GameTime gameTime)
      {
         GetInput();
         ApplyPhysics(gameTime);

         UpdateFireballs(gameTime);

         if (IsAlive && IsOnGround)
         {
            if (Math.Abs(Velocity.X) > 0)
               sprite.PlayAnimation(runAnimation);
            else
               sprite.PlayAnimation(idleAnimation);
         }

         if (Invinsible)
         {
            starTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (starTimer > STAR_TIMER)
            {
               Invinsible = false;
               starTimer = 0;
               LoadContent();
            }
         }

         if (WasHit)
         {
            hitTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (hitTimer > HIT_TIMER)
            {
               WasHit = false;
               hitTimer = 0;
               LoadContent();
            }
         }

         // Clear input.
         movement = 0.0f;
         isJumping = false;
         bounceJump = false;
         noteBounce = false;
         //isRunning = false;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="gameTime"></param>
      public void UpdateFireballs(GameTime gameTime)
      {
         for (int i = 0; i < fireballs.Count; ++i)
         {
            fireballs[i].Update(gameTime);
            if (fireballs[i].Done)
               fireballs.RemoveAt(i--);
         }
      }

      /// <summary>
      /// Gets player horizontal movement and jump commands from input.
      /// </summary>
      private void GetInput()
      {
         // Get input state.
         prevKeyState = curKeyState;
         curKeyState = Keyboard.GetState();

         // Ignore small movements to prevent running in place.
         if (Math.Abs(movement) < 0.5f)
            movement = 0.0f;

         // If any digital horizontal movement input is found, override the analog movement.
         if (curKeyState.IsKeyDown(Keys.Left))
            movement = -1.0f;
         else if (curKeyState.IsKeyDown(Keys.Right))
            movement = 1.0f;

         // Check if the player wants to jump.
         isJumping = curKeyState.IsKeyDown(Keys.LeftAlt);
         // See if the player wants to run
         isRunning = curKeyState.IsKeyDown(Keys.LeftControl);
         Console.WriteLine("Running: " + isRunning);
         // Check if player wants to shoot a fireball
         if(curKeyState.IsKeyDown(Keys.Space) && !prevKeyState.IsKeyDown(Keys.Space) && Size == 2 && fireballs.Count < 2)
            fireballs.Add(new Projectile(this));
      }

      /// <summary>
      /// Updates the player's velocity and position based on input, gravity, etc.
      /// </summary>
      public void ApplyPhysics(GameTime gameTime)
      {
         float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

         Vector2 previousPosition = Position;

         // Base velocity is a combination of horizontal movement control and
         // acceleration downward due to gravity.
         if (isRunning)
            velocity.X += movement * TurboAcceleration * elapsed;
         else
            velocity.X += movement * MoveAcceleration * elapsed;

         velocity.Y = MathHelper.Clamp(velocity.Y + GravityAcceleration * elapsed, -MaxFallSpeed, MaxFallSpeed);

         velocity.Y = DoJump(velocity.Y, gameTime);

         // Apply pseudo-drag horizontally.
         if (IsOnGround)
            velocity.X *= GroundDragFactor;
         else
            velocity.X *= AirDragFactor;

         // Prevent the player from running faster than his top speed.
         velocity.X = MathHelper.Clamp(velocity.X, -MaxMoveSpeed, MaxMoveSpeed);

         // Apply velocity.
         Position += velocity * elapsed;
         Position = new Vector2((float)Math.Round(Position.X), (float)Math.Round(Position.Y));

         // If the player is now colliding with the level, separate them.
         HandleCollisions();

         // If the collision stopped us from moving, reset the velocity to zero.
         if (Position.X == previousPosition.X)
               velocity.X = 0;

         if (Position.Y == previousPosition.Y)
               velocity.Y = 0;
      }

      /// <summary>
      /// Calculates the Y velocity accounting for jumping and
      /// animates accordingly.
      /// </summary>
      /// <remarks>
      /// During the accent of a jump, the Y velocity is completely
      /// overridden by a power curve. During the decent, gravity takes
      /// over. The jump velocity is controlled by the jumpTime field
      /// which measures time into the accent of the current jump.
      /// </remarks>
      /// <param name="velocityY">
      /// The player's current velocity along the Y axis.
      /// </param>
      /// <returns>
      /// A new Y velocity if beginning or continuing a jump.
      /// Otherwise, the existing Y velocity.
      /// </returns>
      private float DoJump(float velocityY, GameTime gameTime)
      {
         // If the player wants to jump
         if (isJumping || bounceJump || noteBounce)
         {
            if (bounceJump || noteBounce)
               isJumping = true;

            if(bounceJump && firstBounce)
            {
               velocityY = -2000.0f;
               jumpTime = 0.0f;
               firstBounce = false;
            }
            
            if(noteBounce && noteBounceTicks > 0)
            {
               velocityY = -3000.0f;
               jumpTime = 0.1f;
               noteBounceTicks--;
            }

            if(noteBounce && firstBounce)
            {
               jumpTime = 0.0f;
               firstBounce = false;
               noteBounceTicks = 15;    //idea doesnt work correctly.
            }

            // Begin or continue a jump
            if ((!wasJumping && IsOnGround) || jumpTime > 0.0f)
            {
               //if (jumpTime == 0.0f)
                  // jumpSound.Play();

               jumpTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
               sprite.PlayAnimation(jumpAnimation);
            }

            // If we are in the ascent of the jump
            if (0.0f < jumpTime && jumpTime <= MaxJumpTime)
            {
               // Fully override the vertical velocity with a power curve that gives players more control over the top of the jump
               velocityY = JumpLaunchVelocity * (1.0f - (float)Math.Pow(jumpTime / MaxJumpTime, JumpControlPower));
            }
            else
            {
               // Reached the apex of the jump
               jumpTime = 0.0f;
            }
         }
         else
         {
            // Continues not jumping or cancels a jump in progress
            jumpTime = 0.0f;
         }
         wasJumping = isJumping;


         return velocityY;
      }

      /// <summary>
      /// Detects and resolves all collisions between the player and his neighboring
      /// tiles. When a collision is detected, the player is pushed away along one
      /// axis to prevent overlapping. There is some special logic for the Y axis to
      /// handle platforms which behave differently depending on direction of movement.
      /// </summary>
      private void HandleCollisions()
      {
         // Get the player's bounding rectangle and find neighboring tiles.
         Rectangle bounds = BoundingRectangle;
         int leftTile = (int)Math.Floor((float)bounds.Left / Tile.Width);
         int rightTile = (int)Math.Ceiling(((float)bounds.Right / Tile.Width)) - 1;
         int topTile = (int)Math.Floor((float)bounds.Top / Tile.Height);
         int bottomTile = (int)Math.Ceiling(((float)bounds.Bottom / Tile.Height)) - 1;

         // Reset flag to search for ground collision.
         isOnGround = false;

         // For each potentially colliding tile,
         for (int y = topTile; y <= bottomTile; ++y)
         {
            for (int x = leftTile; x <= rightTile; ++x)
            {
               // If this tile is collidable,
               TileCollision collision = Level.GetCollision(x, y);
               if (collision != TileCollision.Passable)
               {
                  if (collision == TileCollision.Death)
                  {
                     OnKilled(null);
                     return;
                  }
                  // Determine collision depth (with direction) and magnitude.
                  Rectangle tileBounds = Level.GetBounds(x, y);
                  Vector2 depth = RectangleExtensions.GetIntersectionDepth(bounds, tileBounds);
                  if (depth != Vector2.Zero)
                  {
                     float absDepthX = Math.Abs(depth.X);
                     float absDepthY = Math.Abs(depth.Y);

                     // Resolve the collision along the shallow axis.
                     if (absDepthY < absDepthX || collision == TileCollision.Platform)
                     {
                        // If we crossed the top of a tile, we are on the ground.
                        if (previousBottom <= tileBounds.Top)
                           isOnGround = true;

                        // Ignore platforms, unless we are on the ground.
                        if (collision == TileCollision.Impassable || IsOnGround)
                        {
                           // Resolve the collision along the Y axis.
                           Position = new Vector2(Position.X, Position.Y + depth.Y);
                           jumpTime = 0.0f;
                           // Perform further collisions with the new bounds.
                           bounds = BoundingRectangle;
                        }
                     }
                     else if (collision == TileCollision.Impassable) // Ignore platforms.
                     {
                        // Resolve the collision along the X axis.
                        Position = new Vector2(Position.X + depth.X, Position.Y);

                        // Perform further collisions with the new bounds.
                        bounds = BoundingRectangle;
                     }
                  }
               }
            }
         }

         // Save the new bounds bottom.
         previousBottom = bounds.Bottom;
      }

      /// <summary>
      /// Called when the player has been killed.
      /// </summary>
      /// <param name="killedBy">
      /// The enemy who killed the player. This parameter is null if the player was
      /// not killed by an enemy (fell into a hole).
      /// </param>
      public void OnKilled(Enemy killedBy)
      {
         isAlive = false;
         Size = 0;
         Lives--;

         //sprite.PlayAnimation(dieAnimation);
      }

      /// <summary>
      /// Called when this player reaches the level's exit.
      /// </summary>
      public void OnReachedExit()
      {
         
      }

      /// <summary>
      /// Draws the animated player.
      /// </summary>
      public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
      {
         // Flip the sprite to face the way we are moving.
         if (Velocity.X > 0)
               flip = SpriteEffects.FlipHorizontally;
         else if (Velocity.X < 0)
               flip = SpriteEffects.None;

         // Draw each fireball in the list and remove them when they are off the screen
         for(int i = 0; i < fireballs.Count; ++i)
         {
            Projectile fireball = fireballs[i];
            if(fireball.Position.X >= this.Position.X + spriteBatch.GraphicsDevice.Viewport.Width
               || fireball.Position.X <= this.Position.X - spriteBatch.GraphicsDevice.Viewport.Width)
               fireballs.RemoveAt(i--);
            else
               fireball.Draw(gameTime, spriteBatch);
         }

         // Draw the player
         if (WasHit)
            sprite.Draw(gameTime, spriteBatch, Position, flip, true);
         else
            sprite.Draw(gameTime, spriteBatch, Position, flip, false);
      }
   }
}
