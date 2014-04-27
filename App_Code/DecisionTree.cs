using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


//T = nodeType
public class DecisionTree
{
    public TreeNode node { set; get; }
    List<TreeEdge> edges = new List<TreeEdge>();

	public DecisionTree()
	{
        node = new TreeNode();
	}

    public DecisionTree(String value)
    {
        this.node = new TreeNode(value);
    }

    public void AddEdge(Object value, DecisionTree childNode)
    {
        TreeEdge edge = new TreeEdge(value, childNode);
        edges.Add(edge);
    }

    public List<DecisionTree> Traverse(Object value)
    {
        List<DecisionTree> results = new List<DecisionTree>();
        foreach(TreeEdge edge in edges)
        {
            if(edge.Evaluate(value))
            {
                results.Add(edge.GetTChildTree());
            }
        }
        return results;
    }

    public List<TreeEdge> GetAllEdges()
    {
        return this.edges;
    }

    public Dictionary<String, int> GetValuesCount()
    {
        return this.GetValuesCount(new Operand());
    }

    public virtual Dictionary<String, int> GetValuesCount(Operand op)
    {
        var resultMap = new Dictionary<string, int>();
        //Recurse all subtrees and add their entries
        foreach (TreeEdge edge in this.GetAllEdges())
        {
            if (op.Evaluate(this.node.value, edge.value))
            {
                var result = edge.GetTChildTree().GetValuesCount(op);
                foreach (var kv in result)
                {
                    if (resultMap.ContainsKey(kv.Key))
                    {
                        resultMap[kv.Key] = resultMap[kv.Key] + kv.Value;
                    }
                    else
                    {
                        resultMap[kv.Key] = kv.Value;
                    }
                }
            }
        }
        return resultMap;
    }
}