<%@ Page Title="" Language="C#" MasterPageFile="~/Report.Master" AutoEventWireup="true" CodeBehind="frmDailyBI.aspx.cs" Inherits="JMReports.WebApp.DailyReport.frmDailyBI" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script type="text/javascript" src="../Scripts/My97DatePicker/WdatePicker.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <table width="90%">
            <tr>
              <td class="style1" >
                  &nbsp;</td>
              <td class="style1">
                  &nbsp;</td>
              <td class="style1">
                  
              </td>
              <td class="style1">
                  <asp:Label ID="lblMessage" runat="server" 
                      style="font-weight: 700; color: #FF3300"></asp:Label>
                </td>
              <td class="style1">
                  <asp:Label runat="server" Width="100%">日期</asp:Label>
                </td>
                <td class="style1">
                  <asp:TextBox ID="txtDatetime" class="Wdate" onClick="WdatePicker()" runat="server"  Width="100px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDatetime" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
                <td class="style1">
                    &nbsp;</td>
                <td class="style1">
                    &nbsp;</td>
                <td class="style1">
                    <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" 
                        Text=" 查 询 " CssClass="btn-primary" />
                </td>
                <td class="style1">
                    &nbsp;</td>
                <td class="style1">
                    &nbsp;</td>
                <td>
                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/DailyReport/frmDailyReport.aspx">返回表格形式</asp:HyperLink>
                </td>
            </tr>
        </table>
         <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="630px" 
            Width="100%" Font-Names="Verdana" Font-Size="8pt" 
            InteractiveDeviceInfos="(Collection)" WaitMessageFont-Names="Verdana" 
            WaitMessageFont-Size="14pt">
            <LocalReport ReportPath="DailyReport\DailyBI.rdlc">
                <DataSources>
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="RevenueDS" />
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource2" Name="RoomRevenueDS" />
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource3" Name="RestuarantRevenueDS" />
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource4" Name="OccupancyRateDS" />
                </DataSources>
            </LocalReport>
        </rsweb:ReportViewer>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server">
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectDataSource2" runat="server">
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectDataSource3" runat="server"></asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectDataSource4" runat="server"></asp:ObjectDataSource>
</asp:Content>
