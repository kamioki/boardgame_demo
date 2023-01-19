using System;
using System.Collections.Generic;
using static System.Console;
using System.Text;
using System.IO;

namespace Board_Game
{
    class Help
    {
        public static void ShowHelp()
        {
            //Load help text file
           string text = File.ReadAllText(@"help.txt");
           WriteLine("\n{0}", text);
           WriteLine("Press any key to exit HELP.");
            ReadKey();
        }
    }
}
