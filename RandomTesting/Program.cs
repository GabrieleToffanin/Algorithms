using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

/// <summary>
/// Consider an undirected graph consisting of  nodes where each node is labeled from  to  and the edge between any two nodes
/// is always of length . We define node  to be the starting position for a BFS.
/// Given a graph, determine the distances from the start node to each of its descendants and return the list
/// in node number order, ascending. If a node is disconnected, it's distance should be .
/// For example, there are  nodes in the graph with a starting node . The list of , and each has a weight of .
/// </summary>
public class Program {
    
    public static void Main(string[] args) {
        /* Enter your code here. Read input from STDIN. Print output to STDOUT. Your class should be named Solution */
        
        var result = Solve();
        
        foreach(var list in result){
            foreach(var item in list){
                Console.Out.Write(item);
                Console.Out.Write(' ');
            }
            Console.Out.WriteLine();
        }
    }
    
    private static List<List<int>> Solve(){
        var src = Console.In.ReadLine();
        List<List<int>> result = new();
        
        while(Console.In.Peek() is not -1){
            var countAndArcs = Console.In.ReadLine().Split(' ');
            var totalNodes = int.Parse(countAndArcs[0]);
            var totalArcs = int.Parse(countAndArcs[1]);
            
            Dictionary<int, List<int>> graph = new();
            
            for (int i = 0; i < totalNodes; i++)
            {
                graph[i + 1] = new List<int>();
            }
            
            for (int i = 0; i < totalArcs; i++){
                string[] currentLine = Console.In.ReadLine().Split(' ');

                var target = int.Parse(currentLine[1]);
                var startingPos = int.Parse(currentLine[0]);
                
                graph[startingPos].Add(target);
                graph[target].Add(startingPos);
                
            }

            src = Console.In.ReadLine();
            var startingNode = GetQueryToken(src);
            
            result.Add(SolveFor(startingNode, graph));
        }
        
        return result;
    }
    
    private static List<int> SolveFor(int src, Dictionary<int, List<int>> graph){
        var result = new List<int>();
        
        foreach(var key in graph.Keys.Where(k => k != src).OrderBy(k => k)){
            result.Add(SolvePath(graph, src, key));
        }
        
        return result;
    }
    
    private static int SolvePath(Dictionary<int, List<int>> graph, int src, int dest){
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
    
    private static int GetQueryToken(string token)
    => int.Parse(token);
}