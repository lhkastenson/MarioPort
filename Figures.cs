/*
 *
 */

namespace MarioPort
{
   class Figures
   {
      public const int N1 = 3;
      public const int N2 = 13;

      public ImageBuffer[,] FigList = new ImageBuffer[N1, N2];
      public ImageBuffer[] Bricks = new ImageBuffer[4];
      public byte Sky;

//        private void ReColor (P1, P2: Pointer; C: Byte);
//        private void ReColor2 (P1, P2: Pointer; C1, C2: Byte);
//        private void Replace (P1, P2: Pointer; N1, N2: Byte);
//        private void Mirror (P1, P2: Pointer);
//        private void Rotate (P1, P2: Pointer);
//        private void InitSky (NewSky: Byte);
//        private void InitPipes (NewColor: Byte);
//        private void InitWalls (W1, W2, W3: Byte);
//        private void DrawSky (X, Y, W, H: Integer);
//        private void SetSkyPalette;
//        private void Redraw (X, Y: Integer);
//        private void BuildWorld;

      //// implementation ////

      // !! Convert is inside of ConvertGrass
      private void ConvertGrass (P0, P1, P2: ImageBufferPtr)
      {
//      var
//        i, j: Integer;
//        C0, C1, C2: Byte;
//          
//        {
//        for i := 1 to H do
//          for j := 1 to W do
//          {
//            C1 := Ord (P1^ [i, j]);
//            C2 := Ord (P2^ [i, j]);
//            Convert;
//            P0^ [i, j] := Chr (C0);
//          }
//      }
      }
      
      private void Convert()
      {
//      {
//        C0 := C1;
//        if C1 = C2 then Exit;
//        if C1 = 2 then
//        {
//          C0 := 153;
//          if C2 = 0 then Exit;
//          C0 := 155;
//        }
//        else
//        if C1 = 3 then
//        {
//          C0 := 154;
//          if C2 = 0 then Exit;
//          C0 := 156;
//        }
//        else  { C1 = 0 }
//          if C2 = 2 then
//            C0 := 157
//          else
//            C0 := 155;
//      }
      }
      
      private void ReColor (P1, P2: Pointer; C: Byte)
      {
//      {
//        asm
//              push    ds
//              push    es
//              lds     si, P1
//              les     di, P2
//              cld
//              mov     cx, H
//      @1:     push    cx
//              mov     cx, W
//      @2:     lodsb
//              cmp     al, $10
//              jbe     @3
//              and     al, 07h
//              add     al, C
//
//      @3:     stosb
//              loop    @2
//              pop     cx
//              loop    @1
//              pop     es
//              pop     ds
//        }
//      }
      }
      
      private void ReColor2 (P1, P2: Pointer; C1, C2: Byte)
      {
//      {
//        asm
//              push    ds
//              push    es
//              lds     si, P1
//              les     di, P2
//              cld
//              mov     cx, H
//      @1:     push    cx
//              mov     cx, W
//      @2:     lodsb
//              cmp     al, $10
//              jbe     @3
//              and     al, 0Fh
//              cmp     al, 8
//              jb      @UseC1
//              and     al, 7
//              add     al, C2
//              jmp     @3
//      @UseC1:
//              add     al, C1
//
//      @3:     stosb
//              loop    @2
//              pop     cx
//              loop    @1
//              pop     es
//              pop     ds
//        }
//      }
      }
      
      private void Replace (P1, P2: Pointer; N1, N2: Byte)
      {
//      {
//        asm
//              push    ds
//              push    es
//              lds     si, P1
//              les     di, P2
//              cld
//              mov     cx, H
//      @1:     push    cx
//              mov     cx, W
//      @2:     lodsb
//              cmp     al, N1
//              jnz     @3
//              mov     al, N2
//      @3:     stosb
//              loop    @2
//              pop     cx
//              loop    @1
//              pop     es
//              pop     ds
//        }
//      }
      }
      
