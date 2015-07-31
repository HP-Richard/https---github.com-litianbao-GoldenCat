<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="DataImportBudget.aspx.cs" Inherits="JMReports.WebApp.Import.DataImportBudget" %>
<%@ Register Src="~/HotelByUser.ascx" TagPrefix="uc1" TagName="HotelByUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <table>
        <tr>
            <td style="width:100px">
                
                <asp:Label ID="Label1" runat="server" Text="请选择导入文件:"></asp:Label>
               
            </td>
            <td><input id="FileExcel"  type="file" class="btn_file"  runat="server" width="400"/>
            </td>
            <td><asp:Button ID="btnImport" runat="server" Text="导入" CssClass="btn_simple" OnClick="btnImport_Click" /></td>

        </tr>
    </table>
    <table>
        <tr>
            <td style="width:100px">
                <asp:Label ID="Label3" runat="server" Text="请选择酒店:"></asp:Label>
            </td>
            <td>
                <%--<asp:DropDownList ID="selDept" runat="server"  Width="150px" >
                     <asp:ListItem></asp:ListItem>
                     <asp:ListItem Value="1">上海君悦</asp:ListItem>
                     <asp:ListItem Value="2">崇明凯悦</asp:ListItem>
                 </asp:DropDownList> --%>
                <uc1:HotelByUser runat="server" id="selDept" Width="150px"/>
            </td>
            <td style="width:80px"><asp:Label ID="Label4" runat="server" Text="请选择年份:"></asp:Label></td>
            <td>    
                <asp:DropDownList ID="ddlYear" runat="server" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged">
                        <asp:ListItem Value="2013">2013年</asp:ListItem>
                        <asp:ListItem Value="2014" Selected="True">2014年</asp:ListItem>
                        <asp:ListItem Value="2015">2015年</asp:ListItem>
                    </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width:100px">
                <asp:Label ID="Label7" runat="server" Text="导入模板下载："></asp:Label>
            </td>
            <td>
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Template/酒店数据导入_预算_上海君悦.xls">上海君悦</asp:HyperLink>
            &nbsp;<asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/Template/酒店数据导入_预算_崇明凯悦.xls">崇明凯悦</asp:HyperLink>
            &nbsp;<asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/Template/酒店数据导入_预算_深圳万豪.xls">深圳万豪</asp:HyperLink>
            </td>
            <td style="width:80px">&nbsp;</td>
            <td>    
                &nbsp;</td>
        </tr>
    </table>
    <p></p>
    <hr />
    <p></p>
    <table style="width:90%">
        <tr>
            <td>
                <asp:CheckBox ID="cbBudget" runat="server" Text="专用表_预算" />
            </td>
            <td>
                <asp:CheckBox ID="cbRoomEffBudget" runat="server" Text="客房部效率_预算" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:CheckBox ID="cbRestaurantEffBudget" runat="server" Text="各餐厅_预算" />
            </td>
            <td>
                <asp:CheckBox ID="cbRestaurantRevBudget" runat="server" Text="各餐厅收入_预算" />
            </td>
        </tr>
       
        <tr>
            <td>
                <asp:CheckBox ID="cbOtherBusinessBudget" runat="server" Text="其他运营部门_预算" />
            </td>
            <td>
                <asp:CheckBox ID="cbRoomMarket" runat="server" Text="客房市场细分_预算" />
            </td>
        </tr>
       
        <tr>
            <td>
                <asp:CheckBox ID="cbRoomSales" runat="server" Text="客房销售渠道_预算" />
            </td>
            <td>
                <asp:CheckBox ID="cbBanquet" runat="server" Text="宴会市场细分_预算" />
            </td>
        </tr>
       
    </table>
    <p></p>
    <hr />

    <p></p>
    <table style="width:90%">
        <tr>
            <td>
                
                <asp:Label ID="lblMessage" runat="server" Text="" style="font-size: x-large; color: #FF3300"></asp:Label>
                
            </td>
        </tr>
    </table>
</asp:Content>
