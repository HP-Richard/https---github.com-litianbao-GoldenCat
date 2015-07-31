<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="UserHotel.aspx.cs" Inherits="JMReports.WebApp.User.UserHotel" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <table style="width:100%">
            <tr>
                <td style="width:30%" valign="top">
                        <asp:ListBox ID="lbUserList" runat="server" Width="99%" Height="400px" AutoPostBack="True" OnSelectedIndexChanged="lbRoleList_SelectedIndexChanged" CssClass="popup_content info"></asp:ListBox>
                </td>
                <td style="width:70%" valign="top">
                    <asp:GridView ID="gvHotel" runat="server" Width="100%" AutoGenerateColumns="False" DataKeyNames="HotelId" CssClass="table_style2">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox ID="cb1" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Code" HeaderText="酒店代码"></asp:BoundField>
                            <asp:BoundField DataField="Name" HeaderText="酒店英文名称" />
                            <asp:BoundField DataField="ChineseName" HeaderText="酒店名称" />
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
                        <asp:Button ID="btnCancel" runat="server" Text="取    消" OnClick="btnCancel_Click" />
                </td>
            </tr>
        </table>

</asp:Content>
