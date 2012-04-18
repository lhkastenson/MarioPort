
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
   public enum EnemyType
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

   public static class Enemies
   {
      public const int StartEnemiesAt  = 2;
      public const int ForgetEnemiesAt = 5;

      public const int Left = 0;
      public const int Right = 1;
         
      public const int kGreen = 0;
      public const int kRed = 1;
      
      public static bool Turbo = false;
      
      public static byte cdChamp;
      public static byte cdLife;
      public static byte cdFlower;
      public static byte cdStar;
      public static byte cdEnemy;
      public static byte cdHit;
      public static byte cdLift;
      public static byte cdStopJump;
      
      public static int PlayerX1;
      public static int PlayerY1;
      public static int PlayerX2;
      public static int PlayerY2;
      public static int PlayerXVel;
      public static int PlayerYVel;
      
      public static bool conStar;
      
      public static void init()
      {
         // init FireballList && KoopaList
      }
      
      //// implementation ////

      private static byte[,] rKoopa = new byte[4, 20 * 24 + 1];

      private static Bitmap[] FireBallList = new Bitmap[4];
      private static Bitmap[, ,] KoopaList = new Bitmap[Right - Left + 1, kRed - kGreen + 1, 2];

      private const int Grounded = 0;
      private const int Falling = 1;
      private const int MaxEnemies = 11;
      private const int MaxEnemiesAtOnce = 25;

      //int i = MarioPort.Buffers.W;
      //int j = Buffers.W;
/*
      public struct EnemyRec
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
         public uint[] BackGrAddr;

         public EnemyRec(bool go)
         {
            BackGrAddr = new uint[Game.MAX_PAGE + 1];
         }
      }

//          EnemyListPtr = EnemyList;
      private static EnemyRec[] EnemyList = new EnemyRec[MaxEnemiesAtOnce];

//      Enemy: EnemyListPtr;
//      private EnemyRec* Enemy = &EnemyList;
      
//      ActiveEnemies: String [MaxEnemiesAtOnce];
      private static string ActiveEnemies;
//      NumEnemies: (byte)absolute ActiveEnemies;
      private static byte NumEnemies;
      // TODO
//      TimeCounter: Byte;
      private static byte TimeCounter;
      **/
      private static Image[,] EnemyPictures = new Image[MaxEnemies, Right - Left + 1];
      /**
      private static void Kill(int i)
      {
         EnemyRec enemyRec = EnemyList[i];

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
      
      private static void ShowStar (int X, int Y)
      {
//         Beep (100);
         if ( (X + Buffers.W > XView) && (X < XView + SCREEN_WIDTH) )
            NewTempObj(tpHit, X, Y, 0, 0, Buffers.W, Buffers.H);
      }

      private static void ShowFire (int X, int Y)
      {
         //Beep (50);
         X = X - 4;
         Y = Y - 4;
         if ( (X + Buffers.W > XView) && (X < XView + SCREEN_WIDTH) )
            NewTempObj(tpFire, X, Y, 0, 0, Buffers.W, Buffers.H);
      }

      //private static void Mirror20x24 (P1, P2: Pointer)
      //{
    
      //  const
      //    Buffers.W = 20;
      //    H = 24;
      //  type
      //    PlaneBuffer = array[0..H - 1, 0..W / 4 - 1] of Byte;
      //    PlaneBufferArray = array[0..3] of PlaneBuffer;
      //    PlaneBufferArrayPtr = PlaneBufferArray;
      //  var
      //    Source, Dest: PlaneBufferArrayPtr;
      //}
      **/
      private static void Mirror20x24(ref Bitmap from, ref Bitmap to)
      {
         
      }
      /**
      //private static void Swap (byte Plane1, byte Plane2)
      //{
      //       var
      //         i, j: Byte;
      //     {
      //       for j = 0 to H - 1 do
      //         for i = 0 to Buffers.W / 4 - 1 do
      //         {
      //           Dest[Plane2, j, i] = Source[Plane1, j, Buffers.W / 4 - 1 - i];
      //           Dest[Plane1, j, i] = Source[Plane2, j, Buffers.W / 4 - 1 - i];
      //         }
      //     Source = P1;
      //     Dest = P2;
      //     Swap (0, 3);
      //     Swap (1, 2);
      //   }
      //}

      **/
      public static void InitEnemyFigures()
      {
         EnemyPictures[0, Right] = Resources.CHIBIBO_000;
         EnemyPictures[1, Right] = Resources.CHIBIBO_001;

         EnemyPictures[2, Right] = Resources.CHIBIBO_002;
         EnemyPictures[3, Right] = Resources.CHIBIBO_003;

         EnemyPictures[4, Left] = Resources.FISH_001;
         //Figures.Mirror(ref EnemyPictures[3, Left], ref EnemyPictures[3, Right]);

         EnemyPictures[5, Left] = Resources.RED_000;
         EnemyPictures[6, Left] = Resources.RED_001;

         EnemyPictures[7, Right] = Resources.GRKP_000;
         EnemyPictures[8, Right] = Resources.GRKP_001;

         EnemyPictures[9, Right] = Resources.RDKP_000;
         EnemyPictures[10, Right] = Resources.RDKP_001;

         for( int i = 0; i < MaxEnemies; i++ )
         {
            if ( i == 6 || i == 7 )
               ;//Figures.Mirror(ref EnemyPictures[i, Left], ref EnemyPictures[i, Right]);
            else
               if ( i != 3 )
                  ;// Figures.Mirror(ref EnemyPictures[i, Left], ref EnemyPictures[i, Right]);
         }
         
         for( int i = 0; i <= 1; i++ )
         {
            for( int j = kGreen; j <= kRed; j++ )
            {
               Mirror20x24(ref KoopaList[Left, j, i], ref KoopaList[Right, j, i]);
            }
         }
      }

      public static void ClearEnemies()
      {/**
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
      **/}/**

      public static void StopEnemies()
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

      public static void NewEnemy(EnemyType InitType, int SubType, int InitX, int InitY, int InitXVel, int InitYVel, int InitDelay)
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

      public static void ShowEnemies()
      {
//      var
//        i, j, Page: Integer;
//        Fig: Pointer;

         int i, j, Page;
         Bitmap Fig;

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
            switch (EnemyList[i].Tp)
            {
               case EnemyType.tpChibibo:
                  DrawImage(EnemyList[i].XPos, EnemyList[i].YPos, Buffers.W, H, EnemyPictures[1 + 3 * EnemyList[i].SubTp, (byte)(DirCounter % 32 < 16)]);
                  break;
               case EnemyType.tpFlatChibibo:
                  DrawImage(EnemyList[i].XPos, EnemyList[i].YPos, Buffers.W, H, EnemyPictures[2 + 3 * EnemyList[i].SubTp, (byte)(DirCounter % 32 < 16)]);
                  break;
               case EnemyType.tpDeadChibibo:
                  UpSideDown(EnemyList[i].XPos, EnemyList[i].YPos, Buffers.W, H, EnemyPictures[1, Left]);
                  break;
               case EnemyType.tpRisingChamp:
                  if (EnemyList[i].YPos != (EnemyList[i].MapY * H))
                     if (EnemyList[i].SubTp == 0)
                        DrawPart(EnemyList[i].XPos, EnemyList[i].YPos, Buffers.W, H, 0, H - EnemyList[i].YPos % H - 1, Resources.CHAMP_000);
                     else
                        DrawPart(EnemyList[i].XPos, EnemyList[i].YPos, Buffers.W, H, 0, H - EnemyList[i].YPos % H - 1, Resources.POISON_000);
                  break;
               case EnemyType.tpChamp:
                  if (EnemyList[i].SubTp == 0)
                     DrawImage(EnemyList[i].XPos, EnemyList[i].YPos, Buffers.W, H, Resources.CHAMP_000);
                  else
                     DrawImage(EnemyList[i].XPos, EnemyList[i].YPos, Buffers.W, H, Resources.POISON_000);
                  break;
               case EnemyType.tpRisingLife:
                  if (EnemyList[i].YPos != (EnemyList[i].MapY * H))
                     DrawPart(EnemyList[i].XPos, EnemyList[i].YPos, Buffers.W, H, 0, H - EnemyList[i].YPos % H - 1, Resources.LIFE_000);
                  break;
               case EnemyType.tpLife:
                  DrawImage(EnemyList[i].XPos, EnemyList[i].YPos, Buffers.W, H, Resources.LIFE_000);
                	break;
               case EnemyType.tpRisingFlower:
                  if (EnemyList[i].YPos != (MapY * H))
                     DrawPart(EnemyList[i].XPos, EnemyList[i].YPos, Buffers.W, H, 0, H - EnemyList[i].YPos % H - 1, Resources.FLOWER_000);
                  break;
               case EnemyType.tpFlower:
                	DrawImage(EnemyList[i].XPos, EnemyList[i].YPos, Buffers.W, H, Resources.FLOWER_000);
                	break;
               case EnemyType.tpRisingStar:
                  if ( EnemyList[i].YPos != (MapY * H) )
                     DrawPart(EnemyList[i].XPos, EnemyList[i].YPos, Buffers.W, H, 0, H - EnemyList[i].YPos % H - 1, Resources.STAR_000);
                  break;
               case EnemyType.tpStar:
                	DrawImage(EnemyList[i].XPos, EnemyList[i].YPos, Buffers.W, H, Resources.STAR_000);
                	break;
               case EnemyType.tpFireBall:
                  if ( EnemyList[i].XPos % 4 < 2 )
                     DrawImage(EnemyList[i].XPos, EnemyList[i].YPos, 12, H / 2, Resources.FIRE_000);
                  else
                     DrawImage(EnemyList[i].XPos, EnemyList[i].YPos, 12, H / 2, Resources.FIRE_001);
                  break;
               case EnemyType.tpVertFish:
                  if ( (YVel != 0) || (YPos < NV * H - H) )
                     DrawImage(EnemyList[i].XPos, EnemyList[i].YPos, Buffers.W, H, EnemyPictures[3, (byte)(PlayerX1 > EnemyList[i].XPos)]);
                  break;
               case EnemyType.tpDeadVertFish:
                  if ( (YPos < NV * H - H) || (YVel != 0) )
                     UpSideDown(EnemyList[i].XPos, EnemyList[i].YPos, Buffers.W, H, EnemyPictures[3, (byte)(PlayerX1 <= EnemyList[i].XPos)]);
                  break;
               case EnemyType.tpVertFireBall:
               {
                  if ( Math.Abs(DelayCounter - MoveDelay) <= 1 )
                  {
                     DrawImage(EnemyList[i].XPos, EnemyList[i].YPos, Buffers.W, H, FireBallList [Random (4)]);
                     NewGlitter(EnemyList[i].XPos + Random (W), EnemyList[i].YPos + Random (H), 57 + Random (7), 14 + Random (20));
                     NewStar(EnemyList[i].XPos + Random (W), EnemyList[i].YPos + Random (H), 57 + Random (7), 14 + Random (20));
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
                           Fig = PPlant002;
                           break;
                        default:
                           Fig = PPlant000;
                     }
                  }
                  else
                  {
                     //case SubTp of
                     switch (SubTp)
                     {
                        case 0:
                        case 1:
                           Fig = PPlant003;
                           break;
                        default:
                           Fig = PPlant001;
                     }
                  }
                  DrawPart(EnemyList[i].XPos, EnemyList[i].YPos, 24, 20, 0, (MapY * H) - EnemyList[i].YPos - 1, Fig);
                  break;
               }
               case EnemyType.tpDeadVertPlant:
               {
                  DelayCounter = 0;
                  MoveDelay = 0;
                  YVel = 0;
                  Status++;
                  if (Status < 12)
                     DrawImage(EnemyList[i].XPos, EnemyList[i].YPos, 24, 20, Hit000);
                  else
                     if (Status > 14)
                        Tp = EnemyType.tpDying;
                  break;
               }
               case EnemyType.tpRed:
                  DrawImage(EnemyList[i].XPos, EnemyList[i].YPos, Buffers.W, H, EnemyPictures[6 + (byte)(DirCounter % 16 <= 8), (byte)(XVel > 0)]);
                  break;
               case EnemyType.tpDeadRed:
                  UpSideDown(EnemyList[i].XPos, EnemyList[i].YPos, Buffers.W, H, EnemyPictures[6 + (byte)(DirCounter % 16 <= 8), (byte)(XVel > 0)]);
                  break;
               case EnemyType.tpKoopa:
                  DrawImage(EnemyList[i].XPos, EnemyList[i].YPos - 10, Buffers.W, 24, KoopaList [(byte)(XVel > 0), SubTp, (byte)(DirCounter % 16 <= 8)]);
                  break;
               case EnemyType.tpWakingKoopa:
               case EnemyType.tpRunningKoopa:
                  DrawImage(EnemyList[i].XPos, EnemyList[i].YPos, Buffers.W, H, EnemyPictures[8 + 2 * SubTp + 1 - (byte)(DirCounter % 16 <= 8), (byte)(DirCounter % 32 <= 16)]);
                  break;
               case EnemyType.tpSleepingKoopa:
                  DrawImage(EnemyList[i].XPos, EnemyList[i].YPos, Buffers.W, H,  EnemyPictures[8 + 2 * SubTp, 0]);
                  break;
               case EnemyType.tpDeadKoopa:
                  UpSideDown(EnemyList[i].XPos, EnemyList[i].YPos, Buffers.W, H, EnemyPictures[8 + 2 * SubTp, (byte)(DirCounter % 16 <= 8)]);
                  break;
               case EnemyType.tpBlockLift:
                  DrawImage(EnemyList[i].XPos, EnemyList[i].YPos, Buffers.W, H, Lift1000);
                  break;
               case EnemyType.tpDonut:
               {
                  if ( Status == 0 )
                  {
                     DrawImage(EnemyList[i].XPos, EnemyList[i].YPos, Buffers.W, H, Resources.DONUT_000);
                     if ( YVel == 0 )
                        Counter = 0;
                  }
                  else
                  {
                     DrawImage(EnemyList[i].XPos, EnemyList[i].YPos, Buffers.W, H, Resources.DONUT_001);
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
      
      public static void HideEnemies()
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

      private static void Check (int i)
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
         switch (EnemyList[i].Tp)
         {
            case EnemyType.tpRisingChamp:
            case EnemyType.tpRisingLife:
            case EnemyType.tpRisingFlower:
            case EnemyType.tpRisingStar:
            {
               if ( ((EnemyList[i].YPos / H) == (EnemyList[i].YPos / H)) && (EnemyList[i].YPos != EnemyList[i].MapY * H) )
               {
                  EnemyList[i].XVel = 1 - 2 * (byte)( Buffers.CanHoldYou( WorldMap[EnemyList[i].MapX + 1, EnemyList[i].MapY - 1] ) );
//                  case Tp of
                  switch (EnemyList[i].Tp)
                  {
                     case EnemyType.tpRisingChamp:
                        EnemyList[i].Tp = EnemyType.tpChamp;
                        break;
                     case EnemyType.tpRisingLife:
                     {
                        EnemyList[i].Tp = EnemyType.tpLife;
                        EnemyList[i].XVel = 2 * XEnemyList[i].Vel;
                        break;
                     }
                     case EnemyType.tpRisingFlower:
                     {
                        EnemyList[i].XVel = 0;
                        EnemyList[i].Tp = EnemyType.tpFlower;
                        break;
                     }
                     case EnemyType.tpRisingStar:
                     {
                        EnemyList[i].Tp = EnemyType.tpStar;
                        EnemyList[i].XVel = 2 * EnemyList[i].XVel;
                        break;
                     }
                  }
                  EnemyList[i].YVel = -7;
                  EnemyList[i].MoveDelay = 1;
                  EnemyList[i].Status = Falling;
               }
               else
               {
                  j = (EnemyList[i].YPos % H);
                  if ( j % 2 == 0 )
                     Beep (130 - 20 * j);
//                  Exit;
                  return;
               }
               break;
            }
            case EnemyType.tpFireBall:
            {
               AtX = (EnemyList[i].XPos + Buffers.W / 4) / Buffers.W;
               NewX = (EnemyList[i].XPos + Buffers.W / 4 + EnemyList[i].XVel) / Buffers.W;
               if ( (AtX != NewX) || (PlayerX1 % Buffers.W == 0) )
               {
                  Y1 = (EnemyList[i].YPos + H / 4 + HSafe) / H - Safe;
                  NewCh1 = WorldMap[NewX, Y1];
                  if ( Buffers.CanHoldYou( NewCh1 ) )
                     EnemyList[i].XVel = 0;
               }
               NewX = EnemyList[i].XPos;
               NewY = EnemyList[i].YPos;
               AtX = (EnemyList[i].XPos + Buffers.W / 4 + EnemyList[i].XVel) / Buffers.W;
               NewY = (EnemyList[i].YPos + 2 + H / 4 + EnemyList[i].YVel + HSafe) / H - Safe;
               NewCh1 = WorldMap[AtX, NewY];
               if ( (EnemyList[i].YVel > 0) && ( Buffers.CanHoldYou(NewCh1) || Buffers.CanStandOn(NewCh1) ) )
               {
                  EnemyList[i].YPos = ((EnemyList[i].YPos + EnemyList[i].YVel - 5 + HSafe) / H - Safe) * H;
                  EnemyList[i].YVel = -2;
               }
               else
                  if ( EnemyList[i].XPos % 3 == 0 )
                     EnemyList[i].YVel++;
               if ( (EnemyList[i].XVel == 0) || (NewX < XView - Buffers.W) || (NewX > XView + NH * Buffers.W + Buffers.W) || (NewY > NV * H) )
               {
                  EnemyList[i].DelayCounter = - (Game.MAX_PAGE + 1);
                  EnemyList[i].Tp = EnemyType.tpDyingFireBall;
               }
//                Exit;
               return;
            }
            case EnemyType.tpStar:
            StartGlitter(EnemyList[i].XPos, EnemyList[i].YPos, Buffers.W, H);
               break;
         }


         if (!(EnemyList[i].Tp == EnemyType.tpVertFish || EnemyList[i].Tp == EnemyType.tpDeadVertFish 
                  || EnemyList[i].Tp == EnemyType.tpVertFireBall || EnemyList[i].Tp == EnemyType.tpVertPlant 
                  || EnemyList[i].Tp == EnemyType.tpDeadVertPlant))
         {
            //Side = Integer(EnemyList[i].XVel > 0) * (W - 1);
            Side = (EnemyList[i].XVel > 0) ? 1 : 0;
            Side *= W - 1;

            AtX = (EnemyList[i].XPos + Side) / Buffers.W;
            NewX = (EnemyList[i].XPos + Side + XVel) / Buffers.W;
            if ( (AtX != NewX) || (Status == Falling) )
            {
               Y1 = (YPos + HSafe) / H - Safe;
               Y2 = (YPos + HSafe + H - 1) / H - Safe;
               NewCh1 = WorldMap[NewX, Y1];
               NewCh2 = WorldMap[NewX, Y2];
               Hold1 = (Buffers.CanHoldYou(NewCh1));
               Hold2 = (Buffers.CanHoldYou(NewCh2));
               if ( Hold1 || Hold2 )
               {
                  if ( Tp == EnemyType.tpRunningKoopa )
                  {
                     ShowStar (EnemyList[i].XPos + XVel, EnemyList[i].YPos);
                     l = (YPos + HSafe + H / 2) / H - Safe;
                     Ch = WorldMap[NewX, l];
                     if ( (EnemyList[i].XPos >= XView) && (EnemyList[i].XPos + Buffers.W <= XView + NH * Buffers.W) )
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

            AtX = (EnemyList[i].XPos + XVel) / Buffers.W;
            NewX = (EnemyList[i].XPos + XVel + Buffers.W - 1) / Buffers.W;
            NewY = (YPos + 1 + H + YVel + HSafe) / H - Safe;

            NewCh1 = WorldMap[AtX, NewY];
            NewCh2 = WorldMap[NewX, NewY];
            Hold1 = (Buffers.CanHoldYou(NewCh1) || Buffers.CanStandOn(NewCh1));
            Hold2 = (Buffers.CanHoldYou(NewCh2) || Buffers.CanStandOn(NewCh2));

            if ( Tp == EnemyType.tpLiftStart || Tp == EnemyType.BlockLift || Tp == EnemyType.tpDonut || Tp == EnemyType.tpLiftEnd )
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
                        if ( (XVel > 0) && (EnemyList[i].XPos % Buffers.W >= 11 && EnemyList[i].XPos % Buffers.W <= 19) )
                           if ( (!Hold2) && Hold1 )
                              XVel = 0;
                        if ( (XVel < 0) && (EnemyList[i].XPos % Buffers.W >= 1 && EnemyList[i].XPos % Buffers.W <= 9) )
                           if ( (!Hold1) && Hold2 )
                              XVel = 0;
                     }
                     break;
                  }
                  case Falling:
                  {
                     if ( Hold1 || Hold2 )
                     {
                        Status = Grounded;
                        EnemyList[i].YPos = ((YPos + YVel + 1 + HSafe) / H - Safe) * H;
                        if ( Tp == EnemyType.tpStar )
                        {
                           YVel = - (5 * YVel) / 2;
                           Status = Falling;
                        }
                        else
                           YVel = 0;
                     }
                     else
                     {
                        YVel++;
                        if ( YVel > 4 ) YVel = 4;
                     }
                     break;
                  }
               }
            }
         }

         NewX1 = EnemyList[i].XPos + XVel;
         NewX2 = NewX1 + Buffers.W - 1 + 4 * (byte)(Tp == EnemyType.tpVertPlant);
         Y1 = EnemyList[i].YPos + YVel;
         Y2 = Y1 + H - 1;

         if ( EnemyList[j].Tp == EnemyType.tpChibibo || EnemyList[j].Tp == EnemyType.tpFlatChibibo || EnemyList[j].Tp == EnemyType.tpVertFish 
               || EnemyList[j].Tp == EnemyType.tpVertPlant || EnemyList[j].Tp == EnemyType.tpDeadVertPlant || EnemyList[j].Tp == EnemyType.tpRed
               || EnemyList[j].Tp == EnemyType.tpKoopa || 
                     EnemyList[j].Tp == EnemyType.tpSleepingKoopa ||
                     EnemyList[j].Tp == EnemyType.tpWakingKoopa ||
                     EnemyList[j].Tp == EnemyType.tpRunningKoopa )
         {
//            for k = 1 to NumEnemies do
            for (k = 0; k < NumEnemies; k++)
            {
               j = (int)(ActiveEnemies[k]);
               if ( j != i )
               {
                  if ( (EnemyList[j].Tp == EnemyType.tpChibibo || EnemyList[j].Tp == EnemyType.tpFlatChibibo || EnemyList[j].Tp == EnemyType.tpRed
                           || EnemyList[j].Tp == EnemyType.tpKoopa ||
                     EnemyList[j].Tp == EnemyType.tpSleepingKoopa ||
                     EnemyList[j].Tp == EnemyType.tpWakingKoopa ||
                     EnemyList[j].Tp == EnemyType.tpRunningKoopa))
                  {
//                     with EnemyList[j] do
                     X = EnemyList[i].XPos + XVel;
                     Y = EnemyList[i].YPos + YVel;
                     
                     if ( NewX1 < X + Buffers.W )
                        if ( (NewX2 > X) )
                           if ( (Y1 < Y + H) )
                              if ( (Y2 > Y) )
                                 if ( EnemyList[j].Tp = EnemyType.tpRunningKoopa )
                                 {
                                    ShowStar (EnemyList[i].XPos, EnemyList[i].YPos);
                                    if ( Tp == EnemyType.tpRunningKoopa )
                                    {
                                       ShowStar (EnemyList[j].XPos, EnemyList[j].YPos);
                                       Kill(j);
                                    }
                                    Kill(i);
                                 }
                                 else
                                 {
                                    if ( Tp != EnemyType.tpRunningKoopa )
                                    {
                                       XVel = - XVel;
                                       EnemyList[j].XVel = - EnemyList[j].XVel;
                                       YVel = - YVel;
                                       EnemyList[j].YVel = - EnemyList[j].YVel;
                                       if ( Math.Abs(X - NewX1) < Buffers.W )
                                         if ( X > NewX1 )
                                         {
                                           EnemyList[i].XPos = EnemyList[i].XPos - XVel;
                                           XVel = -Math.Abs(XVel);
                                         }
                                         else
                                           if ( X < NewX1 )
                                           {
                                             EnemyList[i].XPos = EnemyList[i].XPos - XVel;
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
                        X = EnemyList[i].XPos + XVel;
                        Y = EnemyList[i].YPos + YVel;
//                        }
                        if ( (NewX1 <= X + Buffers.W / 2) )
                        {
                           if ( (NewX2 >= X) )
                              if ( (Y1 <= Y + H / 2) )
                                 if ( Y2 >= Y )
                                 {
                                    EnemyList[j].Tp = EnemyType.tpDyingFireBall;
                                    EnemyList[j].DelayCounter = - (Game.MAX_PAGE + 1);
                                    ShowStar (EnemyList[i].XPos, EnemyList[i].YPos);
                                    Kill(i);
                                 }
                        }
                     }
                  }
               }
            }
         }
      }

      public static void MoveEnemies()
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
            NewX = EnemyList[i].XPos + XVel;
            if ( DelayCounter > MoveDelay )
            {
               EnemyList[i].XPos = LastXPos;
               EnemyList[i].YPos = LastYPos;
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
                                 if ( (EnemyList[i].XPos > PlayerX2 + W) || (EnemyList[i].XPos + 24 + Buffers.W < PlayerX1) )
                                    Status++;
                                 break;
                              case 1:
                                 if ( (EnemyList[i].XPos > PlayerX2) || (EnemyList[i].XPos + 24 < PlayerX1) )
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
                           if ( EnemyList[i].YPos + YVel <= (MapY * H - 19) )
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
                           if ( EnemyList[i].YPos > (MapY * H) )
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
                     if ( EnemyList[i].YPos + H >= NV * H )
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
                     if ( PlayerX1 > EnemyList[i].XPos )
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
                        case EnemyType.tpKoopa:
                        case EnemyType.tpSleepingKoopa:
                        case EnemyType.tpWakingKoopa:
                        case EnemyType.tpRunningKoopa:
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
                        if ( EnemyList[i].XPos % 6 = 0 )
                          Inc (YVel);
                     }
                     else
                        Check(j);
                     EnemyList[i].XPos = EnemyList[i].XPos + XVel;
                     EnemyList[i].YPos = EnemyList[i].YPos + YVel;
                     if ( XVel == 0 )
                     {
                        XVel = -OldXVel;
                        if ( Tp == EnemyType.tpDyingFireBall )
                          ShowFire(EnemyList[i].XPos, EnemyList[i].YPos);
                     }
//                     { if YVel = 0 ) YVel = - OldYVel; }
                  }
                  LastXPos = EnemyList[i].XPos;
                  LastYPos = EnemyList[i].YPos;
               }
            }
            else
            {
               if ( (XVel != 0) || (YVel != 0) )
               {
                  EnemyList[i].XPos = LastXPos + (DelayCounter * XVel) / (MoveDelay + 1);
                  EnemyList[i].YPos = LastYPos + (DelayCounter * YVel) / (MoveDelay + 1);
               }
            }
         }

//        for i = 1 to NumEnemies do
         for (int i = 0; i < NumEnemies; i++)
         {
            j = (int)(ActiveEnemies[i]);
//            with EnemyList[j] do
            if ( EnemyList[j].Tp == tpChibibo ||EnemyList[j].Tp == EnemyType.tpChamp ||EnemyList[j].Tp == EnemyType.tpLife ||EnemyList[j].Tp == EnemyType.tpFlower ||EnemyList[j].Tp == EnemyType.tpStar 
                  ||EnemyList[j].Tp == EnemyType.tpVertFish ||EnemyList[j].Tp == EnemyType.tpVertFireBall ||EnemyList[j].Tp == EnemyType.tpVertPlant
                  ||EnemyList[j].Tp == EnemyType.tpRed ||EnemyList[j].Tp == EnemyType.tpKoopa || EnemyList[j].Tp == EnemyType.tpSleepingKoopa 
                  || EnemyList[j].Tp == EnemyType.tpWakingKoopa || EnemyList[j].Tp == EnemyType.tpRunningKoopa 
                  ||EnemyList[j].Tp == EnemyType.tpLiftStart ||EnemyList[j].Tp == EnemyType.BlockLift ||EnemyList[j].Tp == EnemyType.tpDonut ||EnemyList[j].Tp == EnemyType.tpLiftEnd )
            {
               if ( PlayerX1 < EnemyList[i].XPos + Buffers.W )
               {
                  if ( PlayerX2 > EnemyList[i].XPos )
                  {
                     if ( PlayerY1 + PlayerYVel < EnemyList[i].YPos + H )
                     {
                        if ( PlayerY2 + PlayerYVel > EnemyList[i].YPos )
                        {
                           if ( Star )
                           {
                              if ( !(Tp == EnemyType.tpLiftStart ||EnemyList[j].Tp == EnemyType.BlockLift ||EnemyList[j].Tp == EnemyType.tpDonut ||EnemyList[j].Tp == EnemyType.tpLiftEnd) )
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
                                EnemyList[j].Tp = EnemyType.tpRunningKoopa;
                                 XVel = 5 * (2 * (byte)(EnemyList[i].XPos > PlayerX1) - 1);
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
                                EnemyList[j].Tp = EnemyType.tpDying;
                                 DelayCounter = -(Game.MAX_PAGE + 1);
                                 CoinGlitter(EnemyList[i].XPos, EnemyList[i].YPos);
                                 break;
                              }
                              case EnemyType.tpLife:
                              {
                                 cdLife = 0x1;
                                EnemyList[j].Tp = EnemyType.tpDying;
                                 DelayCounter = - (Game.MAX_PAGE + 1);
                                 CoinGlitter(EnemyList[i].XPos, EnemyList[i].YPos);
                                 Buffers.AddScore(1000);
                                 break;
                              }
                              case EnemyType.tpFlower:
                              {
                                 cdFlower = 0x1;
                                EnemyList[j].Tp = EnemyType.tpDying;
                                 DelayCounter = - (Game.MAX_PAGE + 1);
                                 CoinGlitter(EnemyList[i].XPos, EnemyList[i].YPos);
                                 Buffers.AddScore(1000);
                                 break;
                              }
                              case EnemyType.tpStar:
                              {
                                 cdStar = 0x1;
                                EnemyList[j].Tp = EnemyType.tpDying;
                                 DelayCounter = - (Game.MAX_PAGE + 1);
                                 CoinGlitter(EnemyList[i].XPos, EnemyList[i].YPos);
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
                                 if ( ((PlayerYVel > YVel) || (PlayerYVel > 0)) && (PlayerY2 <= EnemyList[i].YPos + H) )
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
                                          if ( EnemyList[i].YPos + H < NV * H )
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
                                       case EnemyType.tpLiftStart:
                                       case EnemyType.BlockLift:
                                       case EnemyType.tpDonut:
                                       case EnemyType.tpLiftEnd:
                                       {
                                          if ( Tp == EnemyType.tpDonut )
                                          {
                                             Status = 2;
                                             if ( (Counter > 20) && (YVel == 0) )
                                               YVel++;
                                          }
                                          cdStopJump = (byte)(PlayerYVel != 2);
                                          cdLift = 1;
                                          PlayerY1 = EnemyList[i].YPos - 2 * H;
                                          PlayerY2 = EnemyList[i].YPos - 1;
                                          PlayerXVel = XVel;
                                          if ( MoveDelay != 0 )
                                             PlayerXVel = XVel * EnemyList[i].XPos % 2;
                                          PlayerYVel = YVel;
                                          break;
                                       }
                                    }
                                 }
                                 else
                                 {

                                    if (!((Tp == EnemyType.tpVertFish) && (!(Math.Abs(DelayCounter - MoveDelay) <= 1)) ||
                                              (Tp == EnemyType.tpLiftStart || Tp == EnemyType.BlockLift || Tp == EnemyType.tpDonut || Tp == EnemyType.tpLiftEnd)))
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

      public static void StartEnemies (int X, short Dir)
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
                  if ( Buffers.CanHoldYou(WorldMap[X - 1, i]) || Buffers.CanHoldYou(WorldMap[X + 1, i]) )
                     NewEnemy(tpBlockLift, 0, X, i, -Dir, 0, 0);
                  else
                     NewEnemy(tpBlockLift, 0, X, i, 0, -Dir, 0);
                   break;
               case '±': NewEnemy(tpDonut, 0, X, i, 0, 0, 0); break;
               default:
                  Remove = false;
                  break;
            }
            if (Remove)
               WorldMap[X, i] = ' ';
         }
      }
      
      public static void HitAbove (int MapX, int MapY)
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

*/

   } // class Enemies

} // namespace MarioPort
