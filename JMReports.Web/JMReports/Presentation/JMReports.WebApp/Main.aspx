<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="JMReports.WebApp.Main" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>金茂集团酒店数据智能分析平台</title>
    <script type="text/javascript">
        if (self != top) {
            top.location = self.location;
        }
    </script>
</head>
<FRAMESET  bordercolor="#98FB98" rows="96px,*" FRAMEspacing=0 border=0>  
     <FRAME src="top.aspx" scrolling="no" id="0" name="top" noresize>  
      <FRAMESET frameborder="no"  border=0  id="systems" cols="200px,*">  
         <FRAME src="LeftTree.aspx" frameborder="0" id="1" name="1" noresize>  
         <FRAME src="DailyReport\frmDailyBI.aspx" frameborder="0" id="2" name="right">  
     </FRAMESET>  
</FRAMESET>  
</html>
