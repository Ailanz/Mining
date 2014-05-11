using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for MenuDbManager
/// </summary>
public class MenuDbManager : DBManager
{

    public enum Table{
        SERVER, MENU, ORDERS
    }
	public MenuDbManager(string dbLocation) : base(dbLocation)
	{

	}

    public OleDbDataReader GetServerTable()
    {
        return this.Execute("Select * from Server");   
    }

    public OleDbDataReader GetTable(Table table)
    {
        string tableName = "";
        switch(table)
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
            default:
                tableName = "";
                break;
        }
        OleDbDataReader dataReader = this.Execute("Select * from " + tableName);
        return dataReader;
        
    }

    public int InsertServer(String firstName, String lastName, int age, char sex)
    {
        return this.Insert("Insert into SERVER (FIRST_NAME, LAST_NAME, AGE, SEX) VALUES('" + firstName + "','" + lastName + "'," + age + ",'" + sex + "')");
    }

    public int InsertMenu(String name, String type, double price, double cost)
    {
        return this.Insert("Insert into Menu (ITEM_NAME, TYPE, PRICE, COST) VALUES('" + name + "','" + type + "'," + price + ",'" + cost + "')");
    }
}