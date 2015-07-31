using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.SessionState;

namespace JMReports.WebApp
{
    /// <summary>
    /// Summary description for MenuByRole
    /// </summary>
    public class MenuByRole : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";



            string strJson = "{}";
            var principal = HttpContext.Current.Session["JMPrincipal"] as JMReports.Business.MyPrincipal;
            if (principal != null)
            {

                int userId = (principal.Identity as JMReports.Business.MyIdentity).Id;

                Business.ReportInfoComponent rc = new Business.ReportInfoComponent();
                var reports = rc.getReportsById(userId);

                string currentCategory = "", header = "{\"ReportByCategory\":[", trailor = "]}";
                var reportList = new List<string>();
                var categoryList = new List<string>();
                var reportSB = new StringBuilder();

                var categories = from b in reports
                                 group b by b.Category into g
                                 select new { Category = g.Key, Count = g.Count() };

                categoryList.Clear();
                foreach (var cat in categories)
                {
                    currentCategory = cat.Category;
                    string tmp = "";


                    reportList.Clear();

                    tmp = "{\"Category\":\"" + currentCategory + "\",\"Reports\":[";

                    foreach (var item in reports.Where(x => x.Category == cat.Category))
                    {
                        reportList.Add("{\"ReportId\":" + item.ReportId.ToString() + ",\"Name\":\"" + item.Name + "\",\"ChineseName\":\"" + item.ChineseName + "\",\"URL\":\"" + item.URL + "\"}");
                    }

                    categoryList.Add(tmp + String.Join(",", reportList.ToArray()) + "]}");

                }
                reportSB.Append(header);
                reportSB.Append(string.Join(",", categoryList.ToArray()));
                reportSB.Append(trailor);
                strJson = reportSB.ToString();
            }
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