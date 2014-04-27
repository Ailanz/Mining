<%@ Page Language="C#" AutoEventWireup="true" CodeFile="dbhandler.aspx.cs" Inherits="dbhandler" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="/js/DraculaGraph/raphael-min.js"></script>
    <script type="text/javascript" src="/js/DraculaGraph/dracula_graffle.js"></script>
    <script type="text/javascript" src="/js/DraculaGraph/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="/js/DraculaGraph/dracula_graph.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:DataGrid runat="server" ID="table2" />

        <div>
            Tables:
            <asp:DropDownList ID="table" runat="server" AutoPostBack="true" OnTextChanged="submitTable_Click" />
        </div>
        <asp:Panel runat="server" ID="columnPanel" Visible="false">
            Columns:
            <asp:CheckBoxList runat="server" ID="columnNames" />
            <asp:Button ID="submitColumns" runat="server" OnClick="submitColumns_Click" Text="Choose Columns" />
        </asp:Panel>

        <asp:Panel runat="server" ID="targetValuePanel" Visible="false">
            Target Value:
            <asp:DropDownList runat="server" ID="targetValueBox" />
            <asp:Button ID="submitTargetValue" Text="Run Tree" OnClick="submitTargetValue_Click" runat="server" />
        </asp:Panel>

        <asp:Panel runat="server" ID="resultPanel" Visible="false">
            <hr>
            <asp:Label runat="server" ID="result" />
            <div id="canvas"></div>
            <button id="redraw" onclick="redraw();">redraw</button>
            <button id="hide_penguin" onclick="hide('penguin');">hide penguin (beta)</button>
            <button id="hide_penguin" onclick="show('penguin');">show penguin (beta)</button>
        </asp:Panel>
    </div>
    </form>
</body>
</html>
