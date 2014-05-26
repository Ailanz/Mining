using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

/// <summary>
/// Summary description for MenuItemFactory
/// </summary>
public class MenuItemFactory
{
    public List<MenuItem> itemList = new List<MenuItem>();
    private DataTable dataTable = null;

    public MenuItemFactory()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public void Add(MenuItem item)
    {
        this.itemList.Add(item);
    }

    public MenuItem GetMenuItem(int id)
    {
        var item = itemList.First(i => i.id == id);
        return item;
    }

    public MenuItem GetMenuItem(String name)
    {
        var item = itemList.First(i => i.itemName == name);
        return item;
    }

    public void CreateMenuItem(string itemName, string type, double price, double cost)
    {
        MenuDbManager db = new MenuDbManager(DBManager.defaultDbLocation);
        db.Connect();
        db.InsertMenu(itemName, type, price, cost);
        db.Close();
        this.itemList = new List<MenuItem>();
        LoadMenuItems();
    }

    public void CreateMenuItem(MenuItem item)
    {
        CreateMenuItem(item.itemName, item.type, item.price, item.cost);
    }

    public DataTable GetMenuDataTable()
    {
        return this.dataTable;
    }

    public void LoadMenuItems()
    {
        MenuDbManager db = new MenuDbManager(DBManager.defaultDbLocation);
        db.Connect();

        dataTable = db.GetTable(MenuDbManager.Table.MENU);

        foreach (DataRow row in dataTable.Rows)
        {
            MenuItem item = new MenuItem();
            item.id = Convert.ToInt16(row["ID"]);
            item.itemName = row["ITEM_NAME"].ToString();
            item.type = row["TYPE"].ToString();
            item.price = Convert.ToDouble(row["PRICE"]);
            item.cost = Convert.ToDouble(row["COST"]);
            itemList.Add(item);
        }
        db.Close();
    }
}