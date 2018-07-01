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
        private void generate()
        {
            bool[,] used = new bool[9, 9];
            Iterator root = new Iterator(used, cells, 81, new Random());
            root.startGenerate();
        }

        /// <summary>
        /// Инициализирует новую игру с указанным числом пустот
        /// </summary>
        /// <param name="amountOfVoids"></param>
        public void initGame(int amountOfVoids)
        {
            // Снимаем фиксацию
            for (int x = 0; x < 9; x++)
                for (int y = 0; y < 9; y++)
                    cells[x, y].Fixed = false;

            // Оставляем число подсказок в допустимых пределах
            if (amountOfVoids > 65) amountOfVoids = 65;
            else if (amountOfVoids < 0) amountOfVoids = 0;

            // Генерируем поле
            generate();

            // Расставляем пустоты
            createOfVoids(amountOfVoids);

            // Фиксируем непустые ячейки
            for (int x = 0; x < 9; x++)
                for (int y = 0; y < 9; y++)
                    if (cells[x, y].Value != 0) cells[x, y].Fixed = true;
        }

        private void createOfVoids(int Voids)
        {
            void r90(ref int x, ref int y)
            {
                int buf = 8 - y;
                y = x;
                x = buf;
            }
            int n4 = Voids / 4;
            int rest = Voids % 4;
            Random rnd = new Random();

            if (rest % 2 == 1) cells[4, 4].Value = 0;

            for (int i = 0; i < n4; i++)
            {
                int x, y;
                do {
                    x = rnd.Next() % 9;
                    y = rnd.Next() % 9;
                } while ((x == 4 && y == 4) || cells[x, y].Value == 0);

                cells[x, y].Value = 0;
                r90(ref x, ref y);
                cells[x, y].Value = 0;
                r90(ref x, ref y);
                cells[x, y].Value = 0;
                r90(ref x, ref y);
                cells[x, y].Value = 0;
            }

            if (rest / 2 == 1)
            {
                int x, y;
                do
                {
                    x = rnd.Next() % 9;
                    y = rnd.Next() % 9;
                } while ((x == 4 && y == 4) || cells[x, y].Value == 0);

                cells[x, y].Value = 0;
                r90(ref x, ref y);
                r90(ref x, ref y);
                cells[x, y].Value = 0;
            }
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
                        {
                            int v = cells[X * 3 + x, Y * 3 + y].Value;
                            if (v == 0) Console.Write("  ");
                            else Console.Write( v + " ");
                        }
                            
                        Console.Write(' ');
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
            }
        }

        
        public byte this[int x, int y]
        {
            get => cells[x, y].Value;
            set => cells[x, y].Value = value;
        }
    }
}
