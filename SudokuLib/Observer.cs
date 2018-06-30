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
        bool[] used = new bool[10];

        internal void Add(Cell cell)
        {
            for (int i = 0; i < 9; i++)
                if (cells[i] == null)
                {
                    cells[i] = cell;
                    cell.AddObserver(this);
                    return;
                }
            throw new Exception();
        }


    }
}
