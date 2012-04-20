using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MarioPort;
//using Dos;

namespace MarioPort
{
   class Keyboard
   {


      public static bool kbEsc = false;
      public static bool kbBS = false;
      public static bool kbTab = false;
      public static bool kbEnter = false;
      public static bool kbSP = false;
      public static bool kbUpArrow = false;
      public static bool kbLeftArrow = false;
      public static bool kbRightArrow = false;
      public static bool kbDownArrow = false;
      public static bool kbAlt = false;
      public static bool kbCtrl = false;
      public static bool kbShiftl = false;
      public static bool kbShiftr = false;

      public static char Key;
      public static byte bKey;

      public const int MaxKeys = 9;
      public const int keyLeft = 1;
      public const int keyRight = 2;
      public const int keyUp = 3;
      public const int keyDown = 4;
      public const int keyAlt = 5;
      public const int keyCtrl = 6;
      public const int keyShiftL = 7;
      public const int keyShiftR = 8;
      public const int keySpace = 9;
      public const int MAX_SEQ_LEN = 100;

      public ushort[] KeySeq = new ushort[MAX_SEQ_LEN - 1];

      public bool Recording = false;
      public bool Playing = false;

      public bool HandlerActive = false; //: Boolean = FALSE;
      public bool KeyHit = false; //: Boolean = FALSE;

      public const bool PlayingMacro = false;

      public static bool kb1 = false;
      public static bool kb2 = false;
      public static bool kb3 = false;
      public static bool kb4 = false;
      public static bool kb5 = false;
      public static bool kb6 = false;
   }
}
