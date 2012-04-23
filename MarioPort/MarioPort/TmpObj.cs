// Author: Peter Braun
//
// Note:    
//
// File Translation Percentage: 50% translated
//-------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//using VGA256;
//using BackGr;
//using Buffers;
//using Glitter;
//using Figures;
//using Music;
//using Crt;

using MarioPort;
﻿using Resources = MarioPort.Properties.Resources;

namespace MarioPort
{
   //----------------------------------------------------------------
   //Static class that handles using temporary objects in the program
   //----------------------------------------------------------------
   public static class TmpObj
   {
        public const int tpBroken = 1;
        public const int tpCoin = 2;
        public const int tpHit = 3;
        public const int tpFire = 4;
        public const int tpNote = 5;

        public const int BrokenDelay = 3;
        public const int CoinSpeed = -4;
        public const int CoinDelay = 12;
        public const int MaxCoinYVel = 6;
        public const int HitTime = 4;

        public const int MaxTempObj = 20;
        public const int MaxRemove  = 10;

        public class TempRec
        {
           public TempRec()
           {
              this.Visible = new bool[FormMarioPort.MAX_PAGE + 1];
              this.BackGrAddr = new ushort[FormMarioPort.MAX_PAGE + 1];
              this.OldX = new int[FormMarioPort.MAX_PAGE + 1];
              this.OldY = new int[FormMarioPort.MAX_PAGE + 1];
              this.Alive = false;
              this.Tp = 0;
              this.XPos = 0;
              this.YPos = 0;
              this.HSize = 0;
              this.VSize = 0;
              this.XVel = 0;
              this.YVel = 0;
              this.DelayCounter = 0;
           }
           public static TempRec Create()
           {
              return new TempRec();
           }
           public bool Alive;
           public bool[] Visible;
           public byte Tp;
             //{  BackGrBuffer: Array [0 .. MAX_PAGE] of ImageBuffer; }
           public ushort[] BackGrAddr;
           public int XPos, YPos, HSize, VSize, XVel, YVel, DelayCounter;
           public int[] OldX;
           public int[] OldY;
        }

        public class RemoveRec
        {
           public bool Active;
           public int RemCount, RemX, RemY, RemW, RemH, NewImage;
           public RemoveRec()
           {
           }
           public static RemoveRec Create()
           {
              return new RemoveRec();
           }
        }

        public static TempRec[] TempObj = new TempRec[] { TempRec.Create(), TempRec.Create(), TempRec.Create(), TempRec.Create(), TempRec.Create(), TempRec.Create(), TempRec.Create(), TempRec.Create(), TempRec.Create(), TempRec.Create(), TempRec.Create(), TempRec.Create(), TempRec.Create(), TempRec.Create(), TempRec.Create(), TempRec.Create(), TempRec.Create(), TempRec.Create(), TempRec.Create(), TempRec.Create() };
        public static RemoveRec[] RemList = new RemoveRec[] { RemoveRec.Create(), RemoveRec.Create(), RemoveRec.Create(), RemoveRec.Create(), RemoveRec.Create(), RemoveRec.Create(), RemoveRec.Create(), RemoveRec.Create(), RemoveRec.Create(), RemoveRec.Create()};

        public static void InitTempObj()
        {
             int i, j;
             for (i = 0; i < MaxTempObj; i++ )
             {
               TempObj[i].Alive = false;
               for (j = 0; j < FormMarioPort.MAX_PAGE; j++)
                 TempObj[i].Visible[j] = false;
             }
             for (i = 0; i < MaxRemove; i++ )
             {
                RemList[i].Active = false;
             }
             //Recolor(@Part000, @Part000, Options.BrickColor);
        }

        //----------------------------------------------------------------
        //Method to read the background image
        //----------------------------------------------------------------
        public static void ReadBackGr(int i)
        {
           int page = FormMarioPort.formRef.CurrentPage();
           //{ FormMarioPort.formRef.GetImage(TempObj[i].XPos, TempObj[i].YPos, TempObj[i].HSize, TempObj[i].VSize, BackGrBuffer[WorkingPage]); }
           TempObj[i].BackGrAddr[page] = FormMarioPort.formRef.PushBackGr(TempObj[i].XPos, TempObj[i].YPos, TempObj[i].HSize + 4, TempObj[i].VSize);
           TempObj[i].OldX[page] = TempObj[i].XPos;
           TempObj[i].OldY[page] = TempObj[i].YPos;
        }

        //----------------------------------------------------------------
        //Method to determine if the object is available to be changed 
        //or not
        //----------------------------------------------------------------
        public static bool Available(int i)
        {
           int j;
           bool Used;
           Used = TempObj[i].Alive;
           for (j = 0; j <= FormMarioPort.MAX_PAGE; j++)
              Used = Used || TempObj[i].Visible[j];

           return !Used;
        }

