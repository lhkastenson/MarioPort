//-------------------------------------------------------------------
//Purpose: This File contains functions and methods portaining to
//         running the game inside the world 
// 
//Author:  Lon Kastenson
//
//
//-------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MarioPort;
﻿using Resources = MarioPort.Properties.Resources;

namespace MarioPort
{
   public static class Play
   {
      static int k;
      public static bool Stat = false;
      public static bool ShowRetrace = false;

      const int YBase = 9;
      public static bool Waiting, ShowObjects, ShowScore, CountingScore;
      public static int CheatsUsed = 0;
      static int[] TotalBackGrAddr = new int[FormMarioPort.MAX_PAGE];
      private static bool TextStatus, OnlyDraw = false;

      //-------------------------------------------------------------------
      // Sets up and reads in a new world to the buffer. After the world 
      // has been read, it will build and then finally start running the
      // world.
      //    N1: char, the first part (before the hyphen) of the world name.
      //    N2: char, the second part (before the hyphen) of the world name.
      //    Map1: char[,], the map to play
      //    Opt1: WorldOptions, options for the aboveground world of Map1
      //    Opt1b, WorldOptions, options for the underground world of Map1
      //    Map2, char[,] the underground of the map to play
      //    Opt2, WorldOptions, options for the abovegound world of Map2
      //    Opt2b, WorldOptions, options for the underground world of Map2
      //    Player, byte, the player to put in the world.
      //-------------------------------------------------------------------
      public static bool PlayWorld(char N1, char N2, char[,] Map1, Buffers.WorldOptions Opt1, Buffers.WorldOptions Opt1b, 
						  char[,] Map2, Buffers.WorldOptions Opt2, Buffers.WorldOptions Opt2b, byte Player)
	   {
		   int j, k, x, y;
         
	      int[] TotalBackGrAddr = new int[FormMarioPort.MAX_PAGE];
         //PlayWorld = false;
         //Keyboard.Key = 0;

         FormMarioPort.formRef.SetYOffset(YBase);//FormMarioPort.formRef.Y);
         FormMarioPort.formRef.SetYStart (18);
         FormMarioPort.formRef.SetYEnd (125);

         //Palettes.ClearPalette();
         //Palettes.LockPal();
         FormMarioPort.formRef.ClearVGAMem();

         Buffers.TextCounter = 0;

         Buffers.WorldNumber = new string[] {N1.ToString(), "-", N2.ToString()};
         OnlyDraw = ((N1 == '0') && (N2 == '0'));

         ShowObjects = true;

         Players.InPipe = false;
         Players.PipeCode = new char[] { ' ', ' '};
         Buffers.Demo = Buffers.dmNoDemo;

         Buffers.InitLevelScore();

         //FillChar (TotalBackGrAddr, TotalBackGrAddr.Size(), 0);
         for (int i = 0; i < TotalBackGrAddr.Length; i++)
            TotalBackGrAddr[i] = 0;
         ShowScore = false;

         if (! Enemies.Turbo)
         {
            Buffers.ReadWorld (Map2, ref Buffers.WorldMap, Opt2);
            Buffers.Swap();
            Buffers.ReadWorld (Map1, ref Buffers.WorldMap, Opt1);
         }
         else
         {
            Buffers.ReadWorld (Map2, ref Buffers.WorldMap, Opt2b);
            Buffers.Swap();
            Buffers.ReadWorld (Map1, ref Buffers.WorldMap, Opt1b);
         }
         //Console.WriteLine("Done Reading World");
         Players.InitPlayer (Buffers.Options.InitX, Buffers.Options.InitY, Buffers.Player);
         Players.MapX = Buffers.Options.InitX;
         Players.MapY = Buffers.Options.InitY;

         Buffers.XView = 0;
         Buffers.YView = 0;

         //FillChar (LastXView, LastXView.Size(), 0);
         for (int i = 0; i < Buffers.LastXView.Length; i++)
            Buffers.LastXView[i] = 0;
         FormMarioPort.formRef.SetView (Buffers.XView, Buffers.YView);

         BuildLevel();
         try
         {
            Restart();
         }
         catch (Exception e)
         {
            Console.WriteLine("Exception: ", e);
         }

         FormMarioPort.formRef.SetYOffset(YBase);

         Enemies.ClearEnemies();
         //ClearGlitter();
         //Palettes.FadeDown(64);
         //Palettes.ClearPalette();
         FormMarioPort.formRef.ClearVGAMem();
         //Music.StopMusic();
         return false;
         //http://en.wikipedia.org/wiki/Return_statement#Syntax
         //"In Pascal there is no return statement." ...wat!?
		}

