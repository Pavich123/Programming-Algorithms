using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;

namespace lab3
{
    public class AVLTree<T>: IEnumerable<T> where T:IComparable
    {
        public AVLTreeNode<T> Head
        {
            get;
            internal set;
        }
        public int Count
        {
            get;
            private set;
        }
        public IEnumerator<T> InOrderTraversal()
        {
            if (Head != null)
            {
                Stack<AVLTreeNode<T>> stack = new Stack<AVLTreeNode<T>>();
                AVLTreeNode<T> current = Head;
                bool goLeftNext = true;
                stack.Push(current);
                while (stack.Count > 0)
                {
                    if (goLeftNext)
                    {
                        while (current.Left != null)
                        {
                            stack.Push(current);
                            current = current.Left;
                        }
                    }
                    yield return current.Value;
                    if (current.Right != null)
                    {
                        current = current.Right;
                        goLeftNext = true;
                    }
                    else
                    {
                        current = stack.Pop();
                        goLeftNext = false;
                    }
                }
            }
        }
        public IEnumerator<T> GetEnumerator()
        {
            return InOrderTraversal();
        }
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        public void Add(T value)
        {
            if (Head == null)
            {
                Head = new AVLTreeNode<T>(value, null, this);
            }
            else
            {
                AddTo(Head, value);
            }
            Count++;
        }
        private void AddTo(AVLTreeNode<T> node, T value)
        {
            if (value.CompareTo(node.Value) < 0)
            {
                if (node.Left == null)
                {
                    node.Left = new AVLTreeNode<T>(value, node, this);
                }
                else
                {
                    AddTo(node.Left, value);
                }
            }
            else
            {
                if (node.Right == null)
                {
                    node.Right = new AVLTreeNode<T>(value, node, this);
                }
                else
                {
                    AddTo(node.Right, value);
                }
            }
            node.Balance();
        }
        private AVLTreeNode<T> Find(T value)
        {
            AVLTreeNode<T> current = Head;
            while (current != null)
            {
                int result = current.CompareTo(value);
                if (result > 0)
                {
                    current = current.Left;
                }
                else if (result < 0)
                {
                    current = current.Right;
                }
                else
                {
                    break;
                }
            }
            return current;
        }
        public bool Remove(T value)
        {
            AVLTreeNode<T> current;
            current = Find(value);
            Console.WriteLine(current.Value);
            if (current == null)
            {
                return false;
            }
            AVLTreeNode<T> treeToBalance = current.Parent;
            Count--;
            if (current.Right == null)
            {
                if (current.Parent == null)
                {
                    Head = current.Left;
                    if (Head != null)
                    {
                        Head.Parent = null;
                    }
                }
                else
                {
                    int result = current.Parent.CompareTo(current.Value);
                    if (result > 0)
                    {
                        current.Parent.Left = current.Left;
                    }
                    else if (result < 0)
                    {
                       current.Parent.Right = current.Left;
                    }
                }
            }
            else if (current.Right.Left == null)
            {
                current.Right.Left = current.Left;
                if (current.Parent == null)
                {
                    Head = current.Right;
                    if (Head != null)
                    {
                        Head.Parent = null;
                    }
                }
                else
                {
                    int result = current.Parent.CompareTo(current.Value);
                    if (result > 0)
                    {
                        current.Parent.Left = current.Right;
                    }
                    else if (result < 0)
                    {
                        current.Parent.Right = current.Right;
                    }
                }
            }
            else
            {
                AVLTreeNode<T> leftmost = current.Right.Left;
                while (leftmost.Left != null)
                {
                    leftmost = leftmost.Left;
                }
                leftmost.Parent.Left = leftmost.Right;
                leftmost.Left = current.Left;
                leftmost.Right = current.Right;
                if (current.Parent == null)
                {
                    Head = leftmost;
                    if (Head != null)
                    {
                        Head.Parent = null;
                    }
                }
                else
                {
                    int result = current.Parent.CompareTo(current.Value);
                    if (result > 0)
                    {
                        current.Parent.Left = leftmost;
                    }
                    else if (result < 0)
                    {
                        current.Parent.Right = leftmost;
                    }
                }
            }
            if (treeToBalance != null)
            {
                treeToBalance.Balance();
            }
            else
            {
                if (Head != null)
                {
                    Head.Balance();
                }
            }
            return true;
        }
    }
    public class AVLTreeNode<TNode>: IComparable<TNode> where TNode : IComparable
    {
        AVLTree<TNode> _tree;
        AVLTreeNode<TNode> _left;
        AVLTreeNode<TNode> _right;
        public AVLTreeNode(TNode value, AVLTreeNode<TNode> parent, AVLTree<TNode> tree)
        {
            Value = value;
            Parent = parent;
            _tree = tree;
        }
        public AVLTreeNode<TNode> Left
        {
            get
            {
                return _left;
            }
            internal set
            {
                _left = value;
                if (_left != null)
                {
                    _left.Parent = this;
                }
            }
        }
        public AVLTreeNode<TNode> Right
        {
            get
            {
                return _right;
            }
            internal set
            {
                _right = value;
                if (_right != null)
                {
                    _right.Parent = this;
                }
            }
        }
        public AVLTreeNode<TNode> Parent
        {
            get;
            internal set;
        }
        public TNode Value
        {
            get;
            private set;
        }
        public int CompareTo(TNode other)
        {
            return Value.CompareTo(other);
        }
        internal void Balance()
        {
            if (State == TreeState.RightHeavy)
            {
                if (Right != null && Right.BalanceFactor < 0)
                {
                    LeftRightRotation();

                }
                else
                {
                    LeftRotation();
                }
            }
            else if (State == TreeState.RightHeavy)
            {
                if (Right != null && Right.BalanceFactor > 0)
                {
                    RightLeftRotation();


                }
                else
                {
                    RightRotation();
                }
            }
        }
        private void LeftRightRotation()
        {
            Right.RightRotation();
            LeftRotation();
        }
        private void RightLeftRotation()
        {
            Left.LeftRotation();
            RightRotation();
        }
        private void LeftRotation()
        {
            AVLTreeNode<TNode> newRoot = Right;
            ReplaceRoot(newRoot);
            Right = newRoot.Left;
            newRoot.Left = this;
        }
        private void RightRotation()
        {
            AVLTreeNode<TNode> newRoot = Left;
            ReplaceRoot(newRoot);
            Left = newRoot.Right;
            newRoot.Right = this;
        }
        private void ReplaceRoot(AVLTreeNode<TNode> newRoot)
        {
            if (this.Parent != null)
            {
                if (this.Parent.Left == this)
                {
                    this.Parent.Left = newRoot;
                }
                else if (this.Parent.Right == this)
                {
                    this.Parent.Right = newRoot;
                }
            }
            else
            {
                _tree.Head = newRoot;
            }
            newRoot.Parent = this.Parent;
            this.Parent = newRoot;
        }
        private int BalanceFactor
        {
            get
            {
                return RightHeight - LeftHeight;
            }
        }
        private int MaxChildHeight(AVLTreeNode<TNode> node)
        {
            if (node != null)
            {
                return 1 + Math.Max(MaxChildHeight(node.Left), MaxChildHeight(node.Right));
            }
            return 0;
        }
        private int RightHeight
        {
            get
            {
                return MaxChildHeight(Right);
            }
        }
        private int LeftHeight
        {
            get
            {
                return MaxChildHeight(Left);
            }
        }
        private TreeState State
        {
            get
            {
                if (LeftHeight - RightHeight > 1)
                {
                    return TreeState.LeftHeavy;
                }
                if (RightHeight - LeftHeight > 1)
                {
                    return TreeState.RightHeavy;
                }
                return TreeState.Balanced;
            }
        }
        enum TreeState
        {
            Balanced,
            LeftHeavy,
            RightHeavy,
        }

    }
    public class BinaryTreeNode<TNode> : IComparable<TNode> where TNode : IComparable<TNode>
    {
        public BinaryTreeNode(TNode data)
        {
            Data = data;
        }
        public BinaryTreeNode<TNode> Left
        {
            get;
            set;
        }
        public BinaryTreeNode<TNode> Right
        {
            get;
            set;
        }
        public TNode Data
        {
            get;
            private set;
        }
        public int CompareTo(TNode other)
        {
            return Data.CompareTo(other);
        }
        public int CompareNode(BinaryTreeNode<TNode> other)
        {
            return Data.CompareTo(other.Data);
        }
    }

