using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Board_Game
{
    class History : Reversi
    {
        
        public void SaveHistory()
        {
            //Save game history to text file
            //File.WriteAllLines("history.txt", this.BoardHistory);
            List<int[,]> n = this.BoardHistory;
        }
        public static void LoadHistory()
        {
            //Load game history to current list
        }
    }
}
