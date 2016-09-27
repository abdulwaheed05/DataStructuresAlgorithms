using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    class Program
    {
        static void Main(string[] args)
        {
            Graph g = new Graph(GraphType.DirectedWeighted);

            g.AddVertex("a");
            g.AddVertex("b");
            g.AddVertex("c");
            g.AddVertex("d"); 
            g.AddVertex("e");

            g.AddEdge("a", "b", 3);
            g.AddEdge("a", "c", 2);
            g.AddEdge("b", "c", 1);
            g.AddEdge("c", "d", 6);
            g.AddEdge("d", "e", 4);
            g.AddEdge("b", "e", 2);

            g.Print();

            int[] result = g.DjikstraShortestPath("a", "e").Item1;

            Console.ReadLine();
        }
    }
}
