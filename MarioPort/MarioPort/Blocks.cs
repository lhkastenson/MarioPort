using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//using VGA256;
//using Buffers;
//using BackGr;

namespace MarioPort
{
   public static class Blocks
   {

         public const int BumpHeight = 4;
         public const int MoveDelay = 0;
         public const int W = 1; //temp
         public const int H = 1; //temp


         public static char[] BackGrBuffer = new char[W * (H + BumpHeight)]; 
         //ImageBuffer BlockBuffer; //: ImageBuffer;
         public static bool Bumping;
         public static int BumpX, BumpY, OldBumpX, OldBumpY, DY, YPos, DelayCounter;
         public static byte BumpFillAttr;



        public static void InitBlocks()
        {
             Bumping = false;
        }

        public static void SaveBumpBackGr()
        {
             //FormMarioPort.formRef.GetImage (BumpX, BumpY - BumpHeight, W, H + BumpHeight, BackGrBuffer);
             OldBumpX = BumpX;
             OldBumpY = BumpY;
        }

        public static void BumpBlock(int X, int Y)
        {
             if ( Bumping == true ) 
               return;
             BumpX = X;
             BumpY = Y;
             DY = -1 * BumpHeight;
             //FormMarioPort.formRef.GetImage (X, Y, W, H, BlockBuffer);
             //{  BumpFillAttr = FormMarioPort.GetPixel(X, Y + H - 1); }
             SaveBumpBackGr();
             Bumping = true;
             DelayCounter = 0;
        }

        public static void EraseBlocks()
        {
           if (Bumping == true)
           {
              //FormMarioPort.formRef.PutImage(OldBumpX, OldBumpY - BumpHeight, W, H + BumpHeight, BackGrBuffer);
           }
        }

        public static void DrawBlocks()
        {
             int Y; 
             if (Bumping == true ) 
             {
                if (DY < BumpHeight)
                {
                   SaveBumpBackGr();
                   Y = BumpY - BumpHeight + DY; //Y = BumpY - BumpHeight + Abs (DY);
                   //FormMarioPort.formRef.PutImage(BumpX, Y, W, H, BlockBuffer);
                   //{ Fill(BumpX, Y + H, W, BumpHeight - DY, BumpFillAttr); }
                   //Backgr.DrawBackGrBlock(BumpX, Y + H, W, BumpHeight - Abs (DY));
                }
                else if (DelayCounter >= 4)
                   Bumping = false;
             }
        }

        public static void MoveBlocks()
        {
           if (Bumping == true)
           {
              DelayCounter++;
              if (DelayCounter > MoveDelay && DY < BumpHeight)
              {
                 DY++;
                 DelayCounter = 0;
              }
           }

        }
   }
}
