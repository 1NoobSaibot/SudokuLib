using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuLib
{
    public class Sudoku
    {
        Cell[,] cells = new Cell[9, 9];

        public Sudoku()
        {
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    cells[i, j] = new Cell();

            // Горизонтальные линии
            for (int y = 0; y < 9; y++)
            {
                Observer obs = new Observer();
                for (int x = 0; x < 9; x++)
                    obs.Add(cells[x, y]);
            }

            // Вертикальные линии
            for (int x = 0; x < 9; x++)
            {
                Observer obs = new Observer();
                for (int y = 0; y < 9; y++)
                    obs.Add(cells[x, y]);
            }

            for (int X = 0; X < 3; X++)
                for (int Y = 0; Y < 3; Y++)
                {
                    Observer obs = new Observer();
                    for (int x = 0; x < 3; x++)
                        for (int y = 0; y < 3; y++)
                            obs.Add(cells[X*3 + x, Y*3 + y]);
                }
        }

        public void generate()
        {
            bool[,] used = new bool[9, 9];
            Iterator root = new Iterator(used, cells, 81, new Random());
            root.startGenerate();
        }

        public void print()
        {
            for (int Y = 0; Y < 3; Y++)
            {
                for (int y = 0; y < 3; y++)
                {
                    for (int X = 0; X < 3; X++)
                    {
                        for (int x = 0; x < 3; x++)
                            Console.Write(cells[X * 3 + x, Y * 3 + y].Value + " ");
                        Console.Write(' ');
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
            }
        }
    }
}
