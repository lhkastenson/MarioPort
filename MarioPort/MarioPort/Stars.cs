using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//using Buffers;
//using VGA256;

namespace MarioPort
{
   public static class Stars
   {

          public const int MaxWorldSize = 1; //temp
          public const int W = 1; //temp
          public const int STAR_SPEED = 10;
          public static float Max = (MaxWorldSize / STAR_SPEED) * W;

          public static ushort[] StarMap = new ushort[320]; //: array [0 .. 319] of Word;
          public static int[] LastX = new int[4]; //: array [0 .. 3] of Integer;
          public static int BlinkCounter; //Integer;
          public static sbyte C1, C2; //Byte;

          public static void ClearStars() //procedure ClearStars;
          {
             //FillChar(StarBackGr*, SizeOf( StarBackGr* ), (char)0);
             //FillChar(LastX, SizeOf( LastX ), (char)0);
          }

          public static void InitStars() //procedure InitStars;
          {
              //var
              //  int i; //: Integer;
              //begin
              //  ClearStars;
              //  RandSeed := 0;
              //  for i := 0 to 319 do
              //    StarMap [i] := ((Random (Options.Horizon) {+ WindowY}) * 320 + i);
              //  if Options.Stars in [1, 2] then
              //    for i := 0 to 319 do
              //      if Random (10) > 2 then StarMap [i] := 0;
              //  case Options.Stars of
              //    1: begin
              //        C1 = (byte)29;
             //         C2 = (byte)31;
              //       end;
              //    2: begin
             //         C1 = (byte)90;
             //         C2 = (byte)92;
              //       end;
              //  end;
              //end;
          }

          public static void ShowStars() //procedure ShowStars;
          {
              //var
              //  X: Integer;
              //  P1,
              //  P2: Pointer;
             //begin
              //  LastX [CurrentPage] := XView;
              //  X = (8 * XView) / STAR_SPEED;
              //  P1 = @StarMap[0];
              //  P2 = @StarBackGr^ [CurrentPage, 0];
              //  BlinkCounter := Random (320);
                //asm
                //      mov     bx, BlinkCounter
                //      push    es
                //      push    ds
                //      lds     si, P1          { StarMap }
                //      les     di, P2          { StarBackGr }
                //      mov     cx, 320
                //      cld

                //@1:   push    cx
                //      lodsw
                //      or      ax, ax
                //      jz      @2

                //      add     ax, X
                //      push    es
                //      push    di
                //      mov     di, ax
                //      mov     ax, 0A000h
                //      mov     es, ax
                //      seges   mov     dl, [di]
                //      or      dl, dl
                //      jz      @3
                //      cmp     dl, 0F0h
                //      jz      @6
                //      cmp     dl, 0A0h
                //      jae     @5
                //@6:   xor     dl, dl
                //      jmp     @3
                //@5:   mov     al, C1
                //      dec     bx
                //      jnz     @4
                //      mov     al, C2
                //@4:   stosb
                //@3:   pop     di
                //      pop     es
                //      mov     al, dl
                //      stosb

                //@2:   pop     cx
                //      dec     cx
                //      jnz     @1

                //      pop     ds
                //      pop     es
              //  end;
              //end;
          }

          public static void HideStars() //procedure HideStars;
          {
              //var
              //  X: Integer;
              //  P1,
              //  P2: Pointer;
              //begin
              //  X := (8 * LastX [CurrentPage]) div STAR_SPEED;
              //  P1 := @StarMap [0];
              //  P2 := @StarBackGr^ [CurrentPage, 0];
              //  asm
              //        push    es
              //        push    ds
              //        lds     si, P1          { StarMap }
              //        les     di, P2          { StarBackGr }
              //        mov     cx, 320
              //        cld

              //  @1:   push    cx
              //        lodsw
              //        or      ax, ax
              //        jz      @2
              //        add     ax, X
              //        mov     bx, ax

              //        seges   mov     al, [di]
              //        inc     di
              //        or      al, al
              //        jz      @2

              //        push    es
              //        push    di
              //        mov     di, bx
              //        mov     bx, 0A000h
              //        mov     es, bx

              //        stosb
              //        pop     di
              //        pop     es

              //  @2:   pop     cx
              //        dec     cx
              //        jnz     @1

              //        pop     ds
              //        pop     es
              //  end;
              //end;
          }
      //end.
   }
}
