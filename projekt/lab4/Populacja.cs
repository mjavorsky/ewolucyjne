using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4
{
    static class Populacja 
    {
        public static Osobnik[] GenerujKolejkowoLosowo(int liczbaWierzchołków, int liczbaOsobników)
        {
            Osobnik[] populacja = new Osobnik[liczbaOsobników];
            
            FenotypKolejkowo fenotyp = new FenotypKolejkowo();
            FunkcjaCelu funkcja_celu = new FunkcjaCelu();


            Parallel.For(0, liczbaOsobników, indeks =>
            {
                int[] tymczasowa_tablica = new int[liczbaWierzchołków];

                Random los = new Random();

                for (int j = 0; j < liczbaWierzchołków; j++)
                    tymczasowa_tablica[j] = los.Next(1, liczbaWierzchołków + 1 - j);
                Osobnik wygenerowany_osobnik = new Osobnik(tymczasowa_tablica, fenotyp, funkcja_celu);

                while (wygenerowany_osobnik.Liczba_baterii > 99)
                {
                    for (int j = 0; j < liczbaWierzchołków; j++)
                        tymczasowa_tablica[j] = los.Next(1, liczbaWierzchołków + 1 - j);
                    wygenerowany_osobnik = new Osobnik(tymczasowa_tablica, fenotyp, funkcja_celu);
                }



                populacja[indeks] = wygenerowany_osobnik;
            });

            Console.WriteLine("wygenerowano");
            return populacja;
        }

        

        public static Osobnik[] GenerujŚcieżkowoLosowo(int liczbaWierzchołków, int liczbaOsobników)
        {
            Osobnik[] populacja = new Osobnik[liczbaOsobników];

            
            FenotypŚcieżkowo fenotyp = new FenotypŚcieżkowo();
            FunkcjaCelu funkcja_celu = new FunkcjaCelu();

            Random los = new Random();

            Parallel.For(0, liczbaOsobników, indeks =>
            {
                int[] tymczasowa_tablica = Enumerable.Range(1, liczbaWierzchołków).ToArray();
                int[] tymczasowa_tablica2 = tymczasowa_tablica.OrderBy(x => los.Next()).ToArray();
                Osobnik wygenerowany_osobnik = new Osobnik(tymczasowa_tablica2, fenotyp, funkcja_celu);

                while (wygenerowany_osobnik.Liczba_baterii > 99)
                {

                    for (int j = 0; j < liczbaWierzchołków; j++)
                        tymczasowa_tablica[j] = j + 1;
                    tymczasowa_tablica2 = tymczasowa_tablica.OrderBy(x => los.Next()).ToArray();
                    wygenerowany_osobnik = new Osobnik(tymczasowa_tablica2, fenotyp, funkcja_celu);

                }

                populacja[indeks] = wygenerowany_osobnik;
            });

            Console.WriteLine("wygenerowano");
            return populacja;
        }

        public static Osobnik[] GenerujŚcieżkowoZachłannie(int liczbaWierzchołków, int liczbaOsobników)
        {
            Osobnik[] populacja = new Osobnik[liczbaOsobników];
            
            FenotypŚcieżkowo fenotyp = new FenotypŚcieżkowo();
            FunkcjaCelu funkcja_celu = new FunkcjaCelu();

            Parallel.For(0, liczbaOsobników, indeks =>
            {
                int[] tymczasowa_tablica = new int[liczbaWierzchołków];
                tymczasowa_tablica = Zachłanny.WygenerujOsobnika();

                Osobnik wygenerowany_osobnik = new Osobnik(tymczasowa_tablica, fenotyp, funkcja_celu);

                while (wygenerowany_osobnik.Liczba_baterii > 99)
                {
                    tymczasowa_tablica = Zachłanny.WygenerujOsobnika();
                    wygenerowany_osobnik = new Osobnik(tymczasowa_tablica, fenotyp, funkcja_celu);
                }

                populacja[indeks] = wygenerowany_osobnik;
            });

            Console.WriteLine("wygenerowano");
            return populacja;
        }

        public static Osobnik[] GenerujKolejkowoZachłannie(int liczbaWierzchołków, int liczbaOsobników)
        {
            Osobnik[] populacja = new Osobnik[liczbaOsobników];

            FenotypKolejkowo fenotyp = new FenotypKolejkowo();
            FunkcjaCelu funkcja_celu = new FunkcjaCelu();


            Parallel.For(0, liczbaOsobników, indeks =>
            {
                int[] tymczasowa_tablica = new int[liczbaWierzchołków];
                tymczasowa_tablica = ZamieńZeŚcieżkowejNaKolejkową(Zachłanny.WygenerujOsobnika());

                Osobnik wygenerowany_osobnik = new Osobnik(tymczasowa_tablica, fenotyp, funkcja_celu);

                while (wygenerowany_osobnik.Liczba_baterii > 99)
                {
                    tymczasowa_tablica = ZamieńZeŚcieżkowejNaKolejkową(Zachłanny.WygenerujOsobnika());
                    wygenerowany_osobnik = new Osobnik(tymczasowa_tablica, fenotyp, funkcja_celu);
                }

                populacja[indeks] = wygenerowany_osobnik;
            });

            Console.WriteLine("wygenerowano");
            return populacja;
        }

        public static Osobnik[] GenerujŚcieżkowo12345(int liczbaWierzchołków, int liczbaOsobników)
        {
            Osobnik[] populacja = new Osobnik[liczbaOsobników];


            FenotypŚcieżkowo fenotyp = new FenotypŚcieżkowo();
            FunkcjaCelu funkcja_celu = new FunkcjaCelu();

            Parallel.For(0, liczbaOsobników, indeks =>
            {
                int[] tymczasowa_tablica = Enumerable.Range(1, liczbaWierzchołków).ToArray();

                Osobnik wygenerowany_osobnik = new Osobnik(tymczasowa_tablica, fenotyp, funkcja_celu);

                populacja[indeks] = wygenerowany_osobnik;
            });

            Console.WriteLine("wygenerowano");
            return populacja;
        }

        public static Osobnik[] GenerujKolejkowo12345(int liczbaWierzchołków, int liczbaOsobników)
        {
            Osobnik[] populacja = new Osobnik[liczbaOsobników];


            FenotypŚcieżkowo fenotyp = new FenotypŚcieżkowo();
            FunkcjaCelu funkcja_celu = new FunkcjaCelu();

            Parallel.For(0, liczbaOsobników, indeks =>
            {
                int[] tymczasowa_tablica = new int[liczbaWierzchołków];

                Osobnik wygenerowany_osobnik = new Osobnik(tymczasowa_tablica, fenotyp, funkcja_celu);

                populacja[indeks] = wygenerowany_osobnik;
            });

            Console.WriteLine("wygenerowano");
            return populacja;
        }



        public static int[] ZamieńZeŚcieżkowejNaKolejkową(int[] osobnik)
        {
            int liczba_miast = Miasta.Liczba_miast;
            int[] nowy_osobnik = new int[liczba_miast];
            List<int> lista_miast = new List<int>();
            for (int i = 1; i < liczba_miast + 1; i++)
                lista_miast.Add(i);

            int indeks;
            for (int i = 0; i < liczba_miast; i++)
            {
                indeks = lista_miast.IndexOf(osobnik[i]);
                lista_miast.RemoveAt(indeks);
                nowy_osobnik[i] = indeks + 1;
            }

            return nowy_osobnik;
        }


        public static int[] ZamieńZKolejkowejNaŚcieżkową(int[] osobnik)
        {
            int liczba_miast = Miasta.Liczba_miast;
            int[] nowy_osobnik = new int[liczba_miast];
            List<int> lista_miast = new List<int>();
            for (int i = 1; i < liczba_miast + 1; i++)
                lista_miast.Add(i);

            int indeks;
            for (int i = 0; i < liczba_miast; i++)
            {
                indeks = osobnik[i] - 1;
                nowy_osobnik[i] = lista_miast[indeks];
                lista_miast.RemoveAt(indeks);
            }

            return nowy_osobnik;
        }

    }
}