    public class BinaryTree<T> : IEnumerable<T> where T : IComparable<T>
    {
        private int count;
        private BinaryTreeNode<T> head;
        public void Add(T data)
        {
            if (head == null)
            {
                head = new BinaryTreeNode<T>(data);
            }
            else
            {
                AddTo(head, data);
            }
            count++;
        }
        private void AddTo(BinaryTreeNode<T> node, T data)
        {
            if (data.CompareTo(node.Data) < 0)
            {
                if (node.Left == null)
                {
                    node.Left = new BinaryTreeNode<T>(data);
                }
                else
                {
                    AddTo(node.Left, data);
                }
            }
            else
            {
                if (node.Right == null)
                {
                    node.Right = new BinaryTreeNode<T>(data);
                }
                else
                {
                    AddTo(node.Right, data);
                }
            }
        }
        public bool Contains(T data)
        {
            BinaryTreeNode<T> parent;
            return FindWithParent(data, out parent) != null;
        }
        private BinaryTreeNode<T> FindWithParent(T data, out BinaryTreeNode<T> parent)
        {
            BinaryTreeNode<T> current = head;
            parent = null;
            while (current != null)
            {
                int result = current.CompareTo(data);
                if (result > 0)
                {
                    parent = current;
                    current = current.Left;
                }
                else if (result < 0)
                {
                    parent = current;
                    current = current.Right;
                }
                else
                {
                    break;
                }
            }
            return current;
        }
        public IEnumerator<T> GetEnumerator()
        {
            return InOrderTraversal();
        }
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        public IEnumerator<T> InOrderTraversal()
        {
            if (head != null)
            {
                Stack<BinaryTreeNode<T>> stack = new Stack<BinaryTreeNode<T>>();
                BinaryTreeNode<T> current = head;
                bool goLeftNext = true;
                stack.Push(current);
                while (stack.Count > 0)
                {
                    if (goLeftNext)
                    {
                        while (current.Left != null)
                        {
                            stack.Push(current);
                            current = current.Left;
                        }
                    }
                    yield return current.Data;
                    if (current.Right != null)
                    {
                        current = current.Right;
                        goLeftNext = true;
                    }
                    else
                    {
                        current = stack.Pop();
                        goLeftNext = false;
                    }
                }
            }
        }
        public int Count
        {
            get { return count; }
        }
        public bool Remove(T data)
        {
            BinaryTreeNode<T> current;
            BinaryTreeNode<T> parent;
            current = FindWithParent(data, out parent);
            if (current == null)
            {
                return false;
            }
            count--;
            if (current.Right == null)
            {
                if (parent == null)
                {
                    head = current.Left;
                }
                else
                {
                    int result = parent.CompareTo(current.Data);
                    if (result > 0)
                    {
                        parent.Left = current.Left;
                    }
                    else if (result < 0)
                    {
                        parent.Right = current.Right;
                    }
                }
            }
            else if (current.Right.Left == null)
            {
                current.Right.Left = current.Left;
                if (parent == null)
                {
                    head = current.Right;
                }
                else
                {
                    int result = parent.CompareTo(current.Data);
                    if (result > 0)
                    {
                        parent.Left = current.Left;
                    }
                    else if (result < 0)
                    {
                        parent.Right = current.Right;
                    }
                }
            }
            else
            {
                BinaryTreeNode<T> leftmost = current.Right.Left;
                BinaryTreeNode<T> leftmostParent = current.Right;
                while (leftmost.Left != null)
                {
                    leftmostParent = leftmost;
                    leftmost = leftmost.Left;
                }
                leftmostParent.Left = leftmost.Right;
                leftmost.Left = current.Left;
                leftmost.Right = current.Right;
                if (parent == null)
                {
                    head = leftmost;
                }
                else
                {
                    int result = parent.CompareTo(current.Data);
                    if (result > 0)
                    {
                        parent.Left = leftmost;
                    }
                    else if (result < 0)
                    {
                        parent.Right = leftmost;
                    }
                }
            }
            return true;
        }
    }



