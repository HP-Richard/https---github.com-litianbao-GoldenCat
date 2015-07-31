<%@ Page Title="" Language="C#" MasterPageFile="~/Report.Master" AutoEventWireup="true" CodeBehind="HotelAnalysis.aspx.cs" Inherits="JMReports.WebApp.Analysis.HotelAnalysis" %>
<%@ Register Src="~/HotelByUser.ascx" TagPrefix="uc1" TagName="HotelByUser" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

            <table>
        <tr>
            <td style="width:100px">
                <asp:Label ID="Label3" runat="server" Text="请选择酒店:"></asp:Label>
            </td>
            <td>
                <uc1:HotelByUser runat="server" id="selDept" Width="150px"/>
            </td>
            <td style="width:80px"><asp:Label ID="Label4" runat="server" Text="请选择年份:"></asp:Label></td>
            <td>    
                <asp:DropDownList ID="ddlYear" runat="server">
                        <asp:ListItem Value="2013">2013年</asp:ListItem>
                        <asp:ListItem Value="2014" Selected="True">2014年</asp:ListItem>
                        <asp:ListItem Value="2015">2015年</asp:ListItem>
                    </asp:DropDownList>

            </td>
               <td   style="width:80px">
                    <asp:Label ID="Label5" runat="server" Text="月份："></asp:Label>
                </td>
                <td   style="width:80px">
                    &nbsp;</td>
            <td>
                指标维度
            </td>
            <td>

                <asp:DropDownList ID="ddlAccountItem" runat="server" Width="300px">
                </asp:DropDownList>

            </td>
            <td>

            </td>
            <td>
                <asp:Button ID="btnSearch" runat="server" Text="查    询" OnClick="btnSearch_Click" Width="60px" />
            </td>
            <td>

                &nbsp;</td>

        </tr>
        </table>

<%--        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="580px" 
            Width="100%" Font-Names="Verdana" Font-Size="8pt" 
            InteractiveDeviceInfos="(Collection)" WaitMessageFont-Names="Verdana" 
            WaitMessageFont-Size="14pt">
            <LocalReport ReportPath="Analysis\HotelAnalysisReport.rdlc">
                <DataSources>
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="HotelAnalysis_Month_DataSet" />
                </DataSources>
            </LocalReport>
        </rsweb:ReportViewer>

        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server"></asp:ObjectDataSource>--%>


            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="580px" 
            Width="100%" Font-Names="Verdana" Font-Size="8pt" 
            InteractiveDeviceInfos="(Collection)" WaitMessageFont-Names="Verdana" 
            WaitMessageFont-Size="14pt">
            <LocalReport ReportPath="Analysis\HotelAnalysisReport.rdlc">
                <DataSources>
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="HotelAnalysisDataSet" />
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource2" Name="HotelAnalysis2DataSet" />
                </DataSources>
            </LocalReport>
        </rsweb:ReportViewer>

        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server"></asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjectDataSource2" runat="server"></asp:ObjectDataSource>
</asp:Content>
