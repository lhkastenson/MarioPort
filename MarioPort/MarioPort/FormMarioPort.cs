using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace MarioPort
{
   public partial class FormMarioPort : Form
   {
      public static FormMarioPort formRef = null;

      private Graphics graphics;

      //const VGA_SEGMENT = $A000;  do not think we will need this
      public const int windowHeight = 13 * 14;
      public const int windowWidth = 16 * 20;
      public const int SCREEN_WIDTH = 320;
      public const int SCREEN_HEIGHT = 200;
      public const int virScreenWidth = SCREEN_WIDTH + 2 * 20;
      public const int virScreenHeight = 182;
      public const int bytesPerLine = virScreenWidth / 4;

      /*MISC_OUTPUT         = $03C2;
      SC_INDEX            = $03C4;
      GC_INDEX            = $03CE;
      CRTC_INDEX          = $03D4;
      VERT_RESCAN         = $03DA;*/

      const int mapMask = 2;
      const int memoryMode = 4;
      const int vert_retraceMask = 8;
      const int maxScanLine = 9;

      byte START_ADDRESS_HIGH = 11;
      byte START_ADDRESS_LOW = 12;
      byte UNDERLINE = 20;
      byte MODE_CONTROL = 23;

      const int readMap = 4;
      public int graphicsMode = 5;
      const int misc = 6;

      const int maxScreens = 24;
      public const int MAX_PAGE = 1;
      const int pageSize = (virScreenHeight + maxScreens) * bytesPerLine;

      byte page0 = 0;
      uint page1 = 32768;

      const int yBase = 9;
      public bool InGraphicsMode = false;
      public static int xView = 0;
      public static int yView = 0;
      int page = 0;
      ushort pageOffset = 0;
      int yOffset = 0;
      const int safe = 34 * bytesPerLine;

      /*Stack: array[0..MAX_PAGE] of Word =
      (PAGE_0 + PAGE_SIZE + SAFE,
      PAGE_1 + PAGE_SIZE + SAFE);*/
      //?????????????????????????????????????????
      //Stack<ushort> array = new Stack<ushort>(MAX_PAGE);

      //Var
      byte OldScreenMode;
      //OldExitProc: Pointer;

      Thread thread;

      public FormMarioPort()
      {
         InitializeComponent();
         formRef = this;
         graphics = CreateGraphics();
         //Buffers.data = Buffers.GameData.Create();
         thread = new Thread(new ThreadStart(Mario.main));
         thread.Start();
         //Mario.main(new string[0]);
      }

      public void PutImage(int XPos, int YPos, int Width, int Height, Bitmap bitmap)
      {
         graphics.DrawImage(bitmap, XPos, YPos, Width, Height);
      }

      public void DrawImage(int XPos, int YPos, int Width, int Height, Bitmap bitmap)
      {
         graphics.DrawImage(bitmap, XPos, YPos, Width, Height);
      }

      //{ Be sure to return to textmode if program is halted }
      public void newExitProc()
      {
         /*OldMode;
         ExitProc := OldExitProc;*/
      }

      //{ Set screen width (NewWidth >= 40) }
      public void setWidth(ushort newWidth)
      {
         //Width = newWidth;
      }

      // function DetectVGA: Boolean;
      public bool DetectVGA()
      {
         bool VGADetected = false;

         return true;
      }

      //procedure InitVGA;
      //{ Start graphics mode 320x200 256 colors }
      public void InitializeVGA()
      {

      }

      //procedure OldMode;
      //{ Return to the original screenmode }
      public void OldMode()
      {
         if (InGraphicsMode)
         {
            ClearVGAMem();
            ClearPalette();
            ShowPage();
         }

         SetMode(OldScreenMode);
         InGraphicsMode = false;
         //ExitProc = OldExitProc();
      }

      //function GetMode: Byte;
      // get video mode
      public byte GetMode()
      {
         return (byte)graphicsMode;
      }

      //procedure SetMode (NewMode: Byte);
      // set video mode
      public void SetMode(byte newMode)
      {
          graphicsMode = newMode;
      }

      //procedure ClearVGAMem;
      public void ClearVGAMem()
      {

      }

      //procedure WaitDisplay;
      public void WaitDisplay()
      {

      }

      //procedure WaitRetrace;
      public void WaitRetrace()
      {

      }

      //procedure SetView (X, Y: Integer);
      public void SetView(int x, int y)
      {
         xView = x;
         yView = y;
      }

      //procedure SetViewport (X, Y: Integer; PageNr: Byte);
      //{ Set the offset of video memory }
      public void SetViewport(int X, int Y, byte PageNr)
      {
          
      }

      //procedure SwapPages;
      public void SwapPages()
      {
         if (page == 0)
         {
            page = 1;
            pageOffset = (ushort)(page1 + yOffset * bytesPerLine);
         }
         else if (page == 1)
         {
            page = 0;
            pageOffset = (ushort)(page0 + yOffset * bytesPerLine);
         }
      }

      //procedure ShowPage;
      public void ShowPage()
      {
         SetViewport(xView, yView, (byte)(page));
         SwapPages();
      }

      //procedure Border (Attr: Byte);
      //{ Draw a border around the screen }
      public void Border(byte Attr)
      {
         /*asm
            push    bp
            mov     ax, 1001h
            mov     bh, Attr
            int     10h
            pop     bp
         end;*/
      }

      //procedure SetYStart (NewYStart: Integer);
      public void SetYStart(int yStart)
      {
         /*asm
            mov     dx, CRTC_INDEX
            mov     al, 16h
            mov     ah, Byte Ptr [NewYStart]
            and     ah, 7Fh
            out     dx, ax
         end;*/
      }

      //procedure SetYEnd (NewYEnd: Integer);
      public void SetYEnd(int yEnd)
      {
         /*asm
            mov     dx, CRTC_INDEX
            mov     al, 15h
            mov     ah, Byte Ptr [NewYEnd]
            out     dx, ax
         end;*/
      }

      //procedure SetYOffset (NewYOffset: Integer);
      public void SetYOffset(int newYOffset)
      {
         yOffset = newYOffset;
      }

      //function GetYOffset: Integer;
      public int GetYOffset()
      {
         return yOffset;
      }

      //procedure PutPixel (X, Y: Integer; Attr: Byte);
      //{ Draw a single pixel at (X, Y) with color Attr }
      public void PutPixel(int x, int y, byte attr)
      {
          Color color = Color.FromArgb(attr);
          Pen pen = new Pen(color, 1);
          graphics.DrawLine(pen, x, y, x, y);
      }

      //function GetPixel (X, Y: Integer): Byte;
      //{ Get color of pixel at (X, Y) }
      public byte GetPixel(int x, int y)
      {
         return 1;
      }

      //procedure RecolorImage (XPos, YPos, Width, Height: Integer; var BitMap; Diff: Byte);
      public void RecolorImage(int xPos, int yPos, int width, int height, Bitmap var, byte diff)
      {

      }

      //procedure DrawPart (XPos, YPos, Width, Height, Y1, Y2: Integer; var BitMap);
      public void DrawPart(int xPos, int yPos, int width, int height, int y1, int y2, Bitmap var)
      {
          //graphics.DrawImage(var, xPos, yPos, width, height);
      }

      //procedure UpSideDown (XPos, YPos, Width, Height: Integer; var BitMap);
      //{ Draw an image on the screen up-side-down (NULL-bytes are ignored) }
      public void UpSideDown(int xPos, int yPos, int width, int height, Bitmap var)
      {
          
      }

      //procedure GetImage (XPos, YPos, Width, Height: Integer; var BitMap);
      public void GetImage(int xPos, int yPos, int width, int height, Bitmap var)
      {

      }

      //procedure Fill (X, Y, W, H: Integer; Attr: Integer);
      //{ Fills an area on the screen with Attr }
      public void Fill(int x, int y, int width, int height, int attr)
      {
          Color color = Color.FromArgb(attr);
          SolidBrush br = new SolidBrush(color);
          graphics.FillRectangle(br, x, y, width, height);

      }

      //procedure SetPalette (Color, Red, Green, Blue: Byte);
      public void setPalete(byte color, byte red, byte green, byte blue)
      {
          
      }

      //procedure ReadPalette (var NewPalette);
      //{ Read whole palette }
      public void ReadPalette(/*NewPallete var*/)
      {

      }

      //procedure ClearPalette;
      public void ClearPalette()
      {

      }

      //function CurrentPage: Integer;
      public int CurrentPage()
      {
         return page;
      }

      //function GetPageOffset: Word;
      public ushort GetPageOffset()
      {
         return pageOffset;
      }

      //procedure ResetStack;
      public void ResetStack()
      {
         //Stack[0] = PAGE_0 + PAGE_SIZE + SAFE;
         //Stack[1] = PAGE_1 + PAGE_SIZE + SAFE;
      }

      //function PushBackGr (X, Y, W, H: Integer): Word;
      public ushort PushBackGr(int x, int y, int w, int h)
      {
         Stack<ushort> StackPointer = new Stack<ushort>();
         if (!((y + h >= 0) && (y < 200)))
            return 0;

         StackPointer.Push((ushort)page);
         /*asm
             mov     bx, PageOffset
             mov     di, StackPointer
             push    ds
             push    es

             mov     ax, VGA_SEGMENT
             mov     ds, ax
             mov     es, ax

             cld
             mov     dx, SC_INDEX
             mov     ax, 0100h + MAP_MASK
             out     dx, ax
             mov     ax, X
             mov     [di], ax
             mov     ax, 0200h + MAP_MASK
             out     dx, ax
             mov     ax, Y
             mov     [di], ax
             mov     ax, 0400h + MAP_MASK
             out     dx, ax
             mov     ax, W
             mov     [di], ax
             mov     ax, 0800h + MAP_MASK
             out     dx, ax
             mov     ax, H
             stosw
             mov     al, 'M'
             stosb

             mov     dx, GC_INDEX
             mov     al, GRAPHICS_MODE
             out     dx, al
             inc     dx
             in      al, dx
             push    ax
             mov     al, 41h
             out     dx, al

             mov     dx, SC_INDEX
             mov     ax, 0F00h + MAP_MASK
             out     dx, ax

             mov     ax, READ_MAP
             mov     dx, GC_INDEX
             out     dx, ax

             mov     dx, Y
             mov     ax, BYTES_PER_LINE
             mul     dx
             mov     si, X
             shr     si, 1
             shr     si, 1
             add     si, ax
             add     si, bx

             mov     cx, W
             shr     cx, 1
             shr     cx, 1

             mov     bx, H

       @1:   push    cx
             rep
             movsb                   { copy 4 pixels }
             pop     cx
             add     si, BYTES_PER_LINE
             sub     si, cx
             dec     bx
             jnz     @1

             mov     dx, GC_INDEX
             pop     ax
             mov     ah, al
             mov     al, GRAPHICS_MODE
             out     dx, ax

             pop     es
             pop     ds
         end;
         PushBackGr := Stack [Page];
         Inc (Stack [Page], W * H + 8);
       end;*/
         return 1;
      }

      //procedure PopBackGr (Address: Word);
      public void PopBackGr(ushort Address)
      {
         int x, y, w, h;

         if (Address == 0)
            return;
         /*asm
             mov     bx, PageOffset
             mov     si, Address

             push    ds
             push    es

             mov     ax, VGA_SEGMENT
             mov     ds, ax
             mov     es, ax

             cld
             mov     dx, GC_INDEX
             mov     ax, 0000h + READ_MAP
             out     dx, ax
             mov     ax, [si]
             mov     X, ax
             mov     ax, 0100h + READ_MAP
             out     dx, ax
             mov     ax, [si]
             mov     Y, ax
             mov     ax, 0200h + READ_MAP
             out     dx, ax
             mov     ax, [si]
             mov     W, ax
             mov     ax, 0300h + READ_MAP
             out     dx, ax
             lodsw
             mov     H, ax
             lodsb
             cmp     al, 'M'
             jz      @@1
     {$IFDEF DEBUG}
             int     3
     {$ENDIF}
             jmp     @End
         @@1:
             mov     dx, GC_INDEX
             mov     al, GRAPHICS_MODE
             out     dx, al
             inc     dx
             in      al, dx
             push    ax
             mov     al, 41h
             out     dx, al

             mov     dx, SC_INDEX
             mov     ax, 0F00h + MAP_MASK
             out     dx, ax

             mov     ax, READ_MAP
             mov     dx, GC_INDEX
             out     dx, ax

             mov     dx, Y
             mov     ax, BYTES_PER_LINE
             mul     dx
             mov     di, X
             shr     di, 1
             shr     di, 1
             add     di, ax
             add     di, bx

             mov     cx, W
             shr     cx, 1
             shr     cx, 1

             mov     bx, H

       @1:   push    cx
             rep
             movsb                   { copy 4 pixels }
             pop     cx
             add     di, BYTES_PER_LINE
             sub     di, cx
             dec     bx
             jnz     @1

             mov     dx, GC_INDEX
             pop     ax
             mov     ah, al
             mov     al, GRAPHICS_MODE
             out     dx, ax

       @end: pop     es
             pop     ds
         end;*/
      }

      //procedure DrawBitmap (X, Y: Integer; var BitMap; Attr: Byte);
      //{ Bitmap starts with size W, H (Byte) }
      public void DrawBitmap(int x, int y, Bitmap var, byte attr)
      {
           //graphics.DrawImage(var, x, y);
      }

      private void FormMarioPort_Load(object sender, EventArgs e)
      {

      }

      private void FormMarioPort_FormClosing(object sender, FormClosingEventArgs e)
      {
         thread.Abort();
      }
   }
}
