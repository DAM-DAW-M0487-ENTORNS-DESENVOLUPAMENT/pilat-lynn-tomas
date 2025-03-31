using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEnumerador
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /// <summary>
            /// Classe MyEnumerator generadora de cursors
            /// </summary>
            public class MyEnumerator : IEnumerator<T>
            {
            private int limmit;
            private int position;
            private T[] values;

            /// <summary>
            /// Constructor de la subclasse MyEnumenator
            /// </summary>
            /// <param name="dades">Taula de dades de la TaulaLlista</param>
            /// <param name="nElem">Nombre d'elements dins la TaulaLlista</param>
            public MyEnumerator(T[] dades, int nElem)
            {
                this.values = dades;
                this.position = 0;
                this.limmit = nElem;
            }

            /// <summary>
            /// Propietat que retorna l'objecte a la posició actual del cursor
            /// </summary>
            public T Current
            {
                get
                {
                    if (position == -1 || position >= limmit) throw new Exception("OUT OF RANGE");

                    return values[position];
                }
            }

            /// <summary>
            /// Propietat que obté l'objecte de la posició actual del cursor cridant a la propietat Current
            /// </summary>
            object IEnumerator.Current
            {
                get
                {
                    return Current;
                }
            }

            /// <summary>
            /// Mètode que destrueix el cursor
            /// </summary>
            public void Dispose()
            {
                this.values = null;
            }

            /// <summary>
            /// Mètode que avança el cursor una propietat (si és possible)
            /// </summary>
            /// <returns>Si el cursor avanca, retorna "true"; si el cursor no pot avançar, retorna "false"</returns>
            public bool MoveNext()
            {
                bool theresNext = true;
                position++;
                if (position >= limmit) theresNext = false;
                return theresNext;
            }

            /// <summary>
            /// Mètode que reinicia el cursor
            /// </summary>
            public void Reset()
            {
                this.position = -1;
            }
            }

    }
}
