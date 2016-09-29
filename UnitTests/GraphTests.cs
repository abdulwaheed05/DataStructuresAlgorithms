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

        [TestMethod]
        public void TopologicalSortDFSTest()
        {
            Graph g = new Graph(GraphType.DirectedWeighted);
            g.AddVertex("a");
            g.AddVertex("b");
            g.AddVertex("c");
            g.AddVertex("d");
            g.AddVertex("e");

            g.AddEdge("a", "b", 5);
            g.AddEdge("a", "c", 1);
            g.AddEdge("c", "d", -7);
            g.AddEdge("b", "d", 15);
            g.AddEdge("d", "e", 10);

            var result = g.TopologicalSortDFS();
            AssertExtensions.AreEqualLists(new List<string> { "e", "d", "b", "c", "a" }, result);
        }

        [TestMethod]
        public void TopologicalSortDFSSingleVertexTest()
        {
            Graph g = new Graph(GraphType.DirectedWeighted);
            g.AddVertex("a");

            var result = g.TopologicalSortDFS();
            AssertExtensions.AreEqualLists(new List<string> { "a" }, result);
        }

        [TestMethod]
        public void TopologicalSortDFSTwoVerticesTest()
        {
            Graph g = new Graph(GraphType.DirectedWeighted);
            g.AddVertex("a");
            g.AddVertex("b");
            g.AddEdge("b", "a", 10);

            var result = g.TopologicalSortDFS();
            AssertExtensions.AreEqualLists(new List<string> { "a", "b" }, result);
        }

        [TestMethod]
        public void TopologicalSortKahnsAlgorithmTest()
        {
            Graph g = new Graph(GraphType.DirectedWeighted);
            g.AddVertex("a");
            g.AddVertex("b");
            g.AddVertex("c");
            g.AddVertex("d");
            g.AddVertex("e");

            g.AddEdge("a", "b", 5);
            g.AddEdge("a", "c", 1);
            g.AddEdge("c", "d", -7);
            g.AddEdge("b", "d", 15);
            g.AddEdge("d", "e", 10);

            var result = g.TopologicalSortKahnsAlgorithm();
            AssertExtensions.AreEqual(new List<string> { "e", "d", "c", "b", "a" }, result);
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

        public static bool AreEqualLists<T>(List<T> expected, List<T> actual)
        {
            if (expected.Count != actual.Count) throw new InvalidOperationException("length of list1 and list2 does not match");

            for (int i = 0; i < expected.Count; i++)
            {
                if (!expected[i].Equals(actual[i])) throw new InvalidOperationException(string.Format("index expected={0} actual={1} at index {2}", expected[i], actual[i], i));
            }

            return true;
        }

        public static bool AreEqual<T>(IEnumerable<T> expected, IEnumerable<T> actual)
        {
            using(var a = actual.GetEnumerator())
            using (var e = expected.GetEnumerator())
            {
                while (a.MoveNext() && e.MoveNext())
                {
                    if (!e.Current.Equals(a.Current)) throw new InvalidOperationException(string.Format("expected={0} actual={1}", e.Current, a.Current));
                }

                if (a.MoveNext() ^ e.MoveNext())
                {
                    throw new InvalidOperationException("length of sequence is not the same");
                }
            }

            return true;
        }
    }
}