    enum Color
    {
        Red,
        Black
    }
    class RB
    {
        public class Node
        {
            public Color colour;
            public Node left;
            public Node right;
            public Node parent;
            public int data;

            public Node(int data) { this.data = data; }
            public Node(Color colour) { this.colour = colour; }
            public Node(int data, Color colour) { this.data = data; this.colour = colour; }
        }
     
        private Node root;
      
        public RB() { }
      
        private void LeftRotate(Node X)
        {
            Node Y = X.right; 
            X.right = Y.left;
            if (Y.left != null)
            {
                Y.left.parent = X;
            }
            if (Y != null)
            {
                Y.parent = X.parent;
            }
            if (X.parent == null)
            {
                root = Y;
            }
            if (X == X.parent.left)
            {
                X.parent.left = Y;
            }
            else
            {
                X.parent.right = Y;
            }
            Y.left = X; 
            if (X != null)
            {
                X.parent = Y;
            }

        }
       
        private void RightRotate(Node Y)
        {
            Node X = Y.left;
            Y.left = X.right;
            if (X.right != null)
            {
                X.right.parent = Y;
            }
            if (X != null)
            {
                X.parent = Y.parent;
            }
            if (Y.parent == null)
            {
                root = X;
            }
            if (Y == Y.parent.right)
            {
                Y.parent.right = X;
            }
            if (Y == Y.parent.left)
            {
                Y.parent.left = X;
            }

            X.right = Y;
            if (Y != null)
            {
                Y.parent = X;
            }
        }
      
