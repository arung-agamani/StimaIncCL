using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;


namespace StimaIncCL
{
    public class Graph
    {
        public List<GraphNode> nodes;
        public Queue<GraphNode> bfsQueue, childQueue;
        public StreamReader populationReader, routesReader;
        public string startNode;
        private int graphSize;
        public float[,] adjMatrix;

        public Graph()
        {
            Console.WriteLine("New graph object has been created!");
            this.nodes = new List<GraphNode>();
            this.bfsQueue = new Queue<GraphNode>();
            this.childQueue = new Queue<GraphNode>();
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
            adjMatrix = new float[graphSize, graphSize];
            startNode = line.Substring(2);
            Console.WriteLine("Graph size is " + graphSize);
            Console.WriteLine("Starting node : " + startNode);
            for (int i = 0; i < graphSize; i++)
            {
                line = populationReader.ReadLine();
                nodes.Add(new GraphNode(line.Substring(0, 1), Convert.ToInt32(line.Substring(2))));
            }
        }

        public float getMatrixAt(int row, int column)
        {
            return adjMatrix[row, column];
        }

        public GraphNode getNodeWithLabel (string lab)
        {
            return nodes.Find(x => x.getLabel() == lab);
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

        public void searchBFS(int days)
        {
            bfsQueue.Enqueue(nodes.Find(x => x.getLabel() == startNode)); // Initial push
            while (bfsQueue.Count > 0)
            {
                GraphNode current = bfsQueue.Dequeue();
                for (int i = 0; i < graphSize; i++)
                {
                    if (getMatrixAt(current.getId(), i) != 0 && infectionFunc(current, nodes.Find(x => x.getId() == i), getMatrixAt(current.getId(), i)))
                    {
                        // buat fungsi kapan tersebar
                        // 
                    }
                }
            }
        }

        public void readRoutes()
        {
            routesReader = new StreamReader("../../routes.txt");
            string line;
            int counter, firstIdx, secondIdx;
            counter = Convert.ToInt32(routesReader.ReadLine());
            Console.WriteLine($"There are {counter} infection routes");

            while (counter > 0)
            {
                line = routesReader.ReadLine();
                firstIdx = (nodes.Find(node => node.getLabel() == (line.Substring(0, 1)))).getId();
                secondIdx = (nodes.Find(node => node.getLabel() == (line.Substring(2, 1)))).getId();
                adjMatrix[firstIdx, secondIdx] = float.Parse(line.Substring(4), CultureInfo.InvariantCulture);

                Console.WriteLine($"Matrix index {firstIdx},{secondIdx} has been initiated with value {adjMatrix[firstIdx, secondIdx]}");
                counter--;
            }
        }

        /* int ta = t(A) yang ada di spek tubes gayn */
        public static float logisticsFunc(GraphNode a, int ta)
        {
            float numerator = (float)a.getPopulationCount();
            float denominator = (float)(1 + (a.getPopulationCount() - 1) * (Math.Pow(Math.E, ta * (-0.25))));
            return (numerator / denominator);
        }

        /* Untuk ngecek apakah node b berhasil diinfeksi node a, misal ada instance Graph g, 
           gunakan Graph.infectionFunc(a, b, g.getMatrixAt(A.getId(), B.getId())) 
           Also ubah 4 dalam fungsi ini jadi a.gett()*/
        public static bool infectionFunc(GraphNode a, GraphNode b, float tr)
        {
            float check = Graph.logisticsFunc(a, 4) * tr;
            return check > 1;
        }
    }
}