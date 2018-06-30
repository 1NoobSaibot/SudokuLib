using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuLib
{ 
    internal class Cell
    {
        #region
        internal class EventArgs
        {
            public byte oldValue { get; private set; }
            public byte newValue { get; private set; }

            public EventArgs(byte oldValue, byte newValue)
            {
                this.oldValue = oldValue;
                this.newValue = newValue;
            }
        }

        

        internal delegate void CellEvent(EventArgs args);
        #endregion
        private byte pValue = 0;
        internal byte Value
        {
            get => pValue;
            set
            {
                if (pValue == value) return;
                EventArgs args = new EventArgs(pValue, value);
                pValue = value;

            }
        }
        internal event CellEvent ValueChanged;
        private Observer[] observers = new Observer[3];

        internal void AddObserver(Observer observer)
        {
            for (int i = 0; i < 3; i++)
                if(observers[i] == null)
                {
                    observers[i] = observer;
                    return;
                }
            throw new Exception();
        }
    }
}
