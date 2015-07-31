using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;

namespace JMReports.WebApp.RoomEfficiency
{
  public partial class RoomEfficiencyReport : System.Web.UI.Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        if (HttpContext.Current.Session["CurrentCondition"] != null)
        {
          var Search = HttpContext.Current.Session["CurrentCondition"] as JMReports.Business.SearchCondition;
          this.ddlYear.SelectedValue = Search.YearCode;
          this.ddlMonth.SelectedValue = Search.MonthCode;
        }
        else
        {
          this.ddlYear.SelectedValue = System.DateTime.Now.Year.ToString();
          if (DateTime.Now.Month == 1)
          {
            this.ddlMonth.SelectedValue = "1";
          }
          else
          {
            this.ddlMonth.SelectedValue = (System.DateTime.Now.Month - 1).ToString();
          }
        }

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
      JMReports.Business.SearchCondition currentCondition = new Business.SearchCondition();
      currentCondition.Hotel = this.selDept.SelectedValue;
      currentCondition.YearCode = this.ddlYear.SelectedValue;
      currentCondition.MonthCode = this.ddlMonth.SelectedValue;
      HttpContext.Current.Session["CurrentCondition"] = currentCondition;

      ObjectDataSource ods = this.ObjectDataSource1;
      {
        ods.TypeName = " JMReports.WebApp.ReportBussiness.RoomEfficiencyReport";
        ods.SelectMethod = "getRoomEfficiency";
        ods.SelectParameters.Clear();
        ods.SelectParameters.Add("HotelId", this.selDept.SelectedValue.ToString());
        ods.SelectParameters.Add("mYear", this.ddlYear.SelectedValue.ToString());
        ods.SelectParameters.Add("mMonth", this.ddlMonth.SelectedValue.ToString());
      }

      //添加参数
      ReportViewer rv = this.ReportViewer1;
      {

        string title = string.Format("{0}{1} {2} 客房部效率表", this.ddlYear.SelectedItem.Text, this.ddlMonth.SelectedItem.Text, this.selDept.SelectedItem.Text);
        ReportParameter p1 = new ReportParameter("title", title);
        rv.LocalReport.SetParameters(new ReportParameter[] { p1 });

        rv.LocalReport.EnableHyperlinks = true;
        rv.PageCountMode = PageCountMode.Actual;
        rv.ShowBackButton = false;
        rv.ShowRefreshButton = false;
        rv.LocalReport.Refresh();
      }
    }
  }
}