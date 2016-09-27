using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    public class Graph
    {
        private Dictionary<string, List<Edge>> vertexEdgeMap;
        private List<string> vertices;
        private GraphType type;

        public Graph(GraphType type)
        {
            this.vertexEdgeMap = new Dictionary<string, List<Edge>>();
            this.vertices = new List<string>();
            this.type = type;
        }

        public void AddVertex(string vertex)
        {
            this.AddVertex(vertex, new List<Edge>());
        }

        public void AddVertex(string vertex, List<Edge> neighbors)
        {
            this.vertices.Add(vertex);
            this.vertexEdgeMap.Add(vertex, neighbors);
        }

        public void AddEdge(string from, string to, int weight)
        {
            this.vertexEdgeMap[from].Add(new Edge() { To = to, Weight = weight });

            if (this.type == GraphType.Undirect || this.type == GraphType.UndirectedWeighted)
            {
                this.vertexEdgeMap[to].Add(new Edge() { To = from, Weight = weight });
            }
        }

        public void Print()
        {
            foreach (var vertex in vertexEdgeMap)
            {
                foreach (var edge in vertex.Value)
                {
                    Console.WriteLine("{0} to {1} with weight {2}", vertex.Key, edge.To, edge.Weight);
                }
            }
        }

        #region Graph Algorithms

        public Tuple<int[], LinkedList<string>> DjikstraShortestPath(string source, string destination)
        {
            // for each vertex in the graph goto all it's neighbors and update their distance
            // value only if the current computed value is larger
            // pick the one with minimum weight in every iteration

            Dictionary<string, int> distanceFromSource = new Dictionary<string, int>();
            Dictionary<string, string> prev = new Dictionary<string, string>();
            distanceFromSource[source] = 0;

            MinHeapWithNoDuplicates<string, int> queue = new MinHeapWithNoDuplicates<string, int>();
            queue.Insert(source, 0);
            foreach (var vertex in this.vertices)
            {
                if (!vertex.Equals(source))
                {
                    distanceFromSource[vertex] = int.MaxValue;
                    prev[vertex] = null;
                    queue.Insert(vertex, int.MaxValue);
                }
            }

            while (!queue.IsEmpty())
            {
                string vertex = queue.Extract();
                foreach (var edge in this.vertexEdgeMap[vertex])
                {
                    if (distanceFromSource[vertex] + edge.Weight < distanceFromSource[edge.To])
                    {
                        distanceFromSource[edge.To] = distanceFromSource[vertex] + edge.Weight;
                        prev[edge.To] = vertex;
                        queue.UpdatePriority(edge.To, distanceFromSource[edge.To]);
                    }
                }
            }

            LinkedList<string> shortestPath = new LinkedList<string>();

            string v = destination;
            while (prev[v] != null)
            {
                shortestPath.AddFirst(v);
                v = prev[v];
            }

            shortestPath.AddFirst(source);
            return Tuple.Create<int[], LinkedList<string>>(distanceFromSource.Values.ToArray(), shortestPath);
        }

        public Tuple<int[], LinkedList<string>> BellmanFord(string source, string destination)
        {
            // set distance from source to infinity
            Dictionary<string, int> distanceFromSource = new Dictionary<string, int>();
            Dictionary<string, string> prev = new Dictionary<string, string>();
            
            foreach (var vertex in this.vertices)
            {
                distanceFromSource[vertex] = int.MaxValue;
                prev[vertex] = null;
            }

            distanceFromSource[source] = 0;

            for (int i = 0; i < this.vertices.Count - 1; i++)
            {
                foreach (var v in vertices)
                {
                    foreach (var edge in this.vertexEdgeMap[v])
                    {
                        if (distanceFromSource[v] + edge.Weight < distanceFromSource[edge.To])
                        {
                            distanceFromSource[edge.To] = distanceFromSource[v] + edge.Weight;
                            prev[edge.To] = v;
                        }
                    }
                }

            }

            foreach (var v in vertices)
            {
                foreach (var edge in this.vertexEdgeMap[v])
                {
                    if (distanceFromSource[v] + edge.Weight < distanceFromSource[edge.To])
                    {
                        throw new InvalidOperationException("Graph contains negative weight cycles");
                    }
                }
            }

            LinkedList<string> shortestPath = new LinkedList<string>();

            string dest = destination;
            while (prev[dest] != null)
            {
                shortestPath.AddFirst(dest);
                dest = prev[dest];
            }

            shortestPath.AddFirst(source);
            return Tuple.Create<int[], LinkedList<string>>(distanceFromSource.Values.ToArray(), shortestPath);
        }

        public void TopoSortShortestPath(string source, string destination)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    public class Edge
    {
        public string To { get; set; }
        public int Weight { get; set; }
    }

    public enum GraphType
    {
        Undirect,
        Directed,
        UndirectedWeighted,
        DirectedWeighted
    }
}
