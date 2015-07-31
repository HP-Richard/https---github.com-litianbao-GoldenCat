<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="DataImport.aspx.cs" Inherits="JMReports.WebApp.Import.DataImport" %>

<%@ Register Src="~/HotelByUser.ascx" TagPrefix="uc1" TagName="HotelByUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <table>
        <tr>
            <td style="width: 100px">
                <asp:Label ID="Label3" runat="server" Text="请选择酒店:"></asp:Label>
            </td>
            <td>
                <%--<asp:DropDownList ID="selDept" runat="server"  Width="150px" >
                     <asp:ListItem></asp:ListItem>
                     <asp:ListItem Value="1">上海君悦</asp:ListItem>
                     <asp:ListItem Value="2">崇明凯悦</asp:ListItem>
                 </asp:DropDownList> --%>
                <uc1:HotelByUser runat="server" ID="selDept"  />
            </td>
            <td style="width: 80px">
                <asp:Label ID="Label4" runat="server" Text="请选择年份:"></asp:Label></td>
            <td>
                <asp:DropDownList ID="ddlYear" runat="server" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged">
                    <asp:ListItem Value="2013">2013年</asp:ListItem>
                    <asp:ListItem Value="2014" Selected="True">2014年</asp:ListItem>
                    <asp:ListItem Value="2015">2015年</asp:ListItem>
                </asp:DropDownList>

            </td>
            <td style="width: 80px">
                <asp:Label ID="Label5" runat="server" Text="月份："></asp:Label>
            </td>
            <td style="width: 80px">
                <asp:DropDownList ID="ddlMonth" runat="server">
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
        <tr>
            <td style="width: 100px">
                <asp:Label ID="Label7" runat="server" Text="导入模板下载："></asp:Label>
            </td>
            <td>
                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Template/酒店数据导入_上海君悦.xls">上海君悦</asp:HyperLink>
                &nbsp;<asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/Template/酒店数据导入_崇明凯悦.xls">崇明凯悦</asp:HyperLink>
                &nbsp;<asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/Template/酒店数据导入_深圳万豪.xls">深圳万豪</asp:HyperLink>
                &nbsp;<asp:HyperLink ID="HyperLink5" runat="server" NavigateUrl="~/Template/酒店数据导入_三亚希尔顿.xls">三亚希尔顿</asp:HyperLink>
                &nbsp;<asp:HyperLink ID="HyperLink6" runat="server" NavigateUrl="~/Template/酒店数据导入_北京万丽.xls">北京万丽</asp:HyperLink>
                &nbsp;<asp:HyperLink ID="HyperLink7" runat="server" NavigateUrl="~/Template/酒店数据导入_三亚丽兹.xls">三亚丽兹</asp:HyperLink>
                &nbsp;<asp:HyperLink ID="HyperLink8" runat="server" NavigateUrl="~/Template/酒店数据导入_丽江君悦.xls">丽江君悦</asp:HyperLink>
                &nbsp;<asp:HyperLink ID="HyperLink9" runat="server" NavigateUrl="~/Template/酒店数据导入_南京威斯汀.xls">南京威斯汀</asp:HyperLink>
                &nbsp;<asp:HyperLink ID="HyperLink10" runat="server" NavigateUrl="~/Template/酒店数据导入_北京威斯汀.xls">北京威斯汀</asp:HyperLink>
            </td>
            <td style="width: 80px"></td>
            <td>&nbsp;</td>
            <td style="width: 80px">&nbsp;</td>
            <td style="width: 80px">&nbsp;</td>

        </tr>
    </table>
    <p></p>
    <hr />
    <p></p>
    <table style="width: 90%">
        <tr>
            <td>
                <asp:CheckBox ID="cbSpecial" runat="server" Text="专用表" />
            </td>
            <td>
                <asp:CheckBox ID="cbSpecialForcast" runat="server" Text="专用表_预测" />
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:CheckBox ID="cbRestaurantEfficiency" runat="server" Text="餐饮部效率" />
            </td>
            <td>
                <asp:CheckBox Visible="false" ID="cbOtherBusiness" runat="server" Text="其他运营部门" />
            </td>
            <td>
                <asp:CheckBox Visible="false" ID="cb4" runat="server" Text="客房部效率" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label6" runat="server" Style="font-weight: 700" Text="市场类报表"></asp:Label>
            </td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:CheckBox ID="cbMarket" runat="server" Text="客房市场细分" />
            </td>
            <td>
                <asp:CheckBox ID="cbSales" runat="server" Text="客房销售渠道" />
            </td>
            <td>
                <asp:CheckBox ID="cbDinnerPart" runat="server" Text="宴会细分市场" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:CheckBox ID="cbForecast" runat="server" Text="预测" />
            </td>
            <td>
                <asp:CheckBox ID="cbCompete" runat="server" Text="客房竞争组合" />
            </td>
            <td>&nbsp;</td>
        </tr>
    </table>
    <p></p>
    <hr />
    <table>
        <tr>
            <td style="width: 100px">

                <asp:Label ID="Label1" runat="server" Text="请选择导入文件:"></asp:Label>

            </td>
            <td>
                <input id="FileExcel" type="file" class="btn_file" runat="server" width="400" />
            </td>
            <td>
                <asp:Button ID="btnImport" runat="server" Text="导入" CssClass="btn_simple" OnClick="btnImport_Click" /></td>

        </tr>
    </table>
    <p></p>
    <table style="width: 90%">
        <tr>
            <td>

                <asp:Label ID="lblMessage" runat="server" Text="" Style="font-size: x-large; color: #FF3300"></asp:Label>

            </td>
        </tr>
    </table>
</asp:Content>
