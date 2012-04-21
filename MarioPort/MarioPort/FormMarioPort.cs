//---------------------------------------------------------
// Purpose: This file was intended to be a port of the 
//			original VGA file.  There have been additional 
//			methods added to deal with different 
//			functionality.
//
// Author: Joel Fendrick, Tom Schroeder 
//
//
// Notes:  Anything that is commented out, or does not have
//			a body means that there was reason to believe 
//			this was not needed because of additional 
//			functionality of c#. Empty methods were kept
//			because there may be a function call to it
//			from another class.
//		Total Translation Complete: ~50%, most of what is 
//							not translated is asembly
//---------------------------------------------------------

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

      public const int YBASE = 9;
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
         //this.DoubleBuffered = true;

         SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
         SetStyle(ControlStyles.ResizeRedraw, true);
         SetStyle(ControlStyles.UserPaint, true);
         SetStyle(ControlStyles.AllPaintingInWmPaint, true);

         InitializeComponent();
         formRef = this;
         graphics = CreateGraphics();
         //Buffers.data = Buffers.GameData.Create();
         thread = new Thread(new ThreadStart(Mario.main));
         thread.Start();
         //Mario.main(new string[0]);
      }

      //-----------------------------------------------------
	  // Draws an image to the screen at XPos,YPos with size
	  // of width and height.
	  //-----------------------------------------------------
      public void PutImage(int XPos, int YPos, int Width, int Height, Bitmap bitmap)
      {
         if (bitmap == null)
            return;
         graphics.DrawImage(bitmap, XPos - xView, YPos, Width, Height);
      }

	  //-----------------------------------------------------
	  // Draws an image to the screen at XPos,YPos with size
	  // of width and height.
	  //-----------------------------------------------------
      public void DrawImage(int XPos, int YPos, int Width, int Height, Bitmap bitmap)
      {
         if (bitmap == null)
            return;

         graphics.DrawImage(bitmap, XPos - xView, YPos, Width, Height);
      }

      //-----------------------------------------------------
	  // Not Implemented
	  //-----------------------------------------------------
      public void newExitProc()
      {
         /*OldMode;
         ExitProc := OldExitProc;*/
      }
	  
	  //-----------------------------------------------------
      // Not Implemented
	  //-----------------------------------------------------
      public void setWidth(ushort newWidth)
      {
	  
      }

      //-----------------------------------------------------
	  // Not Implemented
	  //-----------------------------------------------------
      public bool DetectVGA()
      {
         return true;
      }
	  //-----------------------------------------------------
      // Start graphics mode 320x200 256 colors 
	  //-----------------------------------------------------
      public void InitializeVGA()
      {

      }

	  //-----------------------------------------------------
      // Return to the original screenmode 
	  //-----------------------------------------------------
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


	  //-----------------------------------------------------
	  // Returns the current graphicsMode
	  //-----------------------------------------------------
      public byte GetMode()
      {
         return (byte)graphicsMode;
      }

      //-----------------------------------------------------
	  // Sets the current graphicsMode to newMode
	  //-----------------------------------------------------
      public void SetMode(byte newMode)
      {
          graphicsMode = newMode;
      }

      //-----------------------------------------------------
	  // Not Implemented
	  //-----------------------------------------------------
      public void ClearVGAMem()
      {

      }

      //-----------------------------------------------------
	  // Not Implemented
	  //-----------------------------------------------------
      public void WaitDisplay()
      {

      }

      //-----------------------------------------------------
	  // NotImplemented
	  //-----------------------------------------------------
      public void WaitRetrace()
      {

      }

      //-----------------------------------------------------
	  // Sets the view to x,y coordinates
	  //-----------------------------------------------------
      public void SetView(int x, int y)
      {
         xView = x;
         yView = y;
      }

      //-----------------------------------------------------
      // Not Implemented
	  //-----------------------------------------------------
      public void SetViewport(int X, int Y, byte PageNr)
      {
          
      }

      //-----------------------------------------------------
	  // swaps between page0 and page1
	  //-----------------------------------------------------
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

      //-----------------------------------------------------
	  // shows the page current page
	  //-----------------------------------------------------
      public void ShowPage()
      {
         SetViewport(xView, yView, (byte)(page));
         SwapPages();
      }

      //-----------------------------------------------------
      // Not Implemented
	  //-----------------------------------------------------
      public void Border(byte Attr)
      {

      }

	  //-----------------------------------------------------
      // Not Implemented
	  //-----------------------------------------------------
      public void SetYStart(int yStart)
      {

      }

      //-----------------------------------------------------
      // Not Implemented
	  //-----------------------------------------------------
      public void SetYEnd(int yEnd)
      {

      }

      //-----------------------------------------------------
      // sets the offset of y to newYOffset
	  //-----------------------------------------------------
      public void SetYOffset(int newYOffset)
      {
         yOffset = newYOffset;
      }

      //-----------------------------------------------------
      // returns the offset of y
	  //-----------------------------------------------------
      public int GetYOffset()
      {
         return yOffset;
      }

      //-----------------------------------------------------
      // Draw a single pixel at (X, Y) with color Attr 
	  //-----------------------------------------------------
      public void PutPixel(int x, int y, byte attr)
      {
          // just call bitmap.setPixel() to set a pixel
		  //Color color = Color.FromArgb(attr);
		  //Pen pen = new Pen(color, 1);
		  //graphics.DrawLine(pen, x, y, x, y);
      }

      //-----------------------------------------------------
      // Get color of pixel at (X, Y) 
	  //-----------------------------------------------------
      public byte GetPixel(int x, int y)
      {
         //just call bitmap.getPixel(), this returns a Color.
		 return 1;
      }

      //-----------------------------------------------------
	  // changes the color of the image by diff.
	  //	One line of code commented out causes error.
	  //-----------------------------------------------------
      public void RecolorImage(int xPos, int yPos, int width, int height, Bitmap var, byte diff)
      {
		 System.Drawing.Imaging.ColorPalette tempCP;
         Color [] tempColor;
         tempCP = var.Palette;
         tempColor = tempCP.Entries;

         for (int i = 0; i < tempColor.Length; i++)
         {
            tempColor[i] = Color.FromArgb(tempColor[i].ToArgb() + diff);
         }
         //tempCP = tempColor;               // need to figure this out.
         var.Palette = tempCP;

      }

      //-----------------------------------------------------
	  // draws a the portion of a bitmap between y1 and y2 
	  // to position xPos, yPos
	  //-----------------------------------------------------
      public void DrawPart(int xPos, int yPos, int width, int height, int y1, int y2, Bitmap var)
      {
          Rectangle tempRec = new Rectangle(xPos, y1, width, y2);    		   // makes rectangle the size of the horizontal slice between y1 and y2.
          graphics.DrawImage(var, xPos - xView, yPos, tempRec, GraphicsUnit.Pixel);    //draws portion of image specified from tempRec at xPos, yPos
      }

      //-----------------------------------------------------
	  // Flips given image upsidedown
	  //-----------------------------------------------------
      public void UpSideDown(int xPos, int yPos, int width, int height, Bitmap var)
      {
          var.RotateFlip(RotateFlipType.RotateNoneFlipX);
      }

      //-----------------------------------------------------
	  //
	  //-----------------------------------------------------
      public void GetImage(int xPos, int yPos, int width, int height, Bitmap var)
      {

      }

      //-----------------------------------------------------
	  // Fills an area on the screen with Attr 
	  //-----------------------------------------------------
      public void Fill(int x, int y, int width, int height, int attr)
      {
          Color color = Color.FromArgb(attr);
          SolidBrush br = new SolidBrush(color);
          graphics.FillRectangle(br, x, y, width, height);

      }

      //-----------------------------------------------------
	  // Not Implemented
	  //-----------------------------------------------------
      public void setPalete(byte color, byte red, byte green, byte blue)
      {
          
      }

      //-----------------------------------------------------
      // Not Implemeented
	  //-----------------------------------------------------
      public void ReadPalette(/*NewPallete var*/)
      {

      }

      //-----------------------------------------------------
	  // Not Implemented
	  //-----------------------------------------------------
      public void ClearPalette()
      {

      }

      //-----------------------------------------------------
	  // returns the current page
	  //-----------------------------------------------------
      public int CurrentPage()
      {
         return page;
      }

      //-----------------------------------------------------
	  // returns the offset of the page
	  //-----------------------------------------------------
      public ushort GetPageOffset()
      {
         return pageOffset;
      }

      //-----------------------------------------------------
	  // resets the stack back to original values
	  //-----------------------------------------------------
      public void ResetStack()
      {
         //Stack[0] = PAGE_0 + PAGE_SIZE + SAFE;
         //Stack[1] = PAGE_1 + PAGE_SIZE + SAFE;
      }

      //-----------------------------------------------------
	  // Mostly not Implemented due to mostly asembly.
	  //-----------------------------------------------------
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

      //-----------------------------------------------------
	  // Mostly not Implemented due to mostly asembly.
	  //-----------------------------------------------------
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

      //-----------------------------------------------------
	  // Draws a bitmap, var, at x,y
	  //-----------------------------------------------------
      public void DrawBitmap(int x, int y, Bitmap var, byte attr)
      {
           //graphics.DrawImage(var, x, y);
      }

      private void FormMarioPort_Load(object sender, EventArgs e)
      {
         //System.Threading.Thread thread = new Thread(this.redraw);
         //thread.Start();
      }



      private void FormMarioPort_FormClosing(object sender, FormClosingEventArgs e)
      {
         thread.Abort();
      }

      private void FormMarioPort_KeyDown(object sender, KeyEventArgs e)
      {
         if (e.KeyData == Keys.Up)
            Keyboard.kbUpArrow = true;
         else if (e.KeyData == Keys.Down)
            Keyboard.kbDownArrow = true;
         else if (e.KeyData == Keys.Left)
            Keyboard.kbLeftArrow = true;
         else if (e.KeyData == Keys.Right)
            Keyboard.kbRightArrow = true;
         else if (e.KeyData == Keys.Space)
         {
            Keyboard.kbAlt = true;
            Keyboard.kbSP = true;
         }
         else if (e.KeyData == Keys.Enter)
            Keyboard.kbEnter = true;
         else if (e.KeyData == Keys.Tab)
            Keyboard.kbTab = true;
         else if (e.KeyData == Keys.Back)
            Keyboard.kbBS = true;
         else if (e.KeyData == Keys.Escape)
            Keyboard.kbEsc = true;
         else if (Control.ModifierKeys == Keys.Control)
            Keyboard.kbCtrl = true;
         //else if (Control.ModifierKeys == Keys.Alt)
         //   Keyboard.kbAlt = true;
         else if (Control.ModifierKeys == Keys.Shift)
            Keyboard.kbShiftl = true;
         else if (Control.ModifierKeys == Keys.Shift)
            Keyboard.kbShiftr = true;

         else if (e.KeyData == Keys.D1)
            Keyboard.kb1 = true;
         else if (e.KeyData == Keys.D2)
            Keyboard.kb2 = true;
         else if (e.KeyData == Keys.D3)
            Keyboard.kb3 = true;
         else if (e.KeyData == Keys.D4)
            Keyboard.kb4 = true;
         else if (e.KeyData == Keys.D5)
            Keyboard.kb5 = true;
         else if (e.KeyData == Keys.D6)
            Keyboard.kb6 = true;
      }

      private void FormMarioPort_KeyUp(object sender, KeyEventArgs e)
      {
         if (e.KeyData == Keys.Up)
            Keyboard.kbUpArrow = false;
         else if (e.KeyData == Keys.Down)
            Keyboard.kbDownArrow = false;
         else if (e.KeyData == Keys.Left)
            Keyboard.kbLeftArrow = false;
         else if (e.KeyData == Keys.Right)
            Keyboard.kbRightArrow = false;
         else if (e.KeyData == Keys.Space)
         {
            Keyboard.kbAlt = false;
            Keyboard.kbSP = false;
         }
         else if (e.KeyData == Keys.Enter)
            Keyboard.kbEnter = false;
         else if (e.KeyData == Keys.Tab)
            Keyboard.kbTab = false;
         else if (e.KeyData == Keys.Back)
            Keyboard.kbBS = false;
         else if (e.KeyData == Keys.Escape)
            Keyboard.kbEsc = false;
         else if (Control.ModifierKeys == Keys.Control)
            Keyboard.kbCtrl = false;
         //else if (Control.ModifierKeys == Keys.Alt)
         //   Keyboard.kbAlt = false;
         else if (Control.ModifierKeys == Keys.Shift)
            Keyboard.kbShiftl = false;
         else if (Control.ModifierKeys == Keys.Shift)
            Keyboard.kbShiftr = false;

         //else if (e.KeyData == Keys.D1)
         //   Keyboard.kb1 = false;
         //else if (e.KeyData == Keys.D2)
         //   Keyboard.kb2 = false;
         //else if (e.KeyData == Keys.D3)
         //   Keyboard.kb3 = false;
         //else if (e.KeyData == Keys.D4)
         //   Keyboard.kb4 = false;
         //else if (e.KeyData == Keys.D5)
         //   Keyboard.kb5 = false;
         //else if (e.KeyData == Keys.D6)
         //   Keyboard.kb6 = false;
      }

      private void FormMarioPort_Paint(object sender, PaintEventArgs e)
      {
         //for (int x = Buffers.XView / Buffers.W; x < Buffers.XView / Buffers.W + Buffers.NH; x++)
         //   for (int y = 0; y <= Buffers.NV; y++)
         //      Figures.Redraw(x, y);
         //Enemies.ShowEnemies();
      }
   }
}
