
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing;
using MarioPort;
﻿using Resources = MarioPort.Properties.Resources;

namespace MarioPort
{
   //-------------------------------------------------
   // Enemy Types
   //-------------------------------------------------
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

   //-------------------------------------------------
   // Class that controls the Enemies
   //-------------------------------------------------
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
      
      public static bool Star;
      
      public static void init()
      {
         // init FireballList && KoopaList
      }

      private static byte[,] rKoopa = new byte[4, 20 * 24 + 1];

      private static Bitmap[] FireBallList = new Bitmap[4];
      private static Bitmap[, ,] KoopaList = new Bitmap[Right - Left + 1, kRed - kGreen + 1, 2];

      private const int Grounded = 0;
      private const int Falling = 1;
      private const int MaxEnemies = 11;
      private const int MaxEnemiesAtOnce = 25;

      //int i = MarioPort.Buffers.W;
      //int j = Buffers.W;

      //-------------------------------------------------
      // Enemy Rectangle
      //-------------------------------------------------
      public class EnemyRec
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

         public EnemyRec()
         {
            BackGrAddr = new uint[FormMarioPort.MAX_PAGE + 1];
         }

         public static EnemyRec[] Create()
         {
            EnemyRec[] enemyRec = new EnemyRec[MaxEnemiesAtOnce];
            for (int i = 0; i < MaxEnemiesAtOnce; i++)
               enemyRec[i] = new EnemyRec();
            return enemyRec;
         }
      }

      private static EnemyRec[] EnemyList = EnemyRec.Create();
      private static string ActiveEnemies = "";
      private static byte NumEnemies;
      private static byte TimeCounter;

      private static Bitmap[,] EnemyPictures = new Bitmap[MaxEnemies, Right - Left + 1];
      
      //-------------------------------------------------
      // Kill the Enemy at EnemyList[i]
      //-------------------------------------------------
      private static void Kill(int i)
      {
         switch (EnemyList[i].Tp)
         {
            case EnemyType.tpChibibo:
            {
               EnemyList[i].Tp = EnemyType.tpDeadChibibo;
               EnemyList[i].XVel = -1 + 2 * System.Convert.ToByte ((EnemyList[i].XPos + EnemyList[i].XVel) % Buffers.W > Buffers.W / 2);
               EnemyList[i].YVel = -4;
               EnemyList[i].MoveDelay = 0;
               EnemyList[i].DelayCounter = 0;
               Buffers.AddScore(100);
               break;
            }
            case EnemyType.tpRed:
            {
               EnemyList[i].Tp = EnemyType.tpDeadRed;
               EnemyList[i].XVel = -1 + 2 * System.Convert.ToByte((EnemyList[i].XPos + EnemyList[i].XVel) % Buffers.W > Buffers.W / 2);
               EnemyList[i].YVel = -4;
               EnemyList[i].MoveDelay = 0;
               EnemyList[i].DelayCounter = 0;
               Buffers.AddScore(100);
               break;
            }
            case EnemyType.tpKoopa:
            case EnemyType.tpSleepingKoopa:
            case EnemyType.tpWakingKoopa:
            case EnemyType.tpRunningKoopa:
            {
               EnemyList[i].Tp = EnemyType.tpDeadKoopa;
               EnemyList[i].XVel = -1 + 2 * System.Convert.ToByte((EnemyList[i].XPos + EnemyList[i].XVel) % Buffers.W > Buffers.W / 2);
               EnemyList[i].YVel = -4;
               EnemyList[i].MoveDelay = 0;
               EnemyList[i].DelayCounter = 0;
               Buffers.AddScore(100);
               break;
            }
            case EnemyType.tpVertFish:
            {
               EnemyList[i].Tp = EnemyType.tpDeadVertFish;
               EnemyList[i].XVel = 0;
               EnemyList[i].YVel = 0;
               EnemyList[i].MoveDelay = 2;
               EnemyList[i].DelayCounter = 0;
               EnemyList[i].Status = Falling;
               Buffers.AddScore(100);
               break;
            }
            case EnemyType.tpVertPlant:
            {
               EnemyList[i].Tp = EnemyType.tpDeadVertPlant;
               EnemyList[i].DelayCounter = 0;
               EnemyList[i].YVel = 0;
               Buffers.AddScore(100);
               break;
            }
            default:
//               throw new Exception();
               break;
         }
      }
      
      //-------------------------------------------------
      // Show a temporary star
      //-------------------------------------------------
      private static void ShowStar (int X, int Y)
      {
//         Beep (100);
         if ((X + Buffers.W > Buffers.XView) && (X < Buffers.XView + FormMarioPort.SCREEN_WIDTH))
            TmpObj.NewTempObj(TmpObj.tpHit, X, Y, 0, 0, Buffers.W, Buffers.H);
      }

      //-------------------------------------------------
      // 
      //-------------------------------------------------
      private static void ShowFire (int X, int Y)
      {
         //Beep (50);
         X = X - 4;
         Y = Y - 4;
         if ((X + Buffers.W > Buffers.XView) && (X < Buffers.XView + FormMarioPort.SCREEN_WIDTH))
            TmpObj.NewTempObj(TmpObj.tpFire, X, Y, 0, 0, Buffers.W, Buffers.H);
      }
      
      //-------------------------------------------------
      // 
      //-------------------------------------------------
      private static void Mirror20x24(Bitmap from, ref Bitmap to)
      {
         from.RotateFlip(RotateFlipType.Rotate180FlipY);
         to = from.Clone() as Bitmap;
      }
      
      //-------------------------------------------------
      // 
      //-------------------------------------------------
      public static void InitEnemyFigures()
      {
         EnemyPictures[0, Right] = Resources.CHIBIBO_000;
         EnemyPictures[1, Right] = Resources.CHIBIBO_001;

         EnemyPictures[2, Right] = Resources.CHIBIBO_002;
         EnemyPictures[3, Right] = Resources.CHIBIBO_003;

         EnemyPictures[4, Left] = Resources.FISH_001;
         Figures.Mirror(EnemyPictures[3, Left], ref EnemyPictures[3, Right]);

         EnemyPictures[5, Left] = Resources.RED_000;
         EnemyPictures[6, Left] = Resources.RED_001;

         EnemyPictures[7, Right] = Resources.GRKP_000;
         EnemyPictures[8, Right] = Resources.GRKP_001;

         EnemyPictures[9, Right] = Resources.RDKP_000;
         EnemyPictures[10, Right] = Resources.RDKP_001;

         for( int i = 0; i < MaxEnemies; i++ )
         {
            if ( i == 6 || i == 7 )
               Figures.Mirror(EnemyPictures[i, Left], ref EnemyPictures[i, Right]);
            else
               if ( i != 3 )
                  Figures.Mirror(EnemyPictures[i, Left], ref EnemyPictures[i, Right]);
         }
         
         for( int i = 0; i <= 1; i++ )
         {
            for( int j = kGreen; j <= kRed; j++ )
            {
               Mirror20x24(KoopaList[Left, j, i], ref KoopaList[Right, j, i]);
            }
         }
      }

      //-------------------------------------------------
      // 
      //-------------------------------------------------
      public static void ClearEnemies()
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

      //-------------------------------------------------
      // 
      //-------------------------------------------------
      public static void StopEnemies()
      {
         int j;
         for (int i = 0; i < NumEnemies; i++)
         {
            j = (int)ActiveEnemies[i];

            switch (EnemyList[j].Tp)
            {
               case EnemyType.tpChibibo:
                  Buffers.WorldMap[EnemyList[j].MapX, EnemyList[j].MapY] = '€';
                  break;
               case EnemyType.tpVertFish:
                  Buffers.WorldMap[EnemyList[j].MapX, EnemyList[j].MapY - 2] = '§';
                  break;
               case EnemyType.tpVertFireBall:
                  Buffers.WorldMap[EnemyList[j].MapX, EnemyList[j].MapY - 2] = '‚';
                  break;
               case EnemyType.tpVertPlant:
                  Buffers.WorldMap[EnemyList[j].MapX, EnemyList[j].MapY - 2] = (char)((int)('„') + EnemyList[j].SubTp);
                  break;
               case EnemyType.tpRed:
                  Buffers.WorldMap[EnemyList[j].MapX, EnemyList[j].MapY] = '‡';
                  break;
               case EnemyType.tpKoopa:
               case EnemyType.tpSleepingKoopa:
               case EnemyType.tpWakingKoopa:
               case EnemyType.tpRunningKoopa:
                  Buffers.WorldMap[EnemyList[j].MapX, EnemyList[j].MapY] = (char)((int)('ˆ') + EnemyList[j].SubTp);
                  break;
               case EnemyType.tpBlockLift:
                  Buffers.WorldMap[EnemyList[j].MapX, EnemyList[j].MapY] = '°';
                  break;
               case EnemyType.tpDonut:
                  Buffers.WorldMap[EnemyList[j].MapX, EnemyList[j].MapY] = '±';
                  break;
               default:
                  //                  throw new Exception();
                  break;
            }
         }

         ClearEnemies();
      }

      //-------------------------------------------------
      // 
      //-------------------------------------------------
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

            for (i = 0; i < NumEnemies; i++)
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
         EnemyList[i].YPos = EnemyList[i].MapY * Buffers.H;
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

         ActiveEnemies = ActiveEnemies + (char)(i);
      }
      
      //-------------------------------------------------
      // 
      //-------------------------------------------------
      public static void ShowEnemies()
      {
         int i, j, Page;
         Bitmap Fig;

         Page = FormMarioPort.formRef.CurrentPage();

         for (i = 0; i < NumEnemies; i++)
         {
            j = (int)(ActiveEnemies[i]);

            if ( (EnemyList[j].XPos + 1 * Buffers.W < Buffers.XView) ||
                     (EnemyList[j].XPos > Buffers.XView + FormMarioPort.SCREEN_WIDTH + 0 * Buffers.W) ||
                           (EnemyList[j].YPos >= Buffers.YView + FormMarioPort.SCREEN_HEIGHT))
            {
               EnemyList[j].BackGrAddr[Page] = 0xFFFF;
            }
            else
            {
               if ( EnemyList[j].Tp == EnemyType.tpFireBall || EnemyList[j].Tp == EnemyType.tpDyingFireBall )
                  EnemyList[j].BackGrAddr[Page] = FormMarioPort.formRef.PushBackGr(EnemyList[j].XPos, EnemyList[j].YPos, Buffers.W, Buffers.H / 2);
               else
                  if ( EnemyList[j].Tp == EnemyType.tpVertPlant || EnemyList[j].Tp == EnemyType.tpDeadVertPlant )
                     EnemyList[j].BackGrAddr[Page] = FormMarioPort.formRef.PushBackGr(EnemyList[j].XPos, EnemyList[j].YPos, 24, 20);
                  else
                     if ( EnemyList[j].Tp == EnemyType.tpKoopa ||     
                          EnemyList[j].Tp == EnemyType.tpSleepingKoopa ||
                          EnemyList[j].Tp == EnemyType.tpWakingKoopa ||
                          EnemyList[j].Tp == EnemyType.tpRunningKoopa ||
                          EnemyList[j].Tp == EnemyType.tpDyingKoopa ||
                          EnemyList[j].Tp == EnemyType.tpDeadKoopa )
                        EnemyList[j].BackGrAddr[Page] = FormMarioPort.formRef.PushBackGr(EnemyList[j].XPos, EnemyList[j].YPos - 10, 24, 24);
                     else
                        EnemyList[j].BackGrAddr[Page] = FormMarioPort.formRef.PushBackGr(EnemyList[j].XPos, EnemyList[j].YPos, Buffers.W + 4, Buffers.H);
            }

            switch (EnemyList[i].Tp)
            {
               case EnemyType.tpChibibo:
                  FormMarioPort.formRef.DrawImage(EnemyList[i].XPos, EnemyList[i].YPos, Buffers.W, Buffers.H, EnemyPictures[1 + 3 * EnemyList[i].SubTp, System.Convert.ToByte(EnemyList[i].DirCounter % 32 < 16)]);
                  break;
               case EnemyType.tpFlatChibibo:
                  FormMarioPort.formRef.DrawImage(EnemyList[i].XPos, EnemyList[i].YPos, Buffers.W, Buffers.H, EnemyPictures[2 + 3 * EnemyList[i].SubTp, System.Convert.ToByte(EnemyList[i].DirCounter % 32 < 16)]);
                  break;
               case EnemyType.tpDeadChibibo:
                  FormMarioPort.formRef.UpSideDown(EnemyList[i].XPos, EnemyList[i].YPos, Buffers.W, Buffers.H, EnemyPictures[1, Left]);
                  break;
               case EnemyType.tpRisingChamp:
                  if (EnemyList[i].YPos != (EnemyList[i].MapY * Buffers.H))
                     if (EnemyList[i].SubTp == 0)
                        FormMarioPort.formRef.DrawPart(EnemyList[i].XPos, EnemyList[i].YPos, Buffers.W, Buffers.H, 0, Buffers.H - EnemyList[i].YPos % Buffers.H - 1, Resources.CHAMP_000);
                     else
                        FormMarioPort.formRef.DrawPart(EnemyList[i].XPos, EnemyList[i].YPos, Buffers.W, Buffers.H, 0, Buffers.H - EnemyList[i].YPos % Buffers.H - 1, Resources.POISON_000);
                  break;
               case EnemyType.tpChamp:
                  if (EnemyList[i].SubTp == 0)
                     FormMarioPort.formRef.DrawImage(EnemyList[i].XPos, EnemyList[i].YPos, Buffers.W, Buffers.H, Resources.CHAMP_000);
                  else
                     FormMarioPort.formRef.DrawImage(EnemyList[i].XPos, EnemyList[i].YPos, Buffers.W, Buffers.H, Resources.POISON_000);
                  break;
               case EnemyType.tpRisingLife:
                  if (EnemyList[i].YPos != (EnemyList[i].MapY * Buffers.H))
                     FormMarioPort.formRef.DrawPart(EnemyList[i].XPos, EnemyList[i].YPos, Buffers.W, Buffers.H, 0, Buffers.H - EnemyList[i].YPos % Buffers.H - 1, Resources.LIFE_000);
                  break;
               case EnemyType.tpLife:
                  FormMarioPort.formRef.DrawImage(EnemyList[i].XPos, EnemyList[i].YPos, Buffers.W, Buffers.H, Resources.LIFE_000);
                	break;
               case EnemyType.tpRisingFlower:
                  if (EnemyList[i].YPos != (EnemyList[i].MapY * Buffers.H))
                     FormMarioPort.formRef.DrawPart(EnemyList[i].XPos, EnemyList[i].YPos, Buffers.W, Buffers.H, 0, Buffers.H - EnemyList[i].YPos % Buffers.H - 1, Resources.FLOWER_000);
                  break;
               case EnemyType.tpFlower:
                  FormMarioPort.formRef.DrawImage(EnemyList[i].XPos, EnemyList[i].YPos, Buffers.W, Buffers.H, Resources.FLOWER_000);
                	break;
               case EnemyType.tpRisingStar:
                  if (EnemyList[i].YPos != (EnemyList[i].MapY * Buffers.H))
                     FormMarioPort.formRef.DrawPart(EnemyList[i].XPos, EnemyList[i].YPos, Buffers.W, Buffers.H, 0, Buffers.H - EnemyList[i].YPos % Buffers.H - 1, Resources.STAR_000);
                  break;
               case EnemyType.tpStar:
                  FormMarioPort.formRef.DrawImage(EnemyList[i].XPos, EnemyList[i].YPos, Buffers.W, Buffers.H, Resources.STAR_000);
                	break;
               case EnemyType.tpFireBall:
                  if ( EnemyList[i].XPos % 4 < 2 )
                     FormMarioPort.formRef.DrawImage(EnemyList[i].XPos, EnemyList[i].YPos, 12, Buffers.H / 2, Resources.FIRE_000);
                  else
                     FormMarioPort.formRef.DrawImage(EnemyList[i].XPos, EnemyList[i].YPos, 12, Buffers.H / 2, Resources.FIRE_001);
                  break;
               case EnemyType.tpVertFish:
                  if ((EnemyList[i].YVel != 0) || (EnemyList[i].YPos < Buffers.NV * Buffers.H - Buffers.H))
                     FormMarioPort.formRef.DrawImage(EnemyList[i].XPos, EnemyList[i].YPos, Buffers.W, Buffers.H, EnemyPictures[3, System.Convert.ToByte(PlayerX1 > EnemyList[i].XPos)]);
                  break;
               case EnemyType.tpDeadVertFish:
                  if ((EnemyList[i].YPos < Buffers.NV * Buffers.H - Buffers.H) || (EnemyList[i].YVel != 0))
                     FormMarioPort.formRef.UpSideDown(EnemyList[i].XPos, EnemyList[i].YPos, Buffers.W, Buffers.H, EnemyPictures[3, System.Convert.ToByte(PlayerX1 <= EnemyList[i].XPos)]);
                  break;
               case EnemyType.tpVertFireBall:
               {
                  if ( Math.Abs(Blocks.DelayCounter - Blocks.MoveDelay) <= 1 )
                  {
                     FormMarioPort.formRef.DrawImage(EnemyList[i].XPos, EnemyList[i].YPos, Buffers.W, Buffers.H, FireBallList[new Random().Next(4)]);
                     //Glitter.NewGlitter(EnemyList[i].XPos + Random (W), EnemyList[i].YPos + Random (H), 57 + Random (7), 14 + Random (20));
                     //Stars.NewStar(EnemyList[i].XPos + Random (W), EnemyList[i].YPos + Random (H), 57 + Random (7), 14 + Random (20));
                  }
                  break;
               }
               case EnemyType.tpVertPlant:
               {
                  if ( TimeCounter % 32 < 16 )
                  {
                     switch (EnemyList[i].SubTp)
                     {
                        case 0:
                        case 1:
                           Fig = Resources.PPLANT_002;
                           break;
                        default:
                           Fig = Resources.PPLANT_000;
                           break;
                     }
                  }
                  else
                  {
                     switch (EnemyList[i].SubTp)
                     {
                        case 0:
                        case 1:
                           Fig = Resources.PPLANT_003;
                           break;
                        default:
                           Fig = Resources.PPLANT_001;
                           break;
                     }
                  }
                  FormMarioPort.formRef.DrawPart(EnemyList[i].XPos, EnemyList[i].YPos, 24, 20, 0, (EnemyList[i].MapY * Buffers.H) - EnemyList[i].YPos - 1, Fig);
                  break;
               }
               case EnemyType.tpDeadVertPlant:
               {
                  EnemyList[i].DelayCounter = 0;
                  EnemyList[i].MoveDelay = 0;
                  EnemyList[i].YVel = 0;
                  EnemyList[i].Status++;
                  if (EnemyList[i].Status < 12)
                     FormMarioPort.formRef.DrawImage(EnemyList[i].XPos, EnemyList[i].YPos, 24, 20, Resources.HIT_000);
                  else
                     if (EnemyList[i].Status > 14)
                        EnemyList[i].Tp = EnemyType.tpDying;
                  break;
               }
               case EnemyType.tpRed:
               FormMarioPort.formRef.DrawImage(EnemyList[i].XPos, EnemyList[i].YPos, Buffers.W, Buffers.H, EnemyPictures[6 + System.Convert.ToByte(EnemyList[i].DirCounter % 16 <= 8), System.Convert.ToByte(EnemyList[i].XVel > 0)]);
                  break;
               case EnemyType.tpDeadRed:
                  FormMarioPort.formRef.UpSideDown(EnemyList[i].XPos, EnemyList[i].YPos, Buffers.W, Buffers.H, EnemyPictures[6 + System.Convert.ToByte(EnemyList[i].DirCounter % 16 <= 8), System.Convert.ToByte(EnemyList[i].XVel > 0)]);
                  break;
               case EnemyType.tpKoopa:
                  FormMarioPort.formRef.DrawImage(EnemyList[i].XPos, EnemyList[i].YPos - 10, Buffers.W, 24, KoopaList[System.Convert.ToByte(EnemyList[i].XVel > 0), EnemyList[i].SubTp, System.Convert.ToByte(EnemyList[i].DirCounter % 16 <= 8)]);
                  break;
               case EnemyType.tpWakingKoopa:
               case EnemyType.tpRunningKoopa:
                  FormMarioPort.formRef.DrawImage(EnemyList[i].XPos, EnemyList[i].YPos, Buffers.W, Buffers.H, EnemyPictures[8 + 2 * EnemyList[i].SubTp + 1 - System.Convert.ToByte(EnemyList[i].DirCounter % 16 <= 8), System.Convert.ToByte(EnemyList[i].DirCounter % 32 <= 16)]);
                  break;
               case EnemyType.tpSleepingKoopa:
                  FormMarioPort.formRef.DrawImage(EnemyList[i].XPos, EnemyList[i].YPos, Buffers.W, Buffers.H, EnemyPictures[8 + 2 * EnemyList[i].SubTp, 0]);
                  break;
               case EnemyType.tpDeadKoopa:
                  FormMarioPort.formRef.UpSideDown(EnemyList[i].XPos, EnemyList[i].YPos, Buffers.W, Buffers.H, EnemyPictures[8 + 2 * EnemyList[i].SubTp, System.Convert.ToByte(EnemyList[i].DirCounter % 16 <= 8)]);
                  break;
               case EnemyType.tpBlockLift:
                  FormMarioPort.formRef.DrawImage(EnemyList[i].XPos, EnemyList[i].YPos, Buffers.W, Buffers.H, Resources.LIFT1_000);
                  break;
               case EnemyType.tpDonut:
               {
                  if (EnemyList[i].Status == 0)
                  {
                     FormMarioPort.formRef.DrawImage(EnemyList[i].XPos, EnemyList[i].YPos, Buffers.W, Buffers.H, Resources.DONUT_000);
                     if (EnemyList[i].YVel == 0)
                        EnemyList[i].Counter = 0;
                  }
                  else
                  {
                     FormMarioPort.formRef.DrawImage(EnemyList[i].XPos, EnemyList[i].YPos, Buffers.W, Buffers.H, Resources.DONUT_001);
                     EnemyList[i].Status--;
                  }
                  if (EnemyList[i].YVel > 0)
                     if (EnemyList[i].Counter % 24 == 0)
                        EnemyList[i].YVel++;
                  EnemyList[i].Counter++;
                  break;
               }
            }
         }
      }
      
      //-------------------------------------------------
      // 
      //-------------------------------------------------
      public static void HideEnemies()
      {
         int i, j, Page;
         Page = FormMarioPort.formRef.CurrentPage();
         for ( i = NumEnemies - 1; i >= 0; i++)
         {
            j = (int)(ActiveEnemies[i]);

            if (EnemyList[j].BackGrAddr[Page] != 0xFFFF)
               FormMarioPort.formRef.PopBackGr( (ushort)EnemyList[j].BackGrAddr[Page] );
         }
      }
      
      //-------------------------------------------------
      // 
      //-------------------------------------------------
      private static void Check (int i)
      {
         const int Safe = Buffers.EY1;
         const int HSafe = Buffers.H * Safe;

         char NewCh1, NewCh2, Ch;
         int j, k, l, Side, AtX, NewX, NewX1, NewX2, Y1, Y2, NewY;
         bool Hold1, Hold2;
         int X, Y;

         switch (EnemyList[i].Tp)
         {
            case EnemyType.tpRisingChamp:
            case EnemyType.tpRisingLife:
            case EnemyType.tpRisingFlower:
            case EnemyType.tpRisingStar:
            {
               if ( ((EnemyList[i].YPos / Buffers.H) == (EnemyList[i].YPos / Buffers.H)) && (EnemyList[i].YPos != EnemyList[i].MapY * Buffers.H) )
               {
                  EnemyList[i].XVel = 1 - 2 * System.Convert.ToByte( Buffers.CanHoldYou( Buffers.WorldMap[EnemyList[i].MapX + 1, EnemyList[i].MapY - 1] ) );

                  switch (EnemyList[i].Tp)
                  {
                     case EnemyType.tpRisingChamp:
                        EnemyList[i].Tp = EnemyType.tpChamp;
                        break;
                     case EnemyType.tpRisingLife:
                     {
                        EnemyList[i].Tp = EnemyType.tpLife;
                        EnemyList[i].XVel = 2 * EnemyList[i].XVel;
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
                  j = (EnemyList[i].YPos % Buffers.H);
                  if ( j % 2 == 0 )
                     //Beep (130 - 20 * j);
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
                  Y1 = (EnemyList[i].YPos + Buffers.H / 4 + HSafe) / Buffers.H - Safe;
                  NewCh1 = Buffers.WorldMap[NewX, Y1];
                  if ( Buffers.CanHoldYou( NewCh1 ) )
                     EnemyList[i].XVel = 0;
               }
               NewX = EnemyList[i].XPos;
               NewY = EnemyList[i].YPos;
               AtX = (EnemyList[i].XPos + Buffers.W / 4 + EnemyList[i].XVel) / Buffers.W;
               NewY = (EnemyList[i].YPos + 2 + Buffers.H / 4 + EnemyList[i].YVel + HSafe) / Buffers.H - Safe;
               NewCh1 =Buffers. WorldMap[AtX, NewY];
               if ( (EnemyList[i].YVel > 0) && ( Buffers.CanHoldYou(NewCh1) || Buffers.CanStandOn(NewCh1) ) )
               {
                  EnemyList[i].YPos = ((EnemyList[i].YPos + EnemyList[i].YVel - 5 + HSafe) / Buffers.H - Safe) * Buffers.H;
                  EnemyList[i].YVel = -2;
               }
               else
                  if ( EnemyList[i].XPos % 3 == 0 )
                     EnemyList[i].YVel++;
               if ( (EnemyList[i].XVel == 0) || (NewX < Buffers.XView - Buffers.W) || (NewX > Buffers.XView + Buffers.NH * Buffers.W + Buffers.W) || (NewY > Buffers.NV * Buffers.H) )
               {
                  EnemyList[i].DelayCounter = - (FormMarioPort.MAX_PAGE + 1);
                  EnemyList[i].Tp = EnemyType.tpDyingFireBall;
               }
               return;
            }
            case EnemyType.tpStar:
               Glitter.StartGlitter(EnemyList[i].XPos, EnemyList[i].YPos, Buffers.W, Buffers.H);
               break;
         }


         if (!(EnemyList[i].Tp == EnemyType.tpVertFish || EnemyList[i].Tp == EnemyType.tpDeadVertFish 
                  || EnemyList[i].Tp == EnemyType.tpVertFireBall || EnemyList[i].Tp == EnemyType.tpVertPlant 
                  || EnemyList[i].Tp == EnemyType.tpDeadVertPlant))
         {
            Side = (EnemyList[i].XVel > 0) ? 1 : 0;
            Side *= Buffers.W - 1;

            AtX = (EnemyList[i].XPos + Side) / Buffers.W;
            NewX = (EnemyList[i].XPos + Side + EnemyList[i].XVel) / Buffers.W;
            if ( (AtX != NewX) || (EnemyList[i].Status == Falling) )
            {
               Y1 = (EnemyList[i].YPos + HSafe) / Buffers.H - Safe;
               Y2 = (EnemyList[i].YPos + HSafe + Buffers.H - 1) / Buffers.H - Safe;
               NewCh1 = Buffers.WorldMap[NewX, Y1];
               NewCh2 = Buffers.WorldMap[NewX, Y2];
               Hold1 = (Buffers.CanHoldYou(NewCh1));
               Hold2 = (Buffers.CanHoldYou(NewCh2));
               if ( Hold1 || Hold2 )
               {
                  if ( EnemyList[i].Tp == EnemyType.tpRunningKoopa )
                  {
                     ShowStar (EnemyList[i].XPos + EnemyList[i].XVel, EnemyList[i].YPos);
                     l = (EnemyList[i].YPos + HSafe + Buffers.H / 2) / Buffers.H - Safe;
                     Ch = Buffers.WorldMap[NewX, l];
                     if ( (EnemyList[i].XPos >= Buffers.XView) && (EnemyList[i].XPos + Buffers.W <= Buffers.XView + Buffers.NH * Buffers.W) )
                     {
                        switch (Ch)
                        {
                           case 'J':
                              TmpObj.BreakBlock (NewX, l);
                              break;
                           case '?':
                           {
                              switch (Buffers.WorldMap[NewX, l - 1])
                              {
                                 case ' ':
                                    TmpObj.HitCoin (NewX * Buffers.W, l * Buffers.H, true);
                                    break;
                                 case 'à':
                                 {
                                    if ( Buffers.data.mode[Buffers.Player] == Buffers.mdSmall )
                                       NewEnemy(EnemyType.tpRisingChamp, 0, NewX, l, 0, -1, 1);
                                    else
                                       NewEnemy(EnemyType.tpRisingFlower, 0, NewX, l, 0, -1, 1);
                                    break;
                                 }
                                 case 'á':
                                    NewEnemy(EnemyType.tpRisingLife, 0, NewX, l, 0, -1, 2);
                                    break;
                              }
                              break;
                           }
                              
                           Buffers.WorldMap[NewX, l] = '@';
                        }
                     }
                  }
               }
               EnemyList[i].XVel = 0;
            }

            AtX = (EnemyList[i].XPos + EnemyList[i].XVel) / Buffers.W;
            NewX = (EnemyList[i].XPos + EnemyList[i].XVel + Buffers.W - 1) / Buffers.W;
            NewY = (EnemyList[i].YPos + 1 + Buffers.H + EnemyList[i].YVel + HSafe) / Buffers.H - Safe;

            NewCh1 = Buffers.WorldMap[AtX, NewY];
            NewCh2 = Buffers.WorldMap[NewX, NewY];
            Hold1 = (Buffers.CanHoldYou(NewCh1) || Buffers.CanStandOn(NewCh1));
            Hold2 = (Buffers.CanHoldYou(NewCh2) || Buffers.CanStandOn(NewCh2));

            if ( EnemyList[i].Tp == EnemyType.tpLiftStart || EnemyList[i].Tp == EnemyType.tpBlockLift || EnemyList[i].Tp == EnemyType.tpDonut || EnemyList[i].Tp == EnemyType.tpLiftEnd )
            {
               if ( (EnemyList[i].YVel != 0) && (!(EnemyList[i].Tp == EnemyType.tpDonut))  )
               {
                  if ( EnemyList[i].YVel < 0 )
                     Hold1 = (EnemyList[i].YPos + EnemyList[i].YVel) / Buffers.H < EnemyList[i].MapY;
                  if ( Hold1 )
                     EnemyList[i].YVel = -EnemyList[i].YVel;
               }
            }
            else
            {
               switch (EnemyList[i].Status)
               {
                  case Grounded:
                  {
                     if ( !(Hold1 || Hold2) )
                     {
                        EnemyList[i].Status = Falling;
                        EnemyList[i].YVel = 1;
                     }
                     if ( (EnemyList[i].SubTp == 1) && (EnemyList[i].Tp == EnemyType.tpKoopa) )
                     {
                        if ( (EnemyList[i].XVel > 0) && (EnemyList[i].XPos % Buffers.W >= 11 && EnemyList[i].XPos % Buffers.W <= 19) )
                           if ( (!Hold2) && Hold1 )
                              EnemyList[i].XVel = 0;
                        if ( (EnemyList[i].XVel < 0) && (EnemyList[i].XPos % Buffers.W >= 1 && EnemyList[i].XPos % Buffers.W <= 9) )
                           if ( (!Hold1) && Hold2 )
                              EnemyList[i].XVel = 0;
                     }
                     break;
                  }
                  case Falling:
                  {
                     if ( Hold1 || Hold2 )
                     {
                        EnemyList[i].Status = Grounded;
                        EnemyList[i].YPos = ((EnemyList[i].YPos + EnemyList[i].YVel + 1 + HSafe) / Buffers.H - Safe) * Buffers.H;
                        if ( EnemyList[i].Tp == EnemyType.tpStar )
                        {
                           EnemyList[i].YVel = - (5 * EnemyList[i].YVel) / 2;
                           EnemyList[i].Status = Falling;
                        }
                        else
                           EnemyList[i].YVel = 0;
                     }
                     else
                     {
                        EnemyList[i].YVel++;
                        if ( EnemyList[i].YVel > 4 ) 
                           EnemyList[i].YVel = 4;
                     }
                     break;
                  }
               }
            }
         }

         NewX1 = EnemyList[i].XPos + EnemyList[i].XVel;
         NewX2 = NewX1 + Buffers.W - 1 + 4 * System.Convert.ToByte(EnemyList[i].Tp == EnemyType.tpVertPlant);
         Y1 = EnemyList[i].YPos + EnemyList[i].YVel;
         Y2 = Y1 + Buffers.H - 1;

         if (EnemyList[i].Tp == EnemyType.tpChibibo || EnemyList[i].Tp == EnemyType.tpFlatChibibo || EnemyList[i].Tp == EnemyType.tpVertFish
               || EnemyList[i].Tp == EnemyType.tpVertPlant || EnemyList[i].Tp == EnemyType.tpDeadVertPlant || EnemyList[i].Tp == EnemyType.tpRed
               || EnemyList[i].Tp == EnemyType.tpKoopa ||
                     EnemyList[i].Tp == EnemyType.tpSleepingKoopa ||
                     EnemyList[i].Tp == EnemyType.tpWakingKoopa ||
                     EnemyList[i].Tp == EnemyType.tpRunningKoopa)
         {
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
                     X = EnemyList[i].XPos + EnemyList[j].XVel;
                     Y = EnemyList[i].YPos + EnemyList[j].YVel;
                     
                     if ( NewX1 < X + Buffers.W )
                        if ( (NewX2 > X) )
                           if ( (Y1 < Y + Buffers.H) )
                              if ( (Y2 > Y) )
                                 if ( EnemyList[j].Tp == EnemyType.tpRunningKoopa )
                                 {
                                    ShowStar (EnemyList[i].XPos, EnemyList[i].YPos);
                                    if ( EnemyList[j].Tp == EnemyType.tpRunningKoopa )
                                    {
                                       ShowStar (EnemyList[j].XPos, EnemyList[j].YPos);
                                       Kill(j);
                                    }
                                    Kill(i);
                                 }
                                 else
                                 {
                                    if ( EnemyList[j].Tp != EnemyType.tpRunningKoopa )
                                    {
                                       EnemyList[j].XVel = - EnemyList[j].XVel;
                                       EnemyList[j].XVel = - EnemyList[j].XVel;
                                       EnemyList[j].YVel = - EnemyList[j].YVel;
                                       EnemyList[j].YVel = - EnemyList[j].YVel;
                                       if ( Math.Abs(X - NewX1) < Buffers.W )
                                         if ( X > NewX1 )
                                         {
                                           EnemyList[i].XPos = EnemyList[i].XPos - EnemyList[j].XVel;
                                           EnemyList[j].XVel = -Math.Abs(EnemyList[j].XVel);
                                         }
                                         else
                                           if ( X < NewX1 )
                                           {
                                             EnemyList[i].XPos = EnemyList[i].XPos - EnemyList[j].XVel;
                                             EnemyList[j].XVel = Math.Abs(EnemyList[j].XVel);
                                           }
                                    }
                                 }
                  }
                  else
                  {
                     if ( (EnemyList[j].Tp == EnemyType.tpFireBall) )
                     {
                        X = EnemyList[i].XPos + EnemyList[j].XVel;
                        Y = EnemyList[i].YPos + EnemyList[j].YVel;
                        
                        if ( (NewX1 <= X + Buffers.W / 2) )
                        {
                           if ( (NewX2 >= X) )
                              if ( (Y1 <= Y + Buffers.H / 2) )
                                 if ( Y2 >= Y )
                                 {
                                    EnemyList[j].Tp = EnemyType.tpDyingFireBall;
                                    EnemyList[j].DelayCounter = - (FormMarioPort.MAX_PAGE + 1);
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
      
      //-------------------------------------------------
      // 
      //-------------------------------------------------
      public static void MoveEnemies()
      {
         int i, j, Page, NewX, OldXVel, OldYVel;

         Page = FormMarioPort.formRef.CurrentPage();
         TimeCounter++;
         
         for (i = 0; i < NumEnemies; i++)
         {
            j = (int)(ActiveEnemies[i]);

            EnemyList[j].DelayCounter++;
            NewX = EnemyList[i].XPos + EnemyList[j].XVel;
            if (EnemyList[j].DelayCounter > EnemyList[j].MoveDelay)
            {
               EnemyList[i].XPos = EnemyList[j].LastXPos;
               EnemyList[i].YPos = EnemyList[j].LastYPos;
               EnemyList[j].DirCounter++;

               if (EnemyList[j].Tp == EnemyType.tpVertFish || EnemyList[j].Tp == EnemyType.tpVertFireBall || EnemyList[j].Tp == EnemyType.tpVertPlant)
               {
                  if (EnemyList[j].Tp == EnemyType.tpVertPlant)
                  {
                     switch (EnemyList[j].Status)
                     {
                        case 0:
                        {
                           switch (EnemyList[j].SubTp)
                           {
                              case 0:
                                 if ( (EnemyList[i].XPos > PlayerX2 + Buffers.W) || (EnemyList[i].XPos + 24 + Buffers.W < PlayerX1) )
                                    EnemyList[j].Status++;
                                 break;
                              case 1:
                                 if ( (EnemyList[i].XPos > PlayerX2) || (EnemyList[i].XPos + 24 < PlayerX1) )
                                    EnemyList[j].Status++;
                                 break;
                              case 2:
                                 EnemyList[j].Status++;
                                 break;
                           }
                           EnemyList[j].YVel = 0;
                           EnemyList[j].DelayCounter = 0;
                           EnemyList[j].MoveDelay = 1;
                           break;
                        }
                        case 1:
                        {
                           EnemyList[j].YVel = -1;
                           EnemyList[j].DelayCounter = 0;
                           EnemyList[j].MoveDelay = 2;
                           if (EnemyList[i].YPos + EnemyList[j].YVel <= (EnemyList[j].MapY * Buffers.H - 19))
                           {
                              EnemyList[j].YVel = 0;
                              EnemyList[j].DelayCounter = 0;
                              EnemyList[j].MoveDelay = 2;
                              EnemyList[j].Counter = 0;
                              EnemyList[j].Status++;
                           }
                           break;
                        }
                        case 2:
                        {
                           EnemyList[j].Counter++;
                           if (EnemyList[j].Counter > 200)
                              EnemyList[j].Status++;
                           EnemyList[j].MoveDelay = 0;
                           EnemyList[j].DelayCounter = 0;
                           break;
                        }
                        case 3:
                        {
                           EnemyList[j].YVel = 1;
                           EnemyList[j].DelayCounter = 0;
                           EnemyList[j].MoveDelay = 2;
                           if (EnemyList[i].YPos > (EnemyList[j].MapY * Buffers.H))
                              EnemyList[j].Status++;
                           break;
                        }
                        case 4:
                        {
                           EnemyList[j].YVel = 0;
                           EnemyList[j].MoveDelay = 100 + new Random().Next(100);
                           EnemyList[j].DelayCounter = 0;
                           EnemyList[j].Status = 0;
                           break;
                        }
                     }
                  }
                  else
                  {
                     if ( EnemyList[i].YPos + Buffers.H >= Buffers.NV * Buffers.H )
                     {
                        if (EnemyList[j].YVel > 0)
                        {
                           EnemyList[j].YVel = 0;
                           EnemyList[j].MoveDelay = 100 + new Random().Next(300);
                           EnemyList[j].DelayCounter = 0;
                        }
                     }
                     else
                     {
                        EnemyList[j].YVel = -10;
                        EnemyList[j].MoveDelay = 1;
                        EnemyList[j].DelayCounter = 0;
                        if (EnemyList[j].Tp == EnemyType.tpVertFireBall)
                        {
                           //Beep (100);
                           EnemyList[j].YVel = -9;
                        }
                     }
                  }
               }
               if (EnemyList[j].Tp == EnemyType.tpSleepingKoopa)
               {
                  EnemyList[j].Counter++;
                  if (EnemyList[j].Counter > 150)
                  {
                     EnemyList[j].Tp = EnemyType.tpWakingKoopa;
                     EnemyList[j].XVel = 1;
                     EnemyList[j].Counter = 0;
                  }
               }
               if (EnemyList[j].Tp == EnemyType.tpWakingKoopa)
               {
                  EnemyList[j].XVel = -EnemyList[j].XVel;
                  EnemyList[j].MoveDelay = 1;
                  EnemyList[j].DelayCounter = 0;
                  EnemyList[j].Counter++;
                  if (EnemyList[j].Counter > 50)
                  {
                     EnemyList[j].Tp = EnemyType.tpKoopa;
                     if ( PlayerX1 > EnemyList[i].XPos )
                        EnemyList[j].XVel = 1;
                     else
                        EnemyList[j].XVel = -1;
                  }
               }
               
               if ( EnemyList[j].Tp == EnemyType.tpDying || EnemyList[j].Tp == EnemyType.tpDyingFireBall || EnemyList[j].Tp == EnemyType.tpDyingKoopa )
                  EnemyList[j].Tp = EnemyType.tpDead;
               else
               {
                  if ((EnemyList[j].Tp == EnemyType.tpFlatChibibo) || (NewX <= -Buffers.W) || (NewX < Buffers.XView - ForgetEnemiesAt * Buffers.W) ||
                           (NewX > Buffers.XView + Buffers.NH * Buffers.W + ForgetEnemiesAt * Buffers.W) || (EnemyList[j].YPos + EnemyList[j].YVel > Buffers.NV * Buffers.H))
                  {
                     switch (EnemyList[j].Tp)
                     {
                        case EnemyType.tpChibibo:
                           Buffers.WorldMap[EnemyList[j].MapX, EnemyList[j].MapY] = '';
                           break;
                        case EnemyType.tpVertFish:
                           Buffers.WorldMap[EnemyList[j].MapX, EnemyList[j].MapY - 2] = '';
                           break;
                        case EnemyType.tpVertFireBall:
                           Buffers.WorldMap[EnemyList[j].MapX, EnemyList[j].MapY - 2] = '';
                           break;
                        case EnemyType.tpVertPlant:
                           Buffers.WorldMap[EnemyList[j].MapX, EnemyList[j].MapY - 2] = (char)((int)('') + EnemyList[j].SubTp);
                           break;
                        case EnemyType.tpRed:
                           Buffers.WorldMap[EnemyList[j].MapX, EnemyList[j].MapY] = '';
                           break;
                        case EnemyType.tpKoopa:
                        case EnemyType.tpSleepingKoopa:
                        case EnemyType.tpWakingKoopa:
                        case EnemyType.tpRunningKoopa:
                           Buffers.WorldMap[EnemyList[j].MapX, EnemyList[j].MapY] = (char)((int)('') + EnemyList[j].SubTp);
                           break;
                        case EnemyType.tpBlockLift:
                           Buffers.WorldMap[EnemyList[j].MapX, EnemyList[j].MapY] = '°';
                           break;
                        case EnemyType.tpDonut:
                           Buffers.WorldMap[EnemyList[j].MapX, EnemyList[j].MapY] = '±';
                           break;
                     }
              
                     if ( EnemyList[j].Tp == EnemyType.tpKoopa )
                        EnemyList[j].Tp = EnemyType.tpDyingKoopa;
                     else
                     {
                        if ( EnemyList[j].Tp != EnemyType.tpFireBall )
                           EnemyList[j].Tp = EnemyType.tpDying;
                        else
                           EnemyList[j].Tp = EnemyType.tpDyingFireBall;
                     }
                     EnemyList[j].DelayCounter = -(FormMarioPort.MAX_PAGE + 1);
                  }
                  else
                  {
                     EnemyList[j].DelayCounter = 0;
                     OldXVel = EnemyList[j].XVel;

                     if ( EnemyList[j].Tp == EnemyType.tpVertFish || EnemyList[j].Tp == EnemyType.tpDeadVertFish || EnemyList[j].Tp == EnemyType.tpVertFireBall || EnemyList[j].Tp == EnemyType.tpDeadVertPlant )
                     {
                        if ((EnemyList[j].DirCounter % 3 == 0) && (EnemyList[j].YPos + Buffers.H < Buffers.NV * Buffers.H))
                          EnemyList[j].YVel++;
                     }

                     if ( EnemyList[j].Tp == EnemyType.tpDeadChibibo || EnemyList[j].Tp == EnemyType.tpDeadRed || EnemyList[j].Tp == EnemyType.tpDeadKoopa )
                     {
                        if ( EnemyList[i].XPos % 6 == 0 )
                          EnemyList[j].YVel++;
                     }
                     else
                        Check(j);
                     EnemyList[i].XPos = EnemyList[i].XPos + EnemyList[j].XVel;
                     EnemyList[i].YPos = EnemyList[i].YPos + EnemyList[j].YVel;
                     if (EnemyList[j].XVel == 0)
                     {
                        EnemyList[j].XVel = -OldXVel;
                        if ( EnemyList[j].Tp == EnemyType.tpDyingFireBall )
                          ShowFire(EnemyList[i].XPos, EnemyList[i].YPos);
                     }
                  }
                  EnemyList[j].LastXPos = EnemyList[i].XPos;
                  EnemyList[j].LastYPos = EnemyList[i].YPos;
               }
            }
            else
            {
               if ((EnemyList[j].XVel != 0) || (EnemyList[j].YVel != 0))
               {
                  EnemyList[i].XPos = EnemyList[j].LastXPos + (EnemyList[j].DelayCounter * EnemyList[j].XVel) / (EnemyList[j].MoveDelay + 1);
                  EnemyList[i].YPos = EnemyList[j].LastYPos + (EnemyList[j].DelayCounter * EnemyList[j].YVel) / (EnemyList[j].MoveDelay + 1);
               }
            }
         }

         for (i = 0; i < NumEnemies; i++)
         {
            j = (int)(ActiveEnemies[i]);

            if ( EnemyList[j].Tp == EnemyType.tpChibibo ||EnemyList[j].Tp == EnemyType.tpChamp ||EnemyList[j].Tp == EnemyType.tpLife ||EnemyList[j].Tp == EnemyType.tpFlower ||EnemyList[j].Tp == EnemyType.tpStar 
                  ||EnemyList[j].Tp == EnemyType.tpVertFish ||EnemyList[j].Tp == EnemyType.tpVertFireBall ||EnemyList[j].Tp == EnemyType.tpVertPlant
                  ||EnemyList[j].Tp == EnemyType.tpRed ||EnemyList[j].Tp == EnemyType.tpKoopa || EnemyList[j].Tp == EnemyType.tpSleepingKoopa 
                  || EnemyList[j].Tp == EnemyType.tpWakingKoopa || EnemyList[j].Tp == EnemyType.tpRunningKoopa 
                  ||EnemyList[j].Tp == EnemyType.tpLiftStart ||EnemyList[j].Tp == EnemyType.tpBlockLift ||EnemyList[j].Tp == EnemyType.tpDonut ||EnemyList[j].Tp == EnemyType.tpLiftEnd )
            {
               if ( PlayerX1 < EnemyList[i].XPos + Buffers.W )
               {
                  if ( PlayerX2 > EnemyList[i].XPos )
                  {
                     if ( PlayerY1 + PlayerYVel < EnemyList[i].YPos + Buffers.H )
                     {
                        if ( PlayerY2 + PlayerYVel > EnemyList[i].YPos )
                        {
                           if ( Star )
                           {
                              if ( !(EnemyList[j].Tp == EnemyType.tpLiftStart ||EnemyList[j].Tp == EnemyType.tpBlockLift ||EnemyList[j].Tp == EnemyType.tpDonut ||EnemyList[j].Tp == EnemyType.tpLiftEnd) )
                              {
                                 //Beep (800);
                                 Kill(j);
                                 cdHit = 1;
                              }
                           }

                           switch (EnemyList[j].Tp)
                           {
                              case EnemyType.tpSleepingKoopa:
                              case EnemyType.tpWakingKoopa:
                              {
                                 EnemyList[j].Tp = EnemyType.tpRunningKoopa;
                                 EnemyList[j].XVel = 5 * (2 * System.Convert.ToByte(EnemyList[i].XPos > PlayerX1) - 1);
                                 EnemyList[j].MoveDelay = 0;
                                 EnemyList[j].DelayCounter = 0;
                                 //Beep (800);
                                 cdEnemy = 1;
                                 Buffers.AddScore(100);
                                 break;
                              }
                              case EnemyType.tpChamp:
                              {
                                 if (EnemyList[j].SubTp == 0)
                                 {
                                    cdChamp = 0x1;
                                    Buffers.AddScore(1000);
                                 }
                                 else
                                 {
                                    cdHit = 1;
                                 }
                                 EnemyList[j].Tp = EnemyType.tpDying;
                                 EnemyList[j].DelayCounter = -(FormMarioPort.MAX_PAGE + 1);
                                 Glitter.CoinGlitter(EnemyList[i].XPos, EnemyList[i].YPos);
                                 break;
                              }
                              case EnemyType.tpLife:
                              {
                                 cdLife = 0x1;
                                 EnemyList[j].Tp = EnemyType.tpDying;
                                 EnemyList[j].DelayCounter = -(FormMarioPort.MAX_PAGE + 1);
                                 Glitter.CoinGlitter(EnemyList[i].XPos, EnemyList[i].YPos);
                                 Buffers.AddScore(1000);
                                 break;
                              }
                              case EnemyType.tpFlower:
                              {
                                 cdFlower = 0x1;
                                 EnemyList[j].Tp = EnemyType.tpDying;
                                 EnemyList[j].DelayCounter = -(FormMarioPort.MAX_PAGE + 1);
                                 Glitter.CoinGlitter(EnemyList[i].XPos, EnemyList[i].YPos);
                                 Buffers.AddScore(1000);
                                 break;
                              }
                              case EnemyType.tpStar:
                              {
                                 cdStar = 0x1;
                                 EnemyList[j].Tp = EnemyType.tpDying;
                                 EnemyList[j].DelayCounter = -(FormMarioPort.MAX_PAGE + 1);
                                 Glitter.CoinGlitter(EnemyList[i].XPos, EnemyList[i].YPos);
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
                                 if (((PlayerYVel > EnemyList[j].YVel) || (PlayerYVel > 0)) && (PlayerY2 <= EnemyList[i].YPos + Buffers.H))
                                 {
                                    switch (EnemyList[j].Tp)
                                    {
                                       case EnemyType.tpChibibo:
                                       {
                                          EnemyList[j].Tp = EnemyType.tpFlatChibibo;
                                          EnemyList[j].XVel = 0;
                                          EnemyList[j].DelayCounter = -2 - 15 * System.Convert.ToByte(EnemyList[j].YVel == 0);
                                          //Beep (800);
                                          cdEnemy = 1;
                                          Buffers.AddScore(100);
                                          break;
                                       }
                                       case EnemyType.tpVertFish:
                                       {
                                          if ( EnemyList[i].YPos + Buffers.H < Buffers.NV * Buffers.H )
                                          {
                                             Kill(j);
                                             //Beep (800);
                                             cdEnemy = 1;
                                          }
                                          break;
                                       }
                                       case EnemyType.tpKoopa:
                                       case EnemyType.tpRunningKoopa:
                                       {
                                           EnemyList[j].Tp = EnemyType.tpSleepingKoopa;
                                           EnemyList[j].XVel = 0;
                                           EnemyList[j].Counter = 0;
                                           //Beep (800);
                                           cdEnemy = 1;
                                           Buffers.AddScore(100);
                                           break;
                                       }
                                       case EnemyType.tpLiftStart:
                                       //case EnemyType.tpBlockLift:
                                       case EnemyType.tpDonut:
                                       case EnemyType.tpLiftEnd:
                                       {
                                          if ( EnemyList[j].Tp == EnemyType.tpDonut )
                                          {
                                             EnemyList[j].Status = 2;
                                             if ((EnemyList[j].Counter > 20) && (EnemyList[j].YVel == 0))
                                                EnemyList[j].YVel++;
                                          }
                                          cdStopJump = System.Convert.ToByte(PlayerYVel != 2);
                                          cdLift = 1;
                                          PlayerY1 = EnemyList[i].YPos - 2 * Buffers.H;
                                          PlayerY2 = EnemyList[i].YPos - 1;
                                          PlayerXVel = EnemyList[j].XVel;
                                          if (EnemyList[j].MoveDelay != 0)
                                             PlayerXVel = EnemyList[j].XVel * EnemyList[i].XPos % 2;
                                          PlayerYVel = EnemyList[j].YVel;
                                          break;
                                       }
                                    }
                                 }
                                 else
                                 {

                                    if (!((EnemyList[j].Tp == EnemyType.tpVertFish) && (!(Math.Abs(EnemyList[j].DelayCounter - EnemyList[j].MoveDelay) <= 1)) ||
                                              (EnemyList[j].Tp == EnemyType.tpLiftStart || EnemyList[j].Tp == EnemyType.tpBlockLift || EnemyList[j].Tp == EnemyType.tpDonut || EnemyList[j].Tp == EnemyType.tpLiftEnd)))
                                    {
                                       cdHit = 1;
                                       if ( Star )
                                          Kill(j);
                                    }
                                 }
                                 break;
                              }
                           }
                        }
                     }
                  }
               }
            }
         } // end for
         
         i = 1;
         while ( i <= ActiveEnemies.Length )
         {
            if ( EnemyList[(int)(ActiveEnemies[i])].Tp == EnemyType.tpDead )
               //Delete(ActiveEnemies, i, 1);
               ActiveEnemies.Remove(i, 1);
            else
               i++;
         }
        
         NumEnemies = (byte)ActiveEnemies.Length;
      }
      
      //-------------------------------------------------
      // 
      //-------------------------------------------------
      public static void StartEnemies (int X, short Dir)
      {
         int i;
         bool Remove;

         if ( (X < 0) || (X > Buffers.Options.XSize) )
            return;
            
         for ( i = 0; i < Buffers.NV; i++)
         {
            Remove = true;
            Random random = new Random();
            switch ((char)Buffers.WorldMap[X, i])
            {
               case '€': NewEnemy(EnemyType.tpChibibo, 0, X, i, 1 * Dir, 0, 2); break;
               case '§': NewEnemy(EnemyType.tpVertFish, 0, X, (i + 2), 0, 0, 50 + random.Next(100)); break;
               case '‚': NewEnemy(EnemyType.tpVertFireBall, 0, X, (i + 2), 0, 0, 50 + random.Next (100)); break;
               case 'ƒ': NewEnemy(EnemyType.tpChibibo, 1, X, i, 1 * Dir, 0, 2); break;
               case '„':
               case '…':
               case '†': NewEnemy(EnemyType.tpVertPlant, (int)(Buffers.WorldMap[X, i]) - (int)(''), X, (i + 2), 0, 0, 20 + random.Next(50)); break;
               case '‡': NewEnemy(EnemyType.tpRed, 0, X, i, 1 * Dir, 0, 2); break;
               case 'ˆ':
               case '‰':
               case 'Š': NewEnemy(EnemyType.tpKoopa, (int)(Buffers.WorldMap[X, i]) - (int)(''), X, i, Dir, 0, 2); break;
               case '°': 
                  if ( Buffers.CanHoldYou((char)Buffers.WorldMap[X - 1, i]) || Buffers.CanHoldYou((char)Buffers.WorldMap[X + 1, i]) )
                     NewEnemy(EnemyType.tpBlockLift, 0, X, i, -Dir, 0, 0);
                  else
                     NewEnemy(EnemyType.tpBlockLift, 0, X, i, 0, -Dir, 0);
                   break;
               case '±': NewEnemy(EnemyType.tpDonut, 0, X, i, 0, 0, 0); break;
               default:
                  Remove = false;
                  break;
            }
            if (Remove)
               Buffers.WorldMap[X, i] = ' ';
         }
      }
      
      //-------------------------------------------------
      // 
      //-------------------------------------------------
      public static void HitAbove (int MapX, int MapY)
      {
         int i, j, X, Y;

         Y = MapY * Buffers.H;
         X = MapX * Buffers.W;

         for (i = 0; i < NumEnemies; i++)
         {
            j = (int)(ActiveEnemies[i]);

            if (EnemyList[j].YPos == Y)
            {
               if ((EnemyList[j].XPos + EnemyList[j].XVel + Buffers.W > X) && (EnemyList[j].XPos + EnemyList[j].XVel < X + Buffers.W))
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
                           if (((EnemyList[j].XVel > 0) && (EnemyList[j].XPos + EnemyList[j].XVel + Buffers.W / 2 <= X)) ||
                                 ((EnemyList[j].XVel < 0) && (EnemyList[j].XPos + EnemyList[j].XVel + Buffers.W / 2 >= X)))
                           {
                              EnemyList[j].XVel *= -1;
                              EnemyList[j].YVel = -7;
                              EnemyList[j].Status = Falling;
                              if (EnemyList[j].Tp == EnemyType.tpKoopa || EnemyList[j].Tp == EnemyType.tpSleepingKoopa ||
                                    EnemyList[j].Tp == EnemyType.tpWakingKoopa)
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
