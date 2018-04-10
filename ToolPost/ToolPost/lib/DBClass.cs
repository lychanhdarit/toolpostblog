using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ToolPost.lib
{
    public class DBClass : BaseServ
    
    {
        int result;
        public DBClass()
        {
            result = 0;
        }

        #region sqlHelp
        public void ImporttoSQL(string sPath, string sqlfromExcel, string toTable)
        {
            string sSourceConstr = string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 12.0 Xml;HDR=YES;""", sPath);

            string sDestConstr = dbConnString;
            OleDbConnection sSourceConnection = new OleDbConnection(sSourceConstr);
            using (sSourceConnection)
            {
                // string sql = string.Format("Select [Employee ID],[Employee Name],[Designation],[Posting],[Dept] FROM [{0}]", "Sheet1$");
                OleDbCommand command = new OleDbCommand(sqlfromExcel, sSourceConnection);
                sSourceConnection.Open();
                using (OleDbDataReader dr = command.ExecuteReader())
                {
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(sDestConstr))
                    {
                        bulkCopy.DestinationTableName = toTable;
                        //You can mannualy set the column mapping by the following way.
                        //bulkCopy.ColumnMappings.Add("Employee ID", "Employee Code");
                        bulkCopy.WriteToServer(dr);
                    }
                }
            }
        }
        public DataTable sqlGetData(string sqlCommand)
        {
            try
            {
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                SqlConnection con = new SqlConnection(dbConnString);
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand, con);
                adapter.Fill(ds);
                dt = ds.Tables[0];
                con.Close();
                return dt;
            }
            catch (Exception) { return null; }
        }
        public DataRow sqlGetDataRow(string sqlCommand)
        {
            try
            {
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                SqlConnection con = new SqlConnection(dbConnString);
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand, con);
                adapter.Fill(ds);
                dt = ds.Tables[0];
                con.Close();
                if (dt.Rows.Count > 0)
                    return dt.Rows[0];
                return null;
            }
            catch (Exception) { return null; }
        }
        public int sqlSetData(string sqlCommand)
        {
            //try
            //{
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(dbConnString);
            con.Open();
            SqlCommand cmd = new SqlCommand(sqlCommand, con);
            cmd.ExecuteNonQuery();
            con.Close();
            return 1;
            //}
            //catch (Exception) { return 0; }
        }
        public virtual object ExecuteScalar(string ProcedureName, params object[] Parameters)
        {
            return SqlHelper.ExecuteScalar(dbConnString, ProcedureName, Parameters);
        }

        public virtual int ExecuteNonQuery(string ProcedureName, params object[] Parameters)
        {
            return SqlHelper.ExecuteNonQuery(dbConnString, ProcedureName, Parameters);

        }
        public virtual DataRow GetDataRow(string ProcedureName, params object[] Parameters)
        {
            return GetDataRow(0, ProcedureName, Parameters);
        }
        public virtual DataRow GetDataRow(int RowIndex, string ProcedureName, params object[] Parameters)
        {
            DataTable dt = GetDataTable(0, ProcedureName, Parameters);
            DataRow dr = null;

            if (dt != null)
            {
                if (RowIndex >= 0 && RowIndex < dt.Rows.Count)
                {
                    dr = dt.Rows[RowIndex];
                }
                dt.Dispose();
            }
            return dr;
        }
        public static DataTable GetDataTable(int TableIndex, string ProcedureName, params object[] Parameters)
        {
            DataSet ds = GetDataSet(ProcedureName, Parameters);
            DataTable dt = null;
            if (ds != null && ds.Tables.Count > 0)
            {
                if (TableIndex >= 0 && TableIndex < ds.Tables.Count)
                    dt = ds.Tables[TableIndex];

                ds.Dispose();
            }
            return dt;
        }

        public static DataTable GetDataTable(string ProcedureName, params object[] Parameters)
        {
            DataSet ds = GetDataSet(ProcedureName, Parameters);
            DataTable dt = null;
            if (ds != null && ds.Tables.Count > 0)
            {
                dt = ds.Tables[0];
                ds.Dispose();
            }
            return dt;
        }

        public static DataSet GetDataSet(string ProcedureName, params object[] Parameters)
        {
            //try
            //{
            return SqlHelper.ExecuteDataset(dbConnString, ProcedureName, Parameters);
            //}
            //catch
            //{
            //    return SqlHelper.ExecuteDataset(dbConnString1, ProcedureName, Parameters);
            //}
        }
        public object ExecuteScalarSQL(string sql)
        {
            return SqlHelper.ExecuteScalar(dbConnString, CommandType.Text, sql);
        }
        public object ExecuteNonQuerySQL(string sql)
        {
            return SqlHelper.ExecuteNonQuery(dbConnString, CommandType.Text, sql);
        }

        #endregion

        #region News
        public DataRow Get_Info_News_url(string url)
        {
            return GetDataRow("[get_info_news_url]", url);
        }
        public DataTable Get_All_LoaiTin()
        {
            return GetDataTable("Get_All_LoaiTin");
        }
        public DataTable get_data_LoaiTin(string name, int maloai, bool? isActived)
        {
            return GetDataTable("get_data_LoaiTin", name, maloai, isActived);
        }
        public DataTable get_all_LoaiTin_UrlNotNull_RV()
        {
            return GetDataTable("get_all_LoaiTin_UrlNotNull_RV");
        }
        public DataTable get_all_LoaiTin_UrlNotNull()
        {
            return GetDataTable("get_all_LoaiTin_UrlNotNull");
        }
        public DataTable get_all_LoaiTin_idDanhMuc(int idDanhMuc)
        {
            return GetDataTable("get_all_LoaiTin_idDanhMuc", idDanhMuc);
        }
        public DataTable get_menu_LoaiTin(int idPatient, int idDanhMuc)
        {
            return GetDataTable("get_menu_LoaiTin", idPatient, idDanhMuc);
        }
        public DataTable get_menu(int isParent)
        {
            return GetDataTable("[get_menu]", isParent);
        }
        public DataTable updown_menu(int id, int index)
        {
            return GetDataTable("[updown_menu]", id, index);
        }
        public DataTable get_Top_Random_LoaiTin_idDanhMuc(int idDanhMuc, int top)
        {
            return GetDataTable("get_Top_Random_LoaiTin_idDanhMuc", idDanhMuc, top);
        }
        public DataTable get_all_words()
        {
            return GetDataTable("[get_all_words]");
        }
        public DataTable get_top_words(int top)
        {
            return GetDataTable("[get_top_words]", top);
        }

        public DataTable get_all_Tinh()
        {
            return GetDataTable("[get_all_Tinh]");
        }
        public DataTable get_all_Quan(int idTinh)
        {
            return GetDataTable("[get_all_Quan]", idTinh);
        }
        public DataTable get_all_DanhMuc()
        {
            return GetDataTable("[get_all_DanhMucTin]");
        }
        public DataTable get_all_slider(bool? isActived)
        {
            return GetDataTable("[get_all_slider]", isActived);
        }
        public DataRow get_info_slider(int id)
        {
            return GetDataRow("[get_info_slider]", id);
        }
        public DataTable get_all_Menu()
        {
            return GetDataTable("[get_all_menu]");
        }
        public DataTable get_all_Media()
        {
            return GetDataTable("[get_all_Media]");
        }
        public DataTable search_media(string name)
        {
            return GetDataTable("[search_media]", name);
        }
        public DataTable get_all_member()
        {
            return GetDataTable("[get_all_member]");
        }
        public DataTable get_all_admin()
        {
            return GetDataTable("[get_all_admin]");
        }
        public DataTable get_all_media_idMC(int idC, int top)
        {
            return GetDataTable("[get_all_media_idMC]", idC, top);
        }
        public DataTable get_top_Media(int top)
        {
            return GetDataTable("[get_top_Media]", top);
        }
        public DataTable get_all_Media_C()
        {
            return GetDataTable("[get_all_Media_C]");
        }
        public DataTable get_all_DanhMucTin_notNews()
        {
            return GetDataTable("[get_all_DanhMucTin_notNews]");
        }
        public DataTable get_all_DanhMucTin_isNews()
        {
            return GetDataTable("[get_all_DanhMucTin_isNews]");
        }
        public DataTable get_all_Top_LoaiTin(int top)
        {
            return GetDataTable("get_all_Top_LoaiTin", top);
        }
        public DataTable Get_All_News()
        {
            return GetDataTable("Get_All_News");
        }
        public DataTable search_all_news(string keys)
        {
            return GetDataTable("search_all_news", keys);
        }
        public DataTable Get_All_Pages()
        {
            return GetDataTable("Get_All_Pages");
        }
        public DataTable get_data_pages(bool isHome)
        {
            return GetDataTable("get_data_pages", isHome);
        }
        public DataTable get_all_Register()
        {
            return GetDataTable("get_all_Register");
        }
        public DataTable get_all_news_actived(bool isactived)
        {
            return GetDataTable("[get_all_news_actived]", isactived);
        }
        public DataTable get_data_news(string title, int maloai, bool? isactived)
        {
            return GetDataTable("get_data_news", title, maloai, isactived);
        }
        public DataTable get_all_news_user(string user)
        {
            return GetDataTable("get_all_news_user", user);
        }
        public DataTable get_all_gioi_han_tin(string user, DateTime ngaydang)
        {
            return GetDataTable("get_all_gioi_han_tin", user, ngaydang);
        }
        public DataTable get_all_news_hethan(DateTime ngayhethan, string user)
        {
            return GetDataTable("get_all_news_hethan", ngayhethan, user);
        }
        public DataRow Get_Info_News(int id)
        {
            return GetDataRow("Get_Info_News", id);
        }
        public DataRow Get_Info_Pages(int id)
        {
            return GetDataRow("Get_Info_Pages", id);
        }
        public DataRow Get_URL_Pages(string url)
        {
            return GetDataRow("Get_URL_Pages", url);
        }
        public DataRow get_Info_DanhMucTin(int id)
        {
            return GetDataRow("get_Info_DanhMucTin", id);
        }
        public DataRow get_Info_Media(int id)
        {
            return GetDataRow("get_Info_Media", id);
        }
        public DataRow get_Info_Menu(int id)
        {
            return GetDataRow("get_Info_Menu", id);
        }

        public DataRow get_Info_Media_C(int id)
        {
            return GetDataRow("get_Info_Media_C", id);
        }
        public DataRow get_info_caidat()
        {
            return GetDataRow("[get_info_caidat]");
        }
        public DataRow get_Info_DanhMucTin_idNews(int idNews)
        {
            return GetDataRow("get_DanhMuc", idNews);
        }
        public DataRow get_info_Register(int id)
        {
            return GetDataRow("get_info_Register", id);
        }
        public DataRow Get_Info_ID_TT(string id)
        {
            return GetDataRow("[get_info_ID_TT]", id);
        }
        public DataRow get_all_news_url(string url)
        {
            return GetDataRow("[get_all_news_url]", url);
        }
        public DataRow get_info_words(int id)
        {
            return GetDataRow("[get_info_words]", id);
        }
        public DataRow get_info_loai(int id)
        {
            return GetDataRow("get_info_loai", id);
        }
        public DataRow get_info_blog(string id)
        {
            return GetDataRow("get_info_blog", id);
        }
        public DataTable check_info_member(string input, string cmd)
        {
            return GetDataTable("check_info_member", input, cmd);
        }
        public DataRow get_Info_user_login(string id)
        {
            return GetDataRow("get_Info_user_login", id);
        }
        public DataRow get_Info_user_cms(string id)
        {
            return GetDataRow("get_Info_user_cms", id);
        }
        public DataTable get_data_KhuVuc(string id)
        {
            return GetDataTable("get_Info_KhuVuc", id);
        }
        public DataRow get_Info_KhuVuc(string id)
        {
            return GetDataRow("get_Info_KhuVuc", id);
        }
        public DataRow get_info_loai_code(string code)
        {
            return GetDataRow("get_info_loai_code", code);
        }
        public DataRow get_info_Tinh(int id)
        {
            return GetDataRow("get_info_Tinh", id);
        }
        public DataRow get_info_Quan(int id)
        {
            return GetDataRow("get_info_Quan", id);
        }
        public DataRow get_info_url()
        {
            return GetDataRow("[get_info_url]");
        }
        public DataRow Get_Code_News(string id)
        {
            return GetDataRow("Get_Code_News", id);
        }
        public DataRow get_loai_url(string id)
        {
            return GetDataRow("get_loai_url", id);
        }
        public DataTable Get_Tin_LQ(int maLoai)
        {
            return GetDataTable("Get_Tin_LQ", maLoai);
        }
        public DataTable Get_Top_Tin_LQ(int maLoai, int top)
        {
            return GetDataTable("Get_Top_Tin_LQ", maLoai, top);
        }
        public DataTable Get_All_News_IDLoai(int maLoai)
        {
            return GetDataTable("Get_All_News_IDLoai", maLoai);
        }

        public DataTable Search_News(int maLoai, int tinh, int quan, int maDanhMuc)
        {
            return GetDataTable("Search_News", maLoai, tinh, quan, maDanhMuc);
        }
        public DataTable Get_All_News_DanhMuc(int maDanhMuc)
        {
            return GetDataTable("[Get_All_News_DanhMuc]", maDanhMuc);
        }
        public DataTable Get_all_post_actived(int maloai, bool actived)
        {
            return GetDataTable("[Get_all_post_actived]", maloai, actived);
        }
        public DataTable Get_All_News_DanhMuc(int maDanhMuc, int isSEO)
        {
            return GetDataTable("[Get_All_News_DanhMuc_SEO]", maDanhMuc, isSEO);
        }
        public DataTable Get_Rss_News(string code)
        {
            return GetDataTable("Get_Rss_News", code);
        }
        public DataTable Get_Top_News_IDLoai(int maLoai, int top)
        {
            return GetDataTable("Get_Top_News_IDLoai", maLoai, top);
        }

        public DataTable Get_Top_News_CodeLoai(string maLoai, int top)
        {
            return GetDataTable("Get_Top_News_CodeLoai", maLoai, top);
        }
        public DataTable Get_Top_News_CodeLoai(string maLoai, int top, int isSEO)
        {
            return GetDataTable("Get_Top_News_CodeLoai_SEO", maLoai, top, isSEO);
        }
        public DataTable Get_All_News_Loai_SEO(int maLoai, int isSEO)
        {
            return GetDataTable("[Get_All_News_Loai_SEO]", maLoai, isSEO);
        }

        public DataTable Get_Top_News_Tag(string maLoai, int top)
        {
            return GetDataTable("[Get_Top_News_Tag]", maLoai, top);
        }
        public DataTable Get_Top_Pages_Tag(string maLoai, int top)
        {
            return GetDataTable("[Get_Top_Pages_Tag]", maLoai, top);
        }
        public DataTable Get_Top_isNews_CodeLoai(string maLoai, int top)
        {
            return GetDataTable("Get_Top_isNews_CodeLoai", maLoai, top);
        }
        public DataTable Get_Top_News_isNews(int top)
        {
            return GetDataTable("Get_Top_News_isNews", top);
        }
        public DataTable Get_Top_News_notNews(int top)
        {
            return GetDataTable("Get_Top_News_notNews", top);
        }
        public DataTable Get_Radom_News_isNews(int top)
        {
            return GetDataTable("Get_Radom_News_isNews", top);
        }
        public DataRow Login(string username, string mk)
        {
            return GetDataRow("[get_user_login]", username, mk);
        }
        //[get_Info_admin_login]
        public DataRow Admin_Login(string username, string mk, string ip)
        {
            return GetDataRow("[get_admin_login]", username, mk, ip);
        }
        public DataRow check_user_login(string username, string cmd)
        {
            return GetDataRow("[check_user_login]", username, cmd);
        }
        public DataTable get_top_news(int top)
        {
            return GetDataTable("get_top_news", top);
        }
        public DataTable get_top_lienket(int top)
        {
            return GetDataTable("get_top_lienket", top);
        }

        public DataTable get_all_comments_width_post(int idPost, bool? isActived)
        {
            return GetDataTable("[get_all_comments_width_post]", idPost, isActived);
        }
        public DataRow OT_insert_update_email(string email, string pass, bool? isActived, string user, string sqlcommand)
        {
            SqlParameter[] parameters = new SqlParameter[] {
            new SqlParameter("@email", SqlDbType.NVarChar, 500),
            new SqlParameter("@pass", SqlDbType.NVarChar, 500),
            new SqlParameter("@isActived", SqlDbType.Bit),
            new SqlParameter("@user", SqlDbType.NVarChar, 500),
             new SqlParameter("@sqlcommand", SqlDbType.NVarChar, 500),
        };

            parameters[0].Value = email;
            parameters[1].Value = pass;
            parameters[2].Value = isActived;
            parameters[3].Value = user;
            parameters[4].Value = sqlcommand;
            return GetDataRow("[OT_insert_update_email]", parameters);
        }

        public DataRow OT_IU_CONFIG_RSS_BLOG(int id, int blog, string rss, string data, bool? isActived, int time, string sqlcommand)
        {
            SqlParameter[] parameters = new SqlParameter[] {
            new SqlParameter("@id", SqlDbType.Int),
            new SqlParameter("@blog", SqlDbType.Int),
            new SqlParameter("@rss", SqlDbType.NVarChar, 500),
            new SqlParameter("@data", SqlDbType.NVarChar, 500),
            new SqlParameter("@isActived", SqlDbType.Bit),
            new SqlParameter("@time", SqlDbType.Int),
             new SqlParameter("@sqlcommand", SqlDbType.NVarChar, 500),
        };

            parameters[0].Value = id;
            parameters[1].Value = blog;
            parameters[2].Value = rss;
            parameters[3].Value = data;
            parameters[4].Value = isActived;
            parameters[5].Value = time;
            parameters[6].Value = sqlcommand;
            return GetDataRow("[OT_IU_CONFIG_RSS_BLOG]", parameters);
        }

        public DataRow OT_IU_Email_Blog(string email, string blog, bool? isActived, string username, string sqlcommand)
        {
            SqlParameter[] parameters = new SqlParameter[] {
           new SqlParameter("@email", SqlDbType.NVarChar, 500),
            new SqlParameter("@blog", SqlDbType.NVarChar, 500),
            new SqlParameter("@isActived", SqlDbType.Bit),
            new SqlParameter("@username", SqlDbType.NVarChar, 500),
             new SqlParameter("@sqlcommand", SqlDbType.NVarChar, 500),
        };

            parameters[0].Value = email;
            parameters[1].Value = blog;
            parameters[2].Value = isActived;
            parameters[3].Value = username;
            parameters[4].Value = sqlcommand;
            return GetDataRow("[OT_IU_Email_Blog]", parameters);
        }
        public DataTable get_all_comments(bool? isActived)
        {
            return GetDataTable("[get_all_comments]", isActived);
        }
        public DataRow get_info_comments(int? autoid, string name, string email, int? idParent, int? idPost, bool? isActived)
        {
            SqlParameter[] parameters = new SqlParameter[] {
            new SqlParameter("@id", SqlDbType.Int),
            new SqlParameter("@name", SqlDbType.NVarChar, 500),
            new SqlParameter("@email", SqlDbType.NVarChar, 500),
            new SqlParameter("@idParent",  SqlDbType.Int),
            new SqlParameter("@idPost", SqlDbType.Int),
            new SqlParameter("@isActived", SqlDbType.Bit),
        };

            parameters[0].Value = autoid;
            parameters[1].Value = name;
            parameters[2].Value = email;
            parameters[3].Value = idParent;
            parameters[4].Value = idPost;
            parameters[5].Value = isActived;

            return GetDataRow("get_info_comments", parameters);
        }
        public DataTable Get_PostURL(int? id, string url, string blogID)
        {
            return GetDataTable("[Get_PostURL]", id, url, blogID);
        }
        public DataTable OT_COUNT_BLOG_DAY(int id, DateTime ngay)
        {
            return GetDataTable("[OT_COUNT_BLOG_DAY]", id, ngay);
        }
        public DataTable Get_TagPage(int? id)
        {
            return GetDataTable("[Get_TagPage]", id);
        }
        public DataTable OT_GET_EMAIL_DROP(string username)
        {
            return GetDataTable("[OT_GET_EMAIL_DROP]", username);
        }
        public DataTable OT_GET_BLOG_DROP(string username)
        {
            return GetDataTable("[OT_GET_BLOG_DROP]", username);
        }
        public object insert_update_tagPages(int autoid, string name, string url, string tagTitle, string tagDesc, string tagContent, string sql)
        {
            SqlParameter[] parameters = new SqlParameter[] {
            new SqlParameter("@id", SqlDbType.Int),
            new SqlParameter("@name", SqlDbType.NVarChar,500),
            new SqlParameter("@url", SqlDbType.NVarChar, 500),
            new SqlParameter("@tagTitle", SqlDbType.NVarChar, 500),
            new SqlParameter("@tagDesc", SqlDbType.NVarChar, 500),
            new SqlParameter("@tagContent", SqlDbType.NVarChar, 500),
            new SqlParameter("@sql", SqlDbType.NVarChar, 500),
        };

            parameters[0].Value = autoid;
            parameters[1].Value = name;
            parameters[2].Value = url;
            parameters[3].Value = tagTitle;
            parameters[4].Value = tagDesc;
            parameters[5].Value = tagContent;
            parameters[6].Value = sql;
            return ExecuteScalar("[insert_update_tagPages]", parameters);
        }
        public object OT_IU_tagPages(int autoid, string name, string url, string tagTitle, string tagDesc, string tagContent, string username, string sql)
        {
            SqlParameter[] parameters = new SqlParameter[] {
            new SqlParameter("@id", SqlDbType.Int),
            new SqlParameter("@name", SqlDbType.NVarChar,500),
            new SqlParameter("@url", SqlDbType.NVarChar, 500),
            new SqlParameter("@tagTitle", SqlDbType.NVarChar, 500),
            new SqlParameter("@tagDesc", SqlDbType.NVarChar, 500),
            new SqlParameter("@tagContent", SqlDbType.NVarChar, 500),
            new SqlParameter("@username", SqlDbType.NVarChar, 500),
            new SqlParameter("@sql", SqlDbType.NVarChar, 500),
        };

            parameters[0].Value = autoid;
            parameters[1].Value = name;
            parameters[2].Value = url;
            parameters[3].Value = tagTitle;
            parameters[4].Value = tagDesc;
            parameters[5].Value = tagContent;
            parameters[6].Value = username;
            parameters[7].Value = sql;
            return ExecuteScalar("[OT_IU_tagPages]", parameters);
        }
        public object insert_update_postURL(int autoid, string name, string url, string blogID, string sql)
        {
            SqlParameter[] parameters = new SqlParameter[] {
            new SqlParameter("@id", SqlDbType.Int),
            new SqlParameter("@name", SqlDbType.NVarChar,500),
            new SqlParameter("@url", SqlDbType.NVarChar, 500),
            new SqlParameter("@blogID", SqlDbType.NVarChar, 500),
            new SqlParameter("@sql", SqlDbType.NVarChar, 500),
        };

            parameters[0].Value = autoid;
            parameters[1].Value = name;
            parameters[2].Value = url;
            parameters[3].Value = blogID;
            parameters[4].Value = sql;
            return ExecuteScalar("[insert_update_postURL]", parameters);
        }
        public object insert_update_delete_words(int autoid, string name, string link, string title, string desc, string sql)
        {
            SqlParameter[] parameters = new SqlParameter[] {
            new SqlParameter("@id", SqlDbType.Int),
            new SqlParameter("@name", SqlDbType.NVarChar,500),
            new SqlParameter("@link", SqlDbType.NVarChar, 500),
            new SqlParameter("@title", SqlDbType.NVarChar, 500),
            new SqlParameter("@desc", SqlDbType.NVarChar, 500),
            new SqlParameter("@sql", SqlDbType.NVarChar, 500),
        };

            parameters[0].Value = autoid;
            parameters[1].Value = name;
            parameters[2].Value = link;
            parameters[3].Value = title;
            parameters[4].Value = desc;
            parameters[5].Value = sql;
            return ExecuteScalar("[insert_update_delete_words]", parameters);
        }
        public object OnInsert_Update_Delete_Pages(int autoid, string code, string title, string desc, string keywords, string name, DateTime ngaydang, string tomtat, string noidung, bool isActived, string hinhAnh, string id_tt, string username, string sql)
        {
            SqlParameter[] parameters = new SqlParameter[] {
            new SqlParameter("@id", SqlDbType.Int),
            new SqlParameter("@code", SqlDbType.NVarChar),
            new SqlParameter("@title", SqlDbType.NVarChar, 500),
            new SqlParameter("@desc", SqlDbType.NVarChar, 500),
            new SqlParameter("@keywords", SqlDbType.NVarChar, 500),
            new SqlParameter("@name", SqlDbType.NVarChar, 500),
            new SqlParameter("@ngaydang", SqlDbType.DateTime),
             new SqlParameter("@tomtat", SqlDbType.NText),
            new SqlParameter("@noidung", SqlDbType.NText),
            new SqlParameter("@isActived", SqlDbType.Bit),
            new SqlParameter("@hinhAnh", SqlDbType.NVarChar, 500),
            new SqlParameter("@id_tt", SqlDbType.NVarChar, 500),
            new SqlParameter("@username", SqlDbType.NVarChar, 500),
            new SqlParameter("@sql", SqlDbType.NVarChar, 500),
        };

            parameters[0].Value = autoid;
            parameters[1].Value = code;
            parameters[2].Value = title;
            parameters[3].Value = desc;
            parameters[4].Value = keywords;
            parameters[5].Value = name;
            parameters[6].Value = ngaydang;
            parameters[7].Value = tomtat;
            parameters[8].Value = noidung;
            parameters[9].Value = isActived;
            parameters[10].Value = hinhAnh;
            parameters[11].Value = id_tt;
            parameters[12].Value = username;
            parameters[13].Value = sql;
            return ExecuteScalar("Insert_Update_Delete_Pages", parameters);
        }
        public object insert_update_comments(int autoid, string name, string email, string message, int idParent, int idPost, bool? isActived, string sql)
        {
            SqlParameter[] parameters = new SqlParameter[] {
            new SqlParameter("@id", SqlDbType.Int),
            new SqlParameter("@name", SqlDbType.NVarChar, 500),
            new SqlParameter("@email", SqlDbType.NVarChar, 500),
             new SqlParameter("@message", SqlDbType.NVarChar, 500),
            new SqlParameter("@idParent",  SqlDbType.Int),
            new SqlParameter("@idPost", SqlDbType.Int),
            new SqlParameter("@isActived", SqlDbType.Bit),
            new SqlParameter("@sql", SqlDbType.NVarChar, 500),
        };

            parameters[0].Value = autoid;
            parameters[1].Value = name;
            parameters[2].Value = email;
            parameters[3].Value = message;
            parameters[4].Value = idParent;
            parameters[5].Value = idPost;
            parameters[6].Value = isActived;
            parameters[7].Value = sql;
            return ExecuteScalar("insert_update_comments", parameters);
        }

        public object update_content_news(string noidung, int id)
        {
            SqlParameter[] parameters = new SqlParameter[] {
            new SqlParameter("@noidung", SqlDbType.NText),
            new SqlParameter("@id", SqlDbType.Int)
        };
            parameters[0].Value = noidung;
            parameters[1].Value = id;
            return ExecuteScalar("update_content_news", parameters);
        }
        public object OnInsert_Update_Delete_News(int autoid, string code, string title, string desc, string keywords, string name, DateTime ngaydang, string tomtat, string noidung, bool isActived, string hinhAnh, int maloai, string id_tt, string gia, string diachi, int quan, int tinh, string username, DateTime? handangtin, string nguon, string img_home, string sql)
        {
            SqlParameter[] parameters = new SqlParameter[] {
            new SqlParameter("@id", SqlDbType.Int),
            new SqlParameter("@code", SqlDbType.NVarChar),
            new SqlParameter("@title", SqlDbType.NVarChar, 500),
            new SqlParameter("@desc", SqlDbType.NVarChar, 500),
            new SqlParameter("@keywords", SqlDbType.NVarChar, 500),
            new SqlParameter("@name", SqlDbType.NVarChar, 500),
            new SqlParameter("@ngaydang", SqlDbType.DateTime),
             new SqlParameter("@tomtat", SqlDbType.NText),
            new SqlParameter("@noidung", SqlDbType.NText),
            new SqlParameter("@isActived", SqlDbType.Bit),
            new SqlParameter("@hinhAnh", SqlDbType.NVarChar, 500),
            new SqlParameter("@maloai", SqlDbType.Int, 500),
            new SqlParameter("@id_tt", SqlDbType.NVarChar, 500),
            new SqlParameter("@gia", SqlDbType.NVarChar, 500),
            new SqlParameter("@dc", SqlDbType.NVarChar, 500),
            new SqlParameter("@quan", SqlDbType.Int),
            new SqlParameter("@tinh", SqlDbType.Int),
            new SqlParameter("@username", SqlDbType.NVarChar, 500),
            new SqlParameter("@handangtin", SqlDbType.DateTime),
            new SqlParameter("@nguon", SqlDbType.NVarChar, 500),
            new SqlParameter("@img_home", SqlDbType.NVarChar, 500),
            new SqlParameter("@sql", SqlDbType.NVarChar, 500),
        };

            parameters[0].Value = autoid;
            parameters[1].Value = code;
            parameters[2].Value = title;
            parameters[3].Value = desc;
            parameters[4].Value = keywords;
            parameters[5].Value = name;
            parameters[6].Value = ngaydang;
            parameters[7].Value = tomtat;
            parameters[8].Value = noidung;
            parameters[9].Value = isActived;
            parameters[10].Value = hinhAnh;
            parameters[11].Value = maloai;
            parameters[12].Value = id_tt;
            parameters[13].Value = gia;
            parameters[14].Value = diachi;
            parameters[15].Value = quan;
            parameters[16].Value = tinh;
            parameters[17].Value = username;
            parameters[18].Value = handangtin;
            parameters[19].Value = nguon;
            parameters[20].Value = img_home;
            parameters[21].Value = sql;
            return ExecuteScalar("Insert_Update_Delete_News", parameters);
        }
        public object insert_update_delete_mypost(int autoid, string title, DateTime ngaydang, string noidung,
            bool isActived, string maloai, string username, string sql)
        {
            SqlParameter[] parameters = new SqlParameter[] {
            new SqlParameter("@id", SqlDbType.Int),
            new SqlParameter("@title", SqlDbType.NVarChar, 500),
            new SqlParameter("@ngaydang", SqlDbType.DateTime),
            new SqlParameter("@noidung", SqlDbType.NText),
            new SqlParameter("@isActived", SqlDbType.Bit),
            new SqlParameter("@maloai", SqlDbType.Int, 500),
            new SqlParameter("@username", SqlDbType.NVarChar, 500),
            new SqlParameter("@sql", SqlDbType.NVarChar, 500),
        };

            parameters[0].Value = autoid;
            parameters[1].Value = title;
            parameters[2].Value = ngaydang;
            parameters[3].Value = noidung;
            parameters[4].Value = isActived;
            parameters[5].Value = maloai;
            parameters[6].Value = username;
            parameters[7].Value = sql;
            return ExecuteScalar("insert_update_delete_mypost", parameters);
        }
        public object insert_update_delete_member(string username, string mk, string hoten, DateTime ngaysinh,
            int gioitinh, string diachi, string email, string website, string sodienthoai, int quan, int tinh, string yahoo, string facebook, string skype, string sql)
        {
            SqlParameter[] parameters = new SqlParameter[] {
            new SqlParameter("@username", SqlDbType.Int),
            new SqlParameter("@matkhau", SqlDbType.NVarChar, 500),
            new SqlParameter("@hoten", SqlDbType.NVarChar, 500),
            new SqlParameter("@ngaysinh", SqlDbType.NVarChar, 500),
            new SqlParameter("@gioitinh", SqlDbType.NVarChar, 500),
            new SqlParameter("@diachi", SqlDbType.NVarChar, 500),
            new SqlParameter("@email", SqlDbType.NVarChar, 500),
            new SqlParameter("@website", SqlDbType.NVarChar, 500),
            new SqlParameter("@sodienthoai", SqlDbType.NVarChar, 500),
            new SqlParameter("@quan", SqlDbType.Int),
            new SqlParameter("@tinh", SqlDbType.Int),
            new SqlParameter("@yahoo", SqlDbType.NVarChar, 500),
            new SqlParameter("@facebook", SqlDbType.NVarChar, 500),
            new SqlParameter("@skype", SqlDbType.NVarChar, 500),
            new SqlParameter("@sql", SqlDbType.NVarChar, 500),
        };
            parameters[0].Value = username;
            parameters[1].Value = mk;
            parameters[2].Value = hoten;
            parameters[3].Value = ngaysinh;
            parameters[4].Value = gioitinh;
            parameters[5].Value = diachi;
            parameters[6].Value = email;
            parameters[7].Value = website;
            parameters[8].Value = sodienthoai;
            parameters[9].Value = quan;
            parameters[10].Value = tinh;
            parameters[11].Value = yahoo;
            parameters[12].Value = facebook;
            parameters[13].Value = skype;
            parameters[14].Value = sql;
            return ExecuteScalar("insert_update_delete_member", parameters);
        }

        public object Change_Pass(string username, string mk)
        {
            SqlParameter[] parameters = new SqlParameter[] {

            new SqlParameter("@username", SqlDbType.NVarChar, 500),
            new SqlParameter("@matkhau", SqlDbType.NVarChar, 500),
        };
            parameters[0].Value = username;
            parameters[1].Value = mk;
            return ExecuteScalar("Change_Pass", parameters);
        }
        public object OnInsert_Update_Delete_LoaiTin(int ma, string name, string url, string url_ct, string title,
            string desc, string noidung, string keywords, string images, string code, int idDanhMuc, bool isNews, int isPatient, int order_by, bool actived, string username, string sql)
        {
            SqlParameter[] parameters = new SqlParameter[] {

            new SqlParameter("@ma", SqlDbType.Int),
            new SqlParameter("@name", SqlDbType.NVarChar, 500),
            new SqlParameter("@url", SqlDbType.NVarChar, 500),
            new SqlParameter("@url_ct", SqlDbType.NVarChar, 500),
            new SqlParameter("@title", SqlDbType.NVarChar, 500),
            new SqlParameter("@desc", SqlDbType.NVarChar, 500),
            new SqlParameter("@noidung", SqlDbType.NVarChar, 500),
            new SqlParameter("@keywords", SqlDbType.NVarChar, 500),
            new SqlParameter("@images", SqlDbType.NVarChar, 500),
            new SqlParameter("@code", SqlDbType.NVarChar, 500),
            new SqlParameter("@idDanhMuc", SqlDbType.Int),
            new SqlParameter("@isNews", SqlDbType.Bit),
            new SqlParameter("@isPatient", SqlDbType.Int),
            new SqlParameter("@order_by", SqlDbType.Int),
            new SqlParameter("@actived", SqlDbType.Bit),
             new SqlParameter("@username", SqlDbType.NVarChar, 500),
            new SqlParameter("@sql", SqlDbType.NVarChar, 500),
        };
            parameters[0].Value = ma;
            parameters[1].Value = name;
            parameters[2].Value = url;
            parameters[3].Value = url_ct;
            parameters[4].Value = title;
            parameters[5].Value = desc;
            parameters[6].Value = noidung;
            parameters[7].Value = keywords;
            parameters[8].Value = images;
            parameters[9].Value = code;
            parameters[10].Value = idDanhMuc;
            parameters[11].Value = isNews;
            parameters[12].Value = isPatient;
            parameters[13].Value = order_by;
            parameters[14].Value = actived;
            parameters[15].Value = username;
            parameters[16].Value = sql;
            return ExecuteScalar("Insert_Update_Delete_LoaiTin", parameters);
        }
        public object OnInsert_Update_Delete_LoaiTin_Blog(int ma, string name, string url, string url_ct, string title,
            string desc, string noidung, string keywords, string images, string code, int idDanhMuc, bool isNews, int isPatient, int order_by, bool actived, string username, string idblog, string sql)
        {
            SqlParameter[] parameters = new SqlParameter[] {

            new SqlParameter("@ma", SqlDbType.Int),
            new SqlParameter("@name", SqlDbType.NVarChar, 500),
            new SqlParameter("@url", SqlDbType.NVarChar, 500),
            new SqlParameter("@url_ct", SqlDbType.NVarChar, 500),
            new SqlParameter("@title", SqlDbType.NVarChar, 500),
            new SqlParameter("@desc", SqlDbType.NVarChar, 500),
            new SqlParameter("@noidung", SqlDbType.NVarChar, 500),
            new SqlParameter("@keywords", SqlDbType.NVarChar, 500),
            new SqlParameter("@images", SqlDbType.NVarChar, 500),
            new SqlParameter("@code", SqlDbType.NVarChar, 500),
            new SqlParameter("@idDanhMuc", SqlDbType.Int),
            new SqlParameter("@isNews", SqlDbType.Bit),
            new SqlParameter("@isPatient", SqlDbType.Int),
            new SqlParameter("@order_by", SqlDbType.Int),
            new SqlParameter("@actived", SqlDbType.Bit),
             new SqlParameter("@username", SqlDbType.NVarChar, 500),
              new SqlParameter("@idblog", SqlDbType.NVarChar, 500),
            new SqlParameter("@sql", SqlDbType.NVarChar, 500),
        };
            parameters[0].Value = ma;
            parameters[1].Value = name;
            parameters[2].Value = url;
            parameters[3].Value = url_ct;
            parameters[4].Value = title;
            parameters[5].Value = desc;
            parameters[6].Value = noidung;
            parameters[7].Value = keywords;
            parameters[8].Value = images;
            parameters[9].Value = code;
            parameters[10].Value = idDanhMuc;
            parameters[11].Value = isNews;
            parameters[12].Value = isPatient;
            parameters[13].Value = order_by;
            parameters[14].Value = actived;
            parameters[15].Value = username;
            parameters[16].Value = idblog;
            parameters[17].Value = sql;
            return ExecuteScalar("Insert_Update_Delete_LoaiTin_blog", parameters);
        }
        public object OnInsert_Update_Delete_Config(int id, string url, string name, string title,
            string desc, string keywords, string tieude, DateTime ngaydang, string tomtat, string noidung, string foot, string oldR, string newR, string sql)
        {
            SqlParameter[] parameters = new SqlParameter[] {
            new SqlParameter("@id", SqlDbType.Int),
            new SqlParameter("@url", SqlDbType.NVarChar),
            new SqlParameter("@name", SqlDbType.NVarChar),
            new SqlParameter("@title", SqlDbType.NVarChar, 500),
            new SqlParameter("@desc", SqlDbType.NVarChar, 500),
            new SqlParameter("@keywords", SqlDbType.NVarChar, 500),
            new SqlParameter("@name", SqlDbType.NVarChar, 500),
            new SqlParameter("@ngaydang", SqlDbType.DateTime),
             new SqlParameter("@tomtat", SqlDbType.NText),
            new SqlParameter("@noidung", SqlDbType.NText),
            new SqlParameter("@foot", SqlDbType.NVarChar),
            new SqlParameter("@oldR", SqlDbType.NVarChar, 500),
            new SqlParameter("@newR", SqlDbType.NVarChar, 500),
            new SqlParameter("@sql", SqlDbType.NVarChar, 500),
        };

            parameters[0].Value = id;
            parameters[1].Value = url;
            parameters[2].Value = name;
            parameters[3].Value = title;
            parameters[4].Value = desc;
            parameters[5].Value = keywords;
            parameters[6].Value = tieude;
            parameters[7].Value = ngaydang;
            parameters[8].Value = tomtat;
            parameters[9].Value = noidung;
            parameters[10].Value = foot;
            parameters[11].Value = oldR;
            parameters[12].Value = newR;
            parameters[13].Value = sql;

            return ExecuteScalar("Insert_Update_Delete_Config", parameters);
        }
        public object insert_update_delete_url(string code, string link, string sql)
        {
            SqlParameter[] parameters = new SqlParameter[] {
            new SqlParameter("@code", SqlDbType.NVarChar,500),
            new SqlParameter("@links", SqlDbType.NVarChar,500),
            new SqlParameter("@sql", SqlDbType.NVarChar,500),
        };

            parameters[0].Value = code;
            parameters[1].Value = link;
            parameters[2].Value = sql;

            return ExecuteScalar("[insert_update_delete_url]", parameters);
        }
        public object insert_update_delete_hinh_anh(int id, string name, string filename, string link, bool isActived, string codeLoai, string sql)
        {
            SqlParameter[] parameters = new SqlParameter[] {
            new SqlParameter("@id", SqlDbType.Int),
            new SqlParameter("@name", SqlDbType.NVarChar,500),
            new SqlParameter("@filename", SqlDbType.NVarChar,500),
            new SqlParameter("@link", SqlDbType.NVarChar,500),
            new SqlParameter("@isActibed", SqlDbType.Bit),
            new SqlParameter("@CodeLoai", SqlDbType.NVarChar,500),
            new SqlParameter("@sql", SqlDbType.NVarChar,500),
        };

            parameters[0].Value = id;
            parameters[1].Value = name;
            parameters[2].Value = filename;
            parameters[3].Value = link;
            parameters[4].Value = isActived;
            parameters[5].Value = codeLoai;
            parameters[6].Value = sql;
            return ExecuteScalar("[insert_update_delete_hinhAnh]", parameters);
        }
        public object insert_update_delete_slider(int id, string name, string filename, string link, bool isActived, string sql)
        {
            SqlParameter[] parameters = new SqlParameter[] {
            new SqlParameter("@id", SqlDbType.Int),
            new SqlParameter("@name", SqlDbType.NVarChar,500),
            new SqlParameter("@filename", SqlDbType.NVarChar,500),
            new SqlParameter("@link", SqlDbType.NVarChar,500),
            new SqlParameter("@isActibed", SqlDbType.Bit),
            new SqlParameter("@sql", SqlDbType.NVarChar,500),
        };

            parameters[0].Value = id;
            parameters[1].Value = name;
            parameters[2].Value = filename;
            parameters[3].Value = link;
            parameters[4].Value = isActived;
            parameters[5].Value = sql;

            return ExecuteScalar("[insert_update_delete_slider]", parameters);
        }
        public object insert_update_delete_Register(int id, string phone, string email, string sqlCommamd)
        {
            SqlParameter[] parameters = new SqlParameter[] {
            new SqlParameter("@id", SqlDbType.Int),
            new SqlParameter("@phone", SqlDbType.NVarChar,500),
            new SqlParameter("@email", SqlDbType.NVarChar,500),
            new SqlParameter("@sqlCommamd", SqlDbType.NVarChar,500),
        };

            parameters[0].Value = id;
            parameters[1].Value = phone;
            parameters[2].Value = email;
            parameters[3].Value = sqlCommamd;
            return ExecuteScalar("[insert_update_delete_Register]", parameters);
        }
        public object OnInsert_Update_Delete_Menu(int ma, string name, string link, bool isAct, int isparent, int numberSort, int styleShow, string sql)
        {
            SqlParameter[] parameters = new SqlParameter[] {
            new SqlParameter("@ma", SqlDbType.Int),
            new SqlParameter("@name", SqlDbType.NVarChar, 500),
            new SqlParameter("@link", SqlDbType.NVarChar, 500),
            new SqlParameter("@isAct", SqlDbType.Bit),
            new SqlParameter("@isparent", SqlDbType.Int),
            new SqlParameter("@numberSort", SqlDbType.Int),
            new SqlParameter("@styleShow", SqlDbType.Int),
            new SqlParameter("@sql", SqlDbType.NVarChar, 500),
        };
            parameters[0].Value = ma;
            parameters[1].Value = name;
            parameters[2].Value = link;
            parameters[3].Value = isAct;
            parameters[4].Value = isparent;
            parameters[5].Value = numberSort;
            parameters[6].Value = styleShow;
            parameters[7].Value = sql;
            return ExecuteScalar("Insert_Update_Delete_Menu", parameters);
        }
        public object OnInsert_Update_Delete_DanhMuc(int ma, string title, string mota, string keywords, string noidung, bool isMenu, string images, int order_by, string sql)
        {
            SqlParameter[] parameters = new SqlParameter[] {
            new SqlParameter("@ma", SqlDbType.Int),
            new SqlParameter("@name", SqlDbType.NVarChar, 500),
            new SqlParameter("@mota", SqlDbType.NVarChar, 500),
            new SqlParameter("@keywords", SqlDbType.NVarChar, 500),
            new SqlParameter("@noidung", SqlDbType.NVarChar, 500),
            new SqlParameter("@isMenu", SqlDbType.Bit),
            new SqlParameter("@images", SqlDbType.NVarChar, 500),
            new SqlParameter("@order_by", SqlDbType.Int),
            new SqlParameter("@sql", SqlDbType.NVarChar, 500),
        };
            parameters[0].Value = ma;
            parameters[1].Value = title;
            parameters[2].Value = mota;
            parameters[3].Value = keywords;
            parameters[4].Value = noidung;
            parameters[5].Value = isMenu;
            parameters[6].Value = images;
            parameters[7].Value = order_by;
            parameters[8].Value = sql;
            return ExecuteScalar("Insert_Update_Delete_DanhMuc", parameters);
        }
        //
        public object actived_user(string code)
        {
            SqlParameter[] parameters = new SqlParameter[] {
            new SqlParameter("@code", SqlDbType.NVarChar, 500)
        };
            parameters[0].Value = code;
            return ExecuteScalar("actived_user", parameters);
        }
        public object insert_update_delete_FooterLinks(int ma, string name, string url, bool isActived, string sql)
        {
            SqlParameter[] parameters = new SqlParameter[] {
            new SqlParameter("@ma", SqlDbType.Int),
            new SqlParameter("@name", SqlDbType.NVarChar, 500),
            new SqlParameter("@url", SqlDbType.NVarChar, 500),
            new SqlParameter("@isMenu", SqlDbType.Bit),
            new SqlParameter("@sql", SqlDbType.NVarChar, 500),
        };
            parameters[0].Value = ma;
            parameters[1].Value = name;
            parameters[2].Value = url;
            parameters[3].Value = isActived;
            parameters[4].Value = sql;
            return ExecuteScalar("BDSinsert_update_delete_FooterLinks", parameters);
        }
        public object OnInsert_Update_Delete_Media(int ma, string name, string desc, string link, string images, int id_c, string sql)
        {
            SqlParameter[] parameters = new SqlParameter[] {
            new SqlParameter("@ma", SqlDbType.Int),
            new SqlParameter("@name", SqlDbType.NVarChar, 500),
            new SqlParameter("@desc", SqlDbType.NVarChar, 500),
            new SqlParameter("@link", SqlDbType.NVarChar, 500),
            new SqlParameter("@images", SqlDbType.NVarChar, 500),
            new SqlParameter("@i_c", SqlDbType.Int, 500),
            new SqlParameter("@sql", SqlDbType.NVarChar, 500),
        };
            parameters[0].Value = ma;
            parameters[1].Value = name;
            parameters[2].Value = desc;
            parameters[3].Value = link;
            parameters[4].Value = images;
            parameters[5].Value = id_c;
            parameters[6].Value = sql;
            return ExecuteScalar("Insert_Update_Delete_Media", parameters);
        }
        public object OnInsert_Update_Delete_Menu(int ma, string name, string link, string sql)
        {
            SqlParameter[] parameters = new SqlParameter[] {
            new SqlParameter("@ma", SqlDbType.Int),
            new SqlParameter("@name", SqlDbType.NVarChar, 500),
            new SqlParameter("@link", SqlDbType.NVarChar, 500),
            new SqlParameter("@sql", SqlDbType.NVarChar, 500),
        };
            parameters[0].Value = ma;
            parameters[1].Value = name;
            parameters[2].Value = link;
            parameters[3].Value = sql;
            return ExecuteScalar("Insert_Update_Delete_Menu", parameters);
        }
        public object Oninsert_update_delete_loai_media(int ma, string name, string sql)
        {
            SqlParameter[] parameters = new SqlParameter[] {
            new SqlParameter("@ma", SqlDbType.Int),
            new SqlParameter("@name", SqlDbType.NVarChar, 500),
            new SqlParameter("@sql", SqlDbType.NVarChar, 500),
        };
            parameters[0].Value = ma;
            parameters[1].Value = name;
            parameters[2].Value = sql;
            return ExecuteScalar("insert_update_delete_loai_media", parameters);
        }


        public object insert_update_delete_tuvan(int ma, string name, string ngay, string email, string diachi, string dienthoai, string skype, string noidung, bool isDeleted, int status, string username, string sql)
        {
            SqlParameter[] parameters = new SqlParameter[] {
            new SqlParameter("@ma", SqlDbType.Int),
            new SqlParameter("@name", SqlDbType.NVarChar, 500),
            new SqlParameter("@ngay", SqlDbType.NVarChar, 500),
            new SqlParameter("@email", SqlDbType.NVarChar, 500),
            new SqlParameter("@diachi", SqlDbType.NVarChar, 500),
            new SqlParameter("@dienthoai", SqlDbType.NVarChar, 500),
            new SqlParameter("@skype", SqlDbType.NVarChar, 500),
            new SqlParameter("@noidung", SqlDbType.NVarChar, 500),
            new SqlParameter("@isDelete", SqlDbType.Bit),
            new SqlParameter("@status", SqlDbType.Int),
            new SqlParameter("@username", SqlDbType.NVarChar, 500),
            new SqlParameter("@sql", SqlDbType.NVarChar, 500),
        };
            parameters[0].Value = ma;
            parameters[1].Value = name;
            parameters[2].Value = ngay;
            parameters[3].Value = email;
            parameters[4].Value = diachi;
            parameters[5].Value = dienthoai;
            parameters[6].Value = skype;
            parameters[7].Value = noidung;
            parameters[8].Value = isDeleted;
            parameters[9].Value = status;
            parameters[10].Value = username;
            parameters[11].Value = sql;
            return ExecuteScalar("insert_update_delete_tuvan", parameters);
        }
        public object insert_update_caidat(int ma, int sotin, int solink, int songay, string tieudetrang, string tieudetrangchu, string desc, string tukhoa, string tieudevideo, string tieuderaovat, string head_page)
        {
            SqlParameter[] parameters = new SqlParameter[] {
            new SqlParameter("@ma", SqlDbType.Int),
            new SqlParameter("@tin", SqlDbType.Int),
            new SqlParameter("@link", SqlDbType.Int),
            new SqlParameter("@ngay", SqlDbType.Int),
            new SqlParameter("@tieudetrang", SqlDbType.NVarChar,500),
            new SqlParameter("@tieudetrangchu", SqlDbType.NVarChar,500),
            new SqlParameter("@description", SqlDbType.NVarChar,500),
            new SqlParameter("@keywords", SqlDbType.NVarChar,500),
            new SqlParameter("@tieudetrangchuvideo", SqlDbType.NVarChar,500),
            new SqlParameter("@tieudetrangchuraovat", SqlDbType.NVarChar,500),
            new SqlParameter("@head_page", SqlDbType.NText),
        };
            parameters[0].Value = ma;
            parameters[1].Value = sotin;
            parameters[2].Value = solink;
            parameters[3].Value = songay;
            parameters[4].Value = tieudetrang;
            parameters[5].Value = tieudetrangchu;
            parameters[6].Value = desc;
            parameters[7].Value = tukhoa;
            parameters[8].Value = tieudevideo;
            parameters[9].Value = tieuderaovat;
            parameters[10].Value = head_page;
            return ExecuteScalar("insert_update_caidat", parameters);
        }
        public object Update_News(int autoid, string title, string tomtat, string noidung, string hinhAnh, string sql)
        {
            SqlParameter[] parameters = new SqlParameter[] {
            new SqlParameter("@id", SqlDbType.Int),
            new SqlParameter("@title", SqlDbType.NVarChar, 500),
             new SqlParameter("@tomtat", SqlDbType.NText),
            new SqlParameter("@noidung", SqlDbType.NText),
            new SqlParameter("@hinhAnh", SqlDbType.NVarChar, 500),
            new SqlParameter("@sql", SqlDbType.NVarChar, 500),
        };

            parameters[0].Value = autoid;
            parameters[1].Value = title;
            parameters[2].Value = tomtat;
            parameters[3].Value = noidung;
            parameters[4].Value = hinhAnh;
            parameters[5].Value = sql;
            return ExecuteScalar("Update_News", parameters);
        }
        public object insert_admin_update_delete_member(string username, string mk, string hoten, DateTime ngaysinh,
           int gioitinh, string diachi, string email, string website, string sodienthoai, int quan, int tinh,
            string yahoo, string facebook, string skype, bool actived, string sql)
        {
            SqlParameter[] parameters = new SqlParameter[] {

            new SqlParameter("@username", SqlDbType.Int),
            new SqlParameter("@matkhau", SqlDbType.NVarChar, 500),
            new SqlParameter("@hoten", SqlDbType.NVarChar, 500),
            new SqlParameter("@ngaysinh", SqlDbType.NVarChar, 500),
            new SqlParameter("@gioitinh", SqlDbType.NVarChar, 500),
            new SqlParameter("@diachi", SqlDbType.NVarChar, 500),
            new SqlParameter("@email", SqlDbType.NVarChar, 500),
            new SqlParameter("@website", SqlDbType.NVarChar, 500),
            new SqlParameter("@sodienthoai", SqlDbType.NVarChar, 500),
            new SqlParameter("@quan", SqlDbType.Int),
            new SqlParameter("@tinh", SqlDbType.Int),
            new SqlParameter("@yahoo", SqlDbType.NVarChar, 500),
            new SqlParameter("@facebook", SqlDbType.NVarChar, 500),
            new SqlParameter("@skype", SqlDbType.NVarChar, 500),
            new SqlParameter("@isactived", SqlDbType.Bit, 500),
            new SqlParameter("@sql", SqlDbType.NVarChar, 500),
        };
            parameters[0].Value = username;
            parameters[1].Value = mk;
            parameters[2].Value = hoten;
            parameters[3].Value = ngaysinh;
            parameters[4].Value = gioitinh;
            parameters[5].Value = diachi;
            parameters[6].Value = email;
            parameters[7].Value = website;
            parameters[8].Value = sodienthoai;
            parameters[9].Value = quan;
            parameters[10].Value = tinh;
            parameters[11].Value = yahoo;
            parameters[12].Value = facebook;
            parameters[13].Value = skype;
            parameters[14].Value = actived;
            parameters[15].Value = sql;
            return ExecuteScalar("insert_admin_update_delete_member", parameters);
        }
        //insert    update    lock    unlock    del   changepass
        public object insert_update_delete_cms_user(string username, string mk, string hoten, string email, string dienthoai, bool isactived, bool isadmin,
            string URLImages, string UserCreate, string UserLastEdit, string ip, string sql)
        {
            SqlParameter[] parameters = new SqlParameter[] {

            new SqlParameter("@username", SqlDbType.NVarChar,500),
            new SqlParameter("@matkhau", SqlDbType.NVarChar,500),
            new SqlParameter("@hoten", SqlDbType.NVarChar,500),
            new SqlParameter("@cmnd", SqlDbType.NVarChar,500),
            new SqlParameter("@dienthoai", SqlDbType.NVarChar,500),
            new SqlParameter("@isactived", SqlDbType.Bit),
            new SqlParameter("@isadmin", SqlDbType.Bit),
            new SqlParameter("@URLImages", SqlDbType.NVarChar,500),
            new SqlParameter("@UserCreate", SqlDbType.NVarChar,500),
            new SqlParameter("@UserLastEdit", SqlDbType.NVarChar,500),
            new SqlParameter("@ip", SqlDbType.NVarChar,500),
            new SqlParameter("@sql", SqlDbType.NVarChar, 500),

        };
            parameters[0].Value = username;
            parameters[1].Value = mk;
            parameters[2].Value = hoten;
            parameters[3].Value = email;
            parameters[4].Value = dienthoai;
            parameters[5].Value = isactived;
            parameters[6].Value = isadmin;
            parameters[7].Value = URLImages;
            parameters[8].Value = UserCreate;
            parameters[9].Value = UserLastEdit;
            parameters[10].Value = ip;
            parameters[11].Value = sql;
            return ExecuteScalar("insert_update_delete_cms_user", parameters);
        }
        public object insert_update_delete_cms_user(Accounts acount, string sql)
        {
            SqlParameter[] parameters = new SqlParameter[] {

            new SqlParameter("@username", SqlDbType.NVarChar,500),
            new SqlParameter("@matkhau", SqlDbType.NVarChar,500),
            new SqlParameter("@hoten", SqlDbType.NVarChar,500),
            new SqlParameter("@cmnd", SqlDbType.NVarChar,500),
            new SqlParameter("@dienthoai", SqlDbType.NVarChar,500),
            new SqlParameter("@isactived", SqlDbType.Bit),
            new SqlParameter("@isadmin", SqlDbType.Bit),
            new SqlParameter("@URLImages", SqlDbType.NVarChar,500),
            new SqlParameter("@UserCreate", SqlDbType.NVarChar,500),
            new SqlParameter("@UserLastEdit", SqlDbType.NVarChar,500),
            new SqlParameter("@ip", SqlDbType.NVarChar,500),
            new SqlParameter("@sql", SqlDbType.NVarChar, 500),

        };
            parameters[0].Value = acount.username;
            parameters[1].Value = acount.password;
            parameters[2].Value = acount.fullname;
            parameters[3].Value = acount.email;
            parameters[4].Value = acount.phone;
            parameters[5].Value = acount.active;
            parameters[6].Value = acount.admin;
            parameters[7].Value = acount.imageUrl;
            parameters[8].Value = acount.userCreate;
            parameters[9].Value = acount.userEdit;
            parameters[10].Value = acount.IP;
            parameters[11].Value = sql;
            return ExecuteScalar("insert_update_delete_cms_user", parameters);
        }
        //[]

        public object insert_update_delete_khuvuc_user(string username, string khuvuc, int tinh, string sql)
        {
            SqlParameter[] parameters = new SqlParameter[] {

            new SqlParameter("@username", SqlDbType.NVarChar),
            new SqlParameter("@khuvuc", SqlDbType.NVarChar),
             new SqlParameter("@tinh", SqlDbType.Int),
            new SqlParameter("@sql", SqlDbType.NVarChar, 500),
        };
            parameters[0].Value = username;
            parameters[1].Value = khuvuc;
            parameters[2].Value = tinh;
            parameters[3].Value = sql;
            return ExecuteScalar("insert_update_delete_khuvuc_user", parameters);

        }
        public string getId()
        {
            DateTime date = DateTime.Now;
            string month = String.Format("{0:MM}", date);
            string day = String.Format("{0:dd}", date);
            string hours = String.Format("{0:HH}", date);
            string minute = String.Format("{0:mm}", date);
            string second = String.Format("{0:ss}", date);

            return date.Year + month + day + "-" + hours + minute + second;
        }
        #endregion

        #region BDS
        public object BDSinsert_update_delete_TinBDS(int autoid,
            string code, string TieuDe, string NoiDung, string Description_SEO, string Keywords_SEO, DateTime? NgayBD, DateTime? NgayKT, string hinhAnh, int idHinhThuc,
            int IdLoaiBDS, int IdTinh, int IdQuan, int IdXa, int IdPho, int IdDuAn, double DienTich, double Gia, string DonViGia, string TenLienHe,
            string DiaChi, string DiaChiLienHe, string DienThoai, string Email, string BanDo, int MatTien, int DuongVao, int HuongNha, int HuongBanCong,
            int SoTang, int SoPhongNgu, int SoToilet, string noiThat, string NguoiDang, int LoaiTin, int LoaiRao, bool isActived, string sql)
        {
            SqlParameter[] parameters = new SqlParameter[] {
            new SqlParameter("@Id", SqlDbType.Int),
            new SqlParameter("@code", SqlDbType.NVarChar),
            new SqlParameter("@TieuDe", SqlDbType.NVarChar, 500),
            new SqlParameter("@NoiDung", SqlDbType.NVarChar, 500),
            new SqlParameter("@Description_SEO", SqlDbType.NVarChar, 500),

            new SqlParameter("@Keywords_SEO", SqlDbType.NVarChar, 500),
            new SqlParameter("@NgayBD", SqlDbType.DateTime),
             new SqlParameter("@NgayKT", SqlDbType.DateTime),
            new SqlParameter("@hinhAnh", SqlDbType.NText),
            new SqlParameter("@idHinhThuc", SqlDbType.Int),

            new SqlParameter("@IdLoaiBDS", SqlDbType.Int, 500),
            new SqlParameter("@IdTinh", SqlDbType.Int, 500),
            new SqlParameter("@IdQuan", SqlDbType.Int, 500),
            new SqlParameter("@IdXa", SqlDbType.Int, 500),
            new SqlParameter("@IdPho", SqlDbType.Int, 500),

            new SqlParameter("@IdDuAn", SqlDbType.Int),
            new SqlParameter("@DienTich", SqlDbType.Float),
            new SqlParameter("@Gia", SqlDbType.Float, 500),
            new SqlParameter("@DonViGia", SqlDbType.NVarChar),
            new SqlParameter("@TenLienHe", SqlDbType.NVarChar, 500),

            new SqlParameter("@DiaChi", SqlDbType.NVarChar, 500),
            new SqlParameter("@DiaChiLienHe", SqlDbType.NVarChar, 500),
            new SqlParameter("@DienThoai", SqlDbType.NVarChar, 500),
            new SqlParameter("@Email", SqlDbType.NVarChar, 500),
            new SqlParameter("@BanDo", SqlDbType.NVarChar, 500),
            new SqlParameter("@MatTien", SqlDbType.Int, 500),

            new SqlParameter("@DuongVao", SqlDbType.NVarChar, 500),
            new SqlParameter("@HuongNha", SqlDbType.NVarChar, 500),
            new SqlParameter("@HuongBanCong", SqlDbType.NVarChar, 500),
            new SqlParameter("@SoTang", SqlDbType.Int, 500),
            new SqlParameter("@SoPhongNgu", SqlDbType.Int, 500),

            new SqlParameter("@SoToilet", SqlDbType.Int, 500),
            new SqlParameter("@NoiThat", SqlDbType.NVarChar, 500),
            new SqlParameter("@NguoiDang", SqlDbType.NVarChar, 500),
            new SqlParameter("@LoaiTin", SqlDbType.Int, 500),
            new SqlParameter("@LoaiRao", SqlDbType.Int, 500),
            new SqlParameter("@IsActived", SqlDbType.Bit),
            new SqlParameter("@sql", SqlDbType.NVarChar, 500),
        };

            parameters[0].Value = autoid;
            parameters[1].Value = code;
            parameters[2].Value = TieuDe;
            parameters[3].Value = NoiDung;
            parameters[4].Value = Description_SEO;

            parameters[5].Value = Keywords_SEO;
            parameters[6].Value = NgayBD;
            parameters[7].Value = NgayKT;
            parameters[8].Value = hinhAnh;
            parameters[9].Value = idHinhThuc;


            parameters[10].Value = IdLoaiBDS;
            parameters[11].Value = IdTinh;
            parameters[12].Value = IdQuan;
            parameters[13].Value = IdXa;

            parameters[14].Value = IdPho;
            parameters[15].Value = IdDuAn;
            parameters[16].Value = DienTich;
            parameters[17].Value = Gia;
            parameters[18].Value = DonViGia;

            parameters[19].Value = TenLienHe;
            parameters[20].Value = DiaChi;
            parameters[21].Value = DiaChiLienHe;
            parameters[22].Value = DienThoai;
            parameters[23].Value = Email;
            parameters[24].Value = BanDo;

            parameters[25].Value = MatTien;
            parameters[26].Value = DuongVao;
            parameters[27].Value = HuongNha;

            parameters[28].Value = HuongBanCong;
            parameters[29].Value = SoTang;

            parameters[30].Value = SoPhongNgu;
            parameters[31].Value = SoToilet;
            parameters[32].Value = noiThat;
            parameters[33].Value = NguoiDang;
            parameters[34].Value = LoaiTin;

            parameters[35].Value = LoaiRao;
            parameters[36].Value = isActived;
            parameters[37].Value = sql;
            return ExecuteScalar("[BDSinsert_update_delete_TinBDS]", parameters);
        }
        public DataTable BDSget_TinBDS(int autoid, DateTime? NgayBD, DateTime? NgayKT, int idHinhThuc, int IdLoaiBDS, int IdTinh, int IdQuan,
            int IdDuAn, double DienTich, double DienTichDen, double GiaTu, double GiaDen, string Email, string NguoiDang, int LoaiTin, int LoaiRao, bool? isActived, string sql)
        {
            SqlParameter[] parameters = new SqlParameter[] {
            new SqlParameter("@Id", SqlDbType.Int),

            new SqlParameter("@NgayBD", SqlDbType.DateTime),
             new SqlParameter("@NgayKT", SqlDbType.DateTime),
            new SqlParameter("@idHinhThuc", SqlDbType.Int),

            new SqlParameter("@IdLoaiBDS", SqlDbType.Int, 500),
            new SqlParameter("@IdTinh", SqlDbType.Int, 500),
            new SqlParameter("@IdQuan", SqlDbType.Int, 500),

            new SqlParameter("@IdDuAn", SqlDbType.Int),
            new SqlParameter("@DienTichTu", SqlDbType.Float),
            new SqlParameter("@DienTichDen", SqlDbType.Float),
            new SqlParameter("@Gia", SqlDbType.Float, 500),
            new SqlParameter("@GiaDen", SqlDbType.Float, 500),
            new SqlParameter("@Email", SqlDbType.NVarChar, 500),
            new SqlParameter("@NguoiDang", SqlDbType.NVarChar, 500),
            new SqlParameter("@LoaiTin", SqlDbType.Int, 500),
            new SqlParameter("@LoaiRao", SqlDbType.Int, 500),
            new SqlParameter("@IsActived", SqlDbType.Bit),
            new SqlParameter("@sql", SqlDbType.NVarChar, 500),
        };

            parameters[0].Value = autoid;
            parameters[1].Value = NgayBD;
            parameters[2].Value = NgayKT;
            parameters[3].Value = idHinhThuc;
            parameters[4].Value = IdLoaiBDS;
            parameters[5].Value = IdTinh;
            parameters[6].Value = IdQuan;
            parameters[7].Value = IdDuAn;
            parameters[8].Value = DienTich;
            parameters[9].Value = DienTichDen;
            parameters[10].Value = GiaTu;
            parameters[11].Value = GiaDen;
            parameters[12].Value = Email;
            parameters[13].Value = NguoiDang;
            parameters[14].Value = LoaiTin;
            parameters[15].Value = LoaiRao;
            parameters[16].Value = isActived;
            parameters[17].Value = sql;
            return GetDataTable("[BDSget_TinBDS]", parameters);
        }
        public DataTable BDSget_Data_TinBDS(int autoid,
            string code, string TieuDe, string NoiDung, string Description_SEO, string Keywords_SEO, DateTime? NgayBD, DateTime? NgayKT, string hinhAnh, int idHinhThuc,
            int IdLoaiBDS, int IdTinh, int IdQuan, int IdXa, int IdPho, int IdDuAn, double DienTich, double DienTichDen, double GiaTu, double GiaDen, string DonViGia, string TenLienHe,
            string DiaChi, string DienThoai, string Email, string BanDo, int MatTien, int DuongVao, int HuongNha, int HuongBanCong,
            int SoTang, int SoPhongNgu, int SoToilet, string noiThat, string NguoiDang, int LoaiTin, int LoaiRao, bool? isActived, string sql)
        {
            SqlParameter[] parameters = new SqlParameter[] {
            new SqlParameter("@Id", SqlDbType.Int),
            new SqlParameter("@code", SqlDbType.NVarChar),
            new SqlParameter("@TieuDe", SqlDbType.NVarChar, 500),
            new SqlParameter("@NoiDung", SqlDbType.NVarChar, 500),
            new SqlParameter("@Description_SEO", SqlDbType.NVarChar, 500),
            new SqlParameter("@Keywords_SEO", SqlDbType.NVarChar, 500),
            new SqlParameter("@NgayBD", SqlDbType.DateTime),
             new SqlParameter("@NgayKT", SqlDbType.DateTime),
            new SqlParameter("@hinhAnh", SqlDbType.NVarChar),
            new SqlParameter("@idHinhThuc", SqlDbType.Int),

            new SqlParameter("@IdLoaiBDS", SqlDbType.Int, 500),
            new SqlParameter("@IdTinh", SqlDbType.Int, 500),
            new SqlParameter("@IdQuan", SqlDbType.Int, 500),
            new SqlParameter("@IdXa", SqlDbType.Int, 500),
            new SqlParameter("@IdPho", SqlDbType.Int, 500),

            new SqlParameter("@IdDuAn", SqlDbType.Int),
            new SqlParameter("@DienTichTu", SqlDbType.Float),
            new SqlParameter("@DienTichDen", SqlDbType.Float),
            new SqlParameter("@Gia", SqlDbType.Float, 500),
            new SqlParameter("@GiaDen", SqlDbType.Float, 500),
            new SqlParameter("@DonViGia", SqlDbType.NVarChar),
            new SqlParameter("@TenLienHe", SqlDbType.NVarChar, 500),

            new SqlParameter("@DiaChi", SqlDbType.NVarChar, 500),
            new SqlParameter("@DienThoai", SqlDbType.NVarChar, 500),
            new SqlParameter("@Email", SqlDbType.NVarChar, 500),
            new SqlParameter("@BanDo", SqlDbType.NVarChar, 500),
            new SqlParameter("@MatTien", SqlDbType.Int, 500),

            new SqlParameter("@DuongVao", SqlDbType.Int, 500),
            new SqlParameter("@HuongNha", SqlDbType.Int, 500),
            new SqlParameter("@HuongBanCong", SqlDbType.Int, 500),
            new SqlParameter("@SoTang", SqlDbType.Int, 500),
            new SqlParameter("@SoPhongNgu", SqlDbType.Int, 500),

            new SqlParameter("@SoToilet", SqlDbType.Int, 500),
            new SqlParameter("@NoiThat", SqlDbType.NVarChar, 500),
            new SqlParameter("@NguoiDang", SqlDbType.NVarChar, 500),
            new SqlParameter("@LoaiTin", SqlDbType.Int, 500),
            new SqlParameter("@LoaiRao", SqlDbType.Int, 500),
            new SqlParameter("@IsActived", SqlDbType.Bit),
            new SqlParameter("@sql", SqlDbType.NVarChar, 500),
        };

            parameters[0].Value = autoid;
            parameters[1].Value = code;
            parameters[2].Value = TieuDe;
            parameters[3].Value = NoiDung;
            parameters[4].Value = Description_SEO;

            parameters[5].Value = Keywords_SEO;
            parameters[6].Value = NgayBD;
            parameters[7].Value = NgayKT;
            parameters[8].Value = hinhAnh;
            parameters[9].Value = idHinhThuc;


            parameters[10].Value = IdLoaiBDS;
            parameters[11].Value = IdTinh;
            parameters[12].Value = IdQuan;
            parameters[13].Value = IdXa;

            parameters[14].Value = IdPho;
            parameters[15].Value = IdDuAn;
            parameters[16].Value = DienTich;
            parameters[17].Value = DienTichDen;
            parameters[18].Value = GiaTu;
            parameters[19].Value = GiaDen;
            parameters[20].Value = DonViGia;

            parameters[21].Value = TenLienHe;
            parameters[22].Value = DiaChi;
            parameters[23].Value = DienThoai;
            parameters[24].Value = Email;
            parameters[25].Value = BanDo;

            parameters[26].Value = MatTien;
            parameters[27].Value = DuongVao;
            parameters[28].Value = HuongNha;

            parameters[29].Value = HuongBanCong;
            parameters[30].Value = SoTang;

            parameters[31].Value = SoPhongNgu;
            parameters[32].Value = SoToilet;
            parameters[33].Value = noiThat;
            parameters[34].Value = NguoiDang;
            parameters[35].Value = LoaiTin;

            parameters[36].Value = LoaiRao;
            parameters[37].Value = isActived;
            parameters[38].Value = sql;
            return GetDataTable("[BDSget_Data_TinBDS]", parameters);
        }
        public object BDSinsert_update_delete_Huong(int id, string name, string sql)
        {
            SqlParameter[] parameters = new SqlParameter[] {

            new SqlParameter("@id", SqlDbType.Int),
            new SqlParameter("@name", SqlDbType.NVarChar, 500),
            new SqlParameter("@sql", SqlDbType.NVarChar, 500),
        };
            parameters[0].Value = id;
            parameters[1].Value = name;
            parameters[2].Value = sql;
            return ExecuteScalar("[BDSinsert_update_delete_Huong]", parameters);
        }
        //[]
        public object BDSinsert_update_delete_TaiKhoan(int id, string username, string sotien, string tienno, string ghichu, string sql)
        {
            SqlParameter[] parameters = new SqlParameter[] {
           new SqlParameter("@id", SqlDbType.Int),
            new SqlParameter("@username", SqlDbType.NVarChar,500),
            new SqlParameter("@sotien", SqlDbType.Float,500),
            new SqlParameter("@tienno", SqlDbType.Float,500),
            new SqlParameter("@ghichu", SqlDbType.NVarChar,500),
            new SqlParameter("@sql", SqlDbType.NVarChar, 500),
        };
            parameters[0].Value = id;
            parameters[1].Value = username;
            parameters[2].Value = sotien;
            parameters[3].Value = tienno;
            parameters[4].Value = ghichu;
            parameters[5].Value = sql;
            return ExecuteScalar("BDSinsert_update_delete_TaiKhoan", parameters);
        }
        public object BDSinsert_update_delete_ThanhToan(int id, string username, int idTin, string sotien, DateTime ngayThanhToan, string sql)
        {
            SqlParameter[] parameters = new SqlParameter[] {
           new SqlParameter("@id", SqlDbType.Int),
            new SqlParameter("@username", SqlDbType.NVarChar,500),
            new SqlParameter("@sotien", SqlDbType.Float,500),
            new SqlParameter("@tienno", SqlDbType.Float,500),
            new SqlParameter("@ghichu", SqlDbType.NVarChar,500),
            new SqlParameter("@sql", SqlDbType.NVarChar, 500),
        };
            parameters[0].Value = id;
            parameters[1].Value = username;
            parameters[2].Value = idTin;
            parameters[3].Value = sotien;
            parameters[4].Value = ngayThanhToan;
            parameters[5].Value = sql;
            return ExecuteScalar("BDSinsert_update_delete_ThanhToan", parameters);
        }
        public DataTable BDSget_TinBDS_HetHan(DateTime? date, string user)
        {
            SqlParameter[] parameters = new SqlParameter[] {

            new SqlParameter("@date", SqlDbType.DateTime),
            new SqlParameter("@user", SqlDbType.NVarChar,100)
        };
            parameters[0].Value = date;
            parameters[1].Value = user;
            return GetDataTable("[BDSget_TinBDS_HetHan]", parameters);
        }
        public DataTable BDSGet_All_Huong()
        {
            return GetDataTable("BDSGet_All_Huong");
        }
        public DataRow BDSGet_Info_Huong(int id)
        {
            SqlParameter[] parameters = new SqlParameter[] {

            new SqlParameter("@id", SqlDbType.Int),
        };
            parameters[0].Value = id;

            return GetDataRow("BDSGet_Info_Huong", id);
        }
        public object BDSinsert_update_delete_Comment(int id, string noidung, int idTopic, string username, DateTime thoigian, string sql)
        {
            SqlParameter[] parameters = new SqlParameter[] {

            new SqlParameter("@id", SqlDbType.Int),
            new SqlParameter("@noidung", SqlDbType.NVarChar, 500),
            new SqlParameter("@idTopic", SqlDbType.Int),
            new SqlParameter("@username", SqlDbType.NVarChar, 500),
            new SqlParameter("@thoigian", SqlDbType.DateTime, 500),
            new SqlParameter("@sql", SqlDbType.NVarChar, 500),
        };
            parameters[0].Value = id;
            parameters[1].Value = noidung;
            parameters[2].Value = idTopic;
            parameters[3].Value = username;
            parameters[4].Value = thoigian;
            parameters[5].Value = sql;
            return ExecuteScalar("[BDSinsert_update_delete_Comment]", parameters);
        }
        public DataTable BDSget_all_Comment()
        {
            return GetDataTable("[BDSget_all_Comment]");
        }
        public DataTable BDSget_Data_Comment(int id, int idTopic, string username)
        {
            SqlParameter[] parameters = new SqlParameter[] {
            new SqlParameter("@id", SqlDbType.Int),
            new SqlParameter("@idTopic", SqlDbType.Int),
            new SqlParameter("@Username", SqlDbType.NVarChar,500),
        };
            parameters[0].Value = id;
            parameters[1].Value = idTopic;
            parameters[2].Value = username;
            return GetDataTable("[BDSget_info_Comment]", parameters);
        }
        public DataTable BDSget_Data_TaiKhoan(int id, string username, string sotien, string tienno, string ghichu, string sql)
        {
            SqlParameter[] parameters = new SqlParameter[] {
           new SqlParameter("@id", SqlDbType.Int),
            new SqlParameter("@username", SqlDbType.NVarChar,500),
            new SqlParameter("@sotien", SqlDbType.Float,500),
            new SqlParameter("@tienno", SqlDbType.Float,500),
            new SqlParameter("@ghichu", SqlDbType.NVarChar,500),
            new SqlParameter("@sql", SqlDbType.NVarChar, 500),
        };
            parameters[0].Value = id;
            parameters[1].Value = username;
            parameters[2].Value = sotien;
            parameters[3].Value = tienno;
            parameters[4].Value = ghichu;
            parameters[5].Value = sql;
            return GetDataTable("BDSget_Data_TaiKhoan", parameters);
        }
        public DataTable BDSget_Data_ThanhToan(int id, string username, int idTin, string sotien, DateTime ngayThanhToan, string sql)
        {
            SqlParameter[] parameters = new SqlParameter[] {
           new SqlParameter("@id", SqlDbType.Int),
            new SqlParameter("@username", SqlDbType.NVarChar,500),
            new SqlParameter("@sotien", SqlDbType.Float,500),
            new SqlParameter("@tienno", SqlDbType.Float,500),
            new SqlParameter("@ghichu", SqlDbType.NVarChar,500),
            new SqlParameter("@sql", SqlDbType.NVarChar, 500),
        };
            parameters[0].Value = id;
            parameters[1].Value = username;
            parameters[2].Value = idTin;
            parameters[3].Value = sotien;
            parameters[4].Value = ngayThanhToan;
            parameters[5].Value = sql;
            return GetDataTable("BDSget_Data_ThanhToan", parameters);
        }
        //-----------------------------------------------------------------------------------------------------
        public object BDSinsert_update_delete_HinhThuc(int id, string name, string sql)
        {
            SqlParameter[] parameters = new SqlParameter[] {

            new SqlParameter("@id", SqlDbType.Int),
            new SqlParameter("@name", SqlDbType.NVarChar, 500),
            new SqlParameter("@sql", SqlDbType.NVarChar, 500),
        };
            parameters[0].Value = id;
            parameters[1].Value = name;
            parameters[2].Value = sql;
            return ExecuteScalar("BDSinsert_update_delete_HinhThuc", parameters);
        }
        public DataTable BDSget_all_HinhThuc()
        {
            return GetDataTable("BDSget_all_HinhThuc");
        }
        public DataRow BDSget_info_HinhThuc(int id)
        {
            SqlParameter[] parameters = new SqlParameter[] {

            new SqlParameter("@id", SqlDbType.Int),
        };
            parameters[0].Value = id;

            return GetDataRow("BDSget_info_HinhThuc", id);
        }

        //-----------------------------------------
        public object BDSinsert_update_delete_NhanTin(int id, string email, int tinh, int quan, int hinthuc, int loai, int duan, string sql)
        {
            SqlParameter[] parameters = new SqlParameter[] {

            new SqlParameter("@id", SqlDbType.Int),
            new SqlParameter("@email", SqlDbType.NVarChar, 500),
            new SqlParameter("@idTinh", SqlDbType.Int),
            new SqlParameter("@idQuan", SqlDbType.Int),
            new SqlParameter("@idHinhThuc", SqlDbType.Int),
            new SqlParameter("@idLoai", SqlDbType.Int),
            new SqlParameter("@idDuAn", SqlDbType.Int),
            new SqlParameter("@sql", SqlDbType.NVarChar, 500),
        };
            parameters[0].Value = id;
            parameters[1].Value = email;
            parameters[2].Value = tinh;
            parameters[3].Value = quan;
            parameters[4].Value = hinthuc;
            parameters[5].Value = loai;
            parameters[6].Value = duan;
            parameters[7].Value = sql;
            return ExecuteScalar("[BDSinsert_update_delete_NhanTin]", parameters);
        }
        public DataRow BDSget_Info_NhanTin(int id, string name, int tinh, int quan, int hinthuc, int loai, int duan)
        {
            SqlParameter[] parameters = new SqlParameter[] {

            new SqlParameter("@id", SqlDbType.Int),
            new SqlParameter("@email", SqlDbType.NVarChar, 500),
            new SqlParameter("@idTinh", SqlDbType.Int),
            new SqlParameter("@idQuan", SqlDbType.Int),
            new SqlParameter("@idHinhThuc", SqlDbType.Int),
            new SqlParameter("@idLoai", SqlDbType.Int),
            new SqlParameter("@idDuAn", SqlDbType.Int),
        };
            parameters[0].Value = id;
            parameters[1].Value = name;
            parameters[2].Value = tinh;
            parameters[3].Value = quan;
            parameters[4].Value = hinthuc;
            parameters[5].Value = loai;
            parameters[6].Value = duan;
            return GetDataRow("[BDSget_Info_NhanTin]", parameters);
        }
        public DataTable BDSget_Data_NhanTin(int id, string name, int tinh, int quan, int hinthuc, int loai, int duan)
        {
            SqlParameter[] parameters = new SqlParameter[] {

            new SqlParameter("@id", SqlDbType.Int),
            new SqlParameter("@email", SqlDbType.NVarChar, 500),
            new SqlParameter("@idTinh", SqlDbType.Int),
            new SqlParameter("@idQuan", SqlDbType.Int),
            new SqlParameter("@idHinhThuc", SqlDbType.Int),
            new SqlParameter("@idLoai", SqlDbType.Int),
            new SqlParameter("@idDuAn", SqlDbType.Int),
        };
            parameters[0].Value = id;
            parameters[1].Value = name;
            parameters[2].Value = tinh;
            parameters[3].Value = quan;
            parameters[4].Value = hinthuc;
            parameters[5].Value = loai;
            parameters[6].Value = duan;
            return GetDataTable("[BDSget_Info_NhanTin]", parameters);
        }
        public DataTable BDSget_all_NhanTin()
        {
            return GetDataTable("[BDSget_all_NhanTin]");
        }
        public DataRow BDSget_info_DuAn(int id, string name, int tinh, int quan, int xa, int pho)
        {
            SqlParameter[] parameters = new SqlParameter[] {

            new SqlParameter("@id", SqlDbType.Int),
            new SqlParameter("@name", SqlDbType.NVarChar, 500),
            new SqlParameter("@idTinh", SqlDbType.Int),
            new SqlParameter("@idQuan", SqlDbType.Int),
            new SqlParameter("@idXa", SqlDbType.Int),
            new SqlParameter("@idPho", SqlDbType.Int),
        };
            parameters[0].Value = id;
            parameters[1].Value = name;
            parameters[2].Value = tinh;
            parameters[3].Value = quan;
            parameters[4].Value = xa;
            parameters[5].Value = pho;

            return GetDataRow("BDSget_info_DuAn", parameters);
        }
        public DataRow get_Info_user_email(string email)
        {
            SqlParameter[] parameters = new SqlParameter[] {

            new SqlParameter("@email", SqlDbType.Int),
        };
            parameters[0].Value = email;
            return GetDataRow("get_Info_user_email", parameters);
        }
        //------------------------------------------------------------
        public object BDSinsert_update_delete_DuAn(int id, string name, int tinh, int quan, int xa, int pho, string sql)
        {
            SqlParameter[] parameters = new SqlParameter[] {

            new SqlParameter("@id", SqlDbType.Int),
            new SqlParameter("@name", SqlDbType.NVarChar, 500),
            new SqlParameter("@idTinh", SqlDbType.Int),
            new SqlParameter("@idQuan", SqlDbType.Int),
            new SqlParameter("@idXa", SqlDbType.Int),
            new SqlParameter("@idPho", SqlDbType.Int),
            new SqlParameter("@sql", SqlDbType.NVarChar, 500),
        };
            parameters[0].Value = id;
            parameters[1].Value = name;
            parameters[2].Value = tinh;
            parameters[3].Value = quan;
            parameters[4].Value = xa;
            parameters[5].Value = pho;
            parameters[6].Value = sql;
            return ExecuteScalar("[BDSinsert_update_delete_DuAn]", parameters);
        }
        public DataTable BDSget_all_DuAn()
        {
            return GetDataTable("BDSget_all_DuAn");
        }
        public DataTable BDSget_Data_DuAn(int id, string name, int tinh, int quan, int xa, int pho)
        {
            SqlParameter[] parameters = new SqlParameter[] {

            new SqlParameter("@id", SqlDbType.Int),
            new SqlParameter("@name", SqlDbType.NVarChar, 500),
            new SqlParameter("@idTinh", SqlDbType.Int),
            new SqlParameter("@idQuan", SqlDbType.Int),
            new SqlParameter("@idXa", SqlDbType.Int),
            new SqlParameter("@idPho", SqlDbType.Int),
        };
            parameters[0].Value = id;
            parameters[1].Value = name;
            parameters[2].Value = tinh;
            parameters[3].Value = quan;
            parameters[4].Value = xa;
            parameters[5].Value = pho;

            return GetDataTable("BDSget_info_DuAn", parameters);
        }
        //public DataRow BDSget_info_DuAn(int id, string name,int tinh,int quan, int xa, int pho)
        //{
        //    SqlParameter[] parameters = new SqlParameter[] { 

        //        new SqlParameter("@id", SqlDbType.Int),
        //        new SqlParameter("@name", SqlDbType.NVarChar, 500),
        //        new SqlParameter("@idTinh", SqlDbType.Int),
        //        new SqlParameter("@idQuan", SqlDbType.Int),
        //        new SqlParameter("@idXa", SqlDbType.Int),
        //        new SqlParameter("@idPho", SqlDbType.Int),
        //    };
        //    parameters[0].Value = id;
        //    parameters[1].Value = name;
        //    parameters[2].Value = tinh;
        //    parameters[3].Value = quan;
        //    parameters[4].Value = xa;
        //    parameters[5].Value = pho;

        //    return GetDataRow("BDSget_info_DuAn", parameters);
        //}
        //[]
        public DataRow BDSget_info_TinBDS(int id)
        {
            SqlParameter[] parameters = new SqlParameter[] {

            new SqlParameter("@id", SqlDbType.Int)

        };
            parameters[0].Value = id;
            return GetDataRow("BDSget_info_TinBDS", parameters);
        }
        //-----------------------------------------------------------------------------------------------------
        public object BDSinsert_update_delete_LoaiBDS(int id, string name, int HinhThuc, string sql)
        {
            SqlParameter[] parameters = new SqlParameter[] {

            new SqlParameter("@id", SqlDbType.Int),
            new SqlParameter("@name", SqlDbType.NVarChar, 500),
            new SqlParameter("@idHinhThuc", SqlDbType.Int),
            new SqlParameter("@sql", SqlDbType.NVarChar, 500),
        };
            parameters[0].Value = id;
            parameters[1].Value = name;
            parameters[2].Value = HinhThuc;
            parameters[3].Value = sql;
            return ExecuteScalar("[BDSinsert_update_delete_LoaiBDS]", parameters);
        }
        public DataTable BDSget_all_LoaiBDS()
        {
            return GetDataTable("BDSget_all_Loai");
        }
        public DataRow BDSget_info_LoaiBDS(int id, string name, int HinhThuc)
        {
            SqlParameter[] parameters = new SqlParameter[] {

            new SqlParameter("@id", SqlDbType.Int),
            new SqlParameter("@name", SqlDbType.NVarChar, 500),
            new SqlParameter("@idHinhThuc", SqlDbType.Int),
        };
            parameters[0].Value = id;
            parameters[1].Value = name;
            parameters[2].Value = HinhThuc;

            return GetDataRow("BDSget_info_Loai", parameters);
        }
        public DataTable BDSget_Data_LoaiBDS(int id, string name, int HinhThuc)
        {
            SqlParameter[] parameters = new SqlParameter[] {

            new SqlParameter("@id", SqlDbType.Int),
            new SqlParameter("@name", SqlDbType.NVarChar, 500),
            new SqlParameter("@idHinhThuc", SqlDbType.Int),
        };
            parameters[0].Value = id;
            parameters[1].Value = name;
            parameters[2].Value = HinhThuc;

            return GetDataTable("BDSget_info_Loai", parameters);
        }
        //-----------------------------------------------------------------------------------------------------
        public object BDSinsert_update_delete_LoaiRao(int id, string name, decimal Gia, string sql)
        {
            SqlParameter[] parameters = new SqlParameter[] {

            new SqlParameter("@id", SqlDbType.Int),
            new SqlParameter("@name", SqlDbType.NVarChar, 500),
            new SqlParameter("@Gia", SqlDbType.Decimal),
            new SqlParameter("@sql", SqlDbType.NVarChar, 500),
        };
            parameters[0].Value = id;
            parameters[1].Value = name;
            parameters[2].Value = Gia;
            parameters[3].Value = sql;
            return ExecuteScalar("[BDSinsert_update_delete_LoaiRao]", parameters);
        }
        public DataTable BDSget_all_LoaiRao()
        {
            return GetDataTable("BDSget_all_LoaiRao");
        }
        public DataRow BDSget_Info_LoaiRao(int id, string name, decimal gia)
        {
            SqlParameter[] parameters = new SqlParameter[] {

            new SqlParameter("@id", SqlDbType.Int),
            new SqlParameter("@name", SqlDbType.NVarChar, 500),
            new SqlParameter("@idHinhThuc", SqlDbType.Decimal),
        };
            parameters[0].Value = id;
            parameters[1].Value = name;
            parameters[2].Value = gia;

            return GetDataRow("BDSget_Info_LoaiRao", parameters);
        }
        #endregion


    }
}