using System;
using System.IO;
public enum Direction { North, South, East, West };
namespace Practica2fp2
{
    class Map
    {
        // items
        public struct Item
        {
            public string name, description;
            public int hp; // health points
            public int weight; // peso del item
        }
        // lugares del mapa
        public struct Room
        {
            public string name, description;
            public bool exit; // es salida?
            public int[] connections; // vector de 4 componentes
                                      // con el lugar al norte, sur, este y oeste
                                      // -1 si no hay conexion
            public Lista itemsInRoom; // indices al vector de items n los items del lugar
        }
        Room[] rooms; // vector de lugares del mapa
        Item[] items; // vector de items del juego
        int nRooms, nItems; // numero de lugares y numero de items
        int entryRoom; // numero de la habitacion de entrada (leida del mapa)
        public Map(int numRooms, int numItems)
        {
            nRooms = numRooms;
            nItems = numItems;
            rooms = new Room[nRooms];
            items = new Item[nItems];
        }
        public void ReadMap(string file)
        {
            if (file != null)
            {
                int i = 0;
                int j = 0;
                StreamReader f = new StreamReader(file);
                while (!f.EndOfStream)
                {
                    string linea = f.ReadLine();
                    if (linea.StartsWith("room"))
                    {
                        rooms[i] = CreateRoom(linea);
                        i++;
                    }
                    else if (linea.StartsWith("conn")) AddConections(linea);
                    else if (linea.StartsWith("item"))
                    {
                        items[j]=CreateItem(linea);
                        j++;
                    }
                }
            }
            else throw new Exception("El archivo a leer no existe");
            
        }
        private Room CreateRoom(string habitacion)
        {
            Room habitat = new Room();
            habitat.description = ReadDescription(habitacion);
            string[] parteshab = habitacion.Split(" ");
            habitat.name = parteshab[1];
            return habitat;
        }
        private Item CreateItem(string objeto)
        {
            Item obj;
            obj.description = ReadDescription(objeto);
            string[] partesItem = objeto.Split(" ");
            obj.name = partesItem[1];
            obj.weight = int.Parse(partesItem[2]);
            obj.hp = int.Parse(partesItem[3]);
            return obj;
        }
        private void AddConections(string conexion) //Método auxiliar//
        {
            string[] partescon = conexion.Split(" ");
            int i = 0;
            while (i < rooms.Length && rooms[i].name != partescon[1]) i++;
            //throw corta flujo???//
            if (i == partescon.Length) throw new Exception("Ha habido un problema con los nombres de la habitaciones");
            else
            {
                int j = 0;
                while (j < rooms.Length && rooms[j].name != partescon[3]) j++;
                rooms[i].connections[Cardinal(partescon[2])] = j;
            }
        }
        private int Cardinal(string cardinal) //Método auxiliar//
        {
            if (cardinal == "n") return 0;
            else if (cardinal == "s") return 1;
            else if (cardinal == "e") return 2;
            else if (cardinal == "w") return 3;
            else throw new Exception("Ha habido un problema con la dirección de la conexión");
        }
        private void MeteObjeto(string lugar) //Método auxiliar//
        {
            int i = 0;
            while (i < rooms.Length && rooms[i].name != lugar) i++;
            rooms[i].itemsInRoom.InsertaIni();
        }
        private string ReadDescription(string linea)
        {
            string[] partes = linea.Split("\"");
            return partes[1];
        }   
        public void Depura()
        {
            
        }
        
    }
}

