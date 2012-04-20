using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioLuigi
{
   /// <summary>
   /// Controls the collision detection and response behavior of a tile.
   /// </summary>
   enum TileCollision
   {
      /// <summary>
      /// A passable tile is one which does not hinder player motion at all.
      /// </summary>
      Passable = 0,

      /// <summary>
      /// An impassable tile is one which does not allow the player to move through
      /// it at all. It is completely solid.
      /// </summary>
      Impassable = 1,

      /// <summary>
      /// A platform tile is one which behaves like a passable tile except when the
      /// player is above it. A player can jump up through a platform as well as move
      /// past it to the left and right, but can not fall down through the top of it.
      /// </summary>
      Platform = 2,

      /// <summary>
      /// A hidden tile on the screen that will become visible after mario falls through
      /// the top of it.
      /// </summary>
      Hidden = 3,

      /// <summary>
      /// This tile kills the player when touched
      /// </summary>
      Death = 4,
   }

   /// <summary>
   /// Stores the appearance and collision behavior of a tile.
   /// </summary>
   struct Tile
   {
      public Texture2D Texture;
      public TileCollision Collision;
      public SpriteEffects Effects;
      public bool Visible;

      public const int Width = 40;
      public const int Height = 28;

      public static readonly Vector2 Size = new Vector2(Width, Height);

      /// <summary>
      /// Constructs a new tile.
      /// </summary>
      public Tile(Texture2D texture, TileCollision collision, SpriteEffects effects, bool visible)
      {
         Texture = texture;
         Collision = collision;
         Effects = effects;
         Visible = visible;
      }
   }
}
