using System;
using static System.Console;
using System.Collections.Generic;
using System.IO;

namespace Board_Game
{
    class BoardGame
    {
        static void Main()
        {
            //initialize
            new Reversi();
            new Gomoku();
            Menu.GameMode();
            Menu.Play();
        }
    }
   
}