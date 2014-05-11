using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Menu_ManageServer : System.Web.UI.Page
{
    MenuDbManager db = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        String dbLocation = Server.MapPath(Path.Combine("~/App_Data/", Constants.DB_NAME));
        db = new MenuDbManager(dbLocation);
        db.Connect();
        updateServerTable();
    }
    protected void submit_Click(object sender, EventArgs e)
    {
        char gender = (male.Checked ? 'M' : 'F');
        Server server = new Server(firstName.Text, lastName.Text, Convert.ToInt16(age.Text), gender);
        ServerFactory serverFactory = (ServerFactory)Session["Servers"];
        serverFactory.CreateServer(server);

        updateServerTable();
        firstName.Text = "";
        lastName.Text = "";
        age.Text = "";
        male.Checked = false;
        female.Checked = false;
    }

    protected void updateServerTable()
    {        
        DataTable serverTable = new DataTable();
        OleDbDataReader dataReader = db.GetTable(MenuDbManager.Table.SERVER);
        serverTable.Load(dataReader);
        serverList.DataSource = serverTable;
        serverList.ShowHeader = true;
        serverList.DataBind();
    }
}