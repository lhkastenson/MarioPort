
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing;
using MarioPort;
﻿using Resources = MarioPort.Properties.Resources;

/*
 * 
 */

namespace MarioPort
{
   // Temporary forward declarations
   class Game {};
   class Texture2D {};

   enum EnemyType
   {
      tpDead            =  0,
      tpDying           =  1,
      tpChibibo         =  2,
      tpFlatChibibo     =  3,
      tpDeadChibibo     =  4,
      tpRisingChamp     =  5,
      tpChamp           =  6,
      tpRisingLife      =  7,
      tpLife            =  8,
      tpRisingFlower    =  9,
      tpFlower          = 10,
      tpRisingStar      = 11,
      tpStar            = 12,
      tpFireBall        = 13,
      tpDyingFireBall   = 14,
      tpVertFish        = 15,
      tpDeadVertFish    = 16,
      tpVertFireBall    = 17,
      tpVertPlant       = 18,
      tpDeadVertPlant   = 19,
      tpRed             = 20,
      tpDeadRed         = 21,
      
      tpKoopa           = 50,
      tpSleepingKoopa   = 51,
      tpWakingKoopa     = 52,
      tpRunningKoopa    = 53,
      tpDyingKoopa      = 54,
      tpDeadKoopa       = 55,

      tpLiftStart       = 60,
      tpBlockLift       = 60,
      tpDonut           = 61,
      tpLiftEnd         = 69,
   }

   class Enemies
   {
      public const int StartEnemiesAt  = 2;
      public const int ForgetEnemiesAt = 5;

      public const int Left = 0;
      public const int Right = 1;
         
      public const int kGreen = 0;
      public const int kRed = 1;
      
      public const bool Turbo = false;
      
      public byte cdChamp;
      public byte cdLife;
      public byte cdFlower;
      public byte cdStar;
      public byte cdEnemy;
      public byte cdHit;
      public byte cdLift;
      public byte cdStopJump;
      
      public int PlayerX1;
      public int PlayerY1;
      public int PlayerX2;
      public int PlayerY2;
      public int PlayerXVel;
      public int PlayerYVel;
      
      public bool conStar;
      
      public Enemies()
      {
         // init FireballList && KoopaList
      }
      
      //// implementation ////

      private byte[,] rKoopa = new byte[4, 20 * 24 + 1];

      private const Image[] FireBallList = new Image[4];
      private const Image[,,] KoopaList = new Image[Right - Left + 1, kRed - kGreen + 1, 2];

      private const int Grounded = 0;
      private const int Falling = 1;

      private const int MaxEnemies = 11;
      private const int MaxEnemiesAtOnce = 25;

      int i = MarioPort.Buffers.W;

      int j = Buffers.W;

      struct EnemyRec
      {
         public EnemyType Tp;
         public int SubTp;
         public int XPos;
         public int YPos;
         public int LastXPos;
         public int LastYPos;
         public int MapX;
         public int MapY;
         public int XVel;
         public int YVel;
         public int MoveDelay;
         public int DelayCounter;
         public int Counter;
         public int Status;
         public byte DirCounter;
         
         public uint[] BackGrAddr = new uint[Game.MAX_PAGE + 1];
      }

//          EnemyListPtr = EnemyList;
      private EnemyRec[] EnemyList = new EnemyRec[MaxEnemiesAtOnce];

//      Enemy: EnemyListPtr;
//      private EnemyRec* Enemy = &EnemyList;
      
//      ActiveEnemies: String [MaxEnemiesAtOnce];
      private string ActiveEnemies;
//      NumEnemies: (byte)absolute ActiveEnemies;
      private byte NumEnemies;
      // TODO
//      TimeCounter: Byte;
      private byte TimeCounter;

      private Image[,] EnemyPictures = new Image[MaxEnemies, Right - Left + 1];

      private void Kill(int i)
      {
         EnemyRec enemyRec =  EnemyList[i];

         switch (enemyRec.Tp)
         {
            case EnemyType.tpChibibo:
            {
               enemyRec.Tp = EnemyType.tpDeadChibibo;
               enemyRec.XVel = -1 + 2 * (byte) ((enemyRec.XPos + enemyRec.XVel) % Buffers.W > Buffers.W / 2);
               enemyRec.YVel = -4;
               enemyRec.MoveDelay = 0;
               enemyRec.DelayCounter = 0;
               Buffers.AddScore(100);
               break;
            }
            case EnemyType.tpRed:
            {
               enemyRec.Tp = EnemyType.tpDeadRed;
               enemyRec.XVel = -1 + 2 * (byte) ((enemyRec.XPos + enemyRec.XVel) % Buffers.W > Buffers.W / 2);
               enemyRec.YVel = -4;
               enemyRec.MoveDelay = 0;
               enemyRec.DelayCounter = 0;
               Buffers.AddScore(100);
               break;
            }
            case EnemyType.tpKoopa:
            case EnemyType.tpSleepingKoopa:
            case EnemyType.tpWakingKoopa:
            case EnemyType.tpRunningKoopa:
            {
               enemyRec.Tp = EnemyType.tpDeadKoopa;
               enemyRec.XVel = -1 + 2 * (byte) ((enemyRec.XPos + enemyRec.XVel) % Buffers.W > Buffers.W / 2);
               enemyRec.YVel = -4;
               enemyRec.MoveDelay = 0;
               enemyRec.DelayCounter = 0;
               Buffers.AddScore(100);
               break;
            }
            case EnemyType.tpVertFish:
            {
               enemyRec.Tp = EnemyType.tpDeadVertFish;
               enemyRec.XVel = 0;
               enemyRec.YVel = 0;
               enemyRec.MoveDelay = 2;
               enemyRec.DelayCounter = 0;
               enemyRec.Status = Falling;
               Buffers.AddScore(100);
               break;
            }
            case EnemyType.tpVertPlant:
            {
               enemyRec.Tp = EnemyType.tpDeadVertPlant;
               enemyRec.DelayCounter = 0;
               enemyRec.YVel = 0;
               Buffers.AddScore(100);
               break;
            }
            default:
//               throw new Exception();
               break;
         }
      }
      
      private void ShowStar (int X, int Y)
      {
//         Beep (100);
         if ( (X + Buffers.W > XView) && (X < XView + SCREEN_WIDTH) )
            NewTempObj(tpHit, X, Y, 0, 0, Buffers.W, Buffers.H);
      }

