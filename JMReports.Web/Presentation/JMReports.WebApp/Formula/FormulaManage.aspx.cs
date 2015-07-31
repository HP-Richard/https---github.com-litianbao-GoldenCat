using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.IO;

using JMReports.Business;
using JMReports.WebApp.Utility;
using JMReports.LinqToSql;

namespace JMReports.WebApp.Formula
{
    public partial class FormulaManage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                setAccountItem();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Search();
        }

        protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void FormulaGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            // 得到该控件
            GridView theGrid = sender as GridView;
            int newPageIndex = 0;
            if (e.NewPageIndex == -3)
            {
                //点击了Go按钮
                TextBox txtNewPageIndex = null;

                //GridView较DataGrid提供了更多的API，获取分页块可以使用BottomPagerRow 或者TopPagerRow，当然还增加了HeaderRow和FooterRow
                GridViewRow pagerRow = theGrid.BottomPagerRow;

                if (pagerRow != null)
                {
                    //得到text控件
                    txtNewPageIndex = pagerRow.FindControl("txtNewPageIndex") as TextBox;
                }
                if (txtNewPageIndex != null)
                {
                    //得到索引
                    newPageIndex = int.Parse(txtNewPageIndex.Text) - 1;
                }
            }
            else
            {
                //点击了其他的按钮
                newPageIndex = e.NewPageIndex;
            }
            //防止新索引溢出
            newPageIndex = newPageIndex < 0 ? 0 : newPageIndex;
            newPageIndex = newPageIndex >= theGrid.PageCount ? theGrid.PageCount - 1 : newPageIndex;

            //得到新的值
            theGrid.PageIndex = newPageIndex;

            //重新绑定
            Search();

        }

        private void Search()
        {
            string mYear = string.Empty;
            string mHotelId = string.Empty;
            string mAccountItemId = string.Empty;

            mYear = this.ddlYear.SelectedValue;
            mHotelId = this.selDept.SelectedValue;
            mAccountItemId = this.ddlAccountItem.SelectedValue;


            if (mHotelId == "1" || mHotelId == "2")
            {
                this.FormulaGV.Visible = true;
                this.FormulaGV_2.Visible = false;

                JMReports.Business.FormulaManageComponent fc = new FormulaManageComponent();

                this.FormulaGV.DataSource = fc.getFormula(mYear, mHotelId, mAccountItemId).Tables[0];
                this.FormulaGV.DataBind();

            }
            else
            {
                this.FormulaGV_2.Visible = true;
                this.FormulaGV.Visible = false;

                JMReports.Business.FormulaManageComponent fc = new FormulaManageComponent();

                this.FormulaGV_2.DataSource = fc.getFormula(mYear, mHotelId, mAccountItemId).Tables[0];
                this.FormulaGV_2.DataBind();
            }
            


        }

        private void setAccountItem()
        {
            Business.ReportItemComponent ric = new ReportItemComponent();
            this.ddlAccountItem.DataSource = ric.getReportItems();
            this.ddlAccountItem.DataTextField = "ItemName";
            this.ddlAccountItem.DataValueField = "ID";
            this.ddlAccountItem.DataBind();
        }

        private void setReport()
        {

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {

            string mYearCode = string.Empty;
            string mHotelId = string.Empty;
            string mAccountItemId = string.Empty;
            string mHotelName = string.Empty;

            string mItemId = string.Empty;
            string mAccountType = string.Empty;
            string mDepartment = string.Empty;

            mYearCode = this.ddlYear.SelectedValue;
            mHotelId = this.selDept.SelectedValue;
            mHotelName = this.selDept.SelectedItem.Text;
            mAccountItemId = this.ddlAccountItem.SelectedValue;


            if (this.ddlAccountItem.SelectedValue != "")
            {
                string[] items = this.ddlAccountItem.SelectedItem.Text.Split('|');

                mItemId = items[0];
                mAccountType = items[1];
                mDepartment = items[2];
            }




            if (mHotelId == "1" || mHotelId == "2")
            {

                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                string[] fields = new string[] { "ItemId", "HotelName", "AccountType", "Department", "Account", "CostCenter", "YearCode", "HotelId" };
                foreach (string f1 in fields) dt.Columns.Add(new DataColumn(f1));


                foreach (GridViewRow dli in this.FormulaGV.Rows)
                {
                    System.Web.UI.WebControls.TextBox txtAccount =
                        (System.Web.UI.WebControls.TextBox)dli.FindControl("txtAccount");

                    System.Web.UI.WebControls.TextBox txtCostCenter =
                        (System.Web.UI.WebControls.TextBox)dli.FindControl("txtCostCenter");

                    DataRow dr1 = dt.NewRow();

                    dr1["ItemId"] = dli.Cells[1].Text;

                    dr1["HotelName"] = mHotelName;
                    dr1["YearCode"] = mYearCode;

                    dr1["AccountType"] = mAccountType;
                    dr1["Department"] = mDepartment;
                    dr1["Account"] = txtAccount.Text;
                    dr1["CostCenter"] = txtCostCenter.Text;
                    dr1["HotelId"] = mHotelId;

                    dt.Rows.Add(dr1);

                }

                DataRow dr_new = dt.NewRow();

                dr_new["ItemId"] = mItemId;
                dr_new["HotelName"] = mHotelName;
                dr_new["YearCode"] = mYearCode;

                dr_new["AccountType"] = mAccountType;
                dr_new["Department"] = mDepartment;
                dr_new["Account"] = "";
                dr_new["CostCenter"] = "";
                dr_new["HotelId"] = mHotelId;

                dt.Rows.Add(dr_new);

                this.FormulaGV.DataSource = dt;
                this.FormulaGV.DataBind();


            }

            if (mHotelId == "3")
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                string[] fields = new string[] { "ItemId", "HotelName", "AccountType", "Department", "Account_From","Account_To", "CostCenter_From","CostCenter_To", "YearCode", "HotelId" };
                foreach (string f1 in fields) dt.Columns.Add(new DataColumn(f1));


                foreach (GridViewRow dli in this.FormulaGV_2.Rows)
                {
                    System.Web.UI.WebControls.TextBox txtAccount_From =
                        (System.Web.UI.WebControls.TextBox)dli.FindControl("txtAccount_From");

                    System.Web.UI.WebControls.TextBox txtAccount_To =
                        (System.Web.UI.WebControls.TextBox)dli.FindControl("txtAccount_To");

                    System.Web.UI.WebControls.TextBox txtCostCenter_From =
                        (System.Web.UI.WebControls.TextBox)dli.FindControl("txtCostCenter_From");

                    System.Web.UI.WebControls.TextBox txtCostCenter_To =
                        (System.Web.UI.WebControls.TextBox)dli.FindControl("txtCostCenter_To");

                    DataRow dr1 = dt.NewRow();

                    dr1["ItemId"] = dli.Cells[1].Text;

                    dr1["HotelName"] = mHotelName;
                    dr1["YearCode"] = mYearCode;

                    dr1["AccountType"] = mAccountType;
                    dr1["Department"] = mDepartment;
                    dr1["Account_From"] = txtAccount_From.Text;
                    dr1["Account_To"] = txtAccount_To.Text;
                    dr1["CostCenter_From"] = txtCostCenter_From.Text;
                    dr1["CostCenter_To"] = txtCostCenter_To.Text;
                    dr1["HotelId"] = mHotelId;

                    dt.Rows.Add(dr1);

                }

                DataRow dr_new = dt.NewRow();

                dr_new["ItemId"] = mItemId;
                dr_new["HotelName"] = mHotelName;
                dr_new["YearCode"] = mYearCode;

                dr_new["AccountType"] = mAccountType;
                dr_new["Department"] = mDepartment;
                dr_new["Account_From"] = "";
                dr_new["Account_To"] = "";
                dr_new["CostCenter_From"] = "";
                dr_new["CostCenter_To"] = "";
                dr_new["HotelId"] = mHotelId;

                dt.Rows.Add(dr_new);

                this.FormulaGV_2.DataSource = dt;
                this.FormulaGV_2.DataBind();
            }
        }
    }
}