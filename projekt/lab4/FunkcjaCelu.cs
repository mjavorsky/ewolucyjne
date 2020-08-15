using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4
{
    class FunkcjaCelu
    {
        public double Oblicz(Współrzędne[] lista_współrzędnych, int liczba_baterii)
        {
            double czas = 0;
            double x1, x2, y1, y2;
            int z1, z2;
            int liczba_miast = Miasta.Liczba_miast;
            double aktualny_stan_baterii = liczba_baterii * 1000;

            double odległość_między_miastami = 0;
            double prędkość_zero = 0;
            double prędkość = 0;
            bool czy_można_ładować = false;

            for (int i = 0; i < liczba_miast - 1; i++)
            {
                x1 = lista_współrzędnych[i].X;
                y1 = lista_współrzędnych[i].Y;
                z1 = lista_współrzędnych[i].Trzecia_współrzędna;
                x2 = lista_współrzędnych[i + 1].X;
                y2 = lista_współrzędnych[i + 1].Y;
                z2 = lista_współrzędnych[i + 1].Trzecia_współrzędna;
                czy_można_ładować = lista_współrzędnych[i + 1].Czy_można_ładować_baterie;
                odległość_między_miastami = Math.Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1));

                aktualny_stan_baterii -= odległość_między_miastami;

                if (aktualny_stan_baterii < 0)
                    return -1;

                if (czy_można_ładować)
                    aktualny_stan_baterii = liczba_baterii * 1000;

                prędkość_zero = 10 - (z2 - z1);
                prędkość = prędkość_zero * (1 - 0.01 * liczba_baterii);

                czas += odległość_między_miastami / prędkość;
            }

            x1 = lista_współrzędnych[liczba_miast - 1].X;
            y1 = lista_współrzędnych[liczba_miast - 1].Y;
            z1 = lista_współrzędnych[liczba_miast - 1].Trzecia_współrzędna;
            x2 = lista_współrzędnych[0].X;
            y2 = lista_współrzędnych[0].Y;
            z2 = lista_współrzędnych[0].Trzecia_współrzędna;
            czy_można_ładować = lista_współrzędnych[0].Czy_można_ładować_baterie;
            odległość_między_miastami = Math.Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1));

            aktualny_stan_baterii -= odległość_między_miastami;

            if (aktualny_stan_baterii < 0)
                return -1;

            if (czy_można_ładować)
                aktualny_stan_baterii = liczba_baterii * 1000;

            prędkość_zero = 10 - (z2 - z1);
            prędkość = prędkość_zero * (1 - 0.01 * liczba_baterii);

            czas += odległość_między_miastami / prędkość;

            return czas;
        }
    }
}