      private void Mirror (P1, P2: Pointer)
      {
//        type
//          PlaneBuffer = array[0..H - 1, 0..W div 4 - 1] of Byte;
//          PlaneBufferArray = array[0..3] of PlaneBuffer;
//          PlaneBufferArrayPtr = ^PlaneBufferArray;
//        var
//          Source, Dest: PlaneBufferArrayPtr;
//        private void Swap (Plane1, Plane2: Byte);
//          var
//            i, j: Byte;
//        {
//          for j := 0 to H - 1 do
//            for i := 0 to W div 4 - 1 do
//            {
//              Dest^[Plane2, j, i] := Source^[Plane1, j, W div 4 - 1 - i];
//              Dest^[Plane1, j, i] := Source^[Plane2, j, W div 4 - 1 - i];
//            }
//        }
//      {
//        Source := P1;
//        Dest := P2;
//        Swap (0, 3);
//        Swap (1, 2);
//      }
      }

      private void Rotate (P1, P2: Pointer)
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
//      @1:   push    cx
//            mov     cx, W
//      @2:   std
//            lodsb
//            cld
//            stosb
//            loop    @2
//            pop     cx
//            loop    @1
//            pop     es
//            pop     ds
//        }
//      }
      }

      private void InitSky (NewSky: Byte)
      {
//      {
//        Sky := NewSky;
//      }
			Sky = NewSky;
      }
      
      private void InitPipes (NewColor: Byte)
      {
//      {
//        ReColor (@Pipe000, @Pipe000, NewColor);
//        ReColor (@Pipe001, @Pipe001, NewColor);
//        ReColor (@Pipe002, @Pipe002, NewColor);
//        ReColor (@Pipe003, @Pipe003, NewColor);
//
//      }
      }
      
      // Calls InitWall ({ @ } of InitWall) *moved
      private void InitWalls (W1, W2, W3: Byte)
      {
//      {  { InitWalls }
//        InitWall (1, W1);
//        InitWall (2, W2);
//        InitWall (3, W3);
//      }
      }

      private void InitWall (N, WallType: Byte)
      {
//      var
//        i, j: Integer;
//      {
//        case WallType of
//          0: {
//               Move (@Green000^, FigList [N,  1], SizeOf (FigList [N,  1]));
//               Move (@Green001^, FigList [N,  2], SizeOf (FigList [N,  2]));
//               Move (@Green002^, FigList [N,  4], SizeOf (FigList [N,  4]));
//               Move (@Green003^, FigList [N,  5], SizeOf (FigList [N,  5]));
//               Move (@Green004^, FigList [N, 10], SizeOf (FigList [N, 10]));
//             }
//          1: {
//               Move (@Sand000^, FigList [N,  1], SizeOf (FigList [N,  1]));
//               Move (@Sand001^, FigList [N,  2], SizeOf (FigList [N,  2]));
//               Move (@Sand002^, FigList [N,  4], SizeOf (FigList [N,  4]));
//               Move (@Sand003^, FigList [N,  5], SizeOf (FigList [N,  5]));
//               Move (@Sand004^, FigList [N, 10], SizeOf (FigList [N, 10]));
//             }
//          2: {
//               i := Options. GroundColor1;
//               j := Options. GroundColor2;
//               Recolor2 (@Green000, @FigList [N,  1], i, j);
//               Recolor2 (@Green001, @FigList [N,  2], i, j);
//               Recolor2 (@Green002, @FigList [N,  4], i, j);
//               Recolor2 (@Green003, @FigList [N,  5], i, j);
//               Recolor2 (@Green004, @FigList [N, 10], i, j);
//             }
//          3: {
//               Move (@Brown000^, FigList [N,  1], SizeOf (FigList [N,  1]));
//               Move (@Brown001^, FigList [N,  2], SizeOf (FigList [N,  2]));
//               Move (@Brown002^, FigList [N,  4], SizeOf (FigList [N,  4]));
//               Move (@Brown003^, FigList [N,  5], SizeOf (FigList [N,  5]));
//               Move (@Brown004^, FigList [N, 10], SizeOf (FigList [N, 10]));
//             }
//          4: {
//               Move (@Grass000^, FigList [N,  1], SizeOf (FigList [N,  1]));
//               Move (@Grass001^, FigList [N,  2], SizeOf (FigList [N,  2]));
//               Move (@Grass002^, FigList [N,  4], SizeOf (FigList [N,  4]));
//               Move (@Grass003^, FigList [N,  5], SizeOf (FigList [N,  5]));
//               Move (@Grass004^, FigList [N, 10], SizeOf (FigList [N, 10]));
//             }
//          5: {
//               Move (@Des000^, FigList [N,  1], SizeOf (FigList [N,  1]));
//               Move (@Des001^, FigList [N,  2], SizeOf (FigList [N,  2]));
//               Move (@Des002^, FigList [N,  4], SizeOf (FigList [N,  4]));
//               Move (@Des003^, FigList [N,  5], SizeOf (FigList [N,  5]));
//               Move (@Des004^, FigList [N, 10], SizeOf (FigList [N, 10]));
//             }
//
//        }
//
//        Mirror (@FigList [N,  1], @FigList [N,  3]);
//        Rotate (@FigList [N,  4], @FigList [N,  6]);
//        Rotate (@FigList [N,  1], @FigList [N,  9]);
//        Rotate (@FigList [N,  2], @FigList [N,  8]);
//        Rotate (@FigList [N,  3], @FigList [N,  7]);
//        Mirror (@FigList [N, 10], @FigList [N, 11]);
//        Rotate (@FigList [N, 11], @FigList [N, 12]);
//        Mirror (@FigList [N, 12], @FigList [N, 13]);
//
//      }
//

      }
      
