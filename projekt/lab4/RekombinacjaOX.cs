using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4
{
    class RekombinacjaOX : Rekombinacja
    {
        public override Osobnik DokonajRekombinacji(Osobnik mama, Osobnik tata, Mutacja mutacja, Fenotyp fenotyp, FunkcjaCelu funkcja_celu)
        {
            int liczba_miast = Miasta.Liczba_miast;
            int[] genotyp_dziecka = new int[liczba_miast];

            Osobnik dziecko;

            int punkt_przecięcia = LosowaKlasa.Los.Next(0, liczba_miast);
            int punkt_przecięcia2 = LosowaKlasa.Los.Next(punkt_przecięcia, liczba_miast);

            bool[] czy_użyte = new bool[liczba_miast];

            for (int i = punkt_przecięcia; i < punkt_przecięcia2; i++)
            {
                genotyp_dziecka[i] = mama.Genotyp[i];
                czy_użyte[mama.Genotyp[i] - 1] = true;
            }

            int indeks = punkt_przecięcia2;

            for (int i = punkt_przecięcia2; i < liczba_miast; i++)
            {
                while (czy_użyte[tata.Genotyp[indeks] - 1])
                {
                    indeks++;
                    indeks %= liczba_miast;
                }

                genotyp_dziecka[i] = tata.Genotyp[indeks];
                indeks++;
                indeks %= liczba_miast;
            }

            for (int i = 0; i < punkt_przecięcia; i++)
            {
                while (czy_użyte[tata.Genotyp[indeks] - 1])
                {
                    indeks++;
                    indeks %= liczba_miast;
                }

                genotyp_dziecka[i] = tata.Genotyp[indeks];
                indeks++;
                indeks %= liczba_miast;
            }

            double wylosowana_liczba = LosowaKlasa.Los.NextDouble();
            if (wylosowana_liczba < mutacja.Prawdopodobieństwo_mutacji)
                genotyp_dziecka = mutacja.Mutuj(genotyp_dziecka);

            dziecko = new Osobnik(genotyp_dziecka, fenotyp, funkcja_celu);

            return dziecko;
        }
    }
}
