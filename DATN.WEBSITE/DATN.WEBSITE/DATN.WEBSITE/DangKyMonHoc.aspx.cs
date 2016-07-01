using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;
using DATN.BUS;
using DevExpress.Web;
using DevExpress.XtraPrinting.Native;

namespace DATN.WEBSITE
{

    public partial class DangKyMonHoc : System.Web.UI.Page
    {

        private bus_dangnhap client = new bus_dangnhap();
        private bus_dangkyhocphan dangkyhocphan = new bus_dangkyhocphan();
        private DataTable iGridDataSoure = null;
        private DataTable iGridDataSourceDK = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["MA_SINHVIEN"] = "2001120021";
            Session["ID_SINHVIEN"] = 18;
            if (!IsPostBack && !IsCallback)
            {
                if (Session["ID_SINHVIEN"] == null)
                {
                    Response.Redirect("LogIn.aspx");
                }
                else
                {
                    Session["ID_HEDAOTAO"] = dangkyhocphan.GetHeDaoTao(Convert.ToInt32(Session["ID_SINHVIEN"]));

                    if (Session["ID_HEDAOTAO"] != null)
                    {
                        FillComboKhoaHoc(Convert.ToInt32(Session["ID_HEDAOTAO"]));
                        FillComboNgayHoc();
                    }

                    grdDanhSachLopHP.DataSource = dangkyhocphan.GetLopHP();
                    grdDanhSachLopHP.DataBind();

                    iGridDataSourceDK = dangkyhocphan.GetLopHPDK(Convert.ToInt32(Session["ID_SINHVIEN"]));
                    grdViewDanhSachDaDangKy.DataSource = iGridDataSourceDK;
                    grdViewDanhSachDaDangKy.DataBind();
                    ViewState["iGridDataSourceDK"] = iGridDataSourceDK;
                }
            }
            
        }

        void CreateDataTable()
        {
            iGridDataSourceDK = new DataTable();
            iGridDataSourceDK.Columns.Add("ID_DANGKY");
            iGridDataSourceDK.Columns.Add("ID_SINHVIEN");
            iGridDataSourceDK.Columns.Add("ID_THAMSO");
            iGridDataSourceDK.Columns.Add("ID_LOPHOCPHAN");
            iGridDataSourceDK.Columns.Add("MA_MONHOC");
            iGridDataSourceDK.Columns.Add("TEN_MONHOC");
            iGridDataSourceDK.Columns.Add("SO_TC");
            iGridDataSourceDK.Columns.Add("NGAY_DANGKY");
            iGridDataSourceDK.Columns.Add("DON_GIA");
            iGridDataSourceDK.Columns.Add("THANH_TIEN");
            iGridDataSourceDK.Columns.Add("TRANGTHAI");

            iGridDataSourceDK.Rows.Add("", "", "", "", "", "", "", "", "", "", "");
        }

        protected void FillComboKhoaHoc(int temp)
        {
            if (string.IsNullOrEmpty(temp.ToString()))
                return;
            cboKhoa.DataSource = dangkyhocphan.GetAllKhoaHoc(temp);
            cboKhoa.SelectedIndex = 0;
            cboKhoa.DataBindItems();
            cboKhoa_OnSelectedIndexChanged(null, null);
        }

        protected void FillComboLopHoc(int temp)
        {
            if (string.IsNullOrEmpty(temp.ToString()))
                return;
            cboLopHoc.DataSource = dangkyhocphan.GetDSLopHocWhereKhoaHoc(temp);
            cboLopHoc.SelectedIndex = 0;
            cboLopHoc.DataBindItems();
            cboLopHoc_OnSelectedIndexChanged(null, null);
        }

        protected void FillComboNgayHoc()
        {
            cboNgayHoc.DataSource = dangkyhocphan.GetNgayHoc();
            cboNgayHoc.DataBind();
            cboNgayHoc.SelectedIndex = 0;
        }

        protected void FillComboMonHoc(int temp)
        {
            if(string.IsNullOrEmpty(temp.ToString()))
                return;
            cboMonHoc.DataSource = dangkyhocphan.GetMonHocTheoLop(temp);
            cboMonHoc.DataBind();
            cboMonHoc.SelectedIndex = 0;
        }

