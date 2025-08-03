using AmazonPreparation.Structures;

namespace Algorithms.Tests;

public class FindTreeDepthTests
{
    [Fact]
    public void FindTreeDepth_ReturnsCorrectDepth()
    {
        // Arrange
        var root = CreateTestTree(out var tree);
        tree.Root = root;
        
        // Act
        var depth = tree.GetMaxDepth();
        
        // Assert
        Assert.Equal(3, depth);
    }

    private static TreeNode CreateTestTree(out Tree tree)
    {
        TreeNode root = new TreeNode(1);
        root.Left = new TreeNode(2);
        root.Right = new TreeNode(3);
        root.Left.Left = new TreeNode(4);
        root.Left.Right = new TreeNode(5);
        root.Left.Right.Left = new TreeNode(6);
        
        tree = new Tree();
        return root;
    }
}