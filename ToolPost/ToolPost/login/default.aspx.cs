using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ToolPost.lib;

namespace ToolPost.login
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int limit = Request.Cookies.Count; //Get the number of cookies and 
                                               //use that as the limit.
            HttpCookie aCookie;   //Instantiate a cookie placeholder
            string cookieName;

            //Loop through the cookies
            for (int i = 0; i < limit; i++)
            {
                cookieName = Request.Cookies[i].Name;    //get the name of the current cookie
                aCookie = new HttpCookie(cookieName);    //create a new cookie with the same
                                                         // name as the one you're deleting
                aCookie.Value = "";    //set a blank value to the cookie 
                aCookie.Expires = DateTime.Now.AddDays(-1);    //Setting the expiration date
                                                               //in the past deletes the cookie

                Response.Cookies.Add(aCookie);    //Set the cookie to delete it.
            }
            //ltIp.Text = getLocalV4Ip();
        }
        DBClass _db = new DBClass();

        public int Login(string user, string pass)
        {


            DataRow row = _db.Admin_Login(user, BaseView.md5(user + pass), "");
            if (row != null)
            {
                WriteCookie("accountUserName", user);
                WriteCookie("accountUserPass", BaseView.md5(user + pass));
                return 1;
            }
            else
                return 0;
        }
        protected void WriteCookie(string name, string value)
        {
            //Create a new cookie, passing the name into the constructor
            HttpCookie cookie = new HttpCookie(name);
            cookie.Value = value;
            DateTime dtNow = DateTime.Now;
            TimeSpan tsMinute = new TimeSpan(1, 0, 1, 0);
            cookie.Expires = dtNow + tsMinute;
            Response.Cookies.Add(cookie);

        }

        protected string ReadCookie(string name)
        {
            //Get the cookie name the user entered
            String strCookieName = "username";
            HttpCookie cookie = Request.Cookies[strCookieName];
            if (cookie == null)
            {
                return "";
            }
            else
            {
                //Write the cookie value
                String strCookieValue = cookie.Value.ToString();
                return strCookieValue;
            }
        }
        protected void btnDangNhap_Click(object sender, EventArgs e)
        {
            if (Login(txtUsername.Text.Trim(), txtPassword.Text.Trim()) == 1)
            {
                Response.Redirect("~/");
            }
        }
    }
}