      private void ShowFire (int X, int Y)
      {
         //Beep (50);
         X = X - 4;
         Y = Y - 4;
         if ( (X + Buffers.W > XView) && (X < XView + SCREEN_WIDTH) )
            NewTempObj(tpFire, X, Y, 0, 0, Buffers.W, Buffers.H);
      }

      /*
      private void Mirror20x24 (P1, P2: Pointer)
      {
    
        const
          Buffers.W = 20;
          H = 24;
        type
          PlaneBuffer = array[0..H - 1, 0..W / 4 - 1] of Byte;
          PlaneBufferArray = array[0..3] of PlaneBuffer;
          PlaneBufferArrayPtr = PlaneBufferArray;
        var
          Source, Dest: PlaneBufferArrayPtr;
      }*/
      
      private void Mirror20x24(ref Bitmap from, ref Bitmap to)
      {
         
      }
      
      /*
      private void Swap (byte Plane1, byte Plane2)
      {
             var
               i, j: Byte;
           {
             for j = 0 to H - 1 do
               for i = 0 to Buffers.W / 4 - 1 do
               {
                 Dest[Plane2, j, i] = Source[Plane1, j, Buffers.W / 4 - 1 - i];
                 Dest[Plane1, j, i] = Source[Plane2, j, Buffers.W / 4 - 1 - i];
               }
           Source = P1;
           Dest = P2;
           Swap (0, 3);
           Swap (1, 2);
         }
      }
      */

      public void InitEnemyFigures()
      {
         EnemyPictures[1, Right] = Resources.CHIBIBO_000;
         EnemyPictures[2, Right] = Resources.CHIBIBO_001;

         EnemyPictures[4, Right] = Resources.CHIBIBO_002;
         EnemyPictures[5, Right] = Resources.CHIBIBO_003;

         EnemyPictures[3, Left] = Resources.FISH_001;
         Mirror(EnemyPictures[3, Left], EnemyPictures[3, Right]);

         EnemyPictures[6, Left] = Resources.RED_000;
         EnemyPictures[7, Left] = Resources.RED_001;

         EnemyPictures[8, Right] = Resources.GRKP_000;
         EnemyPictures[9, Right] = Resources.GRKP_001;

         EnemyPictures[10, Right] = Resources.RDKP_000;
         EnemyPictures[11, Right] = Resources.RDKP_001;

         for( int i = 0; i < MaxEnemies; i++ )
         {
            if ( i == 6 || i == 7 )
               Mirror(EnemyPictures[i, Left], @EnemyPictures[i, Right]);
            else
               if ( i != 3 )
                  Mirror(EnemyPictures[i, Left], @EnemyPictures[i, Right]);
         }
         
         for( int i = 0; i <= 1; i++ )
         {
            for( int j = kGreen; j <= kRed; j++ )
            {
               Mirror20x24(KoopaList[Left, j, i], KoopaList[Right, j, i]);
            }
         }
      }

      public void ClearEnemies()
      {
         for (int i = 0; i < MaxEnemiesAtOnce; i++)
         {
            EnemyList[i].Tp = EnemyType.tpDead;
         }
         
         NumEnemies = 0;
         cdChamp = 0;
         cdLife = 0;
         cdFlower = 0;
         cdStar = 0;
         cdEnemy = 0;
         cdHit = 0;
         cdLift = 0;
         cdStopJump = 0;
      }

      public void StopEnemies()
      {
         int j;
         for (int i = 0; i < NumEnemies; i++)
         {
            j = (int) ActiveEnemies[i];
            
            switch (EnemyList[j].Tp)
            {
               case EnemyType.tpChibibo:
                  WorldMap[EnemyList[j].MapX, EnemyList[j].MapY] = '';
                  break;
               case EnemyType.tpVertFish:
                  WorldMap[EnemyList[j].MapX, EnemyList[j].MapY - 2] = '';
                  break;
               case EnemyType.tpVertFireBall:
                  WorldMap[EnemyList[j].MapX, EnemyList[j].MapY - 2] = '';
                  break;
               case EnemyType.tpVertPlant:
                  WorldMap[EnemyList[j].MapX, EnemyList[j].MapY - 2] = (char)((int)('') + EnemyList[j].SubTp);
                  break;
               case EnemyType.tpRed:
                  WorldMap[EnemyList[j].MapX, EnemyList[j].MapY] = '';
                  break;
               case EnemyType.tpKoopa:
               case EnemyType.tpSleepingKoopa:
               case EnemyType.tpWakingKoopa:
               case EnemyType.tpRunningKoopa:
                  WorldMap[EnemyList[j].MapX, EnemyList[j].MapY] = (char)((int)('') + EnemyList[j].SubTp);
                  break;
               case EnemyType.tpBlockLift:
                  WorldMap[EnemyList[j].MapX, EnemyList[j].MapY] = '°';
                  break;
               case EnemyType.tpDonut:
                  WorldMap[EnemyList[j].MapX, EnemyList[j].MapY] = '±';
                  break;
               default:
//                  throw new Exception();
                  break;
            }
         }

         ClearEnemies();
      }

      public void NewEnemy(EnemyType InitType, int SubType, int InitX, int InitY, int InitXVel, int InitYVel, int InitDelay)
      {
         int i, j;
         if (Turbo)
         {
            InitXVel = InitXVel * 2;
            InitYVel = InitYVel * 2;
            InitDelay = InitDelay / 2;
         }
         if (InitType == EnemyType.tpFireBall)
         {
            j = 0;
                  
            for(i = 0; i < NumEnemies; i++)
            {
               if (EnemyList[(int)(ActiveEnemies[i])].Tp == EnemyType.tpFireBall)
                  j++;
            }
                  
            if (j >= 2)
               return;
            //StartMusic (FireMusic);
         }

         i = 1;
         
         while (EnemyList[i].Tp != EnemyType.tpDead)
         {
            if (i < MaxEnemiesAtOnce)
               i++;
            else
               return;
         }
         
         EnemyList[i].Tp = InitType;
         EnemyList[i].SubTp = SubType;
         EnemyList[i].MapX = InitX;
         EnemyList[i].MapY = InitY;
         EnemyList[i].XPos = EnemyList[i].MapX * Buffers.W;
         EnemyList[i].YPos = EnemyList[i].MapY * H;
         EnemyList[i].XVel = InitXVel;
         EnemyList[i].YVel = InitYVel;
         EnemyList[i].MoveDelay = InitDelay;
         EnemyList[i].DelayCounter = 0;
         EnemyList[i].DirCounter = 0;
         EnemyList[i].Status = Grounded;
         
         //FillChar (BackGrAddr, SizeOf (BackGrAddr), 0xFF);
         for (int k = 0; k < EnemyList[i].BackGrAddr.Length; k++)
         {
            EnemyList[i].BackGrAddr[k] = 0xFF;
         }

         EnemyList[i].Counter = 0;
//         case Tp of
         switch (EnemyList[i].Tp)
         {
            case EnemyType.tpVertPlant:
            {
               EnemyList[i].XPos += 8;
               EnemyList[i].Status = 0;
               break;
            }
            case EnemyType.tpFireBall:
            {
               if (EnemyList[i].XVel > 0)
                  EnemyList[i].XPos = PlayerX2;
               else
                  EnemyList[i].XPos = PlayerX1;
               break;
            }
         }
         
         EnemyList[i].LastXPos = EnemyList[i].XPos;
         EnemyList[i].LastYPos = EnemyList[i].YPos;
         
         ActiveEnemies = ActiveEnemies + (char) (i);
      }

