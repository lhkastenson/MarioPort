using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//Manually Added
using System.IO; 

namespace MarioPort
{
   public static class Worlds
   {
      public static byte[,] Intro_0()
      {
         string levelInfo = 
            @"AA÷          " +
            @"AA÷          " +
            @"AA           " +
            @"AAðððð       " +
            @"AA÷ð         " +
            @"AA÷          " +
            @"AAððð        " +
            @"AA           " +
            @"AA           " +
            @"AA           " +
            @"AA           " +
            @"AAððððð      " +
            @"AAððð        " +
            @"AA÷          " +
            @"AA÷          " +
            @"AA           " +
            @"AA           ";
         System.Text.ASCIIEncoding  encoding=new System.Text.ASCIIEncoding();
         byte[] temp = encoding.GetBytes(levelInfo);
         byte[,] intro = new byte[temp.Length / 13, 13];
         for(int x = 0; x < (temp.Length / 13); x++) 
         {
            for (int y = 0; y < 13; y++)
            {
               intro[x, y] = temp[x * 13 + y];
            }
         }
         return intro;
      }

      public static Buffers.WorldOptions Options_0()
      {
         Buffers.WorldOptions options = new Buffers.WorldOptions();
         options.InitX = 7 * Buffers.W + 10;
         options.InitY = 9 * Buffers.H;   
         options.SkyType = 10;         
         options.WallType1 = 0;
         options.WallType2 = 0;
         options.WallType3 = 0;        
         options.PipeColor = 0x30;        
         options.GroundColor1 = 0x4B;      
         options.GroundColor2 = 0;        
         options.Horizon = 120;           
         options.BackGrType = 10;         
         options.BackGrColor1 = 0x36;
         options.BackGrColor2 = 0x30; 
         options.Stars = 0;              
         options.Clouds = 0;    
         options.Design = 2;     
         options.C2r = 10; 
         options.C2g = 23;  
         options.C2b = 8;  
         options.C3r = 22; 
         options.C3g = 35; 
         options.C3b = 20;           
         options.BrickColor = 0xB0;        
         options.WoodColor = 0x48;         
         options.XBlockColor = 0xA0;
         return options;
      }

      public static byte[,] Level_1a()
      {
         string levelInfo =
            @"AA÷          " +
            @"AA           " +
            @"AA           " +
            @"AA     ô     " +
            @"AAöööööùúô   " +
            @"AAöööööõöùú  " +
            @"AA      ôõ   " +
            @"AAööööööùú   " +
            @"AA÷     õ    " +
            @"AA÷   ?      " +
            @"AA    ?à     " +
            @"AA           " +
            @"AA           " +
            @"AA220è       " +
            @"AA331â       " +
            @"AA      ô    " +
            @"AAööööööùú   " +
            @"AA      õô   " +
            @"AAöööööööùú  " +
            @"AA       õ   " +
            @"AA           " +
            @"AA           " +
            @"AA€          " +
            @"AA÷          " +
            @"AA÷          " +
            @"AA           " +
            @"AA2220       " +
            @"AA3331       " +
            @"AA           " +
            @"AA           " +
            @"AA      ô    " +
            @"AAööööööùú   " +
            @"AA÷     õ    " +
            @"AA÷          " +
            @"AA÷  J       " +
            @"AA€  J       " +
            @"AA€          " +
            @"AA      J    " +
            @"#%      J    " +
            @"#%§     J    " +
            @"#%      J    " +
            @"AA      J    " +
            @"AA           " +
            @"AA   ******  " +
            @"AA           " +
            @"AAˆ          " +
            @"AA           " +
            @"AA220        " +
            @"AA331        " +
            @"AA22220á     " +
            @"AA33331à     " +
            @"AA    ?à     " +
            @"AA÷          " +
            @"AA÷          " +
            @"AA           " +
            @"AA        $á " +
            @"AA      ô    " +
            @"AAööööööùúô  " +
            @"AAööööööõöùú " +
            @"AA       ôõ  " +
            @"AAöööööööùú  " +
            @"AA       õ   " +
            @"AAˆ          " +
            @"AA           " +
            @"AA    ?      " +
            @"AA    J   J  " +
            @"AA        ?  " +
            @"AA€   ?      " +
            @"AA    J   J  " +
            @"AA        ?â " +
            @"AA           " +
            @"AA€          " +
            @"AA           " +
            @"AA          ü" +
            @"#%          ü" +
            @"AA      ô   ü" +
            @"AAööööööùú  ü" +
            @"AA      õ   ü" +
            @"AA           " +
            @"AA        $ã " +
            @"#%  WW    $  " +
            @"#%  W        " +
            @"#%  W        " +
            @"#%  W‰       " +
            @"#%  WW       " +
            @"#%      WW   " +
            @"#%      W    " +
            @"#%      W‰   " +
            @"#%      W    " +
            @"#%    WWWW   " +
            @"#%    W‰     " +
            @"#%    W      " +
            @"#%    W      " +
            @"#% $à WW     " +
            @"AA÷          " +
            @"AA÷      ô   " +
            @"AAöööööööùú  " +
            @"AA       õ   " +
            @"AA           " +
            @"AA2220 …     " +
            @"AA3331       " +
            @"AA           " +
            @"AA÷€ $   *   " +
            @"AA÷  $   *   " +
            @"AA   $   *   " +
            @"AA€  $   *   " +
            @"AA   $       " +
            @"AA2220       " +
            @"AA3331       " +
            @"AA           " +
            @"AA÷          " +
            @"AA÷          " +
            @"AAˆ  K       " +
            @"AA2220 …     " +
            @"AA3331       " +
            @"AA           " +
            @"AA   ?í      " +
            @"AA÷          " +
            @"AA÷  J       " +
            @"AA   J       " +
            @"AAI  Jà      " +
            @"#%           " +
            @"#%           " +
            @"AA       ô   " +
            @"AAööööööôùú  " +
            @"AAööööööùú   " +
            @"AA      õ    " +
            @"AA    *     W" +
            @"AA÷   *     W" +
            @"AA÷   *     W" +
            @"AAˆ   *    WW" +
            @"AA         WW" +
            @"AA      è0222" +
            @"AAˆ     á1333" +
            @"AA÷        WW" +
            @"AA÷        WW" +
            @"AA          W" +
            @"AA2220à…    W" +
            @"AA3331â      " +
            @"AA           " +
            @"AA÷          " +
            @"AA÷      ô   " +
            @"AAöööööööùú  " +
            @"AA       õ   " +
            @"#%           " +
            @"#%§        $á" +
            @"#%           " +
            @"#%§          " +
            @"#%           " +
            @"AA÷          " +
            @"AA÷  WW      " +
            @"AA÷  W *     " +
            @"AA   W * WW  " +
            @"AA   W * W * " +
            @"AA   W * W€* " +
            @"AA   W * W * " +
            @"AA   W€* WW  " +
            @"AA   W *     " +
            @"AA   WW      " +
            @"AA           " +
            @"AA           " +
            @"AA     ô     " +
            @"AAöööööùúô   " +
            @"AA÷ööööõöùú  " +
            @"AA÷      õ   " +
            @"AA           " +
            @"AAˆ     ô    " +
            @"AAöööþööùú   " +
            @"AA      õ    " +
            @"AA           " +
            @"AA     ô     " +
            @"AA÷ööööùúô   " +
            @"AA÷ööööõöùú  " +
            @"AA÷ˆ    ôõ   " +
            @"AAööööööùú   " +
            @"AA      õ    " +
            @"AA2220ç      " +
            @"AA3331ç      " +
            @"AA÷          " +
            @"AA÷          ";
         System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
         byte[] temp = encoding.GetBytes(levelInfo);
         byte[,] level_1a = new byte[(temp.Length / 13) + 1, 13];
         for (int x = 0; x < (temp.Length / 13); x++)
         {
            for (int y = 0; y < 13; y++)
            {
               level_1a[x, y] = temp[x * 13 + y];
            }
         }
         level_1a[(temp.Length / 13),0] = 0; //Storing 0 at end, not positive if necessary
         return level_1a;
      }

