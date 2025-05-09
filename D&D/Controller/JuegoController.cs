using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using View;

namespace Controller
{
    public class JuegoController
    {
        private List<Personaje> personajes;
        private List<Escena> escenas;
        private ConsolaView vista;
        private Personaje jugador;
        private Random random;

        public JuegoController()
        {
            vista = new ConsolaView();
            random = new Random();
            InicializarPersonajes();
            InicializarEscenas();
        }

        private void InicializarPersonajes()
        {
            personajes = new List<Personaje>
            {
                new Personaje("Guerrero", 16, 12, 10, 20, 15, "Golpe Poderoso"),
                new Personaje("Ladrón", 10, 16, 12, 15, 13, "Ataque Furtivo"),
                new Personaje("Mago", 8, 10, 16, 12, 10, "Bola de Fuego")
            };
        }

        private void InicializarEscenas()
        {
            escenas = new List<Escena>
            {
                new Escena("Un goblin te embosca desde los arbustos con una daga oxidada.", "Combate", 12, "Goblin", 7, 13),
                new Escena("Pisas una baldosa y escuchas un clic. ¡Es una trampa de flechas!", "Trampa", 15),
                new Escena("Un mercader misterioso te ofrece una poción a cambio de información.", "Social", 14)
            };
        }

        public void IniciarJuego()
        {
            jugador = vista.ElegirPersonaje(personajes);
            vista.MostrarMensaje($"¡Te aventuras como {jugador.Nombre}, con la habilidad {jugador.HabilidadEspecial}!");

            while (jugador.PuntosVida > 0)
            {
                Escena escena = escenas[random.Next(escenas.Count)];
                vista.MostrarMensaje($"\n{escena.Descripcion}");
                vista.MostrarEstadoPersonaje(jugador);

                string accion = vista.ElegirAccion(escena);
                ResolverAccion(escena, accion);
            }

            vista.MostrarMensaje("¡Has caído en la aventura! Fin del juego.");
        }

        private void ResolverAccion(Escena escena, string accion)
        {
            int tirada = Dado.Tirar();
            string atributo = "";
            int modificador = 0;

            switch (escena.Tipo)
            {
                case "Combate":
                    if (accion == "atacar")
                    {
                        atributo = "Fuerza";
                        modificador = jugador.ObtenerModificador(atributo);
                        tirada += modificador;
                        if (tirada >= escena.ClaseArmaduraEnemigo)
                        {
                            escena.VidaEnemigo -= random.Next(1, 7); // Daño d6
                            vista.MostrarMensaje($"¡Golpeas al {escena.Enemigo}! Le quedan {escena.VidaEnemigo} PV.");
                        }
                        else
                            vista.MostrarMensaje("¡Fallas el ataque!");
                    }
                    else if (accion == "habilidad")
                    {
                        vista.MostrarMensaje($"Usas {jugador.HabilidadEspecial} y derrotas al enemigo al instante.");
                        escena.VidaEnemigo = 0;
                    }
                    else if (accion == "huir")
                    {
                        atributo = "Destreza";
                        modificador = jugador.ObtenerModificador(atributo);
                        tirada += modificador;
                        if (tirada >= escena.Dificultad)
                            vista.MostrarMensaje("¡Escapas con éxito!");
                        else
                        {
                            jugador.PuntosVida -= random.Next(1, 6);
                            vista.MostrarMensaje("¡El enemigo te golpea mientras huyes!");
                        }
                    }
                    break;

                case "Trampa":
                    if (accion == "desactivar")
                    {
                        atributo = "Inteligencia";
                        modificador = jugador.ObtenerModificador(atributo);
                        tirada += modificador;
                        if (tirada >= escena.Dificultad)
                            vista.MostrarMensaje("Desactivas la trampa con éxito.");
                        else
                        {
                            jugador.PuntosVida -= random.Next(1, 8);
                            vista.MostrarMensaje("¡La trampa te hiere!");
                        }
                    }
                    else if (accion == "saltar")
                    {
                        atributo = "Destreza";
                        modificador = jugador.ObtenerModificador(atributo);
                        tirada += modificador;
                        if (tirada >= escena.Dificultad)
                            vista.MostrarMensaje("Saltas la trampa ágilmente.");
                        else
                        {
                            jugador.PuntosVida -= random.Next(1, 8);
                            vista.MostrarMensaje("¡Tropiezas y la trampa te golpea!");
                        }
                    }
                    else
                    {
                        vista.MostrarMensaje("Ignoras la trampa, pero sientes que algo no está bien...");
                    }
                    break;

                case "Social":
                    if (accion == "hablar")
                    {
                        atributo = "Inteligencia";
                        modificador = jugador.ObtenerModificador(atributo);
                        tirada += modificador;
                        if (tirada >= escena.Dificultad)
                            vista.MostrarMensaje("El mercader te da una poción curativa.");
                        else
                            vista.MostrarMensaje("El mercader se ofende y se marcha.");
                    }
                    else if (accion == "intimidar")
                    {
                        atributo = "Fuerza";
                        modificador = jugador.ObtenerModificador(atributo);
                        tirada += modificador;
                        if (tirada >= escena.Dificultad)
                            vista.MostrarMensaje("El mercader, asustado, te da todo lo que tiene.");
                        else
                        {
                            jugador.PuntosVida -= random.Next(1, 4);
                            vista.MostrarMensaje("El mercader llama a sus guardias y te atacan.");
                        }
                    }
                    else
                    {
                        vista.MostrarMensaje("Ignoras al mercader y sigues tu camino.");
                    }
                    break;
            }

            if (escena.VidaEnemigo > 0 && escena.Tipo == "Combate")
            {
                jugador.PuntosVida -= random.Next(1, 6);
                vista.MostrarMensaje($"El {escena.Enemigo} te ataca y te hace daño.");
            }
        }
    }
}