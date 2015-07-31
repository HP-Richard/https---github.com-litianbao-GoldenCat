using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;

namespace JMReports.WebApp.Import
{
  public partial class DataImportBudgetReport : System.Web.UI.Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        this.ddlYear.SelectedValue = System.DateTime.Now.Year.ToString();

        #region 页面启动时加载报表
        string myScript = @"$(document).ready(function () {
                            var checkValue=$(""#ContentPlaceHolder1_selDept_selDept"").val();  //获取Select选择的Value
                            if (checkValue != ''){$("".btn-primary"").click();}});";
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "MyScript", myScript, true);
        #endregion
      }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
      ObjectDataSource ods = this.ObjectDataSource1;
      {
        ods.TypeName = " JMReports.WebApp.ReportBussiness.DataImportReport";
        ods.SelectMethod = "getDataImportBudgetReport";
        ods.SelectParameters.Clear();
        ods.SelectParameters.Add("HotelId", this.selDept.SelectedValue.ToString());
        ods.SelectParameters.Add("mYear", this.ddlYear.SelectedValue.ToString());
        var url = Request.Url.AbsoluteUri;
        var index = url.LastIndexOf('/');
        url = url.Substring(0, index);
        ods.SelectParameters.Add("urlHeaderPath", url);
      }

      ods = this.ObjectDataSource2;
      {
        ods.TypeName = " JMReports.WebApp.ReportBussiness.DataImportReport";
        ods.SelectMethod = "getDataNotImportBudgetReport";
        ods.SelectParameters.Clear();
        ods.SelectParameters.Add("HotelId", this.selDept.SelectedValue.ToString());
        ods.SelectParameters.Add("mYear", this.ddlYear.SelectedValue.ToString());
      }

      //添加参数
      ReportViewer rv = this.ReportViewer1;
      {
        rv.LocalReport.EnableHyperlinks = true;
        string title = string.Format("{0} {1} 导入预算报表情况", this.ddlYear.SelectedItem.Text, this.selDept.SelectedItem.Text);
        ReportParameter p1 = new ReportParameter("title", title);
        rv.LocalReport.SetParameters(new ReportParameter[] { p1 });

        rv.PageCountMode = PageCountMode.Actual;
        rv.ShowBackButton = false;
        rv.ShowRefreshButton = false;
        rv.LocalReport.Refresh();
      }
    }
  }
}