      public static Buffers.WorldOptions Options_1a()
      {
         Buffers.WorldOptions options = new Buffers.WorldOptions();
         options.InitX = 2 * Buffers.W + 10;
         options.InitY = 9 * Buffers.H;
         options.SkyType = 2;
         options.WallType1 = 3;
         options.WallType2 = 0;
         options.WallType3 = 0;
         options.PipeColor = 0x70;
         options.GroundColor1 = 0x58;
         options.GroundColor2 = 0;
         options.Horizon = 140;
         options.BackGrType = 1;
         options.BackGrColor1 = 0x36;
         options.BackGrColor2 = 0x30;
         options.Stars = 0;
         options.Clouds = 0;
         options.Design = 1;
         options.C2r = 10;
         options.C2g = 23;
         options.C2b = 8;
         options.C3r = 22;
         options.C3g = 35;
         options.C3b = 20;
         options.BrickColor = 0x30;
         options.WoodColor = 0x30;
         options.XBlockColor = 0x68;
         return options;
      }

      public static Buffers.WorldOptions Opt_1a()
      {
         Buffers.WorldOptions options = new Buffers.WorldOptions();
         options.InitX = 2 * Buffers.W + 10;
         options.InitY = 9 * Buffers.H;
         options.SkyType = 5;
         options.WallType1 = 3;
         options.WallType2 = 0;
         options.WallType3 = 0;
         options.PipeColor = 0x70;
         options.GroundColor1 = 0x58;
         options.GroundColor2 = 0;
         options.Horizon = 140;
         options.BackGrType = 3;
         options.BackGrColor1 = 0x36;
         options.BackGrColor2 = 0x30;
         options.Stars = 0;
         options.Clouds = 0;
         options.Design = 1;
         options.C2r = 10;
         options.C2g = 23;
         options.C2b = 8;
         options.C3r = 22;
         options.C3g = 35;
         options.C3b = 20;
         options.BrickColor = 0x30;
         options.WoodColor = 0x30;
         options.XBlockColor = 0x68;
         return options;
      }

      public static byte[,] Level_1b()
      {
         string levelInfo =
            @"WWWWWWWWWWWWW" +
            @"W     W  è022" +
            @"W     W  à133" +
            @"220á#%W   #%W" +
            @"331á  W *   W" +
            @"W     W  *  W" +
            @"W  *#%W * #%W" +
            @"W *   W  *  W" +
            @"W  *  W *   W" +
            @"W * #%W  *#%W" +
            @"W  *  W     W" +
            @"W *   W20   W" +
            @"W  *#%W31 #%W" +
            @"W *     $à  W" +
            @"W   $       W" +
            @"WWWWWWWWWWWWW";
         System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
         byte[] temp = encoding.GetBytes(levelInfo);
         byte[,] level_1b = new byte[temp.Length / 13, 13];
         for (int x = 0; x < (temp.Length / 13); x++)
         {
            for (int y = 0; y < 13; y++)
            {
               level_1b[x, y] = temp[x * 13 + y];
            }
         }
         level_1b[(temp.Length / 13), 0] = 0; //Storing 0 at end, not positive if necessary
         return level_1b;
      }

      public static Buffers.WorldOptions Options_1b()
      {
         Buffers.WorldOptions options = new Buffers.WorldOptions();
         options.InitX = 2 * Buffers.W + 10;
         options.InitY = 9 * Buffers.H;
         options.SkyType = 8;
         options.WallType1 = 102;
         options.WallType2 = 0;
         options.WallType3 = 0;
         options.PipeColor = 0x70;
         options.GroundColor1 = 0x48;
         options.GroundColor2 = 0;
         options.Horizon = 136;
         options.BackGrType = 4;
         options.BackGrColor1 = 0x36;
         options.BackGrColor2 = 0x30;
         options.Stars = 0;
         options.Clouds = 0;
         options.Design = 3;
         options.C2r = 10;
         options.C2g = 23;
         options.C2b = 8;
         options.C3r = 22;
         options.C3g = 35;
         options.C3b = 20;
         options.BrickColor = 0x48;
         options.WoodColor = 0x48;
         options.XBlockColor = 0x68;
         return options;
      }

