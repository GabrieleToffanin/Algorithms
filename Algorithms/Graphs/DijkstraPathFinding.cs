using System;
using System.Collections.Generic;

namespace AmazonPreparation.Graphs;

public class DijkstraPathFinding
{
    // Finds the shortest path from src to all other vertices in an undirected graph
    public static int[] FindPath(int v, int[,] edges, int src)
    {
        // Get the adjacency matrix from the edges
        int[,] adjacencyMatrix = GetAdjancencyMatrix(v, edges);
        // Alloc distances array
        int[] distances = new int[v];
        // Keep track of visited vertex
        bool[] visited = new bool[v];
        
        // Initialize distances to infinity
        InitializeDistances(v, src, distances);
        
        PriorityQueue<int, int> minHeap = new PriorityQueue<int, int>();
        minHeap.Enqueue(src, 0);

        // While there are elements in the min heap
        FindPaths(minHeap, visited, adjacencyMatrix, distances);
        
        return distances;
    }

    private static void FindPaths(PriorityQueue<int, int> minHeap, bool[] visited, int[,] adjacencyMatrix, int[] distances)
    {
        while (minHeap.Count > 0)
        {
            // Dequeue the vertex with the minimum distance
            var element = minHeap.Dequeue();
            var currentVertex = element;
            
            // Check if already visited
            if (visited[currentVertex])
            {
                continue; // Skip if already visited
            }
            // Mark as visited
            visited[currentVertex] = true;

            // Iterate through all vertices adjacent to the current vertex
            VisitNeighbours(minHeap, visited, adjacencyMatrix, distances, currentVertex);
        }
    }

    private static void VisitNeighbours(PriorityQueue<int, int> minHeap, bool[] visited, int[,] adjacencyMatrix, int[] distances,
        int currentVertex)
    {
        for (int j = 0; j < adjacencyMatrix.GetLength(1); j++)
        {
            // Check if the current vertex is connected to the j-th vertex
            if (adjacencyMatrix[currentVertex, j] > 0 && !visited[j])
            {
                // Calculate the new distance
                int newDistance = distances[currentVertex] + adjacencyMatrix[currentVertex, j];
                    
                // If the new distance is less than the current distance, update
                if (newDistance < distances[j])
                {
                    distances[j] = newDistance;
                    // Enqueue the vertex with the new distance
                    minHeap.Enqueue(j, newDistance);
                }
            }
        }
    }

    private static void InitializeDistances(int v, int src, int[] distances)
    {
        for (int i = 0; i < v; i++)
        {
            distances[i] = int.MaxValue;
        }

        // Distance to src ( self ) is 0
        distances[src] = 0;
    }

    /// <summary>
    /// Returns the adjacency matrix for the given edges.
    /// </summary>
    /// <param name="vertexNumber">Total number of vertexes</param>
    /// <param name="edges">Arcs on each edge</param>
    /// <returns>The adjacency Matrix</returns>
    /// <example> For example, given : V = 5, edges[][] = [[0, 1, 4], [0, 2, 8], [1, 4, 6], [2, 3, 2], [3, 4, 10]]
    /// the resulting adjacency matrix will be : <br/>
    ///      0  1  2  3  4 <br/>
    /// 0:   0  4  8  0  0 <br/>
    /// 1:   4  0  0  0  6 <br/>
    /// 2:   8  0  0  2  0 <br/>
    /// 3:   0  0  2  0 10 <br/>
    /// 4:   0  6  0  10  0 <br/> </example>
    
    private static int[,] GetAdjancencyMatrix(int vertexNumber, int[,] edges)
    {
        // Initialize adjacency matrix with total vertices
        int[,] adjacencyMatrix = new int[vertexNumber, vertexNumber];

        // Run over y edges and fill the adjacency matrix
        for (int i = 0; i < edges.GetLength(0); i++)
        {
            // Each edge is represented as [source, destination, weight]
            int source = edges[i, 0];
            int destination = edges[i, 1];
            int weight = edges[i, 2];

            // Fill the adjacency matrix for both directions since it's undirected
            adjacencyMatrix[source, destination] = weight;
            adjacencyMatrix[destination, source] = weight;
        }
        
        return adjacencyMatrix;
    }
}