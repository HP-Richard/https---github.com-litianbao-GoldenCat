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
    public partial class DataImport : System.Web.UI.Page
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
            int monthcode = int.Parse(this.ddlMonth.SelectedValue);
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
                        lblMessage.Text = "上传的.xls文件中不存在Sheet1工作表，请打开确认";
                        DeleteFile(filename);
                        return;
                    }

                    DataTable dt = ds.Tables[0];
                    
                    if (sheetname == "专用表")
                    {
                        String[] cols = { "itemid", "项目", "本月实际" };

                        foreach (String col in cols)
                        {
                            if (!dt.Columns.Contains(col))
                            {
                                lblMessage.Text = String.Format("上传的.xls文件Sheet1表中不存在“{0}”列", col);
                                DeleteFile(filename);
                                return;
                            }
                        }

                        Business.DataImportComponent dc = new DataImportComponent();

                        int importNumber = 0;
                        importNumber = dc.DataImport(dt, "专用表", int.Parse(this.ddlYear.SelectedValue), int.Parse(this.ddlMonth.SelectedValue), int.Parse(this.selDept.SelectedValue));

                        DeleteFile(filename);

                        mMessage = mMessage + "专用表导入:" + importNumber.ToString() + "条数据 <br>";
                        //lblMessage.Text = "数据导入" + importNumber.ToString() + "条数据";

                    }

                    if (sheetname == "专用表_预测")
                    {
                        String[] cols = { "ItemId", "第一个月", "第二个月", "第三个月", "第四个月", "第五个月", "第六个月" };

                        foreach (String col in cols)
                        {
                            if (!dt.Columns.Contains(col))
                            {
                                lblMessage.Text = String.Format("上传的.xls文件Sheet1表中不存在“{0}”列", col);
                                DeleteFile(filename);
                                return;
                            }
                        }

                        Business.DataImportComponent dc = new DataImportComponent();

                        int importNumber = 0;
                        importNumber = dc.DataImport(dt, "专用表_预测", int.Parse(this.ddlYear.SelectedValue), int.Parse(this.ddlMonth.SelectedValue), int.Parse(this.selDept.SelectedValue));

                        DeleteFile(filename);

                        mMessage = mMessage + "专用表_预测导入:" + importNumber.ToString() + "条数据 <br>";

                    }

                    if (sheetname == "餐饮部效率")
                    {
                        String[] cols = { "Itemid", "项目", "本月实际" };

                        foreach (String col in cols)
                        {
                            if (!dt.Columns.Contains(col))
                            {
                                lblMessage.Text = String.Format("上传的.xls文件Sheet1表中不存在“{0}”列", col);
                                DeleteFile(filename);
                                return;
                            }
                        }

                        Business.DataImportComponent dc = new DataImportComponent();

                        int importNumber = 0;
                        importNumber = dc.DataImport(dt, "餐饮部效率", int.Parse(this.ddlYear.SelectedValue), int.Parse(this.ddlMonth.SelectedValue), int.Parse(this.selDept.SelectedValue));

                        DeleteFile(filename);

                        mMessage = mMessage + "餐饮部效率表导入:" + importNumber.ToString() + "条数据 <br>";
                    }

                    if (sheetname == "客房市场细分")
                    {
                        String[] cols = { "ItemId", "英文代码", "细分市场", "KPI", "本月实际", "本月预算", "完成比例", "上年同期", "同比增减", "本年实际", "累计预算", "完成比例", "上年同期", "同比增减" };

                        foreach (String col in cols)
                        {
                            if (!dt.Columns.Contains(col))
                            {
                                lblMessage.Text = String.Format("上传的.xls文件Sheet1表中不存在“{0}”列", col);
                                DeleteFile(filename);
                                return;
                            }
                        }

                        Business.DataImportComponent dc = new DataImportComponent();

                        int importNumber = 0;
                        importNumber = dc.DataImport(dt, "客房市场细分", int.Parse(this.ddlYear.SelectedValue), int.Parse(this.ddlMonth.SelectedValue), int.Parse(this.selDept.SelectedValue));

                        DeleteFile(filename);

                        //lblMessage.Text = "数据导入" + importNumber.ToString() + "条数据";

                        

                        mMessage = mMessage + "客房市场细分导入:" + importNumber.ToString() + "条数据 <br/>";
                        mMessage += WrapErrorMessage(dc, sheetname);
                        //if (dc.Errors != null)
                        //{
                        //    string errorMsg = "---------------<br/>客房市场细分导入失败<br/>";
                        //    foreach (var item in dc.Errors)
                        //    {
                        //        errorMsg += string.Format("ItemId: {0}, {1}, Budget: {2}{3}<br/>", item.ItemId, item.Description, item.LeftBudget, item.ErrorType == Entities.ErrorType.MissingBugdet ? "" : string.Format(" ({0})", item.RightBudget));
                        //    }

                        //    mMessage += errorMsg;
                        //}
                    }

                    if (sheetname == "客房销售渠道")
                    {

                        String[] cols = { "itemid", "ItemName", "KPI", "本月实际", "本月预算", "完成比例", "上年同期", "同比增减", "本年实际", "累计预算", "完成比例", "上年同期", "同比增减" };

                        foreach (String col in cols)
                        {
                            if (!dt.Columns.Contains(col))
                            {
                                lblMessage.Text = String.Format("上传的.xls文件Sheet1表中不存在“{0}”列", col);
                                DeleteFile(filename);
                                return;
                            }
                        }

                        Business.DataImportComponent dc = new DataImportComponent();

                        int importNumber = 0;
                        importNumber = dc.DataImport(dt, "客房销售渠道", int.Parse(this.ddlYear.SelectedValue), int.Parse(this.ddlMonth.SelectedValue), int.Parse(this.selDept.SelectedValue));

                        DeleteFile(filename);
                        
                        //lblMessage.Text = "数据导入" + importNumber.ToString() + "条数据";

                        mMessage = mMessage + "客房销售渠道导入:" + importNumber.ToString() + "条数据 <br>";
                        mMessage += WrapErrorMessage(dc, sheetname);
                    }

                    //宴会细分市场

                    if (sheetname == "宴会细分市场")
                    {

                        String[] cols = { "itemid", "收入", "本月实际", "本月预算", "完成比例", "上年同期", "同比增减", "本年实际", "累计预算", "完成比例", "上年同期", "同比增减" };

                        foreach (String col in cols)
                        {
                            if (!dt.Columns.Contains(col))
                            {
                                lblMessage.Text = String.Format("上传的.xls文件Sheet1表中不存在“{0}”列", col);
                                DeleteFile(filename);
                                return;
                            }
                        }

                        Business.DataImportComponent dc = new DataImportComponent();

                        int importNumber = 0;
                        importNumber = dc.DataImport(dt, "宴会细分市场", int.Parse(this.ddlYear.SelectedValue), int.Parse(this.ddlMonth.SelectedValue), int.Parse(this.selDept.SelectedValue));

                        DeleteFile(filename);

                        //lblMessage.Text = "数据导入" + importNumber.ToString() + "条数据";

                        mMessage = mMessage + "宴会细分市场导入:" + importNumber.ToString() + "条数据 <br>";
                        mMessage += WrapErrorMessage(dc, sheetname);
                    }

                    if (sheetname == "预测")
                    {
                        Business.DataImportComponent dc = new DataImportComponent();

                        int importNumber = 0;
                        importNumber = dc.DataImport(dt, "预测", int.Parse(this.ddlYear.SelectedValue), int.Parse(this.ddlMonth.SelectedValue), int.Parse(this.selDept.SelectedValue));

                        DeleteFile(filename);

                        //lblMessage.Text = "数据导入" + importNumber.ToString() + "条数据";

                        mMessage = mMessage + "预测报表: 导入" + importNumber.ToString() + "条数据 <br>";
                    }


                    if (sheetname == "其他运营部门")
                    {

                        String[] cols = { "itemid", "项目", "本月实际" };

                        foreach (String col in cols)
                        {
                            if (!dt.Columns.Contains(col))
                            {
                                lblMessage.Text = String.Format("上传的.xls文件Sheet1表中不存在“{0}”列", col);
                                DeleteFile(filename);
                                return;
                            }
                        }

                        Business.DataImportComponent dc = new DataImportComponent();

                        int importNumber = 0;
                        importNumber = dc.DataImport(dt, "其他运营部门", int.Parse(this.ddlYear.SelectedValue), int.Parse(this.ddlMonth.SelectedValue), int.Parse(this.selDept.SelectedValue));

                        DeleteFile(filename);

                        //lblMessage.Text = "数据导入" + importNumber.ToString() + "条数据";

                        mMessage = mMessage + "其他运营部门: 导入" + importNumber.ToString() + "条数据 <br>";
                    }

                    if (sheetname == "客房竞争组合")
                    {
                        Business.DataImportComponent dc = new DataImportComponent();

                        int importNumber = 0;
                        importNumber = dc.DataImport(dt, "客房竞争组合", int.Parse(this.ddlYear.SelectedValue), int.Parse(this.ddlMonth.SelectedValue), int.Parse(this.selDept.SelectedValue));

                        DeleteFile(filename);

                        //lblMessage.Text = "数据导入" + importNumber.ToString() + "条数据";

                        mMessage = mMessage + "客房竞争组合导入:" + importNumber.ToString() + "条数据 <br>";
                    }

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
            dc2.LogDataImportStatus(0, yearcode, hotelid, mMessage);



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
                    uploadfilepath = Server.MapPath("~/Import/XLS/");
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

        private string WrapErrorMessage(DataImportComponent dc, string tableName)
        {
            string errorMsg = "";
            if (dc.Errors != null && dc.Errors.Count > 0)
            {
                errorMsg = "---------------<br/>"+tableName+"导入失败<br/>";
                foreach (var item in dc.Errors)
                {
                    errorMsg += string.Format("ItemId: {0}, {1}, Budget: {2}{3}<br/>", item.ItemId, item.Description, item.LeftBudget, item.ErrorType == Entities.ErrorType.MissingBugdet ? "" : string.Format(" ({0})", item.RightBudget));
                }
                                
            }
            errorMsg += "---------------<br/>";

            return errorMsg;
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

        protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void selDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (this.selDept.SelectedValue == "0")
            //{
            //    this.HyperLink1.NavigateUrl = "";
            //    this.Label7.Visible = false;
            //    this.HyperLink1.Visible = false;
            //}
            //if (this.selDept.SelectedValue == "1")
            //{
            //    this.HyperLink1.NavigateUrl = "..\\Template\\酒店数据导入_金茂君悦.xls";
            //    this.Label7.Visible = true ;
            //    this.HyperLink1.Visible = true;
            //}

            //if (this.selDept.SelectedValue =="2")
            //{
            //    this.HyperLink1.NavigateUrl = "..\\Template\\酒店数据导入_崇明凯悦.xls";
            //    this.Label7.Visible = true;
            //    this.HyperLink1.Visible = true ;
            //}
 
        }
    }
}