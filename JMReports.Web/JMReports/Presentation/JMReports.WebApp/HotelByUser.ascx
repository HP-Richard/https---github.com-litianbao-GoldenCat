<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HotelByUser.ascx.cs" Inherits="JMReports.WebApp.HotelByUser1" %>
<asp:DropDownList ID="selDept" runat="server" Width="100px">
    <asp:ListItem></asp:ListItem>
    <%--<asp:ListItem Value="1">上海君悦</asp:ListItem>
    <asp:ListItem Value="2">崇明凯悦</asp:ListItem>--%>
</asp:DropDownList><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="selDept" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>

