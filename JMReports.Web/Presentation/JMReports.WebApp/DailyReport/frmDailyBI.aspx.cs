using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Microsoft.Reporting.WebForms;

namespace JMReports.WebApp.DailyReport
{
  public partial class frmDailyBI : System.Web.UI.Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      Page.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;

      if (!Page.IsPostBack)
      {
        if (this.txtDatetime.Text == string.Empty)
        {
          this.txtDatetime.Text = System.DateTime.Now.AddDays(-2).ToString("yyyy-MM-dd");
          SetDailyRevenueBI();
        }
      }
    }

    private void SetDailyRevenueBI()
    {
      ObjectDataSource ods = this.ObjectDataSource1;
      {
        ods.TypeName = "JMReports.WebApp.ReportBussiness.DailyReport";
        ods.SelectMethod = "GetDailyRevenueBIReport";
        ods.SelectParameters.Clear();
        ods.SelectParameters.Add("dt1", this.txtDatetime.Text);

      }

      ObjectDataSource ods2 = this.ObjectDataSource2;
      {

        ods2.TypeName = "JMReports.WebApp.ReportBussiness.DailyReport";
        ods2.SelectMethod = "GetDailyRoomRevenueBIReport";
        ods2.SelectParameters.Clear();
        ods2.SelectParameters.Add("dt1", this.txtDatetime.Text);
      }

      ObjectDataSource ods3 = this.ObjectDataSource3;
      {

        ods3.TypeName = "JMReports.WebApp.ReportBussiness.DailyReport";
        ods3.SelectMethod = "GetDailyRestaurantRevenueBIReport";
        ods3.SelectParameters.Clear();
        ods3.SelectParameters.Add("dt1", this.txtDatetime.Text);
      }

      ObjectDataSource ods4 = this.ObjectDataSource4;
      {

        ods4.TypeName = "JMReports.WebApp.ReportBussiness.DailyReport";
        ods4.SelectMethod = "GetDailyOccupancyRateBIReport";
        ods4.SelectParameters.Clear();
        ods4.SelectParameters.Add("dt1", this.txtDatetime.Text);
      }

      //添加参数
      ReportViewer rv = this.ReportViewer1;
      {
        ReportParameter p1 = new ReportParameter("title", " 经营日报表(" + this.txtDatetime.Text + ")");
        ReportParameter p2 = new ReportParameter("date1", this.txtDatetime.Text);
        ReportParameter p3 = new ReportParameter("HotelIds");

        if (HttpContext.Current.Session["JMPrincipal"] != null)
        {
          var principal = HttpContext.Current.Session["JMPrincipal"] as JMReports.Business.MyPrincipal;
          if (principal != null)
          {

            int userId = (principal.Identity as JMReports.Business.MyIdentity).Id;
            Business.UserHotelComponent rc = new Business.UserHotelComponent();
            var userHotelList = rc.getUserHotel(userId);
            var abc = from a in userHotelList
                      select a.HotelId.ToString();

            p3.Values.AddRange(abc.ToArray());
          }
        }

        rv.LocalReport.SetParameters(new ReportParameter[] { p1, p2, p3 });

        rv.LocalReport.EnableHyperlinks = true;
        rv.PageCountMode = PageCountMode.Actual;
        rv.ShowBackButton = false;
        rv.ShowToolBar = false;
        rv.ShowRefreshButton = false;
        rv.LocalReport.Refresh();
      }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
      SetDailyRevenueBI();
    }
  }
}