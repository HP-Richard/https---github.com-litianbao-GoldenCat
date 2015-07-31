using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using JMReports.BusinessLib;

namespace JMWindowsService
{
  public partial class JMService : ServiceBase
  {
    private System.Timers.Timer timer;
    public JMService()
    {
      InitializeComponent();
    }

    protected override void OnStart(string[] args)
    {
      double intervalMinutes = 1;
      try { intervalMinutes = Helper.GetAppSetting<double>("IntervalMinutes"); }
      catch { }
      timer = new System.Timers.Timer();
      timer.Interval = intervalMinutes * 60 * 1000;
      timer.Enabled = true;
      timer.Elapsed += timer_Tick;
      timer.Start();
    }

    protected override void OnStop()
    {
      timer.Enabled = false;
      timer.Stop();
    }

    private void timer_Tick(object sender, EventArgs e)
    {
      EmailNotification en = new EmailNotification();
      en.DoSendMailTask();      
    }
  }
}
