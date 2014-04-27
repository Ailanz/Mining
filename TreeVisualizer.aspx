<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TreeVisualizer.aspx.cs" Inherits="TreeVisualizer" %>

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
    <div id="canvas"></div>
<button id="redraw" onclick="redraw();">redraw</button>
<button id="hide_penguin" onclick="hide('penguin');">hide penguin (beta)</button>
<button id="hide_penguin" onclick="show('penguin');">show penguin (beta)</button>
    </div>
    </form>
</body>
</html>
