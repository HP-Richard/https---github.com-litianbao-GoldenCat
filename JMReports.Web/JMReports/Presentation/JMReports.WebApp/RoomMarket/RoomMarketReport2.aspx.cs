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

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
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
                string title = string.Format("{0}{1} {2} 客房市场细分", this.ddlYear.SelectedItem.Text, this.ddlMonth.SelectedItem.Text, this.selDept.SelectedItem.Text);
                ReportParameter p1 = new ReportParameter("title", title);

                rv.LocalReport.DataSources.Clear();
                rv.LocalReport.DataSources.Add(new ReportDataSource("RoomMarketReport2",ods));
                if (this.selDept.SelectedValue != "3")
                {
                    rv.LocalReport.DataSources.Add(new ReportDataSource("CompanyPriceDS", ods2));
                    rv.LocalReport.ReportPath = MapPath(@"~/RoomMarket/RoomMarketReport2.rdlc");

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