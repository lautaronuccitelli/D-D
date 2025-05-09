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
        public int PuntosVida { get; set; }
        public int ClaseArmadura { get; set; }
        public string HabilidadEspecial { get; set; }

        public Personaje(string nombre, int fuerza, int destreza, int inteligencia, int puntosVida, int claseArmadura, string habilidadEspecial)
        {
            Nombre = nombre;
            Fuerza = fuerza;
            Destreza = destreza;
            Inteligencia = inteligencia;
            PuntosVida = puntosVida;
            ClaseArmadura = claseArmadura;
            HabilidadEspecial = habilidadEspecial;
        }

        public int ObtenerModificador(string atributo)
        {
            int valor = atributo switch
            {
                "Fuerza" => Fuerza,
                "Destreza" => Destreza,
                "Inteligencia" => Inteligencia,
                _ => 0
            };
            return (valor - 10) / 2; // Fórmula estándar de D&D para modificadores
        }
    }
}
