<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="RoleReport.aspx.cs" Inherits="JMReports.WebApp.Role.RoleReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <h2>角色报表管理</h2>
        <br />
        <table style="width:100%">
            <tr>
                <td style="width:30%" valign="top">
                        <asp:ListBox ID="lbRoleList" runat="server" Width="99%" Height="400px" AutoPostBack="True" OnSelectedIndexChanged="lbRoleList_SelectedIndexChanged" CssClass="popup_content info"></asp:ListBox>
                </td>
                <td style="width:70%" valign="top">
                    <asp:GridView ID="gvReport" runat="server" Width="100%" AutoGenerateColumns="False" DataKeyNames="ReportId" CssClass="table_style2">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox ID="cb1" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="ChineseName" HeaderText="报表名称" />
                            <asp:BoundField DataField="Description" HeaderText="报表描述" />
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td style="width:30%">
                        &nbsp;</td>
                <td style="width:70%" valign="top">
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="2">
                        <asp:Button ID="btnSave" runat="server" Text="保    存" OnClick="btnSave_Click" />
&nbsp;&nbsp;
                        <asp:Button ID="Button2" runat="server" Text="取    消" />
                </td>
            </tr>
        </table>

</asp:Content>
