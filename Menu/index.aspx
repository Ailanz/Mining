<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="Menu_index" %>

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
        <br />
        <div class="well" style="max-width: 400px; margin: 50px auto 10px;">
            <asp:Button ID="chart" type="button" class="form-control" CssClass="btn btn-primary btn-lg btn-block" runat="server" Text="Chart" OnClick="chart_Click" />
            <asp:Button ID="manageServer" class="form-control" CssClass="btn btn-lg btn-block" runat="server" Text="Manage Servers" OnClick="manageServer_Click" />
            <asp:Button ID="manageMenu" class="form-control" CssClass="btn btn-default btn-lg btn-block" runat="server" Text="Manage Menu" OnClick="manageMenu_Click" />
            <asp:Button ID="manageOrder" class="form-control" CssClass="btn btn-default btn-lg btn-block" runat="server" Text="Manage Orders" OnClick="manageOrder_Click" />
        </div>
    </form>
</body>
</html>
