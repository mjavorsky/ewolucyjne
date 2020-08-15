using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4
{
    class RekombinacjaPMX : Rekombinacja
    {
        public override Osobnik DokonajRekombinacji(Osobnik mama, Osobnik tata, Mutacja mutacja, Fenotyp fenotyp, FunkcjaCelu funkcja_celu)
        {
            int liczba_miast = Miasta.Liczba_miast;
            int[] genotyp_dziecka = new int[liczba_miast];

            Osobnik dziecko;

            int punkt_przecięcia = LosowaKlasa.Los.Next(liczba_miast);
            int punkt_przecięcia2 = LosowaKlasa.Los.Next(liczba_miast);


            bool[] czy_dziecko_zawiera = new bool[liczba_miast];
            int[] na_którym_miejscu_w_ojcu = new int[liczba_miast];

            for (int i = 0; i < liczba_miast; i++)
                na_którym_miejscu_w_ojcu[tata.Genotyp[i] - 1] = i;

            if (punkt_przecięcia > punkt_przecięcia2)
            {
                for (int i = punkt_przecięcia; i < liczba_miast; i++)
                {
                    genotyp_dziecka[i] = tata.Genotyp[i];
                    czy_dziecko_zawiera[genotyp_dziecka[i] - 1] = true;
                }

                for (int i = 0; i < punkt_przecięcia2; i++)
                {
                    genotyp_dziecka[i] = tata.Genotyp[i];
                    czy_dziecko_zawiera[genotyp_dziecka[i] - 1] = true;
                }

                for (int i = punkt_przecięcia2; i < punkt_przecięcia; i++)
                {
                    int tymczasowa_liczba = mama.Genotyp[i];
                    while (czy_dziecko_zawiera[tymczasowa_liczba - 1])
                        tymczasowa_liczba = mama.Genotyp[na_którym_miejscu_w_ojcu[tymczasowa_liczba - 1]];
                    genotyp_dziecka[i] = tymczasowa_liczba;
                }
            }
            else
            {
                for (int i = punkt_przecięcia; i < punkt_przecięcia2; i++)
                {
                    genotyp_dziecka[i] = tata.Genotyp[i];
                    czy_dziecko_zawiera[genotyp_dziecka[i] - 1] = true;
                }

                for (int i = 0; i < punkt_przecięcia; i++)
                {
                    int tymczasowa_liczba = mama.Genotyp[i];
                    while (czy_dziecko_zawiera[tymczasowa_liczba - 1])
                        tymczasowa_liczba = mama.Genotyp[na_którym_miejscu_w_ojcu[tymczasowa_liczba - 1]];
                    genotyp_dziecka[i] = tymczasowa_liczba;
                }

                for (int i = punkt_przecięcia2; i < liczba_miast; i++)
                {
                    int tymczasowa_liczba = mama.Genotyp[i];
                    while (czy_dziecko_zawiera[tymczasowa_liczba - 1])
                        tymczasowa_liczba = mama.Genotyp[na_którym_miejscu_w_ojcu[tymczasowa_liczba - 1]];
                    genotyp_dziecka[i] = tymczasowa_liczba;
                }
            }

            double wylosowana_liczba = LosowaKlasa.Los.NextDouble();
            if (wylosowana_liczba < mutacja.Prawdopodobieństwo_mutacji)
                genotyp_dziecka = mutacja.Mutuj(genotyp_dziecka);

            dziecko = new Osobnik(genotyp_dziecka, fenotyp, funkcja_celu);

            return dziecko;
        }
    }
}
