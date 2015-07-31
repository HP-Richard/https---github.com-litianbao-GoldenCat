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

namespace JMReports.WebApp.Import
{
    public partial class DataImportBudget : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnImport_Click(object sender, EventArgs e)
        {
            if (this.selDept.SelectedValue == "0")
            {
                this.lblMessage.Text = "请选择酒店";
                return;
            }

            if (this.FileExcel.Value == "")
            {
                this.lblMessage.Text = "请选择要上传的文件";
                return;
            }

            if (this.FileExcel.Value.IndexOf(this.selDept.SelectedItem.Text) < 0)
            {
                this.lblMessage.Text = "请选择正确的酒店";
                return;
            }


            this.lblMessage.Text = "正在进行数据导入，请稍后....";

            int yearcode = int.Parse(this.ddlYear.SelectedValue);
            
            int hotelid = int.Parse(this.selDept.SelectedValue);

            string filename = string.Empty;

            string sheetname = string.Empty;
            string tablenames = string.Empty;

            tablenames = this.getTableName();

            string[] SheetList = tablenames.Split(',');

            string mMessage = string.Empty;

            for (int i = 0; i <= SheetList.Length - 1; i++)
            {
                sheetname = SheetList[i].ToString();
                try
                {
                    filename = UpLoadXls(FileExcel);//上传XLS文件
                    string sql = String.Format("select * from {0}", Util_XLS.ConvertToSQLSheetName(sheetname));
                    System.Data.DataSet ds = Util_XLS.SelectFromXLS(filename, sql);
                    if (ds.Tables.Count == 0)
                    {
                        lblMessage.Text = "上传的.xls文件中不存在"+sheetname+"工作表，请打开确认";
                        DeleteFile(filename);
                        return;
                    }

                    DataTable dt = ds.Tables[0];

                    String[] cols;
                        cols= new string[]{ "itemid", "项目", "1月份", "2月份", "3月份", "4月份", "5月份", "6月份", "7月份", "8月份", "9月份", "10月份", "11月份", "12月份"};

                    //if (sheetname == "客房市场细分_预算")
                    //{
                    //    cols = new string[] { "ItemId", "项目", "英文代码", "细分市场", "KPI", "1月份", "2月份", "3月份", "4月份", "5月份", "6月份", "7月份", "8月份", "9月份", "10月份", "11月份", "12月份" };
                        
                    //}
                        if (dt.Columns.IndexOf("项目") < 0)
                            dt.Columns.Add(new DataColumn("项目"));

                    foreach (String col in cols)
                    {
                        if (!dt.Columns.Contains(col))
                        {
                            lblMessage.Text = String.Format("上传的.xls文件\"{1}\"表中不存在“{0}”列", col, sheetname);
                            DeleteFile(filename);
                            return;
                        }
                    }

                    Business.DataImportComponent dc = new DataImportComponent();

                    int importNumber = 0;
                    importNumber = dc.DataImportBudget(dt, sheetname, int.Parse(this.ddlYear.SelectedValue), int.Parse(this.selDept.SelectedValue));

                    DeleteFile(filename);

                    mMessage = mMessage + sheetname+ "表导入:" + importNumber.ToString() + "条数据 <br>";
                }
                catch (Exception ex)
                {
                    DeleteFile(filename);
                    lblMessage.Text = ex.Message;
                }
            }

            lblMessage.Text = mMessage;

            //Log Status in the DataImportStatus table
            Business.DataImportComponent dc2 = new DataImportComponent();
            dc2.LogDataImportStatus(1, yearcode, hotelid, mMessage);

        }

        /// <summary>
        /// 上传Excel文件
        /// </summary>
        /// <param name="inputfile">上传的控件名</param>
        /// <returns></returns>
        private string UpLoadXls(System.Web.UI.HtmlControls.HtmlInputFile inputfile)
        {
            string orifilename = string.Empty;
            string uploadfilepath = string.Empty;
            string modifyfilename = string.Empty;
            string fileExt = "";//文件扩展名
            int fileSize = 0;//文件大小
            try
            {
                if (inputfile.Value != string.Empty)
                {
                    //得到文件的大小
                    fileSize = inputfile.PostedFile.ContentLength;
                    if (fileSize == 0)
                    {
                        throw new Exception("导入的Excel文件大小为0，请检查是否正确！");
                    }
                    //得到扩展名
                    fileExt = inputfile.Value.Substring(inputfile.Value.LastIndexOf(".") + 1);
                    if (fileExt.ToLower() != "xls")
                    {
                        throw new Exception("你选择的文件格式不正确，只能导入EXCEL文件！");
                    }
                    //路径
                    uploadfilepath = Server.MapPath("~/");
                    //新文件名
                    modifyfilename = System.Guid.NewGuid().ToString();
                    modifyfilename += "." + inputfile.Value.Substring(inputfile.Value.LastIndexOf(".") + 1);
                    //判断是否有该目录
                    System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(uploadfilepath);
                    if (!dir.Exists)
                    {
                        dir.Create();
                    }
                    orifilename = uploadfilepath + modifyfilename;
                    //如果存在,删除文件
                    if (File.Exists(orifilename))
                    {
                        File.Delete(orifilename);
                    }
                    // 上传文件
                    inputfile.PostedFile.SaveAs(orifilename);
                }
                else
                {
                    throw new Exception("请选择要导入的Excel文件!");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return orifilename;
        }



        private void DeleteFile(string filename)
        {
            if (filename != string.Empty && File.Exists(filename))
            {
                File.Delete(filename);
            }
        }

        private string getTableName()
        {
            string mTableName = string.Empty;

            if (this.cbBudget.Checked)
            {
                mTableName = mTableName + "专用表_预算" + ",";
            }

            if (this.cbRoomEffBudget.Checked)
            {
                mTableName = mTableName + "客房部效率_预算" + ",";
            }

            if (this.cbRestaurantEffBudget.Checked)
            {
                mTableName = mTableName + "各餐厅_预算" + ",";
            }

            if (this.cbRestaurantRevBudget.Checked)
            {
                mTableName = mTableName + "各餐厅收入_预算" + ",";
            }

            if (this.cbOtherBusinessBudget.Checked)
            {
                mTableName = mTableName + "其他运营部门_预算" + ",";
            }

            if (this.cbRoomMarket.Checked)
            {
                mTableName = mTableName + "客房市场细分_预算" + ",";
            }

            if (this.cbRoomSales.Checked)
            {
                mTableName = mTableName + "客房销售渠道_预算" + ",";
            }

            if (this.cbBanquet.Checked)
            {
                mTableName = mTableName + "宴会市场细分_预算" + ",";
            }

            if (mTableName != "")
            {
                mTableName = mTableName.Substring(0, mTableName.Length - 1);
            }

            return mTableName;
        }

        protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void selDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (this.selDept.SelectedValue == "0")
            //{
            //    this.lbTemplate.Visible = false;
            //}

            //if (this.selDept.SelectedValue == "1")
            //{
            //    this.lbTemplate.Visible = true;
            //    this.lbTemplate.ResolveClientUrl("..\\Template\\酒店数据导入_预算_上海君悦.xls");
            //}

            //if (this.selDept.SelectedValue == "2")
            //{
            //    this.lbTemplate.Visible = true;
            //    this.lbTemplate.ResolveClientUrl("..\\Template\\酒店数据导入_预算_崇明凯悦.xls");
            //}

            //if (this.selDept.SelectedValue == "3")
            //{
            //    this.lbTemplate.Visible = true;
            //    this.lbTemplate.ResolveClientUrl("..\\Template\\酒店数据导入_预算_深圳万豪.xls");
            //}
        }
    }
}