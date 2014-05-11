using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Menu_ManageOrder : System.Web.UI.Page
{
    MenuDbManager db = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Page.ViewStateMode = System.Web.UI.ViewStateMode.Enabled;
        String dbLocation = Server.MapPath(Path.Combine("~/App_Data/", Constants.DB_NAME));
        db = new MenuDbManager(dbLocation);
        db.Connect();
        GetServerList();

        if (ViewState[Constants.Items] == null)
        {
            ViewState[Constants.Items] = new Dictionary<MenuItem, int>();
        }


        if (itemListPanel.Controls.Count == 1)
        {
            DropDownList menuItems = new DropDownList();
            menuItems.CssClass = "form-control";
            menuItems.DataSource = GetMenuItems();
            menuItems.DataBind();
            menuItems.Attributes[Constants.Attributes] = Constants.Attributes;
            itemListPanel.Controls.Add(menuItems);
        }
    }

    protected IEnumerable<String> GetMenuItems()
    {
        MenuItemFactory menuFactory = (MenuItemFactory)Session[Constants.MenuItems];
        var results = from item in menuFactory.itemList
                      select item.itemName;

        return results;
    }

    protected void GetServerList()
    {
        ServerFactory serverFactory = (ServerFactory)Session[Constants.Servers];

        foreach (Server s in serverFactory.serverList)
        {
            ListItem item = new ListItem(s.firstname + " " + s.lastname);
            item.Attributes.Add(Constants.ID, s.id.ToString());
            serverList.Items.Add(item);
        }
    }

    protected void UpdateOrderTable()
    {
        DataTable orderTable = new DataTable();
        OleDbDataReader dataReader = db.GetTable(MenuDbManager.Table.ORDERS);

        orderTable.Load(dataReader);
        orderList.DataSource = orderTable;
        orderList.ShowHeader = true;
        orderList.DataBind();
    }
    protected void submit_Click(object sender, EventArgs e)
    {
        List<int> addedItemsList = (List<int>)ViewState[Constants.Controls];
        var result = from c in this.Controls.OfType<DropDownList>()
                     where c.Attributes[Constants.Attributes].Equals(Constants.Attributes)
                     select c;
        result.First().Enabled = false;
        
    }
    protected void addItem_Click(object sender, EventArgs e)
    {
        //ViewState[Constants.Items];

    }

}