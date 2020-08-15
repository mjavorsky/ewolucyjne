using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4
{
    class MutacjaPrzezZamianęMiastKolejkowo : Mutacja
    {
        public MutacjaPrzezZamianęMiastKolejkowo(double prawdopodobieństwo_mutacji) : base(prawdopodobieństwo_mutacji) { }

        public override int[] Mutuj(int[] osobnik)
        {
            int liczba_miast = Miasta.Liczba_miast;
            int[] zmutowany_osobnik = new int[liczba_miast];
            Populacja.ZamieńZKolejkowejNaŚcieżkową(osobnik).CopyTo(zmutowany_osobnik, 0);
            int miasto1 = LosowaKlasa.Los.Next(liczba_miast);
            int miasto2 = LosowaKlasa.Los.Next(liczba_miast);

            int miasto_tymczasowe = zmutowany_osobnik[miasto1];
            zmutowany_osobnik[miasto1] = zmutowany_osobnik[miasto2];
            zmutowany_osobnik[miasto2] = miasto_tymczasowe;

            return Populacja.ZamieńZeŚcieżkowejNaKolejkową(zmutowany_osobnik);
        }
    }
}
