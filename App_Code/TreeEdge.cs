using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for TreeEdge
/// </summary>
public class TreeEdge
{
    public Object value { get; set; }
    private DecisionTree childTree { get; set; }

	public TreeEdge()
	{
        value = null;
	}

    public TreeEdge(Object value, DecisionTree childTree)
    {
        this.value = value;
        this.childTree = childTree;
    }

    public DecisionTree GetTChildTree()
    {
        return childTree;
    }

    public void SetChildTree(DecisionTree tree)
    {
        this.childTree = tree;
    }

    public bool Evaluate(Object testValue)
    {
        return value.Equals(testValue) ? true : false;
    }
}