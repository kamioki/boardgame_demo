using System;
using static System.Console;
using System.Collections.Generic;
using System.IO;

namespace Board_Game
{
    class Menu: Reversi
    {
        public static void GameMode()
        {
            var reversi = new Reversi();
            var gomoku = new Gomoku();
            string game;
            //Select GameMode
            WriteLine("\n  ==== WELCOME TO BORAD GAME =====\n\nPlease input 'r' for Reversi or 'g' for Gomoku");
            game = ReadLine();
            while (game != "r")
            {
                if (game == "r")
                {
                    Show(reversi);
                }
                if (game == "g")
                {
                    Show(gomoku);
                    WriteLine("Sorry, you can't play Gomoku for now. Select Reversi");
                }
                else
                {
                    WriteLine("Invalid game. Select 'r' or 'g'");
                }
                game = ReadLine();
            }
        }

        public static void Play()
        {
            int x, y;
            string mode;
            string command;
            var reversi = new Reversi();
            //Choose PlayMode
            WriteLine("Select Play Mode:\n 'hc' (● Human vs ○ Computer) or 'hh' (● Human1 vs ○ Human2)");
            mode = ReadLine();
                while (reversi.Finish() == false)
            {
                while (mode != "hh" && mode != "hc" && mode!="cc")
                {
                    WriteLine("Invalid Play Mode!:\n Enter 'HC' (vs ○ Computer) or 'HH' (vs ○ Human2)");
                    mode = ReadLine();
                }
                Show(reversi);
                WriteLine("Mode: {0}\n", mode);
                //To check whole move
                if (mode == "cc")
                {
                    reversi.PCput();
                }
                //Human vs Human input message
                if (mode == "hh")
                {
                    Write("Input 'undo' or hit Enter key to ignore or 'h' for help>> ");
                    command = ReadLine();
                    if (command == "undo") { reversi.Undo(); }
                    if (command == "h") { Help.ShowHelp(); }
                    //can not redo
                    //if (command == "redo") { reversi.Redo(); }
                    else
                    {
                        Write("input column number>> ");
                        if (int.TryParse(ReadLine(), out x) == false) continue;
                        Write("input row number>> ");
                        if (int.TryParse(ReadLine(), out y) == false) continue;
                        reversi.PutStone(x, y);
                    }
                }
                //Human vs Computer input message
                else if (mode == "hc" && reversi.Turn)
                { 
                    Write("Back to your turn? Input 'back' or hit Enter key to ignore or 'h' for help>> ");
                    command = ReadLine();
                    if (command == "back") { reversi.Undo2(); }
                    if (command == "h") { Help.ShowHelp(); }
                    //can not redo
                    //if (command == "redo") { reversi.Redo(); }
                    else
                    {
                        Write("Your turn!\ninput column number>> ");
                        if (int.TryParse(ReadLine(), out x) == false) continue;
                        Write("input row number>> ");
                        if (int.TryParse(ReadLine(), out y) == false) continue;
                        reversi.PutStone(x, y);
                    }
                }
                else
                {
                    WriteLine("\nFinish turn");
                    reversi.PCput();
                }
            }
        }
        //Display reversi board
        static void Show(Reversi reversi)
        {
            //Clear console to show one single board
            //Clear();
            Write(" \n");
            Write(" ");
            //repeat board marker element
            for (int i = 0; i < Reversi.Cell; i++)
                Write(" {0}", i);
            WriteLine();
            for (int i = 0; i < Reversi.Cell; i++)
            {
                Write(i);
                for (int j = 0; j < Reversi.Cell; j++)
                {
                    switch (reversi.Board[j, i])
                    {
                        case Reversi.BLACK:
                            Write("●");
                            break;
                        case Reversi.WHITE:
                            Write("○");
                            break;
                        default:
                            Write(" ･");
                            break;
                    }
                }
                WriteLine();
            }
        }
        //Display Gomoku board 
        static void Show(Gomoku gomoku)
        {
            Write(" ");
            for (int i = 0; i < Gomoku.Cell; i++)
                Write(" {0:D2}", i);
            WriteLine();
            //repeat board marker element
            for (int i = 0; i < Gomoku.Cell; i++)
            {
                Write("{0:D2}", i);
                for (int j = 0; j < Gomoku.Cell; j++)
                {
                    switch (gomoku.Board[j, i])
                    {
                        case Gomoku.BLACK:
                            Write(" ●");
                            break;
                        case Gomoku.WHITE:
                            Write(" ○");
                            break;
                        default:
                            Write(" ･ ");
                            break;
                    }
                }
                WriteLine();
            }
        }
    }
}
