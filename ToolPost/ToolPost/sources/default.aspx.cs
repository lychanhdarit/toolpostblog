using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ToolPost.lib;

namespace ToolPost.sources
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
            string sql = "select * from TagPage  where  username = '" + username + "'";
            if (isAdmin() == true)
                sql = "select * from TagPage ";
            grvTaskNew.DataSource = _db.sqlGetData(sql);
            grvTaskNew.DataBind();

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
            string sql = "select * from TagPage where PageName like '%" + txtSearch.Text + "%' ";
            if (BaseView.GetBooleanFieldValue(rUser, "isAdmin") != true)
            {
                sql = "select * from TagPage where PageName like '%" + txtSearch.Text + "%' and username = '" + username + "'";
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
                    _db.OT_IU_tagPages(ToSQL.SQLToInt(chk.CssClass), "", "", "", "", "", "", "del");
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
            string sql = "select * from TagPage where Id = " + id + "";
            DataRow info = _db.sqlGetDataRow(sql);
            if (info != null)
            {
                txtID.Text = BaseView.GetStringFieldValue(info, "Id");
                txtTieuDe.Text = BaseView.GetStringFieldValue(info, "Pagename");
                txtTagTitle.Text = BaseView.GetStringFieldValue(info, "TagTitle");
                txtTagContent.Text = BaseView.GetStringFieldValue(info, "TagContent");

                btnSua.Text = "Sửa";
                btnSua.Visible = true;
                btnAdd.Visible = false;
            }
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
            try
            {
                DBClass _db = new DBClass();
                string username = ReadCookie("accountUserName");
                _db.OT_IU_tagPages(0, txtTieuDe.Text, "", txtTagTitle.Text, "", txtTagContent.Text, username, "insert");
                getDataSpiner();
                //btnSua.Visible = true;
                //btnAdd.Visible = false;
                ltE.Text = "Đã thêm hoàn tất.";
            }
            catch
            {
                ltE.Text = "Đã có lỗi, liên hệ admin";
            }


        }
        protected void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                DBClass _db = new DBClass();
                string username = ReadCookie("accountUserName");
                _db.OT_IU_tagPages(ToSQL.SQLToInt(txtID.Text), txtTieuDe.Text, "", txtTagTitle.Text, "", txtTagContent.Text, username, "update");
                getDataSpiner();
                txtID.Text = "";
                txtTieuDe.Text = "";
                btnSua.Visible = false;
                btnAdd.Visible = true;
                ltE.Text = "Đã sửa hoàn tất.";
            }
            catch
            {
                ltE.Text = "Đã có lỗi, liên hệ admin";
            }


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