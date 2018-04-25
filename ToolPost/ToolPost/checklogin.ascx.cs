using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ToolPost
{
    public partial class checklogin : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpCookie cookie = Request.Cookies["accountUserName"];
            if (cookie == null)
            {
                Response.Redirect("~/login/");
            }
        }
    }
}