        //----------------------------------------------------------------
        //Method to create a new temporary object
        //----------------------------------------------------------------
        public static void NewTempObj(byte NewType, int X, int Y, int XV, int YV, int Wid, int Ht)
        {
            int i, j;
            if (NewType == tpBroken)
            {
               if (XV > 0)
               {
                  if (X + 32 * XV > Buffers.XView + Buffers.NH * Buffers.W + 2 * Buffers.W)
                     return;

                  else if (X + 32 * XV + 2 * Buffers.W < Buffers.XView)
                     return;
               }
            }

            i = 1;
            while ( Available(i) && i <= MaxTempObj) 
               i++;
            if( i <= MaxTempObj )
            {
               TempObj[i].Alive = true;
               for( j = 0; j <= FormMarioPort.MAX_PAGE; j++)
               {
                  TempObj[i].Visible[j] = false;
                  TempObj[i].Tp = NewType;
                  TempObj[i].XPos = X;
                  TempObj[i].YPos = Y;
                  TempObj[i].XVel = XV;
                  TempObj[i].YVel = YV;
                  TempObj[i].HSize = Wid;
                  TempObj[i].VSize = Ht;
                  ReadBackGr(i);
                  TempObj[i].DelayCounter = 0;
               }
            }
        }

        //----------------------------------------------------------------
        //Method to show the temp object
        //----------------------------------------------------------------
        public static void ShowTempObj()
        {
             int i;
             int page = FormMarioPort.formRef.CurrentPage();
             for( i = 0; i < MaxTempObj; i++ ) 
             {
                if(TempObj[i].Alive == true )
                {
                   ReadBackGr(i);
                   switch(TempObj[i].Tp)
                   {
                       case tpBroken:
                          FormMarioPort.formRef.DrawImage(TempObj[i].XPos, TempObj[i].YPos, TempObj[i].HSize, TempObj[i].VSize, Resources.PART_000);
                          break;
                       case tpCoin:
                          FormMarioPort.formRef.DrawImage(TempObj[i].XPos, TempObj[i].YPos, TempObj[i].HSize, TempObj[i].VSize, Resources.COIN_000);
                          break;
                       case tpHit:
                          FormMarioPort.formRef.DrawImage(TempObj[i].XPos, TempObj[i].YPos, TempObj[i].HSize, TempObj[i].VSize, Resources.WHHIT_000);
                          break;
                       case tpFire:
                          FormMarioPort.formRef.DrawImage(TempObj[i].XPos, TempObj[i].YPos, TempObj[i].HSize, TempObj[i].VSize, Resources.WHFIRE_000);
                          break;
                       case tpNote:
                          FormMarioPort.formRef.DrawImage(TempObj[i].XPos, TempObj[i].YPos, TempObj[i].HSize, TempObj[i].VSize, Resources.NOTE_000);
                          break;
                    }
                } 
                TempObj[i].Visible[page] = true;
             }
        }

        //----------------------------------------------------------------
        //Method to hide the temp object
        //----------------------------------------------------------------
        public static void HideTempObj()
        {
            int i;
            int page = FormMarioPort.formRef.CurrentPage();
             for( i = MaxTempObj; i >= 1; i-- )
             {
                 if(TempObj[i].Visible[page] == true )
                 {
                   //FormMarioPort.formRef.PopBackGr(BackGrAddr[page]);
                   TempObj[i].Visible[page] = false;
                 }
             }
        }

        //----------------------------------------------------------------
        // Method to be able to move the temp object
        //----------------------------------------------------------------
        public static void MoveTempObj()
        {
             int i;
             for (i = 0; i < MaxTempObj; i++ )
             {
                 if(TempObj[i].Alive == true )
                 {
                   switch(TempObj[i].Tp)
                   {
                     case tpBroken:
                        TempObj[i].DelayCounter++;
                        if( TempObj[i].DelayCounter > BrokenDelay)
                        {
                           TempObj[i].DelayCounter = 0;
                           TempObj[i].YVel++;
                           if (TempObj[i].YPos > Buffers.NV * Buffers.H)
                              TempObj[i].Alive = false;
                        }
                        break;
                     case tpCoin:
                         TempObj[i].DelayCounter++;
                         if (TempObj[i].DelayCounter > CoinDelay)
                         {
                           TempObj[i].YVel++;
                           if (TempObj[i].YVel > MaxCoinYVel)
                           {
                             TempObj[i].Alive = false;
                             Glitter.CoinGlitter(TempObj[i].XPos + TempObj[i].XVel, TempObj[i].YPos + TempObj[i].YVel);
                           }
                         }
                        break;
                      case tpHit:
                         TempObj[i].DelayCounter++;
                         if ( TempObj[i].DelayCounter > HitTime )
                           TempObj[i].Alive = false;
                        break;
                      case tpFire:
                         TempObj[i].DelayCounter++;
                         if ( TempObj[i].DelayCounter > HitTime )
                           TempObj[i].Alive = false;
                        break;
                   }
                 }
                 TempObj[i].XPos++;
                 TempObj[i].XVel++;
                 TempObj[i].YPos++;
                 TempObj[i].YVel++; 
             }
        }

