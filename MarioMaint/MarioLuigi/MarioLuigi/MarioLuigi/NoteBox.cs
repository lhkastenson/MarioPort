using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace MarioLuigi
{
   class NoteBox
   {
      private Texture2D texture;
      private Vector2 position;
      private Level level;
      private const int NOTE_BUFFER = 5;

      /// <summary>
      /// Collision bounds of this notebox
      /// </summary>
      public Rectangle BoundingRectangle
      {
         get
         {
            return new Rectangle((int)position.X - (int)Origin.X, 
               (int)position.Y - (int)Origin.Y - NOTE_BUFFER,
               texture.Width, texture.Height);
         }
      }

      /// <summary>
      /// Origin of the note box used for drawing
      /// </summary>
      private Vector2 Origin
      {
         get
         {
            return new Vector2(texture.Width / 2f, texture.Height / 2f);
         }
      }

      /// <summary>
      /// Create a new notebox owned by the level
      /// </summary>
      /// <param name="level"></param>
      /// <param name="position"></param>
      public NoteBox(Level level, Vector2 position) 
      {
         this.level = level;
         this.position = position;
         LoadContent(level.Content);
      }

      /// <summary>
      /// Loads the notebox textures
      /// </summary>
      /// <param name="content"></param>
      public void LoadContent(ContentManager content)
      {
         texture = content.Load<Texture2D>("Tiles/NOTE000");
      }

      /// <summary>
      /// Causes the player to "bounce" when called
      /// </summary>
      /// <param name="collectedBy"></param>
      public void OnCollision(Player collectedBy)
      {
         collectedBy.NoteBounce = true;
         collectedBy.FirstBounce = true;
      }

      /// <summary>
      /// Draws the notebox in world space
      /// </summary>
      /// <param name="gameTime"></param>
      /// <param name="spriteBatch"></param>
      public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
      {
         spriteBatch.Draw(texture, position, null, Color.White, 0.0f, Origin, 1.0f, SpriteEffects.None, 0.0f);
      }
   }
}