        public void DisplayTree()
        {
            if (root == null)
            {
                Console.WriteLine("Nothing in the tree!");
                return;
            }
            if (root != null)
            {
                InOrderDisplay(root);
            }
        }
        
        public Node Find(int key)
        {
            bool isFound = false;
            Node temp = root;
            Node item = null;
            while (!isFound)
            {
                if (temp == null)
                {
                    break;
                }
                if (key < temp.data)
                {
                    temp = temp.left;
                }
                if (key > temp.data)
                {
                    temp = temp.right;
                }
                if (key == temp.data)
                {
                    isFound = true;
                    item = temp;
                }
            }
            if (isFound)
            {
                Console.WriteLine("{0} was found", key);
                return temp;
            }
            else
            {
                Console.WriteLine("{0} not found", key);
                return null;
            }
        }
        
        public void Insert(int item)
        {
            Node newItem = new Node(item);
            if (root == null)
            {
                root = newItem;
                root.colour = Color.Black;
                return;
            }
            Node Y = null;
            Node X = root;
            while (X != null)
            {
                Y = X;
                if (newItem.data < X.data)
                {
                    X = X.left;
                }
                else
                {
                    X = X.right;
                }
            }
            newItem.parent = Y;
            if (Y == null)
            {
                root = newItem;
            }
            else if (newItem.data < Y.data)
            {
                Y.left = newItem;
            }
            else
            {
                Y.right = newItem;
            }
            newItem.left = null;
            newItem.right = null;
            newItem.colour = Color.Red;
            InsertFixUp(newItem);
        }
        private void InOrderDisplay(Node current)
        {
            
            if (current != null)
            {
                InOrderDisplay(current.left);
                Console.WriteLine(current.data);
                InOrderDisplay(current.right);
            }
        }
        private void InsertFixUp(Node item)
        {
            while (item != root && item.parent.colour == Color.Red)
            {
                if (item.parent == item.parent.parent.left)
                {
                    Node Y = item.parent.parent.right;
                    if (Y != null && Y.colour == Color.Red)
                    {
                        item.parent.colour = Color.Black;
                        Y.colour = Color.Black;
                        item.parent.parent.colour = Color.Red;
                        item = item.parent.parent;
                    }
                    else 
                    {
                        if (item == item.parent.right)
                        {
                            item = item.parent;
                            LeftRotate(item);
                        }
                        item.parent.colour = Color.Black;
                        item.parent.parent.colour = Color.Red;
                        RightRotate(item.parent.parent);
                    }

                }
                else
                {
                    
                    Node X = null;

                    X = item.parent.parent.left;
                    if (X != null && X.colour == Color.Black)
                    {
                        item.parent.colour = Color.Red;
                        X.colour = Color.Red;
                        item.parent.parent.colour = Color.Black;
                        item = item.parent.parent;
                    }
                    else 
                    {
                        if (item == item.parent.left)
                        {
                            item = item.parent;
                            RightRotate(item);
                        }
                        
                        item.parent.colour = Color.Black;
                        item.parent.parent.colour = Color.Red;
                        LeftRotate(item.parent.parent);

                    }

                }
                root.colour = Color.Black;
            }
        }
        
        public void Delete(int key)
        {
            
            Node item = Find(key);
            Node X = null;
            Node Y = null;

            if (item == null)
            {
                Console.WriteLine("Nothing to delete!");
                return;
            }
            if (item.left == null || item.right == null)
            {
                Y = item;
            }
            else
            {
                Y = TreeSuccessor(item);
            }
            if (Y.left != null)
            {
                X = Y.left;
            }
            else
            {
                X = Y.right;
            }
            if (X != null)
            {
                X.parent = Y;
            }
            if (Y.parent == null)
            {
                root = X;
            }
            else if (Y == Y.parent.left)
            {
                Y.parent.left = X;
            }
            else
            {
                Y.parent.left = X;
            }
            if (Y != item)
            {
                item.data = Y.data;
            }
            if (Y.colour == Color.Black)
            {
                DeleteFixUp(X);
            }

        }
        
