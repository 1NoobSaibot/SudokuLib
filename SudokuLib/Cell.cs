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
            public enum EventType { Clear = 0, Set = 1, Changed = 2 }
            public byte oldValue { get; private set; }
            public byte newValue { get; private set; }
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

            public EventArgs(byte oldValue, byte newValue)
            {
                this.oldValue = oldValue;
                this.newValue = newValue;
            }
        }

        

        internal delegate void CellEvent(Cell sender, EventArgs args);
        #endregion
        
        internal byte Value
        {
            get => pValue;
            set
            {
                if (pValue == value || observers[0].ValueIsUsed(value) || observers[1].ValueIsUsed(value) || observers[2].ValueIsUsed(value)) return;
                
                EventArgs args = new EventArgs(pValue, value);
                pValue = value;
                ValueChanged(this, args);
            }
        }
        internal event CellEvent ValueChanged;
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
            for (int i = 0; i < 3; i++)
                observers[i].correctDomain(res);
            return res;
        }

        private Observer[] observers = new Observer[3];
        private byte pValue = 0;

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
