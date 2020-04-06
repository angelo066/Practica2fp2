using System;

namespace Practica2fp2
{
    public class Lista
    {
        public Lista()
        {
            pri = null;
        }
        private class Nodo
        {
            public int dato;
            public Nodo sig;
        }
        Nodo pri;
        public void InsertaIni(int e)
        {
            Nodo aux = new Nodo();
            aux.dato = e;
            aux.sig = pri;
            pri = aux;
        }
        public void ver()
        {
            Console.Write("\nLista: ");
            Nodo aux = pri;
            while (aux != null)
            {
                Console.Write(aux.dato + " ");
                aux = aux.sig;
            }
            Console.WriteLine();
            Console.WriteLine();
        }
        public int Suma()
        {
            int resultat = 0;
            Nodo aux = pri;
            for (int i = 0; i < cuentaEltos(); i++) resultat = resultat + aux.dato;
            return resultat;
        }
        public int cuentaEltos()
        {
            int elementos = 0;
            Nodo aux = pri;
            while (aux != null)
            {
                elementos++;
                aux = aux.sig;
            }           //Poniendo aux!=null funciona sin poner el if de abajo//
            return elementos;
        }
        public int cuentaOcurrencias(int e)
        {
            Nodo aux = pri;
            int contador = 0;
            while (aux.sig != null)
            {
                if (aux.dato == e) contador++;
                aux = aux.sig;
            }
            if (aux.dato == e) contador++;
            return contador;
        }
        private Nodo nEsimoNodo(int n)
        {
            if (n == 0) return null;
            if (cuentaEltos() < n) return null;
            Nodo aux = pri;
            int i = 1;
            while (aux != null && i < n)
            {
                aux = aux.sig;
                i++;
            }
            return aux;
        }
        public int nEsimo(int n)
        {
            Nodo aux = nEsimoNodo(n);
            if (aux != null)
            {
                return aux.dato;
            }
            else
            {
                throw new Exception("El elemento en cuestión no existe");
            }
        }
        public void insertaNesimo(int n, int e)
        {
            if (n == 1) throw new Exception("Para insertar en el primer lugar se debe usar InsertaIni");
            Nodo New = new Nodo();
            Nodo nmenos = nEsimoNodo(n - 1);
            Nodo nmas = nEsimoNodo(n);
            New.dato = e;
            New.sig = nmas;
            nmenos.sig = New;
        }
        public void borraTodos(int e)
        {
            //Extender el método anterior para que borre todos en una vuelta a la lista//
            int numerodelemtos = cuentaOcurrencias(e);
            while (numerodelemtos > 0)
            {
                BorraElto(e);
                numerodelemtos--;
            }
        }
        public void borraNesimo(int n)
        {
            if (cuentaEltos() < n) throw new Exception("El elemento no existe");
            Nodo aux = pri;
            int i = 1;
            while (aux.sig != null && i < n)
            {
                aux = aux.sig;
                i++;
            }
            if (aux == pri) pri = pri.sig;
            else if (aux.sig == null) nEsimoNodo(i - 1).sig = null; //¿No funciona porque lo que tiene que ser null es el indicador?
            else nEsimoNodo(i - 1).sig = aux.sig;
        }
        public void invierte()
        {
            if (pri == null) throw new Exception("La lista está vacía");
            Nodo aux;
            Nodo priaux = null;
            while (pri != null)
            {
                aux = pri;          //Guardamos el nodo de pri//
                pri = pri.sig;      //Cortamos el cable que lo hace primero//
                aux.sig = priaux;   //Cortamos el cable que le indica su siguiente//
                priaux = aux;       //Hacemos que el puntero de primero que asignaremos apunte al último que metemos
            }
            pri = priaux;               //Reasignamos el primero//         
        }
        public bool iguales(Lista l)
        {
            bool iguales = true;
            Nodo aux = pri;
            Nodo igual = l.pri;
            if (cuentaEltos() != l.cuentaEltos()) return false;
            while (aux != null && iguales)
            {
                if (aux.dato != igual.dato) iguales = false;
                aux = aux.sig;
                igual = igual.sig;
            }
            return iguales;
        }
        public bool iguales2(Lista l)
        {
            bool iguales = true;
            if (cuentaEltos() != l.cuentaEltos()) return false;
            Nodo aux = pri;
            Nodo igual = l.pri;
            while (aux != null && iguales)
            {
                while (aux.dato != igual.dato && iguales)
                {
                    if (igual == null) iguales = false;
                    igual = igual.sig;
                }
                aux = aux.sig;
            }
            return iguales;
        }
        public bool BorraElto(int e)
        {
            if (pri == null) return false;
            else if (pri.dato == e)
            {  // pri no es null				
                pri = pri.sig;
                return true;
            }
            else
            { // lista no vacia y e no está el primero
                Nodo aux = pri;
                while (aux.sig != null && aux.sig.dato != e)
                {
                    aux = aux.sig;
                }
                // aux apunta al anterior al q tengo q eliminar
                // o bien aux.sig==null y no he encontrado elto e
                if (aux.sig == null) return false;
                else
                {
                    aux.sig = aux.sig.sig;
                    return true;
                }
            }
        }
    }
}

