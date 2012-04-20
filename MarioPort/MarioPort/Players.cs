
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
   // Class that controls the players
   //-------------------------------------------------
   public static class Players
   {
      public const int stOnTheGround = 0;
      public const int stJumping     = 1;
      public const int stFalling     = 2;

      public const int SCROLL_AT     = 112;

      public const int JumpVel       = 4;
      public const int JumpDelay     = 6;
      public const int MaxYVel       = JumpVel * 2;
      public const int Slip          = 6;
      public const int BlinkTime     = 125;
      public const int StarTime      = 750;
      public const int GrowTime      = 24;

      public const int MAX_SPEED     = 2;

      public static bool Blinking;
      public static bool Growing;
      public static bool InPipe;
      public static char[] PipeCode = new char[2];
      public static int MapX;
      public static int MapY;
      public static bool EarthQuake;
      public static int EarthQuakeCounter;
      public static int Small;
      
      private const int Safe = Buffers.EY1;
      private const int HSafe = Buffers.H * Safe;

      private static bool keyLeft;
      private static bool keyRight;
      private static bool keyUp;
      private static bool keyDown;
      private static bool keyAlt;
      private static bool keyCtrl;
      private static bool keyLeftShift;
      private static bool keyRightShift;
      private static bool keySpace;

      //-------------------------------------------------
      // Screen rectangle
      //-------------------------------------------------
      public class ScreenRec
      {
         public bool Visible;
         public int XPos;
         public int YPos;
         public uint BackGrAddr;

         public ScreenRec()
         {
            Visible = false;
            XPos = 0;
            YPos = 0;
            BackGrAddr = 0;
         }

         public static ScreenRec Create()
         {
            return new ScreenRec();
         }
      }

      public static ScreenRec screenRec = ScreenRec.Create();
      private static ScreenRec[] SaveScreen = new ScreenRec[] { ScreenRec.Create(), ScreenRec.Create() };

      private static int X;
      private static int Y;
      private static int OldX;
      private static int OldY;
      private static int DemoX;
      private static int DemoY;
      private static int DemoCounter1;
      private static int DemoCounter2;
      private static int XVel;
      private static int YVel;

      private static byte Direction;
      private static byte Status;
      private static byte Walkingmode;
      private static byte Counter;
      private static byte WalkCount;

      private static bool HighJump;
      private static bool HitEnemy;
      private static bool Jumped;
      private static bool Fired;

      private static int FireCounter;
      private static int StarCounter;
      private static int GrowCounter;
      private static int BlinkCounter;

      private static char AtCh1;
      private static char AtCh2;
      private static char Below1;
      private static char Below2;
          
      //-------------------------------------------------
      // Mirror bitmap from
      //-------------------------------------------------
      private static void HighMirror(Bitmap from, ref Bitmap to)
      {
         from.RotateFlip(RotateFlipType.Rotate180FlipY);
         to = from.Clone() as Bitmap;
      }
      
      //-------------------------------------------------
      // Initialize Player Figures
      //-------------------------------------------------
      public static void InitPlayerFigures()
      {
         Buffers.Pictures[Buffers.plMario, Buffers.mdSmall, 0, Buffers.dirLeft] = Resources.SWMAR_000;
         Buffers.Pictures[Buffers.plMario, Buffers.mdSmall, 1, Buffers.dirLeft] = Resources.SWMAR_001;
         Buffers.Pictures[Buffers.plMario, Buffers.mdSmall, 2, Buffers.dirLeft] = Resources.SJMAR_000;
         Buffers.Pictures[Buffers.plMario, Buffers.mdSmall, 3, Buffers.dirLeft] = Resources.SJMAR_001;

         Buffers.Pictures[Buffers.plMario, Buffers.mdLarge, 0, Buffers.dirLeft] = Resources.LWMAR_000;
         Buffers.Pictures[Buffers.plMario, Buffers.mdLarge, 1, Buffers.dirLeft] = Resources.LWMAR_001;
         Buffers.Pictures[Buffers.plMario, Buffers.mdLarge, 2, Buffers.dirLeft] = Resources.LJMAR_000;
         Buffers.Pictures[Buffers.plMario, Buffers.mdLarge, 3, Buffers.dirLeft] = Resources.LJMAR_001;
 
         Buffers.Pictures[Buffers.plMario, Buffers.mdFire, 0, Buffers.dirLeft] = Resources.FWMAR_000;
         Buffers.Pictures[Buffers.plMario, Buffers.mdFire, 1, Buffers.dirLeft] = Resources.FWMAR_001;
         Buffers.Pictures[Buffers.plMario, Buffers.mdFire, 2, Buffers.dirLeft] = Resources.FJMAR_000;
         Buffers.Pictures[Buffers.plMario, Buffers.mdFire, 3, Buffers.dirLeft] = Resources.FJMAR_001;

         Buffers.Pictures[Buffers.plLuigi, Buffers.mdSmall, 0, Buffers.dirLeft] = Resources.SWLUI_000;
         Buffers.Pictures[Buffers.plLuigi, Buffers.mdSmall, 1, Buffers.dirLeft] = Resources.SWLUI_001;
         Buffers.Pictures[Buffers.plLuigi, Buffers.mdSmall, 2, Buffers.dirLeft] = Resources.SJLUI_000;
         Buffers.Pictures[Buffers.plLuigi, Buffers.mdSmall, 3, Buffers.dirLeft] = Resources.SJLUI_001;

         Buffers.Pictures[Buffers.plLuigi, Buffers.mdLarge, 0, Buffers.dirLeft] = Resources.LWLUI_000;
         Buffers.Pictures[Buffers.plLuigi, Buffers.mdLarge, 1, Buffers.dirLeft] = Resources.LWLUI_001;
         Buffers.Pictures[Buffers.plLuigi, Buffers.mdLarge, 2, Buffers.dirLeft] = Resources.LJLUI_000;
         Buffers.Pictures[Buffers.plLuigi, Buffers.mdLarge, 3, Buffers.dirLeft] = Resources.LJLUI_001;

         Buffers.Pictures[Buffers.plLuigi, Buffers.mdFire, 0, Buffers.dirLeft] = Resources.FWLUI_000;
         Buffers.Pictures[Buffers.plLuigi, Buffers.mdFire, 1, Buffers.dirLeft] = Resources.FWLUI_001;
         Buffers.Pictures[Buffers.plLuigi, Buffers.mdFire, 2, Buffers.dirLeft] = Resources.FJLUI_000;
         Buffers.Pictures[Buffers.plLuigi, Buffers.mdFire, 3, Buffers.dirLeft] = Resources.FJLUI_001;
         
         for ( int Pl = Buffers.plMario; Pl <= Buffers.plLuigi; Pl++ )
         {
            for ( int Md = Buffers.mdSmall; Md <= Buffers.mdFire; Md++ )
               for ( int N = 0; N <= 3; N++ )
                  HighMirror ( Buffers.Pictures[Pl, Md, N, Buffers.dirLeft], ref Buffers.Pictures[Pl, Md, N, Buffers.dirRight] );
         }
      }
      
      //-------------------------------------------------
      // Initialize Player
      //-------------------------------------------------
      public static void InitPlayer (int InitX, int InitY, byte Name)
      {
         Buffers.Player = Name;
         X = InitX;
         Y = InitY;
         OldX = X;
         OldY = Y;
         XVel = 0;
         YVel = 0;
         Direction = Buffers.dirRight;
         Walkingmode = 0;
         Status = stOnTheGround;
         Jumped = false;
         Fired = false;
         HitEnemy = false;
         
         for( int i = 0; i < FormMarioPort.MAX_PAGE; i++ )
            SaveScreen[i].Visible = false;
         
         Enemies.PlayerX1 = X;
         Enemies.PlayerX2 = X + Buffers.W - 1;
         Enemies.PlayerY1 = Y + Buffers.H;
         Enemies.PlayerY2 = Y + 2 * Buffers.H - 1;
         Enemies.PlayerXVel = XVel;
         Enemies.PlayerYVel = YVel;
         Blinking = false;
         Enemies.Star = false;
         Growing = false;
         EarthQuake = false;
      }
      
      //-------------------------------------------------
      // Draw the Demo
      //-------------------------------------------------
      private static void DrawDemo()
      {
         int i, j;

         SaveScreen[FormMarioPort.formRef.CurrentPage()].BackGrAddr = FormMarioPort.formRef.PushBackGr(X, Y, Buffers.W + 4, 2 * Buffers.H);
         SaveScreen[FormMarioPort.formRef.CurrentPage()].XPos = X;
         SaveScreen[FormMarioPort.formRef.CurrentPage()].YPos = Y;
         SaveScreen[FormMarioPort.formRef.CurrentPage()].Visible = true;
         
         switch (Buffers.Demo)
         {
            case Buffers.dmDownInToPipe:
            case Buffers.dmUpOutOfPipe:
            {
               FormMarioPort.formRef.DrawPart(X, Y + DemoY, Buffers.W, 2 * Buffers.H, 0, 2 * Buffers.H - DemoY - 1, Buffers.Pictures[Buffers.Player, Buffers.data.mode[Buffers.Player], Walkingmode, Direction]);
               break;
            }
            case Buffers.dmUpInToPipe:
            case Buffers.dmDownOutOfPipe:
            {
               FormMarioPort.formRef.DrawPart(X, Y + DemoY, Buffers.W, 2 * Buffers.H, -DemoY, 2 * Buffers.H, Buffers.Pictures[Buffers.Player, Buffers.data.mode[Buffers.Player], Walkingmode, Direction]);
               Figures.Redraw( MapX, MapY - 1 );
               Figures.Redraw(MapX + 1, MapY - 1);
               break;
            }
            case Buffers.dmDead:
            {
               FormMarioPort.formRef.DrawImage(X, Y, Buffers.W, 2 * Buffers.H, Buffers.Pictures[Buffers.Player, Buffers.data.mode[Buffers.Player], Walkingmode, Direction]);
               break;
            }
         }
         OldX = X;
         OldY = Y;
      }
      
      //-------------------------------------------------
      // Draw the Player
      //-------------------------------------------------
      public static void DrawPlayer()
      {
         if (Buffers.Demo != Buffers.dmNoDemo)
         {
            DrawDemo();
            return;
         }
         if ( !Blinking || (BlinkCounter % 2 == 0) )
         {
            SaveScreen[FormMarioPort.formRef.CurrentPage()].BackGrAddr = FormMarioPort.formRef.PushBackGr(X, Y, Buffers.W + 4, 2 * Buffers.H);
            SaveScreen[FormMarioPort.formRef.CurrentPage()].XPos = X;
            SaveScreen[FormMarioPort.formRef.CurrentPage()].YPos = Y;
            SaveScreen[FormMarioPort.formRef.CurrentPage()].Visible = true;

            if ((Buffers.data.mode[Buffers.Player] == Buffers.mdFire) && keySpace && (FireCounter < 7))
            {
               FireCounter++;
               FormMarioPort.formRef.DrawPart(X, Y + 1, Buffers.W, 2 * Buffers.H, 0, 20, Buffers.Pictures[Buffers.Player, Buffers.mdFire, 1, Direction]);
               FormMarioPort.formRef.DrawPart(X, Y, Buffers.W, 2 * Buffers.H, 21, 2 * Buffers.H, Buffers.Pictures[Buffers.Player, Buffers.mdFire, 0, Direction]);
            }
            else
               if (Enemies.Star || Growing)
                  FormMarioPort.formRef.RecolorImage(X, Y, Buffers.W, 2 * Buffers.H, Buffers.Pictures[Buffers.Player, Buffers.data.mode[Buffers.Player], Walkingmode, Direction], Convert.ToByte(((GrowCounter + StarCounter) /*&& 1*/) * 16 -
                        Convert.ToByte((GrowCounter + StarCounter) /*& 0xF*/ < 8)));
               else
                  FormMarioPort.formRef.DrawImage(X, Y, Buffers.W, 2 * Buffers.H, Buffers.Pictures[Buffers.Player, Buffers.data.mode[Buffers.Player], Walkingmode, Direction]);
            
            OldX = X;
            OldY = Y;
         }
      }
      
      //-------------------------------------------------
      // Erase the Player
      //-------------------------------------------------
      public static void ErasePlayer()
      {
         if ( !SaveScreen[FormMarioPort.formRef.CurrentPage()].Visible )
            return;
         
         //FormMarioPort.PopBackGr(BackGrAddr);
         
         SaveScreen[FormMarioPort.formRef.CurrentPage()].Visible = false;
      }

      //-------------------------------------------------
      // Execute the Demo
      //-------------------------------------------------
      public static void DoDemo()
      {
         Small = 9 * System.Convert.ToByte(Buffers.data.mode[Buffers.Player] == Buffers.mdSmall);
         switch(Buffers.Demo)
         {
            case Buffers.dmDownInToPipe:
            case Buffers.dmUpOutOfPipe:
            {
               if ( PipeCode[1] == 'ç' )
               {
                  if ( !Mario.Passed )
                  {
                     Mario.Passed = true;
                     Buffers.TextCounter = 0;
                  }
               }

               DemoCounter1++;
               if ( DemoCounter1 % 3 == 0 )
               {
                  if (Buffers.Demo == Buffers.dmDownInToPipe)
                  {
                     DemoY++;
                     if ( (DemoY > 2 * Buffers.H - Small) )
                     {
                        DemoCounter2++;
                        DemoY--;
                        if ( DemoCounter2 > 10 )
                           InPipe = true;
                     }
                  }
                  else
                  {
                     DemoY--;
                     if ( (DemoY < 0) )
                     {
                        DemoY++;
                        Buffers.Demo = Buffers.dmNoDemo;
                     }
                  }
               }
               break;
            }
            case Buffers.dmUpInToPipe:
            case Buffers.dmDownOutOfPipe:
            {
               DemoCounter1++;
               if ( DemoCounter1 % 3 == 0 )
               {
                  if (Buffers.Demo == Buffers.dmDownOutOfPipe)
                  {
                     DemoY++;
                     if ( DemoY > - Small )
                     {
                        Buffers.Demo = Buffers.dmNoDemo;
                        DemoY--;
                     }
                  }
                  else
                  {
                     DemoY--;
                     if ( (DemoY < -2 * Buffers.H + Small) )
                     {
                        DemoCounter2++;
                        DemoY++;
                        if ( DemoCounter2 > 10 )
                           InPipe = true;
                     }
                  }
               }
               break;
            }

            case Buffers.dmDead:
            {
               DemoCounter1++;
               if ( DemoCounter1 % 7 == 0 )
                  YVel++;
               Y = Y + YVel;
               if (Y > Buffers.NV * Buffers.H)
                  Buffers.GameDone = true;
               break;
            }
         }
      }
      
      //-------------------------------------------------
      // Start the Demo
      //-------------------------------------------------
      private static void StartDemo (int dm)
      {
         Buffers.Demo = dm;
         DemoCounter1 = 0;
         DemoCounter2 = 0;
         DemoX = 0;
         DemoY = 0;
         Below1 = ' ';
         Below2 = ' ';
         AtCh1 = ' ';
         AtCh2 = ' ';

         if (dm == Buffers.dmDownInToPipe || dm == Buffers.dmUpInToPipe || dm == Buffers.dmDownOutOfPipe || dm == Buffers.dmUpOutOfPipe)
            //Music.StartMusic (PipeMusic);
         
         switch (dm)
         {
            case Buffers.dmUpOutOfPipe:
               DemoY = 2 * Buffers.H - 9 * Convert.ToByte(Buffers.data.mode[Buffers.Player] == Buffers.mdSmall);
               break;
            case Buffers.dmDownOutOfPipe:
            {
               DemoY = -2 * Buffers.H;
               Y += Buffers.H - 7 * Convert.ToByte(Buffers.data.mode[Buffers.Player] == Buffers.mdSmall) - 2;
               break;
            }
            case Buffers.dmDead:
            {
              YVel = -3;
              //Beep (220 );
              break;
            }
         }
         InPipe = false;
      }
      
      //-------------------------------------------------
      // Check the pipe below for type
      //-------------------------------------------------
      private static void CheckPipeBelow()
      {
         int Mo;
         if ( (XVel != 0) || (YVel != 0) || (Y % Buffers.H != 0) )
            return;
         Mo = X % Buffers.W;
//         if ( !(Mo in [4 .. Buffers.W - 4]) )
         if ( Mo >= 4 && Mo <= Buffers.W - 4 )
            return;
         if ( (Below1 != '0') || (Below2 != '1') || (!(AtCh1 >= 'à' && AtCh1 <= 'ç')) // $E0..$E7: Enter pipe
               || (!(AtCh2 >= 'à' && AtCh2 <= 'ï')) )
            return;
         PipeCode[1] = AtCh1;
         PipeCode[2] = AtCh2;
         StartDemo(Buffers.dmDownInToPipe);
      }
      
      //-------------------------------------------------
      // Check the pipe above for type
      //-------------------------------------------------
      private static void CheckPipeAbove (char C1, char C2)
      {
         int Mo = X % Buffers.W;
         if ( !(Mo >= 4 && Mo <= Buffers.W - 4) )
            return;
         if ( (C1 != '0') || (C2 != '1') )
            return;
         MapX = X / Buffers.W;
         MapY = Y / Buffers.H + 1;
         if ( (!(Buffers.WorldMap[MapX, MapY] >= 'à' && Buffers.WorldMap[MapX, MapY] <= 'ç')) // $E0..$E7: Enter pipe
                  || (!(Buffers.WorldMap[MapX + 1, MapY] >= 'à' && Buffers.WorldMap[MapX + 1, MapY] <= 'ï')) )
            return;
         PipeCode[1] = (char)Buffers.WorldMap[MapX, MapY];
         PipeCode[2] = (char)Buffers.WorldMap[MapX + 1, MapY];
         StartDemo(Buffers.dmUpInToPipe);
      }
      
      
      static int Side, NewX1, NewX2, NewY, Y1, Y2, Y3, Mo;
      static char NewCh1, NewCh2, NewCh3, ch;
      static bool Hold1, Hold2, Hold3, Hit = false;

      //-------------------------------------------------
      // Check the player for actions
      //-------------------------------------------------
      private static void Check()
      {
         bool Small;

         NewCh1 = ' ';
         NewCh2 = ' ';
         NewCh3 = ' ';

         Side  = System.Convert.ToByte(XVel > 0) * (Buffers.W - 1);
         NewX1 = (X + Side) / Buffers.W;
         NewX2 = (X + Side + XVel) / Buffers.W;
         Small = Buffers.data.mode[Buffers.Player] == Buffers.mdSmall;

         if ( NewX1 != NewX2 )
         {
            Y1 = (Y + HSafe + (4)) / Buffers.H - Safe;
            Y2 = (Y + HSafe + Buffers.H) / Buffers.H - Safe;
            Y3 = (Y + HSafe + 2 * Buffers.H - 1) / Buffers.H - Safe;
            NewCh1 = (char)Buffers.WorldMap[NewX2, Y1];
            NewCh2 = (char)Buffers.WorldMap[NewX2, Y2];
            NewCh3 = (char)Buffers.WorldMap[NewX2, Y3];

            if ( NewCh3 == '*' )
               TmpObj.HitCoin(NewX2 * Buffers.W, Y3 * Buffers.H, false);
            
            if ( NewCh2 == '*' )
               TmpObj.HitCoin(NewX2 * Buffers.W, Y2 * Buffers.H, false); 
            else if ( NewCh2 == 'z' )
               Enemies.Turbo = true;


         	if (!Small && NewCh1 == '*' )
               TmpObj.HitCoin(NewX2 * Buffers.W, Y1 * Buffers.H, false );

            Hold1 = ( Buffers.CanHoldYou(NewCh1) ) && (!Small );
            Hold2 = ( Buffers.CanHoldYou(NewCh2) );
            Hold3 = ( Buffers.CanHoldYou(NewCh3) );

            if ( Hold1 || Hold2 || Hold3 )
            {
               XVel = 0;
               Walkingmode = 0;
            }
         }

         NewX1 = (X + XVel) / Buffers.W;
         NewX2 = (X + XVel + Buffers.W - 1) / Buffers.W;

         if ( Enemies.cdEnemy != 0 )
            CheckJump();

         if ( (Status == stJumping) )
            NewY = (Y + 1 + (4) + (Buffers.H - 1 - (4)) * Convert.ToByte(Small) + YVel + HSafe) / Buffers.H - Safe;
         else
            NewY = (Y + 1 + 2 * Buffers.H + YVel + HSafe) / Buffers.H - Safe;

         NewCh1 = (char)Buffers.WorldMap[NewX1, NewY];
         NewCh2 = (char)Buffers.WorldMap[NewX2, NewY];
         NewCh3 = (char)Buffers.WorldMap[(X + XVel + Buffers.W / 2) / Buffers.W, NewY];
         Hold1 = ( Buffers.CanHoldYou(NewCh1) || Buffers.CanStandOn(NewCh1)  );
         Hold2 = ( Buffers.CanHoldYou(NewCh2) || Buffers.CanStandOn(NewCh2)  );
         Hold3 = ( Buffers.CanHoldYou(NewCh3) || Buffers.CanStandOn(NewCh3)  );

         switch (Status)
         {
            case stFalling:
            {
              	CheckFall();
               break;
            }
            case stOnTheGround:
            {
               if ( (Enemies.cdLift == 0) )
               {
                  if ( !(Hold1 || Hold2) )
                  {
                     Status = stFalling;
                     if ( Math.Abs(XVel) < 2 )
                        Y++;
                  }
                  else
                  {
                     if  ( (NewCh1 == 'K') || (NewCh2 == 'K') )
                        CheckFall();
                     else
                     {
                        if ( XVel == 0 )
                        {
                           Below1 = NewCh1;
                           Below2 = NewCh2;
                        	MapX = NewX1; //Codes for pipes
                           MapY = NewY - 1;
                        	AtCh1 = (char)Buffers.WorldMap[MapX, MapY];
                        	AtCh2 = (char)Buffers.WorldMap[MapX + 1, MapY];

                           Mo = (X + XVel) % Buffers.W;
                           if ( !Hold1 && (Mo >= 1 && Mo <= 5) )
                              XVel--;
                           if ( !Hold2 && (Mo >= Buffers.W - 5 && Mo <= Buffers.W - 1) )
                              XVel++;
                        }
                     }
                     CheckJump();
                  }
               }
               else
               {
                  YVel = Enemies.PlayerYVel;
                  CheckJump();
               }
               break;
            }

            case stJumping:
            {
               Hold1 = ( Buffers.CanHoldYou(NewCh1) || Buffers.CanStandOn(NewCh1)  );
               Hold2 = ( Buffers.CanHoldYou(NewCh2) || Buffers.CanStandOn(NewCh2)  );
               Hold3 = ( Buffers.CanHoldYou(NewCh3) || Buffers.CanStandOn(NewCh3)  );

               Hit = (Hold1 || Hold2 );
               if ( Hit )
               {
                  Mo = (X + XVel) % Buffers.W;
                  if ( (Mo >= 1 && Mo <= 4 && Mo >= Buffers.W - 4 && Mo <= Buffers.W - 1) && (!Hold3) )
                  {
                     if ( !(( NewCh1 == Buffers.Hidden ) && ( NewCh2 == Buffers.Hidden )) )
                        Hit = false;
                     if ( (Mo < Buffers.W / 2) && (!( NewCh2 == Buffers.Hidden )) )
                        X -= Mo;
                     else
                        if ( (Mo >= Buffers.W / 2) && (!( NewCh1 == Buffers.Hidden )) )
                           X += Buffers.W - Mo;
                  }
               }
               if ( !Hit )
               {
                  if ( NewCh1 == '*' )
                     TmpObj.HitCoin(NewX1 * Buffers.W, NewY * Buffers.H, false);
                  
                  if ( NewCh1 == '*' )
                     TmpObj.HitCoin (NewX2 * Buffers.W, NewY * Buffers.H, false );
                
                  if ( (Counter % (JumpDelay + Convert.ToByte(HighJump)) == 0) || ((!keyAlt) && (!HitEnemy)) )
                     YVel++;

                  if ( YVel >= 0 )
                  {
                     YVel = 0;
                     Status = stFalling;
                  }
               }
               else
               {
                  char Ch = (char)0;

                  if (Mo >= 0 && Mo <= (Buffers.W / 2 - 1))
                  {
                     if (  Buffers.CanHoldYou(NewCh1) || Buffers.CanStandOn(NewCh1)  )
                     {
                        Ch = NewCh1;
                        NewX2 = NewX1;
                     }
                     else
                        Ch = NewCh2;
                  }
                  else if (Mo >= (Buffers.W / 2) && Mo <= Buffers.W - 1)
                  {
                     Ch = NewCh2;
                     if ( !( Buffers.CanHoldYou(Ch) || Ch == Buffers.Hidden ) )
                     {
                        Ch = NewCh1;
                        NewX2 = NewX1;
                     }
                  }
             
                  switch (Ch)
                  {
                     case '=':
                        Enemies.cdHit = 1;
                        break;
                     case '0':
                     case '1':
                        if ( keyUp )
                           CheckPipeAbove (NewCh1, NewCh2 );
                        break;
                     case '?':
                     case '$':
                     case 'J':
                     case 'K':
                     {
                        Mo = 0;

                        if ( Buffers.WorldMap[NewX2, NewY - 1] >= 'à' && Buffers.WorldMap[NewX2, NewY - 1] <= 'â' )
                        {
                           Buffers.WorldMap[NewX2, NewY] = '?';
                           Ch = '?';
                        }
                        else if ( Buffers.WorldMap[NewX2, NewY - 1] == 'ï' )
                        {
                           Buffers.WorldMap[NewX2, NewY] = 'K';
                           Ch = 'K';
                        }
                        else
                        {
                           if ( !Small && (Ch == 'J') )
                           {
                              TmpObj.BreakBlock (NewX2, NewY );
                              Buffers.AddScore (10);
                              Mo = 1;
                           }
                        }

                        if ( Mo == 0 )
                        {
                           Blocks.BumpBlock (NewX2 * Buffers.W, NewY * Buffers.H );
                           //Beep (110 );
                        }

                        if (Buffers.WorldMap[NewX2, NewY - 1] >= 'ã' && Buffers.WorldMap[NewX2, NewY - 1] <= 'ì')
                        {
                           if ( !(Ch == 'J' || Ch == 'K') )
                           {
                              TmpObj.HitCoin (NewX2 * Buffers.W, NewY * Buffers.H, true );
                              if ( Buffers.WorldMap[NewX2, NewY - 1] != ' ' )
                              {
                                 Buffers.WorldMap[NewX2, NewY - 1] = (char)(Buffers.WorldMap[NewX2, NewY - 1] + 1);
                                 if ( Buffers.WorldMap[NewX2, NewY] == '$' )
                                 {
                                    TmpObj.Remove (NewX2 * Buffers.W, NewY * Buffers.H, Buffers.W, Buffers.H, 2 );
                                    Buffers.WorldMap[NewX2, NewY] = '?';
                                 }
                              }
                           }
                        }
                        else if (Buffers.WorldMap[NewX2, NewY - 1] == 'à')
                        {
                           if ( Buffers.data.mode[Buffers.Player] == Buffers.mdSmall )
                              Enemies.NewEnemy (EnemyType.tpRisingChamp, 0, NewX2, NewY, 0, -1, 2);
                           else
                              Enemies.NewEnemy (EnemyType.tpRisingFlower, 0, NewX2, NewY, 0, -1, 2 );
                        }
                        else if (Buffers.WorldMap[NewX2, NewY - 1] == 'á')
                           Enemies.NewEnemy (EnemyType.tpRisingLife, 0, NewX2, NewY, 0, -1, 2 );
                        else if (Buffers.WorldMap[NewX2, NewY - 1] == 'â')
                           Enemies.NewEnemy (EnemyType.tpRisingStar, 0, NewX2, NewY, 0, -1, 1 );
                        else if (Buffers.WorldMap[NewX2, NewY - 1] == '*') 
                           TmpObj.HitCoin (NewX2 * Buffers.W, (NewY - 1) * Buffers.H, false );
                        else if (Buffers.WorldMap[NewX2, NewY - 1] == 'í') 
                           Enemies.NewEnemy (EnemyType.tpRisingChamp, 1, NewX2, NewY, 0, -1, 2 );

                        Enemies.HitAbove (NewX2, NewY - 1 );
                        if ( Ch == 'K' )
                        {
                           TmpObj.Remove (NewX2 * Buffers.W, NewY * Buffers.H, Buffers.W, Buffers.H, TmpObj.tpNote );
                           Buffers.WorldMap[NewX2, NewY] = 'K';
                        }
                        else
                        {
                           if (Ch != 'J')
                              if (!(Buffers.WorldMap[NewX2, NewY - 1] >= 'ã' && Buffers.WorldMap[NewX2, NewY - 1] <= 'ì'))
                              {
                                 TmpObj.Remove (NewX2 * Buffers.W, NewY * Buffers.H, Buffers.W, Buffers.H, 1 );
                                 Buffers.WorldMap[NewX2, NewY] = '@';
                              }
                        }
                        break;
                     }
                     default:
                     //Beep (30);
                     break;
                  }
                  
                  if ( (Ch != 'J') || (Buffers.data.mode[Buffers.Player] == Buffers.mdSmall) )
                  {
                     YVel = 0;
                     Status = stFalling;
                  }
                  if ( Ch == 'K' )
                     YVel = 3;
               }
               break;
            }
         }
      }
      
      //-------------------------------------------------
      // Check status of player - Fall
      //-------------------------------------------------
      private static void CheckFall()
      {
         int Mo = 0;
         char Ch;
         if ( !(Hold1 || Hold2) )
         {
            if ( NewCh1 == '*' )
               TmpObj.HitCoin( NewX1 * Buffers.W, NewY * Buffers.H, false );
            
            if ( NewCh1 == '*' )
               TmpObj.HitCoin( NewX2 * Buffers.W, NewY * Buffers.H, false );
               
            if ( Counter % JumpDelay == 0 )
               YVel++;
            
            if ( YVel > MaxYVel )
               YVel = MaxYVel;
         }
         else
         {
            if ( (NewCh1 == '=') || (NewCh2 == '=') )
               Enemies.cdHit = 1;

            Mo = (X + XVel) % Buffers.W;
            Y = ((Y + YVel + 1 + HSafe) / Buffers.H - Safe) * Buffers.H;
            YVel = 0;
            Status = stOnTheGround;
            Jumped = true;

            if ( (NewCh1 == 'K') || (NewCh2 == 'K') )
            {
               //StartMusic ( NoteMusic );
               if ( NewCh1 == 'K' )
               {
                  Blocks.BumpBlock ( NewX1 * Buffers.W, NewY * Buffers.H );
                  TmpObj.Remove ( NewX1 * Buffers.W, NewY * Buffers.H, Buffers.W, Buffers.H, TmpObj.tpNote );
                  Buffers.WorldMap[NewX1, NewY] = 'K';
               }
               if ( NewCh2 == 'K' )
               {
                  Blocks.BumpBlock (NewX2 * Buffers.W, NewY * Buffers.H );
                  TmpObj.Remove (NewX2 * Buffers.W, NewY * Buffers.H, Buffers.W, Buffers.H, TmpObj.tpNote );
                  Buffers.WorldMap[NewX2, NewY] = 'K';
               }
               Counter = 0;
               Status = stJumping;
               Jumped = false;
               HighJump = true;
               YVel = -5;
               HitEnemy = true;
            }
         }
         
         if ( Mo >= 0 && Mo <= Buffers.W / 2 - 1 )
         {
            if ( Hold1 )
            {
               Ch = NewCh1;
               NewX2 = NewX1;
            }
            else
               Ch = NewCh2;
         }
         else if ( Mo >= Buffers.W / 2 && Mo <= Buffers.W )
         {
            if ( Hold2 )
               Ch = NewCh2;
            else
            {
               Ch = NewCh1;
               NewX2 = NewX1;
            }
         }
         
      }
      
      //-------------------------------------------------
      // Check status of player - Jump
      //-------------------------------------------------
      private static void CheckJump()
      {  
         if (Enemies.cdEnemy != 0)
         {
            HitEnemy = true;
            Jumped = false;
         }
         if ( !Jumped )
         {
            if (keyAlt || HitEnemy)
            {
               Counter = 0;
               Status = stJumping;
               HighJump = (Math.Abs(XVel) == 2) || (HitEnemy && keyAlt );
               YVel = - JumpVel - 2 * Convert.ToByte(HitEnemy && keyAlt) - Convert.ToByte(Enemies.Turbo );
            }
         }
         Enemies.cdEnemy = 0;
      }

      //-------------------------------------------------
      // Move the player
      //-------------------------------------------------
      public static void MovePlayer()
      {
         int MaxSpeed, MinSpeed, OldXVel, OldXView;
         bool CheckX;
         byte OldDir;
         bool LastKeyRight, LastKeyLeft;

         if (InPipe)
         {
            if (Buffers.WorldMap[MapX, MapY + 1] == '0')
               StartDemo(Buffers.dmUpOutOfPipe);
            else
               if (Buffers.WorldMap[MapX, MapY - 1] == '0')
                  StartDemo(Buffers.dmDownOutOfPipe);
            return;
         }

         if (Enemies.cdChamp != 0)
         {
            if (Buffers.data.mode[Buffers.Player] == Buffers.mdSmall)
            {
               Buffers.data.mode[Buffers.Player] = Buffers.mdLarge;
               Growing = true;
               GrowCounter = 0;
            }
            //StartMusic (GrowMusic );
            Enemies.cdChamp = 0;
         }

         if (Enemies.cdLife != 0)
         {
            Enemies.cdLife = 0;
            TmpObj.AddLife();
         }
         if (Enemies.cdFlower != 0)
         {
            Buffers.data.mode[Buffers.Player] = Buffers.mdFire;
            Fired = true;
            FireCounter = 0;
            //StartMusic (GrowMusic );
            Growing = true;
            GrowCounter = 0;
            Enemies.cdFlower = 0;
         }

         //if ( !Blinking && !Enemies.Star && !Growing )
         if (!Blinking && !Growing)
         {
            if (Enemies.cdHit != 0)

               switch (Buffers.data.mode[Buffers.Player])
               {
                  case Buffers.mdSmall:
                     {
                        BlinkCounter = 0;
                        Blinking = true;
                        StartDemo(Buffers.dmDead);
                        //StartMusic (DeadMusic );
                        return;
                     }
                  case Buffers.mdLarge:
                  case Buffers.mdFire:
                     {
                        Buffers.data.mode[Buffers.Player] = Buffers.mdSmall;
                        BlinkCounter = 0;
                        Blinking = true;
                        //StartMusic (HitMusic );
                        break;
                     }
                  default:
                     break;
               }
            Enemies.cdHit = 0;
         }
         else
            Enemies.cdHit = 0;

         if (Blinking)
         {
            BlinkCounter++;
            if (BlinkCounter >= BlinkTime)
               Blinking = false;
         }

         if (Enemies.cdStar != 0)
         {
            //StartMusic(StarMusic );
            StarCounter = 0;
            Enemies.Star = true;
         }

         if (Enemies.Star)
         {
            StarCounter++;
            if (StarCounter >= StarTime)
               Enemies.Star = false;
            if (StarCounter % 3 == 0)
               Glitter.StartGlitter(X, Y + 11 * Convert.ToByte(Buffers.data.mode[Buffers.Player] = Buffers.mdSmall), Buffers.W, Buffers.H + 3 + 11 * Convert.ToByte(Buffers.data.mode[Buffers.Player] != Buffers.mdSmall));
            Enemies.cdStar = 0;
         }

         if (Growing)
         {
            GrowCounter++;
            if (GrowCounter > GrowTime)
               Growing = false;
         }

         Counter++;
         if ((XVel == 0) && (YVel == 0))
            Counter = 0;
         CheckX = (Counter % Slip == 0);

         OldDir = Direction;
         OldXVel = XVel;
         
//         LastKeyLeft = Keyboard.keyLeft;
//         LastKeyRight = Keyboard.keyRight;

         keyLeft = Keyboard.kbLeftArrow;
         keyRight = Keyboard.kbRightArrow;
         keyUp = Keyboard.kbUpArrow;
         keyDown = Keyboard.kbDownArrow;
         keyAlt = Keyboard.kbAlt;
         keyCtrl = Keyboard.kbCtrl;
         keySpace = Keyboard.kbSP;

         if (keyRight && (Direction == Buffers.dirLeft))
         {
            OldDir = Buffers.dirRight;
            OldXVel = -XVel;
         }
         if (keyLeft && (Direction == Buffers.dirRight))
         {
            OldDir = Buffers.dirLeft;
            OldXVel = -XVel;
         }

         if (Fired && (!keySpace))
            Fired = false;

         if (keySpace && (!Fired) && (Buffers.data.mode[Buffers.Player] == Buffers.mdFire))
         {
            FireCounter = 0;
            Enemies.NewEnemy(EnemyType.tpFireBall, 0, X / Buffers.W + Direction, (Y + Buffers.H) / Buffers.H,
               10 * (-1 + 2 * Direction), 3 + 3 * (Convert.ToByte(keyDown) - Convert.ToByte(keyUp)), 2);
            Fired = true;
         }

         if (Enemies.cdLift != 0)
         {
            Y = Enemies.PlayerY1;
            XVel = Enemies.PlayerXVel;
            YVel = Enemies.PlayerYVel;
            Status = stOnTheGround;
         }
         if (Enemies.cdStopJump != 0)
         {
            Jumped = true;
            Enemies.cdStopJump = 0;
         }

         if (Jumped && (!keyAlt))
            Jumped = false;

         MaxSpeed = +MAX_SPEED - 1 + Convert.ToByte(keyCtrl) + Convert.ToByte(Enemies.Turbo) + Math.Abs(Enemies.cdLift * Enemies.PlayerXVel);
         MinSpeed = -MAX_SPEED + 1 - Convert.ToByte(keyCtrl) - Convert.ToByte(Enemies.Turbo) - Math.Abs(Enemies.cdLift * Enemies.PlayerXVel);

         if (keyLeft)
         {
            if ((XVel > MinSpeed))
            {
               if (CheckX || (Enemies.cdLift != 0))
                  XVel -= 1 + Convert.ToByte((Enemies.cdLift != 0) && keyCtrl);
            }
            else
               XVel = MinSpeed;
            Direction = Convert.ToByte(XVel > 0);
            if (X + XVel < 0)
               XVel = -X;
         }
         else
            if ((XVel < 0) && CheckX && (Enemies.cdLift == 0))
               XVel++;

         if (keyRight)
         {
            if ((XVel < MaxSpeed))
            {
               if (CheckX || Enemies.cdLift != 0)
                  XVel += 1 + Convert.ToByte(Enemies.cdLift != 0 && keyCtrl);
            }
            else
               XVel = MaxSpeed;
            Direction = Convert.ToByte(XVel >= 0);
         }
         else
            if ((XVel > 0) && CheckX && (Enemies.cdLift == 0))
               XVel--;

         if (keyLeft && keyRight)
         {
            Direction = OldDir;
            XVel = OldXVel;
         }

         if (Y + YVel >= Buffers.NV * Buffers.H)
         {
            Buffers.GameDone = true;
            //StartMusic (DeadMusic );
         }

         if (Status == stOnTheGround)
            HitEnemy = false;

         Check();

         if ((Status == stOnTheGround) && YVel == 0)
         {
            if ((XVel == 0) || ((Enemies.cdLift != 0) && (XVel == Enemies.PlayerXVel)))
            {
               Walkingmode = 0;
               WalkCount = 0;
            }
            else
            {
               WalkCount++;
               WalkCount = (byte)(WalkCount & 0xf);
               Walkingmode = Convert.ToByte(WalkCount < 0x8);
            }
         }
         else
         {
            if (YVel < 0)
               Walkingmode = 2;
            else
               Walkingmode = 3;
         }

         if (keyDown)
            CheckPipeBelow();

         X += XVel;
         Y += YVel;

         //Console.WriteLine(X);
         //if ( X % 40 == 0)
         //   X += 1;

         //X++;
         //Y = 10;

         OldXView = Buffers.XView;
         Buffers.XView = Buffers.XView - System.Convert.ToByte(Keyboard.kbShiftl) + System.Convert.ToByte(Keyboard.kbShiftr);
         if (X + Buffers.W + SCROLL_AT > Buffers.XView + 320)
            Buffers.XView = X + Buffers.W + SCROLL_AT - 320;
         if (X < Buffers.XView + SCROLL_AT)
            Buffers.XView = X - SCROLL_AT;
         if (Buffers.XView - OldXView > MAX_SPEED + Convert.ToByte(Enemies.Turbo))
            Buffers.XView = OldXView + MAX_SPEED + Convert.ToByte(Enemies.Turbo);
         if (Buffers.XView - OldXView < -MAX_SPEED - Convert.ToByte(Enemies.Turbo))
            Buffers.XView = OldXView - MAX_SPEED - Convert.ToByte(Enemies.Turbo);
         if (Buffers.XView < 0)
         {
            Buffers.XView = 0;
            if (X < 0) X = 0;
         }

         if (Buffers.XView > (Buffers.Options.XSize - Buffers.NH) * Buffers.W)
            Buffers.XView = (Buffers.Options.XSize - Buffers.NH) * Buffers.W;
         if (Buffers.XView < OldXView)
            if ((Buffers.WorldMap[Buffers.XView / Buffers.W, Buffers.NV] == 254))
               if ((Buffers.WorldMap[(Buffers.XView / Buffers.W), (int)(Enemies.PlayerY1 / Buffers.H)] != ' '))
                  Buffers.XView = OldXView;
         if (Buffers.XView > OldXView)
            if ((Buffers.WorldMap[((Buffers.XView - 1) / Buffers.W + Buffers.NH), Buffers.NV] == 255))
               if ((Buffers.WorldMap[((Buffers.XView - 1) / Buffers.W + Buffers.NH), (int)(Enemies.PlayerY1 / Buffers.H)] != ' '))
                  Buffers.XView = OldXView;

         Enemies.PlayerX1 = X + XVel;
         Enemies.PlayerX2 = Enemies.PlayerX1 + Buffers.W - 1;
         Enemies.PlayerY1 = Y;
         if (Buffers.data.mode[Buffers.Player] == Buffers.mdSmall)
            Enemies.PlayerY1 = Y + Buffers.H;
         else
            Enemies.PlayerY1 = Y;
         Enemies.PlayerY2 = Y + 2 * Buffers.H - 1;
         Enemies.PlayerXVel = XVel;
         Enemies.PlayerYVel = YVel;

         if (Enemies.cdLift != 0)
         {
            Enemies.PlayerYVel += 2 - YVel;
            Enemies.cdLift = 0;
         }
      }
      

   } // end class Players
} // end namespace MarioPort
