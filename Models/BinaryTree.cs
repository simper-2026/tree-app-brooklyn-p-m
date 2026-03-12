using System.Dynamic;

namespace Models;

public class BinaryTree
{
    public void Insert(int value)
    {
        
    }
    public string InOrder()
    {
        return "";
    }
    public int Height()
    {
        return 0;
    }
    public string ToMermaid()
    {
        return "";
    }

}

public class Node
{
    public int Value { get; set; }
    public Node? Left;
    public Node? Right;

    public Node(int value, Node right, Node left)
    {
        Value = value;
        Right = right;
        Left = left;
    }
}