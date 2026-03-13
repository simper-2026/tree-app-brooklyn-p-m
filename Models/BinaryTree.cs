using System.Dynamic;
using System.Text;

namespace Models;

public class BinaryTree
{
    public Node? root;
    public void Insert(int value)
    {
        Insert(root, value);
    }
    private void Insert(Node? current, int insert)
    {
        if (current == null)
        {
            root = new Node(insert, null, null);
        }
        else
        {
            if (current.Value == insert)
            {
                return;
            }
            else
            {
                if (current.Value > insert)
                {
                    if (current.Left != null)
                        Insert(current.Left, insert);
                    else
                    {
                        current.Left = new Node(insert, null, null);
                    }
                }
                if (current.Value < insert)
                {
                    if (current.Right != null)
                        Insert(current.Right, insert);
                    else
                    {
                        current.Right = new Node(insert, null, null);
                    }

                }
            }
        }
    }
    public string InOrder()
    {
        return InOrder(root!, new());
    }
    private string InOrder(Node current, StringBuilder s)
    {
        if (current != null)
        {
            if (current.Left != null)
            {
                InOrder(current.Left, s);
                if (current.Right != null)
                {
                    s.Append($"{current.Value}, ");
                    InOrder(current.Right, s);
                }
                else
                {
                    s.Append($"{current.Value}, ");
                }
            }
            else
            {
                s.Append($"{current.Value}, ");
            }
        }
        return s.ToString();
    }
    public int Height()
    {
        int value = 0;
        return Height(root!, value);
    }
    private int Height(Node current, int value)
    {
        if (current != null)
        {
            int left = 0;
            int right = 0;
            if (current.Left != null)
            {
                left = Height(current.Left, left);
            }
            if (current.Right != null)
            {
                right = Height(current.Right, right);
            }
            if (left > right)
            {
                value += right;
            }
            else
            {
                value += left;
            }
        }
        return value;
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