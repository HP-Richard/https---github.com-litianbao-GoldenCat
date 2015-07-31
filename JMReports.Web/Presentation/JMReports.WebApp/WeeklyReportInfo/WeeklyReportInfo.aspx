<%@ Page Title="" Language="C#" MasterPageFile="~/Report.Master" AutoEventWireup="true" CodeBehind="WeeklyReportInfo.aspx.cs" Inherits="JMReports.WebApp.WeeklyReportInfo.WeeklyReportInfo" ValidateRequest="false" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Src="~/HotelByUser.ascx" TagPrefix="uc1" TagName="HotelByUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta http-equiv="Content-Type" content="text/html;charset=utf-8" />
    <title>UMEDITOR 完整demo</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../umeditor/themes/default/css/umeditor.css" type="text/css" rel="stylesheet">
    <script type="text/javascript" src="../umeditor/third-party/jquery.min.js"></script>
    <script type="text/javascript" charset="utf-8" src="../umeditor/umeditor.config.js"></script>
    <script type="text/javascript" charset="utf-8" src="../umeditor/umeditor.min.js"></script>
    <script type="text/javascript" src="../umeditor/lang/zh-cn/zh-cn.js"></script>
    <script type="text/javascript" src="../Scripts/My97DatePicker/WdatePicker.js"></script>
    <style type="text/css">
        h1
        {
            font-family: "微软雅黑";
            font-weight: normal;
        }

        .btn
        {
            display: inline-block;
            *border-left: 0 none #e6e6e6;
            border-right: 0 none #e6e6e6;
            border-top: 0 none #e6e6e6;
            border-bottom: 0 none #b3b3b3;
            display: inline;
            padding: 4px 12px;
            margin-bottom: 0;
            *margin-left: .3em;
            font-size: 14px;
            line-height: 20px;
            color: #333333;
            text-align: center;
            text-shadow: 0 1px 1px rgba(255, 255, 255, 0.75);
            vertical-align: middle;
            cursor: pointer;
            background-color: #f5f5f5;
            *background-color: #e6e6e6;
            background-repeat: repeat-x;
            *-webkit-border-radius: 4px;
            -moz-border-radius: 4px;
            border-radius: 4px;
            zoom: 1;
            -webkit-box-shadow: inset 0 1px 0 rgba(255, 255, 255, 0.2), 0 1px 2px rgba(0, 0, 0, 0.05);
            -moz-box-shadow: inset 0 1px 0 rgba(255, 255, 255, 0.2), 0 1px 2px rgba(0, 0, 0, 0.05);
            box-shadow: inset 0 1px 0 rgba(255, 255, 255, 0.2), 0 1px 2px rgba(0, 0, 0, 0.05);
            background-image: linear-gradient(to bottom, #ffffff, #e6e6e6);*
        }

            .btn:hover,
            .btn:focus,
            .btn:active,
            .btn.active,
            .btn.disabled,
            .btn[disabled]
            {
                color: #333333;
                background-color: #e6e6e6;
                *background-color: #d9d9d9;
            }

            .btn:active,
            .btn.active
            {
                background-color: #cccccc \9;
            }

            .btn:first-child
            {
                *margin-left: 0;
            }

            .btn:hover,
            .btn:focus
            {
                color: #333333;
                text-decoration: none;
                background-position: 0 -15px;
                -webkit-transition: background-position 0.1s linear;
                -moz-transition: background-position 0.1s linear;
                -o-transition: background-position 0.1s linear;
                transition: background-position 0.1s linear;
            }

            .btn:focus
            {
                outline: thin dotted #333;
                outline: 5px auto -webkit-focus-ring-color;
                outline-offset: -2px;
            }

            .btn.active,
            .btn:active
            {
                background-image: none;
                outline: 0;
                -webkit-box-shadow: inset 0 2px 4px rgba(0, 0, 0, 0.15), 0 1px 2px rgba(0, 0, 0, 0.05);
                -moz-box-shadow: inset 0 2px 4px rgba(0, 0, 0, 0.15), 0 1px 2px rgba(0, 0, 0, 0.05);
                box-shadow: inset 0 2px 4px rgba(0, 0, 0, 0.15), 0 1px 2px rgba(0, 0, 0, 0.05);
            }

            .btn.disabled,
            .btn[disabled]
            {
                cursor: default;
                background-image: none;
                opacity: 0.65;
                filter: alpha(opacity=65);
                -webkit-box-shadow: none;
                -moz-box-shadow: none;
                box-shadow: none;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>周报信息</h2>
    <hr class="hr0" />

    <div id="Div1" runat="server">
        <asp:HiddenField ID="hdIsSubmited" runat="server"></asp:HiddenField>
        <table id="tab" style="width: 70%">
            <tbody>
                <tr>
                    <td>
                        <h3>一.  本周经营信息摘要：</h3>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table width="90%">
                            <tr>
                                <td class="style1" width="80">
                                    <asp:Label ID="Label1" runat="server" AssociatedControlID="selDept" Width="100%">酒店</asp:Label>
                                </td>
                                <td class="style1" width="110">
                                    <%--<asp:DropDownList ID="selDept" runat="server" Width="200" >
                     <asp:ListItem></asp:ListItem>
                     <asp:ListItem Value="1">上海君悦</asp:ListItem>
                     <asp:ListItem Value="2">崇明凯悦</asp:ListItem>
                 </asp:DropDownList>--%>
                                    <uc1:HotelByUser runat="server" ID="selDept" Width="150px" />
                                </td>
                                <td class="style1" width="80">
                                    <asp:Label ID="Label2" runat="server" AssociatedControlID="selDept" Width="100%">日期</asp:Label>
                                </td>
                                <td class="style1" width="120">
                                    <asp:TextBox ID="txtDatetime" class="Wdate" onClick="WdatePicker()" runat="server" Width="100px"></asp:TextBox>
                                </td>
                                <td class="style1" width="80">&nbsp;</td>
                                <td class="style1" width="10%">&nbsp;</td>
                                <td class="style1" width="80">&nbsp;</td>
                                <td class="style1" width="10%">&nbsp;</td>
                                <td class="style1" width="80"></td>
                                <td class="style1" width="70">&nbsp;</td>
                                <td class="style1">
                                    <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click"
                                        Text=" 查 询 " CssClass="btn-primary" />
                                </td>
                            </tr>
                            <tr>
                                <td class="style1" colspan="9">
                                    <asp:Label ID="Label3" runat="server"
                                        Style="font-weight: 700; color: #FF3300"></asp:Label>

                                </td>
                                <td class="style1">&nbsp;</td>
                                <td class="style1">&nbsp;</td>
                            </tr>
                        </table>
                        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="385px"
                            Width="100%" Font-Names="Verdana" Font-Size="8pt"
                            InteractiveDeviceInfos="(Collection)" WaitMessageFont-Names="Verdana"
                            WaitMessageFont-Size="14pt">
                            <LocalReport ReportPath="WeeklyReportInfo\WeeklyReport.rdlc">
                                <DataSources>
                                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="DailyReport" />
                                </DataSources>
                            </LocalReport>
                        </rsweb:ReportViewer>
                        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server"></asp:ObjectDataSource>
                    </td>
                </tr>
                <tr>
                    <td>
                        <h3>二.  本周HSE重点工作</h3>
                    </td>
                </tr>
                <tr>
                    <td>
                        <!--style给定宽度可以影响编辑器的最终宽度-->
                        <script type="text/plain" id="myEditor1" style="width: 90%; height: 240px;"></script>
                        <asp:HiddenField ID="txtContext1" runat="server"></asp:HiddenField>
                    </td>
                </tr>
                <tr>
                    <td>
                        <h3>三.  本周现场团队重点工作和工作异常点</h3>
                    </td>
                </tr>
                <tr>
                    <td>
                        <script type="text/plain" id="myEditor2" style="width: 90%; height: 240px;"></script>
                        <asp:HiddenField ID="txtContext2" runat="server"></asp:HiddenField>
                    </td>
                </tr>
                <tr>
                    <td>
                        <h3>四.  本周酒店获奖情况</h3>
                    </td>
                </tr>
                <tr>
                    <td>
                        <script type="text/plain" id="myEditor3" style="width: 90%; height: 240px;"></script>
                        <asp:HiddenField ID="txtContext3" runat="server"></asp:HiddenField>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%; text-align: center;">
                        <asp:Button ID="btnSave" runat="server" Width="100px" Text="保 存" OnClientClick="return beforeSave();" OnClick="btnSave_Click" />&nbsp;&nbsp;
                         <asp:Button ID="btnSubmit" runat="server" Width="100px" Text="提 交" OnClientClick="return beforeSave();" OnClick="btnSubmit_Click" />&nbsp;&nbsp;
                          <asp:Button ID="btnDelete" runat="server" Width="100px" Text="删 除" OnClientClick="if(!confirm('确定要删除数据吗？')) return false;" OnClick="btnDelete_Click" />&nbsp;&nbsp;
                       <asp:Button ID="btnBack" runat="server" Width="100px" Text="返 回" OnClientClick="javascript:location.href='WeeklyReportList.aspx';return false;" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblMessage" runat="server" Style="font-size: large; color: #FF0000"></asp:Label></td>
                </tr>
            </tbody>
        </table>
    </div>

    <div>
        <h3 id="focush2"></h3>
    </div>
    <script type="text/javascript">
        //实例化编辑器
        var um = UM.getEditor('myEditor1');
        var um = UM.getEditor('myEditor2');
        var um = UM.getEditor('myEditor3');
        //um.addListener('blur', function () {
        //    $('#focush2').html('编辑器失去焦点了')
        //});
        //um.addListener('focus', function () {
        //    $('#focush2').html('')
        //});
        if ($("#<%=hdIsSubmited.ClientID%>")[0].value == "true") { setDisabled(); }
        setContent();
        function hasContent(editorId) {
            return UM.getEditor(editorId).hasContents();
        }
        function setContent() {
            var hd1 = $("#<%=txtContext1.ClientID%>")[0];
            UM.getEditor('myEditor1').setContent(hd1.value, false);
            var hd2 = $("#<%=txtContext2.ClientID%>")[0];
            UM.getEditor('myEditor2').setContent(hd2.value, false);
            var hd3 = $("#<%=txtContext3.ClientID%>")[0];
            UM.getEditor('myEditor3').setContent(hd3.value, false);
        }
        function beforeSave() {
            if (hasContent('myEditor1') && hasContent('myEditor2') && hasContent('myEditor3')) {
                var hd1 = $("#<%=txtContext1.ClientID%>")[0];
                hd1.value = UM.getEditor('myEditor1').getContent();
                var hd2 = $("#<%=txtContext2.ClientID%>")[0];
                hd2.value = UM.getEditor('myEditor2').getContent();
                var hd3 = $("#<%=txtContext3.ClientID%>")[0];
                hd3.value = UM.getEditor('myEditor3').getContent();
            }
            else {
                $("#<%=lblMessage.ClientID%>")[0].innerText = "请输入周报内容！";
                return false;
            }
        }
        function setDisabled() {
            UM.getEditor('myEditor1').setDisabled();
            UM.getEditor('myEditor2').setDisabled();
            UM.getEditor('myEditor3').setDisabled();
        }
        function setCollapse() {
            UM.getEditor('myEditor1').setDisabled();
            UM.getEditor('myEditor2').setDisabled();
            UM.getEditor('myEditor3').setDisabled();
        }

    </script>
</asp:Content>
