using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class dbhandler : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            //String dbLocation = Server.MapPath(Path.Combine("~/App_Data/", "testdb.accdb"));
            String dbLocation = Server.MapPath(Path.Combine("~/App_Data/", "testdb.accdb"));
            if(Request.QueryString["file"] != null)
            {
                dbLocation = Server.MapPath(Path.Combine("~/App_Data/", Request.QueryString["file"]));
            }

            DBManager db = new DBManager(dbLocation);
            db.Connect();
            table.DataSource = db.GetTableNames();
            table.DataBind();
            db.Close();
            submitTable_Click(null, null);
        }
    }

    protected void submitTable_Click(object sender, EventArgs e)
    {
        columnPanel.Visible = true;

        String dbLocation = Server.MapPath(Path.Combine("~/App_Data/", Request.QueryString["file"]));
        DBManager db = new DBManager(dbLocation);
        db.Connect();
        columnNames.DataSource = db.GetColumnNames(table.SelectedValue);
        columnNames.DataBind();
        db.Close();
        
    }

    public void dbstuff()
    {
        String dbLocation = Server.MapPath(Path.Combine("~/App_Data/", Request.QueryString["file"]));
        DBManager db = new DBManager(dbLocation);
        db.Connect();
        string query = "select * from lucky";

        OleDbDataReader dr = db.Execute(query);
        DataTable dt = new DataTable();
        dt.Load(dr);
        table.DataSource = dt;
        table.DataBind();

        List<String> tableNames = db.GetTableNames();
        foreach (String s in tableNames)
        {
            Response.Write("Table Name: " + s + "\n\t");
        }

        Response.Write("Entropy: " + DecisionTreeUtil.CalculateEntropy(0.36, 0.24, 0.4) + "<br/>");
        Response.Write("Column Names: " + String.Join(",", db.GetColumnNames("lucky")));

        db.Close();
    }
    protected void submitColumns_Click(object sender, EventArgs e)
    {
        List<ListItem> selected = columnNames.Items.Cast<ListItem>().Where(li => li.Selected).ToList();
        targetValuePanel.Visible = true;
        targetValueBox.DataSource = selected;
        targetValueBox.DataBind();

    }
    protected void submitTargetValue_Click(object sender, EventArgs e)
    {
        String dbLocation = Server.MapPath(Path.Combine("~/App_Data/", Request.QueryString["file"]));
        DBManager db = new DBManager(dbLocation);
        List<ListItem> selected = columnNames.Items.Cast<ListItem>().Where(li => li.Selected).ToList();
        List<String> selectedStrings = new List<String>();
        selected.ForEach(s => selectedStrings.Add(s.Text));

        resultPanel.Visible = true;
        DecisionTreeFactory treeFactory = new DecisionTreeFactory(db, selectedStrings, targetValueBox.SelectedValue.ToString(), table.SelectedValue);
        result.Text="Filtered: " + String.Join(", ", treeFactory.filteredSelectedColumns) + " Target: " + treeFactory.targetColumn + "<br/>";
        DecisionTree tree = treeFactory.CreateTree();
        Operand op = new Operand().Add(Operand.Comparison.EQUALS, "PLAY", "NO").Add(Operand.Comparison.EQUALS, "HUMIDITY", "HIGH");
        foreach(var kv in tree.GetValuesCount(op))
        {
            result.Text += "Diagnostic: " + kv.Key + " : " + kv.Value + " <br/>";
        }
        GenerateJS(tree, op);
    }

    public void GenerateJS(DecisionTree root, Operand op)
    {
        if (!ClientScript.IsStartupScriptRegistered("JSScript"))
        {
            StringBuilder sb = DecisionTreeUtil.GenerateJS(root, op);
            ClientScript.RegisterStartupScript(this.GetType(), "JSScript", sb.ToString());
        }
    }

   

}