      public static byte[,] Level_2a()
      {
         string levelInfo =
            @"AAAAAAAAAAAAA" +
            @"AA           " +
            @"AA           " +
            @"AA           " +
            @"AA           " +
            @"AA         üJ" +
            @"AA         üJ" +
            @"AA         üJ" +
            @"AA220 …    üJ" +
            @"AA331      üJ" +
            @"AA22220 …  üJ" +
            @"AA33331    üJ" +
            @"AA    ?    üJ" +
            @"AA    ?à   üJ" +
            @"AA20 …     üJ" +
            @"AA31       üJ" +
            @"AA         üJ" +
            @"AA         üJ" +
            @"#%           " +
            @"#%           " +
            @"#%           " +
            @"AA       AAAA" +
            @"AA       AAAA" +
            @"AA       AAAA" +
            @"AA       AAAA" +
            @"AAI      AAAA" +
            @"#%Iˆ       I " +
            @"#%           " +
            @"#%‚          " +
            @"#%           " +
            @"#%I        I " +
            @"AAI      AAAA" +
            @"AA       AAAA" +
            @"AA          A" +
            @"AA          A" +
            @"AA          A" +
            @"AA220è      A" +
            @"AA331à      A" +
            @"AA222220 …  A" +
            @"AA333331    A" +
            @"            A" +
            @"            A" +
            @" K          A" +
            @"AA          A" +
            @"AA          A" +
            @"AA     A    A" +
            @"AA     A    A" +
            @"AA     A    A" +
            @"AA     A    A" +
            @"AAˆ    A    A" +
            @"AA     A    A" +
            @"#%     A    A" +
            @"#%‚         A" +
            @"#%     A    A" +
            @"AA     A    A" +
            @"AA     A    A" +
            @"AA     A    A" +
            @"#%     A    A" +
            @"#%‚         A" +
            @"#%     A    A" +
            @"AA     A ** A" +
            @"AA     A ** A" +
            @"AA     A ** A" +
            @"AA     A ** A" +
            @"AA     A ** A" +
            @"AA     A ** A" +
            @"AA     A ** A" +
            @"AA     A€** A" +
            @"AA     A ** A" +
            @"AA     A€   A" +
            @"AA          A" +
            @"AA    A20à… A" +
            @"AA    A31ã  A" +
            @"AA    AAAAAAA" +
            @"AA     $ã    " +
            @"AA           " +
            @"AAˆ          " +
            @"AA           " +
            @"AA2220à†   J " +
            @"AA3331á    J " +
            @"#%         J " +
            @"#%         Jà" +
            @"#%         J " +
            @"AA22220 †  J " +
            @"AA33331    J‰" +
            @"#%         J " +
            @"#%         J " +
            @"#%         J " +
            @"#%  W      J " +
            @"AA220 †    J " +
            @"AA331      J " +
            @"#%           " +
            @"#%           " +
            @"#%           " +
            @"AA2220à      " +
            @"AA3331à      " +
            @"AA222220     " +
            @"AA333331     " +
            @"#% I   ?à    " +
            @"#% I         " +
            @"#%‚          " +
            @"#% I         " +
            @"#% I         " +
            @"#%‚          " +
            @"#% I         " +
            @"AA I         " +
            @"AA2220è      " +
            @"AA3331â      " +
            @"AA   ?â  ?í  " +
            @"AA           " +
            @"AA           " +
            @"AA220 …      " +
            @"AA331        " +
            @"AA           " +
            @"AA           " +
            @"AA22220è     " +
            @"AA33331ã     " +
            @"#%        *  " +
            @"#%‚        * " +
            @"#%‚        * " +
            @"#%        *  " +
            @"AA22220 …    " +
            @"AA33331      " +
            @"AA    W      " +
            @"AA ** W      " +
            @"AA ** W      " +
            @"AA ** W      " +
            @"AA ** W     A" +
            @"AA    W  WW A" +
            @"#%    W  W  A" +
            @"#%    W  W  A" +
            @"#%    W  W  A" +
            @"AA    W  Wˆ A" +
            @"AA    W     A" +
            @"AA    W     A" +
            @"AA    WW    A" +
            @"#%‚         A" +
            @"AA    WW    A" +
            @"AA    W  *  A" +
            @"AA    W  *  A" +
            @"AA    W  *  A" +
            @"AA    W€ *  A" +
            @"AA    WW    A" +
            @"AA          A" +
            @"AA          A" +
            @"AA220 †     A" +
            @"AA331       A" +
            @"AA222220 †  A" +
            @"AA333331    A" +
            @"AA          A" +
            @"AA          A" +
            @"AAI         A" +
            @"AAI         A" +
            @"#%          A" +
            @"#%‚         A" +
            @"#%          A" +
            @"#%I         A" +
            @"#%I         A" +
            @"#%          A" +
            @"#%‚         A" +
            @"#%          A" +
            @"AAI         A" +
            @"AAI         A" +
            @"AA          A" +
            @"AA          A" +
            @"AAþþ        A" +
            @"AA          A" +
            @"AA          A" +
            @"AAˆ         A" +
            @"AA          A" +
            @"AA2220ç     A" +
            @"AA3331ç     A" +
            @"AA   ?ã     A" +
            @"AA€         A" +
            @"AA€         A" +
            @"AA          A" +
            @"AA          A" +
            @"AAAAAAAAAAAAA" +
            @"¯AAAAAAAAAAAA" +
            @"AAAAAAAAAAAAA" +
            @"AAAAAAAAAAAAA" +
            @"AAAAAAAAAAAAA" +
            @"AAAAAAAAAAAAA" +
            @"AAAAAAAAAAAAA" +
            @"AAAAAAAAAAAAA" +
            @"AAAAAAAAAAAAA" +
            @"AAAAAAAAAAAAA" +
            @"AAAAAAAAAAAAA" +
            @"AAAAAAAAAAAAA" +
            @"AAAAAAAAAAAAA" +
            @"AAAAAAAAAAAAA" +
            @"AAA    ?á    " +
            @"AAAþþ        " +
            @"AAA          " +
            @"AAA  ****    " +
            @"AAA  ****    " +
            @"AAA  ****    " +
            @"AAA  ****    " +
            @"AAA  ****    " +
            @"AAA          " +
            @"AAA          " +
            @"AAA2220ç     " +
            @"AAA3331ç     " +
            @"AAAAAAAAAAAAA" +
            @"®AAAAAAAAAAA-" +
            @"¯AAAAAAAAAAA-" +
            @"      Aˆ    A" +
            @"      A *** A" +
            @"220 †   *** A" +
            @"331         A" +
            @"  A   0222222" +
            @"  A   1333333" +
            @"  A      è022" +
            @"  Aˆ     á133" +
            @"220       A  " +
            @"331       A  " +
            @"22220 …   A  " +
            @"33331     A  " +
            @"          A  " +
            @"          022" +
            @"20        133" +
            @"31      02222" +
            @" A      13333" +
            @" A          A" +
            @" A‰  *****  A" +
            @" A   *****  A" +
            @"20 † *****  A" +
            @"31          A" +
            @"22220 … à0222" +
            @"33331   â1333" +
            @"            A" +
            @"            A" +
            @"    @    ?à A" +
            @"AAAAAAAAAAAAA";
         System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
         byte[] temp = encoding.GetBytes(levelInfo);
         byte[,] level_2a = new byte[temp.Length / 13, 13];
         for (int x = 0; x < (temp.Length / 13); x++)
         {
            for (int y = 0; y < 13; y++)
            {
               level_2a[x, y] = temp[x * 13 + y];
            }
         }
         level_2a[(temp.Length / 13), 0] = 0; //Storing 0 at end, not positive if necessary
         return level_2a;
      }

      public static Buffers.WorldOptions Options_2a()
      {
         Buffers.WorldOptions options = new Buffers.WorldOptions();
         options.InitX = 2 * Buffers.W + 10;
         options.InitY = 0 * Buffers.H;
         options.SkyType = 8;
         options.WallType1 = 102;
         options.WallType2 = 101;
         options.WallType3 = 0;
         options.PipeColor = 0x50;
         options.GroundColor1 = 0x48;
         options.GroundColor2 = 0;
         options.Horizon = 136;
         options.BackGrType = 4;
         options.BackGrColor1 = 0x34;
         options.BackGrColor2 = 0x4C;
         options.Stars = 0;
         options.Clouds = 0;
         options.Design = 4;
         options.C2r = 10;
         options.C2g = 23;
         options.C2b = 8;
         options.C3r = 22;
         options.C3g = 35;
         options.C3b = 20;
         options.BrickColor = 0x48;
         options.WoodColor = 0x30;
         options.XBlockColor = 0x68;
         return options;
      }

      public static Buffers.WorldOptions Opt_2a()
      {
         Buffers.WorldOptions options = new Buffers.WorldOptions();
         options.InitX = 2 * Buffers.W + 10;
         options.InitY = 0 * Buffers.H;
         options.SkyType = 6;
         options.WallType1 = 102;
         options.WallType2 = 101;
         options.WallType3 = 0;
         options.PipeColor = 0x50;
         options.GroundColor1 = 0x48;
         options.GroundColor2 = 0;
         options.Horizon = 136;
         options.BackGrType = 6;
         options.BackGrColor1 = 0x65;
         options.BackGrColor2 = 0x1A;
         options.Stars = 0;
         options.Clouds = 0;
         options.Design = 4;
         options.C2r = 10;
         options.C2g = 23;
         options.C2b = 8;
         options.C3r = 22;
         options.C3g = 35;
         options.C3b = 20;
         options.BrickColor = 0x48;
         options.WoodColor = 0x30;
         options.XBlockColor = 0x68;
         return options;
      }

      public static byte[,] Level_2b()
      {
         byte[,] level_2b = new byte[1,1];
         level_2b[0,0] = 0;
         return level_2b;
      }

      public static Buffers.WorldOptions Options_2b()
      {
         return new Buffers.WorldOptions();
      }

