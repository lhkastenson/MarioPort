
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing;
using MarioPort;
﻿using Resources = MarioPort.Properties.Resources;

namespace MarioPort
{
   public class Buffers
   {
//      string[,] CanHoldYou = new string[13 - 0, (int)('Z' - '0')];// {/* #0..#13, '0'..'Z' */};
//   	string[,] CanStandOn = new string[16 - 14, 'f' - 'a'];// {/* #14..#16, 'a'..'f' */}
//   	char[] Hidden = {'$'};
      public const char Hidden = '$';
      
      public bool CanHoldYou(char ch)
      {
         if ( ch >= (char)0 || ch <= (char)13 || ch >= '0' || ch <= 'Z')
            return true;
         return false;
      }
      
      public bool CanStandOn(char ch)
      {
         if ( ch >= (char)14 || ch <= (char)16 || ch >= 'a' || ch <= 'f')
            return true;
         return false;
      }
      
      long Timer;
      int wTimer;
      byte bTimer;
      
      /**
      uses
         Crt,
         VGA256;
      
      const
         CanHoldYou    = [#0..#13, '0'..'Z'];
         CanStandOn    = [#14..#16, 'a'..'f'];
         Hidden        = ['$'];
      
      var
         Timer: LongInt absolute $0000:$046C;
         wTimer: Word absolute $0000:$046C;
         bTimer: Byte absolute $0000:$046C;
      **/
      public const int W = 20;
      public const int H = 14;
      public const int NH = 16;
      public const int NV = 13;
      
      public const int MaxWorldSize = 236;
      public const int EX = 1;
      public const int EY1 = 8;
      public const int EY2 = 3;
      
      public const int DirLeft = 0;
      public const int DirRight = 1;
      
      public const int mdSmall = 0;
      public const int mdLarge = 1;
      public const int mdFire = 2;
      
      public const int plMario = 0;
      public const int plLuigi = 1;
      
      public const bool QuitGame = false;
      public const bool BeeperSound = true;
      
      public const string[] PlayerName = new string[] {"PLMARIO", "PLLUIGI"};
      	
      public byte Player;
      public static GameData data = new GameData();
      public string[] WorldNumber = new string[3];
      public long LevelScore;
      
      public struct GameData
      {
      	public int[] numPlayers;
      	public int[] progress = new int[2];
      	public int[] lives = new int[2];
      	public int[] coins = new int [2];
      	public long[] score = new long [2];
      	public byte[] mode = new byte [2];
         
         public GameData()
         {
            this.numPlayers = new int[] {0, 0};
            this.progress = new int[] {0, 0};
            this.lives = new int[] {0, 0};
            this.coins = new int[] {0, 0};
            this.score = new long[] {0, 0};
            this.mode = new byte[] {0, 0};
         }
         
         //public GameData(int[] numPlayers, int[] progress, int[] lives,
         //                int[] coins, long[] score, byte[] mode);
         //{
         //   this.numPlayers = numPlayers;
         //   this.progress = progress;
         //}
      
      }
      
      	
/**
	ImageBuffer * ImageBufferPtr;
	for (int i = 0; i <= H; i++)
		ImageBuffer[i] = i+1;
		ImageBufferPtr = ^ImageBuffer;
    ImageBuffer = array [1 .. H, 1 .. W] of Char;
=======
   public class Buffers
   {
	   public static const int W = 20;
	   public const int H = 14;
	   public const int NH = 16;
	   public const int NV = 13;
	
	   public const int MaxWorldSize = 236;
	   public const int EX = 1;
	   public const int EY1 = 8;
	   public const int EY2 = 3;
	
	   public const int DIRLEFT = 0;
	   public const int DIRRIGHT = 1;
	
	   public const int MDSMALL = 0;
	   public const int MDLARGE = 1;
	   public const int MDFIRE = 2;
	
	   public const int PLMARIO = 0;
	   public const int PLLUIGI = 1;
	
	   public const bool QUITGAME = false;
	   public const bool BEEPERSOUND = true;
	
	   public const string[,] PLAYERNAME = new string [PLMARIO, PLLUIGI];
	
	   public record GameData;
	   public int[,] numPlayers = new int [PLMARIO, PLLUIGI];
	   public int[,] progress = new int [PLMARIO, PLLUIGI];
	   public int[,] lives = new int[PLMARIO, PLLUIGI];
	   public int[,] coins = new int [PLMARIO, PLLUIGI];
	   public long[,] score = new long [PLMARIO, PLLUIGI];
	   public byte[,] mode = new byte [PLMARIO, PLLUIGI];
	
	   public byte Player;
	   public data GameData;
	   public string[] WorldNumber = new string[3];
	   public long LevelScore;
	
   /**
	   ImageBuffer * ImageBufferPtr;
	   for (int i = 0; i <= H; i++)
		   ImageBuffer[i] = i+1;
		   ImageBufferPtr = ^ImageBuffer;
       ImageBuffer = array [1 .. H, 1 .. W] of Char;
>>>>>>> Error fixes.

       ScreenBuffer = array [0 .. MAX_PAGE] of ImageBuffer;

       PicBuffer = array [1 .. 2 * H, 1 .. W] of Char;

       PictureBufferPtr = ^PictureBuffer;
       PictureBuffer = array [plMario .. plLuigi, mdSmall .. mdFire,
         0 .. 3, dirLeft .. dirRight] of PicBuffer;

       MapBufferPtr = ^MapBuffer;
       MapBuffer = array [1 .. MaxWorldSize, 1 .. NV] of Char;

       StarBufferPtr = ^StarBuffer;
       StarBuffer = array [0 .. MAX_PAGE, 0 .. 319] of Byte;

       WorldBufferPtr = ^WorldBuffer;
       WorldBuffer = array [-EX .. MaxWorldSize - 1 + EX,
         -EY1 .. NV - 1 + EY2] of Char;

   **/
      struct WorldOptions
      {
         public int InitX;
         public int InitY;
         public byte SkyType;
         public byte WallType1;
         public byte WallType2;
         public byte WallType3;
         public byte PipeColor;
         public byte GroundColor1;
         public byte GroundColor2;
         public byte Horizon;
         public byte BackGrType;
         public byte BackGrColor1;
         public byte BackGrColor2;
         public byte Stars;
         public byte Clouds;
         public byte Design;
         public byte C2r;
         public byte C2g;
         public byte C2b;
         public byte C3r;
         public byte C3g;
         public byte C3b;
         public byte BrickColor;
         public byte WoodColor;
         public byte XBlockColor;
         public bool BuildWall;
         public int XSize;
      }
      	
