using SudokuLib;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        // Параллельная задача, которая выполняет печать.
        // Нужна для печати во время генерации
        static Task printer = new Task(Print);

        // Сам судоку
        static Sudoku sudoku = new Sudoku();

        // Точка входа
        static void Main(string[] args)
        {
            // запуск задачи-Принтера
            printer.Start();

            // Инициализирует поле для игры
            sudoku.initGame(62);

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

        // Печать судоку каждые 0.15 секунды
        static void Print()
        {
            do
            {
                Console.CursorLeft = 0;
                Console.CursorTop = 0;
                sudoku.print();

                //Задержка на 0.15 секунд
                Thread.Sleep(150);
            } while (true);
        }
    }
}