      //-------------------------------------------------------------------
      // Scrolls the screen to move along with mario as he moves.
      //-------------------------------------------------------------------
		public static void MoveScreen()
		{
         int Scroll;
         int N1, N2;
         int OldX, NewX, Page;
         Random rand = new Random();
         Page = FormMarioPort.formRef.CurrentPage();
         Scroll = Buffers.XView - Buffers.LastXView[Page];
         if (!Players.EarthQuake)
            FormMarioPort.formRef.SetView(Buffers.XView, Buffers.YView);
         else
         {
            Players.EarthQuakeCounter++;
            if (Players.EarthQuakeCounter > 0)
               Players.EarthQuake = false;
            int Rand1 = rand.Next(0, 2);
            int Rand2 = rand.Next(0, 2);
            FormMarioPort.formRef.SetView (Buffers.XView, Buffers.YView + Rand1 - Rand2);				
         }
         if (Scroll < 0)
            Enemies.StartEnemies ((Buffers.XView / Buffers.W) - Enemies.StartEnemiesAt, 1);
         else if (Scroll > 0)
            Enemies.StartEnemies((Buffers.XView / Buffers.W) + Buffers.NH + Enemies.StartEnemiesAt, -1);
			
         int i = Buffers.Options.Horizon;
         Buffers.Options.Horizon = (byte)(i + FormMarioPort.formRef.GetYOffset() - FormMarioPort.YBASE);
         BackGr.DrawBackGr(false);
         Buffers.Options.Horizon = (byte)i;
			
         if (Scroll > 0)
            for (int j = Buffers.LastXView[Page]; j < Buffers.XView; j++)
            {
               i = j - Buffers.W - Buffers.W;
               if (i >= 0)
                  FormMarioPort.formRef.PutPixel(i, 0, 0);
               i = Buffers.W - j % Buffers.W - 1;
               Figures.Redraw (j / Buffers.W + Buffers.NH + 1, i);
            }
				
            if (Scroll < 0)
               for (int j = Buffers.LastXView[Page]; j > Buffers.XView; j--)
               {
                  i = Buffers.W - j % Buffers.W - 1;
                  Figures.Redraw(j / Buffers.W - 1, i);
               }
		}

      //-------------------------------------------------------------------
      // Determines if mario found the pipe exit 
      //-------------------------------------------------------------------
	   public static bool FindPipeExit()
	   {
         //int i, j;
         for (int i = 0; i < Buffers.Options.XSize - 1; i++)
            for (int j = 0; j < Buffers.NH; j++)
               if (i != Players.MapX || k != Players.MapY)
               {
                  if (Buffers.WorldMap[i,j] >= 'a' || Buffers.WorldMap[i,j] <= 'i' &&
                     Buffers.WorldMap[i + 1, j] == Players.PipeCode [2]) //need to figure out how to do this.
                  {
                     Players.MapX = i;
                     Players.MapY = j;
                     Buffers.XView =  ((i - Buffers.NH / 2) * Buffers.W) + 1;
                     if (Buffers.XView > (Buffers.Options.XSize - Buffers.NH) * Buffers.W)
                        Buffers.XView = (Buffers.Options.XSize - Buffers.NH) * Buffers.W;
                     if (Buffers.XView < 0)
                        Buffers.XView = 0;
                     return true;
                  }
               }
         return false;
         
	   }

      //-------------------------------------------------------------------
      // Writes the current player's score to the screen
      //-------------------------------------------------------------------
	   public static void WriteTotalScore()
	   {
         //int i;
         //string s;
         ////SetFont(0, Bold + Shadow);
         ////Str (Data.Score[Player]: 11, S);
         //for (int i = 4; i < s.Size(); i++)
         //   if (S[i] == ' ')
         //      S[i] = '0';
         //CenterText (120, "TOTAL SCORE: " + S, 31);
	   }

