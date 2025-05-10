using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Escena
    {
        public string Descripcion { get; set; }
        public string Tipo { get; set; }
        public int Dificultad { get; set; }
        public string Enemigo { get; set; }
        public int VidaEnemigo { get; set; }
        public int ClaseArmaduraEnemigo { get; set; }

        public Escena(string descripcion, string tipo, int dificultad, string enemigo = "", int vidaEnemigo = 0, int claseArmaduraEnemigo = 10)
        {
            Descripcion = descripcion;
            Tipo = tipo;
            Dificultad = dificultad;
            Enemigo = enemigo;
            VidaEnemigo = vidaEnemigo;
            ClaseArmaduraEnemigo = claseArmaduraEnemigo;
        }
    }
}
