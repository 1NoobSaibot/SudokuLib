using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuLib
{
    class Iterator
    {
        Cell cell;
        Iterator next;
        bool[] domain = null;

        internal Iterator(bool[,] used, Cell[,] cells, int amount, Random rnd)
        {
            int x, y;
            do
            {
                x = rnd.Next() % 9;
                y = rnd.Next() % 9;
            } while (used[x, y]);

            cell = cells[x, y];
            used[x, y] = true;

            if (amount > 1) next = new Iterator(used, cells, amount - 1, rnd);
        }

        internal bool startGenerate()
        {
            domain = cell.domain(domain);
            for (int i = 1; i < 10; i++)
                if (domain[i])
                {
                    cell.Value = (byte)i;
                    if (next == null) return true;
                    else if (next.startGenerate()) return true;
                }
            cell.Value = 0;
            return false;
        }
    }
}
