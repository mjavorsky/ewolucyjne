using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4
{
    class Osobnik
    {
        int liczba_baterii;
        int[] genotyp;
        Współrzędne[] fenotyp;
        double wartość;

        public Osobnik(int[] genotyp, Fenotyp fenotyp, FunkcjaCelu funkcja_celu)
        {
            this.genotyp = genotyp;
            this.fenotyp = fenotyp.Oblicz(genotyp);
            Oblicz(funkcja_celu);
        }

        public int[] Genotyp
        {
            get
            {
                return genotyp;
            }
        }

        internal Współrzędne[] Fenotyp
        {
            get
            {
                return fenotyp;
            }
        }

        public double Wartość
        {
            get
            {
                return wartość;
            }
        }

        public int Liczba_baterii
        {
            get
            {
                return liczba_baterii;
            }
        }

        private void Oblicz(FunkcjaCelu funkcja)
        {
            liczba_baterii = 0;

            do
            {
                liczba_baterii++;
                wartość = funkcja.Oblicz(fenotyp, liczba_baterii);
            } while (wartość == -1);
        }
    }
}
