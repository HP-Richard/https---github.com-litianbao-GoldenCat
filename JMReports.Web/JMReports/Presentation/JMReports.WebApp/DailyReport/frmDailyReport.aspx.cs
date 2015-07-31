﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Microsoft.Reporting.WebForms;

namespace JMReports.WebApp.DailyReport
{
    public partial class frmDailyReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (this.txtDatetime.Text == string.Empty)
                {
                    this.txtDatetime.Text = System.DateTime.Now.AddDays(-2).ToString("yyyy-MM-dd");

                    string myScript = @"$(document).ready(function () {
                            var checkValue=$(""#ContentPlaceHolder1_selDept_selDept"").val();  //获取Select选择的Value
                            
                               if (checkValue != '')
                                {
                                    $("".btn-primary"").click();
                                }
                         });
                         ";
Page.ClientScript.RegisterClientScriptBlock(this.GetType(),"MyScript", myScript, true);
                    //Page.RegisterClientScriptBlock

                    //this.selDept.SelectedItem.Value = "1";
                    //this.selDept.SelectedItem.Text = "上海君悦";

                    //showReport();

                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            showReport();
        }

        private void showReport()
        {
            ObjectDataSource ods = this.ObjectDataSource1;
            {

                ods.TypeName = "JMReports.WebApp.ReportBussiness.DailyReport";
                ods.SelectMethod = "getDailyReport";
                ods.SelectParameters.Clear();
                ods.SelectParameters.Add("dt1", this.txtDatetime.Text);
                ods.SelectParameters.Add("hotel", this.selDept.SelectedValue.ToString());
            }

            //添加参数
            ReportViewer rv = this.ReportViewer1;
            {
                string title = string.Format("{0} {1} 经营日报表", this.txtDatetime.Text, this.selDept.SelectedItem.Text);
                ReportParameter p1 = new ReportParameter("title", title);

                ReportParameter p2 = new ReportParameter("date1", this.txtDatetime.Text);
                rv.LocalReport.SetParameters(new ReportParameter[] { p1, p2 });


                rv.LocalReport.EnableHyperlinks = true;
                rv.PageCountMode = PageCountMode.Actual;
                rv.ShowBackButton = false;
                rv.ShowRefreshButton = false;
                rv.LocalReport.Refresh();
            }
        }
    }
}