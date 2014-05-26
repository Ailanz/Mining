using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Menu_Chart : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        var r = 1000;
        var known = new Dictionary<double, double>
                { 
                    { 0.0, 0.0 },
                    { 0.1, 0.1 },
                    { 20.0, 0.25 * r },
                    { 120.0, 0.75 * r }
                };

        foreach (var pair in known)
        {
            //Response.Write(String.Format("{0:0.000}\t{1:0.000}", pair.Key, pair.Value) + "<br/>");
        }

        var scaler = new SplineInterpolator(known);
        var start = known.First().Key;
        var end = known.Last().Key;
        var step = (end - start) / 100;

        var points = new Dictionary<double, double>();

        //chart.Series[0].

        for (var x = start; x <= end; x += step)
        {
            var y = scaler.GetValue(x);
            points.Add(x, y);

            if (x > Double.MinValue && y >= Double.MinValue && x < Double.MaxValue && y < Double.MaxValue)
            {
                //Response.Write(String.Format("\t\t{0:0.000}\t{1:0.000}", x, y) + "<br/>");
                chart.Series[0].Points.AddXY(x, y);
            }
        }

        DisplayDataTables();
        FillChart2();
    }

    public void DisplayDataTables()
    {
        ServerFactory serverFactory = (ServerFactory)Session[Constants.ServersFactory];
        serverGrid.DataSource = serverFactory.GetServerDataTable();
        serverGrid.DataBind();

        MenuItemFactory menuFactory = (MenuItemFactory)Session[Constants.MenuItemsFactory];
        menuGrid.DataSource = menuFactory.GetMenuDataTable();
        menuGrid.DataBind();

        OrderFactory orderFactory = (OrderFactory)Session[Constants.OrderFactory];
        orderGrid.DataSource = orderFactory.GetOrderDataTable();
        orderGrid.DataBind();

        orderItemGrid.DataSource = orderFactory.GetOrderItemDataTable();
        orderItemGrid.DataBind();
    }

    public void FillChart2()
    {
        MenuDbManager db = new MenuDbManager(DBManager.defaultDbLocation);
        db.Connect();
        DataTable table = new DataTable();
        table.Load(db.Execute("select ITEM_NAME, (PRICE-COST) as PROFIT from Menu"));

        var tableFilter = from t in table.AsEnumerable()
                          where Convert.ToInt16(t["PROFIT"]) > 2
                          select t;

        DataTable table2 = tableFilter.CopyToDataTable();

        chart2.Series[0].XValueMember = "ITEM_NAME";
        chart2.Series[0].YValueMembers = "PROFIT";
        chart2.DataSource = table2;
        chart2.DataBind();
    }
}