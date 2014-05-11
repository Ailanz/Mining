using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for MenuItem
/// </summary>
[Serializable]
public class MenuItem
{
    public int id { get; set; }
    public string itemName { get; set; }
    public string type { get; set; }
    public double price { get; set; }
    public double cost { get; set; }
	public MenuItem()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public MenuItem(string itemName, string type, double price, double cost)
    {
        this.itemName = itemName;
        this.type = type;
        this.price = price;
        this.cost = cost;
    }
}