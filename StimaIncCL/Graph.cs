using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace StimaIncCL
{
    public class Graph
    {
        public List<GraphNode> nodes;
        private Queue<GraphNode> bfsQueue;
        private StreamReader populationReader, routesReader;
        private string startNode;
        private int graphSize;
        private int[,] adjMatrix;
        private float[,] trMatrix;

        public Graph()
        {
            Console.WriteLine("New graph object has been created!");
            this.nodes = new List<GraphNode>();
            this.bfsQueue = new Queue<GraphNode>();
            populationReader = new StreamReader("../../population.txt");
            // routesReader = new StreamReader("routes.txt");
            /**
             * To do list :
             * 1. Read first line to get number of nodes and initial city
             * 2. Create nodes with information about label and population count
             * 3. Make adjadency matrix, initialize with zeros, read from routes.txt
             * 4. How to handle the traveling probability tho...
             */
            string line;
            line = populationReader.ReadLine();
            graphSize = Convert.ToInt32(line.Substring(0, 1));
            adjMatrix = new int[graphSize, graphSize];
            startNode = line.Substring(2);
            Console.WriteLine("Graph size is " + graphSize);
            Console.WriteLine("Starting node : " + startNode);
            for (int i = 0; i < graphSize; i++)
            {
                line = populationReader.ReadLine();
                nodes.Add(new GraphNode(line.Substring(0, 1), Convert.ToInt32(line.Substring(2))));
            }
        }

        public void printGraph()
        {
            Console.WriteLine("Printing graph content...");
            foreach (var node in nodes)
            {
                Console.WriteLine("Node label : " + node.getLabel() + ", populationCount : " + node.getPopulationCount());
                // Print node's relation
                for (int i = 0; i < nodes.Count; i++)
                {
                    if (adjMatrix[node.getId(),i] != -99)
                    {
                        Console.WriteLine(node.getLabel() + " relates with id " + nodes.Find(n => n.getId() == i).getLabel() + " with Tr: " + adjMatrix[node.getId(), i]);
                    }
                }
            }
        }

        public void searchBFS(int day)
        {
            // Start from startNode

        }
    }
}
