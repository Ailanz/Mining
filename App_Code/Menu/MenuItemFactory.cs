using System;
using System.Collections.Generic;
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
        String dbLocation = HostingEnvironment.MapPath(Path.Combine("~/App_Data/", Constants.DB_NAME));
        MenuDbManager db = new MenuDbManager(dbLocation);
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

    public void LoadMenuItems()
    {
        String dbLocation = HostingEnvironment.MapPath(System.IO.Path.Combine("~/App_Data/", Constants.DB_NAME));
        MenuDbManager db = new MenuDbManager(dbLocation);
        db.Connect();

        System.Data.OleDb.OleDbDataReader dataReader = db.GetTable(MenuDbManager.Table.MENU);
        // Load all MenuItems into Session Variable
        while (dataReader.Read())
        {
            MenuItem item = new MenuItem();
            item.id = Convert.ToInt16(dataReader["ID"]);
            item.itemName = dataReader["ITEM_NAME"].ToString();
            item.type = dataReader["TYPE"].ToString();
            item.price = Convert.ToDouble(dataReader["PRICE"]);
            item.cost = Convert.ToDouble(dataReader["COST"]);
            itemList.Add(item);
        }

        db.Close();
    }
}