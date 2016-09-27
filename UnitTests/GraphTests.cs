using Algorithms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    [TestClass]
    public class GraphTests
    {
        [TestMethod]
        public void DjikstraShortestPathHappyPath()
        {
            Graph g = new Graph(GraphType.DirectedWeighted);
            g.AddVertex("a");
            g.AddVertex("b");
            g.AddVertex("c");
            g.AddVertex("d");

            g.AddEdge("a", "b", 5);
            g.AddEdge("a", "d", 1);
            g.AddEdge("d", "c", 1);

            var result = g.DjikstraShortestPath("a", "c");
            AssertExtensions.AreEqualArrays(new int[] { 0, 5, 2, 1 }, result.Item1);
        }

        [TestMethod]
        public void DjikstraShortestPathCycle()
        {
            Graph g = new Graph(GraphType.DirectedWeighted);
            g.AddVertex("a");
            g.AddVertex("b");
            g.AddVertex("c");

            g.AddEdge("a", "b", 5);
            g.AddEdge("b", "c", 1);
            g.AddEdge("c", "a", 2);

            var result = g.DjikstraShortestPath("a", "c");
            AssertExtensions.AreEqualArrays(new int[] { 0, 5, 6 }, result.Item1);
        }

        [TestMethod]
        public void DjikstraShortestPathNegativeWeightCycle()
        {
            Graph g = new Graph(GraphType.DirectedWeighted);
            g.AddVertex("a");
            g.AddVertex("b");
            g.AddVertex("c");

            g.AddEdge("a", "b", 5);
            g.AddEdge("b", "c", 1);
            g.AddEdge("c", "a", -7);

            var result = g.DjikstraShortestPath("a", "c");
            AssertExtensions.AreEqualArrays(new int[] { 0, 5, 6 }, result.Item1);
        }

        [TestMethod]
        public void DjikstraShortestPathMostVertices()
        {
            Graph g = new Graph(GraphType.DirectedWeighted);
            for (char i = 'a'; i <= 'z'; i++)
            {
                g.AddVertex(i.ToString());
            }

            for (char i = 'b'; i <= 'z' ; i++)
            {
                g.AddEdge(((char)(i-1)).ToString(), i.ToString(), 1);
            }

            g.AddEdge("a", "z", 30);
            g.AddEdge("m", "z", 15);
            g.AddEdge("c", "y", 23);

            var result = g.DjikstraShortestPath("a", "z");
            
            AssertExtensions.AreEqualArrays(Enumerable.Range('a', 26).Select(n => ((char)n).ToString()).ToArray(), 
                                                                                   result.Item2.ToArray());
        }

        [TestMethod]
        public void BellmanFordShortestPathMostVertices()
        {
            Graph g = new Graph(GraphType.DirectedWeighted);
            for (char i = 'a'; i <= 'z'; i++)
            {
                g.AddVertex(i.ToString());
            }

            for (char i = 'b'; i <= 'z'; i++)
            {
                g.AddEdge(((char)(i - 1)).ToString(), i.ToString(), 1);
            }

            g.AddEdge("a", "z", 30);
            g.AddEdge("m", "z", 15);
            g.AddEdge("c", "y", 23);

            var result = g.BellmanFord("a", "z");

            AssertExtensions.AreEqualArrays(Enumerable.Range('a', 26).Select(n => ((char)n).ToString()).ToArray(),
                                                                                   result.Item2.ToArray());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void BellmanFordDetectNegativeWeightCycle()
        {
            Graph g = new Graph(GraphType.DirectedWeighted);
            g.AddVertex("a");
            g.AddVertex("b");
            g.AddVertex("c");

            g.AddEdge("a", "b", 5);
            g.AddEdge("b", "c", 1);
            g.AddEdge("c", "a", -7);

            g.BellmanFord("a", "c");
        }
    }

    public static class AssertExtensions
    {
        public static bool AreEqualArrays<T>(T[] arr1, T[] arr2)
        {
            if (arr1.Length != arr2.Length) throw new InvalidOperationException("length of arr1 and arr2 does not match");

            for (int i = 0; i < arr1.Length; i++)
            {
                if (!arr1[i].Equals(arr2[i])) throw new InvalidOperationException(string.Format("index expecte={0} actual={1} at index {2}", arr1[i], arr2[i], i));
            }

            return true;
        }
    }
}
