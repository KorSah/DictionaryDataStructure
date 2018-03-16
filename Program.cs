using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;


namespace DictionaryDataStructure
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<int, string> dictionary = new Dictionary<int, string>();
            Stopwatch watchDictioanry = Stopwatch.StartNew();
            for (int i = 0; i < 640; i++)
            {
                dictionary.Add(i, "dictionary");
            }
            watchDictioanry.Stop();
            Console.WriteLine(watchDictioanry.ElapsedMilliseconds);

            RedBlackTree<int, string> tree = new RedBlackTree<int, string>();
            Stopwatch watchTree = Stopwatch.StartNew();
            for (int i = 0; i < 320; i++)
            {
                tree.Add(i, "tree");
            }
            watchTree.Stop();
            Console.WriteLine(watchTree.ElapsedMilliseconds);
            RedBlackTree<int, string> rb = new RedBlackTree<int, string>();
            
        }
    }
}

