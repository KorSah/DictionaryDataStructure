using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryDataStructure
{
    /// <summary>
    /// Red-Black Tree which implements Generic IDictionary interface
    /// </summary>
    /// <typeparam name="Tkey">Keys of nodes in the tree</typeparam>
    /// <typeparam name="Tvalue">Values of nodes in the tree</typeparam>
    public class RedBlackTree<Tkey, Tvalue> : IDictionary<Tkey, Tvalue> where Tkey : IComparable<Tkey>
    {
        // The Root of the tree
        public Node<Tkey, Tvalue> Root { private set; get; }

        // Nil of the tree which shows the end of the branch
        public Node<Tkey, Tvalue> Nil { private set; get; }

        /// <summary>
        /// Constructor
        /// </summary>
        public RedBlackTree()
        {
            this.Root = new Node<Tkey, Tvalue>();
            this.Root.Color = Color.Black;
            this.Nil = new Node<Tkey, Tvalue>();
            this.Nil.Color = Color.Black;
        }

        // List for keeping keys
        private List<Tkey> keyList = new List<Tkey>(); 
        // Keys of all nodes 
        public ICollection<Tkey> Keys { get { return keyList; } }

        // List for keeping values
        private List<Tvalue> valueList = new List<Tvalue>();
        // Values of all nodes
        public ICollection<Tvalue> Values { get { return valueList; } }

        // The count of nodes in the tree
        public int Count { private set; get; }

        // Gets a value indicating whether the IDictionary object is read-only
        public bool IsReadOnly { get { return false; } }

        // Indexator
        public Tvalue this[Tkey key]
        {
            get
            {
                return GetNode(key).Value;
            }
            set
            {
                if (ContainsKey(key))
                {
                    Node<Tkey, Tvalue> temp = this.GetNode(key);
                    temp.Value = value;
                }
            }
        }

        /// <summary>
        /// Method fixes the tree after deleting or inserting a node
        /// </summary>
        /// <param name="rotatableNode">Left Rotated node</param>
        private void LeftRotate(Node<Tkey, Tvalue> rotatableNode)
        {
            Node<Tkey, Tvalue> node = rotatableNode.Right;
            rotatableNode.Right = node.Left;
            if (node.Left != Nil)
            {
                node.Left.Parent = rotatableNode;
            }
            node.Parent = rotatableNode.Parent;
            if (rotatableNode.Parent == Nil)
            {
                Root = node;
            }
            else if (rotatableNode == rotatableNode.Parent.Left)
            {
                rotatableNode.Parent.Left = node;
            }
            else 
            {
                rotatableNode.Parent.Right = node;
            }
            node.Left = rotatableNode;
            rotatableNode.Parent = node;
        }

        /// <summary>
        /// Method fixes the tree after deleting or inserting a node
        /// </summary>
        /// <param name="rotatableNode">Right Rotated node</param>
        private void RightRotate(Node<Tkey, Tvalue> rotatableNode)
        {
            Node<Tkey, Tvalue> node = rotatableNode.Left;
            rotatableNode.Left = node.Right;   
            if (node.Right != Nil)
            {
                node.Right.Parent = rotatableNode;
            }
            node.Parent = rotatableNode.Parent;
            if (rotatableNode.Parent == Nil)
            {
                Root = node;
            }
            else if (rotatableNode == rotatableNode.Parent.Right)
            {
                rotatableNode.Parent.Right = node;
            }
            else
            {
                rotatableNode.Parent.Left = node;
            }
            node.Right = rotatableNode;
            rotatableNode.Parent = node;
        }

        /// <summary>
        /// Show Tree
        /// </summary>
        public void PrintTree()
        {
            if (Root == null)
            {
                Console.WriteLine("There is no node in the tree");
                return;
            }
            if (Root != null)
            {
                Print(Root);
            }
        }

        /// <summary>
        /// Show in order
        /// </summary>
        /// <param name="current">Current node</param>
        private void Print(Node<Tkey, Tvalue> current)
        {
            if (current != Nil)
            {
                Print(current.Left);
                Console.Write("({0}) ", current.Key);
                Print(current.Right);
            }
        }
  
        /// <summary>
        /// Adds new node in the tree
        /// </summary>
        /// <param name="key">The key of addable node</param>
        /// <param name="value">The value of addable node</param>
        public void Add(Tkey key, Tvalue value)
        {
            keyList.Add(key);
            valueList.Add(value);
            Node<Tkey, Tvalue> node = new Node<Tkey, Tvalue>(key, value);
            Node<Tkey, Tvalue> y = Nil;
            Node<Tkey, Tvalue> x = Root;
            while (x.Equals(Nil) == false)
            {
                y = x;
                if (key.CompareTo(x.Key) < 0)
                {
                    x = x.Left;
                }
                else if (key.CompareTo(x.Key) > 0)
                {
                    x = x.Right;
                }
                else
                    return;
            }
            node.Parent = y;
            if (y == Nil)
            {
                Root = node;
            }
            else if (node.Key.CompareTo(y.Key) < 0)
            {
                y.Left = node;
            }
            else
            {
                y.Right = node;
            }
            node.Left = Nil;
            node.Right = Nil;
            node.Color = Color.Red;
            InsertFixUp(node);
            this.Count++;
        }

        /// <summary>
        /// Method decides the right color of the node
        /// </summary>
        /// <param name="node">node which color must be fixed</param>
        private void InsertFixUp(Node<Tkey, Tvalue> node)
        {
            Node<Tkey, Tvalue> y = new Node<Tkey, Tvalue>();
            while (node.Parent.Color == Color.Red)
            {
                if (node.Parent == node.Parent.Parent.Left)
                {
                    y = node.Parent.Parent.Right;
                    if (y.Color == Color.Red)
                    {
                        node.Parent.Color = Color.Black;
                        y.Color = Color.Black;
                        node.Parent.Parent.Color = Color.Red;
                        node = node.Parent.Parent;
                    }
                    else
                    {
                        if (node == node.Parent.Right)
                        {
                            node = node.Parent;
                            LeftRotate(node);
                        }
                    }
                    node.Parent.Color = Color.Black;
                    if (node.Parent.Parent != Nil && node.Parent.Parent != null)
                    {
                        node.Parent.Parent.Color = Color.Red;
                        RightRotate(node.Parent.Parent);
                    }
                }
                else
                {
                    y = node.Parent.Parent.Left;
                    if (y.Color == Color.Red)
                    {
                        node.Parent.Color = Color.Black;
                        y.Color = Color.Black;
                        node.Parent.Parent.Color = Color.Red;
                        node = node.Parent.Parent;
                    }
                    else
                    {
                        if (node == node.Parent.Left)
                        {
                            node = node.Parent;
                            RightRotate(node);
                        }
                        node.Parent.Color = Color.Black;
                        node.Parent.Parent.Color = Color.Red;
                        LeftRotate(node.Parent.Parent);
                    }
                }
            }
            Root.Color = Color.Black;
        }

        /// <summary>
        /// Method replaces the deleted node with other node
        /// </summary>
        /// <param name="node1">One of the nodes that will be replaced</param>
        /// <param name="node2">Another node for replaaing</param>
        private void Transplant(Node<Tkey, Tvalue> node1, Node<Tkey, Tvalue> node2)
        {
            if (node1.Parent == Nil)
            {
                Root = node2;
            }
            else if (node1 == node1.Parent.Left)
            {
                node1.Parent.Left = node2;
            }
            else
            {
                node1.Parent.Right = node2;
            }
            node2.Parent = node1.Parent;
        }

        /// <summary>
        /// Method finds and returnes an object
        /// </summary>
        /// <param name="key">The key which object is wanted</param>
        /// <returns>Returns a Node type object of specified key</returns>
        public Node<Tkey, Tvalue> GetNode(Tkey key)
        {
            Node<Tkey, Tvalue> node = Root;
            while (node != null)
            {
                if (key.CompareTo(node.Key) < 0)
                {
                    node = node.Left;
                }
                else if (key.CompareTo(node.Key) > 0)
                {
                    node = node.Right;
                }
                else
                {
                    return node;
                }
            }
            return null;
        }

        /// <summary>
        /// Remove the object corresponding the key
        /// </summary>
        /// <param name="key">The key which object will be removed</param>
        /// <returns>Returns true if object removed succesfully</returns>
        public bool Remove(Tkey key)
        {
            Node<Tkey, Tvalue> deletedNode = GetNode(key);
            if (deletedNode == null || deletedNode == Nil) 
            {
                return false;
            }
            keyList.Remove(key);
            valueList.Remove(deletedNode.Value);
            // Node which will be replaced with deleted node
            Node<Tkey, Tvalue> replNode = new Node<Tkey, Tvalue>();
            Node<Tkey, Tvalue> x = deletedNode;
            Color xInitialColor = x.Color;
            if (deletedNode.Left == Nil)
            {
                replNode = deletedNode.Right;
                Transplant(deletedNode, deletedNode.Right);
            }
            else if (deletedNode.Right == Nil)
            {
                replNode = deletedNode.Left;
                Transplant(deletedNode, deletedNode.Left);
            }
            else
            {
                x = Minimum(deletedNode.Right);
                xInitialColor = x.Color;
                replNode = x.Right;
                if (x.Parent == deletedNode)
                {
                    replNode.Parent = x;
                }
                else
                {
                    Transplant(x, x.Right);
                    x.Right = deletedNode.Right;
                    x.Right.Parent = x;
                }
                Transplant(deletedNode, x);
                x.Left = deletedNode.Left;
                x.Left.Parent = x;
                x.Color = deletedNode.Color;
            }
            if (xInitialColor == Color.Black)
            {
                DeleteFixup(replNode);
            }
            this.Count--;
            return true;
        }

        /// <summary>
        /// Method decides the right color of the node
        /// </summary>
        /// <param name="node">node which color must be fixed</param>
        private void DeleteFixup(Node<Tkey, Tvalue> node)
        {
            Node<Tkey, Tvalue> w = new Node<Tkey, Tvalue>();
            while (node != Root && node.Color == Color.Black)
            {
                if (node == node.Parent.Left)
                {
                    w = node.Parent.Right;
                    if (w.Color == Color.Red)
                    {
                        w.Color = Color.Black;
                        node.Parent.Color = Color.Red;
                        LeftRotate(node.Parent);
                        w = node.Parent.Right;
                    }
                    if (w.Left.Color == Color.Black && w.Right.Color == Color.Black)
                    {
                        w.Color = Color.Red;
                        node = node.Parent;
                    }
                    else
                    {
                        if (w.Right.Color == Color.Black)
                        {
                            w.Left.Color = Color.Black;
                            w.Color = Color.Red;
                            RightRotate(w);
                            w = node.Parent.Right;
                        }
                        w.Color = node.Parent.Color;
                        node.Parent.Color = Color.Black;
                        w.Right.Color = Color.Black;
                        LeftRotate(node.Parent);
                        node = Root;
                    }
                }
                else
                {
                    w = node.Parent.Left;
                    if (w.Color == Color.Red)
                    {
                        w.Color = Color.Black;
                        node.Parent.Color = Color.Red;
                        RightRotate(node.Parent);
                        w = node.Parent.Left;
                    }
                    if (w.Left.Color == Color.Black && w.Right.Color == Color.Black)
                    {
                        w.Color = Color.Red; //Black;
                        node = node.Parent;
                    }
                    else
                    {
                        if (w.Left.Color == Color.Black)
                        {
                            w.Right.Color = Color.Black;
                            w.Color = Color.Red; //Black;
                            LeftRotate(w);
                            w = node.Parent.Left;
                        }
                        w.Color = node.Parent.Color;
                        node.Parent.Color = Color.Black;
                        w.Right.Color = Color.Black;
                        RightRotate(node.Parent);
                        node = Root;
                    }
                }
            }
            node.Color = Color.Black;
        }

        /// <summary>
        /// The node which has the smallest key
        /// </summary>
        /// <param name="node">starting node</param>
        /// <returns>Returns the smallest node</returns>
        private Node<Tkey, Tvalue> Minimum(Node<Tkey, Tvalue> node)
        {
            while (node.Left != Nil)
            {
                node = node.Left;
            }
            return node;
        }

        /// <summary>
        /// Finds the searchable node 
        /// </summary>
        /// <param name="key">Key of the node</param>
        /// <returns>Returns true if there is a node with specified key</returns>
        public bool ContainsKey(Tkey key)
        {
            Node<Tkey, Tvalue> node = Root;
            while (node != Nil)
            {
                if (key.CompareTo(node.Key) < 0)
                {
                    node = node.Left;
                }
                else if (key.CompareTo(node.Key) > 0)
                {
                    node = node.Right;
                }
                else
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Gets the value associated with the specified key
        /// </summary>
        /// <param name="key">The key</param>
        /// <param name="value">The value</param>
        /// <returns>Returns true if the tree contains an element with the specified key</returns>
        public bool TryGetValue(Tkey key, out Tvalue value)
        {
            Node<Tkey, Tvalue> node = Root;

            while (node != Nil)
            {
                if (key.CompareTo(node.Key) < 0)
                {
                    node = node.Left;
                }
                else if (key.CompareTo(node.Key) > 0)
                {
                    node = node.Right;
                }
                else
                {
                    value = node.Value;
                    return true;
                }
            }
            value = default(Tvalue);
            return false;
        }

        /// <summary>
        /// Add node in the tree
        /// </summary>
        /// <param name="item">Key and Value pair</param>
        public void Add(KeyValuePair<Tkey, Tvalue> item)
        {
            Add(item.Key, item.Value);
        }

        /// <summary>
        /// Removes all nodes in the tree
        /// </summary>
        public void Clear()
        {
            this.Root = null;
            this.Count = 0;
        }

        /// <summary>
        /// Finds the node with specified key
        /// </summary>
        /// <param name="item">Key and Value pair</param>
        /// <returns>Returns true if there is a node with specified key</returns>
        public bool Contains(KeyValuePair<Tkey, Tvalue> item)
        {
            return ContainsKey(item.Key);
        }

        /// <summary>
        /// Copy nodes
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="elements"></param>
        /// <param name="array">given array</param>
        /// <param name="arrayIndex">start index</param>
        private static void Copy<T>(ICollection<T> elements, T[] array, int arrayIndex)
        {
            if (array == null)
                throw new ArgumentNullException("Array is null");
            if (arrayIndex < 0 || arrayIndex > array.Length)
                throw new ArgumentOutOfRangeException("Invalid Index");
            if ((array.Length - arrayIndex) < elements.Count)
                throw new ArgumentException("Destination array is not large enough. Check array.Length and arrayIndex.");
            foreach (T item in elements)
                array[arrayIndex++] = item;
        }

        /// <summary>
        /// Copy the nodes into given array
        /// </summary>
        /// <param name="array">given array</param>
        /// <param name="arrayIndex">start index</param>
        public void CopyTo(KeyValuePair<Tkey, Tvalue>[] array, int arrayIndex)
        {
            Copy(this, array, arrayIndex);
        }

        /// <summary>
        /// Removes node from the tree
        /// </summary>
        /// <param name="item">Key and Value pair</param>
        /// <returns>Returns true if node is removed succesfully</returns>
        public bool Remove(KeyValuePair<Tkey, Tvalue> item)
        {
            return Remove(item.Key);
        }

        public IEnumerator<KeyValuePair<Tkey, Tvalue>> GetEnumerator()
        {
            KeyValuePair<Tkey, Tvalue>[] pair = new KeyValuePair<Tkey, Tvalue>[this.Count];
            for (int i = 0; i < pair.Length; i++)
            {
                yield return pair[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
