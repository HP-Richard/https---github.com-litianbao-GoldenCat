using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;

namespace JMReports.WebApp.RoomCompete
{
  public partial class RoomCompete : System.Web.UI.Page
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
      //set session value 
      JMReports.Business.SearchCondition currentCondition = new Business.SearchCondition();
      currentCondition.Hotel = this.selDept.SelectedValue;
      currentCondition.YearCode = this.ddlYear.SelectedValue;
      currentCondition.MonthCode = this.ddlMonth.SelectedValue;
      HttpContext.Current.Session["CurrentCondition"] = currentCondition;


      ObjectDataSource ods = this.ObjectDataSource1;
      {
        ods.TypeName = " JMReports.WebApp.ReportBussiness.RoomCompeteReport";
        ods.SelectMethod = "getCombinationDS";
        ods.SelectParameters.Clear();
        ods.SelectParameters.Add("HotelId", this.selDept.SelectedValue.ToString());
        ods.SelectParameters.Add("mYear", this.ddlYear.SelectedValue.ToString());
        ods.SelectParameters.Add("mMonth", this.ddlMonth.SelectedValue.ToString());
      }

      //添加参数
      ReportViewer rv = this.ReportViewer1;
      {
        string title = string.Format("{0}{1} {2} 客房竞争组合", this.ddlYear.SelectedItem.Text, this.ddlMonth.SelectedItem.Text, this.selDept.SelectedItem.Text);
        ReportParameter p1 = new ReportParameter("title", title);

        rv.LocalReport.SubreportProcessing += LocalReport_SubreportProcessing;
        rv.LocalReport.SetParameters(new ReportParameter[] { p1 });

        rv.LocalReport.EnableHyperlinks = true;
        rv.PageCountMode = PageCountMode.Actual;
        rv.ShowBackButton = false;
        rv.ShowRefreshButton = false;
        rv.LocalReport.Refresh();
      }
    }

    void LocalReport_SubreportProcessing(object sender, SubreportProcessingEventArgs e)
    {
      ObjectDataSource ods2 = this.ObjectDataSource2;
      {
        ods2.TypeName = " JMReports.WebApp.ReportBussiness.RoomCompeteReport";
        ods2.SelectMethod = "getReport2";
        ods2.SelectParameters.Clear();
        ods2.SelectParameters.Add("HotelId", this.selDept.SelectedValue.ToString());
        ods2.SelectParameters.Add("mYear", this.ddlYear.SelectedValue.ToString());
        ods2.SelectParameters.Add("mMonth", this.ddlMonth.SelectedValue.ToString());
        ods2.SelectParameters.Add("combination", e.Parameters["Combination"].Values[0]);
      }

      ObjectDataSource ods = this.ObjectDataSource3;
      {
        ods.TypeName = " JMReports.WebApp.ReportBussiness.RoomCompeteReport";
        ods.SelectMethod = "getRoomCompeteCurrentMonthDS";
        ods.SelectParameters.Clear();
        ods.SelectParameters.Add("HotelId", this.selDept.SelectedValue.ToString());
        ods.SelectParameters.Add("mYear", this.ddlYear.SelectedValue.ToString());
        ods.SelectParameters.Add("mMonth", this.ddlMonth.SelectedValue.ToString());
        ods.SelectParameters.Add("combination", e.Parameters["Combination"].Values[0]);
      }


      e.DataSources.Add(new ReportDataSource("RoomCompeteCurrentMonthDS", ods));
      e.DataSources.Add(new ReportDataSource("RoomCompeteDS2", ods2));
    }
  }
}