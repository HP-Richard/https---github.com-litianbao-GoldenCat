﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;

namespace JMReports.WebApp.RoomMarket
{
    public partial class RoomMarketReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ObjectDataSource ods = this.ObjectDataSource1;
            {
                ods.TypeName = " JMReports.WebApp.ReportBussiness.RoomMarketReport";
                ods.SelectMethod = "getReport";
                ods.SelectParameters.Clear();
                ods.SelectParameters.Add("HotelId", this.selDept.SelectedValue.ToString());
                ods.SelectParameters.Add("mYear", this.ddlYear.SelectedValue.ToString());
                ods.SelectParameters.Add("mMonth", this.ddlMonth.SelectedValue.ToString());
            }

            //添加参数
            ReportViewer rv = this.ReportViewer1;
            {
                ReportParameter p1 = new ReportParameter("title", "客房市场细分");
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