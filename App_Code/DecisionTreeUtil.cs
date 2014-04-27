using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

/// <summary>
/// Summary description for Calc
/// </summary>
public class DecisionTreeUtil
{
    static Random rnd = new Random();

	private DecisionTreeUtil()
	{
        //Dont instantiate this class
	}

    public static double CalculateEntropy(params double[] num)
    {
        double entropy = 0;
        double sum = 0;

        for(int i=0; i < num.Length; i++)
        {
            sum += num[i];
        }

        for (int j = 0; j < num.Length; j++)
        {
            double percent = num[j] / sum;
            entropy += (-1) * num[j] * Math.Log(num[j], 2);
        }

        return entropy;
    }
    public static StringBuilder GenerateJS(DecisionTree root, Operand op)
    {
        StringBuilder sb = new System.Text.StringBuilder();
        sb.Append(@"<script language='javascript'>");
        sb.Append(@"window.onload = function () {");
        sb.Append(@"var g = new Graph();");

        TraverseTree(root, sb, 0, op);

        sb.Append(@"var layouter = new Graph.Layout.Spring(g);");
        sb.Append(@"layouter.layout();");
        sb.Append(@"var renderer = new Graph.Renderer.Raphael('canvas', g, 1600, 815);");
        sb.Append(@"renderer.draw();");
        sb.Append(@"};");
        sb.Append(@"</script>");
        return sb;
    }

    public static StringBuilder GenerateJS(DecisionTree root)
    {
        return DecisionTreeUtil.GenerateJS(root, new Operand());
    }

    public static void TraverseTree(DecisionTree root, StringBuilder sb, int nodeNum, Operand op)
    {
        if (root == null)
        {
            return;
        }
        List<TreeEdge> edges = root.GetAllEdges();
        sb.Append(@"g.addNode(" + nodeNum + ", { label : '" + root.node.value + "' });");

        if (edges.Count == 0)
        {
            //Base Case: if no more edges, return
            return;
        }
        else
        {
            foreach (TreeEdge edge in edges)
            {
                //Recursive Case: for each edge, recurse on their nodes and add edges to them
                int nextNum = rnd.Next(0, int.MaxValue);
                //Color the edges
                if (op.Evaluate(root.node.value, edge.value))
                {
                    TraverseTree(edge.GetTChildTree(), sb, nextNum, op);
                    sb.Append(@"g.addEdge(" + nodeNum + ", " + nextNum + ", { directed : true,  stroke : '#bfa' , fill : '#56f', label : '" + edge.value.ToString() + "' });");
                }
                else
                {
                    TraverseTree(edge.GetTChildTree(), sb, nextNum, Operand.Clone(op).Fail());
                    sb.Append(@"g.addEdge(" + nodeNum + ", " + nextNum + ", { directed : true, label : '" + edge.value.ToString() + "' });");
                }
            }
        }
    }
}