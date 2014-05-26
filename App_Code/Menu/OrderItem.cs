using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for OrderItem
/// </summary>
public class OrderItem
{
    public int groupId { get; set; }
    public int itemId { get; set; }
    public int amount { get; set; }
	public OrderItem()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public OrderItem(int groupId, int itemId, int amount)
    {
        this.groupId = groupId;
        this.itemId = itemId;
        this.amount = amount;
    }
}