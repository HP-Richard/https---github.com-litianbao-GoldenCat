using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;

namespace JMReports.WebApp.RoomCompete
{
    public partial class RoomCompete2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {

            ObjectDataSource ods = this.ObjectDataSource1;
            {
                ods.TypeName = " JMReports.WebApp.ReportBussiness.RoomCompeteReport";
                ods.SelectMethod = "getCombinationDS";
                ods.SelectParameters.Clear();
                ods.SelectParameters.Add("HotelId", this.selDept.SelectedValue.ToString());
                ods.SelectParameters.Add("mYear", this.ddlYear.SelectedValue.ToString());
                ods.SelectParameters.Add("mMonth", this.ddlMonth.SelectedValue.ToString());
            }

            //ObjectDataSource ods = this.ObjectDataSource1;
            //{
            //    ods.TypeName = " JMReports.WebApp.ReportBussiness.RoomCompeteReport";
            //    ods.SelectMethod = "getRoomCompeteCurrentMonthDS";
            //    ods.SelectParameters.Clear();
            //    ods.SelectParameters.Add("HotelId", this.selDept.SelectedValue.ToString());
            //    ods.SelectParameters.Add("mYear", this.ddlYear.SelectedValue.ToString());
            //    ods.SelectParameters.Add("mMonth", this.ddlMonth.SelectedValue.ToString());
            //}

            //ObjectDataSource ods2 = this.ObjectDataSource2;
            //{
            //    ods2.TypeName = " JMReports.WebApp.ReportBussiness.RoomCompeteReport";
            //    ods2.SelectMethod = "getReport2";
            //    ods2.SelectParameters.Clear();
            //    ods2.SelectParameters.Add("HotelId", this.selDept.SelectedValue.ToString());
            //    ods2.SelectParameters.Add("mYear", this.ddlYear.SelectedValue.ToString());
            //    ods2.SelectParameters.Add("mMonth", this.ddlMonth.SelectedValue.ToString());
            //}

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