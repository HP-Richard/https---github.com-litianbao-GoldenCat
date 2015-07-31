using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using JMReports.Business;

namespace JMReports.WebApp.User
{
  public partial class UserPsdMod : CPageBase
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (!Page.IsPostBack)
      {
        var user = (MyPrincipal)HttpContext.Current.Session["JMPrincipal"];
        this.lblUserID.Text = user.identity.Name;
        this.hddId.Value = user.identity.Id.ToString();
      }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
      this.lblMessage.Text = "";

      //if (this.txtNewPassword.Text != this.txtNewPasswordConfirm.Text)
      //{
      //  this.lblMessage.Text = "新密码两次输入不一致，请确认!";
      //  return;
      //}

      //if (this.txtNewPassword.Text == "")
      //{
      //  this.lblMessage.Text = "必须输入密码！";
      //  return;
      //}

      Business.UserComponent uc = new UserComponent();
      if (this.txtPassword.Text.Trim().Length == 0 || !uc.VerifyPassword(this.lblUserID.Text, this.txtPassword.Text.Trim()))
      {
        this.lblMessage.Text = "当前密码输入错误，请重新输入!";
        return;
      }

      var user = uc.getUserById(this.hddId.Value);
      user.Psd = this.txtNewPassword.Text;
      if (uc.Update(user) > 0)
      {
        this.lblMessage.Text = "修改密码成功!";
      }
      else
      {
        this.lblMessage.Text = "修改密码失败，请稍后再试！";
      }
    }
  }
}