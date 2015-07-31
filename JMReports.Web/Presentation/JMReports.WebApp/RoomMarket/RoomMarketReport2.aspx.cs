using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;

namespace JMReports.WebApp.RoomMarket
{
  public partial class RoomMarketReport2 : System.Web.UI.Page
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
        ods.TypeName = " JMReports.WebApp.ReportBussiness.RoomMarketReport";
        ods.SelectMethod = "getReport2";
        ods.SelectParameters.Clear();
        ods.SelectParameters.Add("HotelId", this.selDept.SelectedValue.ToString());
        ods.SelectParameters.Add("mYear", this.ddlYear.SelectedValue.ToString());
        ods.SelectParameters.Add("mMonth", this.ddlMonth.SelectedValue.ToString());
      }

      ObjectDataSource ods2 = this.ObjectDataSource2;
      {
        ods2.TypeName = " JMReports.WebApp.ReportBussiness.RoomMarketReport";
        ods2.SelectMethod = "getRoomMarketCompanyPrice";
        ods2.SelectParameters.Clear();
        ods2.SelectParameters.Add("HotelId", this.selDept.SelectedValue.ToString());
        ods2.SelectParameters.Add("mYear", this.ddlYear.SelectedValue.ToString());
        ods2.SelectParameters.Add("mMonth", this.ddlMonth.SelectedValue.ToString());
      }

      ObjectDataSource ods3 = this.ObjectDataSource3;
      {
        ods3.TypeName = " JMReports.WebApp.ReportBussiness.RoomMarketReport";
        ods3.SelectMethod = "getRoomMarketGroupDetail";
        ods3.SelectParameters.Clear();
        ods3.SelectParameters.Add("HotelId", this.selDept.SelectedValue.ToString());
        ods3.SelectParameters.Add("mYear", this.ddlYear.SelectedValue.ToString());
        ods3.SelectParameters.Add("mMonth", this.ddlMonth.SelectedValue.ToString());
      }



      //添加参数
      ReportViewer rv = this.ReportViewer1;
      {
        string title = string.Format("{0}{1} {2} 客房细分市场", this.ddlYear.SelectedItem.Text, this.ddlMonth.SelectedItem.Text, this.selDept.SelectedItem.Text);
        ReportParameter p1 = new ReportParameter("title", title);

        rv.LocalReport.DataSources.Clear();
        rv.LocalReport.DataSources.Add(new ReportDataSource("RoomMarketReport2", ods));
        if (this.selDept.SelectedValue == "1")
        {
          rv.LocalReport.DataSources.Add(new ReportDataSource("CompanyPriceDS", ods2));
          rv.LocalReport.ReportPath = MapPath(@"~/RoomMarket/RoomMarketReport2.rdlc");
        }
        else if (this.selDept.SelectedValue == "2")
        {
          //rv.LocalReport.DataSources.Add(new ReportDataSource("CompanyPriceDS", ods2));
          rv.LocalReport.ReportPath = MapPath(@"~/RoomMarket/RoomMarketReport2_KY.rdlc");
        }
        else
        {
          rv.LocalReport.DataSources.Add(new ReportDataSource("GroupDetailDS", ods3));
          rv.LocalReport.ReportPath = MapPath(@"~/RoomMarket/RoomMarketReport2_JW.rdlc");
        }


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