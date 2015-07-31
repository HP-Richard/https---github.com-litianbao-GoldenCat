<%@ Page Title="" Language="C#" MasterPageFile="~/Report.Master" AutoEventWireup="true" CodeBehind="frmDailyReport.aspx.cs" Inherits="JMReports.WebApp.DailyReport.frmDailyReport" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <table width="90%">
            <tr>
              <td class="style1" width="80">
                  <asp:Label runat="server" AssociatedControlID="selDept" Width="100%">酒店</asp:Label>
              </td>
              <td class="style1" width="110">
                 <asp:DropDownList ID="selDept" runat="server" Width="200" >
                     <asp:ListItem></asp:ListItem>
                     <asp:ListItem Value="上海君悦">上海君悦</asp:ListItem>
                     <asp:ListItem Value="崇明凯悦">崇明凯悦</asp:ListItem>
                 </asp:DropDownList>
              </td>
              <td class="style1" width="80">
                  <asp:Label runat="server" AssociatedControlID="selDept" Width="100%">日期</asp:Label>
              </td>
              <td class="style1" width="120">
                  <asp:TextBox runat="server" ID="txtDatetime" Width="100px" >2014-01-01</asp:TextBox>
                </td>
              <td class="style1" width="80">
                  &nbsp;</td>
                <td class="style1" width="10%">
                    &nbsp;</td>
                <td class="style1" width="80">
                    &nbsp;</td>
                <td class="style1" width="10%">
                    &nbsp;</td>
                <td class="style1" width="80">
                </td>
                <td class="style1" width="70">
                    &nbsp;</td>
                <td class="style1">
                    <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" 
                        Text=" 查 询 " CssClass="btn-primary" />
                </td>
            </tr>
            <tr>
              <td class="style1" colspan="9">
                  <asp:Label ID="lblMessage" runat="server" 
                      style="font-weight: 700; color: #FF3300"></asp:Label>

              </td>
              <td class="style1">
                  &nbsp;</td>
              <td class="style1">
                  &nbsp;</td>
            </tr>
        </table>
          <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="565px" 
            Width="100%" Font-Names="Verdana" Font-Size="8pt" 
            InteractiveDeviceInfos="(Collection)" WaitMessageFont-Names="Verdana" 
            WaitMessageFont-Size="14pt">
            <LocalReport ReportPath="DailyReport\DailyReport.rdlc">
                <DataSources>
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="DailyReport" />
                </DataSources>
            </LocalReport>
        </rsweb:ReportViewer>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server">
        </asp:ObjectDataSource>
</asp:Content>