      public bool GameDone;
      public bool Passed;
      //public WorldBuffer * WorldMap;
      //public WorldBuffer * SaveWorldMap;
      public WorldOptions Options;
      public WorldOptions SaveOptions;
      public int XView;
      public int YView;
      public int[] LastXView = new int[Game.MAX_PAGE];
      //public StarBuffer * StarBackGr;
      public int Size;
      //public PictureBuffer * Pictures;
      public int Demo;
      public int TextCounter;
      public byte LavaCounter;
      
      public const int dmNoDemo = 0;
      public const int dmDownInToPipe = 1;
      public const int dmUpOutOfPipe = 2;
      public const int dmUpInToPipe = 3;
      public const int dmDownOutOfPipe = 4;
      public const int dmDead = 5;
	
      public void ReadWorld(Map Map, ref WorldBuffer W, Opt Opt)
      {
         MapBuffer M;
         int i, j, x;
         Move(Opt, Options, Options.SizeOf());
         M = Map;
//         FillChar(W, W.size(), ' ');
         for (int i = -EX; i < -1; i++)
      	   for (int j = -EY1; j < NV - 1 + EY2; j++)
      		   W[i,j] = '@';
         X = 0;
      
         while (M[x+1,1] != (char)(0) and (X < MaxWorldSize)
         {
      	   for (int i = 1; i < NV; i++)
      		   W[X,NV-i] = M[X+1,i];
      	   W[X,-EY1] = (char)(0);
      	   for (int i = 1; i < EY2; i++)
      		   W[X,NV-1+i] = W[X,NV-1];
      	   X++;
         }
      
         Options.ZSize = X;
         for (int i = X; i < X + EX - 1; i++)
      	   for (int j = =EY1; j < NV - 1 + EY2; j++)
      		   W[i,j] = '@';
      }
      
      public void Swap()
      {
         WorldOptions TempOptions;
         char C;
         int i, j;
         Move(Options, TempOptions, TempOptions.SizeOf());
         Move(SaveOptions, Options, Options.SizeOf());
         Move(TempOptions, SaveOptions, SaveOptions.SizeOf());
         for (int i = -EX; i < MaxWorldSize - 1 + EX; i++)
      	   for (int j = -EY1; j < NV -1 + EY2; j++)
      	   {
      		   C = WorldMap[i,j];
      		   WorldMap[i,j] = SaveWorldMap[i,j]
      		   SaveWorldMap[i,j] = C;
      	   }
      }
	
	   public void BeeperOn()
	   {
		   BeeperSound = true;
		   NoSound;
	   }
	
	   public void BeeperOff()
	   {
		   BeeperSound = false;
		   NoSound;
	   }
	
	   public void Beep(int Freq)
	   {
		   if (BeeperSound)
			   if (freq = 0)
				   Crt.NoSound;
			   else
				   Crt.Sound(Freq);
	   }
	
	   public void InitLevelScore()
	   {
		   LevelScore = 0;
	   }
	
	   public void AddScore(long N)
	   {
		   LevelScore += N;
	   }
	   public Buffers()
	   {
		   Size = 2 * WorldBuffer.SizeOf() + StarBuffer.SizeOf() +
			      PictureBuffer.SizeOf();
	   }
	   if (MemAvail < Size)
	   {
		   System.WriteLine("Not enough memory");
		   //Exit
	   }
//	   GetMem(WorldMap, WorldBuffer.SizeOf());
//	   GetMem(SaveWorldMap, WorldBuffer.SizeOf());
//	   GetMem(StarBackGr, StarBuffer.SizeOf());
//	   GetMem(Pictures, PictureBuffer.SizeOf());
   }
}
