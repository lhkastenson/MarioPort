using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace MarioLuigi
{
   class Flower : Collectable
   {
      private const float WAIT_TIME = 0.85f;
      private float waiting = 0f;

      public Flower(Level level, Vector2 position, int x, int y)
         : base(level, position, x, y) { }

      public override void LoadContent()
      {
         texture = Level.Content.Load<Texture2D>("Sprites/Collectables/FLOWER000");
         isActive = false;
         Velocity = Vector2.Zero;
         base.LoadContent();
      }

      public override void Update(GameTime gameTime)
      {
         float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
         waiting += elapsed;
         if (waiting > WAIT_TIME)
         {
            isActive = true;
            ApplyPhysics(gameTime);
         }
         else
            Position += Vector2.Multiply(upVelocity, elapsed);
         base.Update(gameTime);
      }

      public override void OnCollected(Player collectedBy)
      {
         if (isActive)
         {
            if (collectedBy.Size < Player.MAX_SIZE)
               collectedBy.Size++;
            collectedBy.Score += POWER_VALUE;
            base.OnCollected(collectedBy);
         }
      }

      public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
      {
         spriteBatch.Draw(texture, Position, Color.White);
      }
   }
}
