<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="UserManager.aspx.cs" Inherits="JMReports.WebApp.User.UserManager" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        $(document).ready(function () {
            $("#btnCancel").click(function () {
                $("#divUser").hide();
            });
            $("#btnAdd").click(function () {
                $("#divUser").show();
            });
        });
    </script>
    <style type="text/css"> 
        div.panel
        {
            height:350px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h5>用户管理</h5>
    <hr class="hr0"/>

    <div id="divUser" runat="server">
        <table class="table_style2" style="width:70%">
               <tbody>
                  <tr>
                     <td style="width:20%">
                        <asp:Label runat="server"  Width="100%">用户ID:</asp:Label>
                     </td>
                     <td>
                         <asp:TextBox runat="server" ID="txtUserId" Width="80%"  />
                     </td>
                     <td style="width:20%">
                        <asp:Label runat="server"  Width="100%">密码:</asp:Label>
                     </td>
                     <td>
                         <asp:TextBox runat="server" ID="txtPassword" Width="80%"  />
                     </td>
                  </tr>
                  <tr>
                     <td>
                        <asp:Label runat="server" Width="100%">电子邮件</asp:Label>
                     </td>
                     <td>
                         <asp:TextBox runat="server" ID="txtEmail" Width="80%" />
                     </td>
                     <td style="width:20%">
                        <asp:Label runat="server"  Width="100%">密码确认:</asp:Label>
                     </td>
                     <td>
                         <asp:TextBox runat="server" ID="txtConfirmPassword" Width="80%" />
                     </td>
                  </tr>
                  <tr>
                     <td>
                         <asp:Label runat="server"  Width="100%">角色</asp:Label>
                     </td>
                     <td>
                         <asp:DropDownList ID="ddlRole" runat="server" Width="80%" >
                         </asp:DropDownList>
                     </td>
                     <td>
                        
                      </td>
                      <td>
                     </td>
                  </tr>
                  <tr>
                     <td colspan="4">
                         <asp:Button ID="btnSave" runat="server" Text="保   存" Width="100px" OnClick="btnSave_Click" />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  
                             <asp:Button ID="btnCancel" runat="server" Width="100px" Text="取   消" OnClick="btnCancel_Click" />
                     </td>
                  </tr>
               </tbody>
          </table>
    </div>

    <br />
    <div style="width:100%">
        <asp:GridView ID="gvUserList" runat="server" CssClass="table_style2" AutoGenerateColumns="False">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:CheckBox ID="cb1" runat="server" />
                    </ItemTemplate>
                    <HeaderStyle Width="15px" />
                    <ItemStyle Width="15px" />
                </asp:TemplateField>
                <asp:BoundField DataField="UserId" HeaderText="用户ID" />
                <asp:BoundField DataField="RoleName" HeaderText="角色" />
                <asp:BoundField DataField="Email" HeaderText="电子邮件" />
                <asp:BoundField DataField="Status" HeaderText="状态" />
            </Columns>
        </asp:GridView>
    </div>

    <div>
        <table style="width:100%">
            <tr>
                <td style="width:20%">
                    <asp:Button ID="btnAdd" runat="server" Width="100px" Text="用户添加" OnClick="btnAdd_Click" />
                </td>
                <td>
                    <asp:Button ID="Button2" runat="server" Width="100px" Text="用户删除" OnClick="Button2_Click" />
                </td>
                <td>3</td>
            </tr>
        </table>
    </div>
</asp:Content>
