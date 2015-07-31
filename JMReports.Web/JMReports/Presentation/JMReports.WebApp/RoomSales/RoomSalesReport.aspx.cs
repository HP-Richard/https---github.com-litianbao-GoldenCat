﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;

namespace JMReports.WebApp.RoomSales
{
    public partial class RoomSalesReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ObjectDataSource ods = this.ObjectDataSource1;
            {
                ods.TypeName = " JMReports.WebApp.ReportBussiness.RoomSalesReport";
                ods.SelectMethod = "getReport";
                ods.SelectParameters.Clear();
                ods.SelectParameters.Add("HotelId", this.selDept.SelectedValue.ToString());
                ods.SelectParameters.Add("mYear", this.ddlYear.SelectedValue.ToString());
                ods.SelectParameters.Add("mMonth", this.ddlMonth.SelectedValue.ToString());
            }

            //添加参数
            ReportViewer rv = this.ReportViewer1;
            {
                
                string title = string.Format("{0}{1} {2} 客房销售渠道", this.ddlYear.SelectedItem.Text, this.ddlMonth.SelectedItem.Text, this.selDept.SelectedItem.Text);
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