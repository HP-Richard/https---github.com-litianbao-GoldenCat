<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="WeeklyReportList.aspx.cs" Inherits="JMReports.WebApp.WeeklyReportInfo.WeeklyReportList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>周报管理
    </h2>
    <hr class="hr0" />
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width: 95%">
                <tr>
                    <td style="width: 60px">
                        <asp:Label ID="Label2" runat="server" Text="提报人："></asp:Label>
                    </td>
                    <td style="width: 110px">
                        <asp:TextBox ID="txtCreaterUser" runat="server"></asp:TextBox>
                    </td>
                    <td style="width: 20px">
                        <td style="width: 20px">
                            <asp:Label ID="Label3" runat="server" Text="周："></asp:Label>
                        </td>
                        <td style="width: 200px">
                            <asp:LinkButton runat="server" ID="lkbPrev" OnClick="lkbPrev_Click" Font-Size="Larger"><<</asp:LinkButton>
                            <asp:Label ID="lblFrom" runat="server" Text="" Font-Size="Larger"></asp:Label>
                            <asp:Label ID="Label1" runat="server" Text="~" Font-Size="Larger"></asp:Label>
                            <asp:Label ID="lblTo" runat="server" Text="" Font-Size="Larger"></asp:Label>
                            <asp:LinkButton runat="server" ID="lkbNext" OnClick="lkbNext_Click" Font-Size="Larger">>></asp:LinkButton>
                        </td>
                           <td style="width: 30px" />
                     <td style="width: 80px">
                            <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click"
                                Text=" 查 询 " CssClass="btn-primary" Width="90px" />
                        </td>
                        <td style="width: 80px">&nbsp;</td>
                        <td>&nbsp;</td>
                </tr>
            </table>

            <div style="width: 100%">
                <div style="float: right; margin: auto;">
                    <asp:HyperLink ID="HyperLink1" NavigateUrl="~/WeeklyReportInfo/WeeklyReportInfo.aspx" runat="server">新增周报</asp:HyperLink>
                </div>
                <asp:GridView ID="gvWeeklyReport" runat="server" CssClass="table_style2" DataKeyNames="Id" AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField Visible="false" DataField="Id" HeaderText="ID" />
                        <asp:BoundField DataField="SysUser.UserId" HeaderText="提报人" />
                        <asp:BoundField DataField="CreateTime" HeaderText="提报时间" />
                        <asp:BoundField DataField="Status" HeaderText="提报状态" />
                        <asp:TemplateField HeaderText="查看">
                            <ItemTemplate>
                                <a href='WeeklyReportInfo.aspx?ID=<%#DataBinder.Eval(Container.DataItem, "Id")%>'>查看周报</a>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        <table width="100%" class="table_style2">
                            <tr style="background-color:#E8E8E8">
                                <td>提报人</td>
                                <td>提报时间</td>
                                <td>提报状态</td>
                                <td>查看</td>
                            </tr>
                              <tr>
                                <td colspan="4" align="center">暂无数据</td>                                
                            </tr>
                      </table>
                    </EmptyDataTemplate>
                </asp:GridView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
