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
        }
    }
}
