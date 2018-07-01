using SudokuLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static Task printer = new Task(Print);
        static Sudoku sudoku = new Sudoku();
        static void Main(string[] args)
        {
            printer.Start();
            sudoku.generate();
        }

        static void Print()
        {
            do
            {
                Console.CursorLeft = 0;
                Console.CursorTop = 0;
                sudoku.print();
                Thread.Sleep(15);
            } while (true);
        }
    }
}
