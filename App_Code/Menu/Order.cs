using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Order
/// </summary>
public class Order
{
    public int groupId { get; set; }
    public int serverId { get; set; }
    public DateTime saleDate { get; set; }
    public string mealType { get; set; }

    public List<OrderItem> orderItems = new List<OrderItem>();

	public Order()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public Order(int groupId, int serverId, DateTime saleDate, string mealType, List<OrderItem> orderItems)
    {
        this.groupId = groupId;
        this.serverId = serverId;
        this.saleDate = saleDate;
        this.mealType = mealType;
        this.orderItems = orderItems;
    }
}