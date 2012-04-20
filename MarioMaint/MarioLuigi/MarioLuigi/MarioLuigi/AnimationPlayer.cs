using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioLuigi
{
   /// <summary>
   /// Controls playback of an Animation.
   /// </summary>
   class AnimationPlayer
   {
      /// <summary>
      /// Gets the animation which is currently playing.
      /// </summary>
      public Animation Animation
      {
         get { return animation; }
      }
      Animation animation;

      /// <summary>
      /// Gets the index of the current frame in the animation.
      /// </summary>
      public int FrameIndex
      {
         get { return frameIndex; }
      }
      int frameIndex;

      /// <summary>
      /// The amount of time in seconds that the current frame has been shown for.
      /// </summary>
      private float time;

      /// <summary>
      /// Gets a texture origin at the bottom center of each frame.
      /// </summary>
      public Vector2 Origin
      {
         get { return new Vector2(Animation.FrameWidth / 2.0f, Animation.FrameHeight); }
      }

      /// <summary>
      /// Begins or continues playback of an animation.
      /// </summary>
      public void PlayAnimation(Animation animation)
      {
         // If this animation is already running, do not restart it.
         if (Animation == animation)
               return;

         // Start the new animation.
         this.animation = animation;
         this.frameIndex = 0;
         this.time = 0f;
      }

      /// <summary>
      /// Advances the time position and draws the current frame of the animation.
      /// </summary>
      public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 position,
         SpriteEffects spriteEffects, bool flashing)
      {
         if (Animation == null)
               throw new NotSupportedException("No animation is currently playing.");

         // Process passing time.
         time += (float)gameTime.ElapsedGameTime.TotalSeconds;
         if (time > Animation.FrameTime)
         {
            //time -= Animation.FrameTime;
            time = 0f;

            // Advance the frame index; looping or clamping as appropriate.
            if (Animation.IsLooping)
               frameIndex = (frameIndex + 1) % Animation.FrameCount;
            else
               frameIndex = Math.Min(frameIndex + 1, Animation.FrameCount - 1);
         }

         // Calculate the source rectangle of the current frame.
         Rectangle source = new Rectangle(FrameIndex * Animation.FrameWidth, 0, Animation.FrameWidth, Animation.FrameHeight);

         // Draw the current frame.
         if (flashing)
            spriteBatch.Draw(Flash(gameTime, Animation.Texture), position, source, Color.White, 0.0f, Origin, 1.0f, spriteEffects, 0.0f);
         else
            spriteBatch.Draw(Animation.Texture, position, source, Color.White, 0.0f, Origin, 1.0f, spriteEffects, 0.0f);
      }


      private Texture2D Flash(GameTime gameTime, Texture2D texture)
      {
         Texture2D flashing = texture;
         Color[] data = new Color[texture.Height * texture.Width];
         flashing.GetData<Color>(data);
         byte alpha;
         for (int i = 0; i < data.Length; ++i)
         {
            Color color = data[i];
            alpha = color.A;
            alpha = (byte)((alpha * gameTime.ElapsedGameTime.TotalMilliseconds) % 255);
            data[i].A = alpha;
         }

         flashing.SetData<Color>(data);

         return flashing;
      }
   }
}
