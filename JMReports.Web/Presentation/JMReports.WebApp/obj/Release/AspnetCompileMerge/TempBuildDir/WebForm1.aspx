<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="JMReports.WebApp.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link rel="stylesheet" href="Styles/table.css" type="text/css" />
    <script type="text/javascript" src="Scripts/jquery.js" ></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#hide").click(function () {
                $("p").hide();
            });
            $("#show").click(function () {
                $("p").show();
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="right">
	        <a href="http://www.baidu.com/" class="point-green1">
    	        <span class="ring-words1">日报表</span>
            </a>
            <a href="http://www.baidu.com/" class="point-yellow1">
    	        <span class="ring-words2">主要餐厅</span>
            </a>
            <a href="http://www.baidu.com/" class="point-blue1">
    	        <span class="ring-words3">客房收入<br/>市场细分</span>
            </a>
            <a href="http://www.baidu.com/" class="point-red1">
    	        <span class="ring-words4">计划<br/>报表</span>
            </a>
         </div>
    </form>
</body>
</html>
