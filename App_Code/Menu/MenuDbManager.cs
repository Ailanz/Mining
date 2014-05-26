using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for MenuDbManager
/// </summary>
public class MenuDbManager : DBManager
{
    public enum Table
    {
        SERVER, MENU, ORDERS, ORDER_ITEM
    }
    public MenuDbManager(string dbLocation)
        : base(dbLocation)
    {

    }

    public OleDbDataReader GetServerTable()
    {
        return this.Execute("Select * from Server");
    }


    public DataTable GetTable(Table table)
    {
        string tableName = "";
        switch (table)
        {
            case Table.SERVER:
                tableName = "Server";
                break;
            case Table.MENU:
                tableName = "Menu";
                break;
            case Table.ORDERS:
                tableName = "Order";
                break;
            case Table.ORDER_ITEM:
                tableName = "OrderItem";
                break;
            default:
                tableName = "";
                break;
        }
        OleDbDataReader dataReader = this.Execute("Select * from [" + tableName + "]");

        DataTable dataTable = new DataTable();
        dataTable.Load(dataReader);
        return dataTable;

    }

    public int InsertServer(String firstName, String lastName, int age, char sex)
    {
        return this.Insert("Insert into [SERVER] (FIRST_NAME, LAST_NAME, AGE, SEX) VALUES('" + firstName + "','" + lastName + "'," + age + ",'" + sex + "')");
    }

    public int InsertMenu(String name, String type, double price, double cost)
    {
        return this.Insert("Insert into [Menu] (ITEM_NAME, TYPE, PRICE, COST) VALUES('" + name + "','" + type + "'," + price + ",'" + cost + "')");
    }

    public int InsertOrder(int groupId, int serverId, string saleDate, string mealType)
    {
        return this.Insert("Insert into [Order] (GROUP_ID, SERVER_ID, SALE_DATE, MEAL_TYPE) VALUES(" + groupId + "," + serverId + ",'" + saleDate + "','" + mealType + "')");
    }

    public int InsertOrderItem(int groupId, int itemId, int amount)
    {
        return this.Insert("Insert into OrderItem (GROUP_ID, ITEM_ID, AMOUNT) VALUES(" + groupId + "," + itemId + "," + amount + ")");
    }
}