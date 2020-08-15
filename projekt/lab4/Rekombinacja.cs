using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4
{
    abstract class Rekombinacja
    {
        public abstract Osobnik DokonajRekombinacji(Osobnik mama, Osobnik tata, Mutacja mutacja, Fenotyp fenotyp, FunkcjaCelu funkcja_celu);
    }
}
