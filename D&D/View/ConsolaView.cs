using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace View
{
    public class ConsolaView
    {
        public Personaje ElegirPersonaje(List<Personaje> personajes)
        {
            Console.WriteLine("Elige un personaje:");
            for (int i = 0; i < personajes.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {personajes[i].Nombre}");
            }

            int eleccion = int.Parse(Console.ReadLine() ?? "1") - 1;
            return personajes[eleccion];
        }

        public string ElegirAccion()
        {
            Console.WriteLine("¿Qué quieres hacer? (atacar / huir / hablar)");
            return Console.ReadLine()?.ToLower() ?? "atacar";
        }

        public void MostrarMensaje(string mensaje)
        {
            Console.WriteLine(mensaje);
        }
    }

}
