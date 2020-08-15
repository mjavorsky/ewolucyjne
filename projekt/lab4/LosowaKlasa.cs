using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4
{
    static class LosowaKlasa
    {
        static Random los = new Random();

        public static Random Los
        {
            get
            {
                return los;
            }
        }
    }
}