      //-------------------------------------------------------------------
      // displays the background
      //-------------------------------------------------------------------
	   public static void ShowTotalBack()
	   {
         if (Buffers.Passed && CountingScore)
            Buffers.Beep(4 * 880);
         TotalBackGrAddr[FormMarioPort.formRef.CurrentPage()] = FormMarioPort.formRef.PushBackGr(Buffers.XView + 160, 120, 120, 8);
         if (Buffers.Passed && CountingScore)
            Buffers.Beep(2 * 880);
         WriteTotalScore();
         if (Buffers.Passed && CountingScore)
            Buffers.Beep(0);
	   }

      //-------------------------------------------------------------------
      // Hides the background
      //-------------------------------------------------------------------
	   public static void HideTotalBack()
	   {
         //int Page = CurrentPage;
         //if (TotalBackGrAddr[Page] != 0)
         //   PopBackGr (TotalBackGrAddr[CurrentPage]);
         //TotalBackGrAddr[Page] = 0;
	   }

      //-------------------------------------------------------------------
      // Pauses the game and enables the cheat console (cheats not yet implemented)
      //-------------------------------------------------------------------
      public static void Pause()
      {
         //   // string * StrPtr;
         //   int i, PauseBack;
         //   char OldKey, Ch;
         //   bool EndPause;
         //   string PauseText, Cheat;
         //   // string * P;

         //   const int CRED_LEN = 26;
         //   // there's got to be a better way to do this...
         //   const byte[] Credit = new Byte[] {CRED_LEN,
         //      ascii.GetBytes('P') 1 + 10.ToString("X"),
         //      ascii.GetBytes('R') 2 + 20.ToString("X"),
         //      ascii.GetBytes('O') 3 + 30.ToString("X"),
         //      ascii.GetBytes('G') 4 + 40.ToString("X"),
         //      ascii.GetBytes('R') 5 + 50.ToString("X"),
         //      ascii.GetBytes('A') 6 + 60.ToString("X"),
         //      ascii.GetBytes('M') 7 + 70.ToString("X"),
         //      ascii.GetBytes('M') 8 + 80.ToString("X"),
         //      ascii.GetBytes('E') 9 + 10.ToString("X"),
         //      ascii.GetBytes('D') 10 + 20.ToString("X"),
         //      ascii.GetBytes(' ') 11 + 30.ToString("X"),
         //      ascii.GetBytes('B') 12 + 40.ToString("X"),
         //      ascii.GetBytes('Y') 13 + 50.ToString("X"),
         //      ascii.GetBytes(' ') 14 + 60.ToString("X"),
         //      ascii.GetBytes('M') 15 + 70.ToString("X"),
         //      ascii.GetBytes('I') 16 + 80.ToString("X"),
         //      ascii.GetBytes('K') 17 + 10.ToString("X"),
         //      ascii.GetBytes('E') 18 + 20.ToString("X"),
         //      ascii.GetBytes(' ') 19 + 30.ToString("X"),
         //      ascii.GetBytes('W') 20 + 40.ToString("X"),
         //      ascii.GetBytes('I') 21 + 50.ToString("X"),
         //      ascii.GetBytes('E') 22 + 60.ToString("X"),
         //      ascii.GetBytes('R') 23 + 70.ToString("X"),
         //      ascii.GetBytes('I') 24 + 80.ToString("X"),
         //      ascii.GetBytes('N') 25 + 10.ToString("X"),
         //      ascii.GetBytes('G') 26 + 20.ToString("X")}

         //   PauseText = "Pause";

         //   PauseMusic();
         //   FadeDown(8);
         //   SwapPages();
         //   PauseBack = PushBackGr (XView + 120, 85, 80, 10);

         //   if (PauseBack != 0)
         //   {
         //      OutPalette (15.ToString("X"), 63, 63, 63);
         //      //SetFont (0, Bold + Shadow);
         //      CenterText (85, PauseText, 15.ToString("X"));
         //   }

         //   EndPause = false;
         //   Cheat = ' ';
         //   While (Key = kbP)
         //      While ( bKey - 128.ToString("X"))
         //         OldKey = Key;
         //   if (Key = kbTab)
         //   {
         //      do
         //      {
         //         if (Key != OldKey)
         //         {
         //            Ch = Key.ToString();
         //            if (Key < 128.ToString("X"))
         //            {
         //               Cheat = Cheat + Key;
         //               EndPause = (Ch == 0);
         //            }
         //            OldKey = Key;

         //            /** Add cheats here
         //            if (Cheat = kbT+kbE+kbS+kbT) or    { TEST }
         //            (Cheat = kb0+kb0+kb4+kb4) then  { 0044 - ShowRetrace }
         //         begin
         //           ShowRetrace := not ShowRetrace;
         //           EndPause := TRUE;
         //         end;
         //         if Cheat = kb0+kb3+kbE+kb8 then   { 03E8 - AddLife }
         //         begin
         //           AddLife;
         //           EndPause := TRUE;
         //         end;
         //         if Cheat = kbB+kb1+kb7+kb2 then   { B172 - 10000 Lives }
         //         begin
         //           Data.Lives[Player] := 10000;
         //           EndPause := TRUE;
         //         end;
         //         if Cheat = kb9+kbC+kb3+kb2 then    { 9C32 - Star }
         //         begin
         //           cdStar := 1;
         //           EndPause := TRUE;
         //         end;
         //         if Cheat = kbF+kb1+kbF+kb2 then    { 9C32 - Champ }
         //         begin
         //           cdChamp := 1;
         //           EndPause := TRUE;
         //         end;
         //         if Cheat = kbF+kbF+kbB+kb5 then    { FFB5 - Flower }
         //         begin
         //           cdFlower := 1;
         //           EndPause := TRUE;
         //         end;
         //         if Cheat = kbD+kb2+kb3+kb5 then   { D235 - Turbo mode }
         //         begin
         //           Turbo := not Turbo;
         //           EndPause := TRUE;
         //         end;
         //         if Cheat = kb7+kb6+kbD+kbD then   { 76DD - Record demo }
         //         begin
         //           RecordMacro;
         //           EndPause := TRUE;
         //         end;
         //         if Cheat = kbC+kb7+kbB+kb4 then   { C7B4 - Play demo }
         //         begin
         //           PlayMacro;
         //           EndPause := TRUE;
         //         end;
         //         if Cheat = kb2+kb0+kb8+kbD then   { 208D - Save demo (file: $.) }
         //         begin
         //           SaveMacro;
         //           EndPause := TRUE;
         //         end;

         //         if Cheat = kb1+kbU+kbP then   { 1UP }
         //         begin
         //         { AddLife; }
         //           if CheatsUsed and 1 = 0 then
         //           begin
         //             NewEnemy (tpLife, 0, XView div W, -1, 2, 0, 2);
         //             CheatsUsed := CheatsUsed or 1;
         //           end
         //           else
         //           begin
         //             NewEnemy (tpChamp, 1, (XView + Random (100)) div W,
         //               -1, 2 - Random (2), 0, 2);
         //             if Random (10) = 0 then
         //               CheatsUsed := CheatsUsed and not 1;
         //           end;
         //           EndPause := TRUE;
         //         end;
         //         if Cheat = kb2+kb3+kb0+kb5 then   { 2305 - next level }
         //         begin
         //           Passed := TRUE;
         //           Waiting := TRUE;
         //           TextCounter := 200;
         //           PipeCode [1] := 'ç';
         //           InPipe := TRUE;
         //           EndPause := TRUE;
         //         end;
         //         if Cheat = kbM+kbO+kbN+kbO then  { MONO }
         //         begin
         //           PaletteEffect := peBlackWhite;
         //           RefreshPalette (Palette);
         //           EndPause := TRUE;
         //         end;
         //         if (Cheat = kbE+kbG+kbA+kbM+kbO+kbD+kbE) then  { VGAMODE }
         //         begin
         //           PaletteEffect := peEGAMode;
         //           RefreshPalette (Palette);
         //           EndPause := TRUE;
         //         end;
         //         if (Cheat = kbV+kbG+kbA+kbM+kbO+kbD+kbE) or  { VGAMODE }
         //            (Cheat = kbC+kbO+kbL+kbO+kbR) then  { COLOR }
         //         begin
         //           PaletteEffect := peNoEffect;
         //           RefreshPalette (Palette);
         //           EndPause := TRUE;
         //         end;

         //         if (Cheat = kbC+kbR+kbE+kbD+kbI+kbT+kbS) then
         //         begin
         //           P := @CREDIT;
         //           Move (P^, PauseText, SizeOf (PauseText));
         //           for i := 1 to CRED_LEN do
         //             PauseText[i] := Chr ((Ord (PauseText[i]) - i) - $10 - (((i - 1) mod 8) shl 4));
         //           if PauseBack <> 0 then
         //             PopBackGr (PauseBack);
         //           PauseBack := PushBackGr (XView + 20, 85, 280, 10);
         //           CenterText (85, PauseText, $0F);
         //         end;
         //            **/
         //         }
         //      } while (EndPause)
         //   }
         //   if (PauseBack != 0)
         //      PopBackGr (PauseBack);
         //   SwapPages();

         //   FadeUp(8);
         //   Key = 255;
        }

