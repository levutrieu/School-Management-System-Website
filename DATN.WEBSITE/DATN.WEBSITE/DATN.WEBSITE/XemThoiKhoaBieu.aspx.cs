using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DATN.BUS;
using DevExpress.Data;
using DevExpress.Spreadsheet;
using DevExpress.Utils;
using System.Reflection;
using DevExpress.XtraSpreadsheet.Model;

namespace DATN.WEBSITE
{
    public partial class XemThoiKhoaBieu : System.Web.UI.Page
    {
        bus_dangkyhocphan client = new bus_dangkyhocphan();
        bus_dangnhap dangnhap = new bus_dangnhap();
        DateTime ngaytem;
        private string datetemp = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && !IsCallback)
            {
                //Session["ID_SINHVIEN"] = 18;
                if (Session["ID_SINHVIEN"] == null )
                {
                    groupinfo.Visible = false;
                    grouploc.Visible = false;
                    LoadThoiKhoaBieu();
                }
                else
                {
                    groupinfo.Visible = true;
                    grouploc.Visible = true;
                    groupnhapthongtin.Visible = false;
                    FillComboHocKy();
                    DataTable iDataSoure = dangnhap.GetThongTinSinhVien(Convert.ToInt32(Session["ID_SINHVIEN"]));
                    if (iDataSoure.Rows.Count > 0)
                    {
                        ViewState["iDataSoure"] = iDataSoure;
                        txtMaSinhVien.Text = iDataSoure.Rows[0]["MA_SINHVIEN"].ToString();
                        txtTenSinhVien.Text = iDataSoure.Rows[0]["TEN_SINHVIEN"].ToString();
                        txtLop.Text = iDataSoure.Rows[0]["TEN_LOP"].ToString();
                        txtNganh.Text = iDataSoure.Rows[0]["TEN_NGANH"].ToString();
                        txtHeDaoTao.Text = iDataSoure.Rows[0]["TEN_HE_DAOTAO"].ToString();
                        txtNienKhoa.Text = iDataSoure.Rows[0]["KHOAHOC"].ToString();
                        FillComboHocKy();
                        groupinfo.Visible = true;
                        grouploc.Visible = true;
                        groupnhapthongtin.Visible = false;
                    }
                }
            }
        }

        void FillComboHocKy()
        {
            DataTable dt = client.GetHocKyNamHoc();
            cboHocKy.DataSource = dt;
            cboHocKy.DataBind();
            cboHocKy.SelectedIndex = 0;
            cboHocKy_OnSelectedIndexChanged(null, null);
        }

        void SetComboTuanTheoNamHoc(int sotuan)
        {
            try
            {
                DataTable dt = client.GetNgay_SoTuan();
                DataTable xdt = new DataTable();
                xdt.Columns.Add("TUAN", typeof(int));
                xdt.Columns.Add("NAME_TUAN", typeof(string));
                int count = 0;
                //int sotuan = Convert.ToInt32(dt.Rows[0]["SO_TUAN"].ToString());
                DateTime ngay = Convert.ToDateTime(dt.Rows[0]["NGAY_BATDAU"].ToString());
                if (sotuan == 1)
                {
                    ngaytem = ngay.AddDays(-1);
                    datetemp = ngay.ToShortDateString();

                    for (int i = sotuan; i <= (sotuan + 23); i++)
                    {
                        count++;
                        DateTime addday = ngaytem.AddDays(i * 7);
                        DataRow r = xdt.NewRow();
                        r["TUAN"] = count;
                        r["NAME_TUAN"] = "Tuần " + count + " [Từ " + datetemp + "  Đến " + addday.ToShortDateString() + "]";
                        datetemp = addday.AddDays(1).ToShortDateString();

                        xdt.Rows.Add(r);
                        xdt.AcceptChanges();
                    }
                }
                if (sotuan == 24)
                {
                    ngaytem = ngay.AddDays((sotuan * 7));
                    count = 23;
                    datetemp = ngaytem.ToShortDateString();
                    ngaytem = ngay.AddDays(-1 + (sotuan * 7));
                    int temp = 0;
                    for (int i = sotuan; i <= (sotuan + 23); i++)
                    {
                        count++;
                        temp++;
                        DateTime addday = ngaytem.AddDays(temp * 7);
                        DataRow r = xdt.NewRow();
                        r["TUAN"] = count;
                        r["NAME_TUAN"] = "Tuần " + count + " [Từ " + datetemp + "  Đến " + addday.ToShortDateString() + "]";
                        datetemp = addday.AddDays(1).ToShortDateString();

                        xdt.Rows.Add(r);
                        xdt.AcceptChanges();
                    }
                }
                if (sotuan == 48)
                {
                    ngaytem = ngay.AddDays((sotuan * 7));
                    count = 47;
                    datetemp = ngaytem.ToShortDateString();
                    ngaytem = ngay.AddDays(-1 + (sotuan * 7));
                    int temp = 0;
                    for (int i = sotuan; i < 52; i++)
                    {
                        count++;
                        temp++;
                        DateTime addday = ngaytem.AddDays(temp * 7);
                        DataRow r = xdt.NewRow();
                        r["TUAN"] = count;
                        r["NAME_TUAN"] = "Tuần " + count + " [Từ " + datetemp + "  Đến " + addday.ToShortDateString() + "]";
                        datetemp = addday.AddDays(1).ToShortDateString();

                        xdt.Rows.Add(r);
                        xdt.AcceptChanges();
                    }
                }
                cboTuan.DataSource = xdt;
                cboTuan.DataBind();
                cboTuan.SelectedIndex = 0;
                cboTuan_OnSelectedIndexChanged(null,null);
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        void LoadThoiKhoaBieu()
        {
            StringBuilder html = new StringBuilder();
            //Table start.
            html.Append("<table border = '1'; class='tbl_TKB'");
            //Building the Header row.
            html.Append("<tr>");
            for (int i = 0; i < header.Count; i++)
            {
                if (i == 0)
                {
                    html.Append("<td style='background-color: dodgerblue;'  width: 10px  class='td'>");
                    html.Append(header[i]);
                    html.Append("</td>");
                }
                else
                {
                    html.Append("<td style='background-color: dodgerblue;' class='tbl_TKBtd'>");
                    html.Append(header[i]);
                    html.Append("</td>");
                }
            }
            int tiet = 1;
            for (int i = 1; i <= 14; i++)
            {
                html.Append("<tr>");
                for (int j = 0; j < 8; j++)
                {
                    if (j == 0)
                    {
                        html.Append("<td width:10px class='td'>");
                        html.Append(tiet);
                        html.Append("</td>");
                    }
                    else
                    {
                        html.Append("<td class='tbl_TKBtd'>");
                        html.Append("");
                        html.Append("</td>");
                    }
                }
                html.Append("</tr>");
                // 1 list có nhiều cái list , list chứa 10 row , trong 1 row có 10 (list) cột , trông mỗi cột là 1 object
                // select row 1 ==> lấy ra row 1 co => 1 list (10  object)
                tiet++;
            }
            html.Append("</table>");
            //Append the HTML string to Placeholder.
            thoikhoabieu.Controls.Add(new Literal { Text = html.ToString() });
        }

        List<string> header = new List<string> { "Tiết", "Thứ 2", "Thứ 3", "Thứ 4", "Thứ 5", "Thứ 6", "Thứ 7", "Chủ nhật" };

        void CreateHeaderTKB(DataTable tbl)
        {
            StringBuilder html = new StringBuilder();
            //Table start.
            html.Append("<table border = '1'; class='tbl_TKB'");
            //Building the Header row.
            html.Append("<tr>");
            for (int i = 0; i < header.Count; i++)
            {
                if (i == 0)
                {
                    html.Append("<td style='background-color: dodgerblue;'  width: 10px  class='td'>");
                    html.Append(header[i]);
                    html.Append("</td>");
                }
                else
                {
                    html.Append("<td style='background-color: dodgerblue;' class='tbl_TKBtd'>");
                    html.Append(header[i]);
                    html.Append("</td>");
                }
            }

            int tiet = 1;
            var data = ConvertTo<ThoiKhoaBieu>(tbl);

            var dataStiet = data.GroupBy(x => x.TIET_BD).Select(x => x.FirstOrDefault()).ToList();

            for (int i = 1; i <= 14; i++)
            {
                // lấy dữ liệu cho row theo tiết bắt đầu.
                // nếu có data nghĩa là row này cosduwx liệu cho từng cột
                // tiếp theo load duwx liệu cho từng cột
                var a1 = data.Where(x => x.TIET_BD == i).ToList();
                html.Append("<tr>");
                for (int j = 0; j < 8; j++)
                {
                    if (a1 != null && a1.Count() > 0)
                    {// tạo cột đầu tiên
                        #region khi co du lieu tai tiet bat dau
                        if (j == 0)
                        {
                            html.Append("<td width:10px class='td'>");
                            html.Append(tiet);
                            html.Append("</td>");
                        }
                        else
                        {// chứa data từ từ 0 tới n 
                            var a2 = a1.Where(x => x.THU == j + 1).ToList();
                            if (a2.Count() > 0)
                            {
                                foreach (var item in a2)
                                {
                                    html.Append("<td class='tbl_TKBtd' rowspan=" + item.SOTIET + ">");
                                    html.Append("GV: " + item.TEN_GIANGVIEN + "</br>");
                                    html.Append("Môn học: " + item.TEN_MONHOC + "</br>");
                                    html.Append("Phòng:" + item.TEN_PHONG + "</br>"); html.Append("</td>");
                                }
                            }
                            else
                            {
                                html.Append("<td class='tbl_TKBtd'>");
                                html.Append("");
                                html.Append("</td>");
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        #region
                        if (j == 0)
                        {
                            html.Append("<td width:10px class='td'>");
                            html.Append(tiet);
                            html.Append("</td>");
                        }
                        else
                        {
                            html.Append("<td class='tbl_TKBtd'>");
                            html.Append("");
                            html.Append("</td>");
                        }
                        #endregion
                    }
                }
                html.Append("</tr>");
                // 1 list có nhiều cái list , list chứa 10 row , trong 1 row có 10 (list) cột , trông mỗi cột là 1 object
                // select row 1 ==> lấy ra row 1 co => 1 list (10  object)
                tiet++;
            }
            html.Append("</table>");
            //Append the HTML string to Placeholder.
            thoikhoabieu.Controls.Add(new Literal { Text = html.ToString() });
        }

        protected void cboHocKy_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Session["ID_NAMHOC_HKY_HTAI"] = cboHocKy.Value;
            int id_hocky_htai = Convert.ToInt32(cboHocKy.Value);
            int xxx = client.GetHocKy(id_hocky_htai);
            if (xxx == 1)
            {
                SetComboTuanTheoNamHoc(1);
            }
            if (xxx == 2)
            {
                SetComboTuanTheoNamHoc(24);
            }
            if (xxx == 3)
            {
                SetComboTuanTheoNamHoc(48);
            }
        }

        protected void cboTuan_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = client.GetTKB(Convert.ToInt32(Session["ID_NAMHOC_HKY_HTAI"]),
                Convert.ToInt32(Session["ID_SINHVIEN"]), Convert.ToInt32(cboTuan.Value));
            thoikhoabieu.Controls.Clear();
            if (dt != null && dt.Rows.Count > 0)
            {
                CreateHeaderTKB(dt);
            }
            else
            {
                LoadThoiKhoaBieu();
            }
        }

        public List<T> ConvertTo<T>(DataTable datatable) where T : new()
        {
            List<T> Temp = new List<T>();
            try
            {
                List<string> columnsNames = new List<string>();
                foreach (DataColumn DataColumn in datatable.Columns)
                    columnsNames.Add(DataColumn.ColumnName);
                Temp = datatable.AsEnumerable().ToList().ConvertAll<T>(row => getObject<T>(row, columnsNames));
                return Temp;
            }
            catch
            {
                return Temp;
            }

        }

        public T getObject<T>(DataRow row, List<string> columnsName) where T : new()
        {
            T obj = new T();
            try
            {
                string columnname = "";
                string value = "";
                PropertyInfo[] Properties;
                Properties = typeof(T).GetProperties();
                foreach (PropertyInfo objProperty in Properties)
                {
                    columnname = columnsName.Find(name => name.ToLower() == objProperty.Name.ToLower());
                    if (!string.IsNullOrEmpty(columnname))
                    {
                        value = row[columnname].ToString();
                        if (!string.IsNullOrEmpty(value))
                        {
                            if (Nullable.GetUnderlyingType(objProperty.PropertyType) != null)
                            {
                                value = row[columnname].ToString().Replace("$", "").Replace(",", "");
                                objProperty.SetValue(obj, Convert.ChangeType(value, Type.GetType(Nullable.GetUnderlyingType(objProperty.PropertyType).ToString())), null);
                            }
                            else
                            {
                                value = row[columnname].ToString().Replace("%", "");
                                objProperty.SetValue(obj, Convert.ChangeType(value, Type.GetType(objProperty.PropertyType.ToString())), null);
                            }
                        }
                    }
                }
                return obj;
            }
            catch
            {
                return obj;
            }
        }

        protected void btnXemThoiKhoaBieu_OnClick(object sender, EventArgs e)
        {
            
            Session["ID_SINHVIEN"] = dangnhap.GetID_SinhVien(textmasv.Text.Trim());
            DataTable iDataSoure = dangnhap.GetThongTinSinhVien(Convert.ToInt32(Session["ID_SINHVIEN"]));
            if (iDataSoure.Rows.Count > 0)
            {
                ViewState["iDataSoure"] = iDataSoure;
                txtMaSinhVien.Text = iDataSoure.Rows[0]["MA_SINHVIEN"].ToString();
                txtTenSinhVien.Text = iDataSoure.Rows[0]["TEN_SINHVIEN"].ToString();
                txtLop.Text = iDataSoure.Rows[0]["TEN_LOP"].ToString();
                txtNganh.Text = iDataSoure.Rows[0]["TEN_NGANH"].ToString();
                txtHeDaoTao.Text = iDataSoure.Rows[0]["TEN_HE_DAOTAO"].ToString();
                txtNienKhoa.Text = iDataSoure.Rows[0]["KHOAHOC"].ToString();
                FillComboHocKy();
                groupinfo.Visible = true;
                grouploc.Visible = true;
                groupnhapthongtin.Visible = false;
            }
        }
    }
}