      public static byte[,] Level_3a()
      {
         string levelInfo =
            @"AA           " +
            @"AA÷        ?à" +
            @"AA÷    I     " +
            @"AA     I‰    " +
            @"AA      ô    " +
            @"AAööööööùú   " +
            @"AA      õ    " +
            @"         I   " +
            @"         I‰  " +
            @"             " +
            @"             " +
            @"             " +
            @"AA     I‰    " +
            @"AA     I     " +
            @"AA           " +
            @"AACCC*       " +
            @"AA÷CCCCCCCC* " +
            @"AA÷CCCCCCCC* " +
            @"AACCCCCCCCC* " +
            @"AACCCCCCC*   " +
            @"AA           " +
            @"AA?à         " +
            @"             " +
            @"             " +
            @"             " +
            @"             " +
            @"20           " +
            @"31      ô    " +
            @"AAööööööùú   " +
            @"AA      õ    " +
            @"AACCCC*      " +
            @"AACCCCCCCCC* " +
            @"AA÷CCCCCCCC* " +
            @"AA÷CCCCCCCC*‰" +
            @"AACCCCCCC*   " +
            @"AAACCCCCC*   " +
            @"AAA‡         " +
            @"AAA          " +
            @"220 …        " +
            @"331          " +
            @"222220 …     " +
            @"333331       " +
            @"             " +
            @"             " +
            @"AA÷          " +
            @"AA÷          " +
            @"AA÷   ?    ? " +
            @"AA    ? ‰  ? " +
            @"AA    ?    ?à" +
            @"AA    ?    ? " +
            @"AA    ?    ? " +
            @"AA          ‰" +
            @"AA    K ô    " +
            @"AAööööööùú   " +
            @"AAI‡    õ    " +
            @"          J* " +
            @"          J* " +
            @"          J* " +
            @"          J* " +
            @"AA÷          " +
            @"AA÷          " +
            @"AA‡          " +
            @"AACCC*       " +
            @"AACCCCCCCCC* " +
            @"AACCCCCCCCC* " +
            @"AACCCCCCCCC*‰" +
            @"AA    K      " +
            @"AA    K      " +
            @"AA‡   K      " +
            @"AA           " +
            @"2220 …    J* " +
            @"3331      J* " +
            @"          J* " +
            @"          J* " +
            @"AJ      ô    " +
            @"Aöööööööùú   " +
            @"A‡      õ    " +
            @"A      I     " +
            @"A      I     " +
            @"A      I     " +
            @"A      I‰    " +
            @"A      I?    " +
            @"A‡           " +
            @"A?á          " +
            @"        I220 " +
            @"        I331 " +
            @"220 …   I    " +
            @"331     I    " +
            @"22220        " +
            @"33331        " +
            @"AA  ?à   I   " +
            @"AA‡ ?    I‰  " +
            @"AACCC÷       " +
            @"AACCC÷       " +
            @"AACCC÷       " +
            @"AACCC        " +
            @"AA‡          " +
            @"AA          I" +
            @"2222220 …   I" +
            @"3333331    üI" +
            @"22220 …     I" +
            @"33331       I" +
            @"AA         üI" +
            @"AA       0222" +
            @"AA÷      1333" +
            @"AA÷        üI" +
            @"            I" +
            @"            I" +
            @"           üI" +
            @"AA      ô    " +
            @"AAööööööùú   " +
            @"AA‰     õ    " +
            @"AACCC*       " +
            @"AACCCCCC*    " +
            @"AACCCCCCCCC* " +
            @"AAACCCCCCCC* " +
            @"AAAAAACCCCC* " +
            @"AAAAAA‰      " +
            @"AAAAAA÷      " +
            @"AAAAAA÷      " +
            @"AAAA         " +
            @"AA           " +
            @"AA      $    " +
            @"AA     ô     " +
            @"AAöööþöùúô   " +
            @"AAöööööõöùú  " +
            @"AA      ôõ$  " +
            @"AAööööööùú   " +
            @"AAˆ     õ    " +
            @"AA÷          " +
            @"AA÷        $ " +
            @"AA÷          " +
            @"AA2220ç      " +
            @"AA3331ç      " +
            @"AA         $ " +
            @"             " +
            @"¯AAAAAAAAAAA " +
            @"AAAAAAAAAAAA " +
            @"AAAAAAAAAAAA " +
            @"AAAAAAAAAAAA " +
            @"AAAAAA       " +
            @"AAAAAACCþþ   " +
            @"AAAAAACC   ? " +
            @"AAAACCCC÷  ? " +
            @"AACCCCCC÷  ? " +
            @"AACCCCCC‰  ? " +
            @"AA         ?á" +
            @"AA2220ç    ? " +
            @"AA3331ç    ? " +
            @"AA         ? " +
            @"AACCCCC    ? " +
            @"AACCCCC‰     " +
            @"AA           " +
            @"AAAAAAAAAAAAA" +
            @"AAAAAAAAAAAAA";
         System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
         byte[] temp = encoding.GetBytes(levelInfo);
         byte[,]Level_3a = new byte[temp.Length / 13, 13];
         for (int x = 0; x < (temp.Length / 13); x++)
         {
            for (int y = 0; y < 13; y++)
            {
               Level_3a[x, y] = temp[x * 13 + y];
            }
         }
         Level_3a[(temp.Length / 13), 0] = 0; //Storing 0 at end, not positive if necessary
         return Level_3a;
      }

      public static Buffers.WorldOptions Options_3a()
      {
         Buffers.WorldOptions options = new Buffers.WorldOptions();
         options.InitX = 2 * Buffers.W + 10;
         options.InitY = 9 * Buffers.H;
         options.SkyType = 10;
         options.WallType1 = 2;
         options.WallType2 = 0;
         options.WallType3 = 0;
         options.PipeColor = 0x18;
         options.GroundColor1 = 0xB2;
         options.GroundColor2 = 0x70;
         options.Horizon = 140;
         options.BackGrType = 1;
         options.BackGrColor1 = 0x36;
         options.BackGrColor2 = 0x30;
         options.Stars = 0;
         options.Clouds = 0;
         options.Design = 1;
         options.C2r = 10;
         options.C2g = 23;
         options.C2b = 8;
         options.C3r = 22;
         options.C3g = 35;
         options.C3b = 20;
         options.BrickColor = 0x30; 
         options.WoodColor = 0x30;
         options.XBlockColor = 0x68;
         return options;
      }

      public static Buffers.WorldOptions Opt_3a()
      {
         Buffers.WorldOptions options = new Buffers.WorldOptions();
         options.InitX = 2 * Buffers.W + 10;
         options.InitY = 9 * Buffers.H;
         options.SkyType = 12;
         options.WallType1 = 2;
         options.WallType2 = 0;
         options.WallType3 = 0;
         options.PipeColor = 0x18;
         options.GroundColor1 = 0xB2;
         options.GroundColor2 = 0x70;
         options.Horizon = 140;
         options.BackGrType = 1;
         options.BackGrColor1 = 0x36;
         options.BackGrColor2 = 0x30;
         options.Stars = 0;
         options.Clouds = 0;
         options.Design = 1;
         options.C2r = 10;
         options.C2g = 23;
         options.C2b = 8;
         options.C3r = 22;
         options.C3g = 35;
         options.C3b = 20;
         options.BrickColor = 0x30;
         options.WoodColor = 0x30;
         options.XBlockColor = 0x68;
         return options;
      }

      public static byte[,] Level_3b()
      {
         byte[,] level_3b = new byte[1,1];
         level_3b[0,0] = 0;
         return level_3b;
      }

      public static Buffers.WorldOptions Options_3b()
      {
         return new Buffers.WorldOptions();
      }

