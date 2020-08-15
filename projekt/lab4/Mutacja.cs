using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4
{
    abstract class Mutacja
    {
        double prawdopodobieństwo_mutacji;

        public Mutacja(double prawdopodobieństwo_mutacji)
        {
            this.prawdopodobieństwo_mutacji = prawdopodobieństwo_mutacji;
        }

        public double Prawdopodobieństwo_mutacji
        {
            get
            {
                return prawdopodobieństwo_mutacji;
            }

            set
            {
                prawdopodobieństwo_mutacji = value;
            }
        }

        public abstract int[] Mutuj(int[] osobnik);
    }
}
