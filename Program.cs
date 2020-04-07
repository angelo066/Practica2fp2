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
            mapa.ReadMap("mapa.dat");
            //mapa.Depura();
        }
    }
}
