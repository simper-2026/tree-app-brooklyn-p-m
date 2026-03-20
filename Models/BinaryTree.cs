using System.Text;

namespace Models;

public class BinaryTree
{
    public Node? root;

    public void Insert(int value)
    {
        root = Insert(root, value, null);
    }

    private Node Insert(Node? current, int value, Node? parent)
    {

        if (current == null)
        {
            return new Node(value, parent);
        }

        if (value < current.Value)
        {
            current.Left = Insert(current.Left, value, current);
        }
        else if (value > current.Value)
        {
            current.Right = Insert(current.Right, value, current);
        }
        else
        {
            return current; 
        }

        UpdateHeight(current);

        return Rebalance(current);
    }

    private Node Rebalance(Node current)
    {
        int balance = GetBalance(current);

        // Left
        if (balance > 1)
        {
            // Left-Right Case
            if (GetBalance(current.Left) < 0)
            {
                current.Left = RotateLeft(current.Left!);
            }
            return RotateRight(current);
        }

        // Right 
        if (balance < -1)
        {
            // Right-Left Case
            if (GetBalance(current.Right) > 0)
            {
                current.Right = RotateRight(current.Right!);
            }
            return RotateLeft(current);
        }

        return current;
    }

    private Node RotateRight(Node y)
    {
        Node x = y.Left!;
        Node T2 = x.Right;

        // Perform rotation
        x.Right = y;
        y.Left = T2;

        // Update Parents
        x.Parent = y.Parent;
        y.Parent = x;
        if (T2 != null) T2.Parent = y;

        // Update heights
        UpdateHeight(y);
        UpdateHeight(x);

        return x;
    }

    private Node RotateLeft(Node x)
    {
        Node y = x.Right!;
        Node T2 = y.Left;

        // Perform rotation
        y.Left = x;
        x.Right = T2;

        // Update Parents
        y.Parent = x.Parent;
        x.Parent = y;
        if (T2 != null) T2.Parent = x;

        // Update heights
        UpdateHeight(x);
        UpdateHeight(y);

        return y;
    }

    private void UpdateHeight(Node n)
    {
        n.Height = 1 + Math.Max(GetHeight(n.Left), GetHeight(n.Right));
    }

    private int GetHeight(Node? n) => n?.Height ?? -1;

    private int GetBalance(Node? n) => n == null ? 0 : GetHeight(n.Left) - GetHeight(n.Right);

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
}

public class Node
{
    public int Value { get; set; }
    public Node? Left { get; set; }
    public Node? Right { get; set; }
    public Node? Parent { get; set; }
    public int Height { get; set; }

    public Node(int value, Node? parent)
    {
        Value = value;
        Parent = parent;
        Height = 0;
    }
}