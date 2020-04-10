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
                for (int i = 0; i < inventario.Length; i++)
                {
                    Console.WriteLine(objeto[i]);
                }
                return true;
            }
            else if (com == "Información player")
            {
                Console.Write(p.GetPlayerInfo());
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
            else if (com == "PickItem")
            {

            }
        }
    }
}
