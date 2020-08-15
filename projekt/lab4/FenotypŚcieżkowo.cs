using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4
{
    class FenotypŚcieżkowo : Fenotyp
    {
        public override Współrzędne[] Oblicz(int[] osobnik)
        {
            Współrzędne[] lista = new Współrzędne[Miasta.Liczba_miast];


            for (int i = 0; i < Miasta.Liczba_miast; i++)
                lista[i] = Miasta.Lista_miast[osobnik[i] - 1];

            return lista;
        }
    }
}
