using AmazonPreparation.Structures;

namespace Algorithms.Tests;

public class LevelOrderTraversalTests
{
    [Fact]
    public void LevelOrderTraversal_ReturnsArrayOfArraysForEachLevel()
    {
        // Arrange
        var tree = CreateTestTree2();
        
        // Act
        var result = tree.GetLevelOrderTraversal();
        
        // Assert
        var resultingCollection = new List<List<int>>
        {
            new(){1},
            new(){2, 3},
            new(){4, 5},
            new(){6}
        };
        
        Assert.Equal(resultingCollection, result, new ListOfListsComparer());
    }
    
    private static Tree CreateTestTree()
    {
        //         1
        //        /\
        //       2  3
        //      /\ /\
        //     4 5 null, null
        //       \
        //        6
        TreeNode root = new TreeNode(1);
        root.Left = new TreeNode(2);
        root.Right = new TreeNode(3);
        root.Left.Left = new TreeNode(4);
        root.Left.Right = new TreeNode(5);
        root.Left.Right.Left = new TreeNode(6);
        
        var tree = new Tree();
        tree.Root = root;
        
        return tree;
    }
    
    private static Tree CreateTestTree2()
    {
        //         1
        //        /\
        //       2  3
        //      /\ /\
        //     4 5 null, null
        //       \
        //        6
        TreeNode root = new TreeNode(1);
        root.Right = new TreeNode(2);
        root.Right.Right = new TreeNode(5);
        root.Right.Right.Left = new TreeNode(3);
        root.Right.Right.Left.Right = new TreeNode(4);
        root.Right.Right.Right = new TreeNode(6);
        
        var tree = new Tree();
        tree.Root = root;
        
        return tree;
    }
}

internal class ListOfListsComparer : IEqualityComparer<IEnumerable<IEnumerable<int>>>
{
    public bool Equals(IEnumerable<IEnumerable<int>> x, IEnumerable<IEnumerable<int>> y)
    {
        return x.SequenceEqual(y, new ListComparer());
    }

    public int GetHashCode(IEnumerable<IEnumerable<int>> obj) => obj.GetHashCode();
}

internal class ListComparer : IEqualityComparer<IEnumerable<int>>
{
    public bool Equals(IEnumerable<int> x, IEnumerable<int> y) => x.SequenceEqual(y);
    public int GetHashCode(IEnumerable<int> obj) => obj.GetHashCode();
}