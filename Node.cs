using System;

namespace DictionaryDataStructure
{
    // Color of nodes
    public enum Color { Red = 1, Black = 2 }

    /// <summary>
    /// Node class
    /// </summary>
    /// <typeparam name="Tkey">Key of the node</typeparam>
    /// <typeparam name="Tvalue">Value of the node</typeparam>
    public class Node<Tkey, Tvalue> where Tkey:IComparable<Tkey> 
    {
        // Color of the node
        public Color Color;

        // Key of the node
        public Tkey Key { set; get; }

        // Value of the node
        public Tvalue Value { set; get; }

        // Parent of the node
        public Node<Tkey, Tvalue> Parent { get; set; }

        // Right child of the node
        public Node<Tkey, Tvalue> Right { set; get; }

        // Left child of the node
        public Node<Tkey, Tvalue> Left { set; get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="value">Value</param>
        public Node(Tkey key, Tvalue value)
        {
            this.Key = key;
            this.Value = value;
            this.Color = Color.Red;
        }
        
        /// <summary>
        /// Parameterless constructor
        /// </summary>
        public Node()
        {
            this.Color = Color.Red;
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="obj">comparable object</param>
        /// <returns>Returns boolean value</returns>
        public override bool Equals(object obj)
        {
            Node<Tkey, Tvalue> objnode = (Node<Tkey, Tvalue>)obj;
            if (this.Key.CompareTo(objnode.Key) == 0 && this.Color == objnode.Color)
            {
                return true;
            }
            else
                return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return this.Key + " " + this.Color.ToString(); 
        }
    }
}
