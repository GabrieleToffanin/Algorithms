namespace AmazonPreparation.Structures;

public class Tree
{
    public TreeNode Root { get; set; }

    public int GetMaxDepth()
    {
        var currentRoot = Root;

        return GetDepthHelper(currentRoot);
    }

    private int GetDepthHelper(TreeNode current)
    {
        if (current is null)
            return -1;
        
        var leftDepth = GetDepthHelper(current.Left);
        var rightDepth = GetDepthHelper(current.Right);
        
        return 1 + Math.Max(leftDepth, rightDepth);
    }

    public List<List<int>> GetLevelOrderTraversal()
    {
        var current = Root;

        return GetLevelOrderTraversalHelper(current);
    }

    private List<List<int>> GetLevelOrderTraversalHelper(TreeNode current)
    {
        var result = new List<List<int>>();
        var queue = new Queue<TreeNode>();
        queue.Enqueue(current);

        while (queue.Count > 0)
        {
            var levelSize = queue.Count;
            var currentLevelList = new List<int>();

            for (int i = 0; i < levelSize; i++)
            {
                var cn = queue.Dequeue();
                currentLevelList.Add(cn.Value);
                if (cn.Left is not null)
                    queue.Enqueue(cn.Left);
                if (cn.Right is not null)
                    queue.Enqueue(cn.Right);
            }
            
            result.Add(currentLevelList);
        }
        
        return result;
    }
}