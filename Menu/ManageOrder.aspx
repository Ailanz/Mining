<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ManageOrder.aspx.cs" Inherits="Menu_ManageOrder" %>

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
                    <asp:Panel ID="itemListPanel" runat="server">
                        <asp:DropDownList ID="menuItemsList" runat="server" class="form-control" />
                        <asp:DropDownList ID="quantity" runat="server" class="form-control">
                            <asp:ListItem Value="1" Text="1" />
                        </asp:DropDownList>
                    </asp:Panel>
                     <br />
                    <asp:Button ID="addItem" runat="server" CssClass="btn btn-default" Text="Add Another Item" OnClick="addItem_Click" />

                </p>
                <p>
                    <asp:TextBox ID="price" class="form-control" runat="server" placeholder="Price (eg. 5.99)" Width="120" MaxLength="8" />
                </p>
                <p>
                    <asp:TextBox ID="cost" class="form-control" runat="server" placeholder="Cost (eg. 5.99)" Width="120" MaxLength="8" />
                </p>
                <p>
                    <asp:Button ID="submit" class="form-control" CssClass="btn btn-default" runat="server" Text="Enter Item" OnClick="submit_Click" />
                </p>

            </asp:Panel>
        </div>
    </form>
</body>
</html>
