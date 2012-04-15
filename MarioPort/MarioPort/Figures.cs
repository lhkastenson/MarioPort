
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing;
using MarioPort;
﻿using Resources = MarioPort.Properties.Resources;

/*
 *
 */

namespace MarioPort
{
   public static class Figures
   {
      public const int N1 = 3;
      public const int N2 = 13;

      public static Bitmap[,] FigList = new Bitmap[N1, N2];
      public static Bitmap[] Bricks = new Bitmap[4];
      public static byte Sky;

/*

//        private static void ReColor (P1, P2: Pointer; C: Byte);
//        private static void ReColor2 (P1, P2: Pointer; C1, C2: Byte);
//        private static void Replace (P1, P2: Pointer; N1, N2: Byte);
//        private static void Mirror (P1, P2: Pointer);
//        private static void Rotate (P1, P2: Pointer);
//        private static void InitSky (NewSky: Byte);
//        private static void InitPipes (NewColor: Byte);
//        private static void InitWalls (W1, W2, W3: Byte);
//        private static void DrawSky (X, Y, W, H: Integer);
//        private static void SetSkyPalette;
//        private static void Redraw (X, Y: Integer);
//        private static void BuildWorld;

      //// implementation ////

      // !! Convert is inside of ConvertGrass
      private static void ConvertGrass(ref Bitmap P0, ref Bitmap P1, ref Bitmap P2)
      {
//      var
//        i, j: Integer;
//        C0, C1, C2: Byte;
//          
//        {
//        for i = 1 to H do
//          for j = 1 to W do
//          {
//            C1 = (int)(P1^ [i, j]);
//            C2 = (int)(P2^ [i, j]);
//            Convert;
//            P0^ [i, j] = Chr (C0);
//          }
//      }
      }
      
      private static void Convert()
      {
//      {
//        C0 = C1;
//        if C1 = C2 ) Exit;
//        if C1 = 2 )
//        {
//          C0 = 153;
//          if C2 = 0 ) Exit;
//          C0 = 155;
//        }
//        else
//        if C1 = 3 )
//        {
//          C0 = 154;
//          if C2 = 0 ) Exit;
//          C0 = 156;
//        }
//        else  { C1 = 0 }
//          if C2 = 2 )
//            C0 = 157
//          else
//            C0 = 155;
//      }
      }
      
//      private static void ReColor (P1, P2: Pointer; C: Byte)
//      {
//      {
//        asm
//              push    ds
//              push    es
//              lds     si, P1
//              les     di, P2
//              cld
//              mov     cx, H
//      1:     push    cx
//              mov     cx, W
//      2:     lodsb
//              cmp     al, $10
//              jbe     3
//              &&     al, 07h
//              add     al, C
//
//      3:     stosb
//              loop    2
//              pop     cx
//              loop    1
//              pop     es
//              pop     ds
//        }
//      }
//      }
      
//      private static void ReColor2 (P1, P2: Pointer; C1, C2: Byte)
//      {
//      {
//        asm
//              push    ds
//              push    es
//              lds     si, P1
//              les     di, P2
//              cld
//              mov     cx, H
//      1:     push    cx
//              mov     cx, W
//      2:     lodsb
//              cmp     al, $10
//              jbe     3
//              &&     al, 0Fh
//              cmp     al, 8
//              jb      UseC1
//              &&     al, 7
//              add     al, C2
//              jmp     3
//      UseC1:
//              add     al, C1
//
//      3:     stosb
//              loop    2
//              pop     cx
//              loop    1
//              pop     es
//              pop     ds
//        }
//      }
//      }
      
//      private static void Replace(P1, P2: Pointer; N1, N2: Byte)
//      {
//      {
//        asm
//              push    ds
//              push    es
//              lds     si, P1
//              les     di, P2
//              cld
//              mov     cx, H
//      1:     push    cx
//              mov     cx, W
//      2:     lodsb
//              cmp     al, N1
//              jnz     3
//              mov     al, N2
//      3:     stosb
//              loop    2
//              pop     cx
//              loop    1
//              pop     es
//              pop     ds
//        }
//      }
//      }
      **/
      public static void Mirror(ref System.Drawing.Bitmap from, ref System.Drawing.Bitmap to)
      { /**
//        type
//          PlaneBuffer = array[0..H - 1, 0..W div 4 - 1] of Byte;
//          PlaneBufferArray = array[0..3] of PlaneBuffer;
//          PlaneBufferArrayPtr = ^PlaneBufferArray;
//        var
//          Source, Dest: PlaneBufferArrayPtr;
//        private static void Swap (Plane1, Plane2: Byte);
//          var
//            i, j: Byte;
//        {
//          for j = 0 to H - 1 do
//            for i = 0 to W div 4 - 1 do
//            {
//              Dest^[Plane2, j, i] = Source^[Plane1, j, W div 4 - 1 - i];
//              Dest^[Plane1, j, i] = Source^[Plane2, j, W div 4 - 1 - i];
//            }
//        }
//      {
//        Source = P1;
//        Dest = P2;
//        Swap (0, 3);
//        Swap (1, 2);
//      }
      **/}/**

      private static void Rotate(ref Bitmap from, ref Bitmap to)
      {
//      {
//        asm
//            push    ds
//            push    es
//            lds     si, P1
//            les     di, P2
//            cld
//            add     si, W * H
//            dec     si
//            mov     cx, H
//      1:   push    cx
//            mov     cx, W
//      2:   std
//            lodsb
//            cld
//            stosb
//            loop    2
//            pop     cx
//            loop    1
//            pop     es
//            pop     ds
//        }
//      }
      }

      private static void InitSky(byte NewSky)
      {
//      {
//        Sky = NewSky;
//      }
			Sky = NewSky;
      }
      
      private static void InitPipes(byte NewColor)
      {
//      {
//        ReColor (@Pipe000, Resources.PIPE_000, NewColor);
//        ReColor (@Pipe001, Resources.PIPE_001, NewColor);
//        ReColor (@Pipe002, Resources.PIPE_002, NewColor);
//        ReColor (@Pipe003, Resources.PIPE_003, NewColor);
//
//      }
      }
      
      // Calls InitWall ({  } of InitWall) *moved
      private static void InitWalls(byte W1, byte W2, byte W3)
      {
         InitWall(1, W1);
         InitWall(2, W2);
         InitWall(3, W3);
      }

      private static void InitWall(byte N, byte WallType)
      {
         switch (WallType)
         {
            case 0:
            {
               FigList[N,  1]  = Resources.GREEN_000;
               FigList[N,  2]  = Resources.GREEN_001;
               FigList[N,  4]  = Resources.GREEN_002;
               FigList[N,  5]  = Resources.GREEN_003;
               FigList[N,  10] = Resources.GREEN_004;
            }
            case 1:
            {
               FigList[N, 1]  = Resources.SAND_000;
               FigList[N, 2]  = Resources.SAND_001;
               FigList[N, 4]  = Resources.SAND_002;
               FigList[N, 5]  = Resources.SAND_003;
               FigList[N, 10] = Resources.SAND_004;
            }
            case 2:
            {
               int i = Options.GroundColor1;
               int j = Options.GroundColor2;
               
               // TODO
//               Recolor2 (@Green000, FigList[N,  1], i, j);
//               Recolor2 (@Green001, FigList[N,  2], i, j);
//               Recolor2 (@Green002, FigList[N,  4], i, j);
//               Recolor2 (@Green003, FigList[N,  5], i, j);
//               Recolor2 (@Green004, FigList[N, 10], i, j);
               
               // TEMP SOLUTION
               FigList[N,  1]  = Resources.GREEN_000;
               FigList[N,  2]  = Resources.GREEN_001;
               FigList[N,  4]  = Resources.GREEN_002;
               FigList[N,  5]  = Resources.GREEN_003;
               FigList[N,  10] = Resources.GREEN_004;
            }
            case 3:
            {
               FigList[N, 1]  = Resources.SAND_000;
               FigList[N, 2]  = Resources.SAND_001;
               FigList[N, 4]  = Resources.SAND_002;
               FigList[N, 5]  = Resources.SAND_003;
               FigList[N, 10] = Resources.SAND_004;
            }
            case 4:
            {
               FigList[N, 1]  = Resources.GRASS_000;
               FigList[N, 2]  = Resources.GRASS_001;
               FigList[N, 4]  = Resources.GRASS_002;
               FigList[N, 5]  = Resources.GRASS_003;
               FigList[N, 10] = Resources.GRASS_004;
            }
            case 5:
            {
               FigList[N, 1]  = Resources.DES_000;
               FigList[N, 2]  = Resources.DES_001;
               FigList[N, 4]  = Resources.DES_002;
               FigList[N, 5]  = Resources.DES_003;
               FigList[N, 10] = Resources.DES_004;
            }
         }

         Mirror (ref FigList[N,  1], ref FigList[N,  3]);
         Rotate (ref FigList[N,  4], ref FigList[N,  6]);
         Rotate (ref FigList[N,  1], ref FigList[N,  9]);
         Rotate (ref FigList[N,  2], ref FigList[N,  8]);
         Rotate (ref FigList[N,  3], ref FigList[N,  7]);
         Mirror (ref FigList[N, 10], ref FigList[N, 11]);
         Rotate (ref FigList[N, 11], ref FigList[N, 12]);
         Mirror (ref FigList[N, 12], ref FigList[N, 13]);
      }
      
      private static void SetSkyPalette()
      {
//      var
//        i, j: Integer;
//      {
//        case Sky of
//          0:
//            {
//               ChangePalette ($E0, 35, 45, 63);
//               ChangePalette ($F0, 20, 38, 48);
//               ChangePalette ($FF, 54, 57, 60);
//             }
//          1:
//             {
//               ChangePalette ($E0, 52, 55, 55);
//               ChangePalette ($F0, 42, 48, 45);
//               ChangePalette ($FF, 61, 61, 61);
//             }
//          2:
//            {
//              for i = $E0 to $EF do
//              {
//                j = i - $E0;
//              { ChangePalette (i, 25 - j, 20 - j, 63 - j); }
//                ChangePalette (i, 48 - 2 * j, 58 - j, 58);
//              }
//            { ChangePalette ($F0, 17, 14, 34); }
//              ChangePalette ($F0, 35, 48, 46);
//            }
//          3:
//             {
//               ChangePalette ($E0,  0,  5,  3);
//               ChangePalette ($F0,  8, 12, 10);
//               ChangePalette ($FF,  8, 13, 13);
//             }
//          4:
//             {
//               ChangePalette ($E0, 35, 45, 53);
//             { ChangePalette ($F0, 53, 63, 63); }
//               ChangePalette ($F0, 23, 39, 43);
//               ChangePalette ($FF, 58, 60, 60);
//             }
//          5:
//            {
//              for i = $E0 to $EF do
//              {
//                j = i - $E0;
//                ChangePalette (i, 58 - j div 2, 56 - j, 38 - j);
//              }
//              ChangePalette ($F0, 52, 49, 32);
//            }
//          6: { Brown bricks }
//            if Options.BackGrType = 4 )
//            {
//              for i = $E0 to $EF do
//                ChangePalette (i, 22, 15, 11);
//              ChangePalette ($FD, 22, 15, 11);
//              ChangePalette ($FE, 19, 12,  8);
//              ChangePalette ($FF, 25, 18, 14);
//            }
//            else
//            {
//              for i = $E0 to $FF do
//                ChangePalette (i, 19,  9,  8);
//              ChangePalette ($D1, 19,  9,  8);
//              ChangePalette ($D6, 21, 11, 10);
//              ChangePalette ($D4, 17,  7,  6);
//            }
//          7: { Gray bricks }
//            if Options.BackGrType = 4 )
//            {
//              for i = $E0 to $EF do
//                ChangePalette (i, 18, 18, 22);
//              ChangePalette ($FD, 18, 18, 22);
//              ChangePalette ($FF, 23, 23, 27);
//              ChangePalette ($FE, 13, 13, 17);
//            }
//            else
//            {
//              for i = $E0 to $FF do
//                ChangePalette (i, 15, 15, 18);
//              ChangePalette ($D1, 15, 15, 18);
//              ChangePalette ($D4, 18, 18, 21);
//              ChangePalette ($D6, 12, 12, 15);
//            }
//          8: { Dark brown bricks }
//            if Options.BackGrType = 4 )
//            {
//              for i = $E0 to $EF do
//                ChangePalette (i, 17, 10, 10);
//              ChangePalette ($FD, 17, 10, 10);
//              ChangePalette ($FE, 11,  5,  5);
//              ChangePalette ($FF, 20, 14, 14);
//            }
//            else
//            {
//              for i = $E0 to $FF do
//                ChangePalette (i, 15,  5,  5);
//              ChangePalette ($D1, 15,  5,  5);
//              ChangePalette ($D4, 20, 10, 10);
//              ChangePalette ($D6, 10,  0,  0);
//            }
//          9:
//            {
//              for i = $E0 to $EF do
//              {
//                j = i - $E0;
//                ChangePalette (i, 63 - j div 3, 50 - j, 25 - j);
//              }
//              ChangePalette ($F0, 48, 35, 18);
//            }
//          10:
//            {
//              for i = $E0 to $EF do
//              {
//                j = i - $E0;
//                ChangePalette (i, 27 - j, 43 - j, 63 - j);
//              }
//              ChangePalette ($F0, 58, 58, 63);
//            }
//          11:
//            {
//            {  ChangePalette ($E0, 52, 55, 55); }
//              for i = $E0 to $EF do
//              {
//                j = i - $E0;
//                ChangePalette (i, 60 - j, 63 - j, 63 - j);
//              }
//              ChangePalette ($F0, 42, 48, 45);
//            {  ChangePalette ($FF, 61, 61, 61); }
//            }
//          12:
//            {
//              for i = $E0 to $EF do
//              {
//                j = i - $E0;
//                ChangePalette (i, 55 - j, 63 - j, 63);
//              }
//              ChangePalette ($F0, 30, 50, 58);
//              ChangePalette ($F0, 36, 45, 41);
//            }
//        }
//      }
      }
      
      private static void DrawSky(int X, int Y, int W, int H)
      {
//      const
//        Y1 = 0;
//        Y2 = Y1 + 96;
//        YStep = (Y2 - Y1) div 16;  { = 6 }
//      var
//        i, j, k: Integer;
//        Mix: Word;
//      {
//        if Options.BackGrType = 0 )
//          Fill (X, Y, W, H, $E0)
//        else
//        case Sky of
//          0, 1, 3, 4:
//            {
//              i = Options.Horizon;
//              j = i - Y;
//              if (i < Y) )
//                Fill (X, Y, W, H, $F0)
//              else
//                if (i > Y + H - 1) )
//                  Fill (X, Y, W, H, $E0)
//                else
//                {
//                  Fill (X, Y, W, j, $E0);
//                  Fill (X, i, W, H - j, $F0);
//                }
//            }
//          2, 5, 9, 10, 11, 12:
//            SmoothFill (X, Y, W, H);
//          6, 7, 8:
//            case Options.BackGrType of
//              4: DrawBricks (X, Y, W, H);
//              5: LargeBricks (X, Y, W, H);
//              6: Pillar (X, Y, W, H);
//              7: Windows (X, Y, W, H);
//            }
//        }
//      }
      }
      
      private static void Redraw (int X, int Y)
      {
         char Ch;
         Bitmap Fig;
         bool L, R, LS, RS;
         int XPos, YPos;
         
         XPos = X * W;
         YPos = Y * H;
         Ch = WorldMap[X, Y];
         
         if ( X >= 0 && Y >= 0 && Y < NV )
         {
            if ( Ch != (char)0 )
            {
               if ( Ch == '%' && Options.Design == 4 )
                  DrawSky(XPos, YPos, W, H / 2);
               else
                  DrawSky(XPos, YPos, W, H);
            }
            if ( Ch == ' ' )
               return;
            if ( WorldMap[X, Y - 1] == (char)18 )
            {
               Fig = FigList[1, 5];
               PutImage(XPos, YPos, W, H, Fig);
            }
            
            switch (Ch)
            {
               case (char)1:
               case (char)2:
               case (char)3:
               case (char)4:
               case (char)5:
               case (char)6:
               case (char)7:
               case (char)8:
               case (char)9:
               case (char)10:
               case (char)11:
               case (char)12:
               case (char)13:
               case (char)14:
               case (char)15:
               case (char)16:
               case (char)17:
               case (char)18:
               case (char)19:
               case (char)20:
               case (char)21:
               case (char)22:
               case (char)23:
               case (char)24:
               case (char)25:
               case (char)26:
               {
                  if ( Ch > (char)13 )
                  {
//                     Ch = Chr ((int)(Ch) - 13)
                     Ch -= (char)13;
                  }
                  else
                  {
                     if ( WorldMap[X - 1, Y] >= 14 && WorldMap[X - 1, Y] <= 26 )
                     {
                        if ( Ch == 1 || Ch == (char)4 || Ch == (char)7 )
                        {
                           Fig = FigList[1, (int)(WorldMap[X - 1, Y]) - 13];
                           PutImage(XPos, YPos, W, H, Fig);
                        }
                     }
                     else
                     {
                        if ( WorldMap[X + 1, Y] >= 14 && WorldMap[X + 1, Y] <= 26 )
                        {
                           if ( Ch == 3 || Ch == (char)6 || Ch == (char)9 )
                           {
                              Fig = FigList[1, (int)(WorldMap[X + 1, Y]) - 13];
                              PutImage(XPos, YPos, W, H, Fig);
                           }
                        }
                     }
                  }
                  break;
               }
               case '?':
                  Fig = Resources.QUEST_000;
                  break;
               case '@':
                  Fig = Resources.QUEST_001;
                  break;
               case 'A':
               {
                  L = WorldMap[X - 1, Y] = 'A';
                  R = WorldMap[X + 1, Y] = 'A';
                  if ( (X + Y) % 2 == 1 )
                  {
                     RS = true;
                     LS = false;
                  }
                  else
                  {
                     LS = true;
                     RS = false;
                  }
                  if (LS && R)
                     Fig = Bricks[1];
                  else
                  {
                     if (RS && L)
                        Fig = Bricks[2];
                     else
                        Fig = Bricks[0];
                  }
                  break;
               }


               case 'I': 
                  Fig = Resources.BLOCK_000;
                  break;
               case 'J':
                  Fig = Resources.BLOCK_001;
                  break;
               case 'K':
                  Fig = Resources.NOTE_000;
                  break;

               case 'X':
                  Fig = Resources.XBLOCK_000;
                  break;

               case 'W':
                  Fig = Resources.WOOD_000;
                  break;
               case '=':
               {
                  Fig = Resources.PIN_000;
                  if ( Buffers.CanHoldYou( WorldMap[X, Y + 1] ) )
                     DrawImage(XPos, YPos, W, H, Fig);
                  else
                     UpSideDown(XPos, YPos, W, H, Fig);
//                  Fig = null;
                  break;
               }

               case '0':
                  Fig = Resources.PIPE_000;
                  break;
               case '1':
                  Fig = Resources.PIPE_001;
                  break;
               case '2':
                  Fig = Resources.PIPE_002;
                  break;
               case '3':
                  Fig = Resources.PIPE_003;
                  break;

               case '*':
                  Fig = Resources.COIN_000;
                  break;

               case 'þ':
               {
                  if ( WorldMap[X, Y - 1] == 'þ' )
                     Fig = Resources.EXIT_001;
                  else
                     Fig = Resources.EXIT_000;
                  break;
               }
               case '÷':
               {
                  if ( (WorldMap[X, Y - 1] == 'ð') && (Options.Design == 2) )
                  {
                     Fig = Resources.SMTREE_001;
                     DrawImage(XPos, YPos, W, H, Fig);
                  }
                  if ( WorldMap[X, Y - 1] == 'ö' )
                  {
                     if ( Options.Design == 1 )
                     {
                        Fig = Resources.WPALM_000;
                        DrawImage(XPos, YPos, W, H, Fig);
                     }
                  }
                  if ( (X == 0) || (WorldMap[X - 1, Y] == Ch) )
                  {
                     if (WorldMap[X + 1, Y] = Ch)
                        Fig = Resources.GRASS2_000;
                     else
                        Fig = Resources.GRASS3_000;
                  }
                  else
                  {
                     if ( WorldMap[X + 1, Y] == Ch )
                        Fig = Resources.GRASS1_000;
                     else
                        Fig = Resources.GRASS3_000;
                  }
                  break;
               }

               case 'ð':
               {
                  switch (Options.Design)
                  {
                     case 1:
                     {
                        if ( WorldMap[X, Y - 1] != Ch )
                           Fig = Resources.FENCE_001;
                        else
                           Fig = Resources.FENCE_000;
                        break;
                     }
                     case 2:
                     {
                        if ( WorldMap[X, Y - 1] != Ch )
                           Fig = Resources.SMTREE_000;
                        else
                           Fig = Resources.SMTREE_001;
                     }
                  }
                  break;
               }
               case 'ö':
               {
                  switch (Options.Design)
                  {
                     case 1: Fig = Resources.WPALM_000;
                  }
               }
               case 'ú':
               {
                  switch (Options.Design)
                  {
                     case 1:
                     {
                        if ( WorldMap[X - 1, Y] == 'ù' )
                        {
                          Fig = Resources.PALM3_000;
                          DrawImage(XPos, YPos, W, H, Fig);
                        }
                        else
                        {
                           if ( WorldMap[X + 1, Y] == 'ù' )
                           {
                              Fig = Resources.PALM1_000;
                              DrawImage(XPos, YPos, W, H, Fig);
                           }
                        }
                        Fig = Resources.Palm0_000;
                     }
                  }
                  break;
               }
               case 'ô': 
               {
                  switch (Options.Design)
                  {
                     case 1:
                     {
                        if ( WorldMap[X, Y + 1] == 'ö' )
                        {
                           Fig = Resources.WPALM_000;
                           DrawImage(XPos, YPos, W, H, Fig);
                        }
                        Fig = Resources.PALM1_000;
                      }
                  }
                  break;
               }
               case 'ù':
               {
                  switch (Options.Design)
                  {
                     case 1:
                        Fig = Resources.PALM2_000;
                  }
               }
               case 'õ':
               {
                  switch (Options.Design)
                  {
                     case 1:
                     {
                        if ( WorldMap[X, Y + 1] == 'ö' )
                        {
                           Fig = Resources.WPALM_000;
                           DrawImage(XPos, YPos, W, H, Fig);
                        }
                        Fig = Resources.PALM3_000;
                     }
                  }
                  break;
               }
               case '#':
               {
                  switch (Options.Design)
                  {
                     case 1:
                        Fig = Resources.FALL_000;
                        break;
                     case 2:
                     {
                        switch (WorldMap[X, Y - 1])
                        {
                           case '#':
                              PutImage(XPos, YPos, W, H, Resources.TREE_001);
                              break;
                           case '%':
                           {
                               Fig = Resources.TREE_000;
                               PutImage(XPos, YPos, W, H, Fig);
                               Fig = Resources.TREE_003;
                               break;
                           }
                           default:
                              Fig = Resources.TREE_003;
                        }
                        break;
                     }
                     case 3:
                        Fig = Resources.WINDOW_001;
                        break;
                     case 4:
                        Fig = Resources.LAVA_000;
                        break;
                     case 5:
                        Fill (XPos, YPos, W, H, 5);
                  }
                  break;
               }
               case '%': 
               {
                  switch (Options.Design)
                  {
                     case 1:
                        Fig = Resources.FALL_001;
                        break;
                     case 2: 
                     {
                        switch (WorldMap[X, Y - 1])
                        {
                           case '%':
                              PutImage(XPos, YPos, W, H, Resources.TREE_000);
                              break;
                           case '#':
                           {
                               Fig = Resources.TREE_001;
                               PutImage(XPos, YPos, W, H, Fig);
                               Fig = Resources.TREE_002;
                               break;
                           }
                           default:
                             Fig = Resources.TREE_002;
                        }
                        break;
                     }
                     case 3:
                        Fig = WINDOW_000;
                        break;
                     case 4:
                        Fig = Resources.LAVA_001;
                        break;
                     case 5:
                     {
                        switch ((X + LavaCounter / 8) % 5)
                        {
                           case 0:
                              Fig = Resources.LAVA2_001;
                              break;
                           case 1:
                              Fig = Resources.LAVA2_002;
                              break;
                           case 2:
                              Fig = Resources.LAVA2_003;
                              break;
                           case 3:
                              Fig = Resources.LAVA2_004;
                              break;
                           case 4:
                              Fig = Resources.LAVA2_005;
                        }
                        break;
                     }
                     break;
                  }
                  break;
               }
            }
            
            if ( Fig != null )
               DrawImage(XPos, YPos, W, H, Fig);
         }
      }
      
      // Impl  } of BuildWall *moved
      private static void BuildWorld()
      {
//      var
//        AB,
//        CD,
//        EF,
//        LastAB,
//        LastCD,
//        LastEF: Char;

//      var
//        i, j, k, l: Integer;
//
//      {  { BuildWorld }
//        for i = 0 to Options.XSize - 1 do
//          for j = 0 to NV - 1 do
//            case WorldMap[i, j] of
//              'ý': {
//                     WorldMap[i, j - 5] = '?';
//                     WorldMap[i, j - 6] = 'á';
//                     WorldMap[i, j] = ' ';
//                   }
//              'ü': {
//                     WorldMap[i, j - 2] = '*';
//                     WorldMap[i, j] = ' ';
//                   }
//              '­': {
//                     k = j + 1;
//                     for l = j downto -1 do
//                       WorldMap[i, l] = WorldMap[i, k];
//                   }
//              '®': {
//                     WorldMap[i, j] = WorldMap[i, j - 1];
//                     WorldMap[i, NV] = (char)254;
//                   }
//              '¯': {
//                     WorldMap[i, j] = WorldMap[i, j - 1];
//                     WorldMap[i, NV] = (char)255;
//                   }
//            }
//
//        LastAB = ' ';
//        LastCD = ' ';
//        LastEF = ' ';
//
//        with Options do
//          BuildWall = (WallType1 < 100);
//
//        if Options.BuildWall )
//        {
//          for i = 0 to Options.XSize - 1 do
//          {
//            for j = 0 to NV - 1 do
//              BuildWall (i, j);
//
//            LastAB = AB;
//            LastAB = CD;
//            LastAB = EF;
//          }
//        }
//        else
//          with Options do
//          {
//            case WallType1 of
//              100:
//                {
//                  Recolor (@Brick0000, Bricks[0], GroundColor1);
//                  Recolor (@Brick0001, Bricks[1], GroundColor1);
//                  Recolor (@Brick0002, Bricks[2], GroundColor1);
//                }
//              101:
//                {
//                  Recolor (@Brick1000, Bricks[0], GroundColor1);
//                  Recolor (@Brick1001, Bricks[1], GroundColor1);
//                  Recolor (@Brick1002, Bricks[2], GroundColor1);
//                }
//              102:
//                {
//                  Recolor (@Brick2000, Bricks[0], GroundColor1);
//                  Recolor (@Brick2001, Bricks[1], GroundColor1);
//                  Recolor (@Brick2002, Bricks[2], GroundColor1);
//                }
//
//            }
//          }
//        ConvertGrass (@Grass1000, Grass1001, Grass1002);
//        ConvertGrass (@Grass2000, Grass2001, Grass2002);
//        ConvertGrass (@Grass3000, Grass3002, Grass3001);
//
//        ConvertGrass (@Palm0000, Palm0001, Palm0002);
//        ConvertGrass (@Palm1000, Palm1001, Palm1002);
//        ConvertGrass (@Palm2000, Palm2001, Palm2002);
//        ConvertGrass (@PALM3_000, Palm3001, Palm3002);
//
//        Recolor (@Block001, Resources.BLOCK_001, Options.BrickColor);
//        Recolor (@Wood000, Resources.WOOD_000, Options.WoodColor);
//        Recolor (@XBlock000, Resources.XBLOCK_000, Options.XBlockColor);
//
//      }
      }
      
      private static void BuildWall (int X, int Y)
      {
//      const
//        IgnoreAbove = ['÷'];
//      var
//        A, B, L, R: Byte;
//        N: Byte;
//        C: Char;
//        Ch, ChLeft: Set of Char;
//      {
//        C = WorldMap[X, Y];
//        case C of
//          'A', 'B': {
//                      AB = C;
//                      Ch = [C] + [#1 .. (char)13];
//                      if LastAB != C )
//                        ChLeft = Ch - [#3, (char)6, (char)9]
//                      else
//                        ChLeft = Ch;
//                      N = 0;
//                    }
//          'C', 'D': {
//                      CD = C;
//                      Ch = [C] + [#1..#26] + ['A', 'B'] + IgnoreAbove;
//                      ChLeft = Ch;
//                      N = 13;
//                    }
//          else Exit;
//        }
//        A = 1 - Byte ((WorldMap[X, Y - 1] in (Ch - IgnoreAbove)) || (Y = 0));
//        B = 2 * Byte (Not ((Y = NV - 1) || (WorldMap[X, Y + 1] in Ch)));
//        L = 4 * Byte (Not ((X = 0) || (WorldMap[X - 1, Y] in ChLeft)));
//        R = 8 * Byte (Not ((X = Options.XSize - 1) || (WorldMap[X + 1, Y] in Ch)));
//        case A + B + L + R of
//          0: {
//               if (X > 0) && (Y > 0) )
//                 if ( !(WorldMap[X - 1, Y - 1] in Ch)) )
//                   { WorldMap[X, Y] = Chr (10 + N); Exit }
//               if (X < Options.XSize - 1) && (Y > 0) )
//                 if  !(WorldMap[X + 1, Y - 1] in Ch) )
//                   { WorldMap[X, Y] = Chr (11 + N); Exit }
//               if (X > 0) && (Y < NV - 1) )
//                 if  !(WorldMap[X - 1, Y + 1] in Ch) )
//                   { WorldMap[X, Y] = Chr (12 + N); Exit }
//               if (X < Options.XSize - 1) && (Y < NV - 1) )
//                 if  !(WorldMap[X + 1, Y + 1] in Ch) )
//                   { WorldMap[X, Y] = Chr (13 + N); Exit }
//               WorldMap[X, Y] = Chr (5 + N);
//             }
//          1: WorldMap[X, Y] = Chr (2 + N);
//          2: WorldMap[X, Y] = Chr (8 + N);
//          4: WorldMap[X, Y] = Chr (4 + N);
//          8: WorldMap[X, Y] = Chr (6 + N);
//
//          5: WorldMap[X, Y] = Chr (1 + N);
//          6: WorldMap[X, Y] = Chr (7 + N);
//          9: WorldMap[X, Y] = Chr (3 + N);
//         10: WorldMap[X, Y] = Chr (9 + N);
//
//          else WorldMap[X, Y] = Chr (5 + N);
//        }
//
//      }
//

      }
*/

   }
}