        //----------------------------------------------------------------
        //Method to remove the temp object
        //----------------------------------------------------------------
        public static void Remove(int X, int Y, int W, int H, int NewImg)
        {
             int i;
             if (Y < 0 )
               return;
             i = 1;
             while (RemList[i].Active == true && i <= MaxRemove)
             {
                i++;
                if (i <= MaxRemove)
                {
                   RemList[i].RemX = X;
                   RemList[i].RemY = Y;
                   RemList[i].RemW = W;
                   RemList[i].RemH = H;
                   RemList[i].NewImage = NewImg;
                   RemList[i].RemCount = (FormMarioPort.MAX_PAGE) + 1;
                   RemList[i].Active = true;
                }
             }
        }

        //----------------------------------------------------------------
        //Method that does a series of removes
        //----------------------------------------------------------------
        public static void RunRemove()
        {
             int i;
             for( i = 0; i < MaxRemove; i++ )
             {
               if (RemList[i].Active == true )
               {
                  switch(RemList[i].NewImage)
                  {
                     case 0:
                        BackGr.DrawBackGrBlock(RemList[i].RemX, RemList[i].RemY, RemList[i].RemW, RemList[i].RemH);
                        break;
                     case 1:
                        FormMarioPort.formRef.DrawImage(RemList[i].RemX, RemList[i].RemY, RemList[i].RemW, RemList[i].RemH, Resources.QUEST_001);
                        break;
                     case 2:
                        FormMarioPort.formRef.DrawImage(RemList[i].RemX, RemList[i].RemY, RemList[i].RemW, RemList[i].RemH, Resources.QUEST_000);
                        break;
                     case 5:
                        FormMarioPort.formRef.DrawImage(RemList[i].RemX, RemList[i].RemY, RemList[i].RemW, RemList[i].RemH, Resources.NOTE_000);
                        break;
                  }
                 RemList[i].RemCount--;
                 if( RemList[i].RemCount < 1 )
                   RemList[i].Active = false;
               }
             }
        }

        //----------------------------------------------------------------
        //Method to handle the breaking of block objects
        //----------------------------------------------------------------
        public static void BreakBlock(int X, int Y)
        {
           int X1, Y1, X2, Y2;
           Buffers.WorldMap[X, Y] = ' ';
           X = X * Buffers.W;
           Y = Y * Buffers.H;
           Remove(X, Y, Buffers.W, Buffers.H, 0);
           X1 = X;
           X2 = X + Buffers.W / 2;
           Y1 = Y;
           Y2 = Y + Buffers.H / 2;
           NewTempObj(tpBroken, X1, Y1, -2, -6, 12, Buffers.H / 2);
           NewTempObj(tpBroken, X2, Y1, 2, -6, 12, Buffers.H / 2);
           NewTempObj(tpBroken, X1, Y2, -2, -4, 12, Buffers.H / 2);
           NewTempObj(tpBroken, X2, Y2, 2, -4, 12, Buffers.H / 2);
           //Beep(110);
        }

        //----------------------------------------------------------------
        //Method to handle when a coin object is hit
        //----------------------------------------------------------------
         public static void HitCoin(int X, int Y, bool ThrowUp)
         {
            int MapX, MapY;
            MapX = X / Buffers.W;
            MapY = Y / Buffers.H;
            if (Buffers.WorldMap[MapX, MapY] == ' ' )
               return;
            if (ThrowUp)
               NewTempObj(tpCoin, X, Y - Buffers.H, 0, CoinSpeed, Buffers.W, Buffers.H);
            else
            {
               Buffers.WorldMap[MapX, MapY] = ' ';
               Remove(X, Y, Buffers.W, Buffers.H, 0);
               Glitter.CoinGlitter(X, Y);
            }

            Buffers.data.coins[Buffers.Player]++;
            Buffers.AddScore(50);
            if (Buffers.data.coins[Buffers.Player] % 100 == 0 )
            {
               AddLife();
               Buffers.data.coins[Buffers.Player] = 0;
            }
         }

        //----------------------------------------------------------------
        //Method to handle adding a life to total lives
        //----------------------------------------------------------------
        public static void AddLife()
        {
            Buffers.data.lives[Buffers.Player]++;
            //StartMusic(LifeMusic);
        }
   }
}
