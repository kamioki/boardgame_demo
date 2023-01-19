using System;
using static System.Console;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Board_Game
{
    class Gomoku
    {
        public int[,] Board;
        public const int Cell = 15;
        public const int NONE = 0;
        public const int WHITE = 1;
        public const int BLACK = -1;
        //public bool Turn;
        //private List<int[,]> BoardHistory;
        //private List<bool> TurnHistory;
        public Gomoku()
        {
            this.Init();
        }
        public void Init()
        {
            this.Board = new int[Cell, Cell];
            this.Board[7, 7] = WHITE;
            this.Board[8, 8] = WHITE;
            this.Board[7, 8] = BLACK;
            this.Board[8, 7] = BLACK;
            //    this.Turn = true;
            //    this.BoardHistory = new List<int[,]>();
            //    this.TurnHistory = new List<bool>();
        }
    }
}
