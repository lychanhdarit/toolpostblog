using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Timers;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace ToolPost
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            System.Timers.Timer myTimer = new System.Timers.Timer();
            myTimer.Interval = 60000; //3.600.000;
            myTimer.AutoReset = true;
            myTimer.Elapsed += new ElapsedEventHandler(myTimer_Elapsed);
            myTimer.Enabled = true;
        }
       

        public void myTimer_Elapsed(object source, System.Timers.ElapsedEventArgs e)
        {
            // Object initialization of data link library
            //ScheduleMailandSMS _ScheduleMailandSMS = new ScheduleMailandSMS();
            //DataTable dttemppayinslip = GetData_PayinslipSendsmmail();
            //foreach (DataRow dr in dttemppayinslip.Rows)
            //{
            //    _ScheduleMailandSMS.SendOrderDetailsMail("", "");
            //}
            using (StreamWriter w = File.AppendText(Server.MapPath("~/log.txt")))
            {
                w.WriteLine(DateTime.Now.ToString());
                w.Flush();
            }
        }
        //Global file events End

        //Mail send function Start
        public void SendOrderDetailsMail(string bodystr, string AMEmailID)
        {
            //DataTable dtemailsetting = _objemailsetting.GetEmailSettingAll();
            //if (dtemailsetting.Rows.Count > 0)
            //{
            //    MailMessage mail = new MailMessage();
            //    SmtpClient SmtpServer = new SmtpClient(dtemailsetting.Rows[0]["SmtpHost"].ToString());

            //    mail.From = new MailAddress(dtemailsetting.Rows[0]["AdminEmail_registeruser"].ToString());
            //    mail.To.Add("pramod,joshi@abc.com");
            //    mail.Subject = "test mail ";
            //    mail.Body = bodystr;
            //    SmtpServer.Port = Convert.ToInt16(dtemailsetting.Rows[0]["SMTPPort"]);
            //    SmtpServer.Credentials = new System.Net.NetworkCredential(dtemailsetting.Rows[0]["SMTPEmail"].ToString(), dtemailsetting.Rows[0]["SMTPPassword"].ToString());
            //    SmtpServer.EnableSsl = false;
            //    mail.IsBodyHtml = true;
            //    SmtpServer.Send(mail);
            //}
            //else
            //{

            //}
        }
    }
}