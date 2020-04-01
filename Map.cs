using System;

public enum Direction { North, South, East, West };
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

    }
    
}