        private void DeleteFixUp(Node X)
        {

            while (X != null && X != root && X.colour == Color.Black)
            {
                if (X == X.parent.left)
                {
                    Node W = X.parent.right;
                    if (W.colour == Color.Red)
                    {
                        W.colour = Color.Black; 
                        X.parent.colour = Color.Red; 
                        LeftRotate(X.parent);
                        W = X.parent.right; 
                    }
                    if (W.left.colour == Color.Black && W.right.colour == Color.Black)
                    {
                        W.colour = Color.Red;
                        X = X.parent; 
                    }
                    else if (W.right.colour == Color.Black)
                    {
                        W.left.colour = Color.Black; 
                        W.colour = Color.Red; 
                        RightRotate(W); 
                        W = X.parent.right; 
                    }
                    W.colour = X.parent.colour; 
                    X.parent.colour = Color.Black; 
                    W.right.colour = Color.Black; 
                    LeftRotate(X.parent); 
                    X = root; 
                }
                else 
                {
                    Node W = X.parent.left;
                    if (W.colour == Color.Red)
                    {
                        W.colour = Color.Black;
                        X.parent.colour = Color.Red;
                        RightRotate(X.parent);
                        W = X.parent.left;
                    }
                    if (W.right.colour == Color.Black && W.left.colour == Color.Black)
                    {
                        W.colour = Color.Black;
                        X = X.parent;
                    }
                    else if (W.left.colour == Color.Black)
                    {
                        W.right.colour = Color.Black;
                        W.colour = Color.Red;
                        LeftRotate(W);
                        W = X.parent.left;
                    }
                    W.colour = X.parent.colour;
                    X.parent.colour = Color.Black;
                    W.left.colour = Color.Black;
                    RightRotate(X.parent);
                    X = root;
                }
            }
            if (X != null)
                X.colour = Color.Black;
        }
        private Node Minimum(Node X)
        {
            while (X.left.left != null)
            {
                X = X.left;
            }
            if (X.left.right != null)
            {
                X = X.left.right;
            }
            return X;
        }
        private Node TreeSuccessor(Node X)
        {
            if (X.left != null)
            {
                return Minimum(X);
            }
            else
            {
                Node Y = X.parent;
                while (Y != null && X == Y.right)
                {
                    X = Y;
                    Y = Y.parent;
                }
                return Y;
            }
        }
    }




