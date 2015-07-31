using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JMReports.WebApp
{
    public partial class Test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["AA"] = DateTime.Now.AddYears(10).ToString();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Session["AA"] = DateTime.Now.ToString();

            Label1.Text = selDept.SelectedValue; //DropDownList1.SelectedItem.Text;


        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            var a = Request.Form[DropDownList1.UniqueID];
            Label1.Text = a.ToString();
        }
    }
}