        protected void cboKhoa_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Session["ID_HEDAOTAO"] = cboKhoa.Value;
            FillComboLopHoc(Convert.ToInt32(Session["ID_HEDAOTAO"]));
        }

        protected void cboLopHoc_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Session["ID_LOPHOC"] = cboLopHoc.Value;
            FillComboMonHoc(Convert.ToInt32(Session["ID_LOPHOC"]));
        }

        protected void btnLuuDangKy_OnClick(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID_DANGKY");
            dt.Columns.Add("ID_SINHVIEN");
            dt.Columns.Add("ID_THAMSO");
            dt.Columns.Add("ID_LOPHOCPHAN");
            dt.Columns.Add("MA_MONHOC");
            dt.Columns.Add("TEN_MONHOC");
            dt.Columns.Add("SO_TC");
            dt.Columns.Add("NGAY_DANGKY");
            dt.Columns.Add("DON_GIA");
            dt.Columns.Add("THANH_TIEN");
            dt.Columns.Add("TRANGTHAI");
            foreach (GridViewRow row in grdViewDanhSachDaDangKy.Rows)
            {

                DataRow r = dt.NewRow();
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox xoa = (row.Cells[0].FindControl("chkXoa") as CheckBox);
                    if (!xoa.Checked)
                    {
                        r["ID_DANGKY"] = (row.Cells[1].FindControl("lbl_Iddangky")as Label).Text;
                        r["ID_SINHVIEN"] = Session["ID_SINHVIEN"];
                        r["ID_THAMSO"] = "0";
                        r["ID_LOPHOCPHAN"] = (row.Cells[3].FindControl("lbl_IdLophocphan") as Label).Text;
                        r["MA_MONHOC"] = (row.Cells[4].FindControl("lbl_Mamonhoc") as Label).Text;
                        r["TEN_MONHOC"] = (row.Cells[5].FindControl("lbl_Tenmonhoc") as Label).Text;
                        r["SO_TC"] = (row.Cells[6].FindControl("lbl_SoTC") as Label).Text;
                        r["NGAY_DANGKY"] = (row.Cells[7].FindControl("lbl_NgayDangKy") as Label).Text;
                        r["DON_GIA"] = (row.Cells[8].FindControl("lbl_DonGia") as Label).Text;
                        r["THANH_TIEN"] = (row.Cells[9].FindControl("lbl_ThanhTien") as Label).Text;
                        r["TRANGTHAI"] = (row.Cells[10].FindControl("lbl_TrangThai") as Label).Text;
                        dt.Rows.Add(r);
                        dt.AcceptChanges();
                    }
                }
            }

            bool res = dangkyhocphan.Insert_HocPhanDK(dt, Session["MA_SINHVIEN"].ToString());
            if (res)
            {
                Response.Write("script language=javascript>alert('Đăng ký môn học thành công!!');</script>");
            }
            else
            {
                Response.Write("script language=javascript>alert('Đăng ký môn học không thành công!!');</script>");
            }
        }

        protected void grdDanhSachLopHP_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onmouseover"] = "this.style.backgroundColor='aquamarine';";
                e.Row.Attributes["onmouseout"] = "this.style.backgroundColor='white';";
                e.Row.ToolTip = "Click last column for selecting this row.";
            }
        }

        DataTable KiemTraCheck(DataTable dt, int lophocphan)
        {
            DataTable xdt = dt.Clone();
            foreach (DataRow r in dt.Rows)
            {
                if (Convert.ToInt32(r["ID_LOPHOCPHAN"].ToString()) != lophocphan)
                {
                    xdt.ImportRow(r);
                }
            }
            return xdt;
        }

        protected void chkChon_OnCheckedChanged(object sender, EventArgs e)
        {
            iGridDataSourceDK = (DataTable) ViewState["iGridDataSourceDK"];
            if (iGridDataSourceDK == null || iGridDataSourceDK.Rows.Count == 0)
            {
                CreateDataTable();
            }
            foreach (GridViewRow row in grdDanhSachLopHP.Rows)
            {
                DataRow r = iGridDataSourceDK.NewRow();
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chon = (row.Cells[0].FindControl("chkChon") as CheckBox);
                    if (chon.Checked)
                    {
                        DataTable dt = KiemTraCheck(iGridDataSourceDK,Convert.ToInt32((row.Cells[1].FindControl("txtID_LopHocPhan") as Label).Text));
                        r["ID_DANGKY"] = "0";
                        r["ID_SINHVIEN"] = Session["ID_SINHVIEN"];
                        r["ID_THAMSO"] = "0";
                        r["ID_LOPHOCPHAN"] = (row.Cells[1].FindControl("txtID_LopHocPhan") as Label).Text;
                        r["MA_MONHOC"] = (row.Cells[2].FindControl("txtMaMonHoc") as Label).Text;
                        r["TEN_MONHOC"] = (row.Cells[3].FindControl("txtTenMonHoc") as Label).Text;
                        r["SO_TC"] = (row.Cells[4].FindControl("txtSTC") as Label).Text;
                        r["NGAY_DANGKY"] = DateTime.Now.ToShortDateString();
                        r["DON_GIA"] = 220;
                        r["THANH_TIEN"] = (double)(Convert.ToDouble(r["SO_TC"]) * Convert.ToDouble(r["DON_GIA"]));
                        r["TRANGTHAI"] = "Chưa lưu";
                        iGridDataSourceDK.Rows.Add(r);
                        iGridDataSourceDK.AcceptChanges();
                    }
                }
            }
            
            grdViewDanhSachDaDangKy.DataSource = iGridDataSourceDK;
            grdViewDanhSachDaDangKy.DataBind();
        }

        protected void btnHuyDangKy_OnClick(object sender, EventArgs e)
        {
            DataTable dt = iGridDataSoure;
         }

        protected void grdViewDanhSachDaDangKy_DataBinding(object sender, EventArgs e)
        {
            iGridDataSoure = dangkyhocphan.GetLopHP();
            iGridDataSoure.Columns.Add("CHK");
            grdDanhSachLopHP.DataSource = iGridDataSoure;
            grdDanhSachLopHP.DataBind();
        }

        protected void cboNgayHoc_OnSelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        protected void cboMonHoc_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        protected void chkXoa_OnCheckedChanged(object sender, EventArgs e)
        {
            
        }
    }
}