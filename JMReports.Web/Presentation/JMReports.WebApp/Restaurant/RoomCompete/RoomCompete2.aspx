<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="RoomCompete2.aspx.cs" Inherits="JMReports.WebApp.RoomCompete.RoomCompete2" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register Src="~/HotelByUser.ascx" TagPrefix="uc1" TagName="HotelByUser" %>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">--%>
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
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <table style="width:95%">
            <tr>
              <td style="width:80px" >
                  <asp:Label ID="Label2" runat="server" Text="酒店："></asp:Label>
              </td>
              <td   style="width:110px">
                 <asp:DropDownList ID="selDept" runat="server"  Width="150px" >
                     <asp:ListItem></asp:ListItem>
                     <asp:ListItem Value="1">上海君悦</asp:ListItem>
                     <asp:ListItem Value="2">崇明凯悦</asp:ListItem>
                 </asp:DropDownList>
                  <%--<uc1:HotelByUser runat="server" id="selDept" Width="150px"/>--%>
              </td>
              <td   style="width:80px">
                 
                  <asp:Label ID="Label3" runat="server" Text="年份："></asp:Label>
                 
              </td>
              <td   style="width:80px">
                    <asp:DropDownList ID="ddlYear" runat="server">
                        <asp:ListItem Value="2013">2013年</asp:ListItem>
                        <asp:ListItem Value="2014" Selected="True">2014年</asp:ListItem>
                        <asp:ListItem Value="2015">2015年</asp:ListItem>
                    </asp:DropDownList>
                </td>
              <td   style="width:80px">
                    <asp:Label ID="Label4" runat="server" Text="月份："></asp:Label>
                </td>
                <td   style="width:80px">
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
                <td   style="width:80px">
                    &nbsp;</td>
                <td   style="width:80px">
                    &nbsp;</td>
                <td   style="width:80px">
                    &nbsp;</td>
                <td   style="width:80px">
                    &nbsp;</td>
                <td  >
                    <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" 
                        Text=" 查 询 " CssClass="btn-primary" Width="90px" />
                </td>
            </tr>
            <tr>
              <td   colspan="9">
                  <asp:Label ID="lblMessage" runat="server" style="font-weight: 700; color: #FF3300"></asp:Label>
              </td>
              <td  >
                  &nbsp;</td>
              <td  >
                  &nbsp;</td>
            </tr>
        </table>

        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="580px" 
            Width="100%" Font-Names="Verdana" Font-Size="8pt" 
            InteractiveDeviceInfos="(Collection)" WaitMessageFont-Names="Verdana" 
            WaitMessageFont-Size="14pt">
            <LocalReport ReportPath="RoomCompete\RoomCompeteReport2.rdlc">
                <DataSources>
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="CombinationDS" />
                    <%--<rsweb:ReportDataSource DataSourceId="ObjectDataSource2" Name="RoomCompeteDS2" />--%>
                </DataSources>
            </LocalReport>
        </rsweb:ReportViewer>

        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server"></asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectDataSource2" runat="server"></asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectDataSource3" runat="server"></asp:ObjectDataSource>
<%--</asp:Content>--%>
        </form>
        </body>
    </html>
