using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace MarioPort
{
   class TXT //TXT
   {
      const int normal = 0;
      const int bold = 1;
      const int shadow = 2;
      bool bShadow = false;
      bool bBold = false;

      byte Base;

      public void SetFont(int i, ushort style)
      {
         if (i == 0)
         {
            Base = 0;
         }
         else if (i == 1)
         {
            Base = 32;
         }
         //bBold := Style or Bold = Style;
         //bShadow := Style or Shadow = Style;
      }
      // according to how this is called in WriteText it should return bitmap 
      // of the char ar the ascii value i.
      public void Letter(int i)
      {
         //ushort w1, w2;

         //i -= Base;
         ///*
         //asm
         //    push    ds
         //    lds     si, P
         //    mov     ax, i
         //    shl     ax, 1
         //    add     si, ax
         //    lodsw
         //    mov     W1, ds
         //    mov     W2, ax
         //    pop     ds
         //end;
         //Letter := Ptr (W1, W2);*/
         //return UIntPtr(w1, w2);
      }

      public int TextWidth(string s)
      {
         //Pointer p;
         int width = 0;
         byte i;
         ushort a;
         //P := Font;
         byte tempBold = 0;
         byte tempShadow = 0;

         for (i = 1; i <= s.Length; i++)
         {
            a = (ushort)(s[i] << 1);
            a = (ushort)(a - 2 * Base);
            /*asm
                push    ds
                lds     si, P
                mov     ax, A
                add     si, ax
                cld
                lodsw
                mov     si, ax
                lodsb
                mov     ah, 0
                add     Width, ax
                pop     ds
              end;*/
         }
         if (bBold)
            tempBold = 1;
         if (bShadow)
            tempShadow = 1;

         width = width + tempBold + tempShadow;
         return width;
      }

      public void WriteText(int x, int y, string s, byte attr)
      {
         //byte i = 1;
         //char c;

         //while (i <= s.Length)
         //{
         //    c = s[i];
         //    if (bShadow)
         //        FormMarioPort.DrawBitmap(x + 1, y + 1, ,16);
         //    if (bBold)
         //    {
         //        if (bShadow)
         //            FormMarioPort.DrawBitmap(x, y + 1, Letter(Ord(c))^, 16);
         //        FormMarioPort.DrawBitmap(x - 1, y, Letter(Ord(c))^, attr);
         //    }
         //    FormMarioPort.DrawBitmap(x, y, Letter((int)c), attr);
         //    x += TextWidth(c + "");
         //    i++;
         //}
      }

      public int CenterX(string s)
      {
          return FormMarioPort.xView + (FormMarioPort.screenWidth - TextWidth(s)) >> 1;
      }

      public void CenterText(int y, string s, byte attr)
      {
         WriteText(CenterX(s), y, s, attr);
      }

      // creates a font type
      public void SwissFont()
      {

      }

      public void Font8x8()
      {

      }
   }
}

