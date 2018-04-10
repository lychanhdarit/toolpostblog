using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace ToolPost.lib
{
    public class BaseServ
    {
        protected static string dbConnString;
        public BaseServ()
        {
            dbConnString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        }
    }
}