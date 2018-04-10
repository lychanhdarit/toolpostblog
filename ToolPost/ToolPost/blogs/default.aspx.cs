using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ToolPost.lib;

namespace ToolPost.blogs
{
    public partial class _default : System.Web.UI.Page
    {
        DBClass _db = new DBClass();
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpCookie cookie = Request.Cookies["accountUserName"];
            if (cookie == null)
            {
                Response.Redirect("~/login/");
            }
            if (!IsPostBack)
            {
                getDataForUser(0);
                getEmail();
                getDM();
            }
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



        private DataTable getDataTable(bool? isactived)
        {
            string username = ReadCookie("accountUserName");

            string sql = "select * from Blogger where NameBlog like N'%" + txtSearch.Text + "%' and username = '" + username + "'";
            //if (isAdmin() == true)
            //{
            //    sql = "select * from Blogger where NameBlog like N'%" + txtSearch.Text + "%'";
            //}
            return _db.sqlGetData(sql);// _db.get_data_news(txtSearch.Text, ToSQL.SQLToInt(ddlLoai.SelectedValue), isactived);
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
                //HyperLink lbTieuDe = (HyperLink)grvTaskNew.Rows[i].FindControl("lbTieuDe");
                if (chk.Checked == true)
                {
                    _db.sqlSetData("delete from Blogger where idBlog = '" + chk.CssClass + "'");
                }
            }
            getDataForUser(0);
        }
        protected void grvTaskNew_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                for (int i = 1; i < grvTaskNew.Columns.Count - 2; i++)
                {
                    e.Row.Cells[i].Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(grvTaskNew, "Select$" + e.Row.RowIndex);
                    e.Row.Cells[i].ToolTip = "Nhấn vào đây để chọn ";

                }

            }
        }
        private void getDM()
        {
            ddlDanhmuc.DataSource = _db.sqlGetData("select * from BloggerCat where username = '" + ReadCookie("accountUserName") + "'");
            ddlDanhmuc.DataBind();
            ddlDanhmuc.Items.Insert(0, new ListItem("-- chọn danh mục --", "0"));
        }
        private void getItem(string id)
        {
            getEmail();
            string sql = "select * from Blogger where IdBlog = '" + id + "'";
            DataRow info = _db.sqlGetDataRow(sql);
            if (info != null)
            {
                txtID.Text = BaseView.GetStringFieldValue(info, "IdBlog");
                txtTieuDe.Text = BaseView.GetStringFieldValue(info, "NameBlog");
                txtEmailSecrect.Text = BaseView.GetStringFieldValue(info, "EmailSecrect");
                chkActived.Checked = BaseView.GetBooleanFieldValue(info, "isActived");
                try
                {
                    ddlDanhmuc.SelectedValue = BaseView.GetStringFieldValue(info, "idC");
                }
                catch (Exception)
                {

                }
                try
                {
                    ddlEmail.SelectedValue = BaseView.GetStringFieldValue(info, "gmail");
                }
                catch (Exception)
                {

                }
                btnAdd.Text = "Sửa";
            }
        }
        protected void btnDuyet_Click(object sender, EventArgs e)
        {
            string username = ReadCookie("accountUserName");
            for (int i = 0; i < grvTaskNew.Rows.Count; i++)
            {
                CheckBox chk = (CheckBox)grvTaskNew.Rows[i].FindControl("chk");
                //HyperLink lbTieuDe = (HyperLink)grvTaskNew.Rows[i].FindControl("lbTieuDe");
                if (chk.Checked == true)
                {
                    _db.sqlSetData("update Blogger set isactived = 1  where idBlog = '" + chk.CssClass + "'");
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

        protected void btnAn_Click(object sender, EventArgs e)
        {
            string username = ReadCookie("accountUserName");
            for (int i = 0; i < grvTaskNew.Rows.Count; i++)
            {
                CheckBox chk = (CheckBox)grvTaskNew.Rows[i].FindControl("chk");
                //HyperLink lbTieuDe = (HyperLink)grvTaskNew.Rows[i].FindControl("lbTieuDe");
                if (chk.Checked == true)
                {
                    _db.sqlSetData("update Blogger set isactived = 0  where idBlog = '" + chk.CssClass + "'");
                }
            }
            getDataForUser(0);
        }

        protected void grvTaskNew_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string username = ReadCookie("accountUserName");
            string id = e.CommandArgument.ToString();
            if (e.CommandName == "actived" || e.CommandName == "del-actived")
            {

                int isActived = e.CommandName == "actived" ? 1 : 0;
                _db.sqlSetData("update Blogger set isactived = " + isActived + "  where idBlog = '" + id + "'");
                getDataForUser(0);
            }
            if (e.CommandName == "del")
            {
                _db.sqlSetData("delete from Blogger where idBlog = '" + id + "'");
                getDataForUser(0);
            }

        }
        protected void chk_CheckedChanged(object sender, EventArgs e)
        {
            if (check.Checked == true)
            {
                for (int i = 0; i < grvTaskNew.Rows.Count; i++)
                {
                    CheckBox chkItem = (CheckBox)grvTaskNew.Rows[i].FindControl("chk");
                    chkItem.Checked = true;
                }
            }
            if (check.Checked == false)
            {
                for (int i = 0; i < grvTaskNew.Rows.Count; i++)
                {
                    CheckBox chkItem = (CheckBox)grvTaskNew.Rows[i].FindControl("chk");
                    chkItem.Checked = false;
                }
            }
        }

        private void getEmail()
        {
            ddlEmail.DataSource = _db.OT_GET_EMAIL_DROP(ReadCookie("accountUserName"));
            ddlEmail.DataBind();
            ddlEmail.Items.Insert(0, new ListItem("-- chọn email --", "0"));
        }
        private void btnConfig_Click(string blogID)
        {
            string username = ReadCookie("accountUserName");
            string sql = "select * from OT_SET_BLOG_EMAIL where blog = '" + blogID + "'";
            DataRow info = _db.sqlGetDataRow(sql);
            if (info != null)
            {
                _db.OT_IU_Email_Blog(ddlEmail.SelectedValue, blogID, true, username, "update");
            }
            else
                _db.OT_IU_Email_Blog(ddlEmail.SelectedValue, blogID, true, username, "insert");
            getDataForUser(0);
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string username = ReadCookie("accountUserName");
            string sql = "select * from Blogger where IdBlog = '" + txtID.Text + "'";
            DataRow info = _db.sqlGetDataRow(sql);
            if (info != null)
            {
                _db.sqlSetData("update Blogger set NameBlog = N'" + txtTieuDe.Text + "' , EmailSecrect = '" + txtEmailSecrect.Text.Replace(" ", "") + "' , isactived = " + (chkActived.Checked == true ? "1" : "0") + ",idC = " + ToSQL.SQLToInt(ddlDanhmuc.SelectedValue) + ",Gmail='" + ddlEmail.SelectedValue + "'  where idBlog = '" + txtID.Text + "'");
                btnAdd.Text = "Thêm mới";
                txtID.Text = "";
                txtTieuDe.Text = "";
                txtEmailSecrect.Text = "";
                getEmail();
                //btnConfig_Click(txtID.Text);
            }
            else
            {
                _db.sqlSetData("Insert Into Blogger values('" + txtID.Text + "', N'" + txtTieuDe.Text + "','" + txtEmailSecrect.Text.Replace(" ", "") + "','" + username + "'," + (chkActived.Checked == true ? "1" : "0") + "," + ToSQL.SQLToInt(ddlDanhmuc.SelectedValue) + ",'" + ddlEmail.SelectedValue + "')");
                //btnConfig_Click(txtID.Text);
                getEmail();
            }
            getDataForUser(0);
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