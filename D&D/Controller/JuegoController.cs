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
                // C
                new Escena("Un goblin te embosca desde los arbustos con una daga oxidada.", "Combate", 12, "Goblin", 7, 13),
                new Escena("Un lobo salvaje gruñe y salta hacia ti desde la niebla.", "Combate", 14, "Lobo Salvaje", 10, 14),
                new Escena("Un esqueleto surge de un montón de huesos y te ataca.", "Combate", 13, "Esqueleto", 8, 12),
                
                // T
                new Escena("Pisas una baldosa y escuchas un clic. Es una trampa de flechas", "Trampa", 15),
                new Escena("Un puente de cuerda cruje peligrosamente mientras lo cruzas.", "Trampa", 16),
                new Escena("Un tesoro brillante sobre un pedestal, pero sientes una trampa cercana.", "Trampa", 14),
                
                // S
                new Escena("Un mercader misterioso te ofrece una pocion a cambio de informacion.", "Social", 14),
                new Escena("Un guardia te detiene en la entrada de la ciudad, sospechando que eres un espia.", "Social", 15),
                new Escena("Un anciano en una taberna te cuenta un rumor, pero quiere algo a cambio.", "Social", 13)
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

            vista.MostrarMensaje("Fin del juego.");
        }

        private void ResolverAccion(Escena escena, string accion)
        {
            int tirada = 0;
            string atributo = "";
            int modificador = 0;

            if (accion != "habilidad" && accion != "ignorar")
            {
                vista.MostrarMensaje("Preparandote para tirar el dado...");
                tirada = Dado.Tirar();
                vista.MostrarMensaje($"¡El dado muestra un {tirada}!");
            }

            switch (escena.Tipo)
            {
                case "Combate":
                    if (accion == "atacar")
                    {
                        atributo = "Fuerza";
                        modificador = jugador.ObtenerModificador(atributo);
                        tirada += modificador;
                        vista.MostrarMensaje($"Modificador de {atributo} (+{modificador}): Total = {tirada}");
                        if (tirada >= escena.ClaseArmaduraEnemigo)
                        {
                            int daño = random.Next(1, 7); 
                            escena.VidaEnemigo -= daño;
                            vista.MostrarMensaje($"¡Golpeas al {escena.Enemigo} por {daño} de daño! Le quedan {escena.VidaEnemigo} PV.");
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
                        vista.MostrarMensaje($"Modificador de {atributo} (+{modificador}): Total = {tirada}");
                        if (tirada >= escena.Dificultad)
                            vista.MostrarMensaje("¡Escapas con exito!");
                        else
                        {
                            int daño = random.Next(1, 6);
                            jugador.PuntosVida -= daño;
                            vista.MostrarMensaje($"¡El enemigo te golpea por {daño} de daño mientras huyes!");
                        }
                    }
                    break;

                case "Trampa":
                    if (accion == "desactivar")
                    {
                        atributo = "Inteligencia";
                        modificador = jugador.ObtenerModificador(atributo);
                        tirada += modificador;
                        vista.MostrarMensaje($"Modificador de {atributo} (+{modificador}): Total = {tirada}");
                        if (tirada >= escena.Dificultad)
                            vista.MostrarMensaje("Desactivas la trampa con éxito.");
                        else
                        {
                            int daño = random.Next(1, 8);
                            jugador.PuntosVida -= daño;
                            vista.MostrarMensaje($"¡La trampa te hiere por {daño} de daño!");
                        }
                    }
                    else if (accion == "saltar")
                    {
                        atributo = "Destreza";
                        modificador = jugador.ObtenerModificador(atributo);
                        tirada += modificador;
                        vista.MostrarMensaje($"Modificador de {atributo} (+{modificador}): Total = {tirada}");
                        if (tirada >= escena.Dificultad)
                            vista.MostrarMensaje("Saltas la trampa agilmente.");
                        else
                        {
                            int daño = random.Next(1, 8);
                            jugador.PuntosVida -= daño;
                            vista.MostrarMensaje($"¡Tropiezas y la trampa te golpea por {daño} de daño!");
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
                        vista.MostrarMensaje($"Modificador de {atributo} (+{modificador}): Total = {tirada}");
                        if (tirada >= escena.Dificultad)
                            vista.MostrarMensaje("El mercader te da una pocion curativa.");
                        else
                            vista.MostrarMensaje("El mercader se ofende y se marcha.");
                    }
                    else if (accion == "intimidar")
                    {
                        atributo = "Fuerza";
                        modificador = jugador.ObtenerModificador(atributo);
                        tirada += modificador;
                        vista.MostrarMensaje($"Modificador de {atributo} (+{modificador}): Total = {tirada}");
                        if (tirada >= escena.Dificultad)
                            vista.MostrarMensaje("El mercader, asustado, te da todo lo que tiene.");
                        else
                        {
                            int daño = random.Next(1, 4);
                            jugador.PuntosVida -= daño;
                            vista.MostrarMensaje($"El mercader llama a sus guardias y te atacan por {daño} de daño.");
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
                int daño = random.Next(1, 6);
                jugador.PuntosVida -= daño;
                vista.MostrarMensaje($"El {escena.Enemigo} te ataca y te hace {daño} de daño.");
            }
        }
    }
}