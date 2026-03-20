using System.Dynamic;
using System.IO.Pipelines;
using System.Security.Cryptography.X509Certificates;
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
            root = new Node(insert, null, null, null);
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
                        current.Left = new Node(insert, null, null, current);
                        Height();
                    }
                }
                if (current.Value < insert)
                {
                    if (current.Right != null)
                        Insert(current.Right, insert);
                    else
                    {
                        current.Right = new Node(insert, null, null, current);
                        Height();
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
            int left = 0, right = 0;
            left = current.Left is null ? 0 : Height(current.Left, left);
            right = current.Right is null ? 0 : Height(current.Right, right);
            if (left > right)
            {
                current.Height = left;
                return left + 1;
            } else
            {
                current.Height = right;
                return right + 1;
            }
        }
        return 0;
    }
    public string ToMermaid()
    {
        if (root == null)
        {
            return "graph TD";
        }
        if (root.Left == null && root.Right == null)
        {
            return $"graph TD\n{root.Value}[{root.Value} h:{root.Height}]";
        }

        int links = 0;

        return $"graph TD\n{ToMermaid(root!, ref links)}";
    }
    private string ToMermaid(Node current, ref int links)
    {
        if (current == null)
        {
            return string.Empty;
        }
        string result = string.Empty;
        if (current.Left != null || current.Right != null)
        {
            if (current.Left != null)
            {
                result += $"{current.Value}[{current.Value} h:{current.Height}] --> {current.Left.Value}[{current.Left.Value} h:{current.Left.Height}]\n";
                links++;
                result += ToMermaid(current.Left, ref links);
            }
            else
            {
                result += $"{current.Value} --> _phl{current.Value}[ ]\n";
                result += $"linkStyle {links} stroke:none, stroke-width:0, fill:none\n";
                result += $"style _phl{current.Value} fill:none,stroke:none,color:none\n";
                links++;
            }
            if (current.Right != null)
            {
                result += $"{current.Value} --> {current.Right.Value}[{current.Right.Value} h:{current.Right.Height}]\n";
                links++;
                result += ToMermaid(current.Right, ref links);
            }
            else
            {
                result += $"{current.Value} --> _phr{current.Value}[ ]\n";
                result += $"linkStyle {links} stroke:none, stroke-width:0, fill:none\n";
                result += $"style _phr{current.Value} fill:none,stroke:none,color:none\n";
                links++;
            }

        }


        return result;
    }
    Node RotateRight(Node z)
    {
        Node y = z.Left;
        Node t3 = y.Right;   // T3 moves from y's right to z's left
        y.Right = z;
        z.Left = t3;
        return y;                // y is the new root of this subtree
    }
    Node RotateLeft(Node z)
    {
        Node y = z.Right;
        Node t2 = y.Left;
        y.Left = z;
        z.Right = t2;
        return y;
    }

}

public class Node(int value, Node right, Node left, Node parent, int height = 0)
{
    public int Value { get; set; } = value;
    public Node? Left = left;
    public Node? Right = right;
    public Node? Parent = parent;
    public int Height = height;
}