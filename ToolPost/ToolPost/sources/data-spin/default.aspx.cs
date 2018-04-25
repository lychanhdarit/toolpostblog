using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ToolPost.lib;

namespace ToolPost.sources.data_spin
{
    public partial class _default : System.Web.UI.Page
    {
        DBClass _db = new DBClass();
    protected void Page_Load(object sender, EventArgs e)
        {
            DBClass _db = new DBClass();
            string username = ReadCookie("accountUserName");
            DataRow rUser = _db.get_Info_user_cms(username);

            if (!IsPostBack)
            {


                getDataSpiner();
            }
        }
        private void getDataSpiner()
        {
            //---------------------------------------
            DBClass _db = new DBClass();
            string username = ReadCookie("accountUserName");
            string sql = "select * from codeproi_name.DataSpin  where  username = '" + username + "'";
            if (isAdmin() == true)
                sql = "select * from codeproi_name.DataSpin ";
            grvTaskNew.DataSource = _db.sqlGetData(sql);
            grvTaskNew.DataBind();

        }
        private void PostBlog(string url, string blogID, string emailBlog, string tieude, string noidung)
        {
            DBClass _db = new DBClass();
            //if (_db.Get_PostURL(null, url, blogID).Rows.Count == 0)
            //{
            MailDaemon.postdefaultEmailBlogger(emailBlog, tieude, noidung);
            _db.insert_update_postURL(0, tieude, url, blogID, "insert");
            ltThongBaoDD.Text = "Đã đăng bài này";
            //}
            //else
            //    ltThongBaoDD.Text = "Bìa này đã có trên blog.";
        }



        private bool isAdmin()
        {
            HttpCookie cookie = Request.Cookies["accountUserName"];
            if (cookie == null)
            {
                Response.Redirect("~/login/");
            }
            DataRow rUser = _db.get_Info_user_cms(ReadCookie("accountUserName"));
            //DataTable dt = null;
            if (BaseView.GetBooleanFieldValue(rUser, "isAdmin") != true)
            {
                return false;
            }
            return true;
        }
        private string ReadCookie(string name)
        {
            //Get the cookie name the user entered
            String strCookieName = name;
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
        private string GridViewSortDirection
        {
            get { return ViewState["SortDirection"] as string ?? "DESC"; }
            set { ViewState["SortDirection"] = value; }
        }
        protected void grvTaskNew_Sorting(object sender, GridViewSortEventArgs e)
        {
            DataTable dt = getDataTable(null);

            if (dt != null)
            {
                grvTaskNew.AllowPaging = false;
                DataView dataView = new DataView(dt);
                dataView.Sort = e.SortExpression + " " + ConvertSortDirectionToSql(e.SortDirection);

                grvTaskNew.DataSource = dataView;
                grvTaskNew.DataBind();
            }
        }
        private string ConvertSortDirectionToSql(SortDirection sortDirection)
        {
            switch (GridViewSortDirection)
            {
                case "ASC":
                    GridViewSortDirection = "DESC";
                    break;

                case "DESC":
                    GridViewSortDirection = "ASC";
                    break;
            }

            return GridViewSortDirection;
        }

        private DataTable getDataUser(string username, string search)
        {
            DataRow rUser = _db.get_Info_user_cms(username);
            string sql = "select * from codeproi_name.DataSpin where name like '%" + txtSearch.Text + "%' ";
            if (BaseView.GetBooleanFieldValue(rUser, "isAdmin") != true)
            {
                sql = "select * from codeproi_name.DataSpin where name like '%" + txtSearch.Text + "%' and username = '" + username + "'";
            }
            return _db.sqlGetData(sql);
        }
        private DataTable getDataTable(bool? isactived)
        {
            string username = ReadCookie("accountUserName");
            if (!String.IsNullOrEmpty(Request.QueryString["userInfo"]))
            {
                username = Request.QueryString["userInfo"];
            }
            return getDataUser(username, txtSearch.Text);
        }
        private void getData(int index, bool? isactived)
        {
            grvTaskNew.PageIndex = index;
            grvTaskNew.DataSource = getDataTable(isactived);
            grvTaskNew.DataBind();
        }
        private void getDataForUser(int index)
        {
            if (isAdmin() == true)
                getData(index, null);
            else
                getData(index, true);
        }
        protected void grvTaskNew_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            getDataForUser(e.NewPageIndex);
        }
        protected void ddlLoai_SelectedIndexChanged(object sender, EventArgs e)
        {
            getDataForUser(0);
        }

