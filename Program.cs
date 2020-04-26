//Ángel López Benítez
//David Rodríguez Gómez
using System;

namespace Practica2fp2
{
    class Program
    {
        static void Main(string[] args)
        {
            Map mapa = new Map(100, 100);
            string file = "mapa.dat";
            bool mapaleido = false;
            try
            {
                mapa.ReadMap(file);
                mapaleido = true;
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
            if (mapaleido)
            {
                Console.WriteLine("¿Cuál es tu nombre?");
                string nombreJ = Console.ReadLine();
                Player thePLayer = new Player(nombreJ, mapa.GetentryRoom());
                string com = "";
                bool quit = false;
                while (!ArrivedAtExit(mapa, thePLayer) && thePLayer.IsAlive() && !quit)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    InfoPlace(mapa, thePLayer.GetPosition());
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("¿Qué quieres hacer?");
                    EscribeCons(mapa, thePLayer);
                    com = Console.ReadLine();
                    try
                    {
                        bool comando = HandleInput(com, thePLayer, mapa, ref quit);
                        if(!comando) Console.WriteLine("Ese comando no es válido");
                    }
                    catch(Exception e)
                    {
                        Console.Write(e.Message);
                    }
                    Console.WriteLine();
                }
                if (ArrivedAtExit(mapa, thePLayer))
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("¡Has ganado!");
                }
                else if (!thePLayer.IsAlive())
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("¡Has muerto!");
                }
            }
            
        }
        static bool HandleInput(string com, Player p, Map m, ref bool quit)
        {
            if (com.StartsWith("Go"))
            {
                string[] partes = com.Split(" ");
                if(p.Move(m, (Direction)m.Cardinal(partes[1])))return true;
                else throw new Exception("En esa dirección no hay nada");
            }
            else if (com == "Inventory")
            {
                string inventario = p.GetInventoryInfo(m);
                string[] objeto = inventario.Split("  ");
                for (int i = 0; i < objeto.Length; i++)
                {
                    Console.WriteLine(objeto[i]);
                }
                return true;
            }
            else if (com == "Me")
            {
                Console.WriteLine(p.GetPlayerInfo());
                return true;
            }
            else if (com == "Look")
            {
                string objetosHabitación = m.GetInfoItemsInRoom(p.GetPosition());
                string[] objetos = objetosHabitación.Split("  ");
                for (int i = 0; i < objetos.Length; i++)
                {
                    Console.WriteLine(objetos[i]);
                }
                return true;
            }
            else if (com == "Info")
            {
                InfoPlace(m, p.GetPosition());
                EscribeCons(m, p);
                return true;
            }
            else if (com == "quit")
            {
                quit = true;
                Console.WriteLine("¡Te echaremos de menos!");
                return true;
            }
            else if (com.StartsWith("Eat"))
            {
                string[] partescomando = com.Split(" ");
                p.EatItem(m, partescomando[1]);
                Console.WriteLine("Me he comido el objeto");
                return true;
            }
            else if (com.StartsWith("Pick"))
            {
                string[] partescomando = com.Split(" ");
                p.PickItem(m, partescomando[1]);
                Console.WriteLine("He cogido el objeto");
                return true;
            }
            else if (com == "Help")
            {
                Opciones();
                return true;
            }
            else return false;
        } 
        static void InfoPlace(Map m, int roomNumber)
        {
            Console.Write(m.GetRoomInfo(roomNumber));
        }
        static bool ArrivedAtExit(Map m, Player thePlayer)
        {//Comprobamos si el jugador está en la salida
            return m.IsExit(thePlayer.GetPosition());
        }
        static void EscribeCons(Map m, Player thePLayer)
        {
            string conexiones = m.GetMovesInfo(thePLayer.GetPosition());
            string[] cons = conexiones.Split("  ");
            for (int i = 0; i < cons.Length; i++) Console.WriteLine(cons[i]);
        }//Método auxiliar//
        static void Opciones()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Movimientos Go + (n,s,e,w)");
            Console.WriteLine("Inventory: Muestra el inventario");
            Console.WriteLine("Me: Información sobre el jugador");
            Console.WriteLine("Look: Información sobre los objetos de la sala");
            Console.WriteLine("Info: Descripción sobre la sala y las conexiones");
            Console.WriteLine("Quit: Salir del juego");
            Console.WriteLine("Eat + Nombre Del Objeto: Comer objeto");
            Console.WriteLine("Pick + Nombre Del Objeto: Recoger Objeto");
        } //Método auxiliar//
    }
}
