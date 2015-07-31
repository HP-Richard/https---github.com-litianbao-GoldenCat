using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JMReports.WebApp
{
  public partial class HotelByUser1 : System.Web.UI.UserControl
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      Page.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
      if (!IsPostBack)
      {
        if (HttpContext.Current.Session["JMPrincipal"] != null)
        {
          var principal = HttpContext.Current.Session["JMPrincipal"] as JMReports.Business.MyPrincipal;
          if (principal != null)
          {

            int userId = (principal.Identity as JMReports.Business.MyIdentity).Id;
            Business.UserHotelComponent rc = new Business.UserHotelComponent();
            var userHotelList = rc.getUserHotel(userId);
            selDept.DataSource = userHotelList;
            selDept.DataTextField = "ChineseName";
            selDept.DataValueField = "HotelId";
            selDept.DataBind();
            selDept.Items.Insert(0, "");
            var defaultHotel = userHotelList.FirstOrDefault();
            if (defaultHotel != null)
              selDept.Items.FindByValue(defaultHotel.HotelId.ToString()).Selected = true;

            try
            {
              var search = HttpContext.Current.Session["CurrentCondition"] as JMReports.Business.SearchCondition;
              selDept.SelectedValue = search.Hotel;
            }
            catch { }
          }
        }
      }
    }

    public int SelectedIndex { set { selDept.SelectedIndex = value; } }
    public string SelectedValue { get { return selDept.SelectedValue.ToString(); } }

    public ListItem SelectedItem { get { return selDept.SelectedItem; } }
  }
}