﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JMReports.WebApp
{
    public partial class top : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var mainPage = "DailyReport/frmDailyBI.aspx";

            var principal = HttpContext.Current.Session["JMPrincipal"] as JMReports.Business.MyPrincipal;
            if (principal != null)
            {
                this.lblUser.Text = string.Format("欢迎{0}: {1}", principal.identity.User.Title, principal.Identity.Name);
            }
            this.lblMap1.Text = string.Format("<a href='{0}' target='right'>首页</a>", mainPage);
            
            if ((Page.Request.QueryString["v1"] != null) && (Page.Request.QueryString["v1"] != ""))
            {
                String v1 = Request.QueryString["v1"].ToString();
                int reportId;
                if (int.TryParse(v1, out reportId))
                {
                    Business.ReportInfoComponent rc = new Business.ReportInfoComponent();
                    var report = rc.getSingleReportByReportId(reportId);
                    this.lblMap1.Text = string.Format("<a href='{3}' target='right'>首页</a>-><a href='#' target='right'>{0}</a>-><a href='{1}' target='right'>{2}</a>", report.Category, report.URL, report.ChineseName, mainPage);
                }
            }


        }
    }
}