using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using JMReports.Business;

namespace JMReports.WebApp.User
{
    public partial class UserManager : CPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                hideDiv();
                setRole();
                getUsers();

            } 

        }

        /// <summary>
        /// 隐藏增加user的div
        /// </summary>
        private void hideDiv()
        {
            this.divUser.Visible = false;
        }

        private void getUsers()
        {

            Business.UserComponent uc = new UserComponent();
            this.gvUserList.DataSource = uc.getUsers();
            this.gvUserList.DataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            this.lblMessage.Text = "";
            this.txtUserId.Text = "";
            this.txtEmail.Text = "";
            this.txtTitle.Text = "";
            this.txtPassword.Text = "";
            this.txtConfirmPassword.Text = "";

            this.lblId.Text = "";

            this.divUser.Visible = true;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            this.lblMessage.Text = "";

            string mId = string.Empty;


            for (int i = 0; i <= gvUserList.Rows.Count - 1; i++)
            {
                CheckBox cb1 = (CheckBox)gvUserList.Rows[i].FindControl("cb1");
                if (cb1.Checked)
                {
                    mId = mId + gvUserList.DataKeys[i].Value + ",";
                }
            }

            mId = mId.Substring(0, mId.Length - 1);
            Business.UserComponent uc = new UserComponent();
            uc.DeleteUser(mId);
            getUsers();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            this.lblMessage.Text = "";
            this.divUser.Visible = false;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            this.lblMessage.Text = "";

            if (this.txtConfirmPassword.Text != this.txtPassword.Text)
            {
                this.lblMessage.Text = "密码不匹配，请确认!";
                return;
            }

            if (this.lblId.Text == "")
            {
                if (this.txtPassword.Text == "")
                {
                    this.lblMessage.Text = "新建用户，必须输入密码！";
                    return;
                }

            }



            JMReports.Entities.User user1 = new Entities.User();
            user1.UserId = this.txtUserId.Text.Trim();
            user1.RoleId = int.Parse(this.ddlRole.SelectedItem.Value);
            user1.Title = this.txtTitle.Text.Trim();
            user1.Email = this.txtEmail.Text.Trim();
            user1.Psd = this.txtPassword.Text.Trim();


            Business.UserComponent uc = new UserComponent();

            if (this.lblId.Text == "")
            {
                user1 = uc.Create(user1);
                if (user1.Id != 0)
                {
                    this.lblMessage.Text = "用户新增成功!";
                    getUsers();

                    this.txtUserId.Text = "";
                    this.txtEmail.Text = "";
                    this.txtTitle.Text = "";
                    this.txtPassword.Text = "";
                    this.txtConfirmPassword.Text = "";
                    this.ddlRole.SelectedValue = "";

                    this.lblId.Text = "";

                }
            }
            else
            {
                user1.Id = int.Parse(this.lblId.Text);

                int i = uc.Update(user1);
                if (i > 0)
                {
                    this.lblMessage.Text = "用户修改成功!";
                    getUsers();

                    this.txtUserId.Text = "";
                    this.txtEmail.Text = "";
                    this.txtTitle.Text = "";
                    this.txtPassword.Text = "";
                    this.txtConfirmPassword.Text = "";
                    this.ddlRole.SelectedValue = "";

                    this.lblId.Text = "";
                }
            }
        }


        private void setRole()
        {
            Business.RoleComponent rc = new RoleComponent();
            this.ddlRole.DataSource = rc.getRoles();
            this.ddlRole.DataValueField = "Id";
            this.ddlRole.DataTextField = "RoleName";
            this.ddlRole.DataBind();

            ListItem lItem = new ListItem("", "");

            this.ddlRole.Items.Insert(0, lItem);


        }

        protected void btnUserHotel_Click(object sender, EventArgs e)
        {
            Page.Response.Redirect("UserHotel.aspx");
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            this.lblMessage.Text = "";

            string mId = string.Empty;

            for (int i = 0; i <= gvUserList.Rows.Count - 1; i++)
            {
                CheckBox cb1 = (CheckBox)gvUserList.Rows[i].FindControl("cb1");
                if (cb1.Checked)
                {
                    mId = mId + gvUserList.DataKeys[i].Value + ",";
                }
            }

            if (mId == string.Empty)
            {
                this.lblMessage.Text = "请选择一位用户";
                return;
            }


            mId = mId.Substring(0, mId.Length - 1);
            string[] ii = mId.Split(',');

            if (ii.Length >1){
                this.alertClient("请选择一位用户");
                return ;
            }


            Business.UserComponent uc = new UserComponent();

            JMReports.Entities.User user1 = uc.getUserById(mId);

            this.txtUserId.Text = user1.UserId;
            this.txtEmail.Text = user1.Email;
            this.txtTitle.Text = user1.Title;
            this.txtPassword.Text = user1.Psd;
            this.txtConfirmPassword.Text = user1.Psd;
            this.ddlRole.SelectedValue = user1.RoleId.ToString();
            this.lblId.Text = user1.Id.ToString();

            this.divUser.Visible = true;
        }
    }
}