using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4
{
    class RekombinacjaCięcieIŁączenie : Rekombinacja
    {
        public override Osobnik DokonajRekombinacji(Osobnik mama, Osobnik tata, Mutacja mutacja, Fenotyp fenotyp, FunkcjaCelu funkcja_celu)
        {
            int liczba_miast = Miasta.Liczba_miast;
            Osobnik dziecko;

            int[] genotyp_dziecka = new int[liczba_miast];

            int punkt_przecięcia = LosowaKlasa.Los.Next(liczba_miast);

            for (int i = 0; i < punkt_przecięcia; i++)
                genotyp_dziecka[i] = mama.Genotyp[i];

            for (int i = punkt_przecięcia; i < liczba_miast; i++)
                genotyp_dziecka[i] = tata.Genotyp[i];


            double wylosowana_liczba = LosowaKlasa.Los.NextDouble();
            if (wylosowana_liczba < mutacja.Prawdopodobieństwo_mutacji)
                genotyp_dziecka = mutacja.Mutuj(genotyp_dziecka);

            dziecko = new Osobnik(genotyp_dziecka, fenotyp, funkcja_celu);

            return dziecko;
        }
    }
}
