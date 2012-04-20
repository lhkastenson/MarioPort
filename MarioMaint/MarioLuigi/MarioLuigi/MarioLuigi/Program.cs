using System;

namespace MarioLuigi
{
#if WINDOWS || XBOX
   static class Program
   {
      /// <summary>
      /// The main entry point for the application.
      /// </summary>
      static void Main(string[] args)
      {
         using (Platform game = new Platform())
         {
            game.Run();
         }
      }
   }
#endif
}

