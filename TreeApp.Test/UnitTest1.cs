namespace TreeApp.Test;

using Models;

public class UnitTest1
{
    [Fact]
    public void Insert()
    {
        BinaryTree tree = new BinaryTree();
        tree.Insert(10);
        Assert.NotNull(tree);
        Assert.Equal(tree.root.Value, 10);
    }
    [Fact]
    public void InsertLeftValue()
    {
        BinaryTree tree = new BinaryTree();
        tree.Insert(10);
        tree.Insert(5);
        Assert.Equal(tree.root.Left.Value, 5);
    }
    [Fact]
    public void InsertRightValue()
    {
        BinaryTree tree = new();
        tree.Insert(10);
        tree.Insert(5);
        tree.Insert(50);
        tree.Insert(46);
        Assert.Equal(50, tree.root!.Right!.Value);
        Assert.Equal(46, tree.root.Right.Left!.Value);
    }
    [Fact]
    public void InOrder()
    {
        // Given
        BinaryTree tree = new();
        tree.Insert(10);
        //tree.Insert(5);
        tree.Insert(50);
        //tree.Insert(46);
        // When
        string expected = "10, 50, ";

        // Then
        Assert.Equal(expected, tree.InOrder());
    }
    [Fact]
    public void Height()
    {
        // Given
        BinaryTree tree = new();
        tree.Insert(10);
        tree.Insert(5);
        tree.Insert(50);
        tree.Insert(46);
        // When
        int i = tree.Height();
        // Then
        Assert.Equal(3, i);
    }
    [Fact]
    public void ToMermaid()
    {
        // Given
        BinaryTree tree = new();
        tree.Insert(10);
        // When
        string s = "graph TD\n10";
        // Then
        Assert.Equal(s, tree.ToMermaid());
    }
    [Fact]
    public void ToMermaidMultipleValues()
    {
        // Given
        BinaryTree tree = new();
        tree.Insert(10);
        tree.Insert(5);
        //tree.Insert(12);
        // When
        string s = "graph TD\n10 --> 5\n5 --> FIX\n5 --> FIX\n10 --> FIX\n";
        // Then
        Assert.Equal(s, tree.ToMermaid());
    }
}