      public static byte[,] Level_4a()
      {
         string levelInfo =
            @"AAAA       AA" +
            @"AAAA       AA" +
            @"AAAA       AA" +
            @"AAAA       AA" +
            @"AAAA       AA" +
            @"AAAI        A" +
            @"#%           " +
            @"#% ±         " +
            @"#% ±         " +
            @"#%           " +
            @"AAAI        A" +
            @"AAAA       AA" +
            @"AAAA       AA" +
            @"AI=       =IA" +
            @"AI=       =IA" +
            @"AAAAI       A" +
            @"AAAAA       A" +
            @"AAAAA       A" +
            @"AAAAA       A" +
            @"AAAAA       A" +
            @"AAAAA       A" +
            @"#%           " +
            @"AAAAA       A" +
            @"AAAAA       A" +
            @"AAAAI       A" +
            @"#%           " +
            @"#%           " +
            @"#%           " +
            @"#%           " +
            @"AAAAI       A" +
            @"AAAAA       A" +
            @"AAAIA       A" +
            @"AAAII       A" +
            @"AAA=        A" +
            @"AAA         A" +
            @"AAA         A" +
            @"AAA         A" +
            @"AAA    ?à   A" +
            @"AAA    ?    A" +
            @"AAA    ?    A" +
            @"#%       ‡  A" +
            @"#%          A" +
            @"AAA         A" +
            @"AAAAI       A" +
            @"AAAAA       A" +
            @"AAAAA       A" +
            @"AA= I‡       " +
            @"AA=          " +
            @"AA=          " +
            @"AA=          " +
            @"AA=          " +
            @"AA= I        " +
            @"AAAAA   AAAAA" +
            @"AAAAA   AAAAA" +
            @"AAAAA   AAAAA" +
            @"AAAAA   AAAAA" +
            @"AAAAA   AAAAA" +
            @"AAAAA   AAAAA" +
            @"#%           " +
            @"#%  ±        " +
            @"#%           " +
            @"AAAAA   AAAAA" +
            @"AAAAA   AAAAA" +
            @"AAAAA   AAAAA" +
            @"AAAAA   AAAAA" +
            @"AAAAA‰  AAAAA" +
            @"AAAAA   AAAAA" +
            @"AAAAA   AAAAA" +
            @"AAAAI        " +
            @"AI=          " +
            @"AI=          " +
            @"AI=          " +
            @"AI=          " +
            @"AAAAI        " +
            @"AAAAA   AAAAA" +
            @"AAAAA   AAAAA" +
            @"AAAAA   AAAAA" +
            @"AAAAA       I" +
            @"#%         =I" +
            @"#%‚        =I" +
            @"#%         =I" +
            @"AAAAA       I" +
            @"AAAAA       A" +
            @"AAAIA=      A" +
            @"AAI2220á    =" +
            @"AII3331à    =" +
            @"AI=          " +
            @"I=           " +
            @"=            " +
            @"             " +
            @"             " +
            @"             " +
            @"      ±      " +
            @"      ±      " +
            @"             " +
            @"             " +
            @"II=          " +
            @"             " +
            @"     ±       " +
            @"             " +
            @"            I" +
            @"  ±        =I" +
            @"  ±         A" +
            @"            A" +
            @"            A" +
            @"AAI=      =IA" +
            @"AAA       =IA" +
            @"AAA    ?á  AA" +
            @"AAI        AA" +
            @"AII        AA" +
            @"AII2220á   AA" +
            @"AAI3331à   AA" +
            @"®AAAAAAAAIAAA" +
            @"¯IIIIIIIIIIII" +
            @"#%          I" +
            @"#%          I" +
            @"#%          I" +
            @"#%          I" +
            @"#%          I" +
            @"#%          I" +
            @"222220è     I" +
            @"333331á     I" +
            @"IIIIIIIIII  I" +
            @"AAAAAAAAAI  I" +
            @"AAAAAAAAAI  I" +
            @"AAAAAAAAAI  I" +
            @"AAAAAAAAAI  I" +
            @"®AAAAAAAAI  I" +
            @"AAAAAAAAAI  I" +
            @"AAAAAAAAAI  I" +
            @"AIIIIIIIIIzzI" +
            @"AI       zzzI" +
            @"AI          I" +
            @"AI          I" +
            @"AIþþ        I" +
            @"AI          I" +
            @"AI          I" +
            @"AI          I" +
            @"AI          I" +
            @"AI    ?à    I" +
            @"AI    ?à    I" +
            @"AI          I" +
            @"AI          I" +
            @"AI          I" +
            @"AI          I" +
            @"AI          I" +
            @"AI          I" +
            @"AI          I" +
            @"AI        * I" +
            @"AI        * I" +
            @"AI  ******* I" +
            @"AI        * I" +
            @"AI        * I" +
            @"AI          I" +
            @"AI  ******* I" +
            @"AI     *    I" +
            @"AI     *    I" +
            @"AI     *    I" +
            @"AI  ******* I" +
            @"AI          I" +
            @"AI  ******* I" +
            @"AI  *  *  * I" +
            @"AI  *  *  * I" +
            @"AI  *  *  * I" +
            @"AI  *     * I" +
            @"AI          I" +
            @"AI          I" +
            @"AI          I" +
            @"AI  ******* I" +
            @"AI  *  *  * I" +
            @"AI  *  *  * I" +
            @"AI  *  *  * I" +
            @"AI  *     * I" +
            @"AI          I" +
            @"AI  ******* I" +
            @"AI       *  I" +
            @"AI      *   I" +
            @"AI     *    I" +
            @"AI  ******* I" +
            @"AI          I" +
            @"AI  ******* I" +
            @"AI  *     * I" +
            @"AI  *     * I" +
            @"AI  *     * I" +
            @"AI   *****  I" +
            @"AI          I" +
            @"AI          I" +
            @"AI  * ***** I" +
            @"AI          I" +
            @"AI  * ***** I" +
            @"AI          I" +
            @"AI          I" +
            @"AI          I" +
            @"AI          I" +
            @"AI          I" +
            @"AI          I" +
            @"AI220ç      I" +
            @"AI331ç      I" +
            @"AI          I" +
            @"AI          I" +
            @"AI          I" +
            @"¯AIIIIIIIIIII";
         System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
         byte[] temp = encoding.GetBytes(levelInfo);
         byte[,]Level_4a = new byte[temp.Length / 13, 13];
         for (int x = 0; x < (temp.Length / 13); x++)
         {
            for (int y = 0; y < 13; y++)
            {
               Level_4a[x, y] = temp[x * 13 + y];
            }
         }
         Level_4a[(temp.Length / 13), 0] = 0; //Storing 0 at end, not positive if necessary
         return Level_4a;
      }

      public static Buffers.WorldOptions Options_4a()
      {
         Buffers.WorldOptions options = new Buffers.WorldOptions();
         options.InitX = 2 * Buffers.W + 10;
         options.InitY = 7 * Buffers.H;
         options.SkyType = 7;
         options.WallType1 = 102;
         options.WallType2 = 102;
         options.WallType3 = 0;
         options.PipeColor = 0x80;
         options.GroundColor1 = 0x68;
         options.GroundColor2 = 0;
         options.Horizon = 12;
         options.BackGrType = 6;
         options.BackGrColor1 = 0x23;
         options.BackGrColor2 = 0;
         options.Stars = 0;
         options.Clouds = 0;
         options.Design = 5;
         options.C2r = 10;
         options.C2g = 23;
         options.C2b = 8;
         options.C3r = 22;
         options.C3g = 35;
         options.C3b = 20;
         options.BrickColor = 0x18;
         options.WoodColor = 0x18;
         options.XBlockColor = 0x68;
         return options;
      }

      public static Buffers.WorldOptions Opt_4a()
      {
         Buffers.WorldOptions options = new Buffers.WorldOptions();
         options.InitX = 2 * Buffers.W + 10;
         options.InitY = 7 * Buffers.H;
         options.SkyType = 7;
         options.WallType1 = 102;
         options.WallType2 = 102;
         options.WallType3 = 0;
         options.PipeColor = 0x30;
         options.GroundColor1 = 0x49;
         options.GroundColor2 = 0;
         options.Horizon = 12;
         options.BackGrType = 6;
         options.BackGrColor1 = 0x34;
         options.BackGrColor2 = 0x4C;
         options.Stars = 0;
         options.Clouds = 0;
         options.Design = 5;
         options.C2r = 10;
         options.C2g = 23;
         options.C2b = 8;
         options.C3r = 22;
         options.C3g = 35;
         options.C3b = 20;
         options.BrickColor = 0x18;
         options.WoodColor = 0x18;
         options.XBlockColor = 0x68;
         return options;
      }

