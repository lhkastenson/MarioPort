using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarioLuigi
{
   static class Options
   {
      /// <summary>
      /// Is the game hud on or off
      /// </summary>
      static public bool Text
      {
         get { return text; }
         set { text = value; }
      }
      static bool text = true;

      /// <summary>
      /// Is the game sound on or off
      /// </summary>
      static public bool Sound
      {
         get { return sound; }
         set { sound = value; }
      }
      static bool sound = true;
      
      /// <summary>
      /// How many lives the character has
      /// </summary>
      static public int Lives
      {
         get { return lives; }
         set { lives = value; }
      }
      static int lives = 3;
      
      /// <summary>
      /// The current score of the player
      /// </summary>
      static public int Score
      {
         get { return score; }
         set { score = value; }
      }
      static int score = 0;

      static public int Size
      {
         get { return size; }
         set { size = value; }
      }
      static int size = 0;

      /// <summary>
      /// How many coinds the player has
      /// </summary>
      static public int Coins
      {
         get { return coins; }
         set { coins = value; }
      }
      static int coins = 0;

      /// <summary>
      /// The level number to be displayed
      /// </summary>
      static public int Level
      {
         get { return level; }
         set { level = value; }
      }
      static int level = 0;
      
      /// <summary>
      /// How many players there are
      /// </summary>
      static public int Players
      {
         get { return Players; }
         set { players = value; }
      }
      static int players = 1;
   }
}
