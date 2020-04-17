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
                hp = hp - HP_PER_MOVEMENT;
                return true;
            }
            return false;
        }
        public void PickItem(Map m, string itemName)
        {//guardamos el índice//
            int objeto = m.FindItemByName(itemName);
            if (objeto == -1) throw new Exception("No existe el objeto");
            else if (weight + m.GetItemWeight(objeto) > MAX_WEIGHT) throw new Exception("Llevas demasiado peso en el inventario");
            //si no lo elimina es que no está//
            else if (!m.PickItemInRoom(pos, objeto)) throw new Exception("El objeto no esta en la sala");
            //Si ya lo ha eliminado en la comprobación, lo insertamos en el inventaio//
            inventory.InsertaIni(objeto);
            weight = weight + m.GetItemWeight(objeto);           
        }
        public void EatItem(Map m, string ItemName)
        {
            int objeto = m.FindItemByName(ItemName);
            if (objeto == -1) throw new Exception("El objeto no existe");
            int itemVida = m.GetItemHP(objeto);
            if (inventory.cuentaOcurrencias(objeto) == 0) throw new Exception("No llevas ese objeto");
            else if (itemVida == 0) throw new Exception("Ese objeto no es comestible");
            else
            {
                hp = hp + itemVida;
                weight = weight - m.GetItemWeight(objeto);
                inventory.BorraElto(objeto);
            }         
        }
        public string GetInventoryInfo(Map m)
        {
            int numeroelementos = inventory.cuentaEltos(); //Mayor legibilidad//
            if (numeroelementos == 0) return "My bag is empty";
            string info="";
            for(int i = 1; i <= numeroelementos; i++)
            {
                int indice = inventory.nEsimo(i);           //Mayor legibilidad//
                info = info + "  " + m.PrintItemInfo(indice);
            }
            return info;
        }
        public string GetPlayerInfo()
        {
            return name+ " " + "hp:" + " " + hp + " "+"Weight:" + weight;
        }
    }
}

