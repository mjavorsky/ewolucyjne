using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4
{
    class ZachłannyWyniki
    {
        int liczba_baterii;
        int[] osobnik;

        public ZachłannyWyniki(int liczba_baterii, int[] osobnik)
        {
            this.liczba_baterii = liczba_baterii;
            this.osobnik = osobnik;
        }

        public int Liczba_baterii
        {
            get
            {
                return liczba_baterii;
            }
        }

        public int[] Osobnik
        {
            get
            {
                return osobnik;
            }
        }
    }
}
