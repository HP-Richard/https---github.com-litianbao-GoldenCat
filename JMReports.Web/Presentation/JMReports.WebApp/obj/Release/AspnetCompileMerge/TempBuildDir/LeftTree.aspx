<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LeftTree.aspx.cs" Inherits="JMReports.WebApp.LeftTree" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Styles/table.css" rel="stylesheet" />
    <script type="text/javascript" src="Scripts/jquery-1.10.2.js" ></script>
    <script type="text/javascript" src="Scripts/common.js"></script>
    <script type="text/javascript">
        function ReportMap(type1) {
            //var map1 = "DailyReport";
            parent.document.frames[0].location = "top.aspx?v1=" + type1;
        }
    </script>
</head>
<body style="background-color:steelblue">
    <form id="form1" runat="server">
    <div>
            <asp:Literal ID="Literal1" runat="server"></asp:Literal>
    </div>
    </form>
</body>
</html>
