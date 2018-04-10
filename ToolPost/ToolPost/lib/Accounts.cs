using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ToolPost.lib
{
    public class Accounts
    {
        public Accounts()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public string username { get; set; }
        public string fullname { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public bool active { get; set; }
        public string password { get; set; }
        public bool admin { get; set; }
        public string imageUrl { get; set; }
        public string userCreate { get; set; }
        public string userEdit { get; set; }
        public string IP { get; set; }

        public Accounts(string _username, string _fullname, string _phone, string _email, bool _active, string _pass, bool _admin, string _images, string _userCreate, string _userEdit, string _IP)
        {
            username = _username;
            fullname = _fullname;
            phone = _phone;
            email = _email;
            active = _active;
            password = _pass;
            admin = _admin;
            imageUrl = _images;
            userCreate = _userCreate;
            userEdit = _userEdit;
            IP = _IP;
        }
        public Accounts(string _username)
        {
            username = _username;
        }
        public Accounts(string _username, string _pass)
        {
            username = _username;
            password = _pass;
        }
    }
}