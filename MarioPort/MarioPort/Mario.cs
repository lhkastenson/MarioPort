//-------------------------------------------------------------------
//Purpose: This File contains the main() of the program.
//         as well as handling the menu and Intro, Grabbing any commandline
//         data and preforming the demo.
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
   public static class Mario
   {
      public const int NUM_LEV = 6;
      public const int LAST_LEV = 2 * NUM_LEV - 1;
      public const int MAX_SAVE = 3;
      public const int WAIT_BEFORE_DEMO = 500;

      private static int P, l, m, n, wd, ht, xp;
      private static int NextNumPlayers, Selected;
      private static bool IntroDone, TestVGAMode, UpDate;
      private static int Counter;
      private static char MacroKey;
      private static byte Page;
      private static int NumOptions;

      private static Statuses Status;

      //-------------------------------------------------------------------
      // Handles the configuration for one session of the mario game
      //    Sound: bool, sound on or off
      //    SLine: bool
      //    Games: GameData[], the 3 different save games
      //    UseJS: bool, joystick support on or off
      //    JSDat: JoyRec, Data for the joystick (not implemented)
      //-------------------------------------------------------------------
      public class ConfigData
      {
         public bool Sound;
         public bool SLine;
         public Buffers.GameData[] Games = new Buffers.GameData[3];
         public bool UseJS;
         //public JoyRec JSDat;

         //-------------------------------------------------------------------
         // Default constructor for ConfigData, sets initial values
         //-------------------------------------------------------------------
         public ConfigData()
         {
            this.Sound = true;
            this.SLine = true;
            this.Games = new Buffers.GameData[3];
            UseJS = false; //make true after we have joystick implemented
         }

         //-------------------------------------------------------------------
         // Factory function for ConfigData to initialize an array of this class
         //-------------------------------------------------------------------
         public static ConfigData Create()
         {
            ConfigData configData = new ConfigData();
            for (int i = 0; i < 3; i++)
               configData.Games[i] = Buffers.GameData.Create();
            return configData;
         }
      }
      public static ConfigData ConfigFile;
      public static int GameNumber;
      public static int CurPlayer;
      public static bool Passed = false;
      public static bool EndGame;
      public static ConfigData Config = ConfigData.Create();

      public static bool MENU = true;
      //-------------------------------------------------------------------
      // enumerated type for the different statuses 
      //-------------------------------------------------------------------
      public enum Statuses
      {
         ST_NONE,
         ST_MENU,
         ST_START,
         ST_LOAD,
         ST_ERASE,
         ST_OPTIONS,
         ST_NUMPLAYERS
      }
      //-------------------------------------------------------------------
      // Pressing up during the menu selection
      //-------------------------------------------------------------------
      public static void Up()
      {
         if ( Selected == 1 )
            if ( Status == Statuses.ST_MENU )
               Selected = NumOptions;
         else
            ;//MacroKey = Keyboard.kbEsc;
         else
            Selected--;
      }
      //-------------------------------------------------------------------
      // Pressing down during the menu selection
      //-------------------------------------------------------------------
      public static void Down()
      {

      }

#if (DEBUG)
      //-------------------------------------------------------------------
      // halt the mouse (not implemented)
      //-------------------------------------------------------------------
      public static void MouseHalt()
      {
         //halt
      }
#endif
      //-------------------------------------------------------------------
      // starting a new game with new data
      //-------------------------------------------------------------------
      public static void NewData()
      {
         Buffers.data.lives = new int[] { 3, 3 };
         Buffers.data.coins = new int[] { 0, 0 };
         Buffers.data.score = new long[] { 0, 0 };
         Buffers.data.progress = new int[] { 0, 0 };
         Buffers.data.mode = new byte[] { Buffers.mdSmall, Buffers.mdSmall };
      }
      //-------------------------------------------------------------------
      // retrives the arguments given at the command line (not implemented)
      //-------------------------------------------------------------------
      public static string GetConfigName(string[] args)
      {
         //byte absolute len = S;
         string S = args[0];
         // Not sure if we absolutely need this function it's calls are in all the preprocessor
         // debug statements
         //S[S.Length - 2] = "C";
         //S[S.Length - 1] = "F";
         //S[S.Length - 0] = "G";
         return S;
      }

      //-------------------------------------------------------------------
      // Reads the commands given at the command line (not implemented)
      //-------------------------------------------------------------------
      public static void ReadConfig(string[] args)
      {
         ConfigData F;
         int j;
         string Name;
#if MENU
   		Assign(F, GetConfigName());
   		Reset(F);
   		Read(F, Config);
   		Close(F);
   		if IOResult != 0 
#endif
         //{
         //   NewData();
         //   for(int i = 0; i < MAX_SAVE - 1; i++)
         //      ConfigData.Games[i] = Buffers.data;  //pretty sure this needs to be Config.Games[i] but it errors out
         //   Config.SLine = true;
         //   Config.Sound = true;
         //   Config.UseJS = false;
         //   GameNumber = -1;
         //}
         Play.Stat = Config.SLine;
         //Buffers.BeeperSound = Config.Sound;
         //Name = args[0];
         //j = 0;
         //if(Name.Length > 9)
         //   Name = Name.Remove(1, Name.Length - 9);
         //for(int i = 0; i < Name.Length; i++)
         //Inc(j, Ord(Name[i].ToUpper));
         //if (j != 648)
         //   RunError(201); //more debugging stuff?
      }

      //-------------------------------------------------------------------
      // writes a config file out based on the data from the menu
      //-------------------------------------------------------------------
      public static void WriteConfig()
      {
         ConfigData F;
         Config.SLine = Play.Stat;
         Config.Sound = Buffers.BeeperSound;
#if MENU
   		Assign(F, GetConfigName());
   		ReWrite(F);
   		if (IOResult = 0)
   		{
   			Write(F, Config); //May need FileIO specific functions
   			Close(F);
   		}
#endif
      }

      //-------------------------------------------------------------------
      // calibrates the joystick (not implemented)
      //-------------------------------------------------------------------
      public static void CalibrateJoystick()
      {
         // TODO if we implement joystick
      }
      /**
   
        procedure CalibrateJoystick;
        begin
          Delay (100);
          WriteLn ('Rotate joystick and press button');
          WriteLn ('or press any key to use keyboard...');
          Delay (100);
          Key := #0;
          repeat
            Calibrate;
            Write (#13, 'X = ', Byte (jsRight) - Byte (jsLeft): 2,
                      '  Y = ', Byte (jsDown) - Byte (jsUp): 2);
          until jsButton1 or jsButton2 or (Key <> #0);
          WriteLn;
          if (Key <> #0) then
          begin
            jsEnabled := FALSE;
            ReadJoystick;
          end;
          Config.UseJS := jsEnabled;
          Config.JSDat := jr;
          Key := #0;
        end;
   
        **/

      //-------------------------------------------------------------------
      // Read the commands given at the command line
      //    args: string[], array of command line arguments
      //-------------------------------------------------------------------
      public static void ReadCmdLine(string[] args)
      {
         string S;
         for(int i = 0; i < args.Length; i++)
         {
            S = args[i];
            while(!string.IsNullOrEmpty(S))
            {
               if(S.Length >= 2 && (S[1] == '/' || S[1] == '-'))
               {
                  switch(S[2])
                  {
                     case 'S':
                        Play.Stat = true;
                        break;
                     case 'Q':
                        Buffers.BeeperOff();
                        break;
                     case 'J':
                        CalibrateJoystick();
                        break;
                     default:
                        break;
                  }
                  S = S.Remove(1, 2);
               }
               else
                  S = S.Remove(1, 1);
            }
         }
      }

      //-------------------------------------------------------------------
      // starts a demo based on the macro saved
      //-------------------------------------------------------------------
      public static bool Demo()
      {
         NewData();
         Enemies.Turbo = false;
         Buffers.data.progress[Buffers.plMario] = 5;
         //Keyboard.PlayMacro();
         return Play.PlayWorld(' ', ' ', Worlds.Level_6a(), Worlds.Options_6a(), Worlds.Options_6a(),
                 Worlds.Level_6b(), Worlds.Options_6b(), Worlds.Options_6b(), Buffers.plMario);
         //Keyboard.StopMacro();
      }

      //-------------------------------------------------------------------
      // starts up mario with the menu, if a specific amount of time passes
      // the demo will play until the user presses any key and stops the demo
      //-------------------------------------------------------------------
      public static void Intro()
      {
         int i = 0, j = 0;
         Status = Statuses.ST_NONE;
         Statuses OldStatus = Statuses.ST_NONE;
         Statuses LastStatus = Statuses.ST_NONE;
         string[] Menu = new String[5];
         uint[,] BG = new uint[FormMarioPort.MAX_PAGE + 1, 5];

         //nested procedures
         Page = (byte)FormMarioPort.formRef.CurrentPage();
         Status = Statuses.ST_NONE;
         TestVGAMode = false;
         GameNumber = -1;
         NextNumPlayers = Buffers.data.numPlayers;

         do //until IntroDone and (not TestVGAMode);
         {
            //if(FormMarioPort.formRef.TestVGAMode)
            //   FormMarioPort.InitVGA();
            TestVGAMode = false;
            IntroDone = false;
            NewData();

            Play.PlayWorld('0', '0', Worlds.Intro_0(), Worlds.Options_0(), Worlds.Options_0(),
                    Worlds.Intro_0(), Worlds.Options_0(), Worlds.Options_0(), Buffers.plMario);
            BackGr.InitBackGr(3, 0);

            //Palettes.OutPalette(0xA0, 35, 45, 50);
            //Palettes.OutPalette(0xA1, 45, 55, 60);

            //Palettes.OutPalette(0xEF, 30, 40, 30);
            //Palettes.OutPalette(0x18, 10, 15, 25);

            //Palettes.OutPalette(0x8D, 28, 38, 50);
            //Palettes.OutPalette(0x8F, 40, 50, 63);

            //for(int i = 1; i < 50; i++)
            //   Palettes.BlinkPalette();

            for(int p = 0; p < FormMarioPort.MAX_PAGE; p++)
            {
               for( i = 1; i >= 0; i--)
                  for( j = 1; j >= 0; j--)
                     for(int k = 1; k >= 0; k--)
                     {
                        FormMarioPort.formRef.DrawImage(38 + i + j, 29 + i + k, 108, 28, Resources.INTRO_000);
                        FormMarioPort.formRef.DrawImage(159 + i + j, 29 + i + k, 24, 28, Resources.INTRO_001);
                        FormMarioPort.formRef.DrawImage(198 + i + j, 29 + i + k, 84, 28, Resources.INTRO_002);
                     }
               BackGr.DrawBackGrMap(10 * Buffers.H + 6, 11 * Buffers.H - 1, 54, 0xA0);
               BackGr.DrawBackGrMap(10 * Buffers.H + 6, 11 * Buffers.H - 1, 55, 0xA1);
               BackGr.DrawBackGrMap(10 * Buffers.H + 6, 11 * Buffers.H - 1, 53, 0xA1);
               for( i = 0; i < Buffers.NH + 1; i++)
                  for( j = 0; j < Buffers.NV + 1; j++)
                     if((i == 0 || i == Buffers.NH - 1) || (j == 0 || j == Buffers.NV))
                        FormMarioPort.formRef.DrawImage(i * Buffers.W, j * Buffers.H, Buffers.W, Buffers.H, Resources.BLOCK_000);
               Players.DrawPlayer();
               FormMarioPort.formRef.ShowPage();
            }
            //UnlockPal();
            //Keyboard.Key = 0;
            //FadeUp(64);
            //ResetStack();

            //FillChar(BG, BG.Size(), 0);
            //FillChar(Menu, Menu.Size(), 0);
            //SetFont(0, Bold + Shadow);

            if(Status != Statuses.ST_OPTIONS)
            {
               OldStatus = Statuses.ST_NONE;
               LastStatus = Statuses.ST_NONE;
               Status = Statuses.ST_MENU;
               Selected = 1;
            }
            UpDate = true;
            Counter = 1;

            do //until IntroDone or (Counter = WAIT_BEFORE_DEMO);
            {
               if (UpDate || Status != OldStatus)
               {
                  if (Status != OldStatus)
                     Selected = 1;
                  switch (Status)
                  {
                     case Statuses.ST_MENU:
                        Menu[0] = "START";
                        Menu[1] = "OPTIONS";
                        Menu[2] = "END";
                        Menu[3] = "";
                        Menu[4] = "";
                        NumOptions = 3;
                        LastStatus = Statuses.ST_MENU;
                        break;
                     case Statuses.ST_OPTIONS:
                        if (Buffers.BeeperSound)
                           Menu[0] = "SOUND ON ";
                        else
                           Menu[0] = "SOUND OFF";
                        if (Play.Stat)
                           Menu[1] = "STATUSLINE OFF";
                        else
                           Menu[1] = "STATUSLINE OFF";
                        Menu[2] = "";
                        Menu[3] = "";
                        Menu[4] = "";
                        NumOptions = 2;
                        LastStatus = Statuses.ST_MENU;
                        break;
                     case Statuses.ST_START:
                        Menu[0] = "NO SAVE";
                        Menu[1] = "GAME SELECT";
                        Menu[2] = "ERASE";
                        Menu[3] = "";
                        Menu[4] = "";
                        NumOptions = 3;
                        LastStatus = Statuses.ST_MENU;
                        break;
                     case Statuses.ST_NUMPLAYERS:
                        Menu[0] = "ONE PLAYER";
                        Menu[1] = "TWO PLAYERS";
                        Menu[2] = "";
                        Menu[3] = "";
                        Menu[4] = "";
                        NumOptions = 3;
                        LastStatus = Statuses.ST_MENU;
                        break;
                     case Statuses.ST_LOAD:
                     case Statuses.ST_ERASE:
                        Menu[0] = "GAME #1 " + 7 + " ";
                        Menu[1] = "Game #2 " + 7 + " ";
                        Menu[2] = "Game #3 " + 7 + " ";
                        Menu[3] = "";
                        Menu[4] = "";

                        for ( i = 0; i < 3; i++)
                           if (Config.Games[i].progress[Buffers.plMario] == 0 &&
                              Config.Games[i].progress[Buffers.plLuigi] == 0)
                              Menu[i] = Menu[i] + "EMPTY";
                           else
                           {
                              m = Config.Games[i].progress[Buffers.plMario];
                              n = Convert.ToInt32((Config.Games[i].progress[CurPlayer] >= NUM_LEV));
                              if (n > 0)
                                 m -= NUM_LEV;
                              if (Config.Games[i].progress[Buffers.plLuigi] > m)
                              {
                                 m = Config.Games[i].progress[Buffers.plLuigi];
                                 Config.Games[i].progress[Buffers.plMario] = m;
                              }
                              Menu[i] = Menu[i] + "LEVEL " + (char)(m +
                                      Convert.ToInt32(Encoding.ASCII.GetBytes("0")) + 1) + " ";
                              if (n == 0)
                                 Menu[i] = Menu[i] + 7 + " ";
                              else
                                 Menu[i] = Menu[i] + "* ";
                              Menu[i] = Menu[i] + (char)(Config.Games[i].numPlayers +
                                      Convert.ToInt32(Encoding.ASCII.GetBytes("0"))) + 'P';
                           }
                        NumOptions = 3;
                        LastStatus = Statuses.ST_START;
                        break;
                     default:
                        break;
                  }
                  wd = 0;
                  xp = 0;

                  for ( i = 0; i < 5; i++)
                  {
                      j = TXT.TextWidth(Menu[i]);
                     if (j > wd)
                     {
                        wd = j;
                        //xp = CenterX(Menu[i]) / 4 * 4;
                     }
                     ht = 8;
                  }
                  OldStatus = Status;
                  UpDate = false;
               }
               MacroKey = '0';
               
                  if ( Keyboard.kbEsc)
                     if (Status == Statuses.ST_MENU)
                     {
                        IntroDone = true;
                        Buffers.QuitGame = true;
                     }
                     else
                        Status = LastStatus;

                  if ( Keyboard.kbUpArrow )
                     Up();

                  if ( Keyboard.kbDownArrow )
                     Down();

                  if ( Keyboard.kbSP || Keyboard.kbEnter )
                     switch (Status)
                     {
                        case Statuses.ST_MENU:
                           switch (Selected)
                           {
                              case 1:
                                 Status = Statuses.ST_START;
                                 break;
                              case 2:
                                 Status = Statuses.ST_OPTIONS;
                                 break;
                              case 3:
                                 IntroDone = true;
                                 Buffers.QuitGame = true;
                                 break;
                           }
                           break;
                        case Statuses.ST_START:
                           switch (Selected)
                           {
                              case 1:
                                 Status = Statuses.ST_NUMPLAYERS;
                                 break;
                              case 2:
                                 Status = Statuses.ST_LOAD;
                                 break;
                              case 3:
                                 Status = Statuses.ST_ERASE;
                                 break;
                           }
                           break;
                        case Statuses.ST_OPTIONS:
                           switch (Selected)
                           {
                              case 1:
                                 if (Buffers.BeeperSound)
                                    Buffers.BeeperOff();
                                 else
                                    Buffers.BeeperOn();
                                 break;
                              case 2:
                                 Play.Stat = !Play.Stat;
                                 break;
                           }
                           break;
                        case Statuses.ST_NUMPLAYERS:
                           switch (Selected)
                           {
                              case 1:
                                 NextNumPlayers = 1;
                                 IntroDone = true;
                                 break;
                              case 2:
                                 NextNumPlayers = 2;
                                 IntroDone = true;
                                 break;
                           }
                           break;
                        case Statuses.ST_LOAD:
                           GameNumber = Selected - 1;
                           Config.Games[GameNumber].numPlayers = 1;
                           if (Config.Games[GameNumber].progress[Buffers.plMario] == 0 &&
                                    Config.Games[GameNumber].progress[Buffers.plLuigi] == 0)
                              Status = Statuses.ST_NUMPLAYERS;
                           else
                           {
                              IntroDone = true;
                              NextNumPlayers = Config.Games[GameNumber].numPlayers;
                           }
                           break;
                        case Statuses.ST_ERASE:
                           NewData();
                           Config.Games[Selected - 1] = Buffers.data;
                           Config.Games[Selected - 1].numPlayers = 1;
                           GameNumber = -1;
                           break;
                     }

               //if(Keyboard.Key != 0)
               //{
               //   Counter = 0;
               //   Keyboard.Key = MacroKey;
               //   UpDate = true;
               //}

               for(int k = 0; k < 5; k++)
               {
                  if(BG[Page, k] != 0)
                     FormMarioPort.formRef.PopBackGr((ushort)BG[Page, k]);
               }

               for(int k = 0; k < 5; k++)
               {
                  if(Menu[k] != "")
                  {
                     m = xp;
                     n = 56 + 14 * k;
                     BG[Page, k] = FormMarioPort.formRef.PushBackGr(50, j, 220, ht);
                     if (k == Selected)
                        ;//TXT.WriteText(i - 12, j, 16, 5);
                     l = 15;
                     if(Menu[k].Length > 19 && Menu[k][10] == '*')
                        //l := 14 + (Counter and 1);
                        l = 14 + (Counter & l);
                     //SetPalette(14, 63, 61, 31);
                     //TXT.WriteText(i + 8, j, Menu[k], l);
                  }
               }
               FormMarioPort.formRef.ShowPage();
               //Palettes.BlinkPalette();
               FormMarioPort.formRef.ResetStack();

               System.Threading.Thread.Sleep(10);
               Counter++;
            } while(!IntroDone && Counter != WAIT_BEFORE_DEMO);

            //FadeDown(64);

            if(!IntroDone)
               Demo();
         } while(!IntroDone || TestVGAMode);

         if(GameNumber != -1)
            Buffers.data = Config.Games[GameNumber];

         Config.Games[Config.Games.Length - 1].numPlayers = NextNumPlayers;
      }
      //-------------------------------------------------------------------
      // displays the player's name (not implemented) 
      //-------------------------------------------------------------------
      static void ShowPlayerName(byte Player)
      {
         int iW, iH;
         //ClearPalette();
         //LockPal();
         //ClearVGAMem;
         //SetView(0, 0);
         iH = 13;
         for(int i = 0; i < FormMarioPort.MAX_PAGE; i++)
         {
            switch(Player)
            {
               case Buffers.plMario:
                  iW = 116;
                  FormMarioPort.formRef.DrawImage (160 - iW / 2, 85 - iH / 2, iW, iH, Resources.START_000);
                  break;
               case Buffers.plLuigi:
                  iW = 108;
                  FormMarioPort.formRef.DrawImage(160 - iW / 2, 85 - iH / 2, iW, iH, Resources.START_001);
                  break;
            }
            //FormMarioPort.ShowPage;
         }
         //NewPalette (P256*);
         //FormMarioPort.UnLockPal();
         //Palettes.ReadPalettes(Palette);
         //for(int i = 0; i < 100; i++)
         //   FormMarioPort.ShowPage();
         //Palettes.ClearPalette();
         //FormMarioPort.ClearVGAMem();
      }
      //-------------------------------------------------------------------
      // main function to drive the program, will determine if the game should
      // start in the menu or start up a world. after deciding that will initialize
      // the appropriate values 
      // finally, when the user either dies or quits the game, it will
      // clear all the buffers and stop the program.
      //-------------------------------------------------------------------
      public static void main()//string[] args)
      {
         try
         {
            int level = -1;

            //Keyboard.InitKeyBoard();
            Buffers.data.numPlayers = 1;
            //ReadConfig(args);
            //ReadCmdLine(args);

            //jr = Config.JSDat;
            //jsEnabled = jsDetected() && Config.UseJS();

#if DEBUG//asm stuff :(
            /**
{$IFDEF DEBUG}
   MouseHaltAddr := @MouseHalt;
   asm
      xor   ax, ax
      int   33h
      inc   ax
      jnz   @End
      mov   al, 0Ch
      mov   cx, 0Ah
      les   dx, MouseHaltAddr
      int   33h
      @End:
   end;
{$ENDIF}
**/
#endif

            if (!FormMarioPort.formRef.DetectVGA())
            {
               Console.Write("VGA graphics adapter required");
               //   //halt (1);
            }

            //Keyboard.ResetKeyBoard();

            if (!FormMarioPort.formRef.InGraphicsMode)
               FormMarioPort.formRef.InitializeVGA();

#if DEBUG
            do
            {
#endif
               FormMarioPort.formRef.ClearVGAMem();

               Players.InitPlayerFigures();
               Enemies.InitEnemyFigures();

               EndGame = false;

               if (MENU)
                  Intro();

               //Randomize();

               if (Buffers.data.numPlayers == 2)
                  if (Buffers.data.progress[Buffers.plMario] > Buffers.data.progress[Buffers.plLuigi])
                     Buffers.data.progress[Buffers.plLuigi] = Buffers.data.progress[Buffers.plMario];
                  else
                     Buffers.data.progress[Buffers.plMario] = Buffers.data.progress[Buffers.plLuigi];

               Buffers.data.lives[Buffers.plMario] = 3;
               Buffers.data.lives[Buffers.plLuigi] = 3;
               Buffers.data.coins[Buffers.plMario] = 0;
               Buffers.data.coins[Buffers.plLuigi] = 0;
               Buffers.data.score[Buffers.plMario] = 0;
               Buffers.data.score[Buffers.plLuigi] = 0;
               Buffers.data.mode[Buffers.plMario] = Buffers.mdSmall;
               Buffers.data.mode[Buffers.plLuigi] = Buffers.mdSmall;

               do //until EndGame or QuitGame 
               {
                  if (Buffers.data.numPlayers == 1)
                     Buffers.data.lives[Buffers.plLuigi] = 0;
                  for (CurPlayer = Buffers.plMario; CurPlayer < Buffers.data.numPlayers; CurPlayer++)
                  {
                     if (!(EndGame || Buffers.QuitGame))
                        if (Buffers.data.lives[CurPlayer] >= 1)
                        {
                           //Turbo = (ConfigData.Games[GameNumber].progress[CurPlayer] >= NUM_LEV);
                           if (Buffers.data.progress[CurPlayer] > LAST_LEV)
                              Buffers.data.progress[CurPlayer] = NUM_LEV;
#if MENU
      						ShowPlayerName (CurPlayer);
#endif

                           if (Keyboard.kb1)
                              level = 0;
                           else if (Keyboard.kb2)
                              level = 1;
                           else if (Keyboard.kb3)
                              level = 2;
                           else if (Keyboard.kb4)
                              level = 3;
                           else if (Keyboard.kb5)
                              level = 4;
                           else if (Keyboard.kb6)
                              level = 5;
                           //else if (level == -1) return;
                           else break;

                           //switch(Buffers.data.progress[CurPlayer] % NUM_LEV)
                           switch (level)
                           {
                              case 0:
                                 Passed = Play.PlayWorld('x', '1', Worlds.Level_1a(),
                                        Worlds.Options_1a(), Worlds.Opt_1a(),
                                        Worlds.Level_1b(), Worlds.Options_1b(),
                                        Worlds.Options_1b(), Convert.ToByte(CurPlayer));
                                 break;
                              case 1:
                                 Passed = Play.PlayWorld('x', '2', Worlds.Level_2a(),
                                        Worlds.Options_2a(), Worlds.Opt_2a(),
                                        Worlds.Level_2b(), Worlds.Options_2b(),
                                        Worlds.Options_2b(), Convert.ToByte(CurPlayer));
                                 break;
                              case 2:
                                 Passed = Play.PlayWorld('x', '3', Worlds.Level_3a(),
                                        Worlds.Options_3a(), Worlds.Opt_3a(),
                                        Worlds.Level_3b(), Worlds.Options_3b(),
                                        Worlds.Options_3b(), Convert.ToByte(CurPlayer));
                                 break;
                              case 3:
                                 Passed = Play.PlayWorld('x', '4', Worlds.Level_5a(),
                                        Worlds.Options_5a(), Worlds.Opt_5a(),
                                        Worlds.Level_5b(), Worlds.Options_5b(),
                                        Worlds.Options_5b(), Convert.ToByte(CurPlayer));
                                 break;
                              case 4:
                                 Passed = Play.PlayWorld('x', '5', Worlds.Level_6a(),
                                        Worlds.Options_6a(), Worlds.Opt_6a(),
                                        Worlds.Level_6b(), Worlds.Options_6b(),
                                        Worlds.Options_6b(), Convert.ToByte(CurPlayer));
                                 break;
                              case 5:
                                 Passed = Play.PlayWorld('x', '6', Worlds.Level_4a(),
                                         Worlds.Options_4a(), Worlds.Opt_4a(),
                                        Worlds.Level_4b(), Worlds.Options_4b(),
                                        Worlds.Options_4b(), Convert.ToByte(CurPlayer));
                                 break;
                              default:
                                 EndGame = true;
                                 break;
                           }
                           //Console.WriteLine("I want to play a world");
                           if (Passed)
                              Buffers.data.progress[CurPlayer]++;
                           if (Buffers.QuitGame)
                           {
                              EndGame = true;
#if MENU
      							QuitGame = false;
#endif
                           }
                        }
                  }
               } while (!(EndGame || Buffers.QuitGame || (Buffers.data.lives[Buffers.plMario] +
                  Buffers.data.lives[Buffers.plLuigi] == 0)));

               if (GameNumber != -1)
                  Config.Games[GameNumber] = Buffers.data;
#if MENU
      	} while (!QuitGame)
#endif
               WriteConfig();
            } while (!Buffers.QuitGame);
         }
         catch (Exception e)
         {
            Console.WriteLine(e);
            System.Threading.Thread.ResetAbort();
         }

      }
   }
}
