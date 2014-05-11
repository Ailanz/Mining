<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ManageServer.aspx.cs" Inherits="Menu_ManageServer" %>

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
        <div class="container"><br />
            <asp:Panel runat="server" ID="panel">
                <asp:GridView runat="server" ID="serverList" class="table table-hover" BorderWidth="0">
                </asp:GridView>
            </asp:Panel>

            <asp:Panel ID="addServerPanel" Style="border-radius: 5px;" Width="400px" runat="server" CssClass="bg-primary container">
                <br />
                <p>
                    <asp:TextBox ID="firstName" class="form-control" runat="server" placeholder="First Name" />
                </p>
                <p>
                    <asp:TextBox ID="lastName" class="form-control" runat="server" placeholder="Last Name" />
                </p>
                <p>
                    <asp:TextBox ID="age"  class="form-control" runat="server" placeholder="Age" Width="60" MaxLength="2" />
                </p>
                <p>
                    <asp:RadioButton ID="male" class="radio" Text="Male:" GroupName="gender" runat="server" />
                </p>
                <p>
                    <asp:RadioButton ID="female" class="radio" Text="Female:" GroupName="gender" runat="server" />
                </p>
                <p>
                    <asp:Button ID="submit" class="form-control" CssClass="btn btn-default" runat="server" Text="Enter Sever" OnClick="submit_Click" />
                </p>

            </asp:Panel>
        </div>
    </form>
</body>
<script src="//ajax.googleapis.com/ajax/libs/jquery/2.1.0/jquery.min.js"></script>
<script type="text/javascript" src="//netdna.bootstrapcdn.com/bootstrap/3.1.1/js/bootstrap.min.js"></script>
</html>
