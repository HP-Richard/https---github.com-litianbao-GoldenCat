<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="UserPsdMod.aspx.cs" Inherits="JMReports.WebApp.User.UserPsdMod" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        function check() {
            if ($("#<%=this.txtPassword.ClientID%>")[0].value == "") {
                $("#<%=this.lblMessage.ClientID%>")[0].innerText = "请输入当前密码！";
                return false;
            }

            if ($("#<%=this.txtNewPassword.ClientID%>")[0].value == "") {
                $("#<%=this.lblMessage.ClientID%>")[0].innerText = "请输入新密码！";
                return false;
            }

            if ($("#<%=this.txtNewPassword.ClientID%>")[0].value != $("#<%=this.txtNewPasswordConfirm.ClientID%>")[0].value) {
                $("#<%=this.lblMessage.ClientID%>")[0].innerText = "新密码两次输入不一致，请重新输入!";
                return false;
            }
        };
    </script>
    <style type="text/css">
        div.panel
        {
            height: 350px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h5>密码修改</h5>
    <hr class="hr0" />

    <div id="divUser" runat="server" style="margin-left:20px;">
        <table class="table_style2">
            <tbody>
                <tr style="height:45px;">
                    <td style="width: 100px">
                        <asp:Label ID="Label2" runat="server" Width="100%">用　户:</asp:Label>
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblUserID" Width="80px" Height="18px" />
                    </td>
                </tr>
                <tr style="height:45px;">
                    <td>
                        <asp:Label runat="server" Width="100%">当前密码:</asp:Label>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtPassword" Width="200px" TextMode="Password" Height="18px" />
                    </td>
                </tr>
                <tr style="height:45px;">
                    <td>
                        <asp:Label runat="server" Width="100%">新密码:</asp:Label>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtNewPassword" Width="200px" TextMode="Password" Height="18px" />
                    </td>
                </tr>
                <tr style="height:45px;">
                    <td>
                        <asp:Label runat="server" Width="100%">新密码确认:</asp:Label>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtNewPasswordConfirm" Width="200px" TextMode="Password" Height="18px" />
                    </td>
                </tr>
                <tr style="height:45px;">
                    <td colspan="4">
                        <asp:Button ID="btnSave" runat="server" Text="保　存" Width="80px" OnClientClick="return check();" OnClick="btnSave_Click" />
                        <%--<asp:Label ID="lblId" runat="server"></asp:Label>--%>
                        <asp:HiddenField ID="hddId" runat="server" />
                    </td>
                </tr>
            </tbody>
        </table>
    </div>

    <asp:Label ID="lblMessage" runat="server" Style="font-size: large; color: #FF0000"></asp:Label>

</asp:Content>
