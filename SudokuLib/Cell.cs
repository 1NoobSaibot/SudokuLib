using System;

namespace SudokuLib
{ 
    /// <summary>
    /// Ячейка судоку
    /// </summary>
    internal class Cell
    {
        #region Дополнительные типы данных
        /// <summary>
        /// Аргументы событий ячейки
        /// </summary>
        internal class EventArgs
        {
            /// <summary>
            /// События могут быть трёх видов: 0->X = SET   X->0=CLEAR  X->Y=CHANGE где X, Y - числа от 1 до 9
            /// </summary>
            public enum EventType { Clear = 0, Set = 1, Changed = 2 }

            /// <summary>
            /// Старое значение ячейки
            /// </summary>
            public byte oldValue { get; private set; }

            /// <summary>
            /// Новое значение
            /// </summary>
            public byte newValue { get; private set; }

            /// <summary>
            /// Возвращает тип события
            /// </summary>
            public EventType eventType
            {
                get
                {
                    if (newValue == 0) return EventType.Clear;
                    else
                    {
                        if (oldValue == 0) return EventType.Set;
                        else return EventType.Changed;
                    }
                }
            }

            /// <summary>
            /// Создаёт экземпляр аргументов события
            /// </summary>
            /// <param name="oldValue">То, что было</param>
            /// <param name="newValue">То, чем стало</param>
            public EventArgs(byte oldValue, byte newValue)
            {
                this.oldValue = oldValue;
                this.newValue = newValue;
            }
        }

        
        /// <summary>
        /// Делегат событий ячейки
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="args">Набор аргументов</param>
        internal delegate void CellEvent(Cell sender, EventArgs args);
        #endregion
        
        /// <summary>
        /// Значение ячейки 1-9, если пустая - 0
        /// </summary>
        internal byte Value
        {
            // Чтение значения
            get => pValue;

            // Запись значения
            set
            {
                 
                if (                                            // Если
                    pValue == value                                 // Новое значение равно старому
                    || observers[0].ValueIsUsed(value)              // Или наблюдатель 0 против этого присваивания
                    || observers[1].ValueIsUsed(value)              // Или наблюдатель 1 против этого присваивания
                    || observers[2].ValueIsUsed(value))             // Или наблюдатель 2 против этого присваивания
                    return;                                     // Тогда ничего не меняем, а просто выходим.
                

                // Иначе необходимо выполнить присваивание,
                EventArgs args = new EventArgs(pValue, value);
                pValue = value;

                // А затем вызвать событие! Да, само оно себя не вызовет.
                ValueChanged(this, args);
            }
        }

        /// <summary>
        /// Событие "Ячейка изменена". Возникает после присваивания свойству Value нового значения
        /// </summary>
        internal event CellEvent ValueChanged;

        /// <summary>
        /// Возвращает область определения для данной ячейки
        /// </summary>
        /// <param name="arg">массив bool[10] или null</param>
        /// <returns>Возвращает возможность использования для каждого значения 1-9</returns>
        internal bool[] domain(bool[] arg)
        {
            bool[] res;
            if (arg == null) res = new bool[10] { true, true, true, true, true, true, true, true, true, true };
            else
            {
                for (int i = 0; i < 10; i++)
                    arg[i] = true;
                res = arg;
            }

            // Заполнение массива res[] данными
            for (int i = 0; i < 3; i++)
                observers[i].correctDomain(res);

            return res;
        }

        /// <summary>
        /// Набор наблюдателей, отслеживающих данную ячейку
        /// </summary>
        private Observer[] observers = new Observer[3];


        /// <summary>
        /// Значение ячейки 1-9, если пустая - 0
        /// </summary>
        private byte pValue = 0;
        // Данное поле не доступно снаружи, ибо некорректное его изменение может сломить внутреннюю логику Судоку
        // Доступ осуществляется через свойство Value

       
        /// <summary>
        /// Добавляет наблюдателя для данной ячейки
        /// </summary>
        /// <param name="observer"></param>
        internal void AddObserver(Observer observer)
        {
            for (int i = 0; i < 3; i++)
                if(observers[i] == null)
                {
                    observers[i] = observer;
                    return;
                }
            // Пока что количество наблюдателей должно быть ровно три:
            // Наблюдатель вертикальной линии
            // Наблюдатель горизонтальной линии
            // Наблюдатель 3*3-квадрата

            // Если была попытка добавить четвёртого наблюдателя - возникнет ошибка
            throw new Exception();
        }
    }
}
