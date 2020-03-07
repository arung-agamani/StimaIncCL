using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StimaIncCL
{
    public class GraphNode
    {
        private int id;
        private string label;
        private int populationCount;
        private int infectedCount;
        public static int nodeCount;

        public GraphNode(string label, int populationCount, int infectedCount = 0)
        {
            this.label = label;
            this.populationCount = populationCount;
            this.infectedCount = infectedCount;
            this.id = nodeCount;
            nodeCount++;
            //Console.WriteLine("GraphNode() => label : " + label + ", populationCount : " + populationCount + ", infectedCount : " + infectedCount);
            if (infectedCount > 0)
            {
                Console.WriteLine("GraphNode() => label : " + label + ", populationCount : " + populationCount + ", infectedCount : " + infectedCount);
            }
            else
            {
                Console.WriteLine("GraphNode() => label : " + label + " and populationCount : " + populationCount);
            }
        }

        /*public GraphNode(string label, int populationCount)
        {
            this.label = label;
            this.populationCount = populationCount;
            this.id = nodeCount;
            nodeCount++;
            Console.WriteLine("GraphNode() => label : " + label + " and populationCount : " + populationCount);
        }*/

        static GraphNode()
        {
            nodeCount = 0;
        }

        public string getLabel()
        {
            return this.label;
        }

        public int getPopulationCount()
        {
            return this.populationCount;
        }

        public int getInfectedCount()
        {
            return this.infectedCount;
        }

        public int getId()
        {
            return this.id;
        }
    }
}