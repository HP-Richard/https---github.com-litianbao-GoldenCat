<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="FormulaManage.aspx.cs" Inherits="JMReports.WebApp.Formula.FormulaManage" %>
<%@ Register Src="~/HotelByUser.ascx" TagPrefix="uc1" TagName="HotelByUser" %>
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
                <asp:DropDownList ID="ddlYear" runat="server" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged">
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
                会计科目
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
    <p></p>
    <hr />
    <p></p>
    
    <p>
        <asp:GridView ID="FormulaGV" runat="server" Width="100%" AutoGenerateColumns="False" AllowPaging="True" CssClass="table_style2" PageSize="15" OnPageIndexChanging="FormulaGV_PageIndexChanging">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:CheckBox ID="cb1" runat="server" />
                    </ItemTemplate>
                    <HeaderStyle Width="15px" />
                    <ItemStyle Width="15px" />
                </asp:TemplateField>
                <asp:BoundField DataField="ItemId" HeaderText="ItemID" >
                <FooterStyle Height="25px" />
                <HeaderStyle Height="25px" />
                </asp:BoundField>
                <asp:BoundField DataField="HotelName" HeaderText="酒店名称" />
                <asp:BoundField DataField="YearCode" HeaderText="年度" />
                <asp:BoundField DataField="AccountType" HeaderText="科目类型" >
                <HeaderStyle Height="25px" />
                </asp:BoundField>
                <asp:BoundField DataField="Department" HeaderText="科目名称" >
                <HeaderStyle Height="25px" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="Account">
                    <ItemTemplate>
                        <asp:TextBox ID="txtAccount" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Account") %>'></asp:TextBox>
                    </ItemTemplate>
                    <ControlStyle Width="150px" />
                    <HeaderStyle Width="150px" Height="25px" />
                    <ItemStyle Width="150px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Cost Center">
                    <ItemTemplate>
                        <asp:TextBox ID="txtCostCenter" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"CostCenter") %>'></asp:TextBox>
                    </ItemTemplate>
                    <ControlStyle Width="150px" />
                    <HeaderStyle Width="150px" Height="25px" />
                    <ItemStyle Width="150px" />
                </asp:TemplateField>
            </Columns>
            <PagerTemplate>
                当前第:
                <asp:Label ID="LabelCurrentPage" runat="server" Text="<%# ((GridView)Container.NamingContainer).PageIndex + 1 %>"></asp:Label>
                页/共:
                <asp:Label ID="LabelPageCount" runat="server" Text="<%# ((GridView)Container.NamingContainer).PageCount %>"></asp:Label>
                页
                <asp:LinkButton ID="LinkButtonFirstPage" runat="server" CommandArgument="First" CommandName="Page"
                    Visible='<%#((GridView)Container.NamingContainer).PageIndex != 0 %>'>首页</asp:LinkButton>
                <asp:LinkButton ID="LinkButtonPreviousPage" runat="server" CommandArgument="Prev"
                    CommandName="Page" Visible='<%# ((GridView)Container.NamingContainer).PageIndex != 0 %>'>上一页</asp:LinkButton>
                <asp:LinkButton ID="LinkButtonNextPage" runat="server" CommandArgument="Next" CommandName="Page"
                    Visible='<%# ((GridView)Container.NamingContainer).PageIndex != ((GridView)Container.NamingContainer).PageCount - 1 %>'>下一页</asp:LinkButton>
                <asp:LinkButton ID="LinkButtonLastPage" runat="server" CommandArgument="Last" CommandName="Page"
                    Visible='<%# ((GridView)Container.NamingContainer).PageIndex != ((GridView)Container.NamingContainer).PageCount - 1 %>'>尾页</asp:LinkButton>
                转到第
                <asp:TextBox ID="txtNewPageIndex" runat="server" Width="20px" Text='<%# ((GridView)Container.Parent.Parent).PageIndex + 1 %>' />页
                <asp:LinkButton ID="btnGo" runat="server" CausesValidation="False" CommandArgument="-2"
                    CommandName="Page" Text="GO" />

            </PagerTemplate>
        </asp:GridView>
    </p>
    <p>
        <asp:GridView ID="FormulaGV_2" runat="server" Width="100%" AutoGenerateColumns="False" AllowPaging="True" CssClass="table_style2" PageSize="15" OnPageIndexChanging="FormulaGV_PageIndexChanging">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:CheckBox ID="cb1" runat="server" />
                    </ItemTemplate>
                    <HeaderStyle Width="15px" />
                    <ItemStyle Width="15px" />
                </asp:TemplateField>
                <asp:BoundField DataField="ItemId" HeaderText="ItemID" >
                <FooterStyle Height="25px" />
                <HeaderStyle Height="25px" />
                </asp:BoundField>
                <asp:BoundField DataField="HotelName" HeaderText="酒店名称" />
                <asp:BoundField DataField="YearCode" HeaderText="年度" />
                <asp:BoundField DataField="AccountType" HeaderText="科目类型" >
                <HeaderStyle Height="25px" />
                </asp:BoundField>
                <asp:BoundField DataField="Department" HeaderText="科目名称" >
                <HeaderStyle Height="25px" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="Account From">
                    <ItemTemplate>
                        <asp:TextBox ID="txtAccount_From" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Account_From") %>'></asp:TextBox>
                    </ItemTemplate>
                    <ControlStyle Width="100px" />
                    <HeaderStyle Width="100px" Height="25px" />
                    <ItemStyle Width="100px" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Account To">
                    <ItemTemplate>
                        <asp:TextBox ID="txtAccount_to" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Account_To") %>'></asp:TextBox>
                    </ItemTemplate>
                    <ControlStyle Width="100px" />
                    <HeaderStyle Width="100px" Height="25px" />
                    <ItemStyle Width="100px" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Cost Center From">
                    <ItemTemplate>
                        <asp:TextBox ID="txtCostCenter_From" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"CostCenter_From") %>'></asp:TextBox>
                    </ItemTemplate>
                    <ControlStyle Width="100px" />
                    <HeaderStyle Width="100px" Height="25px" />
                    <ItemStyle Width="100px" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Cost Center To">
                    <ItemTemplate>
                        <asp:TextBox ID="txtCostCenter_To" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"CostCenter_To") %>'></asp:TextBox>
                    </ItemTemplate>
                    <ControlStyle Width="100px" />
                    <HeaderStyle Width="100px" Height="25px" />
                    <ItemStyle Width="100px" />
                </asp:TemplateField>
            </Columns>
            <PagerTemplate>
                当前第:
                <asp:Label ID="LabelCurrentPage" runat="server" Text="<%# ((GridView)Container.NamingContainer).PageIndex + 1 %>"></asp:Label>
                页/共:
                <asp:Label ID="LabelPageCount" runat="server" Text="<%# ((GridView)Container.NamingContainer).PageCount %>"></asp:Label>
                页
                <asp:LinkButton ID="LinkButtonFirstPage" runat="server" CommandArgument="First" CommandName="Page"
                    Visible='<%#((GridView)Container.NamingContainer).PageIndex != 0 %>'>首页</asp:LinkButton>
                <asp:LinkButton ID="LinkButtonPreviousPage" runat="server" CommandArgument="Prev"
                    CommandName="Page" Visible='<%# ((GridView)Container.NamingContainer).PageIndex != 0 %>'>上一页</asp:LinkButton>
                <asp:LinkButton ID="LinkButtonNextPage" runat="server" CommandArgument="Next" CommandName="Page"
                    Visible='<%# ((GridView)Container.NamingContainer).PageIndex != ((GridView)Container.NamingContainer).PageCount - 1 %>'>下一页</asp:LinkButton>
                <asp:LinkButton ID="LinkButtonLastPage" runat="server" CommandArgument="Last" CommandName="Page"
                    Visible='<%# ((GridView)Container.NamingContainer).PageIndex != ((GridView)Container.NamingContainer).PageCount - 1 %>'>尾页</asp:LinkButton>
                转到第
                <asp:TextBox ID="txtNewPageIndex" runat="server" Width="20px" Text='<%# ((GridView)Container.Parent.Parent).PageIndex + 1 %>' />页
                <asp:LinkButton ID="btnGo" runat="server" CausesValidation="False" CommandArgument="-2"
                    CommandName="Page" Text="GO" />
            </PagerTemplate>
        </asp:GridView>
    </p>
    <hr />
    <p></p>
    <table style="width:100%">
        <tr>
            <td style="width:20%"></td>
            <td style="width:20%">

                <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" Text="添加" Width="60px" />
            </td>
            <td style="width:20%"><asp:Button ID="btnSave" runat="server" Text="保 存" Width="80px" />
            </td>
            <td style="width:20%"><asp:Button ID="btnCancel" runat="server" Text="取  消" Width="80px" />
            </td>
            <td style="width:20%"></td>
        </tr>
    </table>
    <table style="width:90%">
        <tr>
            <td>
                
                <asp:Label ID="lblMessage" runat="server" Text="" style="font-size: x-large; color: #FF3300"></asp:Label>
                
            </td>
        </tr>
    </table>

</asp:Content>
