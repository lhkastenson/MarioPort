using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace MarioLuigi
{
   class ParallaxBackground
   {
      /// <summary>
      /// The background textures
      /// </summary>
      public Texture2D[] Textures
      { 
         get;
         private set;
      }

      /// <summary>
      /// The speed at which the background moves
      /// </summary>
      public float ScrollRate 
      { 
         get;
         private set;
      }

      /// <summary>
      /// Constructs a new background
      /// </summary>
      /// <param name="content"></param>
      /// <param name="basePath"></param>
      /// <param name="scrollRate"></param>
      public ParallaxBackground(ContentManager content, string basePath, float scrollRate)
      {
         // Assumes each layer only has 3 segments.
         Textures = new Texture2D[1];
         Textures[0] = content.Load<Texture2D>(basePath);
         ScrollRate = scrollRate;
      }

      /// <summary>
      /// Draws the background at the given viewport
      /// </summary>
      /// <param name="spriteBatch"></param>
      /// <param name="cameraPosition"></param>
      public void Draw(SpriteBatch spriteBatch, float cameraPosition)
      {
         // Assume each segment is the same width.
         int segmentWidth = Textures[0].Width;

         // Calculate which segments to draw and how much to offset them.
         float x = cameraPosition * ScrollRate;
         int leftSegment = (int)Math.Floor(x / segmentWidth);
         int rightSegment = leftSegment + 1;
         x = (x / segmentWidth - leftSegment) * -segmentWidth;

         spriteBatch.Draw(Textures[leftSegment % Textures.Length], new Vector2(x, 0.0f), Color.White);
         spriteBatch.Draw(Textures[rightSegment % Textures.Length], new Vector2(x + segmentWidth, 0.0f), Color.White);
      }
   }
}
