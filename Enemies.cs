using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing;

/*
 * 
 */

namespace MarioPort
{

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

      public const int left = 0;
      public const int right = 1;
         
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
         
         public uint[] BackGrAddr = new uint[MAX_PAGE + 1];
      }

//          EnemyListPtr = ^EnemyList;
      private EnemyRec[] EnemyList = new EnemyRec[MaxEnemiesAtOnce];

//      Enemy: EnemyListPtr;
//      private EnemyRec* Enemy = &EnemyList;
      
//      ActiveEnemies: String [MaxEnemiesAtOnce];
      private string ActiveEnemies;
//      NumEnemies: (byte)absolute ActiveEnemies;
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
               enemyRec.XVel = -1 + 2 * (byte) ((enemyRec.XPos + enemyRec.XVel) % W > W / 2);
               enemyRec.YVel = -4;
               enemyRec.MoveDelay = 0;
               enemyRec.DelayCounter = 0;
               //AddScore(100);
               break;
            }
            case EnemyType.tpRed:
            {
              enemyRec.Tp = EnemyType.tpDeadRed;
              enemyRec.XVel = -1 + 2 * (byte) ((enemyRec.XPos + enemyRec.XVel) % W > W / 2);
              enemyRec.YVel = -4;
              enemyRec.MoveDelay = 0;
              enemyRec.DelayCounter = 0;
              //AddScore(100);
               break;
            }
            case EnemyType.tpKoopa:
            case EnemyType.tpSleepingKoopa:
            case EnemyType.tpWakingKoopa:
            case EnemyType.tpRunningKoopa:
            {
              enemyRec.Tp = EnemyType.tpDeadKoopa;
              enemyRec.XVel = -1 + 2 * (byte) ((enemyRec.XPos + enemyRec.XVel) % W > W / 2);
              enemyRec.YVel = -4;
              enemyRec.MoveDelay = 0;
              enemyRec.DelayCounter = 0;
              //AddScore(100);
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
              //AddScore(100);
               break;
            }
            case EnemyType.tpVertPlant:
            {
              enemyRec.Tp = EnemyType.tpDeadVertPlant;
              enemyRec.DelayCounter = 0;
              enemyRec.YVel = 0;
              //AddScore(100);
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
         if (X + W > XView) && (X < XView + SCREEN_WIDTH) )
            NewTempObj (tpHit, X, Y, 0, 0, W, H);
      }

      private void ShowFire (int X, int Y)
      {
         //Beep (50);
         X = X - 4;
         Y = Y - 4;
         if (X + W > XView) && (X < XView + SCREEN_WIDTH) )
            NewTempObj (tpFire, X, Y, 0, 0, W, H);
      }

      /*
      private void Mirror20x24 (P1, P2: Pointer)
      {
    
        const
          W = 20;
          H = 24;
        type
          PlaneBuffer = array[0..H - 1, 0..W / 4 - 1] of Byte;
          PlaneBufferArray = array[0..3] of PlaneBuffer;
          PlaneBufferArrayPtr = ^PlaneBufferArray;
        var
          Source, Dest: PlaneBufferArrayPtr;
      }
        
      private void Swap (byte Plane1, byte Plane2)
      {
             var
               i, j: Byte;
           {
             for j = 0 to H - 1 do
               for i = 0 to W / 4 - 1 do
               {
                 Dest^[Plane2, j, i] = Source^[Plane1, j, W / 4 - 1 - i];
                 Dest^[Plane1, j, i] = Source^[Plane2, j, W / 4 - 1 - i];
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
//      var
//        i, j: Integer;
//      {
//        if MemAvail < SizeOf (EnemyList) )
//        {
//          System.WriteLn ('Not enough memory');
//          Halt;
//        }
//        GetMem (Enemy, SizeOf (EnemyList));
//
//        Move (@Chibibo000^, EnemyPictures [1, Right], SizeOf (ImageBuffer));
//        Move (@Chibibo001^, EnemyPictures [2, Right], SizeOf (ImageBuffer));
//
//        Move (@Chibibo002^, EnemyPictures [4, Right], SizeOf (ImageBuffer));
//        Move (@Chibibo003^, EnemyPictures [5, Right], SizeOf (ImageBuffer));
//
//        Move (@Fish001^, EnemyPictures [3, Left], SizeOf (ImageBuffer));
//        Mirror (@EnemyPictures [3, Left], @EnemyPictures [3, Right]);
//
//        Move (@Red000^, EnemyPictures [6, Left], SizeOf (ImageBuffer));
//        Move (@Red001^, EnemyPictures [7, Left], SizeOf (ImageBuffer));
//
//        Move (@GrKp000^, EnemyPictures [8, Right], SizeOf (ImageBuffer));
//        Move (@GrKp001^, EnemyPictures [9, Right], SizeOf (ImageBuffer));
//
//        Move (@RdKp000^, EnemyPictures [10, Right], SizeOf (ImageBuffer));
//        Move (@RdKp001^, EnemyPictures [11, Right], SizeOf (ImageBuffer));
//
//        for i = 1 to MaxEnemies do
//          if (i in [6, 7]) )
//            Mirror (@EnemyPictures [i, Left], @EnemyPictures [i, Right])
//          else
//            if !(i in [3]) )
//              Mirror (@EnemyPictures [i, Right], @EnemyPictures [i, Left]);
//
//        for i = 0 to 1 do
//          for j = kGreen to kRed do
//            Mirror20x24 (@KoopaList [Left, j, i]^, @KoopaList [Right, j, i]^);
//      }
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
                  WorldMap[MapX, MapY] = '';
                  break;
               case EnemyType.tpVertFish:
                  WorldMap[MapX, MapY - 2] = '';
                  break;
               case EnemyType.tpVertFireBall:
                  WorldMap[MapX, MapY - 2] = '';
               case EnemyType.tpVertPlant:
                  WorldMap[MapX, MapY - 2] = (char) ((int)('') + SubTp);
               case EnemyType.tpRed:
                  WorldMap[MapX, MapY] = '';
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
               default:
//                  throw new Exception();
                  break;
            }
         }

         ClearEnemies();
      }

      public void NewEnemy (EnemyType InitType, int SubType, int InitX, int InitY, int InitXVel, int InitYVel, int InitDelay)
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
         EnemyList[i].XPos = EnemyList[i].MapX * W;
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
         Pointer Fig;

         Page = CurrentPage;
//         for i = 1 to NumEnemies do
         for (i = 0; i < NumEnemies; i++)
         {
            j = (int)(ActiveEnemies[i]);
//            with EnemyList[j] do
            if ( (XPos + 1 * W < XView) || (XPos > XView + SCREEN_WIDTH + 0 * W) || (YPos >= YView + SCREEN_HEIGHT) )
               BackGrAddr [Page] = $FFFF
            else
            {
               if Tp in [tpFireBall, tpDyingFireBall] )
                  BackGrAddr [Page] = PushBackGr (XPos, YPos, W, H / 2)
               else
                  if Tp in [tpVertPlant, tpDeadVertPlant] )
                     BackGrAddr [Page] = PushBackGr (XPos, YPos, 24, 20)
                  else
                     if Tp in [tpKoopa..tpDeadKoopa] )
                        BackGrAddr [Page] = PushBackGr (XPos, YPos - 10, 24, 24)
                     else
                        BackGrAddr [Page] = PushBackGr (XPos, YPos, W + 4, H);
            }
//            case Tp of
            switch (Tp)
            {
               case tpChibibo:
                  DrawImage (XPos, YPos, W, H, EnemyPictures [1 + 3 * SubTp, (byte)(DirCounter % 32 < 16)]);
               case tpFlatChibibo:
                  DrawImage (XPos, YPos, W, H, EnemyPictures [2 + 3 * SubTp, (byte)(DirCounter % 32 < 16)]);
               case tpDeadChibibo:
                  UpSideDown (XPos, YPos, W, H, EnemyPictures [1, Left]);
               case tpRisingChamp:
                  if ( YPos != (MapY * H) )
                     if ( SubTp = 0 )
                        DrawPart (XPos, YPos, W, H, 0, H - YPos % H - 1, @Champ000^)
                     else
                        DrawPart (XPos, YPos, W, H, 0, H - YPos % H - 1, @Poison000^);
               case tpChamp:
                  if ( SubTp == 0 )
                     DrawImage (XPos, YPos, W, H, @Champ000^)
                  else
                     DrawImage (XPos, YPos, W, H, @Poison000^);
               case tpRisingLife:
                  if ( YPos != (MapY * H) )
                     DrawPart (XPos, YPos, W, H, 0, H - YPos % H - 1, @Life000^);
               case tpLife:
                	DrawImage (XPos, YPos, W, H, @Life000^);
               case tpRisingFlower:
                  if ( YPos != (MapY * H) )
                     DrawPart (XPos, YPos, W, H, 0, H - YPos % H - 1, @Flower000^);
               case tpFlower:
                	DrawImage (XPos, YPos, W, H, @Flower000^);
               case tpRisingStar:
                  if ( YPos != (MapY * H) )
                     DrawPart (XPos, YPos, W, H, 0, H - YPos % H - 1, @Star000^);
               case tpStar:
                	DrawImage (XPos, YPos, W, H, @Star000^);
               case tpFireBall:
                  if ( XPos % 4 < 2 )
                     DrawImage (XPos, YPos, 12, H / 2, @Fire000^)
                  else
                     DrawImage (XPos, YPos, 12, H / 2, @Fire001^);
               case tpVertFish:
                  if ( (YVel != 0) || (YPos < NV * H - H) )
                     DrawImage (XPos, YPos, W, H, EnemyPictures [3, (byte)(PlayerX1 > XPos)]);
               case tpDeadVertFish:
                  if ( (YPos < NV * H - H) || (YVel != 0) )
                     UpSideDown (XPos, YPos, W, H, EnemyPictures [3, (byte)(PlayerX1 <= XPos)]);
               case tpVertFireBall:
               {
                  if ( Math.Abs(DelayCounter - MoveDelay) <= 1 )
                  {
                     DrawImage (XPos, YPos, W, H, FireBallList [Random (4)]^);
                     NewGlitter (XPos + Random (W), YPos + Random (H), 57 + Random (7), 14 + Random (20));
                     NewStar (XPos + Random (W), YPos + Random (H), 57 + Random (7), 14 + Random (20));
                  }
               }
               case tpVertPlant:
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
                           Fig = @PPlant000
                     }
                  }
                  else
                  {
                     case SubTp of
                        case 0:
                        case 1:
                           Fig = @PPlant003;
                           break;
                        default:
                           Fig = @PPlant001;
                  }
                  DrawPart (XPos, YPos, 24, 20, 0, (MapY * H) - YPos - 1, Fig);
                }
               case tpDeadVertPlant:
               {
                  DelayCounter = 0;
                  MoveDelay = 0;
                  YVel = 0;
                  Status++;
                  if ( Status < 12 )
                     DrawImage (XPos, YPos, 24, 20, @Hit000^)
                  else
                     if ( Status > 14 )
                        Tp = tpDying;
               }
               case tpRed:
                  DrawImage (XPos, YPos, W, H, EnemyPictures [6 + (byte)(DirCounter % 16 <= 8), (byte)(XVel > 0)]);
               case tpDeadRed:
                  UpSideDown (XPos, YPos, W, H, EnemyPictures [6 + (byte)(DirCounter % 16 <= 8), (byte)(XVel > 0)]);
               case tpKoopa:
                  DrawImage (XPos, YPos - 10, W, 24, KoopaList [(byte)(XVel > 0), SubTp, (byte)(DirCounter % 16 <= 8)]^);
               case tpWakingKoopa, tpRunningKoopa:
                  DrawImage (XPos, YPos, W, H, EnemyPictures [8 + 2 * SubTp + 1 - (byte)(DirCounter % 16 <= 8), (byte)(DirCounter % 32 <= 16)]);
               case tpSleepingKoopa:
                  DrawImage (XPos, YPos, W, H,  EnemyPictures [8 + 2 * SubTp, 0]);
               case tpDeadKoopa:
                  UpSideDown (XPos, YPos, W, H, EnemyPictures [8 + 2 * SubTp, (byte)(DirCounter % 16 <= 8)]);
               case tpBlockLift:
                  DrawImage (XPos, YPos, W, H, @Lift1000^);
               case tpDonut:
               {
                  if Status = 0 )
                  {
                     DrawImage (XPos, YPos, W, H, @Donut000^);
                     if YVel = 0 )
                        Counter = 0;
                  }
                  else
                  {
                     DrawImage (XPos, YPos, W, H, @Donut001^);
                     Dec (Status);
                  }
                  if YVel > 0 )
                     if Counter % 24 = 0 )
                        Inc (YVel);
                  Inc (Counter);
               }
            }
         }
      }
      
      public void HideEnemies()
      {
         int i, j, Page;
         Page = CurrentPage;
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
            case tpRisingChamp:
            case tpRisingLife:
            case tpRisingFlower:
            case tpRisingStar:
            {
               if ((YPos / H) = (YPos / H)) && (YPos != MapY * H) )
               {
                  XVel = 1 - 2 * (byte)(WorldMap[MapX + 1, MapY - 1] in CanHoldYou);
//                  case Tp of
                  switch (Tp)
                  {
                     case tpRisingChamp:
                        Tp = tpChamp;
                     case tpRisingLife:
                     {
                        Tp = tpLife;
                        XVel = 2 * XVel;
                     }
                     case tpRisingFlower:
                     {
                        XVel = 0;
                        Tp = tpFlower;
                     }
                     case tpRisingStar:
                     {
                        Tp = tpStar;
                        XVel = 2 * XVel;
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
            }
            case tpFireBall:
            {
               AtX = (XPos + W / 4) / W;
               NewX = (XPos + W / 4 + XVel) / W;
               if (AtX != NewX) || (PlayerX1 % W = 0) )
               {
                  Y1 = (YPos + H / 4 + HSafe) / H - Safe;
                  NewCh1 = WorldMap[NewX, Y1];
                  if ( NewCh1 in CanHoldYou )
                     XVel = 0;
               }
               NewX = XPos;
               NewY = YPos;
               AtX = (XPos + W / 4 + XVel) / W;
               NewY = (YPos + 2 + H / 4 + YVel + HSafe) / H - Safe;
               NewCh1 = WorldMap[AtX, NewY];
               if (YVel > 0) && (NewCh1 in CanHoldYou + CanStandOn) )
               {
                  YPos = ((YPos + YVel - 5 + HSafe) / H - Safe) * H;
                  YVel = -2;
               }
               else
                  if XPos % 3 = 0 )
                     Inc (YVel);
               if ( (XVel == 0) || (NewX < XView - W) || (NewX > XView + NH * W + W) || (NewY > NV * H) )
               {
                  DelayCounter = - (MAX_PAGE + 1);
                  Tp = tpDyingFireBall;
               }
//                Exit;
               return;
            }
            case tpStar:
               StartGlitter(XPos, YPos, W, H);
         }


         if ( !(Tp in [tpVertFish, tpDeadVertFish, tpVertFireBall, tpVertPlant, tpDeadVertPlant]) )
         {
            Side = Integer (XVel > 0) * (W - 1);
            AtX = (XPos + Side) / W;
            NewX = (XPos + Side + XVel) / W;
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
                  if ( Tp == tpRunningKoopa )
                  {
                     ShowStar (XPos + XVel, YPos);
                     l = (YPos + HSafe + H / 2) / H - Safe;
                     Ch = WorldMap[NewX, l];
                     if ( (XPos >= XView) && (XPos + W <= XView + NH * W) )
                     {
//                        case Ch of
                        switch (Ch)
                        {
                           case 'J': BreakBlock (NewX, l);
                           case '?':
                           {
//                              case WorldMap[NewX, l - 1] of
                              switch (WorldMap[NewX, l - 1])
                              {
                                 case ' ': HitCoin (NewX * W, l * H, TRUE);
                                 case 'à':
                                 {
                                    if Data.%e[Player] in [mdSmall] )
                                       NewEnemy (tpRisingChamp, 0, NewX, l, 0, -1, 1)
                                    else
                                       NewEnemy (tpRisingFlower, 0, NewX, l, 0, -1, 1);
                                 }
                                 case 'á': NewEnemy (tpRisingLife, 0, NewX, l, 0, -1, 2);
                              }
                           }
                           Remove (NewX * W, l * H, W, H, 1);
                           WorldMap[NewX, l] = '@';
                        }
                     }
                  }
               }
               XVel = 0;
            }

            AtX = (XPos + XVel) / W;
            NewX = (XPos + XVel + W - 1) / W;
            NewY = (YPos + 1 + H + YVel + HSafe) / H - Safe;

            NewCh1 = WorldMap[AtX, NewY];
            NewCh2 = WorldMap[NewX, NewY];
            Hold1 = (NewCh1 in CanHoldYou + CanStandOn);
            Hold2 = (NewCh2 in CanHoldYou + CanStandOn);

            if ( Tp in [tpLiftStart..tpLiftEnd] )
            {
               if ( (YVel != 0) && (!(Tp == tpDonut))  )
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
                     if !(Hold1 || Hold2) )
                     {
                        Status = Falling;
                        YVel = 1;
                     }
                     if (SubTp = 1) && (Tp in [tpKoopa]) )
                     {
                        if (XVel > 0) && (XPos % W in [11..19]) )
                           if (!Hold2) && Hold1 ) XVel = 0;
                        if (XVel < 0) && (XPos % W in [1..9]) )
                           if (!Hold1) && Hold2 ) XVel = 0;
                     }
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
                  }
               }
            }
         }

         NewX1 = XPos + XVel;
         NewX2 = NewX1 + W - 1 + 4 * (byte)(Tp in [tpVertPlant]);
         Y1 = YPos + YVel;
         Y2 = Y1 + H - 1;

         if (Tp in [tpChibibo, tpFlatChibibo, tpVertFish, tpVertPlant, tpDeadVertPlant, tpRed, tpKoopa..tpRunningKoopa]) )
         {
//            for k = 1 to NumEnemies do
            for (k = 0; k < NumEnemies; k++)
            {
               j = (int)(ActiveEnemies[k]);
               if ( j != i )
               {
                  if ( (EnemyList[j].Tp in [tpChibibo, tpFlatChibibo, tpRed, tpKoopa..tpRunningKoopa]) )
                  {
//                     with EnemyList[j] do
                     X = XPos + XVel;
                     Y = YPos + YVel;
                     
                     if ( NewX1 < X + W )
                        if ( (NewX2 > X) )
                           if ( (Y1 < Y + H) )
                              if ( (Y2 > Y) )
                                 if ( EnemyList[j].Tp = tpRunningKoopa )
                                 {
                                    ShowStar (XPos, YPos);
                                    if Tp = tpRunningKoopa )
                                    {
                                       ShowStar (EnemyList[j].XPos, EnemyList[j].YPos);
                                       Kill(j);
                                    }
                                    Kill(i);
                                 }
                                 else
                                    if Tp != tpRunningKoopa )
                                    {
                                       XVel = - XVel;
                                       EnemyList[j].XVel = - EnemyList[j].XVel;
                                       YVel = - YVel;
                                       EnemyList[j].YVel = - EnemyList[j].YVel;
                                       if Math.Abs(X - NewX1) < W )
                                         if X > NewX1 )
                                         {
                                           XPos = XPos - XVel;
                                           XVel = -Math.Abs(XVel);
                                         }
                                         else
                                           if X < NewX1 )
                                           {
                                             XPos = XPos - XVel;
                                             XVel = Math.Abs(XVel);
                                           }
                                    }
                  }
                  else
                  {
                     if (EnemyList[j].Tp = tpFireBall) )
                     {
//                        with EnemyList[j] do
//                        {
                        X = XPos + XVel;
                        Y = YPos + YVel;
//                        }
                        if ( (NewX1 <= X + W / 2) )
                        {
                           if ( (NewX2 >= X) )
                              if ( (Y1 <= Y + H / 2) )
                                 if ( Y2 >= Y )
                                 {
                                    EnemyList[j].Tp = tpDyingFireBall;
                                    EnemyList[j].DelayCounter = - (MAX_PAGE + 1);
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

         Page = CurrentPage;
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
//               if ( Tp in [tpVertFish, tpVertFireBall, tpVertPlant] )
               if ( Tp == tpVertFish || Tp == tpVertFireBall || Tp == tpVertPlant )
               {
                  if ( Tp == tpVertPlant )
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
                                 if ( (XPos > PlayerX2 + W) || (XPos + 24 + W < PlayerX1) )
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
                        }
                        case 2:
                        {
                           Counter++;
                           if ( Counter > 200 )
                              Status++;
                           MoveDelay = 0;
                           DelayCounter = 0;
                        }
                        case 3:
                        {
                           YVel = 1;
                           DelayCounter = 0;
                           MoveDelay = 2;
                           if ( YPos > (MapY * H) )
                              Status++;
                        }
                        case 4:
                        {
                           YVel = 0;
                           MoveDelay = 100 + Random (100);
                           DelayCounter = 0;
                           Status = 0;
                        }
                     }
                  }
                  else
                  {
                     if ( YPos + H >= NV * H )
                     {
                        if YVel > 0 )
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
                        if ( Tp == tpVertFireBall )
                        {
                           Beep (100);
                           YVel = -9;
                        }
                     }
                  }
               }
               if ( Tp == tpSleepingKoopa )
               {
                  Counter++;
                  if ( Counter > 150 )
                  {
                     Tp = tpWakingKoopa;
                     XVel = 1;
                     Counter = 0;
                  }
               }
               if ( Tp == tpWakingKoopa )
               {
                  XVel = - XVel;
                  MoveDelay = 1;
                  DelayCounter = 0;
                  Inc (Counter);
                  if ( Counter > 50 )
                  {
                     Tp = tpKoopa;
                     if ( PlayerX1 > XPos )
                        XVel = 1
                     else
                        XVel = -1;
                  }
               }
//               if ( Tp in [tpDying, tpDyingFireBall, tpDyingKoopa] )
               if ( Tp == tpDying || Tp == tpDyingFireBall || Tp == tpDyingKoopa )
                  Tp = tpDead
               else
               {
                  if ( (Tp == tpFlatChibibo) || (NewX <= -W) || (NewX < XView - ForgetEnemiesAt * W) ||
                           (NewX > XView + NH * W + ForgetEnemiesAt * W) || (YPos + YVel > NV * H) )
                  {
//                     case Tp of
                     switch (Tp)
                     {
                        case tpChibibo:
                           WorldMap[MapX, MapY] = '';
                           break;
                        case tpVertFish:
                           WorldMap[MapX, MapY - 2] = '';
                           break;
                        case tpVertFireBall:
                           WorldMap[MapX, MapY - 2] = '';
                           break;
                        case tpVertPlant:
                           WorldMap[MapX, MapY - 2] = (char) ((int)('') + SubTp);
                           break;
                        case tpRed:
                           WorldMap[MapX, MapY] = '';
                           break;
                        case tpKoopa..tpRunningKoopa:
                           WorldMap[MapX, MapY] = (char) ((int)('') + SubTp);
                           break;
                        case tpBlockLift:
                           WorldMap[MapX, MapY] = '°';
                           break;
                        case tpDonut:
                           WorldMap[MapX, MapY] = '±';
                           break;
                     }
              
                     if ( Tp == tpKoopa )
                        Tp = tpDyingKoopa
                     else
                        if ( Tp != tpFireBall )
                           Tp = tpDying
                        else
                           Tp = tpDyingFireBall;
                     DelayCounter = -(MAX_PAGE + 1);
                  }
                  else
                  {
                     DelayCounter = 0;
                     OldXVel = XVel;
                     //{ OldYVel = YVel; }
//                     if ( Tp in [tpVertFish, tpDeadVertFish, tpVertFireBall, tpDeadVertPlant] )
                     if ( Tp == tpVertFish || Tp == tpDeadVertFish || Tp == tpVertFireBall || Tp == tpDeadVertPlant )
                     {
                        if (DirCounter % 3 = 0) && (YPos + H < NV * H) )
                          Inc (YVel);
                     }
//                     if Tp in [tpDeadChibibo, tpDeadRed, tpDeadKoopa] )
                     if ( Tp == tpDeadChibibo || Tp == tpDeadRed || Tp == tpDeadKoopa )
                     {
                        if XPos % 6 = 0 )
                          Inc (YVel);
                     }
                     else
                        Check(j);
                     XPos = XPos + XVel;
                     YPos = YPos + YVel;
                     if ( XVel == 0 )
                     {
                        XVel = -OldXVel;
                        if ( Tp == tpDyingFireBall )
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
            if ( tp in [tpChibibo, tpChamp, tpLife, tpFlower, tpStar, tpVertFish, tpVertFireBall, tpVertPlant, tpRed, tpKoopa..tpRunningKoopa,
                        tpLiftStart..tpLiftEnd] )
            {
               if ( PlayerX1 < XPos + W )
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
                              case tpSleepingKoopa:
                              case tpWakingKoopa:
                              {
                                 Tp = tpRunningKoopa;
                                 XVel = 5 * (2 * (byte)(XPos > PlayerX1) - 1);
                                 MoveDelay = 0;
                                 DelayCounter = 0;
                                 Beep (800);
                                 cdEnemy = 1;
                                 AddScore(100);
                              }
                              case tpChamp:
                              {
                                 if ( SubTp == 0 )
                                 {
                                    cdChamp = $1;
                                    AddScore(1000);
                                 }
                                 else
                                 {
                                    cdHit = 1;
                                 }
                                 Tp = tpDying;
                                 DelayCounter = -(MAX_PAGE + 1);
                                 CoinGlitter(XPos, YPos);
                              }
                              case tpLife:
                              {
                                 cdLife = $1;
                                 Tp = tpDying;
                                 DelayCounter = - (MAX_PAGE + 1);
                                 CoinGlitter(XPos, YPos);
                                 AddScore(1000);
                              }
                              case tpFlower:
                              {
                                 cdFlower = $1;
                                 Tp = tpDying;
                                 DelayCounter = - (MAX_PAGE + 1);
                                 CoinGlitter(XPos, YPos);
                                 AddScore(1000);
                              }
                              case tpStar:
                              {
                                 cdStar = $1;
                                 Tp = tpDying;
                                 DelayCounter = - (MAX_PAGE + 1);
                                 CoinGlitter(XPos, YPos);
                                 AddScore(1000);
                              }
                              case tpVertFireBall:
                              {
                                 cdHit = 1;
                              }

                              default:
                              {
                                 if ( ((PlayerYVel > YVel) || (PlayerYVel > 0)) && (PlayerY2 <= YPos + H) )
                                 {
   //                                 case Tp of
                                    switch (Tp)
                                    {
                                       tpChibibo:
                                       {
                                          Tp = tpFlatChibibo;
                                          XVel = 0;
                                          DelayCounter = - 2 - 15 * (byte)(YVel = 0);
                                          Beep (800);
                                          cdEnemy = 1;
                                          AddScore(100);
                                       }
                                       tpVertFish:
                                       {
                                          if (YPos + H < NV * H) )
                                          {
                                             Kill(j);
                                             Beep (800);
                                             cdEnemy = 1;
                                          }
                                       }
                                       tpKoopa, tpRunningKoopa:
                                       {
                                           Tp = tpSleepingKoopa;
                                           XVel = 0;
                                           Counter = 0;
                                           Beep (800);
                                           cdEnemy = 1;
                                           AddScore(100);
                                       }
                                       tpLiftStart..tpLiftEnd:
                                       {
                                          if Tp = tpDonut )
                                          {
                                             Status = 2;
                                             if (Counter > 20) && (YVel = 0) )
                                               Inc (YVel);
                                          }
                                          cdStopJump = (byte)(PlayerYVel != 2);
                                          cdLift = 1;
                                          PlayerY1 = YPos - 2 * H;
                                          PlayerY2 = YPos - 1;
                                          PlayerXVel = XVel;
                                          if MoveDelay != 0 )
                                             PlayerXVel = XVel * XPos % 2;
                                          PlayerYVel = YVel;
                                       }
                                    }
                                 }
                                 else
                                 {

                                    if (!((Tp == tpVertFish) && (!(Math.Abs(DelayCounter - MoveDelay) <= 1)) || (Tp in [tpLiftStart..tpLiftEnd])))
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
            if ( EnemyList[(int)(ActiveEnemies[i])].Tp == tpDead )
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
               case '': NewEnemy (tpChibibo, 0, X, i, 1 * Dir, 0, 2); break;
               case '': NewEnemy (tpVertFish, 0, X, (i + 2), 0, 0, 50 + Random (100)); break;
               case '': NewEnemy (tpVertFireBall, 0, X, (i + 2), 0, 0, 50 + Random (100)); break;
               case '': NewEnemy (tpChibibo, 1, X, i, 1 * Dir, 0, 2);
               case '':
//               case ' ':
               case '': NewEnemy (tpVertPlant, (int)(WorldMap[X, i]) - (int)(''), X, (i + 2), 0, 0, 20 + Random (50)); break;
               case '': NewEnemy (tpRed, 0, X, i, 1 * Dir, 0, 2); break;
               case '':
               case '':
               case '': NewEnemy (tpKoopa, (int)(WorldMap[X, i]) - (int)(''), X, i, Dir, 0, 2); break;
               case '°': 
                  if (WorldMap[X - 1, i] in CanHoldYou) || (WorldMap[X + 1, i] in CanHoldYou)
                     NewEnemy (tpBlockLift, 0, X, i, -Dir, 0, 0);
                  else
                     NewEnemy (tpBlockLift, 0, X, i, 0, -Dir, 0);
                   break;
               case '±': NewEnemy (tpDonut, 0, X, i, 0, 0, 0); break;
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
         X = MapX * W;
         
         for ( i = 0; i < NumEnemies; i++)
         {
            j = (int)(ActiveEnemies[i]);
            
            if (EnemyList[j].YPos == Y)
            {
               if ( (EnemyList[j].XPos + EnemyList[j].XVel + W > X) && (EnemyList[j].XPos + EnemyList[j].XVel < X + W) )
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
                        if ( ((EnemyList[j].XVel > 0) && (EnemyList[j].XPos + EnemyList[j].XVel + W / 2 <= X)) ||
                              ((EnemyList[j].XVel < 0) && (EnemyList[j].XPos + EnemyList[j].XVel + W / 2 >= X)) )
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