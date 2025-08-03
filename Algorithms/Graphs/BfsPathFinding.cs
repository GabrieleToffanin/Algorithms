using System.Linq;

namespace AmazonPreparation.Graphs;

public class BfsPathFinding
{
    /// <summary>
    /// Finds the shortest path between source and destination using BFS.
    /// ASSUMPTION: All edges have uniform weight of 6.
    /// </summary>
    /// <param name="graph">The graph represented as adjacency list</param>
    /// <param name="src">Source node</param>
    /// <param name="dest">Destination node</param>
    /// <returns>Shortest distance (number of hops * 6), or -1 if no path exists</returns>
    public static int SolvePath(Dictionary<int, List<int>> graph, int src, int dest){
        if (src == dest) return 0;
        if (!graph.ContainsKey(src)) return -1;
        
        var queue = new Queue<int>();
        var visited = new HashSet<int>();
        var distances = new Dictionary<int,int>();
        
        queue.Enqueue(src);
        visited.Add(src);
        distances[src] = 0;
        
        while(queue.Count > 0){
            var current_node = queue.Dequeue();
            var current_distance = distances[current_node];
            
            if (graph.ContainsKey(current_node)){
                foreach(var neighbour in graph[current_node])
                {
                    if (neighbour == dest)
                        return current_distance + 6;
                    
                    if (!visited.Contains(neighbour)){
                        visited.Add(neighbour);
                        distances[neighbour] = current_distance + 6;
                        queue.Enqueue(neighbour);
                    }
                }
            }
        }
        
        return -1;
    }
}