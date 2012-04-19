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

namespace MarioPort
{
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
        public const int MAX_PAGE = 10; //temp
        public const int W = 1; //temp
        public const int H = 1; //temp

        public class TempRec
        {
           public TempRec()
           {
              this.Visible = new bool[MAX_PAGE];
              this.BackGrAddr = new ushort[MAX_PAGE];
              this.OldX = new int[MAX_PAGE];
              this.OldY = new int[MAX_PAGE];
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
               for( j = 0; j < MAX_PAGE; j++)
                 TempObj[i].Visible[j] = false;
             }
             for (i = 0; i < MaxRemove; i++ )
             {
                RemList[i].Active = false;
             }
             //Recolor(@Part000, @Part000, Options.BrickColor);
        }

        public static void ReadBackGr(int i)
        {
           //int page = FormMarioPort.formRef.CurrentPage();
           //{ FormMarioPort.formRef.GetImage(TempObj[i].XPos, TempObj[i].YPos, TempObj[i].HSize, TempObj[i].VSize, BackGrBuffer[WorkingPage]); }
           //TempObj[i].BackGrAddr[page] = PushBackGr(TempObj[i].XPos, TempObj[i].YPos, TempObj[i].HSize + 4, TempObj[i].VSize);
           //TempObj[i].OldX[page] = XPos;
           //TempObj[i].OldY[page] = YPos;
        }

        public static bool Available(int i)
        {
           int j;
           bool Used;
           Used = TempObj[i].Alive;
           for( j = 0; j <= MAX_PAGE; j++ )
              Used = TempObj[i].Visible[j];

           return !Used;
        }

        public static void NewTempObj(byte NewType, int X, int Y, int XV, int YV, int Wid, int Ht)
        {
             int i, j;
             if (NewType == tpBroken)
             {
                if (XV > 0)
                {
                   //if (X + 32 * XV > Buffers.XView + Buffers.NH * W + 2 * W)
                   //   return;

                   //else if (X + 32 * XV + 2 * W < Buffers.XView)
                   //   return;
                }
                  i = 1;
                while ( Available(i) == false && i <= MaxTempObj) 
                   i++;
                if( i <= MaxTempObj )
                {
                   TempObj[i].Alive = true;
                   for( j = 0; j <= MAX_PAGE; j++)
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
        }

        public static void ShowTempObj()
        {
             int i;
             //int page = FormMarioPort.formRef.CurrentPage();
             for( i = 0; i < MaxTempObj; i++ ) 
             {
                if(TempObj[i].Alive == true )
                {
                   ReadBackGr(i);
                   switch(TempObj[i].Tp)
                   {
                       case tpBroken:
                          //FormMarioPort.formRef.DrawImage(XPos, YPos, HSize, VSize, @Part000^);
                          break;
                       case tpCoin:
                          //FormMarioPort.formRef.DrawImage(XPos, YPos, HSize, VSize, @Coin000^);
                          break;
                       case tpHit:
                          //FormMarioPort.formRef.DrawImage(XPos, YPos, HSize, VSize, @WHHit000^);
                          break;
                       case tpFire:
                          //FormMarioPort.formRef.DrawImage(XPos, YPos, HSize, VSize, @WHFire000^);
                          break;
                       case tpNote:
                          //FormMarioPort.formRef.DrawImage(XPos, YPos, HSize, VSize, @Note000^);
                          break;
                    }
                } 
                //TempObj[i].Visible[page] = true;
             }
        }

        public static void HideTempObj()
        {
            int i;
            //int page = FormMarioPort.formRef.CurrentPage();
             for( i = MaxTempObj; i >= 1; i-- )
             {
                 //if(TempObj[i].Visible[page] == true )
                 //{
                 // { PutImage (OldX [WorkingPage], OldY [WorkingPage], HSize, VSize, BackGrBuffer [WorkingPage]); }
                 //  PopBackGr({OldX [WorkingPage], OldY [WorkingPage], HSize + 4, VSize,} BackGrAddr[page]);
                 //  TempObj[i].Visible[page] = false;
                 //}
             }
        }

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
                           //if (TempObj[i].YPos > Buffers.NV * H)
                              //TempObj[i].Alive = false;
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
                   //RemList[i].RemCount = Succ(MAX_PAGE);
                   RemList[i].Active = true;
                }
             }
        }

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
                        //FormMarioPort.formRef.DrawBackGrBlock(RemList[i].RemX, RemList[i].RemY, RemList[i].RemW, RemList[i].RemH);
                        break;
                     case 1:
                        //FormMarioPort.formRef.DrawImage(RemList[i].RemX, RemList[i].RemY, RemList[i].RemW, RemList[i].RemH, @Quest001^);
                        break;
                     case 2:
                        //FormMarioPort.formRef.DrawImage(RemList[i].RemX, RemList[i].RemY, RemList[i].RemW, RemList[i].RemH, @Quest000^);
                        break;
                     case 5:
                        //FormMarioPort.formRef.DrawImage(RemList[i].RemX, RemList[i].RemY, RemList[i].RemW, RemList[i].RemH, @Note000^);
                        break;
                  }
                 RemList[i].RemCount--;
                 if( RemList[i].RemCount < 1 )
                   RemList[i].Active = false;
               }
             }
        }

        public static void BreakBlock(int X, int Y)
        {
           int X1, Y1, X2, Y2;
           //WorldMap^ [X, Y] = ' ';
           X = X * W;
           Y = Y * H;
           Remove (X, Y, W, H, 0);
           X1 = X; 
           X2 = X + W / 2;
           Y1 = Y; 
           Y2 = Y + H / 2;
           NewTempObj (tpBroken, X1, Y1, -2, -6, 12, H / 2);
           NewTempObj (tpBroken, X2, Y1,  2, -6, 12, H / 2);
           NewTempObj (tpBroken, X1, Y2, -2, -4, 12, H / 2);
           NewTempObj (tpBroken, X2, Y2,  2, -4, 12, H / 2);
           //Beep(110);
        }

        public static void HitCoin(int X, int Y, bool ThrowUp)
        {
             int MapX, MapY;
             MapX = X / W;
             MapY = Y / H;
           //  if (WorldMap^ [MapX, MapY] = ' ' )
           //    return;
           //  if (ThrowUp == true )
           //    NewTempObj(tpCoin, X, Y - H, 0, CoinSpeed, W, H)
           //  else
           //    WorldMap^ [MapX, MapY] = ' ';
           //    Remove(X, Y, W, H, 0);
           //    Glitter.CoinGlitter(X, Y);
           //  Beep(2420);
           //{  StartMusic(CoinMusic); }
           //  Data.Coins[Player]++;
           //  AddScore(50);
           //  if (Data.Coins[Player] % 100 = 0 )
           //  {
           //     AddLife();
           //     Data.Coins[Player] = 0;
           //  }
        }

        public static void AddLife()
        {
            //Data.Lives[Player]++;
            //StartMusic(LifeMusic);
        }
   }
}
