using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4
{
    class MutacjaKolejkowa : Mutacja
    {
        public MutacjaKolejkowa(double prawdopodobieństwo_mutacji) : base(prawdopodobieństwo_mutacji) {}

        public override int[] Mutuj(int[] osobnik)
        {
            int liczba_miast = Miasta.Liczba_miast;
            int wylosowany_indeks = LosowaKlasa.Los.Next(liczba_miast);
            int nowa_współrzędna = LosowaKlasa.Los.Next(1, liczba_miast + 1 - wylosowany_indeks);
            osobnik[wylosowany_indeks] = nowa_współrzędna;
            return osobnik;
        }
    }
}
