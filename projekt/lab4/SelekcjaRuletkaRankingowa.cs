using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4
{
    class SelekcjaRuletkaRankingowa : Selekcja
    {
        public override Osobnik DokonajSelekcji(Osobnik[] populacja, Fenotyp fenotyp, FunkcjaCelu funkcja_celu)
        {
            Osobnik[] tymczasowa_populacja = new Osobnik[Algorytm.Liczba_osobników];
            populacja.CopyTo(tymczasowa_populacja, 0);

            tymczasowa_populacja = tymczasowa_populacja.OrderByDescending(x => x.Wartość).ToArray();


            double wylosowane_prawdopodobieństwo = LosowaKlasa.Los.NextDouble();
            double akumulator_prawdopodobieństw = 0;

            for (int i = 0; i < Algorytm.Liczba_osobników; i++)
            {
                akumulator_prawdopodobieństw += prawdopodobieństwa[i];
                if (akumulator_prawdopodobieństw >= wylosowane_prawdopodobieństwo)
                    return tymczasowa_populacja[i];

            }
            Console.WriteLine("UPS");
            return new Osobnik(null, null, null);
        }
    }
}
