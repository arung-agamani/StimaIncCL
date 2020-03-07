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
        public Boolean visited;
        private string label;
        private int populationCount;
        private int infectedCount;

        public GraphNode(string label, int populationCount, int infectedCount)
        {
            this.label = label;
            this.populationCount = populationCount;
            this.infectedCount = infectedCount;
            this.visited = false;
            Console.WriteLine("GraphNode() => label : " + label + ", populationCount : " + populationCount + ", infectedCount : " + infectedCount);
        }

        public GraphNode(string label, int populationCount)
        {
            this.label = label;
            this.populationCount = populationCount;
            Console.WriteLine("GraphNode() => label : " + label + " and populationCount : " + populationCount);
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