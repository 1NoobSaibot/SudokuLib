using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuLib
{
    public class RandomLocker
    {
        static Random rnd = new Random();
        static object locker = new object();

        public int Next()
        {
            lock (locker)
            {
                return rnd.Next();
            }
        }
    }
}
