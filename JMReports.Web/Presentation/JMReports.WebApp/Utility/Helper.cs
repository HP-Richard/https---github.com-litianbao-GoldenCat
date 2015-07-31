using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Caching;
using JMReports.LinqToSql;

namespace JMReports.WebApp.Utility
{
  public static class Helper
  {
    public static void AddCacheData(this DataSet ds, params object[] paras)
    {
      System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace(true);
      var methodBase = st.GetFrame(1).GetMethod();
      string key = methodBase.ReflectedType.FullName + "." + methodBase.Name;
      foreach (var p in paras) key += "_" + p.ToString();

      Cache cache = HttpRuntime.Cache;
      if (cache != null) cache.Insert(key, ds, null, DateTime.Now.AddDays(1).AddHours(8), Cache.NoSlidingExpiration);
    }

    public static DataSet GetCacheData(params object[] paras)
    {
      System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace(true);
      var methodBase = st.GetFrame(1).GetMethod();
      string key = methodBase.ReflectedType.FullName + "." + methodBase.Name;
      foreach (var p in paras) key += "_" + p.ToString();

      Cache cache = HttpRuntime.Cache;
      if (cache != null)
      {
        try { return cache[key] as DataSet; }
        catch { return null; }
      }
      else { return null; }
    }

    public static T GetAppSetting<T>(string key)
    {
      T appSetting = default(T);
      try { appSetting = (T)Convert.ChangeType(System.Configuration.ConfigurationManager.AppSettings[key], typeof(T)); }
      catch { appSetting = default(T); }
      return appSetting;
    }

    public static JMDataContext GetDataContext()
    {
      JMDataContext context = new JMDataContext(System.Configuration.ConfigurationManager.ConnectionStrings[CONSTANT.DATACONNECTIONSTRINGKEY].ConnectionString);
      return context;
    }

    public static SysUser GetUser(this JMDataContext context)
    {
      var userId = GetUserId();
      if (userId > 0)
        return context.SysUser.First(u => u.Id == userId);
      else
        return null;
    }

    public static int GetUserId()
    {
      if (HttpContext.Current.Session["JMPrincipal"] != null)
      {
        var principal = HttpContext.Current.Session["JMPrincipal"] as JMReports.Business.MyPrincipal;
        if (principal != null)
        {
          return (principal.Identity as JMReports.Business.MyIdentity).Id;
        }
      }
      return -1;
    }

    public static string GetBaseDirectory()
    {
      return AppDomain.CurrentDomain.BaseDirectory;
    }
  }

  public class CONSTANT
  {
    public static readonly string USINGCACHESETTINGKEY = "UsingCache";

    public static readonly string DATACONNECTIONSTRINGKEY = "SQLConnectionString";
  }
}