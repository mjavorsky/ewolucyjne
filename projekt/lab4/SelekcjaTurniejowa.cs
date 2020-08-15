using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4
{
    class SelekcjaTurniejowa : Selekcja
    {
        public override Osobnik DokonajSelekcji(Osobnik[] populacja, Fenotyp fenotyp, FunkcjaCelu funkcja_celu)
        {
            int indeks_kandydata1 = LosowaKlasa.Los.Next(Algorytm.Liczba_osobników);
            int indeks_kandydata2 = LosowaKlasa.Los.Next(Algorytm.Liczba_osobników);

            Osobnik kandydat1 = populacja[indeks_kandydata1];
            Osobnik kandydat2 = populacja[indeks_kandydata2];

            if (kandydat1.Wartość < kandydat2.Wartość)
                return kandydat1;
            else
                return kandydat2;
        }
    }
}
