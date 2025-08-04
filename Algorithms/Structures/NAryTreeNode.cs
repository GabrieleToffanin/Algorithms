namespace AmazonPreparation.Structures;

public class NAryTreeNode
{
    public int Value { get; set; }
    public List<NAryTreeNode> Children { get; set; } = [];

    public NAryTreeNode(int value)
    {
        this.Value = value;
    }
}

public class NAryTree
{
    public NAryTreeNode Root { get; set; }
    public int MaxChildrenCount { get; set; } = 3;
    
    public NAryTree(NAryTreeNode root)
    {
        Root = root;
    }
}