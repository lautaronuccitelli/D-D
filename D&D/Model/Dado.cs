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

        public static int Tirar(int caras = 20)
        {
            return random.Next(1, caras + 1);
        }
    }

}
