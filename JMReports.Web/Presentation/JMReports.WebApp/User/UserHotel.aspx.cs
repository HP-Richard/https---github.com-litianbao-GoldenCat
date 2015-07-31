using JMReports.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JMReports.WebApp.User
{
    public partial class UserHotel : CPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                setUserList();
                setHotelList();

                setPage();
            } 
            
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (lbUserList.SelectedIndex >= 0)
            {
                int userId = 0;
                userId = int.Parse(lbUserList.SelectedValue.ToString());

                
                var hotelList = new List<Hotel>();

                for (int i = 0; i <= gvHotel.Rows.Count - 1; i++)
                {
                    CheckBox cb1 = (CheckBox)gvHotel.Rows[i].FindControl("cb1");
                    if (cb1.Checked)
                    {
                        //Page.Response.Write(gvReport.DataKeys[i].Value);
                        hotelList.Add(new Hotel { HotelId = int.Parse(gvHotel.DataKeys[i].Value.ToString()) });
                    }
                }

                Business.UserHotelComponent rc = new Business.UserHotelComponent();

                int retVal = rc.setUserHotel(userId, hotelList);
                this.alertClient(retVal >= 0 ? "用户酒店关联成功!" : "关联失败，请联系系统管理员");
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }

        protected void lbRoleList_SelectedIndexChanged(object sender, EventArgs e)
        {
            setPage();
        }


        public void setHotelList()
        {

            Business.HotelComponent hc = new Business.HotelComponent();

            this.gvHotel.DataSource  = hc.getHotels().Tables[0];
            this.gvHotel.DataBind();

        }

        public void setUserList()
        {
            Business.UserComponent uc = new Business.UserComponent();
            this.lbUserList.DataSource = uc.getUsers();
            this.lbUserList.DataTextField = "Userid";
            this.lbUserList.DataValueField = "id";
            this.lbUserList.DataBind();

            this.lbUserList.SelectedIndex = 0;
        }

        public void setPage()
        {
            for (int i = 0; i <= gvHotel.Rows.Count - 1; i++)
            {
                CheckBox cb1 = (CheckBox)gvHotel.Rows[i].FindControl("cb1");
                cb1.Checked = false;
            }

            if (lbUserList.SelectedIndex >= 0)
            {
                int userId = 0;
                userId = int.Parse(lbUserList.SelectedValue.ToString());

                Business.UserHotelComponent rc = new Business.UserHotelComponent();
                var userHotelList = rc.getUserHotel(userId);
                if (userHotelList != null && userHotelList.Count > 0)
                {

                    foreach(var item in userHotelList)
                    {
                        for (int i = 0; i <= gvHotel.Rows.Count - 1; i++)
                        {
                            if (gvHotel.DataKeys[i].Value.ToString() == item.HotelId.ToString())
                            {
                                CheckBox cb1 = (CheckBox)gvHotel.Rows[i].FindControl("cb1");
                                cb1.Checked = true;
                            }
                        }
                    }
                }


            }
        }

    }
}