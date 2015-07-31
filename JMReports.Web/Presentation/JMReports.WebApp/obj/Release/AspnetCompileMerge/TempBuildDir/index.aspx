<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="JMReports.WebApp.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>金茂集团酒店数据智能分析平台</title>
    <link rel="stylesheet" href="Styles/login.css" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="loginbox">
                <div class="login_content">
                <table style="width:100%;height:100px">
                    <tr>
                        <td style="width:35%">
                            <asp:Label ID="Label1" runat="server" Text="用户名：" CssClass="lab" ></asp:Label>
                        </td>
                        <td style="width:65%" colspan="2">
                            <asp:TextBox ID="txtUserID" runat="server" Height="25px" Width="100%" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:35%">
                            <asp:Label ID="Label2" runat="server" Text="密码：" CssClass="lab" ></asp:Label>
                        </td>
                        <td style="width:65%" colspan="2">
                            <asp:TextBox ID="txtPassword" runat="server" Height="25px"  Width="100%" TextMode="Password"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:35%">
                            <asp:Label ID="Label3" runat="server" Text="验证码：" CssClass="lab" ></asp:Label>
                        </td>
                        <td style="width:35%">
                            <asp:TextBox ID="txtValidCode" runat="server" Height="25px"  Width="100%"></asp:TextBox>
                        </td>
                        <td  style="width:20%">
                            <asp:Image id="Image1" runat="server" ImageUrl="ValidateCode.aspx" Height="25px"></asp:Image>
                        </td>
                    </tr>
                    </table> 
                <table style="width:100%;height:50px">
                    <tr style="text-align:left">
                        <td style="width:30%">
                        </td>
                        <td style="width:70%">
                            <asp:Button ID="btnLogin" runat="server" Height="30px" OnClick="btnLogin_Click" Text="登   录" Width="130px" />
                        </td>
                    </tr>
                </table>
                </div>
            </div>
    </form>
</body>
</html>
