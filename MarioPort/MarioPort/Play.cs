using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarioPort
{
   public static class Play
   {
      public static bool Stat = false;
      public static bool ShowRetrace = false;

      public static int CheatsUsed = 0;

      public static bool PlayWorld(char N1, char N2, byte[,] Map1, Buffers.WorldOptions Opt1, Buffers.WorldOptions Opt1b, 
						  byte[,] Map2, Buffers.WorldOptions Opt2, Buffers.WorldOptions Opt2b, byte Player)
	   {
		   int i, j, k, x, y;
         bool Waiting;
	      bool TextStatus;
	      int[] TotalBackGrAddr = new int[FormMarioPort.MAX_PAGE];
	      bool ShowScore, CountingScore, ShowObjects, OnlyDraw;
         //PlayWorld = false;
         //Key = 0;

         //SetYOffset (YBase);
         //SetYStart (18.ToString("X"));
         //SetYEnd (125.ToString("X"));

         //ClearPalette();
         //LockPal();
         //ClearVGAMem();

         //TextCounter = 0;

         //WorldNumber = N1 + '-' + N2;
         //OnlyDraw = ((N1 = 0) && (N2 = 0));

         //ShowObjects = true;

         //InPipe = false;
         //PipeCode = '  ';
         //Demo = dmNoDemo;

         //InitLevelScore();

         //FillChar (TotalBackGrAddr, TotalBackGrAddr.Size(), 0);
         //ShowScore = false;

         //if (! Turbo)
         //{
         //   ReadWorld (Map2, WorldMap, Opt2);
         //   Swap();
         //   ReadWorld (Map1, WorldMap, Opt1);
         //}
         //else
         //{
         //   ReadWorld (Map2, WorldMap, Opt2b);
         //   Swap();
         //   ReadWorld (Map1, WorldMap, Opt1b);
         //}

         //Options.InitPlayer (InitX, InitY, Player);
         //Options.MapX = InitX;
         //Options.Mapy = InitY;

         //XView = 0;
         //YView = 0;

         //FillChar (LastXView, LastXView.Size(), 0);
         //SetView (XView, YView);

         //   SetYOffset(YBase);

         //   ClearEnemies();
         //   ClearGlitter();
         //   FadeDown(64);
         //   ClearPalette();
         //   ClearVGAMem();
         //   StopMusic();
         //}
         return true;
         //http://en.wikipedia.org/wiki/Return_statement#Syntax
         //"In Pascal there is no return statement." ...wat!?
		}
		public static void MoveScreen()
		{
         //int Scroll;
         //int N1, N2;
         //int i, j, OldX, NewX, Page;
         //Random random = new Random();
         //int randomNumber;
         //Page = CurrentPage;
         //Scroll = XView - LastXView[Page];
         //if (!EarthQuake)
         //   SetView(XView, YView);
         //else
         //{
         //   EarthQuakeCounter++;
         //   if (EarthQuakeCounter > 0)
         //      EarthQuake = false;
         //   int Rand1 = randomNumber.Next(0, 2);
         //   int Rand2 = randomNumber.Next(0, 2);
         //   SetView (XView, YView + Rand1 - Rand2;				
         //}
         //if (Scroll < 0)
         //   StartEnemies ((XView / W) - StartEnemiesAt, 1);
         //else if (Scroll > 0)
         //   StartEnemies((XView / W) + NH + StartEnemiesAt(), -1);
			
         //// Need to find out what else is in "Options"
         //i = Options.Horizon;
         //Options.Horizon = i + GetYOffset() - YBASE;
         //DrawBackGr(false);
         //Options.Horizon = i;
			
         //if (Scroll > 0)
         //   for (int j = LastXView[Page]; j < XView; j++)
         //   {
         //      i = j - W - W;
         //      if (i >= 0)
         //         PutPixel(i, 0, 0);
         //      i = W - j % W - 1;
         //      Redraw (j / W + NH + 1, i);
         //   }
				
         //   if (Scroll < 0)
         //      for (int j = LastXView[Page]; j > XView; j--);
         //      {
         //         i = W - j % W - 1;
         //         Redraw(j / W - 1, i);
         //      }
		}
		
	
	   public static void FindPipeExit()
	   {
         //int i, j;
         //for (int i = 0; i < Options.XSize - 1; i++)
         //   for (int j = 0; j < NH; j++)
         //      if (i != MapX || k != MapY)
         //      {
         //         if (WorldMap*[i,j] in ['a' .. 'i'] &&
         //            WorldMap*[i + 1, j] = PipeCode [2]) //need to figure out how to do this.
         //         {
         //            MaxX = i;
         //            MapY = j;
         //            XView = Succ (i - NH / 2) * W;
         //            if (XView > (Options.XSize - NH) * W)
         //               XView = (Options.XSize - NH) * W
         //            if (XView < 0)
         //               XView = 0;
         //         }
         //      }
	   }
	   
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
	
	   public static void ShowTotalBack()
	   {
         //if (Passed && CountingScore)
         //   Beep (4 * 880);
         //TotalBackGrAddr[CurrentPage] = PushBackGr (XView + 160, 120, 120, 8);
         //if (Passed && CoutingScore)
         //   Beep (2 * 880);
         //WriteTotalScore();
         //if (Passed && CountingScore)
         //   Beep(0);
	   }
	   public static void HideTotalBack()
	   {
         //int Page = CurrentPage;
         //if (TotalBackGrAddr[Page] != 0)
         //   PopBackGr (TotalBackGrAddr[CurrentPage]);
         //TotalBackGrAddr[Page] = 0;
	   }

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
		static void BuildLevel()
		{
            //InitSky (SkyType);
            //InitWalls (WallType1, WallType2, WallType3);
            //InitPipes (PipeColor);
            //InitBackGr (BackGrType, Clouds);
            //if (Stars != 0)
            //   InitStars();
				
            //BuildWorld();
		}
		
		static void Restart()
		{
      //   ResetStack();
			
      //   TextStatus = false;
      //   InitStatus();
			
      //   InitBlocks();
      //   InitTempObj();
      //   ClearGlitter();
      //   ClearEnemies();
			
      //   ShowPage();
			
      //   GameDone = false;
      //   Passed = false;
			
      //   for (int i = StartEnemiesAt * -1; i < NH + StartEnemiesAt; i++)
      //   {
      //      j = (XView / W) + i;
      //      StartEnemies (j, 1 - 2 * System.Text.ASCIIEncoding.GetBytes(j > MapX));
      //   }
			
      //   SetYOffset (YBase);
			
      //   for (int i = 0; i < MAX_PAGE; i++)
      //   {
      //      DrawSky (XView, 0, NH * W, NV * H);
				
      //      StartClouds();
				
      //      for (int x = XView / W - 1; x < XView / W + NH; x++)
      //         for (int y = 0; y < NV - 1; y++)
      //            Redraw (x, y);
					
      //      DrawBackGr(true);
      //      ReadColorMap();
				
      //      if (Options.Stars != 0)
      //         ShowStars();
					
      //      ShowEnemies(0;
      //      if (! OnlyDraw)
      //         DrawPlayer();
      //      ShowPage();				
      //   }
			
      //   Demo = dmNoDemo;
      //   Waiting = false;
			
      //   NewPalette (P256*);
      //   for (int i = 1; i < 100; i++)
      //   {
      //      //Waterfalls()?
      //      BlinkPalette();
      //   }
			
      //   SetSkyPalette();
      //   DrawPalBackGr();
      //   InitGrass();
			
      //   if (OnlyDraw)
      //      Exit();
				
      //   UnLockPal();
      //   FadeUp (64);
      //   Palettes.ReadPalette (Palette);
			
      //   TextStatus = (Stat && !PlayingMacro);
			
      //   do //until gamedone
      //   {
      //      if (!PlayingMacro)
      //      {
      //         if (Key = 31) //'S'
      //         {
      //            Stat = !Stat;
      //            TextStatus = Stat;
      //            Key = 255;
      //         }
      //         if (Key = 16) //'Q'
      //         {
      //            if (BeeperSound)
      //               BeeperOff();
      //            else
      //            {
      //               BeeperOn();
      //               Beep (80);
      //            }
      //            Key = 255;
      //         }
					
      //         if (Key == 197 || Key == 198) //Pause/Break
      //         {
      //            PauseMusic();
      //            do
      //            {
      //               while (Key = 197) {} //busy wait of some sort?
      //            } while kbHit;
      //         }
      //         else
      //         {
      //            if (Key != 0)
      //            {
      //               GameDone = true;
      //               Passed = true;
      //            }
      //         }
					
      //         if (TextCounter) //in 40..40+MAX_PAGE
      //            ShowObjects = false;
						
      //         HideGlitter();
      //         if (Options.Stars != 0)
      //            HideStars();
      //         if (ShowObjects)
      //            HideTempObj();
      //         if (ShowScore)
      //            HideTotalBack();
      //         ErasePlayer();
      //         if (ShowObjects)
      //         {
      //            HideEnemies();
      //            EraseBlocks();
      //         }
      //      }
				
      //      // { Fade }; 
      //      LavaCounter++;
				
      //      if (!Waiting)
      //         if (Demo == dmNoDemo)
      //         {
      //            MoveEnemies();
      //            MovePlayer();
      //         }
      //         else
      //            DoDemo();
				
      //      if (!Waiting)
      //      {
      //         if (Passed)
      //         {
      //            if (Demo = dmNoDemo || InPipe)
      //            {
      //               Waiting = true;
      //               TextCounter = 0;
      //            }
      //            TextCounter++;
      //            if (!ShowScore && (TextCounter)) //in 50..50 + MAX_PAGE
      //            {
      //               //SetFont (0, Bold + Shadow);
      //               CenterText (20, PlayerName [Player], 30.ToString("X"));
      //               //SetFont(1, Bold + Shadow);
      //               CenterText (40, "STAGE CLEAR!", 31);
      //               if (TextCounter = 50 + MAX_PAGE)
      //                  ShowScore = true;
      //            }
      //         }
      //         else
      //            if (GameDone)
      //            {
      //               Data.Lives[Player]--;
      //               Data.Mode[Player = mdSmall;
      //               TextCounter = 0;
      //               Data.Score[Player] += LevelScore;
      //               Waiting = true;
      //               GameDone = false;
      //            }
      //      }
				
      //      if (Key = 25) //P
      //         Pause();
					
      //      if (ShowScore && (TextCounter == 120) && (LevelScore > 0))
      //      {
      //         i = LevelScore - 50;
      //         if (i < 0)
      //            i = 0;
      //         Data.Score[Player] += LevelScore - 1;
      //         LevelScore = i;
      //         TextCounter = 119;
      //         CountingScore = true;
      //      }
      //      else
      //         CountingScore = false;
				
      //      if (Waiting)
      //      {
      //         TextCounter++;
      //         if (Data.Lives[Player] == 0)
      //         {
      //            if (TextCounter) //in 100..100 + MAX_PAGE
      //            {
      //               //SetFont (0, Bold + Shadow);
      //               CenterText (20, PlayerName[Player], 30.ToString("X"));
      //               //SetFont (1, Bold + Shadow);
      //               CenterText (40, "GAME OVER", 31);
      //               ShowScore = true;
      //            }
      //            if (TextCounter > 350)
      //               GameDone = true;
      //         }
      //         else
      //            if (Passed)
      //            {
      //               if (TextCounter > 250)
      //                  Waiting = false;
      //            }
      //            else
      //               if (TextCounter > 100)
      //                  GameDone = true;
      //      }
      //      MoveTempObj();
      //      MoveBlocks();
				
      //      if (Key == kbEsc || Key == 129)
      //         QuitGame = true;
					
      //      MoveScreen();
      //      RunRemove();
				
      //      if (Options.Horizon < NV)
      //      {
      //         j = Options.Horizon - 1;
      //         for (int i = 0 / W; i < NH; i++)
      //         {
      //            k = XView / W + (i + LavaCounter / 8) % (NH + 1);
      //            if (WorldMap * [k, j] = '%')
      //               Redraw(k, j);
      //         }
      //      }
				
      //      ResetStack();
				
      //      if (ShowObjects)
      //      {
      //         DrawBlocks();
      //         ShowEnemies();
      //      }
      //      DrawPlayer();
				
      //      if (ShowScore)
      //         ShowTotalBack();
      //      if (TextStatus)
      //         ShowStatus();
      //      if (ShowObjects)
      //         ShowTempObj();
      //      if (Options.Stars != 0)
      //         ShowStars();
      //      ShowGlitter();
				
      //      LastXView[CurrentPage] = XView;
				
      //      if (ShowRetrace)
      //         SetPalette(0, 0, 0, 0);
					
      //      ShowPage();
				
      //      if (ShowRetrace)
      //         SetPalette( 0, 63, 63, 63);
					
      //      DrawPalBackGr();
				
      //      BlinkPalette();
				
      //      PlayMusic();
				
      //      if (InPipe && PlayingMacro)
      //         GameDone = true;
					
      //      if (InPipe && !GameDone && !Waiting)
      //      {
      //         StopEnemies();
      //         ClearGlitter();
      //         FadeDown(64);
      //         ClearPalette();
      //         LockPal();
      //         ClearVGAMem();
					
      //         switch (PipeCode[1])
      //         {
      //            case 'à'
      //               FindPipeExit();
      //               Delay(100);
      //               break;
      //            case 'á'
      //               Swap();
      //               FindPipeExit();
      //               break;
      //            case 'ç'
      //               GameDone = true;
      //               PlayWorld = true;
      //               break;
      //         }
					
      //         InitPlayer (MaxX * W + W / 2, (MapY - 1) * H, Player);
					
      //         SetView (XView, YView);
      //         SetYOffset (YBase);
					
      //         for (int i = 0; i < MAX_PAGE); i++)
      //            LastXView[i] = XView;
						
      //         if PipeCode[1] == 'à'
      //            Restart();
      //         else
      //            if (PipeCode[1] == 'á')
      //               BuildLevel();
					
      //      }
				
				
      //   } while (GameDone || QuitGame)

   }

   }
}
