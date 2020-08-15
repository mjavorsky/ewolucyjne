using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4
{
    class Współrzędne
    {
        int nr_miasta;
        double x;
        double y;
        int trzecia_współrzędna;
        bool czy_można_ładować_baterie;

        public Współrzędne(int nr_miasta, double x, double y)
        {
            this.nr_miasta = nr_miasta;
            this.x = x;
            this.y = y;
            trzecia_współrzędna = nr_miasta % 7;
            if ((nr_miasta - 1) % 5 == 0)
                czy_można_ładować_baterie = true;
            else
                czy_można_ładować_baterie = false;
        }

        public double X
        {
            get
            {
                return x;
            }

            set
            {
                x = value;
            }
        }

        public double Y
        {
            get
            {
                return y;
            }

            set
            {
                y = value;
            }
        }

        public int Trzecia_współrzędna
        {
            get
            {
                return trzecia_współrzędna;
            }

            set
            {
                trzecia_współrzędna = value;
            }
        }

        public bool Czy_można_ładować_baterie
        {
            get
            {
                return czy_można_ładować_baterie;
            }

            set
            {
                czy_można_ładować_baterie = value;
            }
        }

        public int Nr_miasta
        {
            get
            {
                return nr_miasta;
            }

            set
            {
                nr_miasta = value;
            }
        }
    }
}
