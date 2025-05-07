using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Personaje
    {
        public string Nombre { get; set; }
        public int Fuerza { get; set; }
        public int Destreza { get; set; }
        public int Inteligencia { get; set; }

        public Personaje(string nombre, int fuerza, int destreza, int inteligencia)
        {
            Nombre = nombre;
            Fuerza = fuerza;
            Destreza = destreza;
            Inteligencia = inteligencia;
        }
    }
}
