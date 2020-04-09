using System;
namespace Practica2fp2
{
    class Player
    {
        string name; // nombre del jugador
        int pos; // lugar en el que esta
        int hp; // health points
        int weight; // peso de los objetos que tiene
        Lista inventory; // lista de objetos que lleva
        const int MAX_HP = 10; // maximo health points
        const int HP_PER_MOVEMENT = 2; // hp consumidos por movimiento
        const int MAX_WEIGHT = 20; // maximo peso que puede llevar
        public Player(string playerName, int entryRoom)
        {
            name = playerName;
            pos = entryRoom;
            hp = MAX_HP;
            weight = 0;
            inventory = new Lista();
        }
        public int GetPosition()
        {
            return pos;
        }
        public bool IsAlive()
        {
            return hp > 0;
        }
        public bool Move(Map m, Direction dir)
        {
            int destino = m.Move(pos, dir);
            if (destino != -1)
            {
                pos = destino;
                return true;
            }
            return false;
        }
        public void PickItem(Map m, string itemName)
        {
            
        }
    }
}

