using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Menu_index : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    protected void manageServer_Click(object sender, EventArgs e)
    {
        Response.Redirect("ManageServer.aspx");
    }
    protected void manageMenu_Click(object sender, EventArgs e)
    {
        Response.Redirect("ManageMenu.aspx");

    }
    protected void manageOrder_Click(object sender, EventArgs e)
    {
        Response.Redirect("ManageOrder.aspx");

    }
    protected void chart_Click(object sender, EventArgs e)
    {
        Response.Redirect("Chart.aspx");
    }
}