        protected void btnXoa_Click(object sender, EventArgs e)
        {
            string username = ReadCookie("accountUserName");
            for (int i = 0; i < grvTaskNew.Rows.Count; i++)
            {
                CheckBox chk = (CheckBox)grvTaskNew.Rows[i].FindControl("chk");
                if (chk.Checked == true)
                {
                    _db.sqlSetData("delete from codeproi_name.DataSpin where Id = " + chk.CssClass + "");
                }
            }
            getDataForUser(0);
        }
        protected void grvTaskNew_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                for (int i = 1; i < grvTaskNew.Columns.Count - 1; i++)
                {
                    e.Row.Cells[i].Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(grvTaskNew, "Select$" + e.Row.RowIndex);
                    e.Row.Cells[i].ToolTip = "Nhấn vào đây để chọn ";

                }

            }
        }
        private void getItem(string id)
        {
            string sql = "select * from codeproi_name.DataSpin where Id = " + id + "";
            DataRow info = _db.sqlGetDataRow(sql);
            if (info != null)
            {
                txtID.Text = BaseView.GetStringFieldValue(info, "Id");
                txtTieuDe.Text = BaseView.GetStringFieldValue(info, "name");
                string filename = BaseView.GetStringFieldValue(info, "filename");
                txtFile.Text = filename;
                txtContent.Text = readTextFile(filename);

                btnSua.Text = "Sửa";
                btnSua.Visible = true;
                btnAdd.Visible = false;
            }
        }
        protected void btnDuyet_Click(object sender, EventArgs e)
        {
            string username = ReadCookie("accountUserName");
            for (int i = 0; i < grvTaskNew.Rows.Count; i++)
            {
                CheckBox chk = (CheckBox)grvTaskNew.Rows[i].FindControl("chk");
                HyperLink lbTieuDe = (HyperLink)grvTaskNew.Rows[i].FindControl("lbTieuDe");
                if (chk.Checked == true)
                {
                    _db.sqlSetData("update codeproi_name.DataSpin set isactived = 1  where id = '" + chk.CssClass + "'");
                }
            }
            getDataForUser(0);
        }
        protected void btnChuaDuyet_Click(object sender, EventArgs e)
        {
            getData(0, false);
        }

        protected void btnTim_Click(object sender, EventArgs e)
        {
            getDataForUser(0);
        }



        protected void grvTaskNew_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string username = ReadCookie("accountUserName");
            string id = e.CommandArgument.ToString();

            if (e.CommandName == "xem")
            {
                getItem(id);

            }
        }
        private string[] readLine(TextBox txtMulti)
        {
            string[] delimiter = { Environment.NewLine };
            return txtMulti.Text.Split(delimiter, StringSplitOptions.None);
        }
        private int writeTextFile(string filename, string content)
        {
            try
            {
                string fullpath = Server.MapPath("~/data-spin/" + filename);
                File.WriteAllText(fullpath, String.Empty);
                TextWriter tw = new StreamWriter(fullpath, true);

                tw.WriteLine(content);
                tw.Close();
                return 1;
            }
            catch { return 0; }
        }
        private string readTextFile(string filename)
        {
            string content = "";
            try
            {
                string path = Server.MapPath("~/data-spin/" + filename);
                using (StreamReader sr = File.OpenText(path))
                {
                    string s = String.Empty;
                    while ((s = sr.ReadLine()) != null)
                    {
                        content += s + "\n";
                    }
                }
            }
            catch { }
            return content;
        }
        private int beginWrite(TextBox Name, TextBox Content)
        {
            DBClass _db = new DBClass();
            string content = "";
            for (int i = 0; i < readLine(Content).Length; i++)
            {
                content += readLine(Content)[i] + "\n";
            }
            string name = Name.Text;
            if (name == "")
                name = "Demo Data";
            string username = ReadCookie("accountUserName");

            //string filename = "data-" + username + "-" + BaseView.convertStringLinks(name) + "-" + DateTime.Now.Millisecond + ".txt";

            //string insertSQL = "insert into DataSpin values('" + name + "','" + filename + "',1,'" + username + "') ";
            //_db.sqlSetData(insertSQL);
            writeTextFile(txtFile.Text, content);
            //Luu Database
            return 1;
        }
        private int beginWriteInsert(TextBox Name, TextBox Content)
        {
            DBClass _db = new DBClass();
            string content = "";
            for (int i = 0; i < readLine(Content).Length; i++)
            {
                content += readLine(Content)[i] + "\n";
            }
            string name = Name.Text;
            if (name == "")
                name = "Demo Data";
            string username = ReadCookie("accountUserName");
            string filename = "data-" + username + "-" + BaseView.convertStringLinks(name) + "-" + DateTime.Now.Millisecond + ".txt";
            string insertSQL = "insert into codeproi_name.DataSpin  values(N'" + name + "','" + filename + "',1,'" + username + "') ";
            _db.sqlSetData(insertSQL);
            writeTextFile(filename, content);
            //Luu Database
            return 1;
        }
        protected void chk_CheckedChanged(object sender, EventArgs e)
        {
            if (chk.Checked == true)
            {
                for (int i = 0; i < grvTaskNew.Rows.Count; i++)
                {
                    CheckBox chkItem = (CheckBox)grvTaskNew.Rows[i].FindControl("chk");
                    chkItem.Checked = true;
                }
            }
            if (chk.Checked == false)
            {
                for (int i = 0; i < grvTaskNew.Rows.Count; i++)
                {
                    CheckBox chkItem = (CheckBox)grvTaskNew.Rows[i].FindControl("chk");
                    chkItem.Checked = false;
                }
            }
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            beginWriteInsert(txtTieuDe, txtContent);
            getDataSpiner();
            ltE.Text = "Đã sửa hoàn tất.";
        }
        protected void btnSua_Click(object sender, EventArgs e)
        {
            beginWrite(txtTieuDe, txtContent);
            getDataSpiner();
            ltE.Text = "Đã sửa hoàn tất.";
            btnSua.Visible = false;
            btnAdd.Visible = true;
        }
        protected void grvTaskNew_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in grvTaskNew.Rows)
            {
                if (row.RowIndex == grvTaskNew.SelectedIndex)
                {
                    row.BackColor = ColorTranslator.FromHtml("#fcc075");
                    row.ToolTip = string.Empty;
                    getItem(grvTaskNew.SelectedValue.ToString());
                    lbError.Text = "";
                }
                else
                {
                    //row.BackColor = ColorTranslator.FromHtml("#EFFCDB");
                    row.ToolTip = "Nhấn vào đây để chọn";
                }
            }
        }
    }
}