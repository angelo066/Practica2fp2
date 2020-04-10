﻿using System;
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
                int contador = 0; //Para contar las lineas//
                while (!f.EndOfStream)
                {
                    //try por si hay un problema durante la lectura del mapa//
                    try
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
                            items[j] = CreateItem(linea, j);
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
                        contador++;
                    }
                    catch(Exception e)
                    {
                        throw new Exception(e.Message + "en la línea" + contador);  
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
        private Item CreateItem(string objeto, int indice) 
        {   
            Item obj;
            obj.description = ReadDescription(objeto);
            string[] partesItem = objeto.Split(" ");
            obj.name = partesItem[1];
            //Try por si los pesos o los hp no son números
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
            int i = FindRoomByName(partescon[1]);           //Buscamos habitación incial//
            int j = FindRoomByName(partescon[3]);           //Buscamos habitación de destino//
            //Excepción si no se encunetra una//
            if(i==-1 || j==-1) throw new Exception("Ha habido un problema con los nombres de la habitaciones");
            //Para hacer las conexiónes inversas comprobamos hacía que direción están las habitaciones
            if (partescon[2]=="n"|| partescon[2] == "e")
            {
                    rooms[i].connections[Cardinal(partescon[2])] = j;
                    rooms[j].connections[Cardinal(partescon[2])+1] = i;
            }
            else if (partescon[2] == "s" || partescon[2] == "w")
                {
                    rooms[i].connections[Cardinal(partescon[2])] = j;
                    rooms[j].connections[Cardinal(partescon[2])-1] = i;
                }
            //Si hay un problema con la dirección, excepción//
            else throw new Exception("Ha habido un problema con la dirección de la conexión");            
        }
        public int Cardinal(string cardinal) //Método auxiliar//
        {//Devolvemos un número en función de un cardinal//
            if (cardinal == "n") return 0;
            else if (cardinal == "s") return 1;
            else if (cardinal == "e") return 2;
            else return 3;            
        }
        private void MeteObjeto(string lugar, int indice) //Método auxiliar//
        {
            int i = FindRoomByName(lugar);
            if (i == -1) throw new Exception("La habitación no existe"); //Si no se encuentra excepción//
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
            if (partes[1] == null) throw new Exception("Descripción nula");
            return partes[1];
        }   
        public int FindItemByName(string itemName)
        {
            int indice = -1;
            int i = 0;
            while (i<items.Length && items[i].name != itemName) i++;
            if (i < items.Length) indice = i;
            return indice;
        }
        private int FindRoomByName(string roomName)
        {
            int indice = -1;
            int i = 0;
            while (i < rooms.Length && rooms[i].name != roomName) i++;
            if (i < rooms.Length) indice = i;
            return indice;
        }
        public int GetItemWeight(int itemNumber)
        {
            return items[itemNumber].weight;
        }
        public int GetItemHP(int itemNumber)
        {
            return items[itemNumber].hp;
        }
        public string PrintItemInfo(int itemNumber)
        {
            Item item = items[itemNumber];
            return item.name + " " + GetItemWeight(itemNumber) + " " + GetItemHP(itemNumber) + " " + item.description + "/";
        }
        private string GetRoomInfo(int roomNumber)
        {
            return rooms[roomNumber].name +" "+ rooms[roomNumber].description;
        }
        public string GetInfoItemsInRoom(int roomNumber)
        {
            Room habitación = rooms[roomNumber]; //Mayor legibilidad
            if (habitación.itemsInRoom.cuentaEltos() == 0) return ("I don´t see anything notable here");
            string info = "";
            int i = 1;
            int cota = habitación.itemsInRoom.cuentaEltos(); //Mayor legibilidad//
            while (i <= cota)
            {
                info = info + " " + PrintItemInfo(habitación.itemsInRoom.nEsimo(i))+" ";
                i++;
            }
            return info;
        }
        public bool PickItemInRoom(int roomNumber, int itemNumber)
        {//Borramos el item en cuestión de la habitación
            Room habitacion = rooms[roomNumber];
            return habitacion.itemsInRoom.BorraElto(itemNumber); 
        }
        private bool IsExit(int roomNumber)
        {
            return rooms[roomNumber].exit;
        }
        private int GetentryRoom()
        {
            return entryRoom;
        }
        private string GetMovesInfo(int roomNumber)
        {
            string info = "";
            Room habitación = rooms[roomNumber]; 
            for(int i = 0; i < habitación.connections.Length; i++)
            {
                int indice = habitación.connections[i]; //Mayor legibilidad//
                if (indice != -1) info = info + CardinalDeNúmero(i) + " " + rooms[indice].name;
            }
            return info;
        }
        private char CardinalDeNúmero(int numero)
        {//Devuleve un char en función del número//
            if (numero == 0) return 'n';
            else if (numero == 1) return 's';
            else if (numero == 2) return 'e';
            else if (numero == 3) return 'w';
            else return ' ';
        } //Método auxiliar
        public int Move(int roomNumber, Direction dir)
        {
            return rooms[roomNumber].connections[(int)dir];
        }
        public void Depura()
        {
            for (int i = 0; i < 4; i++)
            {
                
            } 
        }       
    }
}

