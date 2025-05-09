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
            Console.WriteLine("=== Elige tu personaje ===");
            for (int i = 0; i < personajes.Count; i++)
            {
                var p = personajes[i];
                Console.WriteLine($"{i + 1}. {p.Nombre} (Fuerza: {p.Fuerza}, Destreza: {p.Destreza}, Inteligencia: {p.Inteligencia}, PV: {p.PuntosVida}, Habilidad: {p.HabilidadEspecial})");
            }

            int eleccion;
            do
            {
                Console.Write("Selecciona un número: ");
            } while (!int.TryParse(Console.ReadLine(), out eleccion) || eleccion < 1 || eleccion > personajes.Count);

            return personajes[eleccion - 1];
        }

        public string ElegirAccion(Escena escena)
        {
            Console.WriteLine("\n¿Qué deseas hacer?");
            if (escena.Tipo == "Combate")
                Console.WriteLine("1. Atacar\n2. Usar habilidad especial\n3. Huir");
            else if (escena.Tipo == "Trampa")
                Console.WriteLine("1. Desactivar trampa\n2. Saltar sobre la trampa\n3. Ignorar");
            else if (escena.Tipo == "Social")
                Console.WriteLine("1. Hablar\n2. Intimidar\n3. Ignorar");

            string input = Console.ReadLine()?.ToLower();
            return input switch
            {
                "1" => escena.Tipo == "Combate" ? "atacar" : escena.Tipo == "Trampa" ? "desactivar" : "hablar",
                "2" => escena.Tipo == "Combate" ? "habilidad" : escena.Tipo == "Trampa" ? "saltar" : "intimidar",
                "3" => escena.Tipo == "Combate" ? "huir" : escena.Tipo == "Trampa" ? "ignorar" : "ignorar",
                _ => "ignorar"
            };
        }

        public void MostrarMensaje(string mensaje)
        {
            Console.WriteLine(mensaje);
        }

        public void MostrarEstadoPersonaje(Personaje personaje)
        {
            Console.WriteLine($"\nEstado de {personaje.Nombre}: PV: {personaje.PuntosVida}, CA: {personaje.ClaseArmadura}");
        }
    }
}
