<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test3.aspx.cs" Inherits="JMReports.WebApp.Test3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="Scripts/jquery-1.10.2.js"></script>
    <script src="Scripts/Lockform.js"></script>

    <script >
        NCIC.LockForm.Init();
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
          <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="start" />
          测试
    </div>
        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="end" />







        <div class='words2'>
            <div id='hitButton1'>营销类报表<em class='iconDown'> </em></div>
        </div>
        <div class="group1">
            <div class='line2'>
                <div><a href='DailyReport/frmDailyReport.aspx' target='right' onclick='ReportMap(15)'>酒店日报表</a><em class='iconRight'></em></div>
            </div>
            <div class='line2'>
                <div><a href='Special/SpecialReport.aspx' target='right' onclick='ReportMap(1)'>酒店专用表</a><em class='iconRight'></em></div>
            </div>
            <div class='line2'>
                <div><a href='Operation/OperationReport.aspx' target='right' onclick='ReportMap(2)'>经营情况一览</a><em class='iconRight'></em></div>
            </div>
            <div class='line2'>
                <div><a href='HotelWhole/HotelWholeReport.aspx' target='right' onclick='ReportMap(3)'>酒店整体</a><em class='iconRight'></em></div>
            </div>
            <div class='line2'>
                <div><a href='RoomMarket/RoomMarketReport2.aspx' target='right' onclick='ReportMap(4)'>客房细分市场</a><em class='iconRight'></em></div>
            </div>
            <div class='line2'>
                <div><a href='RoomSales/RoomSalesReport.aspx' target='right' onclick='ReportMap(6)'>客房销售渠道</a><em class='iconRight'></em></div>
            </div>
            <div class='line2'>
                <div><a href='BanquetMarket/BanquetMarketReport.aspx' target='right' onclick='ReportMap(12)'>宴会细分市场</a><em class='iconRight'></em></div>
            </div>
            <div class='line2'>
                <div><a href='Unallocate/UnallocateReport.aspx' target='right' onclick='ReportMap(5)'>不可分摊成本费用</a><em class='iconRight'></em></div>
            </div>
            <div class='line2'>
                <div><a href='RoomCompete/RoomCompete.aspx' target='right' onclick='ReportMap(7)'>客房竞争组合</a><em class='iconRight'></em></div>
            </div>
            <div class='line2'>
                <div><a href='Restaurant/frmRestaurantVar.aspx' target='right' onclick='ReportMap(11)'>各餐厅</a><em class='iconRight'></em></div>
            </div>
            <div class='line2'>
                <div><a href='Restaurant/frmRestaurantMain.aspx' target='right' onclick='ReportMap(10)'>主要餐厅汇总</a><em class='iconRight'></em></div>
            </div>
            <div class='line2'>
                <div><a href='OtherBusiness/OtherBusinessRpt.aspx' target='right' onclick='ReportMap(14)'>其他运营部门</a><em class='iconRight'></em></div>
            </div>
        </div>
        <div class='words2'>
            <div id='hitButton2'>效率类报表<em class='iconDown'> </em></div>
        </div>
        <div class="group2">
            <div class='line2'>
                <div><a href='Forecast/ForecastRpt.aspx' target='right' onclick='ReportMap(8)'>预测报表</a><em class='iconRight'></em></div>
            </div>
            <div class='line2'>
                <div><a href='Restaurant/frmRestaurantEfficiency.aspx' target='right' onclick='ReportMap(13)'>餐饮部效率</a><em class='iconRight'></em></div>
            </div>
            <div class='line2'>
                <div><a href='RoomEfficiency/RoomEfficiencyReport.aspx' target='right' onclick='ReportMap(9)'>客房部效率</a><em class='iconRight'></em></div>
            </div>
            <div class='line2'>
                <div><a href='WeeklyReportInfo/WeeklyReportList.aspx' target='right' onclick='ReportMap(9003)'>周报管理</a><em class='iconRight'></em></div>
            </div>
        </div>
        <div class='words2'>
            <div id='hitButton3'>数据分析<em class='iconDown'> </em></div>
        </div>
        <div class="group3">
            <div class='line2'>
                <div><a href='Analysis/HotelAnalysis.aspx' target='right' onclick='ReportMap(7001)'>单家酒店数据分析</a><em class='iconRight'></em></div>
            </div>
        </div>
        <div class='words2'>
            <div id='hitButton4'>系统设置<em class='iconDown'> </em></div>
        </div>
        <div class="group4">
            <div class='line2'>
                <div><a href='User/UserManager.aspx' target='right' onclick='ReportMap(8001)'>用户管理</a><em class='iconRight'></em></div>
            </div>
            <div class='line2'>
                <div><a href='Role/RoleReport.aspx' target='right' onclick='ReportMap(8002)'>角色管理</a><em class='iconRight'></em></div>
            </div>
            <div class='line2'>
                <div><a href='Import/DataImport.aspx' target='right' onclick='ReportMap(8003)'>数据导入</a><em class='iconRight'></em></div>
            </div>
            <div class='line2'>
                <div><a href='Import/DataImportBudget.aspx' target='right' onclick='ReportMap(8004)'>预算导入</a><em class='iconRight'></em></div>
            </div>
            <div class='line2'>
                <div><a href='Import/DataImportReport.aspx' target='right' onclick='ReportMap(9001)'>数据导入情况</a><em class='iconRight'></em></div>
            </div>
            <div class='line2'>
                <div><a href='Import/DataImportBudgetReport.aspx' target='right' onclick='ReportMap(9002)'>预算导入情况</a><em class='iconRight'></em></div>
            </div>
            <div class='line2'>
                <div><a href='Export/DataExport.aspx' target='right' onclick='ReportMap(8005)'>报表导出</a><em class='iconRight'></em></div>
            </div>
            <div class='line2'>
                <div><a href='Formula/FormulaManage.aspx' target='right' onclick='ReportMap(8006)'>公式管理</a><em class='iconRight'></em></div>
            </div>
        </div>







    </form>






</body>
</html>
