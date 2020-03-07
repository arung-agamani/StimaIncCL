using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StimaIncCL
{
    class Program
    {
        static void Main(string[] args)
        {
            Graph a = new Graph();
            Console.ReadKey();
            a.readRoutes();
            Console.ReadKey();
            a.printGraph();
            Console.ReadKey();
            float fuckYou = Graph.logisticsFunc(a.getNodeWithLabel("A"), 4);
            Console.WriteLine(fuckYou);
            Console.ReadKey();
            bool test = Graph.infectionFunc(a.getNodeWithLabel("A"), a.getNodeWithLabel("B"), a.getMatrixAt(0, 1));
            if (test)
            {
                Console.WriteLine("Node B has been infected by Node A");
            }
            else
            {
                Console.WriteLine("IF ANJING KONTOL BABI MONYET MADESU MEMEK BANGSAT.\nGUA PENGEN TIDUR BANGSAT, GUA PENGEN COLI, GUA PENGEN COLI, GUA PENGEN COLI, GUA PEN MAIN, GUA MAU COLI\nAAAAAAAAAA CROTTTTTTTTTTT");
            }
            Console.ReadKey();
        }
    }
}