      public static byte[,] Level_4b()
      {
         string levelInfo =
            @"AAAAAAAAAAAAA" +
            @"AAAJ        A" +
            @"AAA         A" +
            @"AAA         A" +
            @"AAA‰        A" +
            @"AAA         A" +
            @"AAAJ      è02" +
            @"AAAJ      à13" +
            @"AAA         A" +
            @"AAA‰        A" +
            @"AAA         A" +
            @"AAA         A" +
            @"AAAJ        A" +
            @"             " +
            @"             " +
            @"  K          " +
            @"             " +
            @"             " +
            @"  K          " +
            @"             " +
            @"             " +
            @"JJJJJ        " +
            @"  J=         " +
            @"  J=       ?à" +
            @"  J=ˆ      ? " +
            @"  J=       ? " +
            @"JJJJJ        " +
            @"22220        " +
            @"33331        " +
            @"JJJJJJJ      " +
            @"    J=       " +
            @"    J=       " +
            @"    J=ˆ      " +
            @"    J=       " +
            @"JJJJJJJ      " +
            @"220          " +
            @"331          " +
            @"             " +
            @"             " +
            @"  I          " +
            @"  I          " +
            @"22220 „      " +
            @"33331        " +
            @"220à„        " +
            @"331á         " +
            @"  I          " +
            @"  I          " +
            @"             " +
            @"  ±          " +
            @"             " +
            @"  ±     ?ã   " +
            @"             " +
            @"  I          " +
            @"  I          " +
            @"220          " +
            @"331          " +
            @"22220 „      " +
            @"33331        " +
            @"AA=          " +
            @"AA=          " +
            @"AA=          " +
            @"AA=          " +
            @"AA=‰         " +
            @"AA=          " +
            @"AA=          " +
            @"AA=          " +
            @"AA220      IA" +
            @"AA331      AA" +
            @"AA222220è  AA" +
            @"AA333331á  AA" +
            @"AAAAAAAA   AA" +
            @"AAAAAAAA   AA" +
            @"AAAAAAAA   AA" +
            @"AAAAAAAI   AA" +
            @"           AA" +
            @"           AA" +
            @"AA   IAAAAAAA" +
            @"AA   AAAAAAAA" +
            @"AA   AAAAAAAA" +
            @"AA   AAAAAAAA" +
            @"AAˆ  AAAAAAAA" +
            @"AA   IAAAAAAA" +
            @"AA   ?í  AAAA" +
            @"AAˆ      AAAA" +
            @"AA       AAAA" +
            @"AI       AAAA" +
            @"AI‡  AAAAAAAA" +
            @"AA   AAAAAAAA" +
            @"AA   AAAAAAAA" +
            @"AA   IAAAAAAA" +
            @"AA   IIAAAAAA" +
            @"AA    IIAIAAA" +
            @"AA          =" +
            @"AA          =" +
            @"AAJJá       =" +
            @"AAJJJJ      =" +
            @"AAAAAAAI    =" +
            @"AAAAAAAA    =" +
            @"AAAAAAAA    =" +
            @"AAAAAAAA    =" +
            @"AAAAAAAA    =" +
            @"AAAAAAAA    =" +
            @"AAAAAAAA    =" +
            @"AAAAAAAA    =" +
            @"220á   A    =" +
            @"331á   A    =" +
            @"AAA    A    =" +
            @"AAA    I    =" +
            @"AAA         =" +
            @"AAA     $à  =" +
            @"AAAAAAAAAAAAA" +
            @"AAAAAAAAAAAAA" +
            @"AAAAAAAAAAAAA" +
            @"AAAAAAAAAAAAA" +
            @"AAAAAAAAAAAAA" +
            @"AAAAAAAAAAAAA" +
            @"AAAAAAAAAAAAA";
         System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
         byte[] temp = encoding.GetBytes(levelInfo);
         byte[,] Level_4b = new byte[temp.Length / 13, 13];
         for (int x = 0; x < (temp.Length / 13); x++)
         {
            for (int y = 0; y < 13; y++)
            {
               Level_4b[x, y] = temp[x * 13 + y];
            }
         }
         Level_4b[(temp.Length / 13), 0] = 0; //Storing 0 at end, not positive if necessary
         return Level_4b;
      }

      public static Buffers.WorldOptions Options_4b()
      {
         Buffers.WorldOptions options = new Buffers.WorldOptions();
         options.InitX = 2 * Buffers.W + 10;
         options.InitY = 7 * Buffers.H;
         options.SkyType = 6;
         options.WallType1 = 102;
         options.WallType2 = 0;
         options.WallType3 = 0;
         options.PipeColor = 0x18;
         options.GroundColor1 = 0x68;
         options.GroundColor2 = 0;
         options.Horizon = 12;
         options.BackGrType = 6;
         options.BackGrColor1 = 0x27;
         options.BackGrColor2 = 0;
         options.Stars = 0;
         options.Clouds = 0;
         options.Design = 0;
         options.C2r = 10;
         options.C2g = 23;
         options.C2b = 8;
         options.C3r = 22;
         options.C3g = 35;
         options.C3b = 20;
         options.BrickColor = 0x30;
         options.WoodColor = 0x18;
         options.XBlockColor = 0x68;
         return options;
      }

      public static byte[,] Level_5a()
      {
         string levelInfo =
            @"AA           " +
            @"AA           " +
            @"AA           " +
            @"AA           " +
            @"AA           " +
            @"AAðððð       " +
            @"AAððððð      " +
            @"AA           " +
            @"AA÷          " +
            @"AA÷          " +
            @"AA           " +
            @"AA220        " +
            @"AA331        " +
            @"AA2220       " +
            @"AA3331       " +
            @"AA           " +
            @"AAˆ          " +
            @"AA÷          " +
            @"AA÷          " +
            @"AAðððð       " +
            @"AAððððð      " +
            @"AAððð        " +
            @"AA   ?ã      " +
            @"AA2220à…     " +
            @"AA3331á      " +
            @"AA           " +
            @"AAðð     *   " +
            @"AAððððð  *   " +
            @"AAˆ      *   " +
            @"             " +
            @"             " +
            @"AA2220       " +
            @"AA3331       " +
            @"AA           " +
            @"AAðððð       " +
            @"AAðððððð     " +
            @"AAð          " +
            @"AAðð ?   ?   " +
            @"AA   ?   ?à  " +
            @"AA   ?   ?   " +
            @"AA           " +
            @"AAX‰         " +
            @"             " +
            @"             " +
            @"AA  X‰       " +
            @"AAðððððð     " +
            @"AAðððð       " +
            @"AA    X‰     " +
            @"AA÷          " +
            @"AA÷          " +
            @"AA   X‰      " +
            @"AAðð         " +
            @"AAð          " +
            @"AA2220è…     " +
            @"AA3331á      " +
            @"AA           " +
            @"AAððð        " +
            @"AAðð         " +
            @"AA           " +
            @"AA%%%%%      " +
            @"AA#####%%    " +
            @"AA#######    " +
            @"AAðððð       " +
            @"AAˆ          " +
            @"AAðð         " +
            @"AA÷          " +
            @"AA÷          " +
            @"AA2220á…     " +
            @"AA3331à      " +
            @"          *  " +
            @"          *  " +
            @"AA220     *  " +
            @"AA331     *  " +
            @"AA22220      " +
            @"AA33331      " +
            @"AA           " +
            @"AAðððð       " +
            @"AAððð X‰     " +
            @"AA    X      " +
            @"AA           " +
            @"AA  X        " +
            @"AA  X‰       " +
            @"AAðð         " +
            @"AAððð   X‰   " +
            @"AA      X    " +
            @"             " +
            @"AA2222220è   " +
            @"AA3333331á   " +
            @"AA           " +
            @"AAððð        " +
            @"AAððð   ?à   " +
            @"AA‰     ?    " +
            @"AA           " +
            @"AA           " +
            @"             " +
            @"             " +
            @"             " +
            @"           $á" +
            @"        $í   " +
            @"AA   $       " +
            @"AAððð        " +
            @"AA%%%%%%%%   " +
            @"AA%%%%%%%#   " +
            @"AA#######    " +
            @"AAðððð       " +
            @"AA           " +
            @"AA           " +
            @"AAX‰         " +
            @"AA           " +
            @"AAXXX‰       " +
            @"AA           " +
            @"AAXXXXX‰   ?à" +
            @"AA220        " +
            @"AA331        " +
            @"             " +
            @"AA÷          " +
            @"AA÷          " +
            @"AA           " +
            @"AA22220      " +
            @"AA33331      " +
            @"AA2220       " +
            @"AA3331       " +
            @"AA           " +
            @"AAððð        " +
            @"AAðð         " +
            @"AA           " +
            @"             " +
            @"             " +
            @"             " +
            @"AA           " +
            @"AA           " +
            @"AA÷          " +
            @"AA÷          " +
            @"AA‰          " +
            @"AA           " +
            @"AAðð         " +
            @"AAð          " +
            @"AA  XX‰ ***  " +
            @"AA           " +
            @"AAððð        " +
            @"AA   XX‰ *** " +
            @"AA           " +
            @"AAðð         " +
            @"AAððð XX‰ ***" +
            @"AAð          " +
            @"AA           " +
            @"AA220        " +
            @"AA331        " +
            @"AA           " +
            @"AAð          " +
            @"AAððð        " +
            @"AA           " +
            @"AAþþ         " +
            @"AA÷          " +
            @"AA÷          " +
            @"AA÷          " +
            @"AAˆ          " +
            @"AAððððð      " +
            @"AAðððððð     " +
            @"AAðð         " +
            @"AA           " +
            @"AA2220ç      " +
            @"AA3331ç      " +
            @"AA           " +
            @"AA           ";
         System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
         byte[] temp = encoding.GetBytes(levelInfo);
         byte[,] Level_5a = new byte[temp.Length / 13, 13];
         for (int x = 0; x < (temp.Length / 13); x++)
         {
            for (int y = 0; y < 13; y++)
            {
               Level_5a[x, y] = temp[x * 13 + y];
            }
         }
         Level_5a[(temp.Length / 13), 0] = 0; //Storing 0 at end, not positive if necessary
         return Level_5a;
      }

