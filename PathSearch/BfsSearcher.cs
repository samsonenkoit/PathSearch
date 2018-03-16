using PathSearch.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathSearch
{
    /// <summary>
    /// Реализаци алгоритма поиска в ширину
    /// </summary>
    public class BfsSearcher : IPathSearcher
    {
        private class PathItem<T>
        {
            public PathItem<T> PreviousPathItem { get; }

            public T Node { get; }

            public PathItem(PathItem<T> previousPathItem, T node )
            {
                PreviousPathItem = previousPathItem;
                Node = node;
            }

            public override int GetHashCode()
            {
                return Node.GetHashCode();
            }
        }

        /// <summary>
        /// Возвращает кратчайший путь
        /// </summary>
        /// <typeparam name="T">Тип узла графа. Важно что бы T корректно реализовывал Equalse и GetHashCode</typeparam>
        /// <param name="graph"></param>
        /// <param name="startNode"></param>
        /// <param name="endNode"></param>
        /// <returns></returns>
        public IEnumerable<T> Search<T>(Graph<T> graph, T startNode, T endNode)
        {
            if (graph == null)
                throw new ArgumentNullException(nameof(graph));

            if (startNode.Equals(endNode))
                return new T[0];

            var visitedNodes = new Dictionary<int, PathItem<T>>();
            var queue = new Queue<PathItem<T>>();
            queue.Enqueue(new PathItem<T>(null, startNode));

            while(queue.Count > 0)
            {
                var pathItem = queue.Dequeue();

                if (visitedNodes.ContainsKey(pathItem.GetHashCode()))
                    continue;

                visitedNodes.Add(pathItem.GetHashCode(), pathItem);

                foreach(var connectedNode in graph.GetConnected(pathItem.Node))
                {
                    if (connectedNode.Equals(endNode))
                    {
                        var endPath = new PathItem<T>(pathItem, endNode);
                        visitedNodes.Add(endPath.GetHashCode(), endPath);
                        break;
                    }
                    else if (!visitedNodes.ContainsKey(connectedNode.GetHashCode()))
                    {
                        queue.Enqueue(new PathItem<T>(pathItem, connectedNode));
                    }
                }

            }

            return GetPathFromDictionary(visitedNodes, endNode);
        }
        
        private IEnumerable<T> GetPathFromDictionary<T>(Dictionary<int, PathItem<T>> pathDict, T endNode)
        {
            if (!pathDict.TryGetValue(endNode.GetHashCode(), out var endPathItem))
                return new T[0];
            else
            {
                Stack<T> pathStack = new Stack<T>();

                pathStack.Push(endPathItem.Node);
                PathItem<T> currentPathItem = endPathItem.PreviousPathItem;
                while (currentPathItem != null)
                {
                    pathStack.Push(currentPathItem.Node);
                    currentPathItem = currentPathItem.PreviousPathItem;
                }

                return pathStack.ToList();
            }
        }
    }
}
