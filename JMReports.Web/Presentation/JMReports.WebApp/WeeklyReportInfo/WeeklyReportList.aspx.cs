using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JMReports.WebApp.Utility;

namespace JMReports.WebApp.WeeklyReportInfo
{
  public partial class WeeklyReportList : CPageBase
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        var offset = (int)DateTime.Today.DayOfWeek;
        this.lblFrom.Text = DateTime.Today.AddDays(offset * -1 + 1).ToString("yyyy-MM-dd");
        this.lblTo.Text = DateTime.Today.AddDays(offset * -1 + 7).ToString("yyyy-MM-dd");
        BindingData();
      }
    }

    private void BindingData()
    {
      using (var context = Helper.GetDataContext())
      {
        var from = DateTime.Parse(this.lblFrom.Text);
        var to = DateTime.Parse(this.lblTo.Text);
        var user = this.txtCreaterUser.Text.Trim();

        var roleName = string.Empty;
        var userId = Helper.GetUserId();
        if (userId > 0)
        {
          var roleId = context.SysUser.First(u => u.Id == Helper.GetUserId()).RoleId;
          var role = context.Role.FirstOrDefault(r => r.Id == roleId);
          if (role != null) roleName = role.RoleName;
          if (roleName.Contains("周报管理"))  //属于周报管理角色
          {
            var wrs = context.WeeklyReportInfo.Where(w => w.CreateUser == Helper.GetUserId() || w.Status.Trim().Contains("已提交"));
            if (user.Length > 0) wrs = wrs.Where(w => w.SysUser.UserId.Contains(user));
            wrs = wrs.Where(w => w.CreateTime.Value >= from && w.CreateTime.Value < to.AddDays(1));
            this.gvWeeklyReport.DataSource = wrs.ToList();
            this.gvWeeklyReport.DataBind();
          }
          else
          {
            var wrs = context.WeeklyReportInfo.Where(w => w.CreateUser == Helper.GetUserId());
            if (user.Length > 0) wrs = wrs.Where(w => w.SysUser.UserId.Contains(user));
            wrs = wrs.Where(w => w.CreateTime.Value >= from && w.CreateTime.Value < to.AddDays(1));
            this.gvWeeklyReport.DataSource = wrs.ToList();
            this.gvWeeklyReport.DataBind();
          }
        }
      }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
      BindingData();
    }

    protected void lkbPrev_Click(object sender, EventArgs e)
    {
      var from = DateTime.Parse(this.lblFrom.Text);
      this.lblFrom.Text = from.AddDays(-7).ToString("yyyy-MM-dd");
      this.lblTo.Text = from.AddDays(-1).ToString("yyyy-MM-dd");
    }

    protected void lkbNext_Click(object sender, EventArgs e)
    {
      var from = DateTime.Parse(this.lblFrom.Text);
      this.lblFrom.Text = from.AddDays(7).ToString("yyyy-MM-dd");
      this.lblTo.Text = from.AddDays(13).ToString("yyyy-MM-dd");

    }
  }
}