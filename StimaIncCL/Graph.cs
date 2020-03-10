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
        public Queue<GraphNode> bfsQueue;
        public StreamReader populationReader, routesReader;
        public string startNode;
        private int graphSize;
        public float[,] adjMatrix;

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
            adjMatrix = new float[graphSize, graphSize];
            startNode = line.Substring(2);
            Console.WriteLine("Graph size is " + graphSize);
            Console.WriteLine("Starting node : " + startNode);
            for (int i = 0; i < graphSize; i++)
            {
                line = populationReader.ReadLine();
                nodes.Add(new GraphNode(line.Substring(0, 1), Convert.ToInt32(line.Substring(2))));
            }
            (nodes.Find(node => node.getLabel() == startNode)).infected = true;
            (nodes.Find(node => node.getLabel() == startNode)).setTimeInfected(0);
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
            Queue<string> infectionRoute = new Queue<string>();
            bfsQueue.Enqueue(nodes.Find(x => x.getLabel() == startNode)); // Initial push
            while (bfsQueue.Count > 0)
            {
                GraphNode current = bfsQueue.Dequeue();
                string start = current.getLabel();
                Console.WriteLine($"Evaluating Town {start}");
                for (int i = 0; i < graphSize; i++)
                {
                    //Console.WriteLine("I'm here");
                    if ((nodes.Find(node => node.getId() == i)).infected == false && infectionFunc(current, getMatrixAt(current.getId(), i), days))
                    {        
                        //Console.WriteLine("I got here");
                        string end = (nodes.Find(node => node.getId() == i)).getLabel();
                        Console.WriteLine($"Town {start}'s virus spread to Town {end}!");
                        (nodes.Find(node => node.getId() == i)).infected = true;
                        (nodes.Find(node => node.getId() == i)).setTimeInfected(calcInfectionTime(current, getMatrixAt(current.getId(), i)));
                        int dayInfected = (nodes.Find(node => node.getId() == i)).getTimeInfected();
                        Console.WriteLine($"Day Infected : Day {dayInfected}");
                        bfsQueue.Enqueue(nodes.Find(node => node.getId() == i));
                        infectionRoute.Enqueue($"{start} -> {end}");
                    }
                }
            }
            if (infectionRoute.Count > 0)
            {
                string temp;
                Console.WriteLine("Infection Route :");
                while (infectionRoute.Count > 1)
                {
                    temp = infectionRoute.Dequeue();
                    Console.Write($"{temp}, ");
                }
                temp = infectionRoute.Dequeue();
                Console.WriteLine(temp);
            }
            else 
            {
                Console.WriteLine("The virus did not spread");
            }
            Console.WriteLine("Simulation Finished");
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
           gunakan Graph.infectionFunc(a, g.getMatrixAt(a.getId(), b.getId()), T) dengan T
           adalah input interval hari yang didapat dari input user
        */
        public static bool infectionFunc(GraphNode a, float tr, int T)
        {
            float check = Graph.logisticsFunc(a, T - a.getTimeInfected()) * tr;
            return check > 1;
        }

        public static int calcInfectionTime(GraphNode a, float tr)
        {
            float check = (1 / tr);
            // Big risk of infinite loop
            int counter = 0;
            while (logisticsFunc(a, counter) <= check)
            {
                counter++;
            }

            return a.getTimeInfected() + counter;
        }
    }
}