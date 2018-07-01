using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuLib
{
    class Observer
    {
        Cell[] cells = new Cell[9];
        bool[] used = new bool[10] { false, false, false, false, false, false, false, false, false, false };

        internal void Add(Cell cell)
        {
            for (int i = 0; i < 9; i++)
                if (cells[i] == null)
                {
                    cells[i] = cell;
                    cell.AddObserver(this);
                    cell.ValueChanged += ValueChanged;
                    return;
                }
            throw new Exception();
        }

        internal bool ValueIsUsed(int Value)
        {
            if (Value == 0) return false;
            return used[Value];
        }

        void ValueChanged(Cell sender, Cell.EventArgs args)
        {
            switch (args.eventType)
            {
                case Cell.EventArgs.EventType.Clear:
                    used[args.oldValue] = false;
                    break;
                case Cell.EventArgs.EventType.Set:
                    used[args.newValue] = true;
                    break;
                case Cell.EventArgs.EventType.Changed:
                    used[args.oldValue] = false;
                    used[args.newValue] = true;
                    break;
            }
        }

        internal void correctDomain(bool[] res)
        {
            for (int i = 1; i < res.Length; i++)
                res[i] = res[i] && !used[i];
        }
    }
}
