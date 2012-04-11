/**  uses
    CPU286,
    Play,
    Players,
    Enemies,
    Buffers,
    VGA256,
    Worlds,
    BackGr,
    KeyBoard,
    Joystick,
    Figures,
    Palettes,
    Txt,
    Crt,
    Dos;
**/
namespace MarioPort
{
public class Mario
{
	public const int NUM_LEV = 6;
	public const int LAST_LEV = 2 * NUM_LEV -1;
	public const int MAX_SAVE = 3;
	public const int WAIT_BEFORE_DEMO = 500;
	public struct ConfigData
	{
		public bool Sound;
		public bool SLine;
		public buffers.GameData[] Games = new buffers.GameData[MAX_SAVE-1];
		public bool UseJS;
		//public JoyRec JSDat;
	}
	public ConfigData ConfigFile = "file";
	public int GameNumber;
	public int CurPlayer;
	public int Passed;
	public bool EndGame;
	public ConfigData Config;
	
#if (DEBUG)
	public void MouseHalt()
	{
		//halt
	}
#endif

/**

  {$I Block.$00}

  {$I Intro.$00}
  {$I Intro.$01}
  {$I Intro.$02}

  {$I Start.$00}
  {$I Start.$01}
**/

	public void NewData()
	{
      buffers.data.lives = new int[] {3, 3};
      buffers.data.coins = new int[] {0, 0};
      buffers.data.score = new long[] {0, 0};
      buffers.data.progress = new int {0, 0};
      buffers.data.mode = new byte[] {buffers.mdSmall, buffers.mdSmall};

   }

	public string GetConfigName()
	{
		string S;
		byte len;
		S = ParamStr(0);
		S[len - 2] = 'C';
		S[len - 1] = 'F';
		S[len - 0] = 'G';
		return S;
	}
	
	public void ReadConfig()
	{
		int i, j;
		ConfigFile F;
		string Name;
#if MENU
		Assign(F, GetConfigName);
		Reset(F);
		Read(F, Config);
		Close(F);
		if IOResult != 0 
#endif
		{
			NewData();
			for (int i = 0; i < MAX_SAVE - 1; i++)
				Config.Games[i] = Data;
			Config.SLine = true;
			Config.Sound = true;
			Config.UseJS = false;
			GameNumber = -1;
		}
		Config.Play.Stat = buffers.SLine;
		Config.Buffers.BeeperSound = Sound;
		Name = ParamStr(0);
		j = 0;
		if (Name.Length() > 9)
			Name = Name.Remove(1, Name.Length() - 9);
		for(int i = 0; i < Name.Length(); i++)
			Inc(j, Ord(Name[i].ToUpper()));
		if (j != 648)
			RunError(201);
	}
	
