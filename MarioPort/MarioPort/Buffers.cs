using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarioPort
{
   public static class Buffers
   {
      public const char Hidden = '$';

      public static bool CanHoldYou(char ch)
      {
         if (ch >= (char)0 || ch <= (char)13 || ch >= '0' || ch <= 'Z')
            return true;
         return false;
      }

      public static bool CanStandOn(char ch)
      {
         if (ch >= (char)14 || ch <= (char)16 || ch >= 'a' || ch <= 'f')
            return true;
         return false;
      }

      /** uses absolute, so these might not be needed
       * long Timer;
       *int wTimer;
       *byte bTimer;
      **/
      public const int W = 20;
      public const int H = 14;
      public const int NH = 16;
      public const int NV = 13;
      
      public const int MaxWorldSize = 236;
      public const int EX = 1;
      public const int EY1 = 8;
      public const int EY2 = 3;
      
      public const int dirLeft = 0;
      public const int dirRight = 1;
      
      public const int mdSmall = 0;
      public const int mdLarge = 1;
      public const int mdFire = 2;
      
      public const int plMario = 0;
      public const int plLuigi = 1;
      
      public static bool QuitGame = false;
      public static bool BeeperSound = true;
      
      public static string[] PlayerName = {"PLMARIO", "PLLUIGI"};
      	
      public static byte Player;
      public static GameData data = GameData.Create();
      public static string[] WorldNumber = new string[3];
      public static long LevelScore;
      public static char[,] ImageBuffer = new char[H, W];

      public class GameData
      {
         
         public int numPlayers;
         public int[] progress;
         public int[] lives;
         public int[] coins;
         public long[] score;
         public byte[] mode;

         public GameData(int numPlayers, int[] progress, int[] lives, int[] coins, long[] score, byte[] mode)
         {
            this.numPlayers = numPlayers;
            this.progress = progress;
            this.lives = lives;
            this.coins = coins;
            this.score = score;
            this.mode = mode;
         }
         public static GameData Create() //factory function to create arrays of GameData
         {
               int numPlayers = 0;
               int[] progress = new int[] { 0, 0 };
               int[] lives = new int[] { 0, 0 };
               int[] coins = new int[] { 0, 0 };
               long[] score = new long[] {(long) 0, (long)0 };
               byte[] mode = new byte[] { Convert.ToByte(0), Convert.ToByte(0) };
               return new GameData(numPlayers, progress, lives, coins, score, mode);
         }

      }
      public struct WorldOptions
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

      public static bool GameDone;
      public static bool Passed;
      public static byte[,] WorldBuffer = new byte[MaxWorldSize + 2 * EX, NV + EY2 + EY1];
      public static char[,] WorldMap = new char[MaxWorldSize + 2 * EX, NV + EY2 + EY1];
      public static char[,] SaveWorldMap = new char[MaxWorldSize + 2 * EX, NV + EY2 + EY1];
      public static WorldOptions Options = new WorldOptions();
      public static WorldOptions SaveOptions;
      public static int XView;
      public static int YView;
      public static int[] LastXView = new int[FormMarioPort.MAX_PAGE];
      public static byte[,] StarBuffer = new byte[FormMarioPort.MAX_PAGE + 1, 320];
      public static byte[,] StarBackGr;
      public static int Size;
      public static System.Drawing.Bitmap[,,,] Pictures = new System.Drawing.Bitmap[plLuigi + 1,mdFire + 1,4,dirRight + 1];
      public static int Demo;
      public static int TextCounter;
      public static byte LavaCounter;

      public const int dmNoDemo = 0;
      public const int dmDownInToPipe = 1;
      public const int dmUpOutOfPipe = 2;
      public const int dmUpInToPipe = 3;
      public const int dmDownOutOfPipe = 4;
      public const int dmDead = 5;

      public static void ReadWorld(char[,] M, ref char[,] W, WorldOptions Opt)
      {
         Console.WriteLine("Reading the world");
         //MapBuffer M;
         int x;
         //Move(Opt, Buffers.Options, Buffers.Options.SizeOf());
         //M = Map;
         //FillChar(W, W.size(), ' ');
         for (int i = 0; i < W.GetLength(0); i++)
            for (int j = 0; j < W.GetLength(1); j++)
               W[i,j] = ' ';
         for (int i = 0; i < EX - 1; i++)
      	   for (int j = 0; j < NV - 1 + EY2 + EY1; j++)
      		   W[i,j] = '@';
         x = 0;
      
         while (M[x,0] != (0) && (x < MaxWorldSize))
         {
      	   for (int i = 0; i < NV; i++)
      		   W[x, NV - i - 1] = (char)M[x,i];
      	   W[x,EY1] = (char)(0);
      	   for (int i = 1; i < EY2 + EY1; i++)
      		   W[x,NV-1+i] = W[x,NV-1];
      	   x++;
         }
      
         Buffers.Options.XSize = x;
         for (int i = x; i < x + EX - 1; i++)
      	   for (int j = EY1; j < (NV - 1 + EY2); j++)
      		   W[i,j] = '@';
      }

      public static void Swap()
      {
         WorldOptions TempOptions;
         byte C;
         TempOptions = Options;
         Options = SaveOptions;
         SaveOptions = TempOptions;
         for (int i = EX; i < MaxWorldSize - 1 + EX; i++)
      	   for (int j = EY1; j < NV -1 + EY2; j++)
      	   {
      		   C = (byte)WorldMap[i,j];
               WorldMap[i, j] = SaveWorldMap[i, j];
      		   SaveWorldMap[i,j] = (char)C;
      	   }
      }

      public static void BeeperOn()
      {
         BeeperSound = true;
         //NoSound();
      }

      public static void BeeperOff()
      {
         BeeperSound = false;
         //NoSound();
      }

      public static void Beep(int Freq)
      {
      //   if (BeeperSound)
      //      if (freq = 0)
      //         Crt.NoSound;
      //      else
      //         Crt.Sound(Freq);
      }

      public static void InitLevelScore()
	   {
		   LevelScore = 0;
	   }
	
	   public static void AddScore(long N)
	   {
		   LevelScore += N;
	   }
	   static Buffers()
	   {
		   Size = 2 * WorldBuffer.Length + StarBuffer.Length +
			      Pictures.Length;
	   }

      public static void main()
      {
         int MemAvail = 0;
         if(MemAvail < Size)
         {
            //Console.Write("Not enough memory");
            //Exit
         }
         //	      GetMem(WorldMap, WorldBuffer.SizeOf());
         //	      GetMem(SaveWorldMap, WorldBuffer.SizeOf());
         //	      GetMem(StarBackGr, StarBuffer.SizeOf());
         //	      GetMem(Pictures, PictureBuffer.SizeOf());
      }
   }
}
