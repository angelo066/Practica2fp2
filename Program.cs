//Ángel López Benítez
//David Rodríguez Gómez
using System;

namespace Practica2fp2
{
    class Program
    {
        static void Main(string[] args)
        {           
            Map mapa = new Map(18, 8);
            string file = "mapa.dat";
            mapa.ReadMap(file);
            Console.WriteLine("¿Cuál es tu nombre?");
            string nombreJ = Console.ReadLine();
            Player thePLayer = new Player(nombreJ, mapa.GetentryRoom());
            string com = "";
            bool quit = false;
            Console.WriteLine(mapa.GetRoomInfo(thePLayer.GetPosition()));
            while (!ArrivedAtExit(mapa, thePLayer) && thePLayer.IsAlive() && !quit)
            {
                Console.WriteLine(mapa.GetMovesInfo(thePLayer.GetPosition()));
                Console.WriteLine("¿Qué quieres hacer?");
                Console.WriteLine();
                com = Console.ReadLine();
                if (!HandleInput(com, thePLayer, mapa, ref quit))
                {
                    Console.Write("Ese comando no es válido");
                }
                Console.WriteLine();
                Console.WriteLine();
            }
            if (ArrivedAtExit(mapa, thePLayer)) Console.WriteLine("¡Has encontrado al perrete!");
            else if (!thePLayer.IsAlive()) Console.WriteLine("¡Has muerto!");
        }
        static bool HandleInput(string com, Player p, Map m, ref bool quit)
        {
            if (com == "n" || com == "s" || com == "e" || com == "w")
            {
                p.Move(m, (Direction)m.Cardinal(com));
                return true;
            }
            else if (com == "Muestra inventario")
            {
                string inventario = p.GetInventoryInfo(m);
                string[] objeto = inventario.Split("/");
                for (int i = 0; i < objeto.Length; i++)
                {
                    Console.WriteLine(objeto[i]);
                }
                return true;
            }
            else if (com == "Información player")
            {
                Console.WriteLine(p.GetPlayerInfo());
                return true;
            }
            else if (com == "Objetos en la habitación")
            {
                string objetosHabitación = m.GetInfoItemsInRoom(p.GetPosition());
                string[] objetos = objetosHabitación.Split("/");
                for (int i = 0; i < objetos.Length; i++)
                {
                    Console.WriteLine(objetos[i]);
                }
                return true;
            }
            else if (com == "Info de la habitación")
            {
                InfoPlace(m, p.GetPosition());
                return true;
            }
            else if (com == "quit")
            {
                quit = true;
                Console.WriteLine("¡Te echaremos de menos!");
                return true;
            }
            else if (com == "Conexiones")
            {
                Console.WriteLine(m.GetMovesInfo(p.GetPosition()));
                return true;
            }
            else
            {
                try
                {
                    p.PickItem(m, com);
                    return true;
                }
                catch
                {
                    try
                    {
                        p.EatItem(m, com);
                        return true;
                    }
                    catch (Exception e)
                    {
                        Console.Write(e.Message);
                        return false;
                    }

                }
            }
        } 
        static void InfoPlace(Map m, int roomNumber)
        {
            Console.Write(m.GetRoomInfo(roomNumber));
        }
        static bool ArrivedAtExit(Map m, Player thePlayer)
        {
            return m.IsExit(thePlayer.GetPosition());
        }
    }
}
