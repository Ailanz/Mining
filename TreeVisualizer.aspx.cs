using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TreeVisualizer : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DecisionTree root = new DecisionTree("Weather");
        DecisionTree temp = new DecisionTree("Temperature");
        DecisionTree rainTemp = new DecisionTree("RainTemperature");
        DecisionTree cloudTemp = new DecisionTree("CloudTemperature");


        root.AddEdge("Sun", temp);
        root.AddEdge("Rain", rainTemp);
        root.AddEdge("Cloudy", cloudTemp);

        temp.AddEdge("High", rainTemp);
        rainTemp.AddEdge("Rain", cloudTemp);



        List<DecisionTree> trees = root.Traverse(1.2);
        //Response.Write("Value: " + trees.First().node.value);
        //TraverseTree(root);
        GenerateJS(root);
        
    }

    public void GenerateJS(DecisionTree root)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append(@"<script language='javascript'>");
        sb.Append(@"window.onload = function () {");
        sb.Append(@"var g = new Graph();");

        TraverseTree(root, sb, 0);

        sb.Append(@"var layouter = new Graph.Layout.Spring(g);");
        sb.Append(@"layouter.layout();");
        sb.Append(@"var renderer = new Graph.Renderer.Raphael('canvas', g, 1660, 865);");
        sb.Append(@"renderer.draw();");
        sb.Append(@"};");
        sb.Append(@"</script>");

        if (!ClientScript.IsStartupScriptRegistered("JSScript"))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "JSScript", sb.ToString());
        }
    }

    public void TraverseTree(DecisionTree root, StringBuilder sb, int nodeNum)
    {
        int curNum = nodeNum;
        List<TreeEdge> edges = root.GetAllEdges();
        if (edges.Count == 0)
        {
            //Base Case: if no more edges, add node and return
            sb.Append(@"g.addNode("+ nodeNum +", { label : '" + root.node.value + "' });");
            return;
        }
        else
        {
            sb.Append(@"g.addNode(" + nodeNum + ", { label : '" + root.node.value + "' });");
            foreach(TreeEdge edge in edges)
            {
                //Recursive Case: for each edge, recurse on their nodes and add edges to them
                TraverseTree(edge.GetTChildTree(), sb, ++nodeNum);
                sb.Append(@"g.addEdge(" + curNum + ", " + nodeNum + ", { directed : true, label : '" + edge.value.ToString() + "' });");
            }
        }
    }
}