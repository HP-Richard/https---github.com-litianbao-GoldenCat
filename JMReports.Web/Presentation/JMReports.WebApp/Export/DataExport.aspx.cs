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

namespace JMReports.WebApp.Export
{
    public partial class DataExport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var lastMonth = DateTime.Today.AddMonths(-1);
                this.ddlYear.SelectedValue = lastMonth.Year.ToString();
                this.ddlMonth.SelectedValue = lastMonth.Month.ToString();
            }
        }

        protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnExport_Click(object sender, EventArgs e)
        {

            if (this.selDept.SelectedValue == "0")
            {
                this.lblMessage.Text = "请选择酒店";
                return;
            }

            initJavascript();

            string title = this.selDept.SelectedItem.Text;

            int yearcode = int.Parse(this.ddlYear.SelectedValue);
            int monthcode = int.Parse(this.ddlMonth.SelectedValue);
            int hotelid = int.Parse(this.selDept.SelectedValue);

            string sheetname = string.Empty;
            string tablenames = string.Empty;
            tablenames = this.getTableName();
            string[] SheetList = tablenames.Split(',');


            JMReports.Business.DataExportComponent dc = new DataExportComponent();

            try
            {
                int retVal = dc.DataExport(title, hotelid.ToString(), yearcode.ToString(), monthcode.ToString(), tablenames);

                if (retVal == 1)
                {
                    this.lblMessage.Text = "导出excel 已经完成，请在查看下载文件夹";

                }
            }
            catch (Exception ex)
            {
                this.lblMessage.Text = "导出excel 出现错误！请与管理员联系！ 出错信息： " + ex.Message;
            }

            endJavascript();
        }

        private string getTableName()
        {
            string mTableName = string.Empty;

            if (this.cbSpecial.Checked)
            {
                mTableName = mTableName + "专用表" + ",";
            }
            if (this.cbSpecialForcast.Checked)
            {
                mTableName = mTableName + "专用表_预测" + ",";
            }

            if (this.cbRestaurantEfficiency.Checked)
            {
                mTableName = mTableName + "餐饮部效率" + ",";
            }

            if (this.cbMarket.Checked)
            {
                mTableName = mTableName + "客房市场细分" + ",";
            }

            if (this.cbSales.Checked)
            {
                mTableName = mTableName + "客房销售渠道" + ",";
            }

            if (this.cbDinnerPart.Checked)
            {
                mTableName = mTableName + "宴会细分市场" + ",";
            }

            if (this.cbCompete.Checked)
            {
                mTableName = mTableName + "客房竞争组合" + ",";
            }

            if (this.cbForecast.Checked)
            {
                mTableName = mTableName + "预测" + ",";
            }

            if (this.cbOtherBusiness.Checked)
            {
                mTableName = mTableName + "其他运营部门" + ",";
            }

            if (mTableName != "")
            {
                mTableName = mTableName.Substring(0, mTableName.Length - 1);
            }

            return mTableName;
        }

        public static void initJavascript()
        {
            HttpContext.Current.Response.Write(" <script language=JavaScript type=text/javascript>");
            HttpContext.Current.Response.Write("var t_id = setInterval(animate,20);");
            HttpContext.Current.Response.Write("var pos=0;var dir=2;var len=0;");
            HttpContext.Current.Response.Write("function animate(){");
            HttpContext.Current.Response.Write("var elem = document.getElementById('progress');");
            HttpContext.Current.Response.Write("if(elem != null) {");
            HttpContext.Current.Response.Write("if (pos==0) len += dir;");
            HttpContext.Current.Response.Write("if (len>32 || pos>79) pos += dir;");
            HttpContext.Current.Response.Write("if (pos>79) len -= dir;");
            HttpContext.Current.Response.Write(" if (pos>79 && len==0) pos=0;");
            HttpContext.Current.Response.Write("elem.style.left = pos;");
            HttpContext.Current.Response.Write("elem.style.width = len;");
            HttpContext.Current.Response.Write("}}");
            HttpContext.Current.Response.Write("function remove_loading() {");
            HttpContext.Current.Response.Write(" this.clearInterval(t_id);");
            HttpContext.Current.Response.Write("var targelem = document.getElementById('loader_container');");
            HttpContext.Current.Response.Write("targelem.style.display='none';");
            HttpContext.Current.Response.Write("targelem.style.visibility='hidden';");
            HttpContext.Current.Response.Write("}");
            HttpContext.Current.Response.Write("</script>");
            HttpContext.Current.Response.Write("<style>");
            HttpContext.Current.Response.Write("#loader_container {text-align:center; position:absolute; top:40%; width:100%; left: 0;}");
            HttpContext.Current.Response.Write("#loader {font-family:Tahoma, Helvetica, sans; font-size:11.5px; color:#000000; background-color:#FFFFFF; padding:10px 0 16px 0; margin:0 auto; display:block; width:130px; border:1px solid #5a667b; text-align:left; z-index:2;}");
            HttpContext.Current.Response.Write("#progress {height:5px; font-size:1px; width:1px; position:relative; top:1px; left:0px; background-color:#8894a8;}");
            HttpContext.Current.Response.Write("#loader_bg {background-color:#e4e7eb; position:relative; top:8px; left:8px; height:7px; width:113px; font-size:1px;}");
            HttpContext.Current.Response.Write("</style>");
            HttpContext.Current.Response.Write("<div id=loader_container>");
            HttpContext.Current.Response.Write("<div id=loader>");
            HttpContext.Current.Response.Write("<div align=center>正在导出酒店数据 ...</div>");
            HttpContext.Current.Response.Write("<div id=loader_bg><div id=progress> </div></div>");
            HttpContext.Current.Response.Write("</div></div>");
            HttpContext.Current.Response.Flush();
        }

        public static void endJavascript()
        {
            HttpContext.Current.Response.Write(" <script language=JavaScript type=text/javascript>");
            HttpContext.Current.Response.Write("remove_loading()");
            HttpContext.Current.Response.Write("</script>");
            HttpContext.Current.Response.Flush();
        }

    }
}