namespace Entorns___TaulaLlista
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TaulaLlista<int> t = new TaulaLlista<int>(50);

            t.Afegir(55);
            t.Afegir(66);
            t.Afegir(77);
            t.Afegir(88);


            Console.WriteLine(" ");
            Console.WriteLine("TESTING CODE");
            Console.WriteLine(" ");

            Console.WriteLine(t.ToString());


            ConsoleKeyInfo input;
            do
            {
                ShowMenu();
                input = Console.ReadKey();

                switch (input.Key)
                {
                    case ConsoleKey.D1:
                        DoAddItem(t);
                        break;

                    case ConsoleKey.D2:
                        DoCheckItem(t);
                        break;

                    case ConsoleKey.D3:
                        DoRemoveItem(t);
                        break;

                    case ConsoleKey.D4:
                        DoClearList(t);
                        break;

                    case ConsoleKey.D5:
                        DoDisplayContent(t);
                        break;

                    case ConsoleKey.D6:
                        DoCopyTo(t);
                        break;

                    case ConsoleKey.D7:
                        DoIndexOf(t);
                        break;

                    case ConsoleKey.D8:
                        DoInsert(t);
                        break;

                    case ConsoleKey.D9:
                        DoRemoveAt(t);
                        break;

                    default:
                        Console.WriteLine("INVALID SELECTION.");
                        break;
                }
            }

            while (input.Key != ConsoleKey.D0);
        }

        /// <summary>
        /// Menu for each function in the program - each number corresponds to an option the user chooses.
        /// </summary>
        public static void ShowMenu()
        {
            Console.WriteLine("1. ADD AN ITEM TO THE ARRAY");
            Console.WriteLine("2. CHECK IF AN ITEM IS IN THE ARRAY");
            Console.WriteLine("3. REMOVE AN ITEM FROM THE ARRAY");
            Console.WriteLine("4. CLEAR THE ARRAY");
            Console.WriteLine("5. DISPLAY ALL ITEMS IN THE ARRAY");
            Console.WriteLine("6. COPY TO A NEW ARRAY");
            Console.WriteLine("7. FIND THE INDEX OF AN ITEM");
            Console.WriteLine("8. INSERT AN ITEM IN WHATEVER POSITION IN THE ARRAY");
            Console.WriteLine("9. REMOVE AN ITEM AT A SPECIFIC INDEX IN THE ARRAY");
            Console.WriteLine("0. EXIT");
            Console.WriteLine(" ");
        }

        /// <summary>
        /// Method to apply the Add method from ICollection.
        /// </summary>
        /// <param name="t"></param>
        public static void DoAddItem(TaulaLlista<int> t)
        {
            Console.Write("ENTER THE NUMBER YOU WANT TO ADD TO THE LIST ==> ");

            string input = Console.ReadLine();

            try
            {
                int addItem = Convert.ToInt32(input);

                t.Add(addItem);

                Console.WriteLine($"THE ITEM '{addItem}' WAS SUCCESSFULLY ADDED.");
            }
            catch (FormatException)
            {
                Console.WriteLine("INPUT INVALID. PLEASE TRY AGAIN.");
            }
        }



        /// <summary>
        /// Method to apply the Contains method from ICollection.
        /// </summary>
        /// <param name="t"></param>
        public static void DoCheckItem(TaulaLlista<int> t)
        {
            Console.Write("ENTER THE ITEM TO CHECK ==> ");

            string input = Console.ReadLine();

            int item;

            if (input == null)
            {
                item = 0;
            }
            else
            {
                item = Convert.ToInt32(input);
            }


            if (t.Contains(item))
            {
                Console.WriteLine($"THE ITEM '{item}' EXISTS IN THE ARRAY.");
            }
            else
            {
                Console.WriteLine($"THE ITEM '{item}' DOES NOT EXIST IN THE ARRAY.");
            }
        }



        /// <summary>
        /// Method to apply the Remove method from ICollection.
        /// </summary>
        /// <param name="t"></param>
        public static void DoRemoveItem(TaulaLlista<int> t)
        {
            Console.Write("ENTER THE ITEM YOU WANT TO REMOVE ==> ");

            string input = Console.ReadLine();

            int item;

            if (input == null)
            {
                item = 0;
            }
            else
            {
                item = Convert.ToInt32(input);
            }


            if (t.Remove(item))
            {
                Console.WriteLine($"THE ITEM '{item}' HAS BEEN SUCCESSFULLY REMOVED");
            }
            else
            {
                Console.WriteLine($"THE ITEM '{item}' WAS NOT FOUND");
            }
        }



        /// <summary>
        /// Method to apply the Clear method from ICollection.
        /// </summary>
        /// <param name="t"></param>
        public static void DoClearList(TaulaLlista<int> t)
        {
            t.Clear();

            Console.WriteLine("THE ARRAY HAS BEEN CLEARED");
        }



        /// <summary>
        /// Method to apply the CopyTo method from ICollection.
        /// </summary>
        /// <param name="t"></param>
        public static void DoCopyTo(TaulaLlista<int> t)
        {
            Console.Write("ENTER THE SIZE OF THE DESTINATION ARRAY ==> ");

            string sizeInput = Console.ReadLine();

            int size;

            try
            {
                size = Convert.ToInt32(sizeInput);
            }

            catch (FormatException)
            {
                Console.WriteLine("INVALID SIZE. PLEASE ENTER A POSITIVE INTEGER.");
                return;
            }

            if (size <= 0)
            {
                Console.WriteLine("INVALID SIZE. PLEASE ENTER A POSITIVE INTEGER.");
                return;
            }

            Console.Write("ENTER THE STARTING INDEX FOR COPYING ==> ");

            string indexInput = Console.ReadLine();

            int startIndex;

            try
            {
                startIndex = Convert.ToInt32(indexInput);
            }

            catch (FormatException)
            {
                Console.WriteLine("INVALID INDEX. PLEASE ENTER A VALID INTEGER.");
                return;
            }

            int[] newArray = new int[size];

            try
            {
                t.CopyTo(newArray, startIndex);

                Console.WriteLine("ARRAY SUCCESSFULLY COPIED.");

                Console.Write("COPIED CONTENT: ");

                foreach (var item in newArray)
                {
                    Console.Write(item + " ");
                }
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
            }
        }









        /// <summary>
        /// Method to apply the IndexOf method from IList.
        /// </summary>
        /// <param name="t"></param>
        public static void DoIndexOf(TaulaLlista<int> t)
        {
            Console.Write("ENTER THE ITEM TO SEARCH FOR ==> ");

            string input = Console.ReadLine();

            int item;

            try
            {
                item = Convert.ToInt32(input);
            }

            catch (FormatException)
            {
                Console.WriteLine("INVALID INPUT. PLEASE ENTER A NUMBER.");
                return;
            }

            int index = t.IndexOf(item);

            if (index != -1)
            {
                Console.WriteLine($"THE ITEM '{item}' WAS FOUND AT INDEX {index}.");
            }

            else
            {
                Console.WriteLine($"THE ITEM '{item}' WAS NOT FOUND IN THE ARRAY.");
            }
        }



        /// <summary>
        /// Method to apply the Insert method from IList.
        /// </summary>
        /// <param name="t"></param>
        public static void DoInsert(TaulaLlista<int> t)
        {
            Console.Write("ENTER THE INDEX TO INSERT AT ==> ");

            string indexInput = Console.ReadLine();

            Console.Write("ENTER THE ITEM TO INSERT ==> ");

            string itemInput = Console.ReadLine();


            int index = 0;

            int item = 0;

            try
            {
                index = Convert.ToInt32(indexInput);

                item = Convert.ToInt32(itemInput);
            }
            catch (FormatException)
            {
                Console.WriteLine("INVALID INPUT. PLEASE ENTER A VALID INTEGER.");
                return;
            }



            try
            {
                t.Insert(index, item);

                Console.WriteLine($"THE ITEM '{item}' HAS BEEN INSERTED AT INDEX {index}.");
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("INVALID INDEX. PLEASE TRY AGAIN.");
            }
            catch (NotSupportedException)
            {
                Console.WriteLine("CANNOT EDIT A READ-ONLY LIST.");
            }
        }



        /// <summary>
        /// Method to apply the RemoveAt method from IList.
        /// </summary>
        /// <param name="t"></param>
        public static void DoRemoveAt(TaulaLlista<int> t)
        {
            Console.Write("ENTER THE INDEX OF THE ITEM YOU WANT TO REMOVE ==> ");

            string input = Console.ReadLine();

            int index = 0;

            bool isValidIndex = false;

            try
            {
                index = Convert.ToInt32(input);

                isValidIndex = true;
            }

            catch (FormatException)
            {
                Console.WriteLine("INVALID INPUT. PLEASE ENTER A VALID INTEGER.");
                return;
            }

            if (isValidIndex)
            {
                try
                {
                    t.RemoveAt(index);

                    Console.WriteLine($"THE ITEM AT INDEX '{index}' HAS BEEN SUCCESSFULLY REMOVED.");
                }

                catch (ArgumentOutOfRangeException)
                {
                    Console.WriteLine($"THE INDEX '{index}' IS OUT OF RANGE. PLEASE TRY AGAIN.");
                }

                catch (Exception ex)
                {
                    Console.WriteLine($"AN ERROR OCCURRED: {ex.Message}");
                }
            }

        }








        /// <summary>
        /// Method to apply the ToString method
        /// </summary>
        /// <param name="t"></param>
        public static void DoDisplayContent(TaulaLlista<int> t)
        {
            Console.WriteLine("DISPLAYING ALL ITEMS IN THE ARRAY:");

            Console.WriteLine(t.ToString());
        }


    }
}
