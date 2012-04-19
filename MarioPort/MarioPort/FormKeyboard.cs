using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using KeyboardTest;

namespace KeyboardTest
{
   public partial class Form1 : Form
   {
      public Form1()
      {
         InitializeComponent();
      }

      private void Form1_KeyDown(object sender, KeyEventArgs e)
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
            Keyboard.kbSP = true;
         else if (e.KeyData == Keys.Enter)
            Keyboard.kbEnter = true;
         else if (e.KeyData == Keys.Tab)
            Keyboard.kbTab = true;
         else if (e.KeyData == Keys.Back)
            Keyboard.kbBS  = true;
         else if (e.KeyData == Keys.Escape)
            Keyboard.kbEsc = true;
         else if (e.KeyData == Keys.Control)
            Keyboard.kCtrl = true;
         else if (e.KeyData == Keys.Alt)
            Keyboard.kAlt = true;
         else if (e.KeyData == Keys.LShiftKey)
            Keyboard.kbShiftl = true;
         else if (e.KeyData == Keys.RShiftKey)
            Keyboard.kbShiftr = true;
    
      }

      private void Form1_KeyUp(object sender, KeyEventArgs e)
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
            Keyboard.kbSP = false;
         else if (e.KeyData == Keys.Enter)
            Keyboard.kbEnter = false;
         else if (e.KeyData == Keys.Tab)
            Keyboard.kbTab = false;
         else if (e.KeyData == Keys.Back)
            Keyboard.kbBS = false;
         else if (e.KeyData == Keys.Escape)
            Keyboard.kbEsc = false;
         else if (e.KeyData == Keys.Control)
            Keyboard.kCtrl = false;
         else if (e.KeyData == Keys.Alt)
            Keyboard.kAlt = false;
         else if (e.KeyData == Keys.LShiftKey)
            Keyboard.kbShiftl = false;
         else if (e.KeyData == Keys.RShiftKey)
            Keyboard.kbShiftr = false;
      }
   }
}
