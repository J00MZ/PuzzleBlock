﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace PuzzleBlock
{
    interface IGameDrawer
    {
        string ChoosePlacement();
        int ChooseShape();
        void DrawBoard(Board board);
        void DrawShape(Shape shape, int ordinal, bool canFit);
        void ErrorMessage(string message);
        void DrawStats(Board board);
    }

    public class ConsoleGameDrawer : IGameDrawer
    {
        public void DrawBoard(Board board)
        {
            Console.WriteLine(" --- Board - Score: {0} ---", board.Score);
            Console.WriteLine("");
            Console.WriteLine("    A   B   C   D   E   F   G   H");
            Console.WriteLine("  ┌───┬───┬───┬───┬───┬───┬───┬───┐");
            for (int x = 0; x <= 7; x++)
            {
                if (x != 0)
                    Console.WriteLine("  ├───┼───┼───┼───┼───┼───┼───┼───┤");
                Console.Write((x+1)+" │");
                for (int y = 0; y <= 7; y++)
                {
                    if (board.Cells[x][y])
                    {
                        Console.Write(" █ ");
                    }
                    else
                    {
                        Console.Write("   ");
                    }
                    if (y < 7)
                        Console.Write("│");
                    else
                        Console.WriteLine("│ "+(x+1));
                }
            }
            Console.WriteLine("  └───┴───┴───┴───┴───┴───┴───┴───┘");
            Console.WriteLine("    A   B   C   D   E   F   G   H");
            Console.WriteLine();
        }

        public void DrawShape(Shape shape, int ordinal, bool canFit)
        {
            Console.WriteLine(" - Shape #{0} ({1})", ordinal+1, shape?.Name);

            if (shape == null)
                return;

            var block = canFit ? "█" : "▒";
            for (int x = 0; x <= shape.Bits.GetUpperBound(0); x++)
            {
                for (int y = 0; y <= shape.Bits.GetUpperBound(1); y++)
                    Console.Write(shape.Bits[x, y] ? block : "░");
                Console.WriteLine();
            }

            Console.WriteLine();
        }

        public string ChoosePlacement()
        {
            while (true)
            {
                Console.Write("Choose placement [A-H][1-8]: ");
                var res = Console.ReadLine()?.ToLower();
                if (res != null && res[0] >= 'a' && res[0] <= 'h')
                    if ((int)res[1] >= 49 && (int)res[1] <= 57)
                        return res;
            }
        }

        public int ChooseShape()
        {
            while (true)
            {
                Console.Write("Choose shape [1-3]: ");
                var key = Console.ReadKey();
                Console.WriteLine("");
                if (key.KeyChar == '1')
                    return 0;
                if (key.KeyChar == '2')
                    return 1;
                if (key.KeyChar == '3')
                    return 2;
                if (key.KeyChar == '4')
                    return 3;
            }
        }

        public void ErrorMessage(string message)
        {
            Console.WriteLine(message);
        }

        public void DrawStats(Board board)
        {
            Console.WriteLine("Stats:");
            Console.WriteLine(" + Score: {0}", board.Score);
            Console.WriteLine(" + Placements: {0}", board.Stats.Placements);
            Console.WriteLine(" + CellCount Average: {0}", board.Stats.AvgCellCount);
            for (int i = 0; i < 8; i++)
                Console.WriteLine(" + {0} Lines Cleared {1,3} per Placement: {2}", i+1, board.Stats.Lines[i], (float)(board.Stats.Lines[i])/board.Stats.Placements);
        }
    }
}