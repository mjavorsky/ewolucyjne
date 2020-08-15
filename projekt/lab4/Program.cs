using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using System.Diagnostics;

namespace lab4
{
    class Program
    {
        static void Wykonaj(string ścieżka_startowa)
        {
            string ścieżka = ścieżka_startowa; // domyślnie "parametry.txt"
            StreamReader czytany_strumień = new StreamReader(ścieżka);

            // lista miast
            string ścieżka_do_miast = czytany_strumień.ReadLine();

            // wielkość populacji
            int wielkość_populacji = Convert.ToInt32(czytany_strumień.ReadLine());

            // rodzaj algorytmu
            int nr_algorytmu = Convert.ToInt32(czytany_strumień.ReadLine());

            // populacja startowa
            int nr_generowania_populacji_startowej = Convert.ToInt32(czytany_strumień.ReadLine());

            // selekcja
            int nr_selekcji = Convert.ToInt32(czytany_strumień.ReadLine());
            Selekcja selekcja;
            if (nr_selekcji == 0)
                selekcja = new SelekcjaTurniejowa();
            else
            {
                selekcja = new SelekcjaRuletkaRankingowa();
                double[] prawdopodobieństwa = new double[wielkość_populacji];
                int suma = 0;
                for (int i = 0; i < prawdopodobieństwa.Length; i++)
                {
                    prawdopodobieństwa[i] = i + 1;
                    suma += i + 1;
                }
                for (int i = 0; i < prawdopodobieństwa.Length; i++)
                    prawdopodobieństwa[i] = prawdopodobieństwa[i] / suma;
                selekcja.Prawdopodobieństwa = prawdopodobieństwa;
            }

            // rekombinacja, fenotyp
            int nr_rekombinacji = Convert.ToInt32(czytany_strumień.ReadLine());
            Rekombinacja rekombinacja;
            Fenotyp fenotyp;
            Miasta.Lista_miast = WczytywanieZPliku.Wczytaj(ścieżka_do_miast);
            if (nr_rekombinacji == 0)
            {
                rekombinacja = new RekombinacjaPMX();
                fenotyp = new FenotypŚcieżkowo();
            }
            else if (nr_rekombinacji == 1)
            {
                rekombinacja = new RekombinacjaCięcieIŁączenie();
                fenotyp = new FenotypKolejkowo();
            }
            else if (nr_rekombinacji == 2)
            {
                rekombinacja = new RekombinacjaCX();
                fenotyp = new FenotypŚcieżkowo();
            }
            else
            {
                rekombinacja = new RekombinacjaOX();
                fenotyp = new FenotypŚcieżkowo();
            }


            // prawdopodobieństwo mutacji w %
            double prawdopodobieństwo_mutacji = Convert.ToDouble(czytany_strumień.ReadLine()) / 100;

            // mutacja
            int nr_mutacji = Convert.ToInt32(czytany_strumień.ReadLine());
            Mutacja mutacja;
            if (nr_rekombinacji == 1)
                mutacja = new MutacjaKolejkowa(prawdopodobieństwo_mutacji);
            else
            {
                if (nr_mutacji == 0)
                    mutacja = new MutacjaPrzezZamianęMiast(prawdopodobieństwo_mutacji);
                else
                    mutacja = new MutacjaINVEROVER(prawdopodobieństwo_mutacji);
            }
                

            // warunek stopu
            string wiersz = czytany_strumień.ReadLine();
            string[] podzielony_wiersz = wiersz.Split(' ');
            Algorytm.WarunekStopu warunek_stopu = (Algorytm.WarunekStopu)Convert.ToInt32(podzielony_wiersz[0]);
            int parametr_stopu = Convert.ToInt32(podzielony_wiersz[1]);

            // liczba powtórzeń algorytmu
            int liczba_powtórzeń = Convert.ToInt32(czytany_strumień.ReadLine());

            // ścieżka, gdzie zapisaywane są wyniki
            string ścieżka_do_wyników = czytany_strumień.ReadLine();

            czytany_strumień.Close();

            FunkcjaCelu funkcja_celu = new FunkcjaCelu();

            Osobnik[] wyniki = new Osobnik[liczba_powtórzeń];
            int[] numery_pokoleń = new int[liczba_powtórzeń];
            double[] czasy_wykonań = new double[liczba_powtórzeń];


            Stopwatch stoper = new Stopwatch();
            stoper.Start();

            if (nr_algorytmu == 0)
            {
                Parallel.For(0, liczba_powtórzeń, indeks =>
                {
                    Stopwatch stoper_indywidualny = new Stopwatch();
                    stoper_indywidualny.Start();

                    Osobnik[] populacja = new Osobnik[wielkość_populacji];
                    if (nr_rekombinacji == 1)
                    {
                        if (nr_generowania_populacji_startowej == 0)
                            populacja = Populacja.GenerujKolejkowoLosowo(Miasta.Liczba_miast, wielkość_populacji);
                        else if (nr_generowania_populacji_startowej == 1)
                            populacja = Populacja.GenerujKolejkowoZachłannie(Miasta.Liczba_miast, wielkość_populacji);
                        else
                            populacja = Populacja.GenerujKolejkowo12345(Miasta.Liczba_miast, wielkość_populacji);
                    }
                    else
                    {
                        if (nr_generowania_populacji_startowej == 0)
                            populacja = Populacja.GenerujŚcieżkowoLosowo(Miasta.Liczba_miast, wielkość_populacji);
                        else if (nr_generowania_populacji_startowej == 1)
                            populacja = Populacja.GenerujŚcieżkowoZachłannie(Miasta.Liczba_miast, wielkość_populacji);
                        else
                            populacja = Populacja.GenerujŚcieżkowo12345(Miasta.Liczba_miast, wielkość_populacji);
                    }
                    Algorytm algorytm = new Algorytm(populacja, selekcja, rekombinacja, mutacja, warunek_stopu, parametr_stopu, fenotyp, funkcja_celu);

                    Dictionary<int, Osobnik> słownik = algorytm.Oblicz();

                    wyniki[indeks] = słownik.Last().Value;
                    numery_pokoleń[indeks] = słownik.Last().Key;
                    

                    stoper_indywidualny.Stop();
                    czasy_wykonań[indeks] = stoper.ElapsedMilliseconds;

                    Console.WriteLine("Powtórzenie " + indeks + " : " + wyniki[indeks].Wartość);
                    Console.WriteLine("Numer pokolenia: " + numery_pokoleń[indeks]);
                    Console.WriteLine("Czas wykonania: " + czasy_wykonań[indeks] + " ms");
                    Console.WriteLine();
                });
            }
            else if (nr_algorytmu == 1)
            {
                Fenotyp fenotyp2 = new FenotypŚcieżkowo();

                Parallel.For(0, liczba_powtórzeń, indeks =>
                {
                    int[] tymczasowa_tablica = Zachłanny.WygenerujOsobnika();
                    Osobnik osobnik = new Osobnik(tymczasowa_tablica, fenotyp2, funkcja_celu);

                    while (osobnik.Liczba_baterii > 99)
                    {
                        tymczasowa_tablica = Zachłanny.WygenerujOsobnika();
                        osobnik = new Osobnik(tymczasowa_tablica, fenotyp2, funkcja_celu);
                    }

                    wyniki[indeks] = osobnik;
                    Console.WriteLine("Powtórzenie " + indeks + " : " + wyniki[indeks].Wartość);
                });
            }
            else
            {
                
            }


            stoper.Stop();
            double czas_programu = stoper.ElapsedMilliseconds;


            StreamWriter zapisywany_strumień = new StreamWriter(ścieżka_do_wyników);

            zapisywany_strumień.WriteLine(ścieżka_do_miast);

            zapisywany_strumień.WriteLine("Wielkość populacji: " + wielkość_populacji);

            if (nr_algorytmu == 0)
            {
                zapisywany_strumień.WriteLine("Algorytm: ewolucyjny");
                if (nr_generowania_populacji_startowej == 0)
                    zapisywany_strumień.WriteLine("Generowanie populacji: losowe");
                else if (nr_generowania_populacji_startowej == 1)
                    zapisywany_strumień.WriteLine("Generowanie populacji: zachłanne");
                else
                    zapisywany_strumień.WriteLine("Generowanie populacji: 1 2 3 4 5...");

                if (nr_selekcji == 0)
                    zapisywany_strumień.WriteLine("turniej");
                else
                    zapisywany_strumień.WriteLine("ranking");

                if (nr_rekombinacji == 0)
                {
                    zapisywany_strumień.WriteLine("PMX");
                    zapisywany_strumień.WriteLine("Prawdopodobieństwo mutacji: " + prawdopodobieństwo_mutacji * 100 + "%");
                    if (nr_mutacji == 0)
                        zapisywany_strumień.WriteLine("Mutacja: zamiana dwóch losowych miast");
                    else
                        zapisywany_strumień.WriteLine("Mutacja: INVER-OVER");
                }
                else if (nr_rekombinacji == 1)
                {
                    zapisywany_strumień.WriteLine("cięcie i łączenie");
                    zapisywany_strumień.WriteLine("Prawdopodobieństwo mutacji: " + prawdopodobieństwo_mutacji * 100 + "%");
                    zapisywany_strumień.WriteLine("Mutacja: zmiana jednej współrzędnej w reprezentacji kolejkowej");
                }
                else if (nr_rekombinacji == 2)
                {
                    zapisywany_strumień.WriteLine("CX");
                    zapisywany_strumień.WriteLine("Prawdopodobieństwo mutacji: " + prawdopodobieństwo_mutacji * 100 + "%");
                    if (nr_mutacji == 0)
                        zapisywany_strumień.WriteLine("Mutacja: zamiana dwóch losowych miast");
                    else
                        zapisywany_strumień.WriteLine("Mutacja: INVER-OVER");
                }
                else
                {
                    zapisywany_strumień.WriteLine("OX");
                    zapisywany_strumień.WriteLine("Prawdopodobieństwo mutacji: " + prawdopodobieństwo_mutacji * 100 + "%");
                    if (nr_mutacji == 0)
                        zapisywany_strumień.WriteLine("Mutacja: zamiana dwóch losowych miast");
                    else
                        zapisywany_strumień.WriteLine("Mutacja: INVER-OVER");
                }


                if (warunek_stopu == 0)
                    zapisywany_strumień.WriteLine("Warunek stopu: liczba pokoleń = " + parametr_stopu);
                else if ((int)warunek_stopu == 1)
                    zapisywany_strumień.WriteLine("Warunek stopu: czas = " + parametr_stopu + " sek");
                else if ((int)warunek_stopu == 2)
                    zapisywany_strumień.WriteLine("Warunek stopu: poprawa w ostatnich k pokoleń = " + parametr_stopu);
                else
                    zapisywany_strumień.WriteLine("Warunek stopu: różnorodność populacji");
            }
            else if (nr_algorytmu == 1)
            {
                zapisywany_strumień.WriteLine("Algorytm: zachłanny");
            }   
            else
                zapisywany_strumień.WriteLine("Algorytm: Sprawdzenie liczby baterii, dla jakich generowane są ścieżki w algorytmie zachłannym");

            zapisywany_strumień.WriteLine("Liczba powtórzeń: " + liczba_powtórzeń);

            zapisywany_strumień.WriteLine("Czas wykonania: " + czas_programu + " ms");

            zapisywany_strumień.WriteLine();

            zapisywany_strumień.WriteLine("Wyniki:");
            foreach (Osobnik osobnik in wyniki)
                zapisywany_strumień.WriteLine(osobnik.Wartość);
            zapisywany_strumień.WriteLine();


            zapisywany_strumień.WriteLine("Liczby baterii:");
            foreach (Osobnik osobnik in wyniki)
                zapisywany_strumień.WriteLine(osobnik.Liczba_baterii);
            zapisywany_strumień.WriteLine();


            zapisywany_strumień.WriteLine("Numery pokoleń:");
            foreach (int nr_pokolenia in numery_pokoleń)
                zapisywany_strumień.WriteLine(nr_pokolenia);
            zapisywany_strumień.WriteLine();

            zapisywany_strumień.WriteLine("Czasy obliczeń poszczególnych powtórzeń [ms]:");
            foreach (double czas in czasy_wykonań)
                zapisywany_strumień.WriteLine(czas);
            zapisywany_strumień.WriteLine();

            zapisywany_strumień.WriteLine("Numery miast:");
            foreach (Osobnik osobnik in wyniki)
            {
                foreach (Współrzędne miasto in osobnik.Fenotyp)
                    zapisywany_strumień.Write(miasto.Nr_miasta + " ");
                zapisywany_strumień.WriteLine();
                zapisywany_strumień.WriteLine();
            }
            
            zapisywany_strumień.Close();

            Console.WriteLine();
            Console.WriteLine("Gotowe! Wyniki zapisano w '" + ścieżka_do_wyników + "'.");
        }

        static void Main(string[] args)
        {

            Wykonaj(args[0]);

            
        }
    }
}
