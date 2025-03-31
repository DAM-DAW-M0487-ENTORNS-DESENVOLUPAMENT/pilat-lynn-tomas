using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entorns___TaulaLlista
{
    public class TaulaLlista<T> : IEnumerable<T>, ICollection<T>, IList<T>
    {
        public const int DEFAULT_SIZE = 10;
        private T[] dades;
        private int nElem;


        #region Properties

        /// <summary>
        /// Property that counts the number of elements in the array
        /// </summary>
        public int Count
        {
            get { return nElem; }
        }


        /// <summary>
        /// Property that dictates if the file is read-only
        /// CURRENTLY SET TO FALSE FOR EASE OF CODING
        /// </summary>
        public bool IsReadOnly
        {
            get { return false; }
        }



        /// <summary>
        /// Property that allows accessing or modifying the element at the specified index in the array.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException">If the array's index is out of range</exception>
        public T this[int index] //REVIEW
        {
            get
            {
                if (index < 0 || index >= nElem)
                {
                    throw new ArgumentOutOfRangeException(nameof(index), "THE ARRAY'S INDEX IS OUT OF RANGE");
                }

                return dades[index];
            }

            set
            {
                if (IsReadOnly)
                {
                    throw new NotSupportedException("CANNOT EDIT A READ-ONLY FILE.");
                }

                if (index < 0 || index >= nElem)
                {
                    throw new ArgumentOutOfRangeException(nameof(index), "THE ARRAY'S INDEX IS OUT OF RANGE.");
                }

                dades[index] = value;
            }
        }

        #endregion



        #region Constructors
        public TaulaLlista(int capacitatInicial)
        {
            dades = new T[capacitatInicial];
            nElem = 0;
        }


        public TaulaLlista()
        {
            dades = new T[DEFAULT_SIZE];
            nElem = 0;
        }

        #region ALTERNATIVE WRITING FOR PREVIOUS CONSTRUCTOR
        //public TaulaLlista() : this(DEFAULT_SIZE)
        //{
        //      code
        //}
        #endregion


        public TaulaLlista(TaulaLlista<T> TaulaLlista2)
        {
            //TaulaLlista2 is the original array that we are copying


            this.dades = new T[TaulaLlista2.dades.Length];
            this.nElem = TaulaLlista2.nElem;

            for (int i = 0; i < nElem; i++)
            {
                this.dades[i] = TaulaLlista2.dades[i];
            }

            #region foreach + GetEnumerable EXPLANATION CODE
            //int i= 0;

            //foreach (T element in TaulaLlista2.dades)
            //{
            //    this.dades[i] = TaulaLlista2.dades[i];
            //    i++;
            //}
            #endregion
        }

        #endregion



        #region IEnumerator

        public IEnumerator<T> GetEnumerator()
        {
            //for (int i = 0; i < this.nElem; i++)
            //    yield return this.dades[i];


            for (int i = 0; i < this.nElem; i++)
                yield return this.dades[i];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator(); //custom enumerator - MoveNext...
        }

        #endregion



        #region ICollection

        /// <summary>
        /// Aquest mètode afegeix un nou element de tipus genèric T a la següent posició lliure de
        /// l'array (nElem). Si l'array està ple, primer s'ha de duplicar la seva capacitat utilitzant la metode DuplicaCapacitat
        /// </summary>
        /// <param name="item"></param>
        public void Add(T item)
        {

            if (item == null)
            {
                throw new ArgumentNullException(nameof(item), "THE ITEM CANNOT BE NULL");
            }

            if (IsReadOnly)
            {
                throw new NotSupportedException("CANNOT EDIT A READ-ONLY FILE.");
            }


            if (nElem == dades.Length)
            {
                DuplicaCapacitat();
            }

            else
            {
                dades[nElem] = item;
                nElem++;
            }
        }


        /// <summary>
        /// A METHOD THAT CHECKS IF THE DADES ARRAY CONTAINS A SPECIFIC ITEM 
        /// Uses the Equals method to make a traversal
        /// </summary>
        /// <param name="item"></param>
        /// <returns>True if the item is found in the array, otherwise returns false</returns>
        public bool Contains(T item)
        {
            int i = 0;
            bool found = false;

            while (!found && i < nElem)
            {
                if (Equals(dades[i], item))
                {
                    found = true;
                }
                i++;
            }

            return found;
        }


        /// <summary>
        /// Elimina la primera coincidència de l'element genèric T que apareix a l'array
        /// </summary>
        /// <param name="item"></param>
        /// <returns>Retorna un valor bool que ens informarà si hem pogut eliminar l’element passat per paràmetre.</returns>
        /// <exception cref="ArgumentNullException">Item can't be null</exception>
        /// <exception cref="NotSupportedException">Read-only files cannot be edited</exception>
        public bool Remove(T item)
        {

            if (item == null)
            {
                throw new ArgumentNullException(nameof(item), "THE ITEM CANNOT BE NULL");
            }

            if (IsReadOnly)
            {
                throw new NotSupportedException("CANNOT EDIT A READ-ONLY FILE.");
            }


            bool found = false;
            int indexRemove = 0;

            while (!found && indexRemove < nElem)
            {
                if (item.Equals(dades[indexRemove]))
                {
                    found = true;
                }

                else
                {
                    indexRemove++;
                }
            }

            if (found)
            {
                for (int i = indexRemove; i < nElem - 1; i++)
                {
                    dades[i] = dades[i + 1];
                }

                dades[nElem - 1] = default(T);
                nElem--;

            }

            return found;
        }


        /// <summary>
        /// METHOD TO CLEAR AND REINITIALIZE THE ARRAY 
        /// </summary>
        public void Clear()
        {
            if (IsReadOnly)
            {
                throw new NotSupportedException("CANNOT EDIT A READ-ONLY FILE.");
            }

            Array.Clear(dades, 0, nElem - 1); //REINITIALIZE THE ARRAY

            nElem = 0;
        }


        /// <summary>
        /// A METHOD TO COPY ALL THE ELEMENTS FROM ONE ARRAY TO ANOTHER
        /// </summary>
        /// <param name="array"></param>
        /// <param name="arrayIndex"></param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array), "THE ARRAY YOU'RE COPYING TO CANNOT BE NULL");
            }

            if (arrayIndex < 0 || arrayIndex >= array.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(arrayIndex), "THE ARRAY'S INDEX IS OUT OF RANGE");
            }

            if (array.Length - arrayIndex < nElem)
            {
                throw new ArgumentException("NOT ENOUGH SPACE IN THE ARRAY YOU'RE COPYING TO");
            }


            for (int i = 0; i < nElem; i++)
            {
                array[arrayIndex + i] = dades[i];       //arrayIndex + i ensures that the information copied to the new array is placed correctly
            }
        }

        #endregion






        #region IList

        /// <summary>
        /// A method that searches for the first instance of a specific item.
        /// Retorna la primera posició on es troba l'element passat com a paràmetre (item). Si no es troba, retorna -1.
        /// </summary>
        /// <param name="item"></param>
        /// <returns>The first instance of a specific item</returns>
        public int IndexOf(T item)
        {
            bool found = false;
            int index = 0;
            int pos = -1;

            while (!found && index < nElem)
            {
                if (Equals(dades[index], item))
                {
                    found = true;
                }

                else
                {
                    index++;
                }
            }

            if (found)
            {
                pos = index;
            }
            return pos;
        }


        /// <summary>
        /// Insereix l'element item a la posició index especificada. Per fer-ho, el mètode desplaça els elements existents una posició cap a la dreta per fer espai per al nou element.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item"></param>
        /// <exception cref="ArgumentOutOfRangeException">If the array's index is out of range</exception>
        /// <exception cref="NotSupportedException">Cannot edit a read-only file</exception>
        public void Insert(int index, T item)
        {
            if (index < 0 || index > nElem)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "THE ARRAY'S INDEX IS OUT OF RANGE.");
            }

            if (IsReadOnly)
            {
                throw new NotSupportedException("CANNOT EDIT A READ-ONLY FILE");
            }


            if (nElem == dades.Length)
            {
                DuplicaCapacitat();
            }


            for (int i = nElem; i > index; i--) //We have to loop backwards from nElem to free up space to insert the new item
            {
                dades[i] = dades[i - 1];
            }

            dades[index] = item; //Inserting the item

            nElem++;
        }


        /// <summary>
        /// Elimina l'element situat a la posició index. Pots reutilitzar el codi del mètode Remove implementat a la interfície ICollection<T>.
        /// </summary>
        /// <param name="index"></param>
        /// <exception cref="ArgumentOutOfRangeException">If the array's index is out of range</exception>
        /// <exception cref="NotSupportedException">Cannot edit a read-only file</exception>
        public void RemoveAt(int index)
        {
            if (index < 0 || index >= nElem)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "THE ARRAY'S INDEX IS OUT OF RANGE.");
            }

            if (IsReadOnly)
            {
                throw new NotSupportedException("CANNOT EDIT A READ-ONLY FILE");
            }


            for (int i = index; i < nElem - 1; i++) //Similar to deleting an item in project Inventari
            {
                dades[i] = dades[i + 1];
            }

            nElem--;
        }

        #endregion







        #region ADDITIONAL METHODS

        /// <summary>
        /// MODIFICA L'ARRAY "dades" DUPLICANT EL SEU LENGTH I COPIANT ELS ELEMENTS SOBRE L'ARRAY DUPLICAT
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void DuplicaCapacitat()
        {
            T[] dadesDuplicated = new T[dades.Length * 2];

            for (int i = 0; i < nElem; i++)
            {
                dadesDuplicated[i] = dades[i];
            }

            dades = dadesDuplicated;
        }



        /// <summary>
        /// METHOD THAT ADDS AN ELEMENT TO THE ARRAY
        /// </summary>
        /// <param name="dada"></param>
        public void Afegir(T dada)
        {
            if (nElem == dades.Length)
            {
                DuplicaCapacitat();
            }

            dades[nElem] = dada;
            nElem++;
        }



        /// <summary>
        /// A method that checks if 2 items in the array are equal to eachother
        /// </summary>
        /// <param name="item1"></param>
        /// <param name="item2"></param>
        /// <returns>True if the items are the same</returns>
        private bool Equals(T item1, T item2)
        {

            return EqualityComparer<T>.Default.Equals(item1, item2);

        }



        /// <summary>
        /// Returns the items in the array in the form of a string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[");

            for (int i = 0; i < nElem; i++)
            {
                sb.Append(dades[i]);

                if (i < nElem - 1)
                {
                    sb.Append(", ");
                }
            }

            sb.Append("]");
            return sb.ToString();
        }

        #endregion

    }
}
