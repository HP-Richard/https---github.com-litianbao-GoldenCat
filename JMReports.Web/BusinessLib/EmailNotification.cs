using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using JMReports.LinqToSql;

namespace JMReports.BusinessLib
{
  public class EmailNotification
  {
    public void DoSendMailTask()
    {
      using (var context = Helper.GetDataContext())
      {
        var ens = context.ImportCompletedEmailNoticeStatus.Where(s => s.EmailSendStatus == 0);
        foreach (var en in ens)
        {
          string errorMsg = string.Empty;
          var result = SendMail(en.Hotel.ChineseName, en.YearCode.Value, en.MonthCode, en.Kind, out errorMsg);
          
          if (result) { en.EmailSendStatus = 2; } //发送成功
          else
          {
            en.EmailSendStatus = 1;
            en.ErrorMessage = errorMsg;
          }
          context.SubmitChanges();
        }
      }
    }

    public bool SendMail(string hotelName, int yearCode, int? monthCode, string kind, out string errorMessage)
    {
      string xmlPath = Helper.GetBaseDirectory() + "EmailConfig.ini";
      var emailConfig = Helper.XmlFileToObject<EmailConfig>(xmlPath);
      string FromAddress = emailConfig.FromAddress;
      string FromDisplayName = emailConfig.FromDisplayName;
      string ToAddresses = emailConfig.ToAddresses.First(a => a.Key == hotelName).Address;
      string SmtpHost = emailConfig.SmtpHost;
      int SmtpPort = emailConfig.SmtpPort == "" ? 25 : Convert.ToInt32(emailConfig.SmtpPort);
      bool SmtpEnableSSL = emailConfig.SmtpEnableSSL == "" ? false : Convert.ToBoolean(emailConfig.SmtpEnableSSL);
      string SmtpUserName = emailConfig.SmtpUserName;
      string SmtpPassword = emailConfig.SmtpPassword;

      //string Subject = string.Format("{2}{0}年{1}全部数据已导入通知", yearCode.ToString(), monthCode.HasValue ? monthCode.Value.ToString() + "月" : "",hotelName);
      //string Body = string.Format("各位领导：\r\n　　{0}年{1} {2} {3}报表数据已经全部导入，请查阅，谢谢！\r\n　　（本通知为系统转发，请勿回复）\r\n\r\n　　酒店数据智能分析平台 {4}", yearCode.ToString(), monthCode.HasValue ? monthCode.Value.ToString() + "月份" : "", hotelName, kind, DateTime.Now.ToString("yyyy-MM-dd"));
      string Subject =TranslateCode( emailConfig.Subject,hotelName,yearCode,monthCode,kind);
      string Body = TranslateCode(emailConfig.Body.Trim(), hotelName, yearCode, monthCode, kind);

      MailMessage MyEmailMessage = new MailMessage();
      MailAddress from = new MailAddress(FromAddress, FromDisplayName, System.Text.Encoding.UTF8);
      MailAddressCollection to = new MailAddressCollection();
      MyEmailMessage.From = from;
      foreach (string address in ToAddresses.Split(','))
        MyEmailMessage.To.Add(address);
      MyEmailMessage.Subject = Subject;
      MyEmailMessage.SubjectEncoding = System.Text.Encoding.UTF8;
      MyEmailMessage.Body = Body;
      MyEmailMessage.BodyEncoding = System.Text.Encoding.UTF8;
      MyEmailMessage.Priority = MailPriority.High;

      SmtpClient client = new SmtpClient();
      //设置GMail邮箱和密码 
      client.Credentials = new System.Net.NetworkCredential(SmtpUserName, SmtpPassword);
      client.Port = SmtpPort;
      client.Host = SmtpHost;
      client.EnableSsl = SmtpEnableSSL;
      //client.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);
      //client.SendAsync(MyEmailMessage, "UserState");
      try
      {
        client.Send(MyEmailMessage);
        errorMessage = string.Empty;
        return true;
      }
      catch (Exception e)
      {
        try { errorMessage = e.InnerException.Message; }
        catch { errorMessage = e.Message; }
        return false;
      }
    }

    private string TranslateCode(string str, string hotelName, int yearCode, int? monthCode, string kind)
    {
      var tranKey =new Dictionary<string, string>();
      tranKey.Add("{year}", yearCode.ToString()+"年");
      tranKey.Add("{month}", monthCode.HasValue ? monthCode.Value.ToString() + "月" : "");
      tranKey.Add("{hotel}", hotelName);
      tranKey.Add("{kind}", kind);
      tranKey.Add("{today}", DateTime.Now.ToString("yyyy-MM-dd"));
      tranKey.Add("{now}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

      foreach (var key in tranKey)
      {
        str = str.Replace(key.Key, key.Value);
      }
      return str;
    }
    private void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
    {
      // Get the unique identifier for this asynchronous operation.
      String token = (string)e.UserState;

      if (e.Error != null)//邮件发送失败
      {

      }
      else //邮件发送成功
      {

      }
    }
  }
}
