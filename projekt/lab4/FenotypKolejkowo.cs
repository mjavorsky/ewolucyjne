using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4
{
    class FenotypKolejkowo : Fenotyp
    {
        public override Współrzędne[] Oblicz(int[] osobnik)
        {
            List<Współrzędne> lista_współrzędnych = new List<Współrzędne>();
            lista_współrzędnych.AddRange(Miasta.Lista_miast);
            Współrzędne[] lista = new Współrzędne[Miasta.Liczba_miast];

            for (int i = 0; i < Miasta.Liczba_miast; i++)
            {
                lista[i] = lista_współrzędnych[osobnik[i] - 1];
                lista_współrzędnych.RemoveAt(osobnik[i] - 1);
            }

            return lista;
        }
    }
}
