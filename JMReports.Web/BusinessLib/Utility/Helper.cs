using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Serialization;
using JMReports.LinqToSql;

namespace JMReports.BusinessLib
{
  public static class Helper
  {
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


    public static string GetBaseDirectory()
    {
      return AppDomain.CurrentDomain.BaseDirectory;
    }

    public static string ToXML(this object o)
    {
      XmlSerializer ser = new XmlSerializer(o.GetType());
      using (MemoryStream mem = new MemoryStream())
      {
        XmlTextWriter writer = new XmlTextWriter(mem, Encoding.UTF8);
        ser.Serialize(writer, o);
        writer.Close();
        return Encoding.UTF8.GetString(mem.ToArray());
      }
    }

    public static T XMLToObject<T>(this string xml)
    {
      try
      {
        XmlSerializer mySerializer = new XmlSerializer(typeof(T));
        StreamReader mem2 = new StreamReader(new MemoryStream(System.Text.Encoding.UTF8.GetBytes(xml)), System.Text.Encoding.UTF8);
        return (T)mySerializer.Deserialize(mem2);
      }
      catch { return default(T); }
    }

    /// <summary>
    /// 对象序列化至文件
    /// </summary>
    /// <param name="o"></param>
    /// <param name="filename"></param>
    /// <param name="userName"></param>
    /// <returns></returns>
    public static bool ToXmlFile(this object o, string filename)
    {
      try
      {
        if (!filename.Contains(@":\")) filename = AppDomain.CurrentDomain.BaseDirectory + @"\InfoFiles\" + filename.TrimStart(@"\".ToCharArray());
        var path = filename.Substring(0, filename.LastIndexOf(@"\") + 1);
        if (!Directory.Exists(path)) Directory.CreateDirectory(path);
        if (File.Exists(filename)) File.Delete(filename);
        XmlSerializer ser = new XmlSerializer(o.GetType());
        XmlTextWriter writer = new XmlTextWriter(filename, Encoding.UTF8);
        ser.Serialize(writer, o);
        writer.Close();
        return true;
      }
      catch { return false; }
    }

    /// <summary>
    /// 文件反序列化至对象
    /// </summary>
    /// <param name="o"></param>
    /// <param name="userName"></param>
    /// <returns></returns>
    public static T XmlFileToObject<T>(this string filename)
    {
      try
      {
        if (!filename.Contains(@":\")) filename = AppDomain.CurrentDomain.BaseDirectory + @"\InfoFiles\" + filename.TrimStart(@"\".ToCharArray());
        if (File.Exists(filename))
        {
          XmlSerializer ser = new XmlSerializer(typeof(T));
          using (FileStream fs = new FileStream(filename, FileMode.Open))
          {
            T o = (T)ser.Deserialize(fs);
            fs.Close();
            return o;
          }
        }
        else throw new FileNotFoundException(filename);
      }
      catch { return default(T); }
    }
  }

  public class CONSTANT
  {
    public static readonly string DATACONNECTIONSTRINGKEY = "SQLConnectionString";
  }
}