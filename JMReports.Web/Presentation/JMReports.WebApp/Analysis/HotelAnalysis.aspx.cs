using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using JMReports.Business;
using JMReports.WebApp.Utility;
using JMReports.LinqToSql;

using Microsoft.Reporting.WebForms;

namespace JMReports.WebApp.Analysis
{
    public partial class HotelAnalysis : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                setAccountItem();
            }
        }

        private void setAccountItem()
        {
            Business.ReportItemComponent ric = new ReportItemComponent();
            this.ddlAccountItem.DataSource = ric.getAnalysisItems();
            this.ddlAccountItem.DataTextField = "ItemName";
            this.ddlAccountItem.DataValueField = "ID";
            this.ddlAccountItem.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            //set session value 
            JMReports.Business.SearchCondition currentCondition = new Business.SearchCondition();
            currentCondition.Hotel = this.selDept.SelectedValue;
            currentCondition.YearCode = this.ddlYear.SelectedValue;
            currentCondition.ItemId = this.ddlAccountItem.SelectedValue;
            HttpContext.Current.Session["CurrentCondition"] = currentCondition;


            ObjectDataSource ods = this.ObjectDataSource1;
            {
                ods.TypeName = " JMReports.WebApp.ReportBussiness.HotelAnalysisReport";
                ods.SelectMethod = "getAnalysisReport";
                ods.SelectParameters.Clear();
                ods.SelectParameters.Add("mHotelId", this.selDept.SelectedValue.ToString());
                ods.SelectParameters.Add("mYear", this.ddlYear.SelectedValue.ToString());
                ods.SelectParameters.Add("mItemId", this.ddlAccountItem.SelectedValue.ToString());
            }

            ObjectDataSource ods2 = this.ObjectDataSource2;
            {
                ods2.TypeName = " JMReports.WebApp.ReportBussiness.HotelAnalysisReport";
                ods2.SelectMethod = "getAnalysis2Report";
                ods2.SelectParameters.Clear();
                ods2.SelectParameters.Add("mHotelId", this.selDept.SelectedValue.ToString());
                ods2.SelectParameters.Add("mYear", this.ddlYear.SelectedValue.ToString());
                ods2.SelectParameters.Add("mItemId", this.ddlAccountItem.SelectedValue.ToString());
            }

            //添加参数
            ReportViewer rv = this.ReportViewer1;
            {
                string title = string.Format("{0}{1} {2} 酒店报表", this.ddlYear.SelectedItem.Text, this.ddlAccountItem.SelectedItem.Text, this.selDept.SelectedItem.Text);
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