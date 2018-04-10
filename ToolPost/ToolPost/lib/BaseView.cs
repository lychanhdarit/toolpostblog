using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ToolPost.lib
{
    public class BaseView
    {
        public BaseView()
        {

        }
        public static void PrintWebControl(Control ctrl)
        {
            PrintWebControl(ctrl, string.Empty);
        }
        public static string ReplaceSpace(string tempo)
        {
            RegexOptions options = RegexOptions.None;
            Regex regex = new Regex("[ ]{2,}", options);
            return regex.Replace(tempo, " ");
        }
        public static string ReplaceBlog(string tempo)
        {
            RegexOptions options = RegexOptions.None;
            Regex regex = new Regex("[ ]{2,}", options);
            tempo = regex.Replace(tempo, " ");

            return tempo.Replace("\n", "").Replace("\r", "");

        }
        public static int sotin()
        {
            DBClass _db = new DBClass();
            DataRow dr = _db.get_info_caidat();
            int i = 0;
            if (dr != null)
            {
                i = BaseView.GetIntFieldValue(dr, "SoTin");
            }
            return i;
        }

        public static int solinks()
        {
            DBClass _db = new DBClass();
            DataRow dr = _db.get_info_caidat();
            int i = 0;
            if (dr != null)
            {
                i = BaseView.GetIntFieldValue(dr, "SoLink");
            }
            return i;
        }
        public static int songay()
        {
            DBClass _db = new DBClass();
            DataRow dr = _db.get_info_caidat();
            int i = 0;
            if (dr != null)
            {
                i = BaseView.GetIntFieldValue(dr, "SoNgay");
            }
            return i;
        }
        public static bool CheckImg(string url)
        {
            try
            {
                //Creating the HttpWebRequest
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                //Setting the Request method HEAD, you can also use GET too.
                request.Method = "HEAD";
                //Getting the Web Response.
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                //Returns TURE if the Status code == 200
                return (response.StatusCode == HttpStatusCode.OK);
            }
            catch
            {
                //Any exception will returns false.
                return false;
            }
        }
        public static bool KiemTraImages(string url)
        {
            //bool result = false;
            //using (WebClient client = new WebClient())
            //{
            //    try
            //    {
            //        Stream stream = client.OpenRead(url);
            //        if (stream != null)
            //        {
            //            result = true;
            //        }
            //        else
            //        {
            //            result = false;
            //        }
            //    }
            //    catch
            //    {
            //        //Any exception will returns false.
            //        result = false;
            //    }
            //}
            //return result;
            return true;
        }
        //public static string serverUrl = "http://192.168.1.15"; //"http://lamdep123.org";
        public string serverUrl()
        {
            DBClass _db = new DBClass();
            DataRow row = _db.get_info_url();
            if (row != null)
            {
                return BaseView.GetStringFieldValue(row, "links");
            }
            return "";
        }
        public static string UrlServer()
        {
            DBClass _db = new DBClass();
            DataRow row = _db.get_info_url();
            if (row != null)
            {
                return BaseView.GetStringFieldValue(row, "links");
            }
            return "";
        }
        public static DateTime getDateTimeNow()
        {
            DateTime thisTime = DateTime.Now;
            string timeString = "";
            TimeZoneInfo tst = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
            DateTime tstTime = TimeZoneInfo.ConvertTime(thisTime, TimeZoneInfo.Local, tst);
            if (tst.IsDaylightSavingTime(tstTime))
            {
                timeString = tst.DaylightName;
            }
            else
                timeString = tst.StandardName;

            return tstTime;
        }
        public static string repalce_UrlFriendly(string s)
        {
            s = s.Replace("/", "-");
            s = s.Replace("'", "-");
            s = s.Replace(" ", "-");
            s = s.Replace(@"\", "-");
            s = s.Replace("?", "-");
            s = s.Replace(".", "-");
            s = s.Replace("^", "-");
            s = s.Replace("_", "-");
            s = s.Replace("~", "-");
            s = s.Replace("&", "-");
            s = s.Replace("*", "-");
            s = s.Replace("@", "-");
            s = s.Replace("!", "-");
            s = s.Replace("%", "-");
            s = s.Replace("#", "-");
            s = s.Replace("+", "-");
            s = s.Replace("`", "-");
            s = s.Replace("|", "-");
            s = s.Replace(",", "-");
            s = s.Replace("<", "-");
            s = s.Replace(">", "-");
            s = s.Replace("=", "-");
            s = s.Replace("'", "-");
            s = s.Replace(";", "-");
            s = s.Replace(":", "-");
            s = s.Replace("---", "-");
            s = s.Replace("--", "-");
            s = s.Replace("[", "");
            s = s.Replace("]", "");
            return s;
        }
        public static string convertToUnSign2(string s)
        {
            string stFormD = s.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();
            for (int ich = 0; ich < stFormD.Length; ich++)
            {
                System.Globalization.UnicodeCategory uc = System.Globalization.CharUnicodeInfo.GetUnicodeCategory(stFormD[ich]);
                if (uc != System.Globalization.UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(stFormD[ich]);
                }
            }
            sb = sb.Replace('Đ', 'D');
            sb = sb.Replace('đ', 'd');
            return (sb.ToString().Normalize(NormalizationForm.FormD));
        }
        public static string convertStringLinks(string s)
        {
            s = BaseView.convertToUnSign2(s);
            s = BaseView.repalce_UrlFriendly(s);
            return (s.ToLower());
        }
        public static string replaceLinkHtml(string data)
        {
            return data.Replace("<a", "< aa");
        }
        public static string RemoveLinks(string htmlString)
        {
            return Regex.Replace(htmlString, "</?(a|A).*?>", "");
        }
        public static string RemoveHtmlTagsUsingRegex(string htmlString)
        {
            var result = Regex.Replace(htmlString, "<.*?>", string.Empty);
            return result;
        }
        public static string RemoveKiTuDacBietVaKhoangTrang(string htmlString)
        {
            htmlString = htmlString.Replace(" ", "-");
            return Regex.Replace(htmlString, "[^a-zA-Z0-9]", "-");
        }
        static readonly Regex HtmlRegex = new Regex("<.*?>", RegexOptions.Compiled);

        public static string RemoveHtmlTagsUsingCompiledRegex(string htmlString)
        {
            var result = HtmlRegex.Replace(htmlString, string.Empty);
            return result;
        }
        public static string RemoveHtmlTagsUsingCharArray(string htmlString)
        {
            var array = new char[htmlString.Length];
            var arrayIndex = 0;
            var inside = false;

            foreach (var @let in htmlString)
            {
                if (let == '<')
                {
                    inside = true;
                    continue;
                }
                if (let == '>')
                {
                    inside = false;
                    continue;
                }
                if (inside) continue;
                array[arrayIndex] = let;
                arrayIndex++;
            }
            return new string(array, 0, arrayIndex);
        }
        public static byte[] encryptData(string data)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider md5Hasher = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] hashedBytes;
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            hashedBytes = md5Hasher.ComputeHash(encoder.GetBytes(data));
            return hashedBytes;
        }
        public static string md5(string data)
        {
            return BitConverter.ToString(encryptData(data)).Replace("-", "").ToLower();
        }
        public static void PrintWebControl(Control ctrl, string Script)
        {
            StringWriter stringWrite = new StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new System.Web.UI.HtmlTextWriter(stringWrite);
            if (ctrl is WebControl)
            {
                Unit w = new Unit(100, UnitType.Percentage); ((WebControl)ctrl).Width = w;
            }
            Page pg = new Page();
            pg.EnableEventValidation = false;
            if (Script != string.Empty)
            {
                pg.ClientScript.RegisterStartupScript(pg.GetType(), "PrintJavaScript", Script);
            }
            HtmlForm frm = new HtmlForm();
            pg.Controls.Add(frm);
            frm.Attributes.Add("runat", "server");
            frm.Controls.Add(ctrl);
            pg.DesignerInitialize();
            pg.RenderControl(htmlWrite);
            string strHTML = stringWrite.ToString();
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Write(strHTML);
            HttpContext.Current.Response.Write("<script>window.print();</script>");
            HttpContext.Current.Response.End();
        }
        public static void SelectedTreeView(TreeView tree, int parentnodeindex, int nodeindex = -1)
        {
            tree.Nodes[parentnodeindex].Expand();
            if (nodeindex != -1)
            {
                tree.Nodes[parentnodeindex].ChildNodes[nodeindex].Selected = true;
            }
            else
            {
                tree.Nodes[parentnodeindex].Selected = true;
            }
        }
        public static void BindDataToDropdownList(DropDownList list, DataTable data, bool hasNone = true)
        {
            list.DataSource = data;
            list.DataBind();

            if (hasNone)
            {
                AddBlankDropdownItem(list, "0");
            }
        }

        public static void AddBlankDropdownItem(DropDownList list, string value = "")
        {
            if (list.Items.FindByValue(value) != null)
            {
                list.Items.Remove(new ListItem("", value));
            }
            list.Items.Insert(0, new ListItem("", value));
        }
        public static void AddBlankDropdownItem(DropDownList list, string text, string value)
        {
            if (list.Items.FindByValue(value) != null)
            {
                list.Items.Remove(new ListItem(text, value));
            }
            list.Items.Insert(0, new ListItem(text, value));
        }
        public static void BindDataToListBox(ListBox list, DataTable data)
        {
            list.DataSource = data;
            list.DataBind();
        }

        public static void SelectDropdownItem(DropDownList list, object obj)
        {
            string value = (obj != DBNull.Value ? Convert.ToString(obj) : "");

            ListItem item = list.Items.FindByValue(value);
            if (item != null)
            {
                item.Selected = true;
            }
        }

        public static bool SelectDropdownItem(DropDownList list, object obj, object enable)
        {
            SelectDropdownItem(list, obj);
            list.Enabled = (enable != DBNull.Value ? Convert.ToBoolean(enable) : false);
            return list.Enabled;
        }
        public static bool CheckImageFileType(string fileName)
        {
            string ext = Path.GetExtension(fileName);

            switch (ext.ToLower())
            {
                case ".gif":
                    return true;

                case ".png":
                    return true;

                case ".jpg":
                    return true;

                case ".jpeg":
                    return true;

                default:
                    return false;
            }
        }

        public static string Html2Text(string value)
        {
            return value.Replace("<br />", "\r");
        }

        public static string Text2Html(string value)
        {
            return value.Replace("\r", "<br />");
        }

        #region DataView
        public static object GetFieldValue(DataRowView row, string FieldName)
        {
            return (row.Row.Table.Columns.Contains(FieldName) ? row[FieldName] : null);
        }
        public static string GetStringFieldValue(DataRowView row, string FieldName)
        {
            return (row.Row.Table.Columns.Contains(FieldName) && row[FieldName] != DBNull.Value ? Convert.ToString(row[FieldName]) : "");
        }
        public static int GetIntFieldValue(DataRowView row, string FieldName)
        {
            return (row.Row.Table.Columns.Contains(FieldName) && row[FieldName] != DBNull.Value ? Convert.ToInt32(row[FieldName]) : 0);
        }
        public static float GetFloatFieldValue(DataRowView row, string FieldName)
        {
            return (row.Row.Table.Columns.Contains(FieldName) && row[FieldName] != DBNull.Value ? Convert.ToSingle(row[FieldName]) : 0);
        }
        public static bool GetBooleanFieldValue(DataRowView row, string FieldName)
        {
            return (row.Row.Table.Columns.Contains(FieldName) && row[FieldName] != DBNull.Value ? Convert.ToBoolean(row[FieldName]) : false);
        }
        public static DateTime GetDateTimeFieldValue(DataRowView row, string FieldName)
        {
            return (row.Row.Table.Columns.Contains(FieldName) && row[FieldName] != DBNull.Value ? Convert.ToDateTime(row[FieldName]) : DateTime.MinValue);
        }

        public static object GetFieldValue(DataRow row, string FieldName)
        {
            return (row.Table.Columns.Contains(FieldName) ? row[FieldName] : null);
        }
        public static string GetStringFieldValue(DataRow row, string FieldName)
        {
            return (row.Table.Columns.Contains(FieldName) && row[FieldName] != DBNull.Value ? Convert.ToString(row[FieldName]) : "");
        }
        public static int GetIntFieldValue(DataRow row, string FieldName)
        {
            return (row.Table.Columns.Contains(FieldName) && row[FieldName] != DBNull.Value ? Convert.ToInt32(row[FieldName]) : 0);
        }
        public static bool GetBooleanFieldValue(DataRow row, string FieldName)
        {
            return (row.Table.Columns.Contains(FieldName) && row[FieldName] != DBNull.Value ? Convert.ToBoolean(row[FieldName]) : false);
        }
        public static DateTime GetDateTimeFieldValue(DataRow row, string FieldName)
        {
            return (row.Table.Columns.Contains(FieldName) && row[FieldName] != DBNull.Value ? Convert.ToDateTime(row[FieldName]) : DateTime.MinValue);
        }
        #endregion

    }
}