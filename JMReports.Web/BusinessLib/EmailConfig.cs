using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace JMReports.BusinessLib
{
  public class EmailConfig
  {
    public string FromAddress { get; set; }
    public string FromDisplayName { get; set; }
    public List<ToAddress> ToAddresses { get; set; }
    public string SmtpHost { get; set; }
    public string SmtpPort { get; set; }
    public string SmtpEnableSSL { get; set; }
    public string SmtpUserName { get; set; }
    public string SmtpPassword { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
  }

  public class ToAddress
  {
    [XmlAttribute]
    public string Key { get; set; }
    [XmlText]
    public string Address { get; set; }
  }
}
