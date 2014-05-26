using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ManageMenu : System.Web.UI.Page
{
    MenuDbManager db = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        String dbLocation = Server.MapPath(Path.Combine("~/App_Data/", Constants.DB_NAME));
        db = new MenuDbManager(dbLocation);
        db.Connect();
        updateMenuItems();
    }

    protected void submit_Click(object sender, EventArgs e)
    {
        MenuItem item = new MenuItem(itemName.Text, itemType.Text, Convert.ToDouble(price.Text), Convert.ToDouble(cost.Text));
        MenuItemFactory factory = (MenuItemFactory)Session[Constants.MenuItemsFactory];
        factory.CreateMenuItem(item);
        itemName.Text = "";
        itemType.SelectedIndex = 0;
        price.Text = "";
        cost.Text = "";
        updateMenuItems();
    }

    protected void updateMenuItems()
    {
        DataTable menuTable = new DataTable();
        menuTable = db.GetTable(MenuDbManager.Table.MENU);
        menuItems.DataSource = menuTable;
        menuItems.ShowHeader = true;
        menuItems.DataBind();
    }
}