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
      
      public const int DirLeft = 0;
      public const int DirRight = 1;
      
      public const int mdSmall = 0;
      public const int mdLarge = 1;
      public const int mdFire = 2;
      
      public const int plMario = 0;
      public const int plLuigi = 1;
      
      public static bool QuitGame = false;
      public static bool BeeperSound = true;
      
      public static string[] PlayerName = {"PLMARIO", "PLLUIGI"};
      	
      public static byte Player;
      public static GameData data = new GameData();
      public static string[] WorldNumber = new string[3];
      public static long LevelScore;

      public struct GameData
      {
         
         public int[] numPlayers;
         public int[] progress;
         public int[] lives;
         public int[] coins;
         public long[] score;
         public byte[] mode;

         public GameData(bool x = true)
         {
            this.numPlayers = new int[] { 0, 0 };
            this.progress = new int[] { 0, 0 };
            this.lives = new int[] { 0, 0 };
            this.coins = new int[] { 0, 0 };
            this.score = new long[] { 0, 0 };
            this.mode = new byte[] { 0, 0 };
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
      public static byte[,] WorldBuffer;
      public static byte[,] WorldMap;
      public static byte[,] SaveWorldMap;
      public static WorldOptions Options = new WorldOptions();
      public static WorldOptions SaveOptions;
      public static int XView;
      public static int YView;
      //public static int[] LastXView = new int[MAX_PAGE];
      public static byte[,] StarBuffer;
      public static byte[,] StarBackGr;
      public static int Size;
      public static byte[,] PictureBuffer;
      public static int Demo;
      public static int TextCounter;
      public static byte LavaCounter;

      public const int dmNoDemo = 0;
      public const int dmDownInToPipe = 1;
      public const int dmUpOutOfPipe = 2;
      public const int dmUpInToPipe = 3;
      public const int dmDownOutOfPipe = 4;
      public const int dmDead = 5;

      public static void ReadWorld(byte[,] M, ref byte[,] W, WorldOptions Opt)
      {
         //MapBuffer M;
         int x;
         //Move(Opt, Options, Options.SizeOf());
         //M = Map;
         //FillChar(W, W.size(), ' ');
         for (int i = -EX; i < -1; i++)
      	   for (int j = -EY1; j < NV - 1 + EY2; j++)
      		   W[i,j] = (byte)'@';
         x = 0;
      
         while (M[x+1,1] != (0) && (x < MaxWorldSize))
         {
      	   for (int i = 1; i < NV; i++)
      		   W[x,NV-i] = M[x+1,i];
      	   W[x,-EY1] = (0);
      	   for (int i = 1; i < EY2; i++)
      		   W[x,NV-1+i] = W[x,NV-1];
      	   x++;
         }
      
         Options.XSize = x;
         for (int i = x; i < x + EX - 1; i++)
      	   for (int j = EY1; j < (NV - 1 + EY2); j++)
      		   W[i,j] = (byte)'@';
      }

      public static void Swap()
      {
         WorldOptions TempOptions;
         byte C;
         //Move(Options, TempOptions, TempOptions.SizeOf());
         //Move(SaveOptions, Options, Options.SizeOf());
         //Move(TempOptions, SaveOptions, SaveOptions.SizeOf());
         for (int i = -EX; i < MaxWorldSize - 1 + EX; i++)
      	   for (int j = -EY1; j < NV -1 + EY2; j++)
      	   {
      		   C = WorldMap[i,j];
               WorldMap[i, j] = SaveWorldMap[i, j];
      		   SaveWorldMap[i,j] = C;
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

      //public static void Beep(int Freq)
      //{
      //   if (BeeperSound)
      //      if (freq = 0)
      //         Crt.NoSound;
      //      else
      //         Crt.Sound(Freq);
      //}

   }
}
