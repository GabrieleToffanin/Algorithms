using AmazonPreparation.Graphs;

namespace Algorithms.Tests;

public class DijkstraPathFindingTests
{
    [Fact]
    public void FindPath_ShouldReturnShortestDistances()
    {
        // Arrange
        int v = 5;
        int[,] edges = new int[,]
        {
            { 0, 1, 4 },
            { 0, 2, 8 },
            { 1, 4, 6 },
            { 2, 3, 2 },
            { 3, 4, 10 }
        };
        int src = 0;

        // Act
        var result = DijkstraPathFinding.FindPath(v, edges, src);

        // Assert
        Assert.Equal(new[] { 0, 4, 8, 10, 10 }, result);
    }

    [Fact]
    public void FindPath_SingleVertex_ShouldReturnZero()
    {
        // Arrange
        int v = 1;
        int[,] edges = new int[0, 3]; // No edges
        int src = 0;

        // Act
        var result = DijkstraPathFinding.FindPath(v, edges, src);

        // Assert
        Assert.Equal(new[] { 0 }, result);
    }

    [Fact]
    public void FindPath_DisconnectedGraph_ShouldReturnInfinityForUnreachable()
    {
        // Arrange
        int v = 4;
        int[,] edges = new int[,]
        {
            { 0, 1, 5 },  // 0 -> 1 with weight 5
            { 2, 3, 3 }   // 2 -> 3 with weight 3 (disconnected component)
        };
        int src = 0;

        // Act
        var result = DijkstraPathFinding.FindPath(v, edges, src);

        // Assert
        Assert.Equal(0, result[0]); // Distance to self
        Assert.Equal(5, result[1]); // Distance to vertex 1
        Assert.Equal(int.MaxValue, result[2]); // Unreachable vertex 2
        Assert.Equal(int.MaxValue, result[3]); // Unreachable vertex 3
    }

    [Fact]
    public void FindPath_LinearGraph_ShouldReturnCumulativeDistances()
    {
        // Arrange - Linear chain: 0-1-2-3
        int v = 4;
        int[,] edges = new int[,]
        {
            { 0, 1, 2 },
            { 1, 2, 3 },
            { 2, 3, 4 }
        };
        int src = 0;

        // Act
        var result = DijkstraPathFinding.FindPath(v, edges, src);

        // Assert
        Assert.Equal(new[] { 0, 2, 5, 9 }, result);
    }

    [Fact]
    public void FindPath_CompleteGraph_ShouldReturnDirectDistances()
    {
        // Arrange - Complete graph with 3 vertices
        int v = 3;
        int[,] edges = new int[,]
        {
            { 0, 1, 10 },
            { 0, 2, 5 },
            { 1, 2, 2 }
        };
        int src = 0;

        // Act
        var result = DijkstraPathFinding.FindPath(v, edges, src);

        // Assert
        Assert.Equal(new[] { 0, 7, 5 }, result); // 0->2 (5), 0->2->1 (7)
    }

    [Fact]
    public void FindPath_DifferentSourceVertex_ShouldWorkCorrectly()
    {
        // Arrange
        int v = 4;
        int[,] edges = new int[,]
        {
            { 0, 1, 1 },
            { 1, 2, 2 },
            { 2, 3, 3 },
            { 0, 3, 10 }
        };
        int src = 2; // Start from vertex 2

        // Act
        var result = DijkstraPathFinding.FindPath(v, edges, src);

        // Assert
        Assert.Equal(3, result[0]); // 2->1->0 = 2+1 = 3
        Assert.Equal(2, result[1]); // 2->1 = 2
        Assert.Equal(0, result[2]); // Distance to self
        Assert.Equal(3, result[3]); // 2->3 = 3
    }

    [Fact]
    public void FindPath_TriangleWithShortcut_ShouldChooseOptimalPath()
    {
        // Arrange - Triangle where direct path is better than going around
        int v = 3;
        int[,] edges = new int[,]
        {
            { 0, 1, 4 },
            { 1, 2, 3 },
            { 0, 2, 2 } // Direct shortcut
        };
        int src = 0;

        // Act
        var result = DijkstraPathFinding.FindPath(v, edges, src);

        // Assert
        Assert.Equal(new[] { 0, 4, 2 }, result); // Direct path 0->2 is optimal
    }

    [Fact]
    public void FindPath_StarGraph_ShouldReturnCorrectDistances()
    {
        // Arrange - Star graph with center at vertex 0
        int v = 5;
        int[,] edges = new int[,]
        {
            { 0, 1, 1 },
            { 0, 2, 2 },
            { 0, 3, 3 },
            { 0, 4, 4 }
        };
        int src = 0;

        // Act
        var result = DijkstraPathFinding.FindPath(v, edges, src);

        // Assert
        Assert.Equal(new[] { 0, 1, 2, 3, 4 }, result);
    }

    [Fact]
    public void FindPath_SameWeightEdges_ShouldFindShortestPath()
    {
        // Arrange - Graph where all edges have same weight
        int v = 4;
        int[,] edges = new int[,]
        {
            { 0, 1, 1 },
            { 0, 2, 1 },
            { 1, 3, 1 },
            { 2, 3, 1 }
        };
        int src = 0;

        // Act
        var result = DijkstraPathFinding.FindPath(v, edges, src);

        // Assert
        Assert.Equal(new[] { 0, 1, 1, 2 }, result); // Can reach 3 via either 1 or 2
    }
}