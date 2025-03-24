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
        const int DEFAULT_SIZE = 8;
        private T[] data;
        private int top = -1;

        //Interface Propeties
        public int Count { get { return top + 1; } }

        public bool IsReadOnly {  get { return false; } }

        //Builders
        /// <summary>
        /// Constructor que proporciona una Pila de tamany estandar usant la constant DEFAULT_SIZE
        /// </summary>
        public Pila(): this(DEFAULT_SIZE) { }

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
        public Pila(T[] array)
        {
            data = new T[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                data[i] = array[i];
            }
            top = data.Length - 1;
        }

        //Class Methods
        public T Pop()
        {
            if (this.top == -1) throw new InvalidOperationException();

            T topElement = data[top];
            T[] newData = new T[data.Length - 1];
            for (int i = 0; i < top; i++)
            {
                newData[i] = data[i];
            }
            data = newData;
            top--;
            return topElement;
        }

        public T Peek()
        {
            if (this.top == -1) throw new InvalidOperationException();

            return data[this.top];
        }

        public T[] ToArray()
        {
            T[] values = new T[data.Length];
            IEnumerator<T> ptr = new EnumeradorPila(data, top);
            foreach (T item in data)
            {
                int i = 0;
                if (ptr.Current.Equals(data[0]))
                {
                    values[i] = item;
                }
                else
                {
                    values[i] = item;
                    i++;
                }
                ptr.MoveNext();
            }
            ptr.Dispose();
            return values;
        }

        public int EnsureCapacity(int newCapacity)
        {
            if (newCapacity < 0) throw new ArgumentOutOfRangeException();

            int finalCapacity = this.Count;
            if (newCapacity > this.top)
            {
                T[] newData = new T[newCapacity];
                IEnumerator<T> ptr = new EnumeradorPila(data, top);
                foreach (T item in data)
                {
                    int counter = 0;
                    newData[counter] = item;
                    counter++;
                    ptr.MoveNext();
                }
                ptr.Dispose();
                data = newData;
                finalCapacity = newCapacity;
            }
            return finalCapacity;
        }

        public override string ToString()
        {
            StringBuilder sB = new StringBuilder("[ ");
            IEnumerator<T> ptr = new EnumeradorPila(data, top);
            foreach (T i in data)
            {
                if (ptr.Current.Equals(data[0]))
                {
                    sB.Append(ptr.Current);
                }
                else
                {
                    sB.Append(ptr.Current + ", ");
                }
                ptr.MoveNext();
            }
            ptr.Dispose();
            sB.Append(" ]");
            return sB.ToString();
        }

        public override bool Equals(object obj)
        {
            bool igual;
            if (ReferenceEquals(null, obj)) igual = false;
            else if (ReferenceEquals(this, obj)) igual = true;
            else if (obj.GetType() != this.GetType()) igual = false;
            else igual = Equals((T)obj);
            return igual;
        }

        private bool Equals(T obj)
        {
            T thisObj = data[top];
            bool igual = false;
            if (thisObj.Equals(obj)) igual = true;
            return igual;
        }

        //Interface Methods
        public void Add(T item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(T item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public bool Remove(T item)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        //SubClasses
        public class EnumeradorPila : IEnumerator<T>
        {
            const int BOTTOM_LIMMIT = -1;
            private int topElement;
            private T[] values;

            public EnumeradorPila(T[] data, int top)
            {
                this.values = data;
                this.topElement = top;
            }

            public T Current
            {
                get
                {
                    if (topElement == BOTTOM_LIMMIT || topElement == values.Length) throw new Exception("OUT OF RANGE");

                    return values[topElement];
                }
            }

            object IEnumerator.Current
            {
                get
                {
                    return Current;
                }
            }

            public void Dispose()
            {
                this.values = null;
            }

            public bool MoveNext()
            {
                bool thereNext = true;
                topElement--;
                if (topElement == BOTTOM_LIMMIT) thereNext = false;
                return thereNext;
            }

            public void Reset()
            {
                this.topElement = values.Length - 1;
            }
        }
    }
}
