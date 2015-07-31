<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="JMReports.WebApp.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>金茂集团酒店数据智能分析平台</title>
    <link rel="stylesheet" href="Styles/login.css" type="text/css" />
    <script type="text/javascript">
        if (self != top) {
            top.location = self.location;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="loginbox">
                <div class="login_content">
                <table style="width:100%;height:100px">
                    <tr>
                        <td style="width:35%">
                            <asp:Label ID="Label1" runat="server" Text="用户名：" ></asp:Label>
                        </td>
                        <td style="width:65%">
                            <asp:TextBox ID="txtUserID" runat="server" Height="25px" Width="88%" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:35%">
                            <asp:Label ID="Label2" runat="server" Text="密&nbsp;&nbsp;&nbsp;&nbsp;码：" ></asp:Label>
                        </td>
                        <td style="width:65%">
                            <asp:TextBox ID="txtPassword" runat="server" Height="25px"  Width="88%" TextMode="Password"></asp:TextBox>
                        </td>
                    </tr>                  
                    <tr>
                        <td style="width:35%">
                            &nbsp;</td>
                        <td style="width:65%">
                            <asp:Button ID="btnLogin" runat="server" Height="30px" OnClick="btnLogin_Click" Text="登   录" Width="130px"  EnableViewState =" false"/>
                        </td>
                    </tr>                  
                    </table> 
                </div>
            </div>
    </form>
</body>
</html>