      public static Buffers.WorldOptions Options_5a()
      {
         Buffers.WorldOptions options = new Buffers.WorldOptions();
         options.InitX = 2 * Buffers.W + 10;
         options.InitY = 9 * Buffers.H;
         options.SkyType = 12;
         options.WallType1 = 0;
         options.WallType2 = 0;
         options.WallType3 = 0;
         options.PipeColor = 0xB0;
         options.GroundColor1 = 0x58;
         options.GroundColor2 = 0;
         options.Horizon = 148;
         options.BackGrType = 3;
         options.BackGrColor1 = 0x36;
         options.BackGrColor2 = 0x30;
         options.Stars = 0;
         options.Clouds = 0;
         options.Design = 2;
         options.C2r = 10;
         options.C2g = 23;
         options.C2b = 8;
         options.C3r = 22;
         options.C3g = 35;
         options.C3b = 20;
         options.BrickColor = 0x30;
         options.WoodColor = 0x30;
         options.XBlockColor = 0x30;
         return options;
      }

      public static Buffers.WorldOptions Opt_5a()
      {
         Buffers.WorldOptions options = new Buffers.WorldOptions();
         options.InitX = 2 * Buffers.W + 10;
         options.InitY = 9 * Buffers.H;
         options.SkyType = 9;
         options.WallType1 = 6;
         options.WallType2 = 0;
         options.WallType3 = 0;
         options.PipeColor = 0xB0;
         options.GroundColor1 = 0x58;
         options.GroundColor2 = 0;
         options.Horizon = 148;
         options.BackGrType = 1;
         options.BackGrColor1 = 0x36;
         options.BackGrColor2 = 0x30;
         options.Stars = 0;
         options.Clouds = 0;
         options.Design = 2;
         options.C2r = 10;
         options.C2g = 23;
         options.C2b = 8;
         options.C3r = 22;
         options.C3g = 35;
         options.C3b = 20;
         options.BrickColor = 0x30;
         options.WoodColor = 0x30;
         options.XBlockColor = 0x30;
         return options;
      }

      public static byte[,] Level_5b()
      {
         string levelInfo =
            @"AAXXXXXXXXXXX" +
            @"AA=  X      X" +
            @"AA=      ** X" +
            @"AA=      ** X" +
            @"AA=  X   ** X" +
            @"AA=      ** X" +
            @"AA=      ** X" +
            @"AA=  X      X" +
            @"AAXXXXXX    X" +
            @"AA     X    X" +
            @"AA   ? X    X" +
            @"AA  ?à X    X" +
            @"AA     X    X" +
            @"#%          X" +
            @"#%          X" +
            @"#%          X" +
            @"AA K        X" +
            @"#%          X" +
            @"#%          X" +
            @"#%          X" +
            @"AA     X    X" +
            @"AA     X    X" +
            @"AA     X  è02" +
            @"AA     X  à13" +
            @"AA     X    X" +
            @"AA€         X" +
            @"AA          X" +
            @"AA   X    * X" +
            @"AA   X    * X" +
            @"AA   X    * X" +
            @"AA   X    * X" +
            @"AA   X    * X" +
            @"#%          X" +
            @"#%          X" +
            @"#%          X" +
            @"AA   X      X" +
            @"AA   X‰     X" +
            @"AA   X  X‰  X" +
            @"AA      X   X" +
            @"AA      X   X" +
            @"AA          X" +
            @"AA          X" +
            @"AA          X" +
            @"AA2220á   * X" +
            @"AA3331á   * X" +
            @"AA          X" +
            @"AAXXXXXXXXXXX";
         System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
         byte[] temp = encoding.GetBytes(levelInfo);
         byte[,] Level_5b = new byte[temp.Length / 13, 13];
         for (int x = 0; x < (temp.Length / 13); x++)
         {
            for (int y = 0; y < 13; y++)
            {
               Level_5b[x, y] = temp[x * 13 + y];
            }
         }
         Level_5b[(temp.Length / 13), 0] = 0; //Storing 0 at end, not positive if necessary
         return Level_5b;
      }

      public static Buffers.WorldOptions Options_5b()
      {
         Buffers.WorldOptions options = new Buffers.WorldOptions();
         options.InitX = 9 * Buffers.W + 10;
         options.InitY = 0 * Buffers.H;
         options.SkyType = 6;
         options.WallType1 = 102;
         options.WallType2 = 101;
         options.WallType3 = 0;
         options.PipeColor = 0x50;
         options.GroundColor1 = 0x48;
         options.GroundColor2 = 0;
         options.Horizon = 12;
         options.BackGrType = 6;
         options.BackGrColor1 = 0x34;
         options.BackGrColor2 = 0x4C;
         options.Stars = 0;
         options.Clouds = 0;
         options.Design = 5;
         options.C2r = 10;
         options.C2g = 23;
         options.C2b = 8;
         options.C3r = 22;
         options.C3g = 35;
         options.C3b = 20;
         options.BrickColor = 0x48;
         options.WoodColor = 0x30;
         options.XBlockColor = 0xB0;
         return options;
      }

