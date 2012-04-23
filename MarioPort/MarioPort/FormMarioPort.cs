//---------------------------------------------------------
// Purpose: This file was intended to be a port of the 
//			original VGA file.  There have been additional 
//			methods added to deal with different 
//			functionality.
//
// Author: Joel Fendrick, Tom Schroeder 
//
// Notes:  Anything that is commented out, or does not have
//			a body means that there was reason to believe 
//			this was not needed because of additional 
//			functionality of C#. Empty methods were kept
//			because there may be a function call to it
//			from another class.
//
// Total Translation Complete: ~50%, most of what is 
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
      private Bitmap screenBmp;

      public const int windowHeight = 13 * 14;
      public const int windowWidth = 16 * 20;
      public const int SCREEN_WIDTH = 320;
      public const int SCREEN_HEIGHT = 200;
      public const int virScreenWidth = SCREEN_WIDTH + 2 * 20;
      public const int virScreenHeight = 182;
      public const int bytesPerLine = virScreenWidth / 4;

      const int mapMask = 2;
      const int memoryMode = 4;
      const int vert_retraceMask = 8;
      const int maxScanLine = 9;

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

      Thread thread;

      public FormMarioPort()
      {
         //this.DoubleBuffered = true;

         screenBmp = new Bitmap(SCREEN_WIDTH, SCREEN_HEIGHT);

         SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
         SetStyle(ControlStyles.ResizeRedraw, true);
         SetStyle(ControlStyles.UserPaint, true);
         SetStyle(ControlStyles.AllPaintingInWmPaint, true);

         InitializeComponent();
         formRef = this;
         graphics = CreateGraphics();

         thread = new Thread(new ThreadStart(Mario.main));
         thread.Start();
      }

      //-----------------------------------------------------
	   // Draws an image to the screen at XPos,YPos with size
	   // of width and height.
	   //-----------------------------------------------------
      public void PutImage(int XPos, int YPos, int Width, int Height, Bitmap bitmap)
      {
         if (bitmap == null)
            return;

         Superimpose(bitmap, XPos, YPos);
      }

	  //-----------------------------------------------------
	  // Draws an image to the screen at XPos,YPos with size
	  // of width and height.
	  //-----------------------------------------------------
      public void DrawImage(int XPos, int YPos, int Width, int Height, Bitmap bitmap)
      {
         if (bitmap == null)
            return;

         Superimpose(bitmap, XPos, YPos);
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
          //graphics.DrawImage(var, xPos - xView, yPos, tempRec, GraphicsUnit.Pixel);    //draws portion of image specified from tempRec at xPos, yPos
          Superimpose(var, xPos, yPos);
      }

      //-----------------------------------------------------
	  // Flips given image upsidedown
	  //-----------------------------------------------------
      public void UpSideDown(int xPos, int yPos, int width, int height, Bitmap var)
      {
          if (var != null)
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
	   //  Not Implemented due to asembly.
	   //-----------------------------------------------------
      public ushort PushBackGr(int x, int y, int w, int h)
      {
         Stack<ushort> StackPointer = new Stack<ushort>();
         if (!((y + h >= 0) && (y < 200)))
            return 0;

         StackPointer.Push((ushort)page);
         
         // asm
         /*
         PushBackGr := Stack [Page];
         Inc (Stack [Page], W * H + 8);
         */

         return 1;
      }

      //-----------------------------------------------------
	   // Not Implemented due to asembly.
	   //-----------------------------------------------------
      public void PopBackGr(ushort Address)
      {
         int x, y, w, h;

         if (Address == 0)
            return;
         
         // asm 
      }

      //-----------------------------------------------------
	  // Draws a bitmap, var, at x,y
	  //-----------------------------------------------------
      public void DrawBitmap(int x, int y, Bitmap var, byte attr)
      {
         Superimpose(var, x, y);
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

      }

      //-----------------------------------------------------
      // Draw the current scene to the form, then cycle
      // through all game elements to update image
      //-----------------------------------------------------
      public void PaintForm()
      {
         graphics.DrawImage(screenBmp, 0, 0, screenBmp.Width * 2, screenBmp.Height * 2);

         Graphics g = Graphics.FromImage(screenBmp);
         g.Clear(Color.LightBlue);

         Figures.DrawSky (Buffers.XView, 0, Buffers.NH * Buffers.W, Buffers.NV * Buffers.H);

         //BackGr.StartClouds();

         for (int x = Buffers.XView / Buffers.W; x < Buffers.XView / Buffers.W + Buffers.NH; x++)
            for (int y = 0; y < 24; y++)
               Figures.Redraw(x, y);

         BackGr.DrawBackGr(true);
         //ReadColorMap();

         if (Buffers.Options.Stars != 0)
            Stars.ShowStars();

         Enemies.ShowEnemies();

         if (!Play.OnlyDraw)
            Players.DrawPlayer();

         // ShowPage
      }

      //-----------------------------------------------------
      // Superimpose bmp at x, y on screenBmp
      //-----------------------------------------------------
      public void Superimpose(Bitmap bmp, int x, int y)
      {
         Graphics g = Graphics.FromImage(screenBmp);
         g.DrawImage(bmp, x - xView, y, bmp.Width, bmp.Height );
      }
   }
}