	public void WriteConfig()
	{
		ConfigFile F;
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
	
	public void CalibrateJoystick
	{
	// TODO if we implement joystick
	}
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
  
  public void ReadCmdLine()
  {
	int i, j;
	string S;
	for(int i = 0; i < ParamCount; i++)
	{
		S = ParamStr(i);
		while (!string.IsNullOrEmpty(S))
		{
			if (S.Length >= 2 && (S[l] == '/' || S[l] == '-'))
			{
				switch(S[2])
				{
					case 'S':
						Play.Stat = true;
						break;
					case 'Q':
						BeeperOff();
						break;
					case 'J':
						CalibrateJoystick();
						break;
					default:
						break;
				}
				Delete(S, 1, 2);
			}
			else
				Delete(S, 1, 1);
		}
	}
  }

	public void Demo()
	{
		NewData();
		TurboType Turbo = false;
		Data.Progress[plMario] = 5;
		PlayMacro();
		PlayWorld(' ', ' ', @Level_6a*, @Options_6a*, @Options_6a*,
				  @Level_6b*, @Options_6b*, @Options_6b*, plMario);
		StopMacro();
	}
	public void Intro()
	{
		int, P, i, j, k l, wd, ht, xp;
		int NextNumPlayers, Selected;
		bool IntroDone, TestVGAMode, Update;
		int Counter;
		char MacroKey;
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
		Statuses Status, OldStatus, LastStatus;
		string[] Menu = new String[5];
		uint[,] BG = new uint[MAX_PAGE, 4];
		int NumOptions;
		//nested procedures
		void Up()
		{
			if (Selected = 1)
			{
				if (Status = ST_MENU)
					Selected = NumOptions;
				else
					MacroKey = kbEsc;
			}
			else
				Selected--;
		}
		
		void Down()
		{
			if (Selected = NumOptions)
			{
				if (Status = ST_MENU)
					Selected = 1;
				else
					MacroKey = kbEsc
			}
			else
				Selected++;
		}
		Page = CurrentPage;
		Status = ST_NONE;
		TestVGAMode = false;
		GameNumber = -1;
		NextNumPlayers = Data.NumPlayers;
		
		do //until IntroDone and (not TestVGAMode);
		{
			if (TestVGAMode)
				InitVGA;
			TestVGAMode = false;
			IntroDone = false;
			NewData();
			
			PlayWorld(#0, #0, @Intro*, @Options_0*, @Options_0*,
					  @Intro_0*, @Options_0*, @Options_0*, plMario);
			InitBackGr(3,);
			
			OutPalette($A0, 35, 45, 50);
			OutPalette($A1, 45, 55, 60);
			
			OutPalette($EF, 30, 40, 30);
			OutPalette($18, 10, 15, 25);
			
			OutPalette($8D, 28, 38, 50);
			OutPalette($8F, 40, 50, 63);
			
			for (int i = 1; i < 50; i++)
				BlinkPalette();
				
			for (int p = 0; p < MAX_PAGE; p++)
			{
				for (int i = 1; i = 0; i--)
					for (int j = 1; j = 0; j--)
						for (int k = 1; k = 0; k--)
						{
							DrawImage (38 + i + j, 29 + i + k, 108, 28, @Intro000^);
							DrawImage (159 + i + j, 29 + i + k, 24, 28, @Intro001^);
							DrawImage (198 + i + j, 29 + i + k, 84, 28, @Intro002^);
						}
				DrawBackGrMap (10 * H + 6, 11 * H - 1, 54, $A0);
				DrawBackGrMap (10 * H + 6, 11 * H - 1, 55, $A1);
				DrawBackGrMap (10 * H + 6, 11 * H - 1, 53, $A1);
				for (int i = 0; i < NH - 1; i++)
					for (int j = 0; j < NV -1; j++)
						if ((i == 0 || i == NH - 1) || (j == 0 || j == NV - 1))
							DrawImage (i * W, j * H, W, H, @Block000*);
				DrawPlayer();
				ShowPage();
			}
			UnlockPal();
			Key = #0;
			FadeUp (64);
			ResetStack();
			
			FillChar (BG, BG.Size(), 0);
			FillChar (Menu, Menu.Size(), 0);
			SetFont (0, Bold + Shadow);
			
			if (Status != ST_OPTIONS)
			{
				OldStatus = ST_NONE;
				LastStatus = ST_NONE;
				Status = ST_MENU;
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
						case ST_MENU:
							Menu[1] = "START";
							Menu[2] = "OPTIONS";
							Menu[3] = "END";
							Menu[4] = "";
							Menu[5] = "";
							NumOptions = 3;
							LastStatus = ST_MENU;
							break;
						case ST_OPTIONS:
							if (BeeperSound)
								Menu[1] = "SOUND ON ";
							else
								Menu[1] = "SOUND OFF";
							if (Play.Stat)
								Menu[2] = "STATUSLINE OFF";
							else
								Menu[2] = "STATUSLINE OFF";
							Menu[3] = "";
							Menu[4] = "";
							Menu[5] = "";
							NumOptions = 2;
							LastStautus = ST_MENU;
							break;
						case ST_START:
							Menu[1] = "NO SAVE";
							Menu[2] = "GAME SELECT";
							Menu[3] = "ERASE";
							Menu[4] = "";
							Menu[5] = "";
							NumOptions = 3;
							LastStatus = ST_MENU;
							break;
						case ST_NUMPLAYERS:
							Menu[1] = "ONE PLAYER";
							Menu[2] = "TWO PLAYERS";
							Menu[3] = "";
							Menu[4] = "";
							Menu[5] = "";
							NumOptions = 3;
							LastStatus = ST_MENU;
							break;
						case ST_LOAD, ST_ERASE:
							Menu[1] = "GAME #1 '#7' ";
							Menu[2] = "Game #2 '#7' ";
							Menu[3] = "Game #3 '#7' ";
							Menu[4] = "";
							Menu[5] = "";
							for (int i = 0; i < 3; i++)
								if (Config.Games.Progress[plMario] = 0 &&
									Config.Game.Progress[plLuigi] = 0)
									Menu[i] = Menu[i] + "EMPTY";
								else
								{
									j = Config.Games.Progress[plMario];
									k = byte(Progress[CurPlayer] >= NUM_LEV);
									if (k > 0)
										j -= NUM_LEV;
									if (Config.Games.Progress[plLuigi] > j)
									{
										j = Config.Games.Progress[plLuigi];
										Config.Games.Progress[plMario] = j;
									}
									Menu[i] = Menu[i] + "LEVEL " + Chr(j + 
											  Ord('0' + 1) + " ";
									if (k = 0)
										Menu[i] = Menu[i] + #7" "
									else
										Menu[i] = Menu[i] + "* ";
									Menu[i] = Menu[i] + Chr (NumPlayers + 
											  Ord ('0')) + 'P';
								}
								NumOptions = 3;
								LastStatus = ST_START;
							break;
						default:
							break;
					}
					wd = 0;
					xp = 0;
					for (int i = 0; i < 5; i++)
					{
						//j = TextWidth(Menu[i]);
						if (j > wd)
						{
							wd = j;
							//xp = CenterX(Menu[i]) / 4 * 4;
						}
						ht = 8;
					}
					OldStatus = Status;
					Update = false;
				}
				MacroKey = #0
				switch (Key)
				{
					case kbEsc:
						if (Status = ST_MENU)
						{
							IntroDone = true;
							QuitGame = true;
						}
						else
							Status = LastStatus;
						break;
					case kbUpArrow:
						Up();
						break;
					case kbDownArrow:
						Down();
						break;
					case kbSP, kbEnter:
						switch (Status)
						{
							case ST_MENU:
								switch(Selected)
								{
									case 1:
										Status = ST_START;
										break;
									case 2:
										Status = ST_OPTIONS;
										break;
									case 3:
										IntroDone = true;
										QuitGame = true;
										break;
								}
								break;
							case ST_START
								switch (Selected)
								{
									case 1:
										Status = ST_NUMPLAYERS;
										break;
									case 2:
										Status = ST_LOAD;
										break;
									case 3:
										Status = ST_ERASE;
										break;
								}
								break;
							case ST_OPTIONS
								switch (Selected)
								{
									case 1:
										if (BeeperSound)
											BeeperOff();
										else
											BeeperOn();
										break;
									case 2:
										Play.Stat = !Play.Stat;
										break;
								}
								break;
							case ST_NUMPLAYERS:
								switch (Selected)
								{
									case 1:
										NextNumPlayers = 1;
										IntroDone = true;
										break;
									case 2:
										NextNumPlayers = 2;
										IntroDone = true;
										break
								}
								break;
							case ST_LOAD:
								GameNumber = Selected - 1;
								Config.Games[GameNumber].NumPlayers = 1;
								if (Config.Games[GameNumber].Progress[plMario] = 0 &&
									Config.Games[GameNumber].Progress[plLuigi] = 0)
									Status = ST_NUMPLAYERS;
								else
								{
									IntroDone = true;
									NextNumPlayers = ConfigGames[GameNumber].NumPlayers;
								}
								break;
							case ST_ERASE:
								NewData();
								Config.Games[Selected - 1] = Data;
								Config.Games[Selected - 1].NumPlayers = 1;
								GameNumber = -1;
								break;
							case default:
								break;
						}
				}
				if (Key != #0)
				{
					Counter = 0;
					Key = MacroKey;
					Update = true;
				}
				
				for (int k = 0; k < 5; k++)
				{
					if (BG[Page, k] != 0)
						PopBackGr (BG[Page, k]);
				}
				
				for (int k = 0; k < 5; k++)
				{
					if (Menu[k] != "")
					{
						i = xp;
						j = 56 + 14 * k;
						BG[Page, k] = PuashBackGr (50, j, 220, ht);
						if (k = Selected)
							WriteText ( i - 12, j, #16, 5);
						l = 15;
						if (Menu[k].Length() > 19 && Menu[k][10] = '*')
						//l := 14 + (Counter and 1);
							l = 14 + (Counter & l)
						SetPalette (14, 63, 61, 31);
						WriteText (i + 8, j, Menu[k], l);
					}
				}
				ShowPage();
				BlinkPalette();
				ResetStack();
				
				Counter++;
			} while (!IntroDone && Counter != WAIT_BEFORE_DEMO)
			
			FadeDown (64);
			
			if (!IntroDone)
				Demo();
		} while (!IntroDone || TestVGAMode)
		
		if (Game != -1)
			Data = Config.Games[GameNumber];
		Data.NumPlayers = NextNumPlayers;
	}
	
	void ShowPlayerName(byte Player)
	{
		int iW, iH, i;
		ClearPalette();
		LockPal();
		ClearVGAMem;
		SetView (0,0);
		ih = 13;
		for (int i = 0; i < MAX_PAGE; i++)
		{
			switch (Player)
			{
				case plMario:
					iW = 116;
					DrawImage (160 - iW / 2, 85 - iH / 2, iW, iH, @Start000*);
					break;
				case plLuigi:
					iW = 108;
					DrawImage (160 - iW / 2, 85 - iH / 2, iW, iH, @Start001*);
					break;
			}
			ShowPage;
		}
		NewPalette (P256*);
		UnLockPal();
		Palettes.ReadPalettes (Palette);
		for (int i = 0; i < 100; i++)
			ShowPage();
		ClearPalette();
		ClearVGAMem();
	}
	InitKeyBoard();
	Data.NumPlayers = 1;
	ReadConfig();
	ReadCmdLine();
	
	jr = Config.JSDat;
	jsEnabled = jsDetected() && Config.UseJS();

#if //asm stuff :(
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

	if (!DetectVga)
	{
		System.out.WriteLine("VGA graphics adapter required");
		//halt (1);
	}
	
	ResetKeyBoard();
	
	if (!InGraphicsMode)
		InitVGA();
	
#if DEBUG
	do
	{
#endif
		ClearVGAMem();
		
		InitPlayerFigures();
		InitEnemyFigures();
		
		EndGame = false;
#if MENU
		Intro();
#endif
		Randomize();
		
		if (Data.NumPlayers = 2)
			if (Data.Progress[plMario] > Data.Progress[plLuigi])
				Data.Progress[plLuigi] = Data.Progress[plMario]
			else
				Data.Progress[plMario] = Data.Progress[plLuigi];
				
		Data.Lives[plMario] = 3;
		Data.Lives[plLuigi] = 3;
		Data.Coins[plMario] = 0;
		Data.CoinsplLuigi] = 0;
		Data.Score[plMario] = 0;
		Data.Score[plLuigi] = 0;
		Data.Mode[plMario] = mdSmall;
		Data.Mode[plLuigi] = mdSmall;
		
		do //until EndGame or QuitGame 
		{
			if (Data.NumPlayers = 1)
				Data.Lives[plLuigi] = 0;
			for (CurPlayer = plMario; Curplayer < Data.NumPlayers; Curplayer++)
			{
				if (!(EndGame || QuitGame))
					if (Data.Lives[CurPlayer] >= 1)
					{
						Turbo = (Data.Progress[CurPlayer >= NUM_LEV);
						if (Data.Progress[CurPlayer] > LAST_LEV)
							Data.Progress[CurPlayer] = NUM_LEV;
#if MENU
						ShowPlayerName (CurPlayer);
#endif
						switch (Data.Progress[CurPlayer] % NUM_LEV)
						{
							case 0:
								Passed = PlayWorld ('x', '1', @Level_1a*, 
										 @Options_1a*, @Opt_1a*,
										 @Level_1b*, @Options_1b*, 
										 @Options_1b*, CurPlayer);
								break;
							case 1:
								Passed = PlayWorld ('x', '2', @Level_2a*,
										 @Options_2a*, @Opt_2a*,
										 @Level_2b*, @Options_2b*, 
										 @Options_2b*, CurPlayer);
								break;
							case 2:
								Passed = PlayWorld ('x', '3', @Level_3a*, 
										 @Options_3a*, @Opt_3a*,
										 @Level_3b*, @Options_3b*, 
										 @Options_3b*, CurPlayer);
								break;
							case 3:
								Passed = PlayWorld ('x', '4', @Level_5a*, 
										 @Options_5a*, @Opt_5a*,
										 @Level_5b*, @Options_5b*, 
										 @Options_5b*, CurPlayer);
								break;
							case 4:
								Passed = PlayWorld ('x', '5', @Level_6a*, 
										 @Options_6a*, @Opt_6a*,
										 @Level_6b*, @Options_6b*, 
										 @Options_6b*, CurPlayer);
								break;
							case 5:
								Passed = PlayWorld ('x', '6', @Level_4a*, 
									     @Options_4a*, @Opt_4a*,
										 @Level_4b*, @Options_4b*, 
										 @Options_4b*, CurPlayer);
								break;
							case default:
								EndGame = true;
								break;
						}
						if (Passed)
							Data.Progress[CurPlayer]++;
						if (QuitGame)
						{
							EndGame = true;
#if MENU
							QuitGame = false;
#endif
						}
					}
			}
		} while (EndGame || QuitGame || (Data.Lives[plMario] + 
				 Data.Lives[plLuigi] = 0)
		
		if (GameNumber != -1)
			Config.Games[GameNumber] = Data;
#if MENU
	} while (QuitGame)
#endif
	WriteConfig();
}
}