namespace AmazonPreparation.Structures;

public class TrieTree
{
    public TrieTreeNode root;

    public TrieTree(TrieTreeNode root)
    {
        this.root = root;
    }

    public void Insert(string word)
    {
        var currentNode = root;
        
        var normalizedWord = word.ToLowerInvariant();

        foreach (var c in normalizedWord)
        {
            if (!currentNode.Children.ContainsKey(c))
            {
                currentNode.Children[c] = new TrieTreeNode();
            }
            currentNode = currentNode.Children[c];
            currentNode.WordCount++;
        }
        
        currentNode.IsEndOfWord = true;
        currentNode.Word = normalizedWord;
    }

    private TrieTreeNode FindNode(string s)
    {
        var currentNode = root;

        foreach (var c in s)
        {
            if (!currentNode.Children.ContainsKey(c))
            {
                return null; // Node not found
            }
            currentNode = currentNode.Children[c];
        }
        
        return currentNode;
    }

    public int GetWordsWithPrefixCount(string prefix)
    {
        if (string.IsNullOrEmpty(prefix))
        {
            return 0;
        }
        
        var normalizedPrefix = prefix.ToLowerInvariant();
        var node = FindNode(normalizedPrefix);
        
        return node?.WordCount ?? 0;
    }
}

public class TrieTreeNode
{
    public Dictionary<char, TrieTreeNode> Children { get; set; } = new();
    public bool IsEndOfWord { get; set; }
    public string Word { get; set; }
    public int WordCount { get; set; }
}