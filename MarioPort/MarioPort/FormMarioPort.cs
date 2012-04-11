using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MarioPort
{
   public partial class FormMarioPort : Form
   {
      public static FormMarioPort formRef = null;

      private Graphics graphics;

      public FormMarioPort()
      {
         InitializeComponent();
         formRef = this;
         graphics = CreateGraphics();
      }

      public void PutImage(int XPos, int YPos, int Width, int Height, Bitmap bitmap)
      {
         graphics.DrawImage(bitmap, XPos, YPos, Width, Height);
      }

      public void DrawImage(int XPos, int YPos, int Width, int Height, Bitmap bitmap)
      {
         graphics.DrawImage(bitmap, XPos, YPos, Width, Height);
      }
   }
}