      //-------------------------------------------------------------------
      // Builds the level from the using the already initialized buffers
      //-------------------------------------------------------------------
		static void BuildLevel()
		{
            Figures.InitSky(Buffers.Options.SkyType);
            Figures.InitWalls(Buffers.Options.WallType1, Buffers.Options.WallType2, Buffers.Options.WallType3);
            //This loop draws the ground on the Menu screen only
            for (int i = 0; i < Buffers.NH; i++)
               for (int j = 0; j < Buffers.NV; j++)
                  if (j == (Buffers.NV - 1))
                     FormMarioPort.formRef.DrawImage(i * Buffers.W, j * Buffers.H, Buffers.W, Buffers.H, Resources.GREEN_001);
            Figures.InitPipes(Buffers.Options.PipeColor);
            BackGr.InitBackGr(Buffers.Options.BackGrType, Buffers.Options.Clouds);
            if (Buffers.Options.Stars != 0)
               Stars.InitStars();
				
            Figures.BuildWorld();
		}

      //-------------------------------------------------------------------
      // main loop of the world to handle drawing, hiding, and showing of the
      // resources. When mario runs out of lives the thread is killed and cleaned
      // up. read: GAME OVER!
      //-------------------------------------------------------------------
      static void Restart()
      {
         FormMarioPort.formRef.ResetStack();

         TextStatus = false;
         //InitStatus();

         Blocks.InitBlocks();
         TmpObj.InitTempObj();
         Glitter.ClearGlitter();
         Enemies.ClearEnemies();

         FormMarioPort.formRef.ShowPage();

         Buffers.GameDone = false;
         Buffers.Passed = false;

         for (int i = Enemies.StartEnemiesAt * -1; i < Buffers.NH + Enemies.StartEnemiesAt; i++)
         {
            int j = (Buffers.XView / Buffers.W) + i;
            Enemies.StartEnemies(j, (short)(1 - 2 * System.Convert.ToByte(j > Players.MapX)));
         }

         //   SetYOffset (YBase);

         for (int i = 0; i < FormMarioPort.MAX_PAGE; i++)
         {
            //      DrawSky (XView, 0, NH * W, NV * H);

            //      StartClouds();

            for (int x = Buffers.XView / Buffers.W; x < Buffers.XView / Buffers.W + Buffers.NH; x++)
               for (int y = 0; y < Buffers.NV - 1; y++)
                  Figures.Redraw(x, y);

            BackGr.DrawBackGr(true);
            //ReadColorMap();

            if (Buffers.Options.Stars != 0)
               Stars.ShowStars();

            Enemies.ShowEnemies();
            if (!OnlyDraw)
               Players.DrawPlayer();

            FormMarioPort.formRef.ShowPage();
         }

         Buffers.Demo = Buffers.dmNoDemo;
         Waiting = false;

         //Palettes.NewPalette (P256*);
         //for (int i = 1; i < 100; i++)
         //{
         //Waterfalls()?
         //Palettes.BlinkPalette();
         //}

         Figures.SetSkyPalette();
         BackGr.DrawPalBackGr();
         //Palettes.InitGrass();

         if (OnlyDraw)
            return;

         //   Palettes.UnLockPal();
         //   FadeUp (64);
         //   Palettes.ReadPalette (Palette);

         TextStatus = Stat;// && !KeyBoard.PlayingMacro());

         uint counter = 0;
         do //until gamedone
         {
            //Console.WriteLine("Restart Loop");

            //if (!Keyboard.PlayingMacro)
            //{
            //   if (Key = 31) //'S'
            //   {
            //      Stat = !Stat;
            //      TextStatus = Stat;
            //      Keyboard.Key = 255;
            //   }
            //   if (Keyboard.Key = 16) //'Q'
            //   {
            //      if (Buffers.BeeperSound)
            //         Buffers.BeeperOff();
            //      else
            //      {
            //         Buffers.BeeperOn();
            //         Buffers.Beep (80);
            //      }
            //      Key = 255;
            //   }

            //   if (Key == 197 || Key == 198) //Pause/Break
            //   {
            //      Music.PauseMusic();
            //      //do
            //      //{
            //      //   while (Keyboard.Key = 197) {} //busy wait of some sort?
            //      //} while //(Keyboard.kbHit);
            //   }
            //   else
            //   {
            //      if (Keyboard.Key != 0)
            //      {
            //         Buffers.GameDone = true;
            //         Buffers.Passed = true;
            //      }
            //   }

            //   if (Buffers.TextCounter) //in 40..40+MAX_PAGE
            //      ShowObjects = false;

            //   HideGlitter();
            //   if (Options.Stars != 0)
            //      HideStars();
            //   if (ShowObjects)
            //      HideTempObj();
            //   if (ShowScore)
            //      HideTotalBack();
            //   ErasePlayer();
            //   if (ShowObjects)
            //   {
            //      HideEnemies();
            //      EraseBlocks();
            //   }
            //}

            // { Fade }; 
            Buffers.LavaCounter++;

            if (!Waiting)
               if (Buffers.Demo == Buffers.dmNoDemo)
               {
                  Enemies.MoveEnemies();

                  Players.MovePlayer();
               }
               else
                  Players.DoDemo();



            if (!Waiting)
            {
               if (Buffers.Passed)
               {
                  if (Buffers.Demo == Buffers.dmNoDemo || Players.InPipe)
                  {
                     Waiting = true;
                     Buffers.TextCounter = 0;
                  }
                  Buffers.TextCounter++;
                  if (!ShowScore && (Buffers.TextCounter >= 50 && Buffers.TextCounter < 50 + FormMarioPort.MAX_PAGE)) //in 50..50 + MAX_PAGE
                  {
                     //SetFont (0, Bold + Shadow);
                     //TXT.CenterText (20, Buffers.PlayerName [Buffers.Player], 30.ToString("X"));
                     //SetFont(1, Bold + Shadow);
                     //TXT.CenterText (40, "STAGE CLEAR!", 31);
                     if (Buffers.TextCounter == (50 + FormMarioPort.MAX_PAGE))
                        ShowScore = true;
                  }
               }
               else
                  if (Buffers.GameDone)
                  {
                     Buffers.data.lives[Buffers.Player]--;
                     Buffers.data.mode[Buffers.Player] = Buffers.mdSmall;
                     Buffers.TextCounter = 0;
                     Buffers.data.score[Buffers.Player] += Buffers.LevelScore;
                     Waiting = true;
                     Buffers.GameDone = false;
                  }
            }



            //if (Keyboard.Key = 25) //P
            //   Pause();

            if (ShowScore && (Buffers.TextCounter == 120) && (Buffers.LevelScore > 0))
            {
               int i = (int)(Buffers.LevelScore - 50);
               if (i < 0)
                  i = 0;
               Buffers.data.score[Buffers.Player] += Buffers.LevelScore - 1;
               Buffers.LevelScore = i;
               Buffers.TextCounter = 119;
               CountingScore = true;
            }
            else
               CountingScore = false;

            if (Waiting)
            {
               Buffers.TextCounter++;
               if (Buffers.data.lives[Buffers.Player] == 0)
               {
                  if (Buffers.TextCounter >= 100 && Buffers.TextCounter < 100 + FormMarioPort.MAX_PAGE) //in 100..100 + MAX_PAGE
                  {
                     //SetFont (0, Bold + Shadow);
                     //CenterText (20, Buffers.PlayerName[Buffers.Player], 30.ToString("X"));
                     //SetFont (1, Bold + Shadow);
                     //CenterText (40, "GAME OVER", 31);
                     ShowScore = true;
                  }
                  if (Buffers.TextCounter > 350)
                     Buffers.GameDone = true;
               }
               else
                  if (Buffers.Passed)
                  {
                     if (Buffers.TextCounter > 250)
                        Waiting = false;
                  }
                  else
                     if (Buffers.TextCounter > 100)
                        Buffers.GameDone = true;
            }
            TmpObj.MoveTempObj();
            Blocks.MoveBlocks();

            if (Keyboard.kbEsc)
               Buffers.QuitGame = true;

            MoveScreen();
            TmpObj.RunRemove();

            if (Buffers.Options.Horizon < Buffers.NV)
            {
               int j = Buffers.Options.Horizon;
               for (int i = 0 / Buffers.W; i < Buffers.NH; i++)
               {
                  int k = Buffers.XView / Buffers.W + (i + Buffers.LavaCounter / 8) % (Buffers.NH + 1);
                  if (Buffers.WorldMap[k, j] == '%')
                     Figures.Redraw(k, j);
               }
            }


            //FormMarioPort.formRef.ResetStack();

            if (ShowObjects)
            {
               Blocks.DrawBlocks();
               Enemies.ShowEnemies();
            }
            Players.DrawPlayer();

            if (ShowScore)
               ShowTotalBack();
            //if (TextStatus)
            //   Status.ShowStatus();
            if (ShowObjects)
               TmpObj.ShowTempObj();
            if (Buffers.Options.Stars != 0)
               Stars.ShowStars();
            Glitter.ShowGlitter();

            Buffers.LastXView[FormMarioPort.formRef.CurrentPage()] = Buffers.XView;

            //if (ShowRetrace)
            //   SetPalette(0, 0, 0, 0);

            FormMarioPort.formRef.ShowPage();

            //if (ShowRetrace)
            //   SetPalette( 0, 63, 63, 63);

            BackGr.DrawPalBackGr();

            //Palette.BlinkPalette();

            Music.PlayMusic();

            if (Players.InPipe && Keyboard.PlayingMacro)
               Buffers.GameDone = true;


            if (Players.InPipe && !Buffers.GameDone && !Waiting)
            {
               Enemies.StopEnemies();
               Glitter.ClearGlitter();
               //FadeDown(64);
               FormMarioPort.formRef.ClearPalette();
               //FormMarioPort.formRef.LockPal();
               //FormMarioPort.formRef.ClearVGAMem();

               switch (Players.PipeCode[1])
               {
                  case 'à':
                     FindPipeExit();
                     //Delay(100);
                     break;
                  case 'á':
                     //Swap();
                     FindPipeExit();
                     break;
                  case 'ç':
                     Buffers.GameDone = true;
                     //PlayWorld = true;
                     break;
               }

               Players.InitPlayer(Players.MapX * Buffers.W + Buffers.W / 2, (Players.MapY - 1) * Buffers.H, Buffers.Player);

               FormMarioPort.formRef.SetView(Buffers.XView, Buffers.YView);
               FormMarioPort.formRef.SetYOffset(YBase);

               for (int i = 0; i < FormMarioPort.MAX_PAGE; i++)
                  Buffers.LastXView[i] = Buffers.XView;

               if (Players.PipeCode[1] == 'à')
                  Restart();
               else
                  if (Players.PipeCode[1] == 'á')
                     BuildLevel();
            }

            System.Threading.Thread.Sleep(5);

            if (counter % 15 == 0)//
            {
               FormMarioPort.formRef.Invalidate();
               for (int x = Buffers.XView / Buffers.W; x < Buffers.XView / Buffers.W + Buffers.NH; x++)
                  for (int y = 0; y <= Buffers.NV; y++)
                     Figures.Redraw(x, y);
            }

            counter++;
            counter %= 100;


         } while (!Buffers.GameDone && !Buffers.QuitGame);
      }

   }
}
