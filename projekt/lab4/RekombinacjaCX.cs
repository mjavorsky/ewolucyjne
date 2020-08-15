using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4
{
    class RekombinacjaCX : Rekombinacja
    {
        public override Osobnik DokonajRekombinacji(Osobnik mama, Osobnik tata, Mutacja mutacja, Fenotyp fenotyp, FunkcjaCelu funkcja_celu)
        {
            int liczba_miast = Miasta.Liczba_miast;
            int[] genotyp_dziecka = new int[liczba_miast];

            Osobnik dziecko;

            int[] na_którym_miejscu_w_matce = new int[liczba_miast];

            for (int i = 0; i < liczba_miast; i++)
                na_którym_miejscu_w_matce[mama.Genotyp[i] - 1] = i;

            int indeks = 0;

            do
            {
                genotyp_dziecka[indeks] = mama.Genotyp[indeks];
                indeks = na_którym_miejscu_w_matce[tata.Genotyp[indeks] - 1];
            } while (indeks != 0);

            for (int i = 0; i < liczba_miast; i++)
                if (genotyp_dziecka[i] == 0)
                    genotyp_dziecka[i] = tata.Genotyp[i];

            double wylosowana_liczba = LosowaKlasa.Los.NextDouble();
            if (wylosowana_liczba < mutacja.Prawdopodobieństwo_mutacji)
                genotyp_dziecka = mutacja.Mutuj(genotyp_dziecka);

            dziecko = new Osobnik(genotyp_dziecka, fenotyp, funkcja_celu);

            return dziecko;
        }
    }
}
