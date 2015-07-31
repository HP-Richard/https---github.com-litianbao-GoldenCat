using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JMReports.BusinessLib;


namespace ConsoleApplication1
{
  class Program
  {
    static void Main(string[] args)
    {
      EmailNotification en = new EmailNotification();
      en.DoSendMailTask();      
    }
  }
}