    class Program
    {
        public static RB thirdtree = new RB();
        public static AVLTree<int> sectree = new AVLTree<int>();
        public static BinaryTree<int> tree = new BinaryTree<int>();
        static void Main(string[] args)
        {
            Console.WriteLine("Lab3, Pavlyuchenko Artem, IPZ-11/1");
            Menu();
        }
        static void Adding()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            Console.WriteLine("Enter number of nodes in tree:");
            int kolvo = Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i < kolvo; i++)
            {
                tree.Add(new Random().Next(50));
            }
            sw.Stop();
            Console.WriteLine("Duration " + sw.ElapsedMilliseconds + " ms");
        }
        static void Removing()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            Console.WriteLine("Enter the element you want to delete in tree:");
            int element1 = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Element " + element1 + ": " + tree.Remove(element1));
            sw.Stop();
            Console.WriteLine("Duration " + sw.ElapsedMilliseconds + " ms");
        }
        static void Checking()
        {
            Console.WriteLine("Enter the element you want to check in tree:");
            int element = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Element " + element + ": " + tree.Contains(element));
        }
        static void Traversal()
        {
            Console.WriteLine("Tree traversal:");
            foreach (var item in tree)
            {
                Console.Write(item+" ");
            }

        }
        static void Menu()
        {
            char command;
            char key;
            do
            {
                Console.WriteLine("==============================================");
                Console.WriteLine("Menu of command");
                Console.WriteLine("1 if you want to work with binary tree search");
                Console.WriteLine("2 if you want to work with avl tree");
                Console.WriteLine("3 if you want to work with red-black tree");
                Console.WriteLine();
                Console.WriteLine("select of command, press number of key");
                Console.WriteLine("==============================================");
                command = (char)Console.Read(); // вибір команди меню
                Console.ReadLine();
                switch (command)
                {
                    case '1': Task1(); break;
                    case '2': Task2(); break;
                    case '3': Task3(); break;
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
            char command;
            char key;
            do
            {
                Console.WriteLine("==============================================");
                Console.WriteLine("Menu of command");
                Console.WriteLine("1 Add element");
                Console.WriteLine("2 Remove element");
                Console.WriteLine("3 Check element");
                Console.WriteLine("4 Tree traversal");
                Console.WriteLine("select of command, press number of key");
                Console.WriteLine("==============================================");
                command = (char)Console.Read(); // вибір команди меню
                Console.ReadLine();
                switch (command)
                {
                    case '1': Adding(); ; break;
                    case '2': Removing(); ; break;
                    case '3': Checking(); ; break;
                    case '4': Traversal(); ; break;
                    default: Console.WriteLine("wrong command"); break;
                }
                Console.WriteLine("Continue y/n");
                key = (char)Console.Read(); //ввід ключа продовження циклу
                Console.ReadLine();
            } while (key != 'n');
        }
        static void Task2()
        {
            char command;
            char key;
            do
            {
                Console.WriteLine("==============================================");
                Console.WriteLine("Menu of command");
                Console.WriteLine("1 Add element");
                Console.WriteLine("2 Remove element");
                Console.WriteLine("3 Tree traversal");
                Console.WriteLine("select of command, press number of key");
                Console.WriteLine("==============================================");
                command = (char)Console.Read(); // вибір команди меню
                Console.ReadLine();
                switch (command)
                {
                    case '1': AddingAVL();  break;
                    case '2': RemovingAVL();  break;
                    case '3': TraversalAVL();  break;
                    default: Console.WriteLine("wrong command"); break;
                }
                Console.WriteLine("Continue y/n");
                key = (char)Console.Read(); //ввід ключа продовження циклу
                Console.ReadLine();
            } while (key != 'n');

        }
        static void AddingAVL()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            Console.WriteLine("Enter number of nodes in tree:");
            int kolvo = Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i < kolvo; i++)
            {
                sectree.Add(new Random().Next(50));
            }
            sw.Stop();
            Console.WriteLine("Duration " + sw.ElapsedMilliseconds + " ms");
        }
        static void RemovingAVL()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            Console.WriteLine("Enter the element you want to delete in tree:");
            int element1 = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Element " + element1 + ": " + sectree.Remove(element1));
            sw.Stop();
            Console.WriteLine("Duration " + sw.ElapsedMilliseconds + " ms");
        }
        static void TraversalAVL()
        {
            Console.WriteLine("Tree traversal:");
            foreach (var item in sectree)
            {
                Console.WriteLine(item);
            }
        }
        static void Task3()
        {
            char command;
            char key;
            do
            {
                Console.WriteLine("==============================================");
                Console.WriteLine("Menu of command");
                Console.WriteLine("1 Add element");
                Console.WriteLine("2 Remove element");
                Console.WriteLine("3 Display tree");
                Console.WriteLine("select of command, press number of key");
                Console.WriteLine("==============================================");
                command = (char)Console.Read(); // вибір команди меню
                Console.ReadLine();
                switch (command)
                {
                    case '1': AddingColour(); break;
                    case '2': RemovingColour(); break;
                    case '3': Console.WriteLine("Tree traversal:");  thirdtree.DisplayTree(); break;
                    default: Console.WriteLine("wrong command"); break;
                }
                Console.WriteLine("Continue y/n");
                key = (char)Console.Read(); //ввід ключа продовження циклу
                Console.ReadLine();
            } while (key != 'n');
            Console.ReadLine();


        }
        static void AddingColour()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            thirdtree.Insert(5);
            thirdtree.Insert(3);
            thirdtree.Insert(7);
            thirdtree.Insert(1);
            thirdtree.Insert(9);
            thirdtree.Insert(-1);
            thirdtree.Insert(11);
            thirdtree.Insert(6);
            sw.Stop();
            Console.WriteLine("Duration " + sw.ElapsedMilliseconds + " ms");
        }
        static void RemovingColour()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            Console.WriteLine("Enter the element you want to delete in tree:");
            int element1 = Convert.ToInt32(Console.ReadLine());
            thirdtree.Delete(element1);
            sw.Stop();
            Console.WriteLine("Duration " + sw.ElapsedMilliseconds + " ms");
        }
    }
}