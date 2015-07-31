using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace JMReports.WebApp
{
    /// <summary>
    /// Summary description for HotelByUser
    /// </summary>
    public class HotelByUser : IHttpHandler, IRequiresSessionState 
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string session = context.Session["AA"] == null ? "N/A session" : context.Session["AA"].ToString();

            string strJson = "{\"Name\":\"Andrew\",\"Age\":45,\"Time\":\""+session+"\"}";
            

            context.Response.Write(strJson);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}