      public void ShowEnemies()
      {
//      var
//        i, j, Page: Integer;
//        Fig: Pointer;

         int i, j, Page;
         Texture2D Fig;

         Page = Game.CurrentPage;
//         for i = 1 to NumEnemies do
         for (i = 0; i < NumEnemies; i++)
         {
            j = (int)(ActiveEnemies[i]);
//            with EnemyList[j] do
            if ( (EnemyList[j].XPos + 1 * Buffers.W < XView) || 
                     (EnemyList[j].XPos > XView + SCREEN_WIDTH + 0 * Buffers.W) || 
                           (EnemyList[j].YPos >= YView + SCREEN_HEIGHT) )
            {
               EnemyList[j].BackGrAddr[Page] = 0xFFFF;
            }
            else
            {
               if ( EnemyList[j].Tp == EnemyType.tpFireBall || EnemyList[j].Tp == EnemyType.tpDyingFireBall )
                  EnemyList[j].BackGrAddr[Page] = Game.PushBackGr(EnemyList[j].XPos, EnemyList[j].YPos, Buffers.W, Buffers.H / 2);
               else
                  if ( EnemyList[j].Tp == EnemyType.tpVertPlant || EnemyList[j].Tp == EnemyType.tpDeadVertPlant )
                     EnemyList[j].BackGrAddr[Page] = Game.PushBackGr(EnemyList[j].XPos, EnemyList[j].YPos, 24, 20);
                  else
                     if ( EnemyList[j].Tp == EnemyType.tpKoopa ||     
                          EnemyList[j].Tp == EnemyType.tpSleepingKoopa ||
                          EnemyList[j].Tp == EnemyType.tpWakingKoopa ||
                          EnemyList[j].Tp == EnemyType.tpRunningKoopa ||
                          EnemyList[j].Tp == EnemyType.tpDyingKoopa ||
                          EnemyList[j].Tp == EnemyType.tpDeadKoopa )
                        EnemyList[j].BackGrAddr[Page] = Game.PushBackGr(EnemyList[j].XPos, EnemyList[j].YPos - 10, 24, 24);
                     else
                        EnemyList[j].BackGrAddr[Page] = Game.PushBackGr(EnemyList[j].XPos, EnemyList[j].YPos, Buffers.W + 4, H);
            }
//            case Tp of
            switch (Tp)
            {
               case EnemyType.tpChibibo:
                  DrawImage(XPos, YPos, Buffers.W, H, EnemyPictures[1 + 3 * SubTp, (byte)(DirCounter % 32 < 16)]);
                  break;
               case EnemyType.tpFlatChibibo:
                  DrawImage(XPos, YPos, Buffers.W, H, EnemyPictures[2 + 3 * SubTp, (byte)(DirCounter % 32 < 16)]);
                  break;
               case EnemyType.tpDeadChibibo:
                  UpSideDown(XPos, YPos, Buffers.W, H, EnemyPictures[1, Left]);
                  break;
               case EnemyType.tpRisingChamp:
                  if ( YPos != (MapY * H) )
                     if ( SubTp == 0 )
                        DrawPart(XPos, YPos, Buffers.W, H, 0, H - YPos % H - 1, @Champ000)
                     else
                        DrawPart(XPos, YPos, Buffers.W, H, 0, H - YPos % H - 1, @Poison000);
                  break;
               case EnemyType.tpChamp:
                  if ( SubTp == 0 )
                     DrawImage(XPos, YPos, Buffers.W, H, @Champ000)
                  else
                     DrawImage(XPos, YPos, Buffers.W, H, @Poison000);
                  break;
               case EnemyType.tpRisingLife:
                  if ( YPos != (MapY * H) )
                     DrawPart(XPos, YPos, Buffers.W, H, 0, H - YPos % H - 1, @Life000);
                  break;
               case EnemyType.tpLife:
                	DrawImage(XPos, YPos, Buffers.W, H, @Life000);
                	break;
               case EnemyType.tpRisingFlower:
                  if ( YPos != (MapY * H) )
                     DrawPart(XPos, YPos, Buffers.W, H, 0, H - YPos % H - 1, @Flower000);
                  break;
               case EnemyType.tpFlower:
                	DrawImage(XPos, YPos, Buffers.W, H, @Flower000);
                	break;
               case EnemyType.tpRisingStar:
                  if ( YPos != (MapY * H) )
                     DrawPart(XPos, YPos, Buffers.W, H, 0, H - YPos % H - 1, @Star000);
                  break;
               case EnemyType.tpStar:
                	DrawImage(XPos, YPos, Buffers.W, H, @Star000);
                	break;
               case EnemyType.tpFireBall:
                  if ( XPos % 4 < 2 )
                     DrawImage(XPos, YPos, 12, H / 2, @Fire000);
                  else
                     DrawImage(XPos, YPos, 12, H / 2, @Fire001);
                  break;
               case EnemyType.tpVertFish:
                  if ( (YVel != 0) || (YPos < NV * H - H) )
                     DrawImage(XPos, YPos, Buffers.W, H, EnemyPictures[3, (byte)(PlayerX1 > XPos)]);
                  break;
               case EnemyType.tpDeadVertFish:
                  if ( (YPos < NV * H - H) || (YVel != 0) )
                     UpSideDown(XPos, YPos, Buffers.W, H, EnemyPictures[3, (byte)(PlayerX1 <= XPos)]);
                  break;
               case EnemyType.tpVertFireBall:
               {
                  if ( Math.Abs(DelayCounter - MoveDelay) <= 1 )
                  {
                     DrawImage(XPos, YPos, Buffers.W, H, FireBallList [Random (4)]);
                     NewGlitter(XPos + Random (W), YPos + Random (H), 57 + Random (7), 14 + Random (20));
                     NewStar(XPos + Random (W), YPos + Random (H), 57 + Random (7), 14 + Random (20));
                  }
                  break;
               }
               case EnemyType.tpVertPlant:
               {
                  if ( TimeCounter % 32 < 16 )
                  {
//                    case SubTp of
                     switch (SubTp)
                     {
                        case 0:
                        case 1:
                           Fig = @PPlant002;
                           break;
                        default:
                           Fig = @PPlant000;
                     }
                  }
                  else
                  {
                     //case SubTp of
                     switch (SubTp)
                     {
                        case 0:
                        case 1:
                           Fig = @PPlant003;
                           break;
                        default:
                           Fig = @PPlant001;
                     }
                  }
                  DrawPart(XPos, YPos, 24, 20, 0, (MapY * H) - YPos - 1, Fig);
                  break;
               }
               case EnemyType.tpDeadVertPlant:
               {
                  DelayCounter = 0;
                  MoveDelay = 0;
                  YVel = 0;
                  Status++;
                  if ( Status < 12 )
                     DrawImage(XPos, YPos, 24, 20, @Hit000)
                  else
                     if ( Status > 14 )
                        Tp = EnemyType.tpDying;
                  break;
               }
               case EnemyType.tpRed:
                  DrawImage(XPos, YPos, Buffers.W, H, EnemyPictures[6 + (byte)(DirCounter % 16 <= 8), (byte)(XVel > 0)]);
                  break;
               case EnemyType.tpDeadRed:
                  UpSideDown(XPos, YPos, Buffers.W, H, EnemyPictures[6 + (byte)(DirCounter % 16 <= 8), (byte)(XVel > 0)]);
                  break;
               case EnemyType.tpKoopa:
                  DrawImage(XPos, YPos - 10, Buffers.W, 24, KoopaList [(byte)(XVel > 0), SubTp, (byte)(DirCounter % 16 <= 8)]);
                  break;
               case EnemyType.tpWakingKoopa, EnemyType.tpRunningKoopa:
                  DrawImage(XPos, YPos, Buffers.W, H, EnemyPictures[8 + 2 * SubTp + 1 - (byte)(DirCounter % 16 <= 8), (byte)(DirCounter % 32 <= 16)]);
                  break;
               case EnemyType.tpSleepingKoopa:
                  DrawImage(XPos, YPos, Buffers.W, H,  EnemyPictures[8 + 2 * SubTp, 0]);
                  break;
               case EnemyType.tpDeadKoopa:
                  UpSideDown(XPos, YPos, Buffers.W, H, EnemyPictures[8 + 2 * SubTp, (byte)(DirCounter % 16 <= 8)]);
                  break;
               case EnemyType.tpBlockLift:
                  DrawImage(XPos, YPos, Buffers.W, H, @Lift1000);
                  break;
               case EnemyType.tpDonut:
               {
                  if ( Status == 0 )
                  {
                     DrawImage(XPos, YPos, Buffers.W, H, @Donut000);
                     if ( YVel == 0 )
                        Counter = 0;
                  }
                  else
                  {
                     DrawImage(XPos, YPos, Buffers.W, H, @Donut001);
                     Status--;
                  }
                  if ( YVel > 0 )
                     if ( Counter % 24 == 0 )
                        YVel++;
                  Counter++;
                  break;
               }
            }
         }
      }
      
