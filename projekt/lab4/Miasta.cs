using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4
{
    static class Miasta
    {
        static Współrzędne[] lista_miast;
        static int liczba_miast;

        internal static Współrzędne[] Lista_miast
        {
            get
            {
                return lista_miast;
            }

            set
            {
                lista_miast = value;
                liczba_miast = lista_miast.Length;
            }
        }

        public static int Liczba_miast
        {
            get
            {
                return liczba_miast;
            }
        }
    }
}
