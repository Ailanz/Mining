<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ManageOrder.aspx.cs" Inherits="Menu_ManageOrder" %>

<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="ajax" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Manage Servers</title>
    <link rel="stylesheet" href="//netdna.bootstrapcdn.com/bootstrap/3.1.1/css/bootstrap.min.css" />
    <link rel="stylesheet" href="//netdna.bootstrapcdn.com/bootstrap/3.1.1/css/bootstrap-theme.min.css" />
    <script src="//ajax.googleapis.com/ajax/libs/jquery/2.1.0/jquery.min.js"></script>
    <script type="text/javascript" src="//netdna.bootstrapcdn.com/bootstrap/3.1.1/js/bootstrap.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <br />
            <asp:Panel runat="server" ID="panel">
                <asp:GridView runat="server" ID="orderList" class="table table-hover" BorderWidth="0">
                </asp:GridView>
            </asp:Panel>

            <asp:Panel ID="addServerPanel" Style="border-radius: 5px;" Width="350px" runat="server" CssClass="bg-primary container">
                <p>
                    <div class="form-group">
                        <label for="serverList">Server</label>
                        <asp:DropDownList ID="serverList" class="form-control" runat="server" placeholder="Type">
                        </asp:DropDownList>
                    </div>
                </p>
                <p>
                    <asp:Panel ID="itemListPanel" runat="server" Wrap="false">
                        <div class="form-group">
                            <label for="menuItemsList">Item</label>
                            <asp:DropDownList ID="menuItemsList" runat="server" class="form-control" Width="200" />
                        </div>
                        <div class="form-group">
                            <label for="quantity">Quantity</label>
                            <asp:DropDownList ID="quantity" runat="server" CssClass="form-control" Width="75">
                                <asp:ListItem Value="1" Text="1" />
                                <asp:ListItem Value="2" Text="2" />
                                <asp:ListItem Value="3" Text="3" />
                                <asp:ListItem Value="4" Text="4" />
                                <asp:ListItem Value="5" Text="5" />
                                <asp:ListItem Value="6" Text="6" />
                                <asp:ListItem Value="7" Text="7" />
                                <asp:ListItem Value="8" Text="8" />
                                <asp:ListItem Value="9" Text="9" />
                                <asp:ListItem Value="10" Text="10" />
                            </asp:DropDownList>
                        </div>

                    </asp:Panel>
                    <asp:Button ID="addItem" runat="server" CssClass="btn btn-default" Text="Add Item" OnClick="addItem_Click" />
                </p>
                <p>
                    <ajax:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
                    </ajax:ToolkitScriptManager>

                    <asp:TextBox ID="calendar" runat="server" CssClass="form-control" placeholder="Choose a Date"></asp:TextBox>
                    <ajax:CalendarExtender ID="calendarExt" runat="server"
                        TargetControlID="calendar" Format="MM/dd/yyyy">
                    </ajax:CalendarExtender>
                </p>
                <p>
                    <asp:DropDownList ID="mealType" CssClass="form-control" runat="server">
                        <asp:ListItem Value="Breakfast" Text="Breakfast" />
                        <asp:ListItem Value="Lunch" Text="Lunch" />
                        <asp:ListItem Value="Dinner" Text="Dinner" />
                    </asp:DropDownList>
                </p>
                <p>
                    <asp:Button ID="submit" class="form-control" CssClass="btn btn-default" runat="server" Text="Enter Item" OnClick="submit_Click" />
                </p>

            </asp:Panel>
        </div>
    </form>
</body>
</html>