      public void HideEnemies()
      {
         int i, j, Page;
         Page = Game.CurrentPage;
         for ( i = NumEnemies - 1; i >= 0; i++)
         {
            j = (int)(ActiveEnemies[i]);
//            with EnemyList[j] do
            if (EnemyList[j].BackGrAddr[Page] != 0xFFFF)
               PopBackGr( EnemyList[j].BackGrAddr[Page] );
         }
      }

      private void Check (int i)
      {
//      const
//        Safe = EY1;
//        HSafe = H * Safe;
         
         const int Safe = EY1;
         const int HSafe = H * Safe;
         
//      var
//        NewCh1, NewCh2, Ch: Char;
//        j, k, l, Side, AtX, NewX,
//        NewX1, NewX2, Y1, Y2, NewY: Integer;
//        Hold1, Hold2: Boolean;
//        X, Y: Integer;

         char NewCh1, NewCh2, Ch;
         int j, k, l, Side, AtX, NewX, NewX1, NewX2, Y1, Y2, NewY;
         bool Hold1, Hold2;
         int X, Y;

//        with EnemyList[i] do

//          case Tp of
         switch (Tp)
         {
            case EnemyType.tpRisingChamp:
            case EnemyType.tpRisingLife:
            case EnemyType.tpRisingFlower:
            case EnemyType.tpRisingStar:
            {
               if ( ((YPos / H) == (YPos / H)) && (YPos != MapY * H) )
               {
                  XVel = 1 - 2 * (byte)(WorldMap[MapX + 1, MapY - 1] in CanHoldYou);
//                  case Tp of
                  switch (Tp)
                  {
                     case EnemyType.tpRisingChamp:
                        Tp = EnemyType.tpChamp;
                        break;
                     case EnemyType.tpRisingLife:
                     {
                        Tp = EnemyType.tpLife;
                        XVel = 2 * XVel;
                        break;
                     }
                     case EnemyType.tpRisingFlower:
                     {
                        XVel = 0;
                        Tp = EnemyType.tpFlower;
                        break;
                     }
                     case EnemyType.tpRisingStar:
                     {
                        Tp = EnemyType.tpStar;
                        XVel = 2 * XVel;
                        break;
                     }
                  }
                  YVel = -7;
                  MoveDelay = 1;
                  Status = Falling;
               }
               else
               {
                  j = (YPos % H);
                  if j % 2 = 0 )
                     Beep (130 - 20 * j);
//                  Exit;
                  return;
               }
               break;
            }
            case EnemyType.tpFireBall:
            {
               AtX = (XPos + Buffers.W / 4) / Buffers.W;
               NewX = (XPos + Buffers.W / 4 + XVel) / Buffers.W;
               if ( (AtX != NewX) || (PlayerX1 % Buffers.W == 0) )
               {
                  Y1 = (YPos + H / 4 + HSafe) / H - Safe;
                  NewCh1 = WorldMap[NewX, Y1];
                  if ( NewCh1 in CanHoldYou )
                     XVel = 0;
               }
               NewX = XPos;
               NewY = YPos;
               AtX = (XPos + Buffers.W / 4 + XVel) / Buffers.W;
               NewY = (YPos + 2 + H / 4 + YVel + HSafe) / H - Safe;
               NewCh1 = WorldMap[AtX, NewY];
               if ( (YVel > 0) && (NewCh1 in CanHoldYou + CanStandOn) )
               {
                  YPos = ((YPos + YVel - 5 + HSafe) / H - Safe) * H;
                  YVel = -2;
               }
               else
                  if ( XPos % 3 == 0 )
                     Inc (YVel);
               if ( (XVel == 0) || (NewX < XView - Buffers.W) || (NewX > XView + NH * Buffers.W + Buffers.W) || (NewY > NV * H) )
               {
                  DelayCounter = - (Game.MAX_PAGE + 1);
                  Tp = EnemyType.tpDyingFireBall;
               }
//                Exit;
               return;
            }
            case EnemyType.tpStar:
               StartGlitter(XPos, YPos, Buffers.W, H);
               break;
         }


