<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test2.aspx.cs" Inherits="JMReports.WebApp.Test2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <script src="Scripts/jquery-1.10.2.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {         
           
                var data = {};
                $.ajax({
                    type: "POST",
                    url: "MenuByRole.ashx",
                    data: data,
                    success: function (response) {

                        //alert(response);
                        var dataobj = eval("(" + response + ")"); //转换为json对象  
                        //alert(dataobj);
                        //alert("Name:" + dataobj.Category);                        
                        var categories = dataobj.ReportByCategory;
                        for (var i = 0; i < categories.length; i++) {
                            $("#container").append("<tr><td>" + categories[i].Category + "</td><td></td></tr>");
                            var reports = categories[i].Reports;
                            for (var j = 0; j < reports.length; j++) {
                                $("#container").append("<tr><td>" + reports[j].ReportId + "</td><td>" + reports[j].ChineseName + "</td></tr>");
                            }
                            $("#container").append("<tr><td>============</td><td></td></tr>");

                        }
                    }        
            });  
        });
    </script>
</head>

<body>
    <form id="form1" runat="server">
    <div>
    <table id="container"></table>
       
    </div>
    </form>
</body>
</html>
