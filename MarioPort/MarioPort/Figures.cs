
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
      public const int N1 = 4;//3;
      public const int N2 = 14;//13;

      public static Bitmap[,] FigList = new Bitmap[N1, N2];
      public static Bitmap[] Bricks = new Bitmap[4];
      public static byte Sky;



//        private static void ReColor (P1, P2: Pointer; C: Byte);
//        private static void ReColor2 (P1, P2: Pointer; C1, C2: Byte);
//        private static void Replace (P1, P2: Pointer; N1, N2: Byte);
//        private static void Mirror (P1, P2: Pointer);
//        private static void Rotate (P1, P2: Pointer);
//        private static void InitSky (NewSky: Byte);
//        private static void InitPipes (NewColor: Byte);
//        private static void InitWalls (W1, W2, W3: Byte);
//        private static void DrawSky (X, Y, Buffers.W, Buffers.H: Integer);
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
//        for i = 1 to Buffers.H do
//          for j = 1 to Buffers.W do
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
//              mov     cx, Buffers.H
//      1:     push    cx
//              mov     cx, Buffers.W
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
//              mov     cx, Buffers.H
//      1:     push    cx
//              mov     cx, Buffers.W
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
//              mov     cx, Buffers.H
//      1:     push    cx
//              mov     cx, Buffers.W
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

      public static void Mirror(System.Drawing.Bitmap from, ref System.Drawing.Bitmap to)
      {
         from.RotateFlip(RotateFlipType.Rotate180FlipY);
         to = from.Clone() as Bitmap;
      }

      private static void Rotate(Bitmap from, ref Bitmap to)
      {
         from.RotateFlip(RotateFlipType.Rotate180FlipY);
         to = from.Clone() as Bitmap;
      }

      public static void InitSky(byte NewSky)
      {
			Sky = NewSky;
      }

      public static void InitPipes(byte NewColor)
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
      public static void InitWalls(byte W1, byte W2, byte W3)
      {
         InitWall(1, W1);
         InitWall(2, W2);
         InitWall(3, W3);
      }

      public static void InitWall(byte N, byte WallType)
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
               break;
            }
            case 1:
            {
               FigList[N, 1]  = Resources.SAND_000;
               FigList[N, 2]  = Resources.SAND_001;
               FigList[N, 4]  = Resources.SAND_002;
               FigList[N, 5]  = Resources.SAND_003;
               FigList[N, 10] = Resources.SAND_004;
               break;
            }
            case 2:
            {
               int i = Buffers.Options.GroundColor1;
               int j = Buffers.Options.GroundColor2;
               
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
               FigList[N, 10] = Resources.GREEN_004;
               break;
            }
            case 3:
            {
               FigList[N, 1]  = Resources.SAND_000;
               FigList[N, 2]  = Resources.SAND_001;
               FigList[N, 4]  = Resources.SAND_002;
               FigList[N, 5]  = Resources.SAND_003;
               FigList[N, 10] = Resources.SAND_004;
               break;
            }
            case 4:
            {
               FigList[N, 1]  = Resources.GRASS_000;
               FigList[N, 2]  = Resources.GRASS_001;
               FigList[N, 4]  = Resources.GRASS_002;
               FigList[N, 5]  = Resources.GRASS_003;
               FigList[N, 10] = Resources.GRASS_004;
               break;
            }
            case 5:
            {
               FigList[N, 1]  = Resources.DES_000;
               FigList[N, 2]  = Resources.DES_001;
               FigList[N, 4]  = Resources.DES_002;
               FigList[N, 5]  = Resources.DES_003;
               FigList[N, 10] = Resources.DES_004;
               break;
            }
         }

         Mirror (FigList[N,  1], ref FigList[N,  3]);
         Rotate (FigList[N,  4], ref FigList[N,  6]);
         Rotate (FigList[N,  1], ref FigList[N,  9]);
         Rotate (FigList[N,  2], ref FigList[N,  8]);
         Rotate (FigList[N,  3], ref FigList[N,  7]);
         Mirror (FigList[N, 10], ref FigList[N, 11]);
         Rotate (FigList[N, 11], ref FigList[N, 12]);
         Mirror (FigList[N, 12], ref FigList[N, 13]);
      }
      
      public static void SetSkyPalette()
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
//            if Buffers.Options.BackGrType = 4 )
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
//            if Buffers.Options.BackGrType = 4 )
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
//            if Buffers.Options.BackGrType = 4 )
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
//        if Buffers.Options.BackGrType = 0 )
//          Fill (X, Y, Buffers.W, Buffers.H, $E0)
//        else
//        case Sky of
//          0, 1, 3, 4:
//            {
//              i = Buffers.Options.Horizon;
//              j = i - Y;
//              if (i < Y) )
//                Fill (X, Y, Buffers.W, Buffers.H, $F0)
//              else
//                if (i > Y + Buffers.H - 1) )
//                  Fill (X, Y, Buffers.W, Buffers.H, $E0)
//                else
//                {
//                  Fill (X, Y, Buffers.W, j, $E0);
//                  Fill (X, i, Buffers.W, Buffers.H - j, $F0);
//                }
//            }
//          2, 5, 9, 10, 11, 12:
//            SmoothFill (X, Y, Buffers.W, Buffers.H);
//          6, 7, 8:
//            case Buffers.Options.BackGrType of
//              4: DrawBricks (X, Y, Buffers.W, Buffers.H);
//              5: LargeBricks (X, Y, Buffers.W, Buffers.H);
//              6: Pillar (X, Y, Buffers.W, Buffers.H);
//              7: Windows (X, Y, Buffers.W, Buffers.H);
//            }
//        }
//      }
      }
      
      public static void Redraw (int X, int Y)
      {
         char Ch;
         Bitmap Fig = null;
         bool L, R, LS, RS;
         int XPos, YPos;
         
         XPos = X * Buffers.W;
         YPos = Y * Buffers.H;
         Ch = (char)Buffers.WorldMap[X, Y];
         
         if ( X >= 0 && Y >= 0 && Y < Buffers.NV )
         {
            if ( Ch != (char)0 )
            {
               if ( Ch == '%' && Buffers.Options.Design == 4 )
                  DrawSky(XPos, YPos, Buffers.W, Buffers.H / 2);
               else
                  DrawSky(XPos, YPos, Buffers.W, Buffers.H);
            }
            if ( Ch == ' ' )
               return;
            if ( Buffers.WorldMap[X, Y] == (char)18 )
            {
               Fig = FigList[1, 5];
               FormMarioPort.formRef.PutImage(XPos, YPos, Buffers.W, Buffers.H, Fig);
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
                     if ( Buffers.WorldMap[X, Y] >= 14 && Buffers.WorldMap[X, Y] <= 26 )
                     {
                        if ( Ch == 1 || Ch == (char)4 || Ch == (char)7 )
                        {
                           Fig = FigList[1, (int)(Buffers.WorldMap[X, Y]) - 13];
                           FormMarioPort.formRef.PutImage(XPos, YPos, Buffers.W, Buffers.H, Fig);
                        }
                     }
                     else
                     {
                        if ( Buffers.WorldMap[X + 1, Y] >= 14 && Buffers.WorldMap[X + 1, Y] <= 26 )
                        {
                           if ( Ch == 3 || Ch == (char)6 || Ch == (char)9 )
                           {
                              Fig = FigList[1, (int)(Buffers.WorldMap[X + 1, Y]) - 13];
                              FormMarioPort.formRef.PutImage(XPos, YPos, Buffers.W, Buffers.H, Fig);
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
                  L = true;
                     Buffers.WorldMap[X, Y] = 'A';
                     R = true;
                     Buffers.WorldMap[X + 1, Y] = 'A';
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
                  if ( Buffers.CanHoldYou( (char)Buffers.WorldMap[X, Y + 1] ) )
                     FormMarioPort.formRef.DrawImage(XPos, YPos, Buffers.W, Buffers.H, Fig);
                  else
                     FormMarioPort.formRef.UpSideDown(XPos, YPos, Buffers.W, Buffers.H, Fig);
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
                  if ( Buffers.WorldMap[X, Y - 1] == 'þ' )
                     Fig = Resources.EXIT_001;
                  else
                     Fig = Resources.EXIT_000;
                  break;
               }
               case '÷':
               {
                  if ( (Buffers.WorldMap[X, Y - 1] == 'ð') && (Buffers.Options.Design == 2) )
                  {
                     Fig = Resources.SMTREE_001;
                     FormMarioPort.formRef.DrawImage(XPos, YPos, Buffers.W, Buffers.H, Fig);
                  }
                  if ( Buffers.WorldMap[X, Y - 1] == 'ö' )
                  {
                     if ( Buffers.Options.Design == 1 )
                     {
                        Fig = Resources.WPALM_000;
                        FormMarioPort.formRef.DrawImage(XPos, YPos, Buffers.W, Buffers.H, Fig);
                     }
                  }
                  if ( (X == 0) || (Buffers.WorldMap[X - 1, Y] == Ch) )
                  {
                     if (Buffers.WorldMap[X + 1, Y] == (byte)Ch)
                        Fig = Resources.GRASS2_000;
                     else
                        Fig = Resources.GRASS3_000;
                  }
                  else
                  {
                     if ( Buffers.WorldMap[X + 1, Y] == Ch )
                        Fig = Resources.GRASS1_000;
                     else
                        Fig = Resources.GRASS3_000;
                  }
                  break;
               }

               case 'ð':
               {
                  switch (Buffers.Options.Design)
                  {
                     case 1:
                     {
                        if ( Buffers.WorldMap[X, Y - 1] != Ch )
                           Fig = Resources.FENCE_001;
                        else
                           Fig = Resources.FENCE_000;
                        break;
                     }
                     case 2:
                     {
                        if ( Buffers.WorldMap[X, Y - 1] != Ch )
                           Fig = Resources.SMTREE_000;
                        else
                           Fig = Resources.SMTREE_001;
                        break;
                     }
                  }
                  break;
               }
               case 'ö':
               {
                  switch (Buffers.Options.Design)
                  {
                     case 1: Fig = Resources.WPALM_000;
                        break;
                  }
                  break;
               }
               case 'ú':
               {
                  switch (Buffers.Options.Design)
                  {
                     case 1:
                     {
                        if ( Buffers.WorldMap[X - 1, Y] == 'ù' )
                        {
                          Fig = Resources.PALM3_000;
                          FormMarioPort.formRef.DrawImage(XPos, YPos, Buffers.W, Buffers.H, Fig);
                        }
                        else
                        {
                           if ( Buffers.WorldMap[X + 1, Y] == 'ù' )
                           {
                              Fig = Resources.PALM1_000;
                              FormMarioPort.formRef.DrawImage(XPos, YPos, Buffers.W, Buffers.H, Fig);
                           }
                        }
                        Fig = Resources.PALM0_000;
                        break;
                     }
                  }
                  break;
               }
               case 'ô': 
               {
                  switch (Buffers.Options.Design)
                  {
                     case 1:
                     {
                        if ( Buffers.WorldMap[X, Y + 1] == 'ö' )
                        {
                           Fig = Resources.WPALM_000;
                           FormMarioPort.formRef.DrawImage(XPos, YPos, Buffers.W, Buffers.H, Fig);
                        }
                        Fig = Resources.PALM1_000;
                        break;
                      }
                  }
                  break;
               }
               case 'ù':
               {
                  switch (Buffers.Options.Design)
                  {
                     case 1:
                        Fig = Resources.PALM2_000;
                        break;
                  }
                  break;
               }
               case 'õ':
               {
                  switch (Buffers.Options.Design)
                  {
                     case 1:
                     {
                        if ( Buffers.WorldMap[X, Y + 1] == 'ö' )
                        {
                           Fig = Resources.WPALM_000;
                           FormMarioPort.formRef.DrawImage(XPos, YPos, Buffers.W, Buffers.H, Fig);
                        }
                        Fig = Resources.PALM3_000;
                        break;
                     }
                  }
                  break;
               }
               case '#':
               {
                  switch (Buffers.Options.Design)
                  {
                     case 1:
                        Fig = Resources.FALL_000;
                        break;
                     case 2:
                     {
                        switch ((char)Buffers.WorldMap[X, Y - 1])
                        {
                           case '#':
                              FormMarioPort.formRef.PutImage(XPos, YPos, Buffers.W, Buffers.H, Resources.TREE_001);
                              break;
                           case '%':
                           {
                               Fig = Resources.TREE_000;
                               FormMarioPort.formRef.PutImage(XPos, YPos, Buffers.W, Buffers.H, Fig);
                               Fig = Resources.TREE_003;
                               break;
                           }
                           default:
                              Fig = Resources.TREE_003;
                              break;
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
                        FormMarioPort.formRef.Fill (XPos, YPos, Buffers.W, Buffers.H, 5);
                        break;
                  }
                  break;
               }
               case '%': 
               {
                  switch (Buffers.Options.Design)
                  {
                     case 1:
                        Fig = Resources.FALL_001;
                        break;
                     case 2: 
                     {
                        switch ((char)Buffers.WorldMap[X, Y - 1])
                        {
                           case '%':
                              FormMarioPort.formRef.PutImage(XPos, YPos, Buffers.W, Buffers.H, Resources.TREE_000);
                              break;
                           case '#':
                           {
                               Fig = Resources.TREE_001;
                               FormMarioPort.formRef.PutImage(XPos, YPos, Buffers.W, Buffers.H, Fig);
                               Fig = Resources.TREE_002;
                               break;
                           }
                           default:
                             Fig = Resources.TREE_002;
                             break;
                        }
                        break;
                     }
                     case 3:
                        Fig = Resources.WINDOW_000;
                        break;
                     case 4:
                        Fig = Resources.LAVA_001;
                        break;
                     case 5:
                     {
                        switch ((X + Buffers.LavaCounter / 8) % 5)
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
                              break;
                        }
                        break;
                     }
                     break;
                  }
                  break;
               }
            }
            
            if ( Fig != null )
               FormMarioPort.formRef.DrawImage(XPos, YPos, Buffers.W, Buffers.H, Fig);
         }
      }
      
      // Impl  } of BuildWall *moved
      private static char AB = ' ';
      private static char CD = ' ';
      private static char EF = ' ';
      private static char LastAB = ' ';
      private static char LastCD = ' ';
      private static char LastEF = ' ';
      public static void BuildWorld()
      {
         int i, j, k, l;
         for( i = 0; i <= Buffers.Options.XSize - 1; i++)
         {
            for( j = 0; j <= Buffers.NV - 1; j++)
            {
               switch((char)Buffers.WorldMap[i, j])
               {
                  case 'ý': 
                  {
                     Buffers.WorldMap[i, j - 5] = '?';
                     Buffers.WorldMap[i, j - 6] = 'á';
                     Buffers.WorldMap[i, j] = ' ';
                     break; 
                  }
                  case 'ü': 
                  {
                     Buffers.WorldMap[i, j + 2] = '*';
                     Buffers.WorldMap[i, j] = ' ';
                     break;
                  }
                  case '­': 
                  {
                     k = j + 1;
                     for( l = j; l >= 0; l++)
                        Buffers.WorldMap[i, l] = Buffers.WorldMap[i, k];
                     break;
                  }
                  case '®': 
                  {
                     Buffers.WorldMap[i, j] = Buffers.WorldMap[i, j - 1];
                     Buffers.WorldMap[i, Buffers.NV] = (char)254;
                     break;
                  }
                  case '¯':
                  {
                     Buffers.WorldMap[i, j] = Buffers.WorldMap[i, j - 1];
                     Buffers.WorldMap[i, Buffers.NV] = (char)255;
                     break;
                  }
               }
            }
         }

         LastAB = ' ';
         LastCD = ' ';
         LastEF = ' ';

         Buffers.Options.BuildWall = (Buffers.Options.WallType1 < 100);

        if (Buffers.Options.BuildWall )
        {
           for (i = 0; i <= Buffers.Options.XSize - 1; i++)
           {
              for (j = 0; j <= Buffers.NV - 1; j++)
                 BuildWall (i, j);

            LastAB = AB;
            LastAB = CD;
            LastAB = EF;
          }
        }
        //else
        //  switch( Buffers.Options.WallType1 )
        //  {
        //    case 100:
        //        {
        //          Recolor (@Brick0000, Bricks[0], GroundColor1);
        //          Recolor (@Brick0001, Bricks[1], GroundColor1);
        //          Recolor (@Brick0002, Bricks[2], GroundColor1);
        //        }
        //    case 101:
        //        {
        //          Recolor (@Brick1000, Bricks[0], GroundColor1);
        //          Recolor (@Brick1001, Bricks[1], GroundColor1);
        //          Recolor (@Brick1002, Bricks[2], GroundColor1);
        //        }
        //    case 102:
        //        {
        //          Recolor (@Brick2000, Bricks[0], GroundColor1);
        //          Recolor (@Brick2001, Bricks[1], GroundColor1);
        //          Recolor (@Brick2002, Bricks[2], GroundColor1);
        //        }

        //    }
        //  }
        //ConvertGrass (@Grass1000, Grass1001, Grass1002);
        //ConvertGrass (@Grass2000, Grass2001, Grass2002);
        //ConvertGrass (@Grass3000, Grass3002, Grass3001);

        //ConvertGrass (@Palm0000, Palm0001, Palm0002);
        //ConvertGrass (@Palm1000, Palm1001, Palm1002);
        //ConvertGrass (@Palm2000, Palm2001, Palm2002);
        //ConvertGrass (@PALM3_000, Palm3001, Palm3002);

        //Recolor (@Block001, Resources.BLOCK_001, Buffers.Options.BrickColor);
        //Recolor (@Wood000, Resources.WOOD_000, Buffers.Options.WoodColor);
        //Recolor (@XBlock000, Resources.XBLOCK_000, Buffers.Options.XBlockColor);
      }

      private static void BuildWall(int X, int Y)
      {
         const char IgnoreAbove = '÷';
         byte A, B, L, R, N;
         char C;
         SortedSet<char> Ch = new SortedSet<char>();
         SortedSet<char> ChLeft = new SortedSet<char>();
         {
         C = (char)Buffers.WorldMap[X, Y];
         switch((char)C)
         {
            case 'A':
            case 'B':
            {
               AB = C;
               Ch.Add(C);
               for(int m = 1; m <= 13; m++)
                  Ch.Add((char)m);
               if (LastAB != C )
               {
                  ChLeft = Ch;
                  ChLeft.Remove((char)3);
                  ChLeft.Remove((char)6);
                  ChLeft.Remove((char)9);
               }
               else
                 ChLeft = Ch;
               N = 0;
               break;
            }
            case 'C':
            case 'D': 
            {
               CD = C;
               Ch.Clear();
               Ch.Add((char)C);
               for(int m = 1; m <= 26; m++)
                  Ch.Add((char)m);
               Ch.Add('A');
               Ch.Add('B');
               Ch.Add(IgnoreAbove);
               ChLeft = Ch;
               N = 13;
               break;
            }
            default:
               return;
          }
          SortedSet<char> temp = new SortedSet<char>();
          temp = Ch;
          temp.Remove(IgnoreAbove);
          A = System.Convert.ToByte(1 - System.Convert.ToByte(((temp.Contains((char)Buffers.WorldMap[X, Y])) || (Y == 0))));
          B = System.Convert.ToByte(2 * System.Convert.ToByte(!(Y == Buffers.NV - 1) ||  Ch.Contains((char)(Buffers.WorldMap[X, Y + 1]))));
          L = System.Convert.ToByte(4 * System.Convert.ToByte(! ((X == 0) || ChLeft.Contains((char)Buffers.WorldMap[X - 1, Y]))));
          R = System.Convert.ToByte(8 * System.Convert.ToByte(!((X == Buffers.Options.XSize - 1) || Ch.Contains((char)Buffers.WorldMap[X + 1, Y]))));
          switch((A + B + L + R))
          {
            case 0: 
            {
               if ((X > 0) && (Y > 0) )
                  if( ( !(Ch.Contains((char)Buffers.WorldMap[X - 1, Y - 1]))) )
                    { Buffers.WorldMap[X, Y] = (char)(10 + N); return; }
               if ((X < Buffers.Options.XSize - 1) && (Y > 0) )
                  if(  !(Ch.Contains((char)Buffers.WorldMap[X + 1, Y - 1])) )
                  { Buffers.WorldMap[X, Y] = (char)(11 + N); return; }
               if ((X > 0) && (Y < Buffers.NV - 1) )
                  if ( !(Ch.Contains((char)Buffers.WorldMap[X - 1, Y + 1])) )
                  { Buffers.WorldMap[X, Y] = (char)(12 + N); return; }
               if ((X < Buffers.Options.XSize - 1) && (Y < Buffers.NV - 1) )
                  if ( !(Ch.Contains((char)Buffers.WorldMap[X + 1, Y + 1])) )
                  { Buffers.WorldMap[X, Y] = (char)(13 + N); return; }
               Buffers.WorldMap[X, Y] = (char)(5 + N);
              break;
            }
            case 1:
            Buffers.WorldMap[X, Y] = (char)(2 + N);
               break;
            case 2:
               Buffers.WorldMap[X, Y] = (char)(8 + N);
               break;
            case 4:
               Buffers.WorldMap[X, Y] = (char)(4 + N);
               break;
            case 8:
               Buffers.WorldMap[X, Y] = (char)(6 + N);
               break;
            case 5:
               Buffers.WorldMap[X, Y] = (char)(1 + N);
               break;
            case 6:
               Buffers.WorldMap[X, Y] = (char)(7 + N);
               break;
            case 9:
               Buffers.WorldMap[X, Y] = (char)(3 + N);
               break;
            case 10:
               Buffers.WorldMap[X, Y] = (char)(9 + N);
               break;
            default:
               Buffers.WorldMap[X, Y] = (char)(5 + N);
               break;
            }
         }
      }
   }
}