      public static byte[,] Level_6a()
      {
         string levelInfo =
            @"AA           " +
            @"AAðððððð     " +
            @"AAððððð      " +
            @"AA           " +
            @"AA           " +
            @"AA           " +
            @"AAððððð      " +
            @"AA           " +
            @"AAððððððð    " +
            @"AAðððððð     " +
            @"AA÷          " +
            @"AA÷   J      " +
            @"AA‡   J      " +
            @"AAA   J      " +
            @"AAA        ?à" +
            @"AAAA    J    " +
            @"AAAA    J    " +
            @"AAAA‡   J    " +
            @"AAAAA÷       " +
            @"AAAAA÷       " +
            @"AAAAAðððð    " +
            @"AAAAAððððð   " +
            @"AAAAA        " +
            @"AAAA20è„     " +
            @"AAAA31á      " +
            @"AAAA         " +
            @"AAA‡      *  " +
            @"AA        *  " +
            @"AAððð     *  " +
            @"AAðð      *  " +
            @"AA           " +
            @"AA2220       " +
            @"AA3331       " +
            @"AA22220 „    " +
            @"AA33331      " +
            @"AA           " +
            @"AAˆ          " +
            @"AA÷          " +
            @"AA÷   ?à     " +
            @"AAððð ?      " +
            @"AAðððð?      " +
            @"AAðððð       " +
            @"AA           " +
            @"AA           " +
            @"AA22220à…    " +
            @"AA33331á     " +
            @"AA           " +
            @"AA           " +
            @"AAððð        " +
            @"AAðððððð     " +
            @"AAððððð      " +
            @"AA‡          " +
            @"AACCCCCCCCC  " +
            @"AA÷CCCCCCCC  " +
            @"AA÷CCCCCCCC‰ " +
            @"AACCCCCCCCC  " +
            @"AAAAACCCCCC  " +
            @"AAAAAAACCCCC " +
            @"AAAAAAACCCCC " +
            @"AAAAAAA‰     " +
            @"AAAAAAA      " +
            @"ACCC         " +
            @"ACCC÷        " +
            @"AACC÷        " +
            @"AACC         " +
            @"AACC         " +
            @"AAðððððð     " +
            @"AAððð        " +
            @"AAWWWWW‰     " +
            @"AA÷          " +
            @"AA÷   $á   * " +
            @"AAððððððð  * " +
            @"AAðððððððð * " +
            @"AA‡          " +
            @"AAWWWWW‰     " +
            @"AA÷          " +
            @"AA÷        * " +
            @"AA‡        * " +
            @"AAððð $í   * " +
            @"AAðð         " +
            @"AAWWWWW‰     " +
            @"AAððð        " +
            @"AAððð        " +
            @"AA           " +
            @"AA÷   W      " +
            @"AA÷   W      " +
            @"AA    W‰     " +
            @"A            " +
            @"  K          " +
            @"         K   " +
            @"  K          " +
            @"         K   " +
            @"  K          " +
            @"A            " +
            @"AA‡   W      " +
            @"AA    W20á…  " +
            @"AAððð W31à   " +
            @"AAðð  W220è… " +
            @"AA‡   W331á  " +
            @"AA÷   W      " +
            @"AA÷          " +
            @"A            " +
            @"  K      ?ã  " +
            @"A            " +
            @"AA           " +
            @"AAðððððð     " +
            @"AA           " +
            @"AAððððð      " +
            @"AAððððððð    " +
            @"AA           " +
            @"AAWWWWW      " +
            @"   ?á K      " +
            @"A     K    * " +
            @"AA    K    * " +
            @"AA    K    * " +
            @"A     K    * " +
            @"A‡    K    * " +
            @"AA÷   K      " +
            @"AA÷   W220 … " +
            @"A‡    W331   " +
            @"A     W20 …  " +
            @"A     W31    " +
            @"AA÷   K      " +
            @"AA÷          " +
            @"AA÷          " +
            @"AAW          " +
            @"AA           " +
            @"AAþþ         " +
            @"AA‡          " +
            @"AAðððððð     " +
            @"AAðððððððð   " +
            @"AA           " +
            @"AAððððð      " +
            @"AA           " +
            @"AAððððððð    " +
            @"AAðððððð     " +
            @"AA‡          " +
            @"AA2220ç      " +
            @"AA3331ç      " +
            @"AA           " +
            @"AA           ";
         System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
         byte[] temp = encoding.GetBytes(levelInfo);
         byte[,] Level_6a = new byte[temp.Length / 13, 13];
         for (int x = 0; x < (temp.Length / 13); x++)
         {
            for (int y = 0; y < 13; y++)
            {
               Level_6a[x, y] = temp[x * 13 + y];
            }
         }
         Level_6a[(temp.Length / 13), 0] = 0; //Storing 0 at end, not positive if necessary
         return Level_6a;
      }

      public static Buffers.WorldOptions Options_6a()
      {
         Buffers.WorldOptions options = new Buffers.WorldOptions();
         options.InitX = 2 * Buffers.W + 10;
         options.InitY = 9 * Buffers.H;
         options.SkyType = 10;
         options.WallType1 = 0;
         options.WallType2 = 0;
         options.WallType3 = 0;
         options.PipeColor = 0x30;
         options.GroundColor1 = 0x4B;
         options.GroundColor2 = 0;
         options.Horizon = 124;
         options.BackGrType = 10;
         options.BackGrColor1 = 0x36;
         options.BackGrColor2 = 0x30;
         options.Stars = 0;
         options.Clouds = 0;
         options.Design = 2;
         options.C2r = 10;
         options.C2g = 23;
         options.C2b = 8;
         options.C3r = 22;
         options.C3g = 35;
         options.C3b = 20;
         options.BrickColor = 0xB0;
         options.WoodColor = 0x48;
         options.XBlockColor = 0xA0;
         return options;
      }

      public static Buffers.WorldOptions Opt_6a()
      {
         Buffers.WorldOptions options = new Buffers.WorldOptions();
         options.InitX = 2 * Buffers.W + 10;
         options.InitY = 9 * Buffers.H;
         options.SkyType = 3;
         options.WallType1 = 0; 
         options.WallType2 = 0;
         options.WallType3 = 0;
         options.PipeColor = 0x30;
         options.GroundColor1 = 0x4B;
         options.GroundColor2 = 0;
         options.Horizon = 124;
         options.BackGrType = 10;
         options.BackGrColor1 = 0x36;
         options.BackGrColor2 = 0x30;
         options.Stars = 0;
         options.Clouds = 0;
         options.Design = 2;
         options.C2r = 10;
         options.C2g = 23;
         options.C2b = 8;
         options.C3r = 22;
         options.C3g = 35;
         options.C3b = 20;
         options.BrickColor = 0xB0;
         options.WoodColor = 0x48;
         options.XBlockColor = 0xA0;
         return options;
      }

      public static byte[,] Level_6b()
      {
         string levelInfo =
            @"AAA          " +
            @"AAA          " +
            @"AAA2220è     " +
            @"AAA3331à     " +
            @"AAA          " +
            @"AAAððððð     " +
            @"AAAðððð      " +
            @"AAA÷      ** " +
            @"AAA÷  K  ****" +
            @"AAAððð    ** " +
            @"AAAðððð      " +
            @"AAA‡         " +
            @"AAACCCC      " +
            @"AAACCCC‡     " +
            @"AA÷CCCCC     " +
            @"AA÷CCCCC     " +
            @"AA÷CCCCC     " +
            @"AACCCCCCC    " +
            @"AAAAAACCC    " +
            @"AAAAAA÷CC    " +
            @"AAAAAA÷CC    " +
            @"AAAAAACCC    " +
            @"AAACCCCCC    " +
            @"ACCCCCCCC    " +
            @"ACCCCC       " +
            @"ACCCCC       " +
            @"AACCCC       " +
            @"AA‡          " +
            @"AA   K     ?à" +
            @"AAðð         " +
            @"AAðððð       " +
            @"AAððððð      " +
            @"AA           " +
            @"AAX‰         " +
            @"AA   K       " +
            @"AA‡          " +
            @"AA           " +
            @"AAðððð       " +
            @"AA           " +
            @"AA÷       ** " +
            @"AA÷  K   ****" +
            @"AAððð     ** " +
            @"AA‡          " +
            @"AAðððð       " +
            @"AAððððð      " +
            @"AA           " +
            @"AA2220á      " +
            @"AA3331á      " +
            @"AA           " +
            @"AA           ";
         System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
         byte[] temp = encoding.GetBytes(levelInfo);
         byte[,] Level_6b = new byte[temp.Length / 13, 13];
         for (int x = 0; x < (temp.Length / 13); x++)
         {
            for (int y = 0; y < 13; y++)
            {
               Level_6b[x, y] = temp[x * 13 + y];
            }
         }
         Level_6b[(temp.Length / 13), 0] = 0; //Storing 0 at end, not positive if necessary
         return Level_6b;
      }

      public static Buffers.WorldOptions Options_6b()
      {
         Buffers.WorldOptions options = new Buffers.WorldOptions();
         options.InitX = 2 * Buffers.W + 10;
         options.InitY = 9 * Buffers.H;
         options.SkyType = 11;
         options.WallType1 = 2;
         options.WallType2 = 0;
         options.WallType3 = 0;
         options.PipeColor = 0x30;
         options.GroundColor1 = 0xB0;
         options.GroundColor2 = 0x71;
         options.Horizon = 124;
         options.BackGrType = 9;
         options.BackGrColor1 = 0x36;
         options.BackGrColor2 = 0x30;
         options.Stars = 0;
         options.Clouds = 0;
         options.Design = 2;
         options.C2r = 10;
         options.C2g = 20;
         options.C2b = 8;
         options.C3r = 20;
         options.C3g = 40;
         options.C3b = 16;
         options.BrickColor = 0xB0;
         options.WoodColor =  0x48;
         options.XBlockColor = 0x30;
         return options;
      }  
   }
}
