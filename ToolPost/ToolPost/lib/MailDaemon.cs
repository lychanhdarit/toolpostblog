using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace ToolPost.lib
{
    public class MailDaemon
    {
        //private static DBClass _db = new DBClass();
        private static SmtpClient MailClient = new SmtpClient();
        private static string sitename = ConfigurationSettings.AppSettings["SiteName"];
        private static string https = HttpContext.Current.Request.ServerVariables["HTTPS"];
        public static string linkSite = "www.hxml.org";
        #region Email Diable
        private static List<MailAddress> AdditionalCCEmails = new List<MailAddress>
        {
        };

        private static List<MailAddress> AdditionalBCCEmails = new List<MailAddress>
        {
        };

        //public static void registration(string email,string name, string webcode)
        //{
        //    try
        //    {
        //        MailAddress source = new MailAddress("confirmation@mint-corp.com", sitename);
        //        MailAddress recipient = new MailAddress(email, name);

        //        MailMessage msg = new MailMessage();
        //        msg.From = source;
        //        msg.To.Add(recipient);

        //        foreach (MailAddress ma in MailDaemon.AdditionalCCEmails)
        //            msg.CC.Add(ma);

        //        foreach (MailAddress ma in MailDaemon.AdditionalBCCEmails)
        //            msg.Bcc.Add(ma);

        //        msg.Subject = "Registration Confirmation";

        //        string ss = "Hello " + name;

        //        ss += "<br />       <br />";
        //        ss += "You have already registered, click on the link below to complete and sign.<br />";
        //        string link = "http://97.74.205.162:8090/home.aspx?w=" + webcode;
        //        ss += "<a href='" + link + "'>" + link + "</a>";
        //        ss += "<br />If you are not registered, then ignore this message.";



        //        ss += "<br />       <br />";
        //        ss += "We appreciate your business!<br />        <br />";
        //        ss += "Sincerely,     <br />       <br />";
        //        ss += "HomeCare System<br />      <br />";
        //        ss += "Customer Service Team   <br />    <br />";
        //        ss += "1-855-4-ABC    <br />    <br />";
        //        ss += "FAX: 1-866-XXX-XXXX     <br />       <br />";
        //        ss += "Email : support@mint-corp.com";

        //        msg.Body = ss;
        //        msg.IsBodyHtml = true;
        //        MailDaemon.MailClient.Send(msg);
        //    }
        //    catch
        //    {

        //    }

        //}

        //public static void forgotusername(string email, string name, string username)
        //{
        //    try
        //    {
        //        MailAddress source = new MailAddress("confirmation@mint-corp.com", sitename);
        //        MailAddress recipient = new MailAddress(email, name);

        //        MailMessage msg = new MailMessage();
        //        msg.From = source;
        //        msg.To.Add(recipient);

        //        foreach (MailAddress ma in MailDaemon.AdditionalCCEmails)
        //            msg.CC.Add(ma);

        //        foreach (MailAddress ma in MailDaemon.AdditionalBCCEmails)
        //            msg.Bcc.Add(ma);

        //        msg.Subject = "Forgot Username ";

        //        string ss = "Hello " + name;

        //        ss += "<br />       <br />";
        //        ss += "You just send the requested username. At website HomeCare Systems : ";

        //        string link = "http://97.74.205.162:8090/";
        //        ss += "<a href='" + link + "'>" + link + "</a>";

        //        ss += "<br />Username : " + username;


        //        ss += "<br />       <br />";
        //        ss += "We appreciate your business!<br />        <br />";
        //        ss += "Sincerely,     <br />       <br />";
        //        ss += "HomeCare System<br />      <br />";
        //        ss += "Customer Service Team   <br />    <br />";
        //        ss += "1-855-4-ABC    <br />    <br />";
        //        ss += "FAX: 1-866-XXX-XXXX     <br />       <br />";
        //        ss += "Email : support@mint-corp.com";

        //        msg.Body = ss;
        //        msg.IsBodyHtml = true;
        //        MailDaemon.MailClient.Send(msg);
        //    }
        //    catch
        //    {

        //    }

        //}
        //public static void forgotpassword(string email, string name, string passhint)
        //{
        //    try
        //    {
        //        MailAddress source = new MailAddress("confirmation@mint-corp.com", sitename);
        //        MailAddress recipient = new MailAddress(email, name);

        //        MailMessage msg = new MailMessage();
        //        msg.From = source;
        //        msg.To.Add(recipient);

        //        foreach (MailAddress ma in MailDaemon.AdditionalCCEmails)
        //            msg.CC.Add(ma);

        //        foreach (MailAddress ma in MailDaemon.AdditionalBCCEmails)
        //            msg.Bcc.Add(ma);

        //        msg.Subject = "Forgot Password";

        //        string ss = "Hello " + name;

        //        ss += "<br />       <br />";
        //        ss += "You just send the requested Password Hint. At website HomeCare Systems : ";
        //        string link = "http://97.74.205.162:8090/";
        //        ss += "<a href='" + link + "'>" + link + "</a>";
        //        ss += "<br />This is Password Hint: " + passhint;


        //        ss += "<br />       <br />";
        //        ss += "We appreciate your business!<br />        <br />";
        //        ss += "Sincerely,     <br />       <br />";
        //        ss += "HomeCare System<br />      <br />";
        //        ss += "Customer Service Team   <br />    <br />";
        //        ss += "1-855-4-ABC    <br />    <br />";
        //        ss += "FAX: 1-866-XXX-XXXX     <br />       <br />";
        //        ss += "Email : support@mint-corp.com";

        //        msg.Body = ss;
        //        msg.IsBodyHtml = true;
        //        MailDaemon.MailClient.Send(msg);
        //    }
        //    catch
        //    {

        //    }        
        //}
        #endregion
        private static SmtpClient getsmtpgmail()
        {
            string from = "codeproject.info@gmail.com";
            string password = "05051990";

            var smtp = new SmtpClient()
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = ("TLS" != ""),
                UseDefaultCredentials = false,
                Credentials = new System.Net.NetworkCredential(from, password)
            };
            return smtp;

            //string from = "postmaster@smarteyewear.com";
            //string password = "Angela02";

            //var smtp = new SmtpClient()
            //{
            //    Host = "184.168.69.59",
            //    UseDefaultCredentials = false,
            //    Credentials = new System.Net.NetworkCredential(from, password)
            //};
            //return smtp;

        }
        private static SmtpClient getsmtpgmail(string from, string password)
        {

            var smtp = new SmtpClient()
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = ("TLS" != ""),
                UseDefaultCredentials = false,
                Credentials = new System.Net.NetworkCredential(from, password)
            };
            return smtp;


        }
        public static void supportOnline(string name, string email, string body, string emailRecipient)
        {
            var smtp = getsmtpgmail();
            string from = "noreply@gmail.com";

            string subject = "Hỗ trợ online: " + name;
            string body_main = "Khách hàng: " + name + " email: " + email + "<br/>" + body;
            using (MailMessage msg = new MailMessage())
            {
                try
                {
                    MailAddress source = new MailAddress(from, sitename);
                    MailAddress recipient = new MailAddress(emailRecipient);

                    msg.From = source;
                    msg.To.Add(recipient);
                    msg.Subject = subject;
                    msg.Body = body_main;

                    msg.IsBodyHtml = true;
                    smtp.Send(msg);
                }
                catch (Exception) { }
            }
        }
        public static void DangKyNhanTin(string email, string name, string username, string mk)
        {
            var smtp = getsmtpgmail();
            string from = "noreply@gmail.com";
            string body = "";
            string subject = "Bạn đã đăng ký nhận tin thành công ";
            body += "Chào mừng bạn đến với Web Site.<br/> Thông tin của bạn:<br/>";

            using (MailMessage msg = new MailMessage())
            {
                try
                {
                    MailAddress source = new MailAddress(from, sitename);
                    MailAddress recipient = new MailAddress(email);

                    msg.From = source;
                    msg.To.Add(recipient);
                    msg.Subject = subject;
                    msg.Body = body;
                    msg.IsBodyHtml = true;
                    smtp.Send(msg);
                }
                catch (Exception) { }
            }

        }
        public static void NhanTin(string email, string name, string body, string mk)
        {
            var smtp = getsmtpgmail();
            string from = "noreply@gmail.com";
            string subject = "Thông Tin Bất Động Sản ";
            body += "Xin chào:" + email;
            body += "Thông tin bất động sản:<br/>";
            using (MailMessage msg = new MailMessage())
            {
                try
                {
                    MailAddress source = new MailAddress(from, sitename);
                    MailAddress recipient = new MailAddress(email);

                    msg.From = source;
                    msg.To.Add(recipient);
                    msg.Subject = subject;
                    msg.Body = body;

                    msg.IsBodyHtml = true;
                    smtp.Send(msg);
                }
                catch (Exception) { }
            }

        }//
        public static void sendmail(string email, string name, string _body)
        {
            var smtp = getsmtpgmail();
            string from = "noreply@gmail.com";


            string body = "";
            string subject = name;
            body += _body;

            using (MailMessage msg = new MailMessage())
            {
                try
                {
                    MailAddress source = new MailAddress(from, sitename);
                    MailAddress recipient = new MailAddress(email);

                    msg.From = source;
                    msg.To.Add(recipient);
                    msg.Subject = subject;
                    msg.Body = body;

                    msg.IsBodyHtml = true;
                    smtp.Send(msg);
                }
                catch (Exception) { }
            }

        }//
        public static void Reset_password(string email, string name, string username, string mk)
        {
            var smtp = getsmtpgmail();
            string from = "noreply@gmail.com";
            //  DataTable dt = null;// _db.GetInfo_EmailTemplates(3);

            string body = "";
            string subject = "Mật  khẩu mới của bạn ";
            body += "Chào mừng bạn đến với thammydiamond.<br/> Thông tin của bạn:<br/>";
            body += "Số hồ sơ: " + username + "<br/>Mật khẩu: " + mk + @"<br/> Bạn vui lòng đăng nhập vào website:" + linkSite + " cập nhật thông tin!";

            //DataRow row = _db.Get_Info_Emailtemplete("reset_password");
            //if (row != null)
            //{
            //    subject = BaseView.GetStringFieldValue(row, "subject");
            //    body = BaseView.GetStringFieldValue(row, "body");
            //    body = body.Replace("@sohoso", username);
            //    body = body.Replace("@matkhau", mk);
            //}
            using (MailMessage msg = new MailMessage())
            {
                try
                {
                    MailAddress source = new MailAddress(from, sitename);
                    MailAddress recipient = new MailAddress(email);

                    msg.From = source;
                    msg.To.Add(recipient);
                    msg.Subject = subject;
                    msg.Body = body;

                    msg.IsBodyHtml = true;
                    smtp.Send(msg);
                }
                catch (Exception) { }
            }

        }
        public static void forgetPassword(string email, string name, string username, string mk)
        {
            var smtp = getsmtpgmail();
            string from = "noreply@gmail.com";
            //  DataTable dt = null;// _db.GetInfo_EmailTemplates(3);

            string body = "";
            string subject = "Mật  khẩu mới của bạn ";
            body += "Chào mừng bạn đến với chúng tôi.<br/> Thông tin của bạn:<br/>";
            body += "Số hồ sơ: " + username + "<br/>Mật khẩu: " + mk + @"<br/> Bạn vui lòng đăng nhập vào website:" + linkSite + " cập nhật thông tin!";

            //DataRow row = _db.Get_Info_Emailtemplete("quen_mat_khau");
            //if (row != null)
            //{
            //    subject = BaseView.GetStringFieldValue(row, "subject");
            //    body = BaseView.GetStringFieldValue(row, "body");
            //    body = body.Replace("@sohoso", username);
            //    body = body.Replace("@matkhau", mk);
            //}
            using (MailMessage msg = new MailMessage())
            {
                try
                {
                    MailAddress source = new MailAddress(from, sitename);
                    MailAddress recipient = new MailAddress(email);

                    msg.From = source;
                    msg.To.Add(recipient);
                    msg.Subject = subject;
                    msg.Body = body;

                    msg.IsBodyHtml = true;
                    smtp.Send(msg);
                }
                catch (Exception) { }
            }

        }
        public static void sendYourmail(string email, string name, string sdt)
        {
            var smtp = getsmtpgmail();
            string from = "noreply@gmail.com";
            //  DataTable dt = null;// _db.GetInfo_EmailTemplates(3);

            string body = "";
            string subject = "Có 1 đơn đặt hẹn mới ";
            body = "Có 1 đơn đặt hẹn mới <br/>Khách hàng: " + name + "<br/> Số điện thoại: " + sdt + "<br/> Vui lòng kiểm tra đơn hàng " + linkSite + "/adminnktk/";
            using (MailMessage msg = new MailMessage())
            {
                try
                {
                    MailAddress source = new MailAddress(from, sitename);
                    MailAddress recipient = new MailAddress(email);

                    msg.From = source;
                    msg.To.Add(recipient);
                    msg.Subject = subject;
                    msg.Body = body;

                    msg.IsBodyHtml = true;
                    smtp.Send(msg);
                }
                catch (Exception) { }
            }

        }
        public static void Registermail(string email, string name, string username, string mk)
        {
            var smtp = getsmtpgmail();
            string from = "noreply@gmail.com";


            string body = "";
            string subject = "Bạn đã đăng ký thành công ";
            body += "Chào mừng bạn đến với thammydiamond.<br/> Thông tin của bạn:<br/>";
            body += "Số hồ sơ: " + username + "<br/>Mật khẩu: " + mk + @"<br/> Bạn vui lòng đăng nhập vào website: " + linkSite + " cập nhật thông tin!";
            //DataRow row = _db.Get_Info_Emailtemplete("dang_ky");
            //if (row != null)
            //{
            //    subject = BaseView.GetStringFieldValue(row, "subject");
            //    body = BaseView.GetStringFieldValue(row, "body");
            //    body = body.Replace("@sohoso", username);
            //    body = body.Replace("@matkhau", mk);
            //}
            using (MailMessage msg = new MailMessage())
            {
                try
                {
                    MailAddress source = new MailAddress(from, sitename);
                    MailAddress recipient = new MailAddress(email);

                    msg.From = source;
                    msg.To.Add(recipient);
                    msg.Subject = subject;
                    msg.Body = body;

                    msg.IsBodyHtml = true;
                    smtp.Send(msg);
                }
                catch (Exception) { }
            }

        }
        public static void registration(string email, string name, string webcode)
        {
            var smtp = getsmtpgmail();

            string from = "noreply@gmail.com";


            string body = "Để hoàn tất đăng ký, bạn vui lòng xác nhận: " + webcode + "";
            string subject = "Xác nhận tài khoản - Code Project";

            using (MailMessage msg = new MailMessage())
            {
                try
                {
                    MailAddress source = new MailAddress(from, sitename);
                    MailAddress recipient = new MailAddress(email);

                    msg.From = source;
                    msg.To.Add(recipient);
                    msg.Subject = subject;
                    msg.Body = body;

                    msg.IsBodyHtml = true;
                    smtp.Send(msg);
                }
                catch (Exception) { }
            }
        }

        public static void forgotusername(string email, string name, string username)
        {
            var smtp = getsmtpgmail();

            string from = "noreply@gmail.com";

            DataTable dt = null;// _db.GetInfo_EmailTemplates(3);

            string body = "";
            string subject = "Forgot Username";
            if (dt.Rows.Count > 0)
            {
                //body = BaseView.GetStringFieldValue(dt.Rows[0], "Body");
                //body = body.Replace("@name", name);
                //body = body.Replace("@username", username);
                //subject = BaseView.GetStringFieldValue(dt.Rows[0], "Subject");
            }

            using (MailMessage msg = new MailMessage())
            {
                try
                {
                    MailAddress source = new MailAddress(from, sitename);
                    MailAddress recipient = new MailAddress(email);

                    msg.From = source;
                    msg.To.Add(recipient);
                    msg.Subject = subject;
                    msg.Body = body;

                    msg.IsBodyHtml = true;
                    smtp.Send(msg);
                }
                catch (Exception) { }
            }

        }

        public static void forgotpassword(string email, string name, string passhint)
        {
            var smtp = getsmtpgmail();

            string from = "noreply@gmail.com";
            //string from = "notification@mint-corp.com";

            DataTable dt = null;// _db.GetInfo_EmailTemplates(2);
            string body = "";
            string subject = "Forgot Password";
            if (dt.Rows.Count > 0)
            {
                //    body = BaseView.GetStringFieldValue(dt.Rows[0], "Body");
                //    body = body.Replace("@name", name);
                //    body = body.Replace("@passhint", passhint);
                //    subject = BaseView.GetStringFieldValue(dt.Rows[0], "Subject");
            }

            using (MailMessage msg = new MailMessage())
            {
                try
                {
                    MailAddress source = new MailAddress(from, sitename);
                    MailAddress recipient = new MailAddress(email);

                    msg.From = source;
                    msg.To.Add(recipient);
                    msg.Subject = subject;
                    msg.Body = body;

                    msg.IsBodyHtml = true;
                    smtp.Send(msg);
                }
                catch (Exception) { }
            }

        }

        public static void invitenewmember(string email, string code, string name)
        {
            var smtp = getsmtpgmail();

            string from = "noreply@gmail.com";

            DataTable dt = null;// _db.GetInfo_EmailTemplates(4);
            string body = "";
            string subject = "Invite New Member";
            if (dt.Rows.Count > 0)
            {
                //body = BaseView.GetStringFieldValue(dt.Rows[0], "Body");
                //body = body.Replace("@email", email);
                //body = body.Replace("@code", code);
                //body = body.Replace("@name", name);
                //subject = BaseView.GetStringFieldValue(dt.Rows[0], "Subject");
            }

            using (MailMessage msg = new MailMessage())
            {
                try
                {
                    MailAddress source = new MailAddress(from, sitename);
                    MailAddress recipient = new MailAddress(email);

                    msg.From = source;
                    msg.To.Add(recipient);
                    msg.Subject = subject;
                    msg.Body = body;

                    msg.IsBodyHtml = true;
                    smtp.Send(msg);
                }
                catch (Exception) { }
            }

        }
        public static void BirthDayMail(string email, string hoten)
        {
            var smtp = getsmtpgmail();
            string from = "noreply@gmail.com";


            string body = "";
            string subject = " ";
            body += "";
            body += "";
            //DataRow row = _db.Get_Info_Emailtemplete("sinh_nhat");
            //if (row != null)
            //{
            //    subject = BaseView.GetStringFieldValue(row, "subject");
            //    body = BaseView.GetStringFieldValue(row, "body");
            //    body = body.Replace("@hoten", hoten);
            //}
            using (MailMessage msg = new MailMessage())
            {
                try
                {
                    MailAddress source = new MailAddress(from, sitename);
                    MailAddress recipient = new MailAddress(email);

                    msg.From = source;
                    msg.To.Add(recipient);
                    msg.Subject = subject;
                    msg.Body = body;

                    msg.IsBodyHtml = true;
                    smtp.Send(msg);
                }
                catch (Exception) { }
            }

        }
        public static void SixMonthMail(string email, string hoten)
        {
            var smtp = getsmtpgmail();
            string from = "noreply@gmail.com";


            string body = "";
            string subject = " ";
            body += "";
            body += "";
            //DataRow row = _db.Get_Info_Emailtemplete("dieu_tri_sau_thang");
            //if (row != null)
            //{
            //    subject = BaseView.GetStringFieldValue(row, "subject");
            //    body = BaseView.GetStringFieldValue(row, "body");
            //    body = body.Replace("@hoten", hoten);
            //}
            using (MailMessage msg = new MailMessage())
            {
                try
                {
                    MailAddress source = new MailAddress(from, sitename);
                    MailAddress recipient = new MailAddress(email);

                    msg.From = source;
                    msg.To.Add(recipient);
                    msg.Subject = subject;
                    msg.Body = body;

                    msg.IsBodyHtml = true;
                    smtp.Send(msg);
                }
                catch (Exception) { }
            }

        }
        public static int postdefaultEmailBlogger(string email, string title, string body)
        {
            var smtp = getsmtpgmail();
            string from = "noreply@gmail.com";
            using (MailMessage msg = new MailMessage())
            {
                try
                {

                    title = BaseView.ReplaceBlog(title);
                    body = BaseView.ReplaceBlog(body);

                    MailAddress source = new MailAddress(from, sitename);
                    MailAddress recipient = new MailAddress(email);

                    msg.From = source;
                    msg.To.Add(recipient);
                    msg.Subject = title;
                    msg.Body = body;

                    msg.IsBodyHtml = true;
                    smtp.Send(msg);
                    return 1;
                }
                catch (Exception) { return 0; }
            }

        }//

        public static int postBlogger(string emailAdmin, string passAdmin, string email, string title, string body)
        {
            var smtp = getsmtpgmail(emailAdmin, passAdmin);
            string from = "noreply@gmail.com";
            using (MailMessage msg = new MailMessage())
            {
                try
                {

                    title = BaseView.ReplaceBlog(title);
                    body = BaseView.ReplaceBlog(body);

                    MailAddress source = new MailAddress(from, sitename);
                    MailAddress recipient = new MailAddress(email);

                    msg.From = source;
                    msg.To.Add(recipient);
                    msg.Subject = title;
                    msg.Body = body;

                    msg.IsBodyHtml = true;
                    smtp.Send(msg);
                    return 1;
                }
                catch (Exception) { return 0; }
            }

        }//
    }

}