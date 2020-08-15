using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4
{
    static class Zegar
    {
        static Stopwatch stoper = new Stopwatch();

        public static Stopwatch Stoper
        {
            get
            {
                return stoper;
            }

            set
            {
                stoper = value;
            }
        }
    }
}
