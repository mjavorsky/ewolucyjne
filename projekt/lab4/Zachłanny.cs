using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4
{
    static class Zachłanny
    {
        public static ZachłannyWyniki Oblicz()
        {
            int liczba_miast = Miasta.Liczba_miast;
            List<Współrzędne> lista_współrzędnych = new List<Współrzędne>();
            lista_współrzędnych.AddRange(Miasta.Lista_miast);
            
            int[] osobnik = new int[liczba_miast];
            int indeks_miasta = LosowaKlasa.Los.Next(liczba_miast);
            osobnik[0] = lista_współrzędnych[indeks_miasta].Nr_miasta;
            lista_współrzędnych.RemoveAt(indeks_miasta);

            for (int i = 1; i < liczba_miast; i++)
            {
                indeks_miasta = ZnajdźNajbliższeMiasto(lista_współrzędnych, indeks_miasta);
                osobnik[i] = lista_współrzędnych[indeks_miasta].Nr_miasta;
                lista_współrzędnych.RemoveAt(indeks_miasta);
            }
            FenotypŚcieżkowo fenotyp = new FenotypŚcieżkowo();
            FunkcjaCelu funkcja = new FunkcjaCelu();
            int liczba_baterii = 0;
            double wartość_funkcji;
            do
            {
                liczba_baterii++;
                wartość_funkcji = funkcja.Oblicz(fenotyp.Oblicz(osobnik.ToArray()), liczba_baterii);

            } while (wartość_funkcji == -1);

            ZachłannyWyniki wynik = new ZachłannyWyniki(liczba_baterii, osobnik);

            Console.WriteLine("Liczba baterii: " + liczba_baterii);
            Console.WriteLine("Czas " + wartość_funkcji);

            return wynik;
        }

        public static int[] WygenerujOsobnika()
        {
            
            FenotypŚcieżkowo fenotyp = new FenotypŚcieżkowo();
            FunkcjaCelu funkcja = new FunkcjaCelu();
            int[] osobnik = new int[Miasta.Liczba_miast];

            osobnik = StwórzOsobnika();



            return osobnik;
        }

        public static void ZnajdźNajbliższeMiastoPoCzasie(List<Współrzędne> lista_współrzędnych, int aktualny_numer_miasta)
        {

        }

        private static int[] StwórzOsobnika()
        {
            List<Współrzędne> lista_współrzędnych = new List<Współrzędne>();
            lista_współrzędnych.AddRange(Miasta.Lista_miast);
            int liczba_miast = lista_współrzędnych.Count();
            int[] osobnik = new int[Miasta.Liczba_miast];
            int indeks_miasta = LosowaKlasa.Los.Next(liczba_miast);
            osobnik[0] = lista_współrzędnych[indeks_miasta].Nr_miasta;
            lista_współrzędnych.RemoveAt(indeks_miasta);

            for (int i = 1; i < liczba_miast; i++)
            {
                indeks_miasta = ZnajdźNajbliższeMiasto(lista_współrzędnych, indeks_miasta);
                osobnik[i] = lista_współrzędnych[indeks_miasta].Nr_miasta;
                lista_współrzędnych.RemoveAt(indeks_miasta);
            }

            return osobnik;
        }

        private static int ZnajdźNajbliższeMiasto(List<Współrzędne> lista_współrzędnych, int aktualny_numer_miasta)
        {
            double x1 = Miasta.Lista_miast[aktualny_numer_miasta].X;
            double y1 = Miasta.Lista_miast[aktualny_numer_miasta].Y;
            int liczba_pozostałych_miast = lista_współrzędnych.Count();

            double x2 = lista_współrzędnych[0].X;
            double y2 = lista_współrzędnych[0].Y;

            double najkrótsza_odległość = Math.Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1));
            int indeks_najbliższego_miasta = 0;

            for (int i = 1; i < liczba_pozostałych_miast; i++)
            {
                x2 = lista_współrzędnych[i].X;
                y2 = lista_współrzędnych[i].Y;
                double odległość = Math.Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1));
                if (odległość < najkrótsza_odległość)
                {
                    najkrótsza_odległość = odległość;
                    indeks_najbliższego_miasta = i;
                }
            }

            return indeks_najbliższego_miasta;
        }
    }
}
