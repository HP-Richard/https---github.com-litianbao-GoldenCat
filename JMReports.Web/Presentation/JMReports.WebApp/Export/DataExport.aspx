<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="DataExport.aspx.cs" Inherits="JMReports.WebApp.Export.DataExport" %>
<%@ Register Src="~/HotelByUser.ascx" TagPrefix="uc1" TagName="HotelByUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <table>
        <tr>
            <td style="width:100px">
                <asp:Label ID="Label3" runat="server" Text="请选择酒店:" Font-Size="10pt"></asp:Label>
            </td>
            <td>
                <%--<asp:DropDownList ID="selDept" runat="server"  Width="150px" >
                     <asp:ListItem></asp:ListItem>
                     <asp:ListItem Value="1">上海君悦</asp:ListItem>
                     <asp:ListItem Value="2">崇明凯悦</asp:ListItem>
                 </asp:DropDownList> --%>
                <uc1:HotelByUser runat="server" id="selDept" Width="150px"/>
            </td>
            <td style="width:80px"><asp:Label ID="Label4" runat="server" Text="请选择年份:" Font-Size="10pt"></asp:Label></td>
            <td>    
                <asp:DropDownList ID="ddlYear" runat="server" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged" Font-Size="10pt">
                        <asp:ListItem Value="2013">2013年</asp:ListItem>
                        <asp:ListItem Value="2014" Selected="True">2014年</asp:ListItem>
                        <asp:ListItem Value="2015">2015年</asp:ListItem>
                    </asp:DropDownList>

            </td>
               <td   style="width:80px">
                    <asp:Label ID="Label5" runat="server" Text="月份：" Font-Size="10pt"></asp:Label>
                </td>
                <td   style="width:80px">
                    <asp:DropDownList ID="ddlMonth" runat="server" Font-Size="10pt">
                        <asp:ListItem Value="1">1月份</asp:ListItem>
                        <asp:ListItem Value="2">2月份</asp:ListItem>
                        <asp:ListItem Value="3">3月份</asp:ListItem>
                        <asp:ListItem Value="4">4月份</asp:ListItem>
                        <asp:ListItem Value="5">5月份</asp:ListItem>
                        <asp:ListItem Value="6">6月份</asp:ListItem>
                        <asp:ListItem Value="7">7月份</asp:ListItem>
                        <asp:ListItem Value="8">8月份</asp:ListItem>
                        <asp:ListItem Value="9">9月份</asp:ListItem>
                        <asp:ListItem Value="10">10月份</asp:ListItem>
                        <asp:ListItem Value="11">11月份</asp:ListItem>
                        <asp:ListItem Value="12">12月份</asp:ListItem>
                    </asp:DropDownList>
                </td>

        </tr>
        </table>
    <p></p>
    <hr />
    <p></p>
    <table style="width:90%">
        <tr>
            <td>
                <asp:CheckBox ID="cbSpecial" runat="server" Text="专用表" Checked="True" Enabled="False" Font-Size="10pt" />
            </td>
            <td>
                <asp:CheckBox ID="cbSpecialForcast" runat="server" Text="经营情况一览" Checked="True" Enabled="False" Font-Size="10pt" />
            </td>
            <td>
                <asp:CheckBox ID="cbSpecialForcast0" runat="server" Text="酒店整体" Checked="True" Enabled="False" Font-Size="10pt" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:CheckBox ID="cbMarket" runat="server" Text="客房市场细分" Checked="True" Enabled="False" Font-Size="10pt" />
            </td>
            <td>
                <asp:CheckBox ID="cbSales" runat="server" Text="客房销售渠道" Checked="True" Enabled="False" Font-Size="10pt" />
            </td>
            <td>
                <asp:CheckBox ID="cbDinnerPart" runat="server" Text="宴会细分市场" Checked="True" Enabled="False" Font-Size="10pt" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:CheckBox ID="cbOtherBusiness0" runat="server" Text="不可分摊成本费用" Checked="True" Enabled="False" Font-Size="10pt" />
            </td>
            <td>
                <asp:CheckBox ID="cbCompete" runat="server" Text="客房竞争组合" Checked="True" Enabled="False" Font-Size="10pt" />
            </td>
            <td>
                <asp:CheckBox ID="cbRestaurantEfficiency0" runat="server" Text="各餐厅" Checked="True" Enabled="False" Font-Size="10pt" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:CheckBox ID="cbRestaurantEfficiency1" runat="server" Text="主要餐厅汇总" Checked="True" Enabled="False" Font-Size="10pt" />
            </td>
            <td>
                <asp:CheckBox Visible="false" ID="cbOtherBusiness" runat="server" Text="其他运营部门" Checked="True" Enabled="False" Font-Size="10pt" />
            </td>
            <td>
                <asp:CheckBox ID="cbForecast" runat="server" Text="预测" Checked="True" Enabled="False" Font-Size="10pt" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:CheckBox ID="cb4" runat="server" Text="客房部效率" Checked="True" Enabled="False" Font-Size="10pt" />
            </td>
            <td>
                <asp:CheckBox ID="cbRestaurantEfficiency" runat="server" Text="餐饮部效率" Checked="True" Enabled="False" Font-Size="10pt" />
            </td>
            <td>&nbsp;</td>
        </tr>
    </table>
    <p></p>
    <hr />
    <table>
        <tr>
            <td style="width:100px">
                
                <asp:Label ID="Label1" runat="server" Text="导出Excel文件:" Font-Size="10pt"></asp:Label>
               
            </td>
            <td>&nbsp;</td>
            <td>
                <asp:Button ID="btnExport" runat="server" Text="导出" Width="100px" OnClick="btnExport_Click" Font-Size="10pt" />
            </td>

        </tr>
    </table>
    <p></p>
    <table style="width:90%">
        <tr>
            <td>
                
                <asp:Label ID="lblMessage" runat="server" Text="" style="font-size: x-large; color: #FF3300"></asp:Label>
                
            </td>
        </tr>
    </table>
</asp:Content>
