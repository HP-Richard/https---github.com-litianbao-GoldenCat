using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using JMReports.Business;
using System.Web.Security;

namespace JMReports.WebApp
{
    public partial class index : CPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FormsAuthentication.SignOut();
                HttpContext.Current.Session.Clear();
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string userName = this.txtUserID.Text;

            MyPrincipal principal;

            HttpContext.Current.Session.Clear();

            UserComponent uc = new UserComponent();

            if (uc.VerifyPassword(userName, txtPassword.Text))
            {


                principal = new MyPrincipal(userName);

                System.Threading.Thread.CurrentPrincipal = principal;

                HttpContext.Current.Session["JMPrincipal"] = principal;

                HttpContext.Current.User = (MyPrincipal)(HttpContext.Current.Session["JMPrincipal"]);

                try
                {
                    //登陆系统
                    Page.Response.Redirect("Main.aspx");

                    //string url = FormsAuthentication.GetRedirectUrl(userName, true);

                    //Response.Redirect(url);
                    //FormsAuthentication.RedirectFromLoginPage(userName, false);

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                this.txtPassword.Text = "";
                this.txtUserID.Text = "";
            }
        }
    }
}