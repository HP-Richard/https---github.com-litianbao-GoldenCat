using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Text.RegularExpressions;
using JMReports.Business;
using JMReports.Entities;


namespace JMReports.WebApp
{
    public class CPageBase:System.Web.UI.Page
    {

        public CPageBase()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }


        public JMReports.Entities.User CurrentUser
        {
            get
            {

                HttpContext.Current.User = (MyPrincipal)(HttpContext.Current.Session["JMPrincipal"]);

                return ((MyIdentity)(HttpContext.Current.User.Identity)).User;

            }
        }

        public MyPrincipal CurrentPrincipal
        {
            get
            {

                return (MyPrincipal)(HttpContext.Current.Session["JMPrincipal"]);

            }
        }


        public bool IsUserLogin()
        {
            if (!(HttpContext.Current.Session["JMPrincipal"] is Business.MyPrincipal))
            {
                return false;
            }
            else
            {
                return true;
            }

        }


        public string getWhere(string ColName, string strValue)
        {
            if (strValue == null) strValue = "";
            string str = strValue.Trim();
            string strWhere = "";
            if (str != "")
            {
                strWhere = strWhere + " AND " + ColName.Replace("'", "''") + " LIKE '%" + str.Replace("'", "''") + "%' ";
            }
            return strWhere;
        }




        public void alertClient(string mMessage)
        {
            Page.Response.Write(AlertClient(mMessage));
        }


        //允许空
        public bool isDate(string date1)
        {

            if (date1 == "")			//允许空
            {
                return true;
            }


            DateTime dtDate;
            bool bValid = true;
            try
            {
                dtDate = DateTime.Parse(date1);
            }
            catch (System.ArgumentOutOfRangeException)
            {
                bValid = false;
            }
            catch (FormatException)
            {
                bValid = false;
            }

            return bValid;
        }


        public bool isDouble(string date1)
        {
            Double v1;
            bool bValid = true;
            try
            {
                v1 = double.Parse(date1);
            }
            catch (FormatException)
            {
                bValid = false;
            }
            return bValid;
        }


        public bool isInt(string date1)
        {
            int v1;
            bool bValid = true;
            try
            {
                v1 = int.Parse(date1);
            }
            catch (FormatException)
            {
                bValid = false;
            }
            return bValid;
        }


        public bool IsValidEmail(string strIn)
        {
            // Return true if strIn is in valid e-mail format. 
            return Regex.IsMatch(strIn, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }

        /// <summary>
        /// 显示消息提示对话框
        /// </summary>
        /// <param name="page">当前页面指针，一般为this</param>
        /// <param name="msg">提示信息</param>
        public void Show(System.Web.UI.Page page, string msg)
        {
        }

        /// <summary>
        /// 控件点击 消息确认提示框
        /// </summary>
        /// <param name="page">当前页面指针，一般为this</param>
        /// <param name="msg">提示信息</param>
        public void ShowConfirm(System.Web.UI.WebControls.WebControl Control, string msg)
        {
            //Control.Attributes.Add("onClick","if (!window.confirm('"+msg+"')){return false;}");
            Control.Attributes.Add("onclick", "return confirm('" + msg + "');");
        }

        /// <summary>
        /// 显示消息提示对话框，并进行页面跳转
        /// </summary>
        /// <param name="page">当前页面指针，一般为this</param>
        /// <param name="msg">提示信息</param>
        /// <param name="url">跳转的目标URL</param>
        public void ShowAndRedirect(System.Web.UI.Page page, string msg, string url)
        {
            StringBuilder Builder = new StringBuilder();
            Builder.Append("<script language='javascript' defer>");
            Builder.AppendFormat("alert('{0}');", msg);
            Builder.AppendFormat("top.location.href='{0}'", url);
            Builder.Append("</script>");
            //page.RegisterStartupScript("message", Builder.ToString());

        }
        /// <summary>
        /// 输出自定义脚本信息
        /// </summary>
        /// <param name="page">当前页面指针，一般为this</param>
        /// <param name="script">输出脚本</param>
        public void ResponseScript(System.Web.UI.Page page, string script)
        {
            //page.RegisterStartupScript("message", "<script language='javascript' defer>" + script + "</script>");
        }

        /// <summary>
        /// 录入日志
        /// </summary>
        public void writelog(string mFunctionName, string mOpName, int mStuSeq)
        {
            //CLog l = new CLog();

            //l.CurrentUser = CurrentUser.UserName;
            //l.CurrentUserSeq = CurrentUser.Seq;
            //l.FunctionName = mFunctionName;
            //l.OpName = mOpName;
            //l.StuSeq = mStuSeq;
            //l.Insert();
        }


        /// <summary>
        /// 录入日志:没有对单个学生操作的不把学生编号写入日志!
        /// </summary>
        public void writelog(string mFunctionName, string mOpName)
        {
            //CLog l = new CLog();

            //l.CurrentUser = CurrentUser.UserName;
            //l.CurrentUserSeq = CurrentUser.Seq;
            //l.FunctionName = mFunctionName;
            //l.OpName = mOpName;
            //l.StuSeq = 0;
            //l.Insert();
        }


        public string AlertClient(string mMessage)
        {
            string strClientScript = "<Script Language='JScript'>";
            strClientScript = strClientScript + "alert('" + mMessage + "');";
            strClientScript = strClientScript + "</Script>";

            return strClientScript;
        }
    }
}