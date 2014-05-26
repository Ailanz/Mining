<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Chart.aspx.cs" Inherits="Menu_Chart" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="//netdna.bootstrapcdn.com/bootstrap/3.1.1/css/bootstrap.min.css" />
    <link rel="stylesheet" href="//netdna.bootstrapcdn.com/bootstrap/3.1.1/css/bootstrap-theme.min.css" />
    <script src="//ajax.googleapis.com/ajax/libs/jquery/2.1.0/jquery.min.js"></script>
    <script type="text/javascript" src="//netdna.bootstrapcdn.com/bootstrap/3.1.1/js/bootstrap.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="container">
                <div class="row">
                    <div class="col-md-5">
                        <asp:GridView ID="serverGrid" runat="server" />
                    </div>
                    <div class="col-md-5">
                        <asp:GridView ID="menuGrid" runat="server" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-5">
                        <asp:GridView ID="orderGrid" runat="server" />
                    </div>
                    <div class="col-md-5">
                        <asp:GridView ID="orderItemGrid" runat="server" />
                    </div>
                </div>
            </div>
            <asp:Chart ID="chart" runat="server">
                <Series>
                    <asp:Series Name="Categories" ChartType="Line" ChartArea="MainChartArea" BorderWidth="5" Color="Red"></asp:Series>
                </Series>
                <ChartAreas>
                    <asp:ChartArea Name="MainChartArea"></asp:ChartArea>
                </ChartAreas>
            </asp:Chart>

            <asp:Chart ID="chart2" runat="server" Width="500" Height="400">
                <Series>
                    <asp:Series Name="Categories" ChartType="Column" ChartArea="MainChartArea" BorderWidth="2"></asp:Series>
                </Series>
                <ChartAreas>
                    <asp:ChartArea Area3DStyle-Enable3D="true"  Name="MainChartArea"></asp:ChartArea>
                </ChartAreas>
            </asp:Chart>
        </div>
    </form>
</body>
</html>
