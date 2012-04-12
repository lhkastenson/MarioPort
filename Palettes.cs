using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//using Buffers;
//using VGA256;

namespace MarioPortCode
{
  class Palettes
  {

        //{ Reserved:
        //    0: Nothing
        //    1: Star
        //    2,
        //    3: Grass / palm trees
        //    4: -
        //    5: Red (Mario)
        //    6: Red (Champ)
        //    7,
        //    8,
        //    9,
        //   10,
        //   11: Waterfall
        //   12,
        //   13,
        //   14: Coins
        //   15: White (63, 63, 63)
        //}

        //type
          sbyte[ , ] PalType = new sbyte[ 256 , 3 ]; //Array [0 .. 255, 0 .. 2] of ShortInt;

          const int Steps = 32;
          const int BlinkSpeed = 25;
          const int GrassSpeed = 40;
          const int CoinSpeed = 25;
          const int WaterFallSpeed = 10;

          const int peNoEffect      = 0;
          const int peBlackWhite    = 1;
          const int peEGAMode       = 2;


          bool LockPalette = false; //: Boolean = FALSE;
          bool ModifyPalette = false; //: Boolean = TRUE;
          bool FadingDone = false; //: Boolean = TRUE;
          string Palette; //: PalType;
          string P256; //: ^PalType;
          int BlinkCounter = 0; //: Integer = 0;
          int GrassCounter = 0; //: Integer = 0;
          int CoinCounter = 0; //: Integer = 0;
          int WaterFallCounter = 0; //: Integer = 0;
          int PaletteEffect = peNoEffect; //: Integer = peNoEffect;
          bool FadingUp, FadingDown; //: Boolean;
          byte FadingPos; //: Byte;


        public void ReadPalette (string P /*: PalType*/)
        {
           
        }

        public void NewPalette (string P /*PalType*/)
        {
             
        }

        public void ClearPalette()
        {
           
        }

        public void ChangePalette (sbyte Color, sbyte R, sbyte G, sbyte B )
        {
             
        }

        public void StartFadeUp()
        {
             
        }

        public void StartFadeDown()
        {
             
        }

        public void Fade()
        {
           
        }

        public void FadeUp (sbyte N)
        {
           
        }


        public void InitGrass()
        {
           
        }

        public void CopyPalette (sbyte C1, sbyte C2)
        {
           
        }

        public void BlinkPalette()
        {
           
        }

        public void OutPalette (sbyte Color, sbyte Red, sbyte Green, sbyte Blue)
        {
           
        }

        public void LockPal()
        {
           
        }

        public void UnLockPal()
        {
           
        }

        public void RefreshPalette (string P/*: PalType*/)
        {
            
        }
   }
}
