using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using View;

namespace D_D.Controller
{
    public class JuegoController
    {
        private List<Personaje> personajes;
        private ConsolaView vista;

        public JuegoController()
        {
            vista = new ConsolaView();
            personajes = new List<Personaje>
        {
            new Personaje("Guerrero", 8, 5, 3),
            new Personaje("Ladrón", 4, 8, 4),
            new Personaje("Mago", 3, 4, 9)
        };
        }

        public void IniciarJuego()
        {
            var personaje = vista.ElegirPersonaje(personajes);
            vista.MostrarMensaje($"Has elegido a: {personaje.Nombre}");

            string escena = GenerarEscenaAleatoria();
            vista.MostrarMensaje(escena);

            string accion = vista.ElegirAccion();
            if (accion == "atacar")
            {
                int resultado = Dado.Tirar();
                if (resultado >= 10)
                    vista.MostrarMensaje("¡Ataque exitoso!");
                else
                    vista.MostrarMensaje("¡Fallo el ataque!");
            }
        }

        private string GenerarEscenaAleatoria()
        {
            string[] escenas = {
            "Un goblin salta desde los arbustos.",
            "Una trampa se activa al pisar una baldosa.",
            "Un mercader sospechoso te ofrece una poción."
        };
            return escenas[new Random().Next(escenas.Length)];
        }
    }

}
