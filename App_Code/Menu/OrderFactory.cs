using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

/// <summary>
/// Summary description for OrderFactory
/// </summary>
public class OrderFactory
{

    public List<Order> orderList = new List<Order>();
    private DataTable orderDataTable = null;
    private DataTable orderItemDataTable = null;

	public OrderFactory()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public void Add(Order order)
    {
        this.orderList.Add(order);
    }

    public Order GetOrder(int groupId)
    {
        var item = orderList.First(i => i.groupId == groupId);
        return item;
    }

    public void CreateOrder(int serverId, DateTime? saleDate, string mealType, List<KeyValuePair<int,int>> itemId_Amount )
    {
        MenuDbManager db = new MenuDbManager(DBManager.defaultDbLocation);
        db.Connect();

        OleDbDataReader reader = db.Execute("select max(GROUP_ID) from [Order]");
        bool hasValues = reader.Read() && !reader.IsDBNull(0);
        //if Order table is empty, id = 0, else = max + 1
        int groupId = hasValues ? Convert.ToInt16(reader[0]) + 1 : 0;
        
        db.InsertOrder(groupId, serverId, saleDate.ToString(), mealType);
        foreach(var k in itemId_Amount)
        {
            db.InsertOrderItem(groupId, k.Key, k.Value);
        }
        db.Close();
        this.orderList = new List<Order>();
        LoadOrders();
    }
    public DataTable GetOrderDataTable()
    {
        if (orderDataTable == null)
        {
            throw new Exception("Order DataTable not initialized");
        }
        return orderDataTable;
    }

    public DataTable GetOrderItemDataTable()
    {
        if (orderItemDataTable == null)
        {
            throw new Exception("order Item DataTable not initialized");
        }
        return orderItemDataTable;
    }

    public void LoadOrders()
    {
        String dbLocation = HostingEnvironment.MapPath(System.IO.Path.Combine("~/App_Data/", Constants.DB_NAME));
        MenuDbManager db = new MenuDbManager(DBManager.defaultDbLocation);
        db.Connect();

        orderDataTable = db.GetTable(MenuDbManager.Table.ORDERS);
        // Load all MenuItems into Session Variable
        foreach (DataRow row in orderDataTable.Rows)
        {
            Order order = new Order();
            order.groupId = Convert.ToInt16(row["GROUP_ID"]);
            order.serverId = Convert.ToInt16(row["SERVER_ID"]);
            order.mealType = row["MEAL_TYPE"].ToString();
            orderList.Add(order);
        }

        List<OrderItem> orderItems = new List<OrderItem>();
        orderItemDataTable = db.GetTable(MenuDbManager.Table.ORDER_ITEM);
        foreach (DataRow row in orderItemDataTable.Rows)
        {
            OrderItem item = new OrderItem();
            item.groupId = Convert.ToInt16(row["GROUP_ID"]);
            item.itemId = Convert.ToInt16(row["ITEM_ID"]);
            item.amount = Convert.ToInt16(row["AMOUNT"]);
            orderItems.Add(item);
        }

        foreach(Order o in this.orderList)
        {
            var results = orderItems.FindAll(i => i.groupId == o.groupId);
            o.orderItems = results;
        }
        db.Close();
    }


}