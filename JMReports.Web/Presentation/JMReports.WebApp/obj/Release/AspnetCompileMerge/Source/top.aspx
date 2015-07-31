<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="top.aspx.cs" Inherits="JMReports.WebApp.top" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link href="Styles/table.css" rel="stylesheet" />
    <script type="text/javascript" src="Scripts/jquery-1.10.2.js" ></script>
    <script type="text/javascript" src="Scripts/common.js"></script>


</head>
<body>
    <form id="form1" runat="server">
        <div class="back">
                <asp:Label ID="lblUser" runat="server" Text=""></asp:Label>&nbsp;<ul>|</ul>
             <a href="login.html">退出</a>
        </div>
        <div class="top">
                         
            <asp:Label ID="lblMap1" runat="server"></asp:Label>
                         
        </div>
    </form>
</body>
</html>
