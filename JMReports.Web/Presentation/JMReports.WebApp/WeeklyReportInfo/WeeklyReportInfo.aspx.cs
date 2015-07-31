using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JMReports.LinqToSql;
using JMReports.WebApp.Utility;
using Microsoft.Reporting.WebForms;

namespace JMReports.WebApp.WeeklyReportInfo
{
    public partial class WeeklyReportInfo : CPageBase
    {
        private int? Id { get { return (int?)ViewState["Id"]; } set { ViewState["Id"] = value; } }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session["JMPrincipal"] == null)
                Page.Response.Redirect("~/index.aspx");

            if (!IsPostBack)
            {
                if (this.txtDatetime.Text == string.Empty)
                {
                    this.txtDatetime.Text = System.DateTime.Now.AddDays(-2).ToString("yyyy-MM-dd");

                    #region 页面启动时加载报表
                    string myScript = @"$(document).ready(function () {
                            var checkValue=$(""#ContentPlaceHolder1_selDept_selDept"").val();  //获取Select选择的Value
                            if (checkValue != ''){$("".btn-primary"").click();}});";
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "MyScript", myScript, true);
                    #endregion
                }


                try { if (Request.QueryString["ID"] == null) { Id = null; } else Id = Convert.ToInt32(Request.QueryString["ID"]); }
                catch { Id = null; }

                if (Id != null)
                {
                    using (var context = Helper.GetDataContext())
                    {
                        var wri = context.WeeklyReportInfo.FirstOrDefault(w => w.Id == Id);
                        if (wri != null)
                        {
                            this.txtContext1.Value = wri.Context1;
                            this.txtContext2.Value = wri.Context2;
                            this.txtContext3.Value = wri.Context3;

                            if (wri.Status.Trim().Contains("已提交"))
                            {
                                this.hdIsSubmited.Value = "true";
                                this.btnSave.Enabled = false;
                                this.btnSubmit.Enabled = false;
                            }
                            else
                            {
                                this.hdIsSubmited.Value = "false";
                            }
                        }
                    }
                }
                else
                {
                    this.btnDelete.Enabled = false;
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            this.lblMessage.Text = "";

            var ct1 = this.txtContext1.Value.Trim();
            var ct2 = this.txtContext2.Value.Trim();
            var ct3 = this.txtContext3.Value.Trim();
            if (ct1.Length > 0 || ct2.Length > 0 || ct3.Length > 0)
            {
                using (var context = Helper.GetDataContext())
                {
                    if (Id == null)   //新增周报
                    {
                        LinqToSql.WeeklyReportInfo wri = new LinqToSql.WeeklyReportInfo();
                        wri.Context1 = ct1;
                        wri.Context2 = ct2;
                        wri.Context3 = ct3;
                        wri.CreateUser = Helper.GetUserId();
                        wri.CreateTime = DateTime.Now;
                        wri.Status = "暂存";
                        wri.ApproveUsers = "";
                        try
                        {
                            context.WeeklyReportInfo.InsertOnSubmit(wri);
                            context.SubmitChanges();
                            lblMessage.Text = "添加周报成功！";
                            Response.Redirect("WeeklyReportList.aspx");
                        }
                        catch { lblMessage.Text = "无法添加周报，请稍后再试！"; }
                    }
                    else  //修改周报
                    {
                        var wri = context.WeeklyReportInfo.FirstOrDefault(w => w.Id == Id);
                        if (wri != null)
                        {
                            wri.Context1 = this.txtContext1.Value;
                            wri.Context2 = this.txtContext2.Value;
                            wri.Context3 = this.txtContext3.Value;
                            try
                            {
                                context.SubmitChanges();
                                lblMessage.Text = "保存周报成功！";
                                Response.Redirect("WeeklyReportList.aspx");
                            }
                            catch { lblMessage.Text = "无法保存周报，请稍后再试！"; }
                        }
                    }
                }
            }
            else
            {
                lblMessage.Text = "请输入周报内容！";
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            this.lblMessage.Text = "";

            var ct1 = this.txtContext1.Value.Trim();
            var ct2 = this.txtContext2.Value.Trim();
            var ct3 = this.txtContext3.Value.Trim();
            if (ct1.Length > 0 || ct2.Length > 0 || ct3.Length > 0)
            {
                using (var context = Helper.GetDataContext())
                {
                    if (Id == null)   //新增周报
                    {
                        LinqToSql.WeeklyReportInfo wri = new LinqToSql.WeeklyReportInfo();
                        wri.Context1 = ct1;
                        wri.Context2 = ct2;
                        wri.Context3 = ct3;
                        wri.CreateUser = Helper.GetUserId();
                        wri.CreateTime = DateTime.Now;
                        wri.Status = "已提交";
                        wri.ApproveUsers = "";
                        try
                        {
                            context.WeeklyReportInfo.InsertOnSubmit(wri);
                            context.SubmitChanges();
                            lblMessage.Text = "提交周报成功！";
                            Response.Redirect("WeeklyReportList.aspx");
                        }
                        catch  { lblMessage.Text = "无法提交周报，请稍后再试！"; }
                    }
                    else  //修改周报
                    {
                        var wri = context.WeeklyReportInfo.FirstOrDefault(w => w.Id == Id);
                        if (wri != null)
                        {
                            wri.Context1 = this.txtContext1.Value;
                            wri.Context2 = this.txtContext2.Value;
                            wri.Context3 = this.txtContext3.Value;
                            wri.Status = "已提交";
                            wri.ApproveUsers = "";
                            try
                            {
                                context.SubmitChanges();
                                lblMessage.Text = "提交周报成功！";
                                Response.Redirect("WeeklyReportList.aspx");
                            }
                            catch { lblMessage.Text = "无法提交周报，请稍后再试！"; }
                        }
                    }
                }
            }
            else
            {
                lblMessage.Text = "请输入周报内容！";
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            using (var context = Helper.GetDataContext())
            {
                if (Id != null)
                {
                    var wri = context.WeeklyReportInfo.FirstOrDefault(w => w.Id == Id);
                    if (wri != null)
                    {
                        try
                        {
                            context.WeeklyReportInfo.DeleteOnSubmit(wri);
                            context.SubmitChanges();
                            lblMessage.Text = "删除周报成功！";
                            Response.Redirect("WeeklyReportList.aspx");
                        }
                        catch { lblMessage.Text = "无法删除周报，请稍后再试！"; }
                    }
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            var session = HttpContext.Current.Session["CurrentCondition"];
            if (session == null)
            {
                JMReports.Business.SearchCondition currentCondition = new Business.SearchCondition();
                currentCondition.Hotel = this.selDept.SelectedValue;
                if (currentCondition.YearCode == null)
                {
                    var lastMonth = DateTime.Today.AddMonths(-1);
                    currentCondition.YearCode = lastMonth.Year.ToString();
                    currentCondition.MonthCode = lastMonth.Month.ToString();
                }
                HttpContext.Current.Session["CurrentCondition"] = currentCondition;
            }
            else
            {
                var condition = session as Business.SearchCondition;
                condition.Hotel = this.selDept.SelectedValue;
                session = condition;
            }

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