         if ( !(Tp in [tpVertFish, EnemyType.tpDeadVertFish, EnemyType.tpVertFireBall, EnemyType.tpVertPlant, EnemyType.tpDeadVertPlant]) )
         {
            Side = Integer (XVel > 0) * (W - 1);
            AtX = (XPos + Side) / Buffers.W;
            NewX = (XPos + Side + XVel) / Buffers.W;
            if ( (AtX != NewX) || (Status == Falling) )
            {
               Y1 = (YPos + HSafe) / H - Safe;
               Y2 = (YPos + HSafe + H - 1) / H - Safe;
               NewCh1 = WorldMap[NewX, Y1];
               NewCh2 = WorldMap[NewX, Y2];
               Hold1 = (NewCh1 in CanHoldYou);
               Hold2 = (NewCh2 in CanHoldYou);
               if ( Hold1 || Hold2 )
               {
                  if ( Tp == EnemyType.tpRunningKoopa )
                  {
                     ShowStar (XPos + XVel, YPos);
                     l = (YPos + HSafe + H / 2) / H - Safe;
                     Ch = WorldMap[NewX, l];
                     if ( (XPos >= XView) && (XPos + Buffers.W <= XView + NH * Buffers.W) )
                     {
//                        case Ch of
                        switch (Ch)
                        {
                           case 'J':
                              BreakBlock (NewX, l);
                              break;
                           case '?':
                           {
//                              case WorldMap[NewX, l - 1] of
                              switch (WorldMap[NewX, l - 1])
                              {
                                 case ' ':
                                    HitCoin (NewX * Buffers.W, l * H, true);
                                    break;
                                 case 'à':
                                 {
                                    if ( Data.Mode[Player] == mdSmall )
                                       NewEnemy(tpRisingChamp, 0, NewX, l, 0, -1, 1);
                                    else
                                       NewEnemy(tpRisingFlower, 0, NewX, l, 0, -1, 1);
                                    break;
                                 }
                                 case 'á':
                                    NewEnemy(tpRisingLife, 0, NewX, l, 0, -1, 2);
                                    break;
                              }
                              break;
                           }
                           Remove(NewX * Buffers.W, l * H, Buffers.W, H, 1);
                           WorldMap[NewX, l] = '@';
                        }
                     }
                  }
               }
               XVel = 0;
            }

            AtX = (XPos + XVel) / Buffers.W;
            NewX = (XPos + XVel + Buffers.W - 1) / Buffers.W;
            NewY = (YPos + 1 + H + YVel + HSafe) / H - Safe;

            NewCh1 = WorldMap[AtX, NewY];
            NewCh2 = WorldMap[NewX, NewY];
            Hold1 = (NewCh1 in CanHoldYou + CanStandOn);
            Hold2 = (NewCh2 in CanHoldYou + CanStandOn);

            if ( Tp in [tpLiftStart..tpLiftEnd] )
            {
               if ( (YVel != 0) && (!(Tp == EnemyType.tpDonut))  )
               {
                  if ( YVel < 0 )
                     Hold1 = (YPos + YVel) / H < MapY;
                  if ( Hold1 )
                     YVel = -YVel;
               }
            }
            else
            {
//               case Status of
               switch (Status)
               {
                  case Grounded:
                  {
                     if ( !(Hold1 || Hold2) )
                     {
                        Status = Falling;
                        YVel = 1;
                     }
                     if ( (SubTp = 1) && (Tp == EnemyType.tpKoopa) )
                     {
                        if ( (XVel > 0) && (XPos % Buffers.W in [11..19]) )
                           if ( (!Hold2) && Hold1 )
                              XVel = 0;
                        if (XVel < 0) && (XPos % Buffers.W in [1..9]) )
                           if ( (!Hold1) && Hold2 )
                              XVel = 0;
                     }
                     break;
                  }
                  case Falling:
                  {
                     if Hold1 || Hold2 )
                     {
                        Status = Grounded;
                        YPos = ((YPos + YVel + 1 + HSafe) / H - Safe) * H;
                        if Tp in [tpStar] )
                        {
                           YVel = - (5 * YVel) / 2;
                           Status = Falling;
                        }
                        else
                           YVel = 0;
                     }
                     else
                     {
                        Inc (YVel);
                        if YVel > 4 ) YVel = 4;
                     }
                     break;
                  }
               }
            }
         }

         NewX1 = XPos + XVel;
         NewX2 = NewX1 + Buffers.W - 1 + 4 * (byte)(Tp in [tpVertPlant]);
         Y1 = YPos + YVel;
         Y2 = Y1 + H - 1;

         if (Tp in [tpChibibo, EnemyType.tpFlatChibibo, EnemyType.tpVertFish, EnemyType.tpVertPlant, EnemyType.tpDeadVertPlant, EnemyType.tpRed, EnemyType.tpKoopa..tpRunningKoopa]) )
         {
//            for k = 1 to NumEnemies do
            for (k = 0; k < NumEnemies; k++)
            {
               j = (int)(ActiveEnemies[k]);
               if ( j != i )
               {
                  if ( (EnemyList[j].Tp in [tpChibibo, EnemyType.tpFlatChibibo, EnemyType.tpRed, EnemyType.tpKoopa..tpRunningKoopa]) )
                  {
//                     with EnemyList[j] do
                     X = XPos + XVel;
                     Y = YPos + YVel;
                     
                     if ( NewX1 < X + Buffers.W )
                        if ( (NewX2 > X) )
                           if ( (Y1 < Y + H) )
                              if ( (Y2 > Y) )
                                 if ( EnemyList[j].Tp = EnemyType.tpRunningKoopa )
                                 {
                                    ShowStar (XPos, YPos);
                                    if Tp = EnemyType.tpRunningKoopa )
                                    {
                                       ShowStar (EnemyList[j].XPos, EnemyList[j].YPos);
                                       Kill(j);
                                    }
                                    Kill(i);
                                 }
                                 else
                                 {
                                    if Tp != EnemyType.tpRunningKoopa )
                                    {
                                       XVel = - XVel;
                                       EnemyList[j].XVel = - EnemyList[j].XVel;
                                       YVel = - YVel;
                                       EnemyList[j].YVel = - EnemyList[j].YVel;
                                       if Math.Abs(X - NewX1) < Buffers.W )
                                         if ( X > NewX1 )
                                         {
                                           XPos = XPos - XVel;
                                           XVel = -Math.Abs(XVel);
                                         }
                                         else
                                           if ( X < NewX1 )
                                           {
                                             XPos = XPos - XVel;
                                             XVel = Math.Abs(XVel);
                                           }
                                    }
                                 }
                  }
                  else
                  {
                     if ( (EnemyList[j].Tp == EnemyType.tpFireBall) )
                     {
//                        with EnemyList[j] do
//                        {
                        X = XPos + XVel;
                        Y = YPos + YVel;
//                        }
                        if ( (NewX1 <= X + Buffers.W / 2) )
                        {
                           if ( (NewX2 >= X) )
                              if ( (Y1 <= Y + H / 2) )
                                 if ( Y2 >= Y )
                                 {
                                    EnemyList[j].Tp = EnemyType.tpDyingFireBall;
                                    EnemyList[j].DelayCounter = - (Game.MAX_PAGE + 1);
                                    ShowStar (XPos, YPos);
                                    Kill(i);
                                 }
                        }
                     }
                  }
               }
            }
         }
      }

      public void MoveEnemies()
      {
         int i, j, Page, NewX, OldXVel, OldYVel;

         Page = Game.CurrentPage;
         TimeCounter++;
         
//        for i = 1 to NumEnemies do
         for (int i = 0; i < NumEnemies; i++)
         {
            j = (int)(ActiveEnemies[i]);
//          with EnemyList[j] do
            DelayCounter++;
            NewX = XPos + XVel;
            if ( DelayCounter > MoveDelay )
            {
               XPos = LastXPos;
               YPos = LastYPos;
               DirCounter++;
//               if ( Tp in [tpVertFish, EnemyType.tpVertFireBall, EnemyType.tpVertPlant] )
               if ( Tp == EnemyType.tpVertFish || Tp == EnemyType.tpVertFireBall || Tp == EnemyType.tpVertPlant )
               {
                  if ( Tp == EnemyType.tpVertPlant )
                  {
//                     case Status of
                     switch (Status)
                     {
                        case 0:
                        {
//                           case SubTp of
                           switch (SubTp)
                           {
                              case 0:
                                 if ( (XPos > PlayerX2 + W) || (XPos + 24 + Buffers.W < PlayerX1) )
                                    Status++;
                                 break;
                              case 1:
                                 if ( (XPos > PlayerX2) || (XPos + 24 < PlayerX1) )
                                    Status++;
                                 break;
                              case 2:
                                 Status++;
                                 break;
                           }
                           YVel = 0;
                           DelayCounter = 0;
                           MoveDelay = 1;
                        }
                        case 1:
                        {
                           YVel = -1;
                           DelayCounter = 0;
                           MoveDelay = 2;
                           if ( YPos + YVel <= (MapY * H - 19) )
                           {
                              YVel = 0;
                              DelayCounter = 0;
                              MoveDelay = 2;
                              Counter = 0;
                              Status++;
                           }
                           break;
                        }
                        case 2:
                        {
                           Counter++;
                           if ( Counter > 200 )
                              Status++;
                           MoveDelay = 0;
                           DelayCounter = 0;
                           break;
                        }
                        case 3:
                        {
                           YVel = 1;
                           DelayCounter = 0;
                           MoveDelay = 2;
                           if ( YPos > (MapY * H) )
                              Status++;
                           break;
                        }
                        case 4:
                        {
                           YVel = 0;
                           MoveDelay = 100 + Random (100);
                           DelayCounter = 0;
                           Status = 0;
                           break;
                        }
                     }
                  }
                  else
                  {
                     if ( YPos + H >= NV * H )
                     {
                        if ( YVel > 0 )
                        {
                           YVel = 0;
                           MoveDelay = 100 + Random (300);
                           DelayCounter = 0;
                        }
                     }
                     else
                     {
                        YVel = -10;
                        MoveDelay = 1;
                        DelayCounter = 0;
                        if ( Tp == EnemyType.tpVertFireBall )
                        {
                           Beep (100);
                           YVel = -9;
                        }
                     }
                  }
               }
               if ( Tp == EnemyType.tpSleepingKoopa )
               {
                  Counter++;
                  if ( Counter > 150 )
                  {
                     Tp = EnemyType.tpWakingKoopa;
                     XVel = 1;
                     Counter = 0;
                  }
               }
               if ( Tp == EnemyType.tpWakingKoopa )
               {
                  XVel = - XVel;
                  MoveDelay = 1;
                  DelayCounter = 0;
                  Inc (Counter);
                  if ( Counter > 50 )
                  {
                     Tp = EnemyType.tpKoopa;
                     if ( PlayerX1 > XPos )
                        XVel = 1;
                     else
                        XVel = -1;
                  }
               }
//               if ( Tp in [tpDying, EnemyType.tpDyingFireBall, EnemyType.tpDyingKoopa] )
               if ( Tp == EnemyType.tpDying || Tp == EnemyType.tpDyingFireBall || Tp == EnemyType.tpDyingKoopa )
                  Tp = EnemyType.tpDead;
               else
               {
                  if ( (Tp == EnemyType.tpFlatChibibo) || (NewX <= -W) || (NewX < XView - ForgetEnemiesAt * W) ||
                           (NewX > XView + NH * Buffers.W + ForgetEnemiesAt * W) || (YPos + YVel > NV * H) )
                  {
//                     case Tp of
                     switch (Tp)
                     {
                        case EnemyType.tpChibibo:
                           WorldMap[MapX, MapY] = '';
                           break;
                        case EnemyType.tpVertFish:
                           WorldMap[MapX, MapY - 2] = '';
                           break;
                        case EnemyType.tpVertFireBall:
                           WorldMap[MapX, MapY - 2] = '';
                           break;
                        case EnemyType.tpVertPlant:
                           WorldMap[MapX, MapY - 2] = (char) ((int)('') + SubTp);
                           break;
                        case EnemyType.tpRed:
                           WorldMap[MapX, MapY] = '';
                           break;
                        case EnemyType.tpKoopa..tpRunningKoopa:
                           WorldMap[MapX, MapY] = (char) ((int)('') + SubTp);
                           break;
                        case EnemyType.tpBlockLift:
                           WorldMap[MapX, MapY] = '°';
                           break;
                        case EnemyType.tpDonut:
                           WorldMap[MapX, MapY] = '±';
                           break;
                     }
              
                     if ( Tp == EnemyType.tpKoopa )
                        Tp = EnemyType.tpDyingKoopa;
                     else
                     {
                        if ( Tp != EnemyType.tpFireBall )
                           Tp = EnemyType.tpDying;
                        else
                           Tp = EnemyType.tpDyingFireBall;
                     }
                     DelayCounter = -(Game.MAX_PAGE + 1);
                  }
                  else
                  {
                     DelayCounter = 0;
                     OldXVel = XVel;
                     //{ OldYVel = YVel; }
//                     if ( Tp in [tpVertFish, EnemyType.tpDeadVertFish, EnemyType.tpVertFireBall, EnemyType.tpDeadVertPlant] )
                     if ( Tp == EnemyType.tpVertFish || Tp == EnemyType.tpDeadVertFish || Tp == EnemyType.tpVertFireBall || Tp == EnemyType.tpDeadVertPlant )
                     {
                        if ( (DirCounter % 3 == 0) && (YPos + H < NV * H) )
                          Inc (YVel);
                     }
//                     if Tp in [tpDeadChibibo, EnemyType.tpDeadRed, EnemyType.tpDeadKoopa] )
                     if ( Tp == EnemyType.tpDeadChibibo || Tp == EnemyType.tpDeadRed || Tp == EnemyType.tpDeadKoopa )
                     {
                        if ( XPos % 6 = 0 )
                          Inc (YVel);
                     }
                     else
                        Check(j);
                     XPos = XPos + XVel;
                     YPos = YPos + YVel;
                     if ( XVel == 0 )
                     {
                        XVel = -OldXVel;
                        if ( Tp == EnemyType.tpDyingFireBall )
                          ShowFire(XPos, YPos);
                     }
//                     { if YVel = 0 ) YVel = - OldYVel; }
                  }
                  LastXPos = XPos;
                  LastYPos = YPos;
               }
            }
            else
            {
               if ( (XVel != 0) || (YVel != 0) )
               {
                  XPos = LastXPos + (DelayCounter * XVel) / (MoveDelay + 1);
                  YPos = LastYPos + (DelayCounter * YVel) / (MoveDelay + 1);
               }
            }
         }

//        for i = 1 to NumEnemies do
         for (int i = 0; i < NumEnemies; i++)
         {
            j = (int)(ActiveEnemies[i]);
//            with EnemyList[j] do
            if ( tp in [tpChibibo, EnemyType.tpChamp, EnemyType.tpLife, EnemyType.tpFlower, EnemyType.tpStar, EnemyType.tpVertFish, EnemyType.tpVertFireBall, EnemyType.tpVertPlant, EnemyType.tpRed, EnemyType.tpKoopa..tpRunningKoopa,
                        EnemyType.tpLiftStart..tpLiftEnd] )
            {
               if ( PlayerX1 < XPos + Buffers.W )
               {
                  if ( PlayerX2 > XPos )
                  {
                     if ( PlayerY1 + PlayerYVel < YPos + H )
                     {
                        if ( PlayerY2 + PlayerYVel > YPos )
                        {
                           if ( Star )
                           {
                              if ( !(Tp in [tpLiftStart..tpLiftEnd]) )
                              {
                                 Beep (800);
                                 Kill(j);
                                 cdHit = 1;
                              }
                           }
//                           case Tp of
                           switch (Tp)
                           {
                              case EnemyType.tpSleepingKoopa:
                              case EnemyType.tpWakingKoopa:
                              {
                                 Tp = EnemyType.tpRunningKoopa;
                                 XVel = 5 * (2 * (byte)(XPos > PlayerX1) - 1);
                                 MoveDelay = 0;
                                 DelayCounter = 0;
                                 Beep (800);
                                 cdEnemy = 1;
                                 Buffers.AddScore(100);
                                 break;
                              }
                              case EnemyType.tpChamp:
                              {
                                 if ( SubTp == 0 )
                                 {
                                    cdChamp = 0x1;
                                    Buffers.AddScore(1000);
                                 }
                                 else
                                 {
                                    cdHit = 1;
                                 }
                                 Tp = EnemyType.tpDying;
                                 DelayCounter = -(Game.MAX_PAGE + 1);
                                 CoinGlitter(XPos, YPos);
                                 break;
                              }
                              case EnemyType.tpLife:
                              {
                                 cdLife = 0x1;
                                 Tp = EnemyType.tpDying;
                                 DelayCounter = - (Game.MAX_PAGE + 1);
                                 CoinGlitter(XPos, YPos);
                                 Buffers.AddScore(1000);
                                 break;
                              }
                              case EnemyType.tpFlower:
                              {
                                 cdFlower = 0x1;
                                 Tp = EnemyType.tpDying;
                                 DelayCounter = - (Game.MAX_PAGE + 1);
                                 CoinGlitter(XPos, YPos);
                                 Buffers.AddScore(1000);
                                 break;
                              }
                              case EnemyType.tpStar:
                              {
                                 cdStar = 0x1;
                                 Tp = EnemyType.tpDying;
                                 DelayCounter = - (Game.MAX_PAGE + 1);
                                 CoinGlitter(XPos, YPos);
                                 Buffers.AddScore(1000);
                                 break;
                              }
                              case EnemyType.tpVertFireBall:
                              {
                                 cdHit = 1;
                                 break;
                              }

                              default:
                              {
                                 if ( ((PlayerYVel > YVel) || (PlayerYVel > 0)) && (PlayerY2 <= YPos + H) )
                                 {
   //                                 case Tp of
                                    switch (Tp)
                                    {
                                       case EnemyType.tpChibibo:
                                       {
                                          Tp = EnemyType.tpFlatChibibo;
                                          XVel = 0;
                                          DelayCounter = - 2 - 15 * (byte)(YVel = 0);
                                          Beep (800);
                                          cdEnemy = 1;
                                          Buffers.AddScore(100);
                                          break;
                                       }
                                       case EnemyType.tpVertFish:
                                       {
                                          if ( YPos + H < NV * H )
                                          {
                                             Kill(j);
                                             Beep (800);
                                             cdEnemy = 1;
                                          }
                                          break;
                                       }
                                       case EnemyType.tpKoopa:
                                       case EnemyType.tpRunningKoopa:
                                       {
                                           Tp = EnemyType.tpSleepingKoopa;
                                           XVel = 0;
                                           Counter = 0;
                                           Beep (800);
                                           cdEnemy = 1;
                                           Buffers.AddScore(100);
                                           break;
                                       }
                                       case EnemyType.tpLiftStart:// TODO EnemyType.tpLiftStart..tpLiftEnd:
                                       {
                                          if Tp = EnemyType.tpDonut )
                                          {
                                             Status = 2;
                                             if ( (Counter > 20) && (YVel = 0) )
                                               Inc (YVel);
                                          }
                                          cdStopJump = (byte)(PlayerYVel != 2);
                                          cdLift = 1;
                                          PlayerY1 = YPos - 2 * H;
                                          PlayerY2 = YPos - 1;
                                          PlayerXVel = XVel;
                                          if ( MoveDelay != 0 )
                                             PlayerXVel = XVel * XPos % 2;
                                          PlayerYVel = YVel;
                                          break;
                                       }
                                    }
                                 }
                                 else
                                 {

                                    if (!((Tp == EnemyType.tpVertFish) && (!(Math.Abs(DelayCounter - MoveDelay) <= 1)) ||
                                              (Tp in [tpLiftStart..tpLiftEnd])))
                                    {
                                       cdHit = 1;
                                       if ( Star )
                                          Kill(j);
                                    }
                                 }
                              }
                           }
                        }
                     }
                  }
               }
            }
         } // end for
         
         int i = 1;
         while ( i <= ActiveEnemies.Length )
         {
            if ( EnemyList[(int)(ActiveEnemies[i])].Tp == EnemyType.tpDead )
               Delete(ActiveEnemies, i, 1);
            else
               i++;
         }
        
         NumEnemies = Length (ActiveEnemies);
      }

      public void StartEnemies (int X, short Dir)
      {
         int i;
         bool Remove;

         if ( (X < 0) || (X > Options.XSize) )
            return;
            
         for ( i = 0; i < NV; i++)
         {
            Remove = true;
            switch (WorldMap[X, i])
            {
               case '': NewEnemy(tpChibibo, 0, X, i, 1 * Dir, 0, 2); break;
               case '': NewEnemy(tpVertFish, 0, X, (i + 2), 0, 0, 50 + Random (100)); break;
               case '': NewEnemy(tpVertFireBall, 0, X, (i + 2), 0, 0, 50 + Random (100)); break;
               case '': NewEnemy(tpChibibo, 1, X, i, 1 * Dir, 0, 2);
               case '':
//               case ' ':
               case '': NewEnemy(tpVertPlant, (int)(WorldMap[X, i]) - (int)(''), X, (i + 2), 0, 0, 20 + Random (50)); break;
               case '': NewEnemy(tpRed, 0, X, i, 1 * Dir, 0, 2); break;
               case '':
               case '':
               case '': NewEnemy(tpKoopa, (int)(WorldMap[X, i]) - (int)(''), X, i, Dir, 0, 2); break;
               case '°': 
                  if ( (WorldMap[X - 1, i] in CanHoldYou) || (WorldMap[X + 1, i] in CanHoldYou) )
                     NewEnemy(tpBlockLift, 0, X, i, -Dir, 0, 0);
                  else
                     NewEnemy(tpBlockLift, 0, X, i, 0, -Dir, 0);
                   break;
               case '±': NewEnemy(tpDonut, 0, X, i, 0, 0, 0); break;
               default:
                  Remove = FALSE;
                  break;
            }
            if (Remove)
               WorldMap[X, i] = ' ';
         }
      }
      
      public void HitAbove (int MapX, int MapY)
      {
         int i, j, X, Y;
         
         Y = MapY * H;
         X = MapX * Buffers.W;
         
         for ( i = 0; i < NumEnemies; i++)
         {
            j = (int)(ActiveEnemies[i]);
            
            if (EnemyList[j].YPos == Y)
            {
               if ( (EnemyList[j].XPos + EnemyList[j].XVel + Buffers.W > X) && (EnemyList[j].XPos + EnemyList[j].XVel < X + W) )
               {
                  switch (EnemyList[j].Tp)
                  {
                     case EnemyType.tpChamp:
                     case EnemyType.tpLife:
                     case EnemyType.tpFlower:
                     case EnemyType.tpStar:
                     case EnemyType.tpKoopa:
                     case EnemyType.tpSleepingKoopa:
                     case EnemyType.tpWakingKoopa:
                     case EnemyType.tpRunningKoopa:
                     {
                        if ( ((EnemyList[j].XVel > 0) && (EnemyList[j].XPos + EnemyList[j].XVel + Buffers.W / 2 <= X)) ||
                              ((EnemyList[j].XVel < 0) && (EnemyList[j].XPos + EnemyList[j].XVel + Buffers.W / 2 >= X)) )
                        {
                           EnemyList[j].XVel *= -1;
                           EnemyList[j].YVel = -7;
                           EnemyList[j].Status = Falling;
                           //if ( Tp in [tpKoopa..tpWakingKoopa] )
                           if ( EnemyList[j].Tp == EnemyType.tpKoopa || EnemyList[j].Tp == EnemyType.tpSleepingKoopa ||
                                 EnemyList[j].Tp == EnemyType.tpWakingKoopa )
                           {
                              EnemyList[j].Tp = EnemyType.tpSleepingKoopa;
                              EnemyList[j].XVel = 0;
                           }
                        }
                        break;
                     }
                     case EnemyType.tpChibibo:
                     case EnemyType.tpRed:
                     {
                        Kill(j);
                        break;
                     }
                  }
               }
            }
         }
      }



   } // class Enemies

} // namespace MarioPort
