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

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Page.ViewStateMode = System.Web.UI.ViewStateMode.Enabled;
        db = new MenuDbManager(DBManager.defaultDbLocation);
        db.Connect();

        if (!IsPostBack)
        {
            GetServerList();
            menuItemsList.DataSource = GetMenuItems();
            menuItemsList.DataBind();
        }

        if (ViewState[Constants.Items] == null)
        {
            ViewState[Constants.Items] = new Dictionary<MenuItem, int>();
        }
    }

    protected DataTable ConvertToDataTable(Dictionary<MenuItem, int> dic)
    {
        DataTable tb = new DataTable();
        tb.Columns.Add("Quantity");
        tb.Columns.Add("Item");
        foreach(var r in dic)
        {
            var row = tb.NewRow();
            row["Quantity"] = r.Value;
            row["Item"] = r.Key.itemName;
            tb.Rows.Add(row);
        }
        return tb;
    }

    protected IEnumerable<String> GetMenuItems()
    {
        MenuItemFactory menuFactory = (MenuItemFactory)Session[Constants.MenuItemsFactory];
        var results = from item in menuFactory.itemList
                      select item.itemName;

        return results;
    }

    protected void GetServerList()
    {
        ServerFactory serverFactory = (ServerFactory)Session[Constants.ServersFactory];

        foreach (Server s in serverFactory.serverList)
        {
            ListItem item = new ListItem(s.firstname + " " + s.lastname);
            item.Attributes.Add(Constants.ID, s.id.ToString());
            item.Value = s.id.ToString();
            serverList.Items.Add(item);
        }
    }

    protected void UpdateOrderTable()
    {
        DataTable orderTable = db.GetTable(MenuDbManager.Table.ORDERS);
        orderList.DataSource = orderTable;
        orderList.ShowHeader = true;
        orderList.DataBind();
    }
    protected void submit_Click(object sender, EventArgs e)
    {
        OrderFactory orderFactory = (OrderFactory)Session[Constants.OrderFactory];
        ServerFactory serverFactory = (ServerFactory)Session[Constants.ServersFactory];
        
        List<KeyValuePair<int, int>> itemId_AmountList = new List<KeyValuePair<int, int>>();
        foreach (var kv in (Dictionary<MenuItem, int>)ViewState[Constants.Items])
        {
            itemId_AmountList.Add(new KeyValuePair<int, int>(kv.Key.id, kv.Value));
        }
        orderFactory.CreateOrder(Convert.ToInt16(serverList.SelectedValue), DateTime.Parse(calendar.Text), mealType.SelectedItem.Text, itemId_AmountList);
        calendar.Text = "";
        mealType.SelectedIndex = 0;
        serverList.SelectedIndex = 0;
        menuItemsList.SelectedIndex = 0;
        ViewState[Constants.Items] = new Dictionary<MenuItem, int>();
    }
    protected void addItem_Click(object sender, EventArgs e)
    {
        MenuItemFactory factory = (MenuItemFactory)Session[Constants.MenuItemsFactory];
        MenuItem item = factory.GetMenuItem(menuItemsList.SelectedItem.Text);
        ((Dictionary<MenuItem, int>)ViewState[Constants.Items]).Add(item, Convert.ToInt16(quantity.SelectedValue));
        orderList.DataSource = ConvertToDataTable(((Dictionary<MenuItem, int>)ViewState[Constants.Items]));
        orderList.DataBind();
        quantity.SelectedIndex = 0;
        menuItemsList.SelectedIndex = 0;
    }

}