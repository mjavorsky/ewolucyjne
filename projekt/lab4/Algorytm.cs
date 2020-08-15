using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4
{
    class Algorytm
    {
        Osobnik[] populacja;
        Selekcja selekcja;
        Rekombinacja rekombinacja;
        Mutacja mutacja;
        Fenotyp fenotyp;
        FunkcjaCelu funkcja_celu;
        static int liczba_osobników;
        WarunekStopu warunek_stopu;
        int parametr_stopu;

        public enum WarunekStopu
        {
            LICZBA_POKOLEŃ,
            CZAS,
            POPRAWA_W_OSTATNICH_K_POKOLEŃ,
            RÓŻNORODNOŚC_POPULACJI
        }

        int numer_pokolenia = 0;

        Osobnik[] nowa_populacja;

        Dictionary<int, Osobnik> niebo = new Dictionary<int, Osobnik>();
        Osobnik kandydat_do_nieba;

        bool czy_do_nieba;

        public Osobnik[] Populacja
        {
            get
            {
                return populacja;
            }

            set
            {
                populacja = value;
            }
        }

        public static int Liczba_osobników
        {
            get
            {
                return liczba_osobników;
            }
        }

        public Algorytm(Osobnik[] populacja_startowa, Selekcja selekcja, Rekombinacja rekombinacja, Mutacja mutacja, WarunekStopu warunek_stopu, int parametr_stopu, Fenotyp fenotyp, FunkcjaCelu funkcja_celu)
        {
            populacja = populacja_startowa;
            this.selekcja = selekcja;
            this.rekombinacja = rekombinacja;
            this.mutacja = mutacja;
            this.fenotyp = fenotyp;
            this.funkcja_celu = funkcja_celu;
            liczba_osobników = populacja_startowa.Length;
            this.warunek_stopu = warunek_stopu;
            this.parametr_stopu = parametr_stopu;
            nowa_populacja = new Osobnik[Liczba_osobników];
        }      

        public Dictionary<int, Osobnik> Oblicz()
        {
            numer_pokolenia = 0;

            niebo.Clear();

            niebo.Add(numer_pokolenia, Najlepszy(populacja));

            if (warunek_stopu == WarunekStopu.LICZBA_POKOLEŃ)
                while (numer_pokolenia < parametr_stopu)
                    LiczPokolenie();
            else if (warunek_stopu == WarunekStopu.CZAS)
            {
                Stopwatch stoper = new Stopwatch();
                stoper.Start();
                while (stoper.Elapsed < TimeSpan.FromSeconds(parametr_stopu))
                    LiczPokolenie();
                stoper.Stop();
            }
            else if (warunek_stopu == WarunekStopu.POPRAWA_W_OSTATNICH_K_POKOLEŃ)
            {
                bool stop = false;
                int licznik = 0;

                while (!stop)
                {
                    int liczba_w_niebie_przed = niebo.Count;
                    LiczPokolenie();
                    int liczba_w_niebie_po = niebo.Count;
                    if (liczba_w_niebie_przed == liczba_w_niebie_po)
                        licznik++;
                    else
                        licznik = 0;
                    if (licznik == parametr_stopu)
                        stop = true;
                }
            }
            else
            {
                bool stop = false;
                while (!stop)
                    LiczPokolenie();
                stop = true;
                for (int i = 1; i < populacja.Length; i++)
                {
                    if (populacja[0] != populacja[i])
                    {
                        stop = false;
                        break;
                    }
                }
            }

            return niebo;
        }

        private void LiczPokolenie()
        {

            Parallel.For(0, liczba_osobników, indeks =>
            {
                Osobnik mama = selekcja.DokonajSelekcji(populacja, fenotyp, funkcja_celu);
                Osobnik tata = selekcja.DokonajSelekcji(populacja, fenotyp, funkcja_celu);

                Osobnik dziecko = rekombinacja.DokonajRekombinacji(mama, tata, mutacja, fenotyp, funkcja_celu);
                while (dziecko.Liczba_baterii > 99)
                {
                    mama = selekcja.DokonajSelekcji(populacja, fenotyp, funkcja_celu);
                    tata = selekcja.DokonajSelekcji(populacja, fenotyp, funkcja_celu);
                    dziecko = rekombinacja.DokonajRekombinacji(mama, tata, mutacja, fenotyp, funkcja_celu);
                }

                nowa_populacja[indeks] = dziecko;
            });



            nowa_populacja.CopyTo(populacja, 0);
            
            ++numer_pokolenia;

            czy_do_nieba = true;
            kandydat_do_nieba = Najlepszy(populacja);


            if (kandydat_do_nieba.Wartość >= niebo.Last().Value.Wartość)
                czy_do_nieba = false;
            if (czy_do_nieba)
            {
                niebo.Clear();
                niebo.Add(numer_pokolenia, kandydat_do_nieba);
            }

        }

        

        private Osobnik Najgorszy(Osobnik[] populacja)
        {
            int najgorszy = 0;

            for (int i = 1; i < populacja.Length; i++)
                if (populacja[i].Wartość > populacja[najgorszy].Wartość)
                    najgorszy = i;

            return populacja[najgorszy];
        }

        private Osobnik Najlepszy(Osobnik[] populacja)
        {
            int najlepszy = 0;

            for (int i = 1; i < populacja.Length; i++)
                if (populacja[i].Wartość < populacja[najlepszy].Wartość)
                    najlepszy = i;
                
            return populacja[najlepszy];
        }
    }
}
