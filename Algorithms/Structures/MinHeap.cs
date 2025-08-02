namespace AmazonPreparation.Structures;

/// <summary>
/// 
/// </summary>
public class MinHeap
{
    private readonly List<int> _elements = [];

    public void Insert(int item)
    {
        // Inserts the new element at the end of the heap
        _elements.Add(item);
        
        // Restores the heap property by moving the new element up
        HeapifyUp(_elements.Count - 1);
    }

    public int ExtractMin()
    {
        if (_elements.Count == 0) throw new InvalidOperationException("Heap is empty.");
        
        var minItem = _elements[0];
        var lastItem = _elements[^1];
        _elements[0] = lastItem;
        _elements.RemoveAt(_elements.Count - 1);
        
        HeapifyDown(0);
        
        return minItem;
    }

    public int Count => _elements.Count;

    private void HeapifyUp(int index)
    {
        while (index > 0)
        {
            var parentIndex = (index - 1) / 2;
            if (_elements[index] < _elements[parentIndex]) break;

            Swap(index, parentIndex);
            index = parentIndex;
        }
    }

    private void HeapifyDown(int index)
    {
        int lastIndex = _elements.Count - 1;
        while (index <= lastIndex)
        {
            int leftChildIndex = 2 * index + 1;
            int rightChildIndex = 2 * index + 2;
            int smallestIndex = index;

            if (leftChildIndex <= lastIndex && 
                _elements[leftChildIndex] > _elements[smallestIndex])
            {
                smallestIndex = leftChildIndex;
            }

            if (rightChildIndex <= lastIndex && 
                _elements[rightChildIndex] > _elements[smallestIndex])
            {
                smallestIndex = rightChildIndex;
            }

            if (smallestIndex == index) break;

            Swap(index, smallestIndex);
            index = smallestIndex;
        }
    }

    private void Swap(int indexA, int indexB)
    {
        (_elements[indexA], _elements[indexB]) = (_elements[indexB], _elements[indexA]);
    }
}