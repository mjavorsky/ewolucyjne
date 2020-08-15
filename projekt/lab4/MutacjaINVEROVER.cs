using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4
{
    class MutacjaINVEROVER : Mutacja
    {
        public MutacjaINVEROVER(double prawdopodobieństwo_mutacji) : base(prawdopodobieństwo_mutacji) { }

        public override int[] Mutuj(int[] osobnik)
        {
            List<int> zmutowany_osobnik = new List<int>();
            int liczba_miast = Miasta.Liczba_miast;
            int punkt_przecięcia1 = LosowaKlasa.Los.Next(liczba_miast);
            int punkt_przecięcia2 = LosowaKlasa.Los.Next(liczba_miast);

            int długość_przedziału = Math.Abs(punkt_przecięcia2 - punkt_przecięcia1);

            if (punkt_przecięcia1 < punkt_przecięcia2)
            {
                zmutowany_osobnik.AddRange(osobnik);
                zmutowany_osobnik.Reverse(punkt_przecięcia1 + 1, długość_przedziału);
            }
            else
            {
                List<int> tymczasowy_osobnik = new List<int>();
                tymczasowy_osobnik.AddRange(osobnik);
                zmutowany_osobnik.AddRange(tymczasowy_osobnik.GetRange(punkt_przecięcia2 + 1, długość_przedziału));
                tymczasowy_osobnik.RemoveRange(punkt_przecięcia2 + 1, długość_przedziału);
                tymczasowy_osobnik.AddRange(tymczasowy_osobnik.GetRange(0, punkt_przecięcia2 + 1));
                tymczasowy_osobnik.RemoveRange(0, punkt_przecięcia2 + 1);
                tymczasowy_osobnik.Reverse();
                zmutowany_osobnik.AddRange(tymczasowy_osobnik);

                int nr_pierwszego_miasta = osobnik[0];
                int indeks = zmutowany_osobnik.IndexOf(nr_pierwszego_miasta);
                zmutowany_osobnik.AddRange(zmutowany_osobnik.GetRange(0, indeks));
                zmutowany_osobnik.RemoveRange(0, indeks);
            }

            return zmutowany_osobnik.ToArray();
        }

    }
}
