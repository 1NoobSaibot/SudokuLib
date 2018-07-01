using SudokuLib;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        // Параллельная задача, которая решает лёгкий судоку.
        // Нужна для теста режима игры
        static Task solver = new Task(Solve);

        // Сам судоку
        static Sudoku sudoku = new Sudoku();

        // Точка входа
        static void Main(string[] args)
        {
            // Инициализирует поле для игры
            sudoku.initGame(3);
            
            // Прикрепление обработчика событтия РЕШЕНО
            sudoku.OnSolved += SudokuSolved;
            
            // запуск Запуск решателя
            solver.Start();

            // Обрати внимание, Виктория, что на данном этапе генерация выполняется условно непрерывно
            // И в то же самое время Task Принтер выполняет метод Print() печатающий судоку в консоль
            // Выполнение было разбито на два параллельных потока


            // Чтобы программа не завершилась, был добавлен бесконечный цикл
            do
            {
                Thread.Sleep(1500);
            } while (true);
            // Ибо Console.ReadKey() тут не подходит.
        }

        // Функция потока решающего судоку
        static void Solve()
        {
            sudoku.print();
            Console.Write("\n" + sudoku.voidCellAmount + "\n\n");
            for (int y = 0; y < 9; y++)
                for (int x = 0; x < 9; x++)
                    if (sudoku[x, y] == 0)
                    {
                        for (int i = 1; i < 10; i++)
                        {
                            Thread.Sleep(200);
                            sudoku[x, y] = (byte)i;
                            if (sudoku[x, y] == i)
                            {
                                sudoku.print();
                                Console.Write("\n" + sudoku.voidCellAmount + "\n\n");
                            }
                        }
                    }
        }

        // Слушатель события СудокуРешено
        static void SudokuSolved(Sudoku sender)
        {
            Console.WriteLine("Congratulations!!!");
        }
    }
}
