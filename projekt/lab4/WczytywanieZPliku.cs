using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace lab4
{
    static class WczytywanieZPliku
    {
        public static Współrzędne[] Wczytaj(string ścieżka)
        {
            List<Współrzędne> lista_miast = new List<Współrzędne>();

            StreamReader czytany_strumień = new StreamReader(ścieżka);

            for (int i = 0; i < 7; i++)
                czytany_strumień.ReadLine();

            while (!czytany_strumień.EndOfStream)
            {
                string wiersz = czytany_strumień.ReadLine();
                string[] podzielony_wiersz = wiersz.Split(' ');
                podzielony_wiersz[1] = podzielony_wiersz[1].Replace('.', ',');
                podzielony_wiersz[2] = podzielony_wiersz[2].Replace('.', ',');
                Współrzędne wsp = new Współrzędne(Convert.ToInt32(podzielony_wiersz[0]), Convert.ToDouble(podzielony_wiersz[1]), Convert.ToDouble(podzielony_wiersz[2]));
                lista_miast.Add(wsp);
            }

            return lista_miast.ToArray();
        }
    }
}
