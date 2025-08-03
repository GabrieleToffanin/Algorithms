using AmazonPreparation.Graphs;

namespace Algorithms.Tests;

public class BfsPathFindingTests
{
    [Fact]
    public void SolvePath_SameSourceAndDestination_ShouldReturnZero()
    {
        // Arrange
        var graph = new Dictionary<int, List<int>>
        {
            { 1, new List<int> { 2, 3 } },
            { 2, new List<int> { 1, 4 } }
        };

        // Act
        var result = BfsPathFinding.SolvePath(graph, 1, 1);

        // Assert
        Assert.Equal(0, result);
    }

    [Fact]
    public void SolvePath_SourceNotInGraph_ShouldReturnMinusOne()
    {
        // Arrange
        var graph = new Dictionary<int, List<int>>
        {
            { 1, new List<int> { 2, 3 } },
            { 2, new List<int> { 1, 4 } }
        };

        // Act
        var result = BfsPathFinding.SolvePath(graph, 5, 2);

        // Assert
        Assert.Equal(-1, result);
    }

    [Fact]
    public void SolvePath_DirectConnection_ShouldReturnSix()
    {
        // Arrange
        var graph = new Dictionary<int, List<int>>
        {
            { 1, new List<int> { 2, 3 } },
            { 2, new List<int> { 1, 4 } },
            { 3, new List<int> { 1 } }
        };

        // Act
        var result = BfsPathFinding.SolvePath(graph, 1, 2);

        // Assert
        Assert.Equal(6, result);
    }

    [Fact]
    public void SolvePath_TwoHopsPath_ShouldReturnTwelve()
    {
        // Arrange
        var graph = new Dictionary<int, List<int>>
        {
            { 1, new List<int> { 2 } },
            { 2, new List<int> { 3 } },
            { 3, new List<int> { 4 } }
        };

        // Act
        var result = BfsPathFinding.SolvePath(graph, 1, 3);

        // Assert
        Assert.Equal(12, result);
    }

    [Fact]
    public void SolvePath_NoPathExists_ShouldReturnMinusOne()
    {
        // Arrange
        var graph = new Dictionary<int, List<int>>
        {
            { 1, new List<int> { 2 } },
            { 2, new List<int> { 1 } },
            { 3, new List<int> { 4 } },
            { 4, new List<int> { 3 } }
        };

        // Act
        var result = BfsPathFinding.SolvePath(graph, 1, 4);

        // Assert
        Assert.Equal(-1, result);
    }

    [Fact]
    public void SolvePath_ComplexGraph_ShouldFindShortestPath()
    {
        // Arrange
        var graph = new Dictionary<int, List<int>>
        {
            { 1, new List<int> { 2, 3 } },
            { 2, new List<int> { 1, 4, 5 } },
            { 3, new List<int> { 1, 6 } },
            { 4, new List<int> { 2, 7 } },
            { 5, new List<int> { 2, 7 } },
            { 6, new List<int> { 3 } },
            { 7, new List<int> { 4, 5 } }
        };

        // Act
        var result = BfsPathFinding.SolvePath(graph, 1, 7);

        // Assert
        Assert.Equal(18, result); // Path: 1 -> 2 -> 4 -> 7 (3 hops = 18)
    }

    [Fact]
    public void SolvePath_LinearPath_ShouldReturnCorrectDistance()
    {
        // Arrange
        var graph = new Dictionary<int, List<int>>
        {
            { 1, new List<int> { 2 } },
            { 2, new List<int> { 3 } },
            { 3, new List<int> { 4 } },
            { 4, new List<int> { 5 } }
        };

        // Act
        var result = BfsPathFinding.SolvePath(graph, 1, 5);

        // Assert
        Assert.Equal(24, result); // 4 hops: 1->2->3->4->5 = 4 * 6 = 24
    }

    [Fact]
    public void SolvePath_EmptyGraph_ShouldReturnMinusOne()
    {
        // Arrange
        var graph = new Dictionary<int, List<int>>();

        // Act
        var result = BfsPathFinding.SolvePath(graph, 1, 2);

        // Assert
        Assert.Equal(-1, result);
    }

    [Fact]
    public void SolvePath_SingleNodeGraph_SourceEqualsDestination_ShouldReturnZero()
    {
        // Arrange
        var graph = new Dictionary<int, List<int>>
        {
            { 1, new List<int>() }
        };

        // Act
        var result = BfsPathFinding.SolvePath(graph, 1, 1);

        // Assert
        Assert.Equal(0, result);
    }

    [Fact]
    public void SolvePath_CyclicGraph_ShouldFindShortestPath()
    {
        // Arrange
        var graph = new Dictionary<int, List<int>>
        {
            { 1, new List<int> { 2, 4 } },
            { 2, new List<int> { 1, 3 } },
            { 3, new List<int> { 2, 4 } },
            { 4, new List<int> { 1, 3 } }
        };

        // Act
        var result = BfsPathFinding.SolvePath(graph, 1, 3);

        // Assert
        Assert.Equal(12, result); // Shortest path: 1 -> 2 -> 3 or 1 -> 4 -> 3 (both are 2 hops)
    }

    [Fact]
    public void SolvePath_UniformWeightAssumption_AllDistancesAreMultiplesOfSix()
    {
        // Arrange - Create a graph where we test multiple paths
        var graph = new Dictionary<int, List<int>>
        {
            { 1, new List<int> { 2, 3 } },
            { 2, new List<int> { 1, 4 } },
            { 3, new List<int> { 1, 5 } },
            { 4, new List<int> { 2, 6 } },
            { 5, new List<int> { 3, 6 } },
            { 6, new List<int> { 4, 5 } }
        };

        // Act & Assert - Test various paths and verify all distances are multiples of 6
        var testCases = new[]
        {
            new { From = 1, To = 2, ExpectedHops = 1 }, // Direct connection
            new { From = 1, To = 4, ExpectedHops = 2 }, // 1 -> 2 -> 4
            new { From = 1, To = 6, ExpectedHops = 3 }, // 1 -> 2 -> 4 -> 6 or 1 -> 3 -> 5 -> 6
            new { From = 2, To = 5, ExpectedHops = 3 }  // 2 -> 1 -> 3 -> 5
        };

        foreach (var testCase in testCases)
        {
            var result = BfsPathFinding.SolvePath(graph, testCase.From, testCase.To);
            var expectedDistance = testCase.ExpectedHops * 6;
            
            Assert.Equal(expectedDistance, result);
            Assert.True(result % 6 == 0, $"Distance {result} from {testCase.From} to {testCase.To} should be a multiple of 6");
        }
    }
}