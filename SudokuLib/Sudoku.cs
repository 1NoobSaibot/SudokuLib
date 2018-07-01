using System;

namespace SudokuLib
{
    /// <summary>
    /// Игровое поле судоку
    /// </summary>
    public class Sudoku
    {
        /// <summary>
        /// Массив ячеек
        /// </summary>
        Cell[,] cells = new Cell[9, 9];

        /// <summary>
        /// Создаёт совершенно пустой судоку. Для заполнения используйте метод generate()
        /// </summary>
        public Sudoku()
        {
            // Создание ячеек
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

            // Квадраты 3*3
            for (int X = 0; X < 3; X++)
                for (int Y = 0; Y < 3; Y++)
                {
                    Observer obs = new Observer();
                    for (int x = 0; x < 3; x++)
                        for (int y = 0; y < 3; y++)
                            obs.Add(cells[X*3 + x, Y*3 + y]);
                }
        }

        /// <summary>
        /// Заполняет поле псевдослучайными числами.
        /// </summary>
        public void generate()
        {
            bool[,] used = new bool[9, 9];
            Iterator root = new Iterator(used, cells, 81, new Random());
            root.startGenerate();
        }


        /// <summary>
        /// Выводит судоку в консоль
        /// </summary>
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
