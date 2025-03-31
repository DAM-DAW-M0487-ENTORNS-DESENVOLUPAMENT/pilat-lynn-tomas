using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workshop_8
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Pila<char> charPile = new Pila<char>();
                Pila<double> doublePile = new Pila<double>(5);
                int[] intArray = { 2, 3, 4, 5, 6, 7, 8, 9, -41 };
                Pila<int> intPile = new Pila<int>(intArray);
                Console.WriteLine(intPile.Count);
                Console.WriteLine(doublePile.IsReadOnly);
                Console.WriteLine(intPile.IsFull);
                Console.WriteLine(doublePile.IsEmpty);
                Console.WriteLine(charPile.Capacity);
                //Console.WriteLine(intPile[5]);
                charPile.Add('h');
                Console.WriteLine(intPile.Remove(2));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
