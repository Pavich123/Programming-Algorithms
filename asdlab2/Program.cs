using System;
using System.Text;
using System.Diagnostics;
namespace lab1
{
    public class Node
    {
        public int data;
        public Node next;

        public Node(int d)
        {
            data = d;
            next = null;
        }

        public void Print()
        {
            Console.Write(data + " ");
            if (next != null)
            {
                next.Print();
            }
        }

        public void AddToEnd(int data)
        {
            if (next == null)
            {
                next = new Node(data);
            }
            else
            {
                next.AddToEnd(data);
            }
        }
    }
    /// <summary>
    /// клас простору
    /// </summary>
    class Program
    {
        public static int kolvo;
        public static int kolvo1;
        public static int lim1;
        public static int lim2;
        /// <summary>
        ///  головна функція = диспетчер меню
        /// </summary>
        /// <param name="args">рядок для передачи значень у програму ззовні</param>
        static void Main()
        {
            Console.OutputEncoding = UTF8Encoding.UTF8;
            Console.WriteLine("Lab2, Pavlyuchenko Artem, pavlyuchenko0209@gmail.com");
            Menu();
        }
        /// <summary>
        ///  зображення меню команд програми
        /// </summary>
        static void Menu()
        {
            char command;
            char key;
            do
            {
                Console.WriteLine("==============================================");
                Console.WriteLine("Menu of command");
                Console.WriteLine("1 if you want to work with array");
                Console.WriteLine("2 if you want to work with linked list");
                Console.WriteLine();
                Console.WriteLine("select of command, press number of key");
                Console.WriteLine("==============================================");
                command = (char)Console.Read(); // вибір команди меню
                Console.ReadLine();
                switch (command)
                {
                    case '1': Task1(); break;
                    case '2': Task2(); break;
                    default: Console.WriteLine("wrong command"); break;
                }
                Console.WriteLine("Continue y/n");
                key = (char)Console.Read(); //ввід ключа продовження циклу
                Console.ReadLine();
            } while (key != 'n');

        } //end of Menu()
        /// <summary>
        /// генерація масиву
        /// </summary>
        static void Task1()
        {
            Console.WriteLine("Enter the number of values in your massive: ");
            kolvo = Convert.ToInt32(Console.ReadLine());
            int[] numbers = new int[kolvo];
            Console.WriteLine("Enter the limits of values in your massive: ");
            lim1 = Convert.ToInt32(Console.ReadLine());
            lim2 = Convert.ToInt32(Console.ReadLine());
            int lim3 = lim2 - lim1;
            if (lim2 < lim1) Console.WriteLine("Error");
            else
            {
                Console.WriteLine("Your massive: ");
                GeneratingMassive(lim1, lim3, numbers);
                OutputMassive(numbers);
            }
            char command;
            char key;
            do
            {
                Console.WriteLine("==============================================");
                Console.WriteLine("Menu of command");
                Console.WriteLine("1 Insert sorting");
                Console.WriteLine("2 Selection sorting");
                Console.WriteLine("3 Merge sorting"); 
                Console.WriteLine("select of command, press number of key");
                Console.WriteLine("==============================================");
                command = (char)Console.Read(); // вибір команди меню
                Console.ReadLine();
                switch (command)
                {
                    case '1': InsertionSort(numbers); break;
                    case '2': SelectionSort(numbers); break;
                    default: Console.WriteLine("wrong command"); break;
                }
                Console.WriteLine("Continue y/n");
                key = (char)Console.Read(); //ввід ключа продовження циклу
                Console.ReadLine();
            } while (key != 'n');
        }
        static void InsertionSort(int[] items)
        {
            Stopwatch sWatch = new Stopwatch();
            sWatch.Start();
            int sortedRangeEndIndex = 1;
            while (sortedRangeEndIndex < items.Length)
            {
                if (items[sortedRangeEndIndex].CompareTo(items[sortedRangeEndIndex - 1]) < 0)
                {
                    int insertIndex = FindInsertionIndex(items, items[sortedRangeEndIndex]);
                    Insert(items, insertIndex, sortedRangeEndIndex);

                }
                sortedRangeEndIndex++;
            }
            Console.WriteLine("Your massive after sorting: ");
            OutputMassive(items);
            sWatch.Stop();
            Console.WriteLine("Duration " + sWatch.ElapsedMilliseconds + "ms");

        }
        static int FindInsertionIndex(int[] items, int valueToInsert)
        {
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i].CompareTo(valueToInsert) > 0)
                {
                    return i;
                }
            }
            throw new InvalidOperationException("Index wasn`t found");
        }
        static void Insert(int[] itemsArray, int indexInsertingAt, int indexInsertingFrom)
        {
            int temp = itemsArray[indexInsertingAt];
            itemsArray[indexInsertingAt] = itemsArray[indexInsertingFrom];
            for (int current = indexInsertingFrom; current > indexInsertingAt; current--)
            {
                itemsArray[current] = itemsArray[current - 1];
            }
            itemsArray[indexInsertingAt + 1] = temp;
        }

        static void Swap(int[] items, int left, int right)
        {
            if (left != right)
            {
                int temp = items[left];
                items[left] = items[right];
                items[right] = temp;
            }
        }

        static void SelectionSort(int[] items)
        {
            Stopwatch sWatch = new Stopwatch();
            sWatch.Start();
            int sortedRangeEnd = 0;
            while (sortedRangeEnd < items.Length)
            {
                int nextIndex = FindIndexOfSmallestFromIndex(items, sortedRangeEnd);
                Swap(items, sortedRangeEnd, nextIndex);
                sortedRangeEnd++;
            }
            Console.WriteLine("Your massive after sorting: ");
            OutputMassive(items);
            sWatch.Stop();
            Console.WriteLine("Duration " + sWatch.ElapsedMilliseconds + "ms");
        }

        static int FindIndexOfSmallestFromIndex(int[] items, int sortedRangeEnd)
        {
            int currentSmallest = items[sortedRangeEnd];
            int currentSmallestIndex = sortedRangeEnd;
            for (int i = sortedRangeEnd+1; i < items.Length; i++)
            {
                if (currentSmallest.CompareTo(items[i]) > 0)
                {
                    currentSmallest = items[i];
                    currentSmallestIndex = i;
                }
            }
            return currentSmallestIndex;
        }




        





        static void InsertionSort1(Node node)
        {
            Stopwatch sWatch = new Stopwatch();
            sWatch.Start();
            int sortedRangeEndIndex = 1;
            while (sortedRangeEndIndex < kolvo1)
            {
                if (GetElement(node,sortedRangeEndIndex).CompareTo(GetElement(node, sortedRangeEndIndex-1)) < 0)
                {
                    int insertIndex = FindInsertionIndex1(node, GetElement(node, sortedRangeEndIndex));
                    Insert1(node, insertIndex, sortedRangeEndIndex);
                }
                sortedRangeEndIndex++;
            }
            Console.WriteLine("Your massive after sorting: ");
            node.Print();
            Console.WriteLine();
            sWatch.Stop();
            Console.WriteLine("Duration " + sWatch.ElapsedMilliseconds + "ms");

        }
        static int FindInsertionIndex1(Node node, int valueToInsert)
        {
            for (int i = 0; i < kolvo1; i++)
            {
                if (GetElement(node, i).CompareTo(valueToInsert) > 0)
                {
                    return i;
                }
            }
            throw new InvalidOperationException("Index wasn`t found");
        }
        static void Insert1(Node node1, int indexInsertingAt, int indexInsertingFrom)
        {
            int temp = GetElement(node1, indexInsertingAt);

            GetPos(node1, indexInsertingAt).data = GetElement(node1, indexInsertingFrom);
            for (int current = indexInsertingFrom; current > indexInsertingAt; current--)
            {
                GetPos(node1, current).data = GetElement(node1, current-1);
            }
            GetPos(node1, indexInsertingAt+1).data = temp;
        }


        static void Swap1(Node node, int left, int right)
        {
            if (left != right)
            {
                int temp = GetElement(node,left);
                GetPos(node,left).data = GetElement(node, right);
                GetPos(node, right).data = temp;
            }
        }

        static void SelectionSort1(Node node)
        {
            Stopwatch sWatch = new Stopwatch();
            sWatch.Start();
            int sortedRangeEnd = 0;
            while (sortedRangeEnd < kolvo1)
            {
                int nextIndex = FindIndexOfSmallestFromIndex1(node, sortedRangeEnd);
                Swap1(node, sortedRangeEnd, nextIndex);
                sortedRangeEnd++;
            }
            Console.WriteLine("Your massive after sorting: ");
            node.Print();
            Console.WriteLine();
            sWatch.Stop();
            Console.WriteLine("Duration " + sWatch.ElapsedMilliseconds + "ms");
        }

        static int FindIndexOfSmallestFromIndex1(Node node, int sortedRangeEnd)
        {
            int currentSmallest = GetElement(node, sortedRangeEnd);
            int currentSmallestIndex = sortedRangeEnd;
            for (int i = sortedRangeEnd + 1; i < kolvo1; i++)
            {
                if (currentSmallest.CompareTo(GetElement(node, i)) > 0)
                {
                    currentSmallest = GetElement(node, i);
                    currentSmallestIndex = i;
                }
            }
            return currentSmallestIndex;
        }

        



        static void Task2()
        {
            Console.WriteLine("Enter the number of values in your linked list: ");
            kolvo1 = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter the limits of values in your linked list: ");
            int limit1 = Convert.ToInt32(Console.ReadLine());
            int limit2 = Convert.ToInt32(Console.ReadLine());
            int limit3 = limit2 - limit1;
            Random element = new Random();
            Node myNode = new Node(limit1 + element.Next(limit3));
            for (int i = 1; i < kolvo1; i++)
            {
                myNode.AddToEnd(limit1 + element.Next(limit3));
            }
            Console.WriteLine("Your linked list: ");
            myNode.Print();
            Console.WriteLine();
            char command;
            char key;
            do
            {
                Console.WriteLine("==============================================");
                Console.WriteLine("Menu of command");
                Console.WriteLine("1 Insertion sorting");
                Console.WriteLine("2 Selection sorting");
                Console.WriteLine("select of command, press number of key");
                Console.WriteLine("==============================================");
                command = (char)Console.Read(); // вибір команди меню
                Console.ReadLine();
                switch (command)
                {
                    case '1':InsertionSort1(myNode); break;
                    case '2': SelectionSort1(myNode); break;
                    default: Console.WriteLine("wrong command"); break;
                }
                Console.WriteLine("Continue y/n");
                key = (char)Console.Read(); //ввід ключа продовження циклу
                Console.ReadLine();
            } while (key != 'n');
        }
        private static int GetElement(Node start, int position)
        {
            for (int i = 0; i < position; i++)
            {
                start = start.next;
            }
            return start.data;
        }

        private static Node GetPos(Node start, int position)
        {
            for (int i = 0; i < position; i++)
            {
                start = start.next;
            }
            return start;
        }
        static void GeneratingMassive(int lim1, int lim3, int[] items)
        {
            Random rand = new Random();
            for (int i = 0; i < kolvo; i++)
            {
                items[i] = lim1 + rand.Next(lim3);
            }
        }
        /// <summary>
        /// функція виводу масиву на екран
        /// </summary>
        /// <param name="massive"></param>
        static void OutputMassive(int[] massive)
        {
            for (int i = 0; i < massive.Length; i++)
            {
                Console.Write(massive[i] + " ");
            }
            Console.WriteLine();
        }
    }
}