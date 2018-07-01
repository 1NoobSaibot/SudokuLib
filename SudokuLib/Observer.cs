using System;

namespace SudokuLib
{
    /// <summary>
    /// Наблюдатель.
    /// Следит за тем, чтобы значения на линии, либо в квадрате не повторялись
    /// Важная часть внутренней логики.
    /// </summary>
    class Observer
    {
        /// <summary>
        /// Наблюдаемые ячейки
        /// </summary>
        Cell[] cells = new Cell[9];

        /// <summary>
        /// Статистика использования значений. False - значение может быть использовано, True - значение кем-то занято
        /// </summary>
        bool[] used = new bool[10] { false, false, false, false, false, false, false, false, false, false };

        /// <summary>
        /// Добавляет ячейку под наблюдение
        /// </summary>
        /// <param name="cell">Наблюдаемая ячейка</param>
        internal void Add(Cell cell)
        {
            // Поиск пустого элемента в массиве
            for (int i = 0; i < 9; i++)
                if (cells[i] == null)                           // Если АЙНЫЙ элемент массива пустой
                {
                    cells[i] = cell;                                // Сохраняем новую ячейку туда
                    cell.AddObserver(this);                         // Говорим ячейке, что мы присматриваем за ней
                    cell.ValueChanged += ValueChanged;              // Начинаем прослушивать события её изменения
                    return;
                }

            // Число ячеек ограничено девятью
            // Если же попробовать прикрепить десятую ячейку - будет ошибка
            throw new Exception();
        }

        /// <summary>
        /// Использовано ли значение у наблюдателя?
        /// </summary>
        /// <param name="Value">Значение</param>
        /// <returns>Возвращает True, если значение использовано и False в противном случае</returns>
        internal bool ValueIsUsed(int Value)
        {
            // Значение 0 (пустое) можно использовать всегда и везде
            if (Value == 0) return false;

            // Остальные лишь по разу для наблюдателя
            return used[Value];
        }

        /// <summary>
        /// Эта функция отвечает за прослушивание событий изменений ячейки
        /// Вызывается ячейкой внутри свойства Value
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="args">Аргументы события</param>
        void ValueChanged(Cell sender, Cell.EventArgs args)
        {
            // В зависимости от типа события необходимо принять разные меры
            switch (args.eventType) 
            {
                // Случай Очистка
                case Cell.EventArgs.EventType.Clear:
                    used[args.oldValue] = false;            //Разрешить использовать старое значение
                    break;

                // Случай Установка (когда пустая ячейка перестаёт быть пустой)
                case Cell.EventArgs.EventType.Set:
                    used[args.newValue] = true;             //Запретить использовать новое значение
                    break;

                // Случай Изенение (когда НЕ пустая ячейка меняет значение на другое не пустое)
                case Cell.EventArgs.EventType.Changed:
                    used[args.oldValue] = false;            //Разрешить использовать старое
                    used[args.newValue] = true;             //Запретить использовать новое
                    break;
            }
        }

        /// <summary>
        /// Корректирует область определения ячейки
        /// </summary>
        /// <param name="res">Область определения</param>
        internal void correctDomain(bool[] res)
        {
            for (int i = 1; i < res.Length; i++)
                res[i] = res[i] && !used[i];

            // Если RES[i] равен FALSE, То он таким и будет. 
            // Если же он равен TRUE, то его значение может стать FALSE, если оно уже использовано
        }
    }
}
