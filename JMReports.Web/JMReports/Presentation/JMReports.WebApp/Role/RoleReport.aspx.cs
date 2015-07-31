using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JMReports.WebApp.Role
{
    public partial class RoleReport : CPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                setRoleList();
                serReports();

                SetPage();
            } 
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int roleId = 0;
            roleId = int.Parse(lbRoleList.SelectedValue.ToString());


            string mReportString = string.Empty;

            for (int i = 0; i <= gvReport.Rows.Count - 1; i++)
            {
                CheckBox cb1 = (CheckBox)gvReport.Rows[i].FindControl("cb1");
                if (cb1.Checked)
                {
                    //Page.Response.Write(gvReport.DataKeys[i].Value);
                    mReportString = mReportString + gvReport.DataKeys[i].Value + ",";
                }
            }

            if (mReportString != string.Empty)
            {
                mReportString = mReportString.Substring(0, mReportString.Length - 1);
                Business.RoleComponent rc = new Business.RoleComponent();
                int retVal = rc.setRoleReport(roleId, mReportString);
                this.alertClient(retVal >= 0 ? "角色设置成功!" : "失败，请联系系统管理员");
                //Page.Response.Write(lbRoleList.SelectedValue.ToString());
            }
            else
            {
                this.alertClient("请选择对应的报表！");
            }

        }

        private void setRoleList()
        {
            Business.RoleComponent rc = new Business.RoleComponent();
            this.lbRoleList.DataSource = rc.getRoles();
            this.lbRoleList.DataTextField = "RoleName";
            this.lbRoleList.DataValueField = "Id";
            this.lbRoleList.DataBind();

            this.lbRoleList.SelectedIndex = 0;
        }

        private void serReports()
        {
            Business.ReportInfoComponent rc = new Business.ReportInfoComponent();
            this.gvReport.DataSource = rc.getReports();
            this.gvReport.DataBind();
        }

        public void alertClient(string mMessage)
        {
            Page.Response.Write(AlertClient(mMessage));
        }

        private void SetPage()
        {

            for (int i = 0; i <= gvReport.Rows.Count - 1; i++)
            {
                CheckBox cb1 = (CheckBox)gvReport.Rows[i].FindControl("cb1");
                cb1.Checked = false;
            }

            if (lbRoleList.SelectedIndex >= 0)
            {
                int roleId = 0;
                roleId = int.Parse(lbRoleList.SelectedValue.ToString());

                string reportIds = string.Empty;

                Business.RoleComponent rc = new Business.RoleComponent();
                reportIds = rc.getReportIDs(roleId);
                if (reportIds != "")
                {
                    string[] repId = reportIds.Split(',');

                    for (int k = 0; k <= repId.Length - 1; k++)
                    {
                        for (int i = 0; i <= gvReport.Rows.Count - 1; i++)
                        {
                            if (gvReport.DataKeys[i].Value.ToString() == repId[k])
                            {
                                CheckBox cb1 = (CheckBox)gvReport.Rows[i].FindControl("cb1");
                                cb1.Checked = true;
                            }
                        }
                    }
                }


            }

        }

        protected void lbRoleList_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetPage();
        }
    }
}