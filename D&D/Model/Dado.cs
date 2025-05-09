using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public static class Dado
    {
        private static Random random = new Random();

        public static int Tirar(int caras = 20, int modificador = 0)
        {
            int resultado = random.Next(1, caras + 1) + modificador;
            return resultado;
        }

    }
}
