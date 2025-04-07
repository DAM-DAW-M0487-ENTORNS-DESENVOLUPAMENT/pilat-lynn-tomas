using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Workshop_8
{
    public class Pila<T> : IEnumerable<T>, ICollection<T>
    {
        //Atributes
        const int ABSOLUTE_BOTTOM = -1;
        const int DEFAULT_SIZE = 8;
        private T[] data;
        private int top = -1;

        //Interface Propeties
        public int Count { get { return top + 1; } }

        public bool IsReadOnly { get { return false; } }

        /// <summary>
        /// Propietat que indica si la Pila està plena
        /// </summary>
        public bool IsFull
        {
            get
            {
                if (data == null) throw new Exception("STACK IS EMPTY");

                if (data.Length - 1 == top) return true;
                else return false;
            }
        }

        /// <summary>
        /// Propietat si la pila està buida
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                if (top == -1) return true;
                else return false;
            }
        }

        /// <summary>
        /// Propietat (extreta parcialment de la interficie IList<T>, implementant solament la meitat "get")
        /// que ens permet accedir de forma indexada a la pila.
        /// </summary>
        /// <param name="index">Posicio de l'element que volem veure de la pila</param>
        /// <returns>L'element de la pila que es troba en l'index especificat com a paràmetre</returns>
        /// <exception cref="ArgumentOutOfRangeException">L'índex és més gran o més petit que el recompte d'elements en la </exception>
        public T this[int index]
        {
            get
            {
                if (index < 0 || index.Equals(Count)) throw new ArgumentOutOfRangeException();

                return data[index];
            }
        }

        /// <summary>
        /// Propietat que indica la capacitat de la Pila
        /// </summary>
        public int Capacity
        {
            get
            {
                if (data == null) throw new Exception("STAC IS EMPTY");

                return data.Length;
            }
        }

        //Builders
        /// <summary>
        /// Constructor que proporciona una Pila de tamany estandar usant la constant DEFAULT_SIZE
        /// </summary>
        public Pila() : this(DEFAULT_SIZE) { }

        /// <summary>
        /// Constructior que proporciona una Pila amb el tamany especificat en el main.
        /// </summary>
        /// <param name="size">Tamany de la Pila</param>
        public Pila(int size)
        {
            data = new T[size];
        }

        /// <summary>
        /// Constructor que proporciona una Pila basada en una array passada com a parametre
        /// </summary>
        /// <param name="array">Array en la qual es basarà la Pila</param>
        public Pila(T[] array) : this((IEnumerable<T>)array)
        {
            /*data = new T[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                data[i] = array[i];
            }
            top = data.Length - 1;*/
        }

        /// <summary>
        /// Constructor que, donat un element IEnumerable, proporciona una pila basada en aquest paràmetre
        /// </summary>
        /// <param name="elementIEnumerable">Estructura que implementa la interficie IEnumerable, amb la qual construim la pila</param>
        public Pila(IEnumerable<T> elementIEnumerable)
        {
            data = new T[elementIEnumerable.Count()];
            for (int i = 0; i < elementIEnumerable.Count(); i++)
            {
                data[i] = elementIEnumerable.ElementAt(i);
            }
            top = data.Length - 1;
        }

        //Class Methods
        /// <summary>
        /// Mètode que mostra i elimina el primer element de la pila, si aquesta NO està buida
        /// </summary>
        /// <returns>El primer element de la pila</returns>
        /// <exception cref="InvalidOperationException">L'excepció salta si pila està buida</exception>
        public T Pop()
        {
            if (this.top == ABSOLUTE_BOTTOM) throw new InvalidOperationException();

            T topElement = data[this.top];
            data[this.top] = default(T);
            top--;
            return topElement;
        }

        /// <summary>
        /// Mètode que mostra el primer element de la pila SENSE ELIMINAR-L'HO, si aquesta NO està buida
        /// </summary>
        /// <returns>El primer element de la pila</returns>
        /// <exception cref="InvalidOperationException">L'excepció salta si pila està buida</exception>
        public T Peek()
        {
            if (this.top == -1) throw new InvalidOperationException();

            return data[this.top];
        }

        /// <summary>
        /// Mètode que afageix a la pila un element passat per paràmetre
        /// </summary>
        /// <param name="item">Element a afegir a la pila</param>
        /// <exception cref="StackOverflowException">L'excepció salta si la pila està plena</exception>
        public void Push(T item)
        {
            if (this.Count == data.Length) throw new StackOverflowException();

            top++;
            data[top] = item;
        }

        /// <summary>
        /// Mètode que transfereix les dades de la pila a un array exterior amb el mateix tamany que la pila
        /// </summary>
        /// <returns>Array de llargada this.Count amb els valors de la pila</returns>
        /// <exception cref="InvalidOperationException">L'excepció salta si pila està buida</exception>
        public T[] ToArray()
        {
            if (this.Count == 0) throw new InvalidOperationException();

            T[] values = new T[this.Count];
            IEnumerator<T> cursor = new EnumeradorPila(data, top);
            int i = 0;
            values[i] = cursor.Current;
            while (cursor.MoveNext() && i < Count)
            {
                i++;
                values[i] = cursor.Current;
            }
            cursor.Dispose();
            return values;
        }

        /// <summary>
        /// Mètode que compara la capacitat de la pila amb la mova capacitat donada com a paràmetre, i si aquesta és més petita a
        /// la nova capacitat, augmenta la capacitat de la pila. Si la pila té més espai que la nova capacitat donada, la pila no
        /// canvia
        /// </summary>
        /// <param name="newCapacity">Quantitat d'espai que ha de tenir la pila </param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException">L'excepció salta si la nova capacitat és menor a 0</exception>
        public int EnsureCapacity(int newCapacity)
        {
            if (newCapacity < 0) throw new ArgumentOutOfRangeException();

            int finalCapacity = this.Count;
            if (newCapacity > this.Count)
            {
                T[] newData = new T[newCapacity];
                for (int i = 0; i < this.Count; i++)
                {
                    newData[i] = this.data[i];
                }
                finalCapacity = newData.Length;
                data = newData;
            }
            return finalCapacity;
        }

        /// <summary>
        /// Mètode que produeix un string amb els elements de la pila
        /// </summary>
        /// <returns>La llista dels elements dins la pila en format string</returns>
        public override string ToString()
        {
            IEnumerator<T> cursor = new EnumeradorPila(data, top);
            StringBuilder sB = new StringBuilder("[ " + cursor.Current + ", ");
            while (cursor.MoveNext())
            {
                sB.Append(cursor.Current + ", ");
            }
            cursor.Dispose();
            sB[sB.Length - 2] = ' ';
            sB[sB.Length - 1] = ']';
            return sB.ToString();
        }

        /// <summary>
        /// Mètode públic sobreescrit que compara la pila actual i una segona pila, a través d'un mètode privat homonim,
        /// assegurant que les dues piles són comparables en tots els aspectes.
        /// </summary>
        /// <param name="obj">Pila comparada amb la pila actual</param>
        /// <returns>Retorna "true" si les dues piles són iguals (segons les especificacions del mètode privat Equals), "false" en cas contrari</returns>
        public override bool Equals(object obj)
        {
            bool igual;
            if (ReferenceEquals(null, obj)) igual = false;
            else if (ReferenceEquals(this, obj)) igual = true;
            else if (obj.GetType() != this.GetType()) igual = false;
            else igual = Equals((Pila<T>)obj);
            return igual;
        }

        private bool Equals(Pila<T> obj)
        {
            bool pilesIgauls = true;
            if (this.Count == obj.Count)
            {
                int i = 0;
                while (pilesIgauls && i < this.Count)
                {
                    if (!this[i].Equals(obj[i])) pilesIgauls = false;
                    else i++;
                }
            }
            else pilesIgauls = false;
            return pilesIgauls;
        }

        //Interface Methods
        /// <summary>
        /// Mètode heretat de la interficie ICollection<T>, amb la mateixa funcionalitat teòrica que el mètode Push (que és cridat aquí dintre)
        /// </summary>
        /// <param name="item">Element a afegir dins la pila</param>
        /// <exception cref="NotSupportedException">L'excepció salta si la pila és només de lectura</exception>
        public void Add(T item)
        {
            if (IsReadOnly) throw new NotSupportedException();

            Push(item);
        }

        /// <summary>
        /// Mètode heretat de la interficie ICollection<T>, que buida la pila en una sola acció
        /// </summary>
        /// <exception cref="NotSupportedException">L'excepció salta si la pila és només de lectura</exception>
        public void Clear()
        {
            if (IsReadOnly) throw new NotSupportedException();

            data = null;
        }

        /// <summary>
        /// Mètode heretat de la interficie ICollection<T>, que cerca si l'element passat per paràmetre es troba dins la pila
        /// </summary>
        /// <param name="item">Element a cercar dins la pila</param>
        /// <returns>Retorna "true" si "item" està dins la pila, "false" en cas contrari</returns>
        public bool Contains(T item)
        {
            bool contains = false;
            int nElem = top;
            T element = data[nElem];
            while (!contains)
            {
                if (item.Equals(element)) contains = true;
                else
                {
                    nElem--;
                    element = data[nElem];
                }
            }
            return contains;
        }

        /// <summary>
        /// Mètode heretat de la interficie ICollection<T>, que copia la pila dins un array passat com a paràmetre,
        /// des de l'index especificat (també com a paràmetre). Si l'array és nul, l'índex és negatiu o l'array és massa
        /// petit, saltarà excepció
        /// </summary>
        /// <param name="array">Array on es copiaran els elements dins la pila</param>
        /// <param name="arrayIndex">Índex d'inici de la copia dins l'array</param>
        /// <exception cref="ArgumentNullException">L'excepció salta si l'array donat és nul</exception>
        /// <exception cref="ArgumentOutOfRangeException">L'excepció salta si l'índex d'inici de la copia és més petit que 0</exception>
        /// <exception cref="ArgumentException">L'excepció salta si la pila té més elements que l'espai donat per fer la copia dins l'array</exception>
        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null) throw new ArgumentNullException();
            else if (arrayIndex < 0) throw new ArgumentOutOfRangeException();
            else if (top > array.Length - arrayIndex) throw new ArgumentException();
            IEnumerator<T> cursor = new EnumeradorPila(data, top);
            while (cursor.MoveNext())
            {
                array[arrayIndex] = cursor.Current;
                arrayIndex++;
            }
            cursor.Dispose();
        }

        /// <summary>
        /// Mètode heretat de la interficie IEnumerable<T>, que proporciona un cursor a través de la subclasse EnumeradorPila
        /// </summary>
        /// <returns>Un cursor amb el qual podem recorrer la pila</returns>
        public IEnumerator<T> GetEnumerator()
        {
            return new EnumeradorPila(data, top);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Mètode heretat de la interficie ICollection<T>, que, revisant si l'element passat com a paràmetre és el primer de la pila,
        /// l'elimina d'aquesta cridant el mètode Pop(), retornant un valor booleà depenguent de si s'ha pogut eliminar o no.
        /// </summary>
        /// <param name="item">Element que s'hauria d'eliminar de la pila</param>
        /// <returns>Retorna "true" si "item" s'ha tret de la pila, "false" en cas contrari</returns>
        /// <exception cref="NotSupportedException">L'excepció salta si la pila és només de lectura</exception>
        public bool Remove(T item)
        {
            if (IsReadOnly) throw new NotSupportedException();

            bool removed = false;
            T topElement = Peek();
            if (topElement.Equals(item))
            {
                Pop();
                removed = true;
            }
            return removed;
        }

        //SubClasses
        /// <summary>
        /// Subclasse de la classe pila, utilitzada per generar cursors de recorregut de la pila
        /// </summary>
        public class EnumeradorPila : IEnumerator<T>
        {
            const int BOTTOM_LIMMIT = -1;
            private int topElement;
            private T[] values;

            /// <summary>
            /// Constructor del cursor de la pila
            /// </summary>
            /// <param name="data">Taula amb els valors de la pila</param>
            /// <param name="top">Nombre d'elements de la pila, i quin és el primer de tots</param>
            public EnumeradorPila(T[] data, int top)
            {
                this.values = data;
                this.topElement = top;
            }

            /// <summary>
            /// Propietat que ens proporciona el valor actual de la pila
            /// </summary>
            public T Current
            {
                get
                {
                    if (topElement == BOTTOM_LIMMIT || topElement == values.Length) throw new Exception("OUT OF RANGE");

                    return values[topElement];
                }
            }

            /// <summary>
            /// Objecte propietat de retorna el valor de la propietat Current
            /// </summary>
            object IEnumerator.Current
            {
                get
                {
                    return Current;
                }
            }

            /// <summary>
            /// Mètode que utilitzem per destruir el cursor
            /// </summary>
            public void Dispose()
            {
                this.values = null;
            }

            /// <summary>
            /// Mètode que ens indica si el cursor pot accedir el següent element de la pila, efectuant l'acció
            /// </summary>
            /// <returns>Retorna "true" si ens podem moure dins la pila, "false" en cas contrari (top == -1)</returns>
            public bool MoveNext()
            {
                bool thereNext = true;
                topElement--;
                if (topElement == BOTTOM_LIMMIT) thereNext = false;
                return thereNext;
            }

            /// <summary>
            /// Mètode que reinicia el cursor
            /// </summary>
            public void Reset()
            {
                this.topElement = values.Length - 1;
            }
        }
    }
}
