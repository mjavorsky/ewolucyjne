using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4
{
    abstract class Selekcja
    {
        protected double[] prawdopodobieństwa;

        public double[] Prawdopodobieństwa
        {
            get
            {
                return prawdopodobieństwa;
            }

            set
            {
                prawdopodobieństwa = value;
            }
        }

        public abstract Osobnik DokonajSelekcji(Osobnik[] populacja, Fenotyp fenotyp, FunkcjaCelu funkcja_celu);


    }
}
