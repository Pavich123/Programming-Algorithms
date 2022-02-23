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
        public static int[] numbers = new int[1000];
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
            Console.WriteLine("Lab1, Pavlyuchenko Artem, pavlyuchenko0209@gmail.com");
            
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
            Console.WriteLine("Enter the limits of values in your massive: ");
            lim1 = Convert.ToInt32(Console.ReadLine());
            lim2 = Convert.ToInt32(Console.ReadLine());
            int lim3 = lim2 - lim1;
            if (lim2 < lim1) Console.WriteLine("Error");
            else
            {
                Console.WriteLine("Your massive: ");
                GeneratingMassive(lim1, lim3);
                OutputMassive(numbers);
            }
            char command;
            char key;
            do
            {
                Console.WriteLine("==============================================");
                Console.WriteLine("Menu of command");
                Console.WriteLine("1 Linear search");
                Console.WriteLine("2 Search with barrier");
                Console.WriteLine("3 Binary Search");
                Console.WriteLine("4 Binary Search with Golden ratio");
                Console.WriteLine("select of command, press number of key");
                Console.WriteLine("==============================================");
                command = (char)Console.Read(); // вибір команди меню
                Console.ReadLine();
                switch (command)
                {
                    case '1': perebor(numbers); break;
                    case '2': pereborQuick(numbers); break;
                    case '3': binary(numbers); break;
                    case '4': goldenratio(numbers); break;
                    default: Console.WriteLine("wrong command"); break;
                }
                Console.WriteLine("Continue y/n");
                key = (char)Console.Read(); //ввід ключа продовження циклу
                Console.ReadLine();
            } while (key != 'n');
        }
        static void search(Node head)
        {
            Console.WriteLine("Enter the number you want to find: ");
            int key = Convert.ToInt32(Console.ReadLine());
            int x = 0;
            bool flag2 = false;
            while ((x < kolvo1) && (!flag2))
            {
                if (head.data == key)
                {
                    flag2 = true;
                    Console.WriteLine("Index of your element " + x);
                }
                head = head.next;
                x++;
            }
            if (x == kolvo1) Console.WriteLine("Entered number don`t exist in array");
        }

        static void Sort(Node obj)
        {
            int tmp;
            Node index = null;
            while (obj != null)
            {
                index = obj.next;
                while (index != null)
                {
                    if (obj.data > index.data)
                    {
                        tmp = obj.data;
                        obj.data = index.data;
                        index.data = tmp;
                    }
                    index = index.next;
                }
                obj = obj.next;
            }
        }
        private static int GetElement(Node start, int position)
        {
            for (int i = 0; i < position-1; i++)
            {
                start = start.next;
            }
            return start.data;
        }

        static void binaryList(Node head)
        {
            int l = 0;
            int r = kolvo1;
            int m = 0;
            int x;
            Sort(head);
            Console.WriteLine("Your linked list after sorting: ");
            head.Print();
            Console.WriteLine();
            Console.WriteLine("Enter x");
            x = Convert.ToInt32(Console.ReadLine());
            do
            {
                m = r - ((r - l) / 2);
                if (GetElement(head,m) < x) l = m + 1;
                else r = m - 1;
            } while ((l <= r) && (GetElement(head, m) != x));
            if (GetElement(head, m) == x)
            {
                Console.WriteLine("X in linked list:");
                Console.WriteLine("Index of your element " + (m-1));
            }
            else Console.WriteLine("X isn't included in array");
        }
        static void binaryListGold(Node head)
        {
            int l = 0;
            int r = kolvo1;
            int m = 0;
            int x;
            Sort(head);
            Console.WriteLine("Your linked list after sorting: ");
            head.Print();
            Console.WriteLine();
            Console.WriteLine("Enter x");
            x = Convert.ToInt32(Console.ReadLine());
            do
            {
                m = Convert.ToInt32(r - ((r - l) / 1.618));
                if (GetElement(head, m) < x) l = m + 1;
                else r = m - 1;
            } while ((l <= r) && (GetElement(head, m) != x));
            if (GetElement(head, m) == x)
            {
                Console.WriteLine("X in linked list:");
                Console.WriteLine("Index of your element " + (m-1));
            }
            else Console.WriteLine("X isn't included in linked list");
        }

        static void barrier(Node head)
        {
            Console.WriteLine("Enter the number you want to find: ");
            int x = Convert.ToInt32(Console.ReadLine());
            int k = 0;
            head.AddToEnd(x);
            while (head.data != x)
            {
                head = head.next;
                k++;
            }
            if (k == kolvo1) Console.WriteLine("Entered number don`t exist in array");
            else Console.WriteLine("Index of your element " + k);
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
                Console.WriteLine("1 Linear search");
                Console.WriteLine("2 Search with barrier");
                Console.WriteLine("3 Binary Search");
                Console.WriteLine("4 Binary Search with Golden ratio");
                Console.WriteLine("select of command, press number of key");
                Console.WriteLine("==============================================");
                command = (char)Console.Read(); // вибір команди меню
                Console.ReadLine();
                switch (command)
                {
                    case '1': search(myNode); break;
                    case '2': barrier(myNode); break;
                    case '3': binaryList(myNode); break;
                    case '4': binaryListGold(myNode); break;
                    default: Console.WriteLine("wrong command"); break;
                }
                Console.WriteLine("Continue y/n");
                key = (char)Console.Read(); //ввід ключа продовження циклу
                Console.ReadLine();
            } while (key != 'n');
        }
        static void perebor(int[] numbers)
        {
            Stopwatch sWatch = new Stopwatch();
            Console.WriteLine("Enter the number you want to find: ");
            sWatch.Start();
            int x = Convert.ToInt32(Console.ReadLine());
            int i = 0;
            bool flag1 = false;
            while ((i < kolvo) && (!flag1))
            {
                if (numbers[i] == x)
                {
                    flag1 = true;
                    sWatch.Stop();
                    Console.WriteLine("Index of your element " + i );
                    Console.WriteLine("Duration " + sWatch.ElapsedMilliseconds + "ms");
                }
                i++;
            }
            sWatch.Stop();
            if (i == kolvo)
            {
                Console.WriteLine("Entered number don`t exist in array");
                Console.WriteLine("Duration " + sWatch.ElapsedMilliseconds + "ms");
            }

        }
        static void pereborQuick(int[] numbers)
        {
            Stopwatch sWatch = new Stopwatch();
            int[] h1numbers = new int[1000];
            for (int i = 0; i < kolvo; i++)
            {
                h1numbers[i] = numbers[i];
            }
            sWatch.Start();
            Console.WriteLine("Enter the number you want to find: ");
            int x = Convert.ToInt32(Console.ReadLine());
            int k = 0;
            h1numbers[kolvo] = x;
            while (h1numbers[k] != x)
            {
                k++;
            }
            sWatch.Stop();
            if (k == kolvo) Console.WriteLine("Element hasn`t been found");
            else Console.WriteLine("Index of your element " + k);
            Console.WriteLine("Duration " + sWatch.ElapsedMilliseconds + "ms");
        }
        static void binary(int[] numbers)
        {
            int[] hnumbers = new int[1000];
            for (int i = 0; i < kolvo; i++)
            {
                hnumbers[i] = numbers[i];
            }
            for (int i = 0; i < kolvo - 1; i++)
            {
                for (int j = i + 1; j < kolvo; j++)
                {
                    if (hnumbers[i] > hnumbers[j])
                    {
                        int tmp = hnumbers[i];
                        hnumbers[i] = hnumbers[j];
                        hnumbers[j] = tmp;
                    }
                }
            }
            Console.WriteLine("Your massive after sorting: ");
            OutputMassive(hnumbers);
            int l = 0;
            int r = kolvo - 1;
            int m = 0;
            int x;
            Stopwatch sWatch = new Stopwatch();
            sWatch.Start();
            Console.WriteLine("Enter x");
            x = Convert.ToInt32(Console.ReadLine());
            do
            {
                m = r - ((r - l) / 2);
                if (hnumbers[m] < x) l = m + 1;
                else r = m - 1;
            } while ((l <= r) && (hnumbers[m] != x));
            sWatch.Stop();
            if (hnumbers[m] == x)
            {
                Console.WriteLine("X in array:");
                Console.WriteLine("a[" + m + "]= " + hnumbers[m]);
                Console.WriteLine("Duration " + sWatch.ElapsedMilliseconds + "ms");
            }
            else
            {
                Console.WriteLine("X isn't included in array");
                Console.WriteLine("Duration " + sWatch.ElapsedMilliseconds + "ms");
            }
            }
        static void goldenratio(int[] numbers)
        {
            int[] h2numbers = new int[1000];
            for (int i = 0; i < kolvo; i++)
            {
                h2numbers[i] = numbers[i];
            }
            for (int i = 0; i < kolvo - 1; i++)
            {
                for (int j = i + 1; j < kolvo; j++)
                {
                    if (h2numbers[i] > h2numbers[j])
                    {
                        int tmp = h2numbers[i];
                        h2numbers[i] = h2numbers[j];
                        h2numbers[j] = tmp;
                    }
                }
            }
            Console.WriteLine("Your massive after sorting: ");
            OutputMassive(h2numbers);
            int l = 0;
            int r = kolvo - 1;
            int m = 0;
            int x;
            Stopwatch sWatch = new Stopwatch();
            sWatch.Start();
            Console.WriteLine("Enter x");
            x = Convert.ToInt32(Console.ReadLine());
            do
            {
                m = Convert.ToInt32(r-(r-l)/1.618);
                if (h2numbers[m] < x) l = m + 1;
                else r = m - 1;
            } while ((l <= r) && (h2numbers[m] != x));
            sWatch.Stop();
            if (h2numbers[m] == x)
            {
                Console.WriteLine("X in array:");
                Console.WriteLine("a[" + m + "]= " + h2numbers[m]);
                Console.WriteLine("Duration " + sWatch.ElapsedMilliseconds + "ms");
            }
            else
            {
                Console.WriteLine("X isn't included in array");
                Console.WriteLine("Duration " + sWatch.ElapsedMilliseconds + "ms");
            }
            }
        /// <summary>
        /// функція генерації масиву
        /// </summary>
        /// <param name="lim1"></param>
        /// <param name="lim3"></param>
        static void GeneratingMassive(int lim1, int lim3)
        {
            Random rand = new Random();
            for (int i = 0; i < kolvo; i++)
            {
                numbers[i] = lim1 + rand.Next(lim3);
            }
        }
        /// <summary>
        /// функція виводу масиву на екран
        /// </summary>
        /// <param name="massive"></param>
        static void OutputMassive(int[] massive)
        {
            for (int i = 0; i < kolvo; i++)
            {
                Console.Write(massive[i] + " ");
            }
            Console.WriteLine();
        }
    }
}