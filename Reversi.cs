using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Board_Game
{
    class Reversi
    {
        // reversi board
        public int[,] Board;
        // number of cells
        public const int Cell = 8;
        // no stone
        public const int NONE = 0;
        // white stone
        public const int WHITE = 1;
        // black stone
        public const int BLACK = -1;
        // Turn true = black
        public bool Turn;
        // board history
        public List<int[,]> BoardHistory;
        // turn history
        public List<bool> TurnHistory;

        // constructor
        public Reversi()
        {
            this.Init();
        }

        // initialize board
        public void Init()
        {
            this.Board = new int[Cell, Cell];
            this.Board[3, 3] = WHITE;
            this.Board[4, 4] = WHITE;
            this.Board[3, 4] = BLACK;
            this.Board[4, 3] = BLACK;
            this.Turn = true;

            this.BoardHistory = new List<int[,]>();
            this.TurnHistory = new List<bool>();
        }


        // check valid move
        public bool CanPut(int x, int y)
        {
            //put anyway
            var ret = Put(x, y);
            //if not valid, return false
            if (ret == false)
                return false;
            Undo();
            return true;
        }

        // undo -1
        public void Undo()
        {
            // current index -1
            int n = this.BoardHistory.Count - 1;
            if (n < 0)
                return;
            // back boardhistory to -1 status
            this.Board = this.BoardHistory[n];
            this.Turn = this.TurnHistory[n];
            // remove -1 history
            this.BoardHistory.RemoveAt(n);
            this.TurnHistory.RemoveAt(n);
        }
        // undo -2
        public void Undo2()
        {
            int n = this.BoardHistory.Count - 2;
            if (n < 0)
                return;
            this.Board = this.BoardHistory[n];
            this.Turn = this.TurnHistory[n];
            this.BoardHistory.RemoveAt(n);
            this.TurnHistory.RemoveAt(n);
        }
        //public void Redo()
        //{
        //    // current index + 1
        //    int n = this.BoardHistory.Count + 1;
        //    if (n < 0)
        //        return;
        //    this.Board = this.BoardHistory[n];
        //    this.Turn = this.TurnHistory[n];
        //    //this.BoardHistory.RemoveAt(n);
        //    //this.TurnHistory.RemoveAt(n);
        //}


        //Change turn
        private void ChangeTurn()
        {
            this.Turn = !this.Turn;
            for (int i = 0; i < Cell; i++)
            {
                for (int j = 0; j < Cell; j++)
                {
                    // if a valid cell was available, return true
                    if (CanPut(i, j) == true)
                        return;
                }
            }

            // if there is no valid cell, change turn
            this.Turn = !this.Turn;
        }


        //check finish
        public bool Finish()
        {
            // skip turn?
            for (int i = 0; i < Cell; i++)
            {
                for (int j = 0; j < Cell; j++)
                {
                    if (CanPut(i, j) == true)
                        return false;
                }
            }

            // finish when two skips
            return true;
        }

        // count stones
        public int CountStone(int target)
        {
            int count = 0;
            for (int i = 0; i < Cell; i++)
                for (int j = 0; j < Cell; j++)
                    if (Board[i, j] == target)
                        count++;

            return count;
        }

        // put stone and change turn
        public bool PutStone(int x, int y)
        {
            // put anyway
            var flag = Put(x, y);

            if (flag == false)
                return false;
            
            ChangeTurn();

            return true;
        }

        // random put for PC
        public void PCput()
        {
            if (Finish() == true)
                return;
            // select random 0-8 number for x,y
            Random rput = new Random();
            int x = rput.Next(0, Cell);
            int y = rput.Next(0, Cell);
            if (InRange(x, y) == false)
            {
                return;
            }
            if (InRange(x, y) == true)
            {
                PutStone(x, y);
            }
            else Undo();
        }

        // flip stone
        private bool Reverse(int x, int y, int dx, int dy)
        {
            var flip = NowStone();
            var defense = -flip;

            // if the direction was out of range, return false
            if (InRange(x + dx, y + dy) == false)
                return false;
            // return false if there was no opponent stone in +1 cells
            if (Board[x + dx, y + dy] != defense)
                return false;
            // and check further
            for (int i = 2; i < Cell; i++)
            {
                int index_x = x + i * dx;
                int index_y = y + i * dy;

                if (InRange(index_x, index_y) == false)
                {
                    return false;
                }
                else if (Board[index_x, index_y] == flip)
                {
                    //flip if found opponent stones
                    for (; i >= 1; i--)
                        Board[x + i * dx, y + i * dy] = flip;
                    return true;
                }
                else if (Board[index_x, index_y] == NONE)
                {
                    // otherwise, return false
                    return false;
                }
            }

            // if there was no attacker's stone, return false
            return false;
        }


        // method for putting current attacker's stone
        private int NowStone()
        {
            if (this.Turn)
                return BLACK;
            else
                return WHITE;
        }

        // put stone and flip stones
        private bool Put(int x, int y)
        {
            // return false if out of range
            if (InRange(x, y) == false)
                return false;
            // return dalse if there was a stone
            if (Board[x, y] != NONE)
                return false;

            // check flipped?
            bool isChanged = false;
            // save current board status
            var currentBoard = (int[,])(this.Board.Clone());
            // save current turn
            var currentTurn = this.Turn;

            // calculation for flip
            for (int i = 0; i < 9; i++)
            {
                //put -1 to 1 to dx and dy
                int dx = i / 3 - 1;
                int dy = i % 3 - 1;

                //if not both dx/dy were 0, check direction of flip
                if (dx != 0 || dy != 0)
                    isChanged = isChanged | Reverse(x, y, dx, dy);
            }

            // no stone was flipped = return false
            if (isChanged == false)
                return false;

            // put stone
            this.Board[x, y] = NowStone();

            // save board status
            this.BoardHistory.Add(currentBoard);
            this.TurnHistory.Add(currentTurn);

            return true;
        }

        // check if it is in range or not
        private bool InRange(int x, int y)
        {
            if (x < 0 || x >= Cell)
                return false;
            if (y < 0 || y >= Cell)
                return false;

            return true;
        }

    }
}
