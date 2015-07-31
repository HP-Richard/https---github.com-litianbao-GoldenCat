<%@ Page Title="" Language="C#" MasterPageFile="~/Report.Master" AutoEventWireup="true" CodeBehind="DataImportBudgetReport.aspx.cs" Inherits="JMReports.WebApp.Import.DataImportBudgetReport" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register Src="~/HotelByUser.ascx" TagPrefix="uc1" TagName="HotelByUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
          <table style="width:95%">
            <tr>
              <td style="width:80px" >
                  <asp:Label ID="Label2" runat="server" Text="酒店："></asp:Label>
              </td>
              <td   style="width:110px">
                 <%--<asp:DropDownList ID="selDept" runat="server"  Width="150px" >
                     <asp:ListItem></asp:ListItem>
                     <asp:ListItem Value="1">上海君悦</asp:ListItem>
                     <asp:ListItem Value="2">崇明凯悦</asp:ListItem>
                 </asp:DropDownList>--%>
                  <uc1:HotelByUser runat="server" id="selDept" Width="150px"/>
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
            <LocalReport ReportPath="Import\DataImportRpt.rdlc" EnableHyperlinks="true">
                <DataSources>
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="DataImportDs" />
                     <rsweb:ReportDataSource DataSourceId="ObjectDataSource2" Name="DataNotImportDs" />
               </DataSources>
            </LocalReport>
        </rsweb:ReportViewer>

        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server"></asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectDataSource2" runat="server"></asp:ObjectDataSource>
</asp:Content>
