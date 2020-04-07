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
            //Se puede usar el método findroomby name para no tener que hacer búsquedas?//
            //¿Como se supone que tengo que saber en que linea ocurre el error?(Un contador)?//
            //¿Como se cual es el error?//
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
                        items[j]=CreateItem(linea,j);
                        j++;
                    }
                    else if (linea.StartsWith("entry"))
                    {
                        string[] parteshab = linea.Split(" ");
                        int k = 0;
                        while (rooms[k].name != parteshab[1]) k++;
                        entryRoom = k;
                    }
                    else if (linea.StartsWith("exit"))
                    {
                        string[] parteshab = linea.Split(" ");
                        int k = 0;
                        while (rooms[k].name != parteshab[1]) k++;
                        rooms[k].exit = true;
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
            habitat.exit = false;
            InicializaConn(out habitat.connections);
            habitat.itemsInRoom = new Lista();
            return habitat;
        }
        private Item CreateItem(string objeto, int indice)  //Añandido un parámetro int que representa su índice en el v
        {   //Preguntar si usar este índice como parámetro está bien//
            Item obj;
            obj.description = ReadDescription(objeto);
            string[] partesItem = objeto.Split(" ");
            obj.name = partesItem[1];
            try
            {
                obj.weight = int.Parse(partesItem[2]);
                obj.hp = int.Parse(partesItem[3]);
            }
            catch
            {
                throw new Exception("El peso y el hp deben ser números");
            }           
            MeteObjeto(partesItem[4], indice);
            return obj;
        }   
        private void AddConections(string conexion) //Método auxiliar//
        {
            string[] partescon = conexion.Split(" ");
            int i = 0;
            while (i < rooms.Length && rooms[i].name != partescon[1]) i++;
            //throw corta flujo???//
            if (i == rooms.Length) throw new Exception("Ha habido un problema con los nombres de la habitaciones");
            else
            {
                int j = 0;
                while (j < rooms.Length && rooms[j].name != partescon[3]) j++;
                if(partescon[2]=="n"|| partescon[2] == "e")
                {
                    rooms[i].connections[Cardinal(partescon[2])] = j;
                    rooms[j].connections[Cardinal(partescon[2])+1] = i;
                }
                else if (partescon[2] == "s" || partescon[2] == "w")
                {
                    rooms[i].connections[Cardinal(partescon[2])] = j;
                    rooms[j].connections[Cardinal(partescon[2])-1] = i;
                }
                else throw new Exception("Ha habido un problema con la dirección de la conexión");
            }
        }
        private int Cardinal(string cardinal) //Método auxiliar//
        {
            if (cardinal == "n") return 0;
            else if (cardinal == "s") return 1;
            else if (cardinal == "e") return 2;
            else return 3;            
        }
        private void MeteObjeto(string lugar, int indice) //Método auxiliar//
        {
            int i = 0;
            while (i < rooms.Length && rooms[i].name != lugar) i++;
            if (i == rooms.Length) throw new Exception("La habitación no existe");
            rooms[i].itemsInRoom.InsertaIni(indice);
        }
        private void InicializaConn(out int [] conns) //Método auxiliar//
        {
            conns = new int[4];
            for(int i=0; i < conns.Length; i++)
            {
                conns[i] = -1;
            }
        }
        private string ReadDescription(string linea)
        {
            string[] partes = linea.Split("\"");
            return partes[1];
        }   
        /*public void Depura()
        {
            for (int i = 0; i < nRooms; i++)
            {
                //Console.WriteLine(items[i].name + " " + i);
                Console.WriteLine(rooms[i].name);
                rooms[i].itemsInRoom.ver();
            } 
        }*/        
    }
}