      private void SetSkyPalette()
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
//              for i := $E0 to $EF do
//              {
//                j := i - $E0;
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
//              for i := $E0 to $EF do
//              {
//                j := i - $E0;
//                ChangePalette (i, 58 - j div 2, 56 - j, 38 - j);
//              }
//              ChangePalette ($F0, 52, 49, 32);
//            }
//          6: { Brown bricks }
//            if Options.BackGrType = 4 then
//            {
//              for i := $E0 to $EF do
//                ChangePalette (i, 22, 15, 11);
//              ChangePalette ($FD, 22, 15, 11);
//              ChangePalette ($FE, 19, 12,  8);
//              ChangePalette ($FF, 25, 18, 14);
//            }
//            else
//            {
//              for i := $E0 to $FF do
//                ChangePalette (i, 19,  9,  8);
//              ChangePalette ($D1, 19,  9,  8);
//              ChangePalette ($D6, 21, 11, 10);
//              ChangePalette ($D4, 17,  7,  6);
//            }
//          7: { Gray bricks }
//            if Options.BackGrType = 4 then
//            {
//              for i := $E0 to $EF do
//                ChangePalette (i, 18, 18, 22);
//              ChangePalette ($FD, 18, 18, 22);
//              ChangePalette ($FF, 23, 23, 27);
//              ChangePalette ($FE, 13, 13, 17);
//            }
//            else
//            {
//              for i := $E0 to $FF do
//                ChangePalette (i, 15, 15, 18);
//              ChangePalette ($D1, 15, 15, 18);
//              ChangePalette ($D4, 18, 18, 21);
//              ChangePalette ($D6, 12, 12, 15);
//            }
//          8: { Dark brown bricks }
//            if Options.BackGrType = 4 then
//            {
//              for i := $E0 to $EF do
//                ChangePalette (i, 17, 10, 10);
//              ChangePalette ($FD, 17, 10, 10);
//              ChangePalette ($FE, 11,  5,  5);
//              ChangePalette ($FF, 20, 14, 14);
//            }
//            else
//            {
//              for i := $E0 to $FF do
//                ChangePalette (i, 15,  5,  5);
//              ChangePalette ($D1, 15,  5,  5);
//              ChangePalette ($D4, 20, 10, 10);
//              ChangePalette ($D6, 10,  0,  0);
//            }
//          9:
//            {
//              for i := $E0 to $EF do
//              {
//                j := i - $E0;
//                ChangePalette (i, 63 - j div 3, 50 - j, 25 - j);
//              }
//              ChangePalette ($F0, 48, 35, 18);
//            }
//          10:
//            {
//              for i := $E0 to $EF do
//              {
//                j := i - $E0;
//                ChangePalette (i, 27 - j, 43 - j, 63 - j);
//              }
//              ChangePalette ($F0, 58, 58, 63);
//            }
//          11:
//            {
//            {  ChangePalette ($E0, 52, 55, 55); }
//              for i := $E0 to $EF do
//              {
//                j := i - $E0;
//                ChangePalette (i, 60 - j, 63 - j, 63 - j);
//              }
//              ChangePalette ($F0, 42, 48, 45);
//            {  ChangePalette ($FF, 61, 61, 61); }
//            }
//          12:
//            {
//              for i := $E0 to $EF do
//              {
//                j := i - $E0;
//                ChangePalette (i, 55 - j, 63 - j, 63);
//              }
//              ChangePalette ($F0, 30, 50, 58);
//              ChangePalette ($F0, 36, 45, 41);
//            }
//        }
//      }
      }
      
      private void DrawSky (X, Y, W, H: Integer)
      {
//      const
//        Y1 = 0;
//        Y2 = Y1 + 96;
//        YStep = (Y2 - Y1) div 16;  { = 6 }
//      var
//        i, j, k: Integer;
//        Mix: Word;
//      {
//        if Options.BackGrType = 0 then
//          Fill (X, Y, W, H, $E0)
//        else
//        case Sky of
//          0, 1, 3, 4:
//            {
//              i := Options.Horizon;
//              j := i - Y;
//              if (i < Y) then
//                Fill (X, Y, W, H, $F0)
//              else
//                if (i > Y + H - 1) then
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
      
      private void Redraw (X, Y: Integer)
      {
//      var
//        Ch: Char;
//        Fig: Pointer;
//        L, R, LS, RS: Boolean;
//        XPos, YPos: Integer;
//      {
//        XPos := X * W;
//        YPos := Y * H;
//        Ch := WorldMap^ [X, Y];
//        if (X >= 0) and (Y >= 0) and (Y < NV) then
//        {
//          if (not (Ch in [#0])) then
//            if (Ch = '%') and (Options.Design = 4) then
//              DrawSky (XPos, YPos, W, H div 2)
//            else
//              DrawSky (XPos, YPos, W, H);
//          if Ch = ' ' then Exit;
//          if WorldMap^ [X, Y - 1] = #18 then
//          {
//            Fig := @FigList [1, 5];
//            PutImage (XPos, YPos, W, H, Fig^);
//          }
//          Fig := Nil;
//          case Ch of
//
//            #1 .. #26:
//              {
//                if Ch > #13 then
//                  Ch := Chr (Ord (Ch) - 13)
//                else
//                  if WorldMap^ [X - 1, Y] in [#14..#26] then
//                  {
//                    if Ch in [#1, #4, #7] then
//                    {
//                      Fig := @FigList [1, Ord (WorldMap^ [X - 1, Y]) - 13];
//                      PutImage (XPos, YPos, W, H, Fig^);
//                    }
//                  }
//                  else
//                    if WorldMap^ [X + 1, Y] in [#14..#26] then
//                      if Ch in [#3, #6, #9] then
//                      {
//                        Fig := @FigList [1, Ord (WorldMap^ [X + 1, Y]) - 13];
//                        PutImage (XPos, YPos, W, H, Fig^);
//                      }
//
//                Fig := @FigList [1, Ord (Ch)];
//                if not (Ch in [#1, #3, #4, #6, #7, #9]) then
//                {
//                  PutImage (XPos, YPos, W, H, Fig^);
//                  Fig := Nil;
//                }
//              }
//
//            '?': Fig := @Quest000;
//            '@': Fig := @Quest001;
//
//            'A': {
//                   L := WorldMap^ [X - 1, Y] = 'A';
//                   R := WorldMap^ [X + 1, Y] = 'A';
//                   if Odd (X + Y) then
//                   {
//                     RS := True;
//                     LS := False;
//                   }
//                   else
//                   {
//                     LS := True;
//                     RS := False;
//                   }
//                   if (LS and R) then
//                     Fig := @Bricks [1]
//                   else
//                     if (RS and L) then
//                       Fig := @Bricks [2]
//                     else
//                       Fig := @Bricks [0]
//                 }
//
//
//            'I': Fig := @Block000;
//            'J': Fig := @Block001;
//            'K': Fig := @Note000;
//
//            'X': Fig := @XBlock000;
//
//            'W': Fig := @Wood000;
//            '=': {
//                   Fig := @Pin000;
//                   if WorldMap^ [X, Y + 1] in CanHoldYou then
//                     DrawImage (XPos, YPos, W, H, Fig^)
//                   else
//                     UpSideDown (XPos, YPos, W, H, Fig^);
//                   Fig := NIL;
//                 }
//
//            '0': Fig := @Pipe000;
//            '1': Fig := @Pipe001;
//            '2': Fig := @Pipe002;
//            '3': Fig := @Pipe003;
//
//            '*': Fig := @Coin000;
//
//            'þ': if WorldMap^ [X, Y - 1] = 'þ' then
//                   Fig := @Exit001
//                 else
//                   Fig := @Exit000;
//
//            '÷': {
//                   if (WorldMap^ [X, Y - 1] = 'ð') and (Options.Design = 2) then
//                   {
//                     Fig := @SmTree001;
//                     DrawImage (XPos, YPos, W, H, Fig^);
//                   }
//                   if WorldMap^ [X, Y - 1] = 'ö' then
//                     if Options.Design in [1] then
//                     {
//                       Fig := @WPalm000;
//                       DrawImage (XPos, YPos, W, H, Fig^);
//                     }
//                   if (X = 0) or (WorldMap^ [X - 1, Y] = Ch) then
//                   {
//                     if WorldMap^ [X + 1, Y] = Ch then
//                       Fig := @Grass2000
//                     else
//                       Fig := @Grass3000;
//                   }
//                   else
//                     if WorldMap^ [X + 1, Y] = Ch then
//                       Fig := @Grass1000
//                     else
//                       Fig := @Grass3000;
//                 }
//
//            'ð': case Options.Design of
//                   1: if WorldMap^ [X, Y - 1] <> Ch then
//                        Fig := @Fence001
//                      else
//                        Fig := @Fence000;
//                   2: if WorldMap^ [X, Y - 1] <> Ch then
//                        Fig := @SmTree000
//                      else
//                        Fig := @SmTree001;
//                 }
//
//            'ö': case Options.Design of
//                   1: Fig := @WPalm000;
//                 }
//            'ú': case Options.Design of
//                   1: {
//                        if WorldMap^ [X - 1, Y] = 'ù' then
//                        {
//                          Fig := @Palm3000;
//                          DrawImage (XPos, YPos, W, H, Fig^);
//                        }
//                        else
//                        if WorldMap^ [X + 1, Y] = 'ù' then
//                        {
//                          Fig := @Palm1000;
//                          DrawImage (XPos, YPos, W, H, Fig^);
//                        }
//                        Fig := @Palm0000;
//                     }
//                 }
//            'ô': case Options.Design of
//                   1: {
//                        if WorldMap^ [X, Y + 1] = 'ö' then
//                        {
//                          Fig := @WPalm000;
//                          DrawImage (XPos, YPos, W, H, Fig^);
//                        }
//                        Fig := @Palm1000;
//                      }
//                 }
//            'ù': case Options.Design of
//                   1: Fig := @Palm2000;
//                 }
//            'õ': case Options.Design of
//                   1: {
//                        if WorldMap^ [X, Y + 1] = 'ö' then
//                        {
//                          Fig := @WPalm000;
//                          DrawImage (XPos, YPos, W, H, Fig^);
//                        }
//                        Fig := @Palm3000;
//                      }
//                 }
//
//
//            '#': case Options.Design of
//                   1: Fig := @Fall000;
//                   2: case WorldMap^ [X, Y - 1] of
//                        '#': PutImage (XPos, YPos, W, H, @Tree001^);
//                        '%': {
//                               Fig := @Tree000;
//                               PutImage (XPos, YPos, W, H, Fig^);
//                               Fig := @Tree003;
//                             }
//                        else Fig := @Tree003;
//                      }
//                   3: Fig := @Window001;
//                   4: Fig := @Lava000;
//                   5: Fill (XPos, YPos, W, H, 5);
//                 }
//            '%': case Options.Design of
//                   1: Fig := @Fall001;
//                   2: case WorldMap^ [X, Y - 1] of
//                        '%': PutImage (XPos, YPos, W, H, @Tree000^);
//                        '#': {
//                               Fig := @Tree001;
//                               PutImage (XPos, YPos, W, H, Fig^);
//                               Fig := @Tree002;
//                             }
//                        else
//                             Fig := @Tree002;
//                      }
//                   3: Fig := @Window000;
//                   4: Fig := @Lava001;
//                   5: {
//                        case (X + LavaCounter div 8) mod 5 of
//                          0: Fig := @Lava2001;
//                          1: Fig := @Lava2002;
//                          2: Fig := @Lava2003;
//                          3: Fig := @Lava2004;
//                          4: Fig := @Lava2005;
//                        }
//                      }
//                 }
//
//          }
//          if Fig <> Nil then
//            DrawImage (XPos, YPos, W, H, Fig^);
//        }
//      }
      }
      
      // Impl @ } of BuildWall *moved
      private void BuildWorld()
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
//        for i := 0 to Options.XSize - 1 do
//          for j := 0 to NV - 1 do
//            case WorldMap^ [i, j] of
//              'ý': {
//                     WorldMap^ [i, j - 5] := '?';
//                     WorldMap^ [i, j - 6] := 'á';
//                     WorldMap^ [i, j] := ' ';
//                   }
//              'ü': {
//                     WorldMap^ [i, j - 2] := '*';
//                     WorldMap^ [i, j] := ' ';
//                   }
//              '­': {
//                     k := j + 1;
//                     for l := j downto -1 do
//                       WorldMap^ [i, l] := WorldMap^ [i, k];
//                   }
//              '®': {
//                     WorldMap^ [i, j] := WorldMap^ [i, j - 1];
//                     WorldMap^ [i, NV] := #254;
//                   }
//              '¯': {
//                     WorldMap^ [i, j] := WorldMap^ [i, j - 1];
//                     WorldMap^ [i, NV] := #255;
//                   }
//            }
//
//        LastAB := ' ';
//        LastCD := ' ';
//        LastEF := ' ';
//
//        with Options do
//          BuildWall := (WallType1 < 100);
//
//        if Options.BuildWall then
//        {
//          for i := 0 to Options.XSize - 1 do
//          {
//            for j := 0 to NV - 1 do
//              BuildWall (i, j);
//
//            LastAB := AB;
//            LastAB := CD;
//            LastAB := EF;
//          }
//        }
//        else
//          with Options do
//          {
//            case WallType1 of
//              100:
//                {
//                  Recolor (@Brick0000, @Bricks [0], GroundColor1);
//                  Recolor (@Brick0001, @Bricks [1], GroundColor1);
//                  Recolor (@Brick0002, @Bricks [2], GroundColor1);
//                }
//              101:
//                {
//                  Recolor (@Brick1000, @Bricks [0], GroundColor1);
//                  Recolor (@Brick1001, @Bricks [1], GroundColor1);
//                  Recolor (@Brick1002, @Bricks [2], GroundColor1);
//                }
//              102:
//                {
//                  Recolor (@Brick2000, @Bricks [0], GroundColor1);
//                  Recolor (@Brick2001, @Bricks [1], GroundColor1);
//                  Recolor (@Brick2002, @Bricks [2], GroundColor1);
//                }
//
//            }
//          }
//        ConvertGrass (@Grass1000, @Grass1001, @Grass1002);
//        ConvertGrass (@Grass2000, @Grass2001, @Grass2002);
//        ConvertGrass (@Grass3000, @Grass3002, @Grass3001);
//
//        ConvertGrass (@Palm0000, @Palm0001, @Palm0002);
//        ConvertGrass (@Palm1000, @Palm1001, @Palm1002);
//        ConvertGrass (@Palm2000, @Palm2001, @Palm2002);
//        ConvertGrass (@Palm3000, @Palm3001, @Palm3002);
//
//        Recolor (@Block001, @Block001, Options.BrickColor);
//        Recolor (@Wood000, @Wood000, Options.WoodColor);
//        Recolor (@XBlock000, @XBlock000, Options.XBlockColor);
//
//      }
      }
      
      private void BuildWall (X, Y: Integer)
      {
//      const
//        IgnoreAbove = ['÷'];
//      var
//        A, B, L, R: Byte;
//        N: Byte;
//        C: Char;
//        Ch, ChLeft: Set of Char;
//      {
//        C := WorldMap^ [X, Y];
//        case C of
//          'A', 'B': {
//                      AB := C;
//                      Ch := [C] + [#1 .. #13];
//                      if LastAB <> C then
//                        ChLeft := Ch - [#3, #6, #9]
//                      else
//                        ChLeft := Ch;
//                      N := 0;
//                    }
//          'C', 'D': {
//                      CD := C;
//                      Ch := [C] + [#1..#26] + ['A', 'B'] + IgnoreAbove;
//                      ChLeft := Ch;
//                      N := 13;
//                    }
//          else Exit;
//        }
//        A := 1 - Byte ((WorldMap^ [X, Y - 1] in (Ch - IgnoreAbove)) or (Y = 0));
//        B := 2 * Byte (Not ((Y = NV - 1) or (WorldMap^ [X, Y + 1] in Ch)));
//        L := 4 * Byte (Not ((X = 0) or (WorldMap^ [X - 1, Y] in ChLeft)));
//        R := 8 * Byte (Not ((X = Options.XSize - 1) or (WorldMap^ [X + 1, Y] in Ch)));
//        case A + B + L + R of
//          0: {
//               if (X > 0) and (Y > 0) then
//                 if (not (WorldMap^ [X - 1, Y - 1] in Ch)) then
//                   { WorldMap^ [X, Y] := Chr (10 + N); Exit }
//               if (X < Options.XSize - 1) and (Y > 0) then
//                 if not (WorldMap^ [X + 1, Y - 1] in Ch) then
//                   { WorldMap^ [X, Y] := Chr (11 + N); Exit }
//               if (X > 0) and (Y < NV - 1) then
//                 if not (WorldMap^ [X - 1, Y + 1] in Ch) then
//                   { WorldMap^ [X, Y] := Chr (12 + N); Exit }
//               if (X < Options.XSize - 1) and (Y < NV - 1) then
//                 if not (WorldMap^ [X + 1, Y + 1] in Ch) then
//                   { WorldMap^ [X, Y] := Chr (13 + N); Exit }
//               WorldMap^ [X, Y] := Chr (5 + N);
//             }
//          1: WorldMap^ [X, Y] := Chr (2 + N);
//          2: WorldMap^ [X, Y] := Chr (8 + N);
//          4: WorldMap^ [X, Y] := Chr (4 + N);
//          8: WorldMap^ [X, Y] := Chr (6 + N);
//
//          5: WorldMap^ [X, Y] := Chr (1 + N);
//          6: WorldMap^ [X, Y] := Chr (7 + N);
//          9: WorldMap^ [X, Y] := Chr (3 + N);
//         10: WorldMap^ [X, Y] := Chr (9 + N);
//
//          else WorldMap^ [X, Y] := Chr (5 + N);
//        }
//
//      }
//

      }

   }
}