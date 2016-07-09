using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using DATN.BUS;
using DATN.C45;
using DATN.ID3;
using DevExpress.Web;
using DevExpress.XtraPrinting.Native;

namespace DATN.WEBSITE
{

    public partial class DangKyMonHoc : System.Web.UI.Page
    {

        private bus_dangnhap dangnhap = new bus_dangnhap();
        private bus_dangkyhocphan dangkyhocphan = new bus_dangkyhocphan();
        private DataTable iGridDataSoure = null;
        private DataTable iGridDataSourceDK = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && !IsCallback)
            {
                if (Session["ID_SINHVIEN"] == null)
                {
                    paneInfo.Visible = false;
                    paneChucNang.Visible = false;
                    paneDSDangKy.Visible = false;
                    paneDSHocPhan.Visible = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }
                else
                {
                    Session["ID_HEDAOTAO"] = dangkyhocphan.GetHeDaoTao(Convert.ToInt32(Session["ID_SINHVIEN"]));

                    #region Kiểm tra lịch đăng ký học phần
                    DataTable dt = dangkyhocphan.GetThamSoDangDotDangKy(Convert.ToInt32(Session["ID_HEDAOTAO"]));
                    if (dt == null || dt.Rows.Count == 0)
                    {
                        txtThongbao.Text = "Hiện tại không có đợt đăng ký học phần nào";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "thongBao();", true);
                        return;
                    }
                    #endregion

                    if (Session["ID_HEDAOTAO"] != null)
                    {
                        FillComboKhoaHoc(Convert.ToInt32(Session["ID_HEDAOTAO"]));
                    }
                    paneInfo.Visible = true;
                    DataTable DanhSachHocPhan = dangkyhocphan.GetDanhSachLopHocPhan(Convert.ToInt32(Session["ID_KHOAHOC"]), Convert.ToInt32(Session["ID_NGANH"]));
                    grdDanhSachLopHP.DataSource = DanhSachHocPhan;
                    grdDanhSachLopHP.DataBind();
                    ViewState["DanhSachHocPhan"] = DanhSachHocPhan;


                    iGridDataSourceDK = dangkyhocphan.GetLopHPDK(Convert.ToInt32(Session["ID_SINHVIEN"]));
                    grdViewDanhSachDaDangKy.DataSource = iGridDataSourceDK;
                    grdViewDanhSachDaDangKy.DataBind();
                    ViewState["iGridDataSourceDK"] = iGridDataSourceDK;


                    DataTable iDataSoure = (DataTable)ViewState["iDataSoure"];
                    if (iDataSoure != null)
                    {
                        Session["MA_SINHVIEN"] = iDataSoure.Rows[0]["MA_SINHVIEN"].ToString();
                    }
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

            //iGridDataSourceDK.Rows.Add("", "", "", "", "", "", "", "", "", "", "");
        }

        void LoadGridHocPhan()
        {
            DataTable DanhSachHocPhan = dangkyhocphan.GetDanhSachLopHocPhan(Convert.ToInt32(Session["ID_KHOAHOC"]), Convert.ToInt32(Session["ID_NGANH"]));
            grdDanhSachLopHP.DataSource = DanhSachHocPhan;
            grdDanhSachLopHP.DataBind();
            ViewState["DanhSachHocPhan"] = DanhSachHocPhan;
        }

        #region Fill combobox
        protected void FillComboKhoaHoc(int idhedaotao)
        {
            if (string.IsNullOrEmpty(idhedaotao.ToString()))
                return;
            cboKhoa.DataSource = dangkyhocphan.GetAllKhoaHoc(idhedaotao);
            cboKhoa.SelectedIndex = 0;
            cboKhoa.DataBindItems();
            cboKhoa_OnSelectedIndexChanged(null, null);
        }

        protected void FillComboNganh(int idkhoahoc)
        {
            if (string.IsNullOrEmpty(idkhoahoc.ToString()))
                return;
            cboNganh.DataSource = dangkyhocphan.GetNganhWhereKhoaHoc(idkhoahoc);
            cboNganh.SelectedIndex = 0;
            cboNganh.DataBindItems();
            cboNganh_OnSelectedIndexChanged(null, null);
        }

        protected void FillComboMonHoc(int idkhoahoc, int idnganh)
        {
            if (string.IsNullOrEmpty(idkhoahoc.ToString()) && string.IsNullOrEmpty(idnganh.ToString()))
                return;
            cboMonHoc.DataSource = dangkyhocphan.GetMonHoc(idkhoahoc, idnganh);
            cboMonHoc.DataBind();
            cboMonHoc.SelectedIndex = 0;
            Session["ID_MONHOC"] = cboMonHoc.Value;
            cboMonHoc_OnSelectedIndexChanged(null, null);
        }

        #endregion

        #region Event changed combobox
        protected void cboKhoa_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboKhoa.Items.Count > 0)
            {
                Session["ID_KHOAHOC"] = cboKhoa.Value;
                FillComboNganh(Convert.ToInt32(Session["ID_KHOAHOC"]));
                LoadGridHocPhan();
            }
        }

        protected void cboNganh_OnSelectedIndexChanged(object sender, EventArgs e)
        {

            if (cboNganh.Items.Count > 0)
            {
                Session["ID_NGANH"] = cboNganh.Value;
                FillComboMonHoc(Convert.ToInt32(Session["ID_KHOAHOC"]), Convert.ToInt32(Session["ID_NGANH"].ToString()));
                LoadGridHocPhan();
            }

        }

        protected void cboMonHoc_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboMonHoc.Items.Count > 0)
            {
                Session["ID_MONHOC"] = cboMonHoc.Value;
                int idmonhoc = Convert.ToInt32(Session["ID_MONHOC"]);
                DataTable iGridDataSource_Temp = (DataTable)ViewState["DanhSachHocPhan"];
                if (iGridDataSource_Temp != null && iGridDataSource_Temp.Rows.Count > 0)
                {
                    if (idmonhoc != 0)
                    {
                        var temp = (from data in
                                        iGridDataSource_Temp.AsEnumerable()
                                                .Where(t => t.Field<int>("ID_MONHOC") == idmonhoc)
                                    select data).ToList();
                        if (temp.Count > 0)
                        {
                            DataTable dt = temp.CopyToDataTable();
                            grdDanhSachLopHP.DataSource = dt;
                            grdDanhSachLopHP.DataBind();
                        }
                    }
                    else
                    {
                        grdDanhSachLopHP.DataSource = iGridDataSource_Temp;
                        grdDanhSachLopHP.DataBind();
                    }
                }
            }
        }
        #endregion

        protected void grdDanhSachLopHP_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onmouseover"] = "this.style.backgroundColor='aquamarine';";
                e.Row.Attributes["onmouseout"] = "this.style.backgroundColor='white';";
                e.Row.ToolTip = "Click last column for selecting this row.";
            }
        }

        bool KiemTraCheck(DataTable dt, int lophocphan)
        {

            foreach (DataRow r in dt.Rows)
            {
                if (Convert.ToInt32(r["ID_LOPHOCPHAN"].ToString()) == lophocphan)
                {
                    return true;
                }
            }
            return false;
        }

        protected void chkChon_OnCheckedChanged(object sender, EventArgs e)
        {
            iGridDataSourceDK = (DataTable)ViewState["iGridDataSourceDK"];
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
                        if (iGridDataSourceDK == null && iGridDataSourceDK.Rows.Count == 0)
                        {
                            r["ID_DANGKY"] = "0";
                            r["ID_SINHVIEN"] = Session["ID_SINHVIEN"];
                            r["ID_THAMSO"] = "0";
                            r["ID_LOPHOCPHAN"] = (row.Cells[1].FindControl("txtID_LopHocPhan") as Label).Text;
                            r["MA_MONHOC"] = (row.Cells[2].FindControl("txtMaMonHoc") as Label).Text;
                            r["TEN_MONHOC"] = (row.Cells[3].FindControl("txtTenMonHoc") as Label).Text;
                            r["SO_TC"] = (row.Cells[4].FindControl("txtSTC") as Label).Text;
                            r["NGAY_DANGKY"] = DateTime.Now.ToShortDateString();
                            r["DON_GIA"] = dangkyhocphan.GetHocPhi_LT(Convert.ToInt32(Session["ID_HEDAOTAO"]), Convert.ToInt32((row.Cells[5].FindControl("txtIS_LYTHUYET") as Label).Text));
                            r["THANH_TIEN"] = (double)(Convert.ToDouble(r["SO_TC"]) * Convert.ToDouble(r["DON_GIA"]));
                            r["TRANGTHAI"] = "Chưa lưu";
                            iGridDataSourceDK.Rows.Add(r);
                            iGridDataSourceDK.AcceptChanges();
                        }
                        else
                        {
                            bool isCheck = KiemTraCheck(iGridDataSourceDK, Int32.Parse((row.Cells[1].FindControl("txtID_LopHocPhan") as Label).Text));
                            if (!isCheck)
                            {
                                r["ID_DANGKY"] = "0";
                                r["ID_SINHVIEN"] = Session["ID_SINHVIEN"];
                                r["ID_THAMSO"] = "0";
                                r["ID_LOPHOCPHAN"] = (row.Cells[1].FindControl("txtID_LopHocPhan") as Label).Text;
                                r["MA_MONHOC"] = (row.Cells[2].FindControl("txtMaMonHoc") as Label).Text;
                                r["TEN_MONHOC"] = (row.Cells[3].FindControl("txtTenMonHoc") as Label).Text;
                                r["SO_TC"] = (row.Cells[4].FindControl("txtSTC") as Label).Text; r["NGAY_DANGKY"] = DateTime.Now.ToShortDateString();
                                r["DON_GIA"] = dangkyhocphan.GetHocPhi_LT(Convert.ToInt32(Session["ID_HEDAOTAO"]), Convert.ToInt32((row.Cells[5].FindControl("txtIS_LYTHUYET") as Label).Text));
                                r["THANH_TIEN"] = (double)(Convert.ToDouble(r["SO_TC"]) * Convert.ToDouble(r["DON_GIA"]));
                                r["TRANGTHAI"] = "Chưa lưu";
                                iGridDataSourceDK.Rows.Add(r);
                                iGridDataSourceDK.AcceptChanges();
                            }
                        }

                    }
                }
            }
            var data = iGridDataSourceDK.AsEnumerable().GroupBy(t => t.Field<string>("MA_MONHOC")).Select(x => x.FirstOrDefault()).ToArray();
            if (data.Count() > 0)
            {
                iGridDataSourceDK = data.CopyToDataTable();
            }

            grdViewDanhSachDaDangKy.DataSource = iGridDataSourceDK;
            grdViewDanhSachDaDangKy.DataBind();
        }

        #region Button xử lý
        protected void btnDangNhap_OnClick(object sender, EventArgs e)
        {
            int res = dangnhap.CheckLogin(txtMaSinhVien.Text, txtMaKhau.Text);
            if (res == 1)
            {
                #region
                Session["ID_SINHVIEN"] = UserCommon.IdSinhVien;

                DataTable iDataSoure = dangnhap.GetThongTinSinhVien(Convert.ToInt32(Session["ID_SINHVIEN"]));
                ViewState["iDataSoure"] = iDataSoure;
                paneInfo.Visible = true;
                paneChucNang.Visible = true; paneDSDangKy.Visible = true;
                paneDSHocPhan.Visible = true;
                Session["MA_SINHVIEN"] = iDataSoure.Rows[0]["MA_SINHVIEN"].ToString();

                Session["ID_HEDAOTAO"] = dangkyhocphan.GetHeDaoTao(Convert.ToInt32(Session["ID_SINHVIEN"]));
                if (Session["ID_HEDAOTAO"] != null)
                {
                    FillComboKhoaHoc(Convert.ToInt32(Session["ID_HEDAOTAO"]));
                }
                #endregion
                DataTable DanhSachHocPhan = dangkyhocphan.GetDanhSachLopHocPhan(Convert.ToInt32(Session["ID_KHOAHOC"]), Convert.ToInt32(Session["ID_NGANH"]));
                grdDanhSachLopHP.DataSource = DanhSachHocPhan;
                grdDanhSachLopHP.DataBind();
                ViewState["DanhSachHocPhan"] = DanhSachHocPhan;

                iGridDataSourceDK = dangkyhocphan.GetLopHPDK(Convert.ToInt32(Session["ID_SINHVIEN"]));
                grdViewDanhSachDaDangKy.DataSource = iGridDataSourceDK;
                grdViewDanhSachDaDangKy.DataBind();
                ViewState["iGridDataSourceDK"] = iGridDataSourceDK;
            }
            else
            {
                Response.Write("Sai thông tin đăng nhập");
            }
            LoadGridHocPhan();
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
                        r["ID_DANGKY"] = (row.Cells[1].FindControl("lbl_Iddangky") as Label).Text;
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
                txtThongbao.Text = "Lưu đăng ký thành công";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "thongBao();", true);

                grdDanhSachLopHP.DataSource = dangkyhocphan.GetDanhSachLopHocPhan(Convert.ToInt32(Session["ID_MONHOC"]), Convert.ToInt32(Session["THU"]));
                grdDanhSachLopHP.DataBind(); iGridDataSourceDK = dangkyhocphan.GetLopHPDK(Convert.ToInt32(Session["ID_SINHVIEN"]));
                grdViewDanhSachDaDangKy.DataSource = iGridDataSourceDK;
                grdViewDanhSachDaDangKy.DataBind();
                ViewState["iGridDataSourceDK"] = iGridDataSourceDK;
            }
            else
            {
                txtThongbao.Text = "Lưu đăng ký không thành công";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "thongBao();", true);
            }
            LoadGridHocPhan();
        }

        protected void btnHuyDangKy_OnClick(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["iGridDataSourceDK"];

            DataTable xdt = new DataTable();
            xdt.Columns.Add("ID_DANGKY");
            xdt.Columns.Add("ID_SINHVIEN");
            xdt.Columns.Add("ID_THAMSO");
            xdt.Columns.Add("ID_LOPHOCPHAN");
            xdt.Columns.Add("DON_GIA");
            xdt.Columns.Add("THANH_TIEN");
            if (dt.Rows.Count > 0)
            {
                foreach (GridViewRow row in grdViewDanhSachDaDangKy.Rows)
                {
                    DataRow r = xdt.NewRow();
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox xoa = (row.Cells[0].FindControl("chkXoa") as CheckBox);
                        if (xoa.Checked)
                        {
                            r["ID_DANGKY"] = (row.Cells[1].FindControl("lbl_Iddangky") as Label).Text;
                            r["ID_SINHVIEN"] = Session["ID_SINHVIEN"];
                            r["ID_THAMSO"] = "0";// chua get tham so
                            r["ID_LOPHOCPHAN"] = (row.Cells[3].FindControl("lbl_IdLophocphan") as Label).Text;
                            r["DON_GIA"] = (row.Cells[7].FindControl("lbl_DonGia") as Label).Text;
                            r["THANH_TIEN"] = (row.Cells[8].FindControl("lbl_ThanhTien") as Label).Text;
                            xdt.Rows.Add(r);
                            xdt.AcceptChanges();
                        }
                    }
                }
            }
            bool res = dangkyhocphan.Insert_DangKyHuy(xdt, (Session["MA_SINHVIEN"].ToString()));
            if (res)
            {
                txtThongbao.Text = "Hủy đăng ký thành công";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "thongBao();", true);
                iGridDataSourceDK = dangkyhocphan.GetLopHPDK(Convert.ToInt32(Session["ID_SINHVIEN"]));
                grdViewDanhSachDaDangKy.DataSource = iGridDataSourceDK;
                grdViewDanhSachDaDangKy.DataBind();
                ViewState["iGridDataSourceDK"] = iGridDataSourceDK;
            }
            else
            {
                txtThongbao.Text = "Hủy đăng ký không thành công";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "thongBao();", true);
            }
            LoadGridHocPhan();}
        #endregion

        #region ID3
        List<DATN.ID3.TTS_ID3> iData_AllTrees = new List<TTS_ID3>();

        private double TuVanMHID(DataTable iData_DiemMau, DataTable iData_DiemDieuKien)
        {
            try
            {
                #region Thuc hien thuat toan cho tung mon hoc

                DataTable iGridDataSource = iData_DiemMau;

                List<double> ds_diemTK = new List<double>(); //Danh sach cac bo diem TK co trong bang du lieu

                #region Lay danh sach diem KET QUA MON DANG TU VAN

                foreach (DataRow drTK in iGridDataSource.Rows)
                {
                    int check = 0;
                    foreach (double kt in ds_diemTK)
                    {
                        if (kt == Convert.ToDouble(drTK[iGridDataSource.Columns.Count - 1]))
                        {
                            check = 1;
                        }
                    }
                    if (check == 0)
                    {
                        ds_diemTK.Add(Convert.ToDouble(drTK[iGridDataSource.Columns.Count - 1]));
                    }
                }
                if (ds_diemTK.Count % 2 != 0)
                {
                    ds_diemTK.Add(ds_diemTK[0]);
                }

                #endregion

                string xcolumn = iGridDataSource.Columns[iGridDataSource.Columns.Count - 1].ColumnName; // Ten cot diem TK mon dang xet
                iData_AllTree.Clear();

                #region Tao tree

                for (int id = 0; id < ds_diemTK.Count - 1; id += 2)
                {
                    #region Tao Attribute

                    List<DATN.ID3.Attribute> Attributes = new List<DATN.ID3.Attribute>(); // danh sach thuoc tinh
                    for (int i = 0; i < iGridDataSource.Columns.Count - 1; i++)
                    {
                        DATN.ID3.Attribute x = new DATN.ID3.Attribute();
                        x.Name = iGridDataSource.Columns[i].ColumnName;
                        Attributes.Add(x);
                    }

                    #endregion

                    #region Duyet 1 lan 2 gia tri diem TK de tao tree
                    DataTable iDataXet = null;
                    var filteredRows = from row in iGridDataSource.AsEnumerable()
                                       where row.Field<double>(xcolumn) == ds_diemTK[id] || row.Field<double>(xcolumn) == ds_diemTK[id + 1]
                                       select row;
                    DataRow[] xcheck = filteredRows.ToArray();
                    //DataRow[] xcheck = iGridDataSource.Select("Convert(" + xcolumn + ",'System.Double')= " + ds_diemTK[id].ToString() + " or Convert(" + xcolumn + ",'System.Double')= " + ds_diemTK[id + 1].ToString());
                    iDataXet = xcheck.CopyToDataTable();

                    #endregion

                    #region Tao tree

                    for (int i = 0; i < iDataXet.Rows.Count; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            int check = 0;
                            for (int z = 0; z < Attributes[j].Value.Count; z++)
                            {
                                if (Convert.ToDouble(Attributes[j].Value[z]) == Convert.ToDouble(iDataXet.Rows[i][j]))
                                {
                                    check = 1;
                                }
                            }
                            if (check == 0)
                            {
                                Attributes[j].Value.Add(Convert.ToDouble(iDataXet.Rows[i][j]));
                            }
                        }
                    }

                    #region Tao ds dieu kien mau

                    List<List<double>> Examples = new List<List<double>>();
                    foreach (DataRow xdr in iDataXet.Rows)
                    {
                        List<double> example = new List<double>();
                        for (int i = 0; i < iDataXet.Columns.Count; i++)
                        {
                            example.Add(Convert.ToDouble(xdr[i]));
                        }
                        Examples.Add(example);
                    }

                    #endregion

                    List<DATN.ID3.Attribute> at = new List<DATN.ID3.Attribute>();
                    for (int i = 0; i < Attributes.Count; i++)
                    {
                        at.Add(Attributes[i]);
                    }
                    List<double> ds_tmp = new List<double>();
                    for (int i = 0; i < Examples.Count; i++)
                    {
                        int check = 0;
                        foreach (double value in ds_tmp)
                        {
                            if (Examples[i][Examples[i].Count - 1] == value)
                            {
                                check = 1;
                            }
                        }
                        if (check == 0)
                            ds_tmp.Add(Examples[i][Examples[i].Count - 1]);
                    }
                    DATN.ID3.TTS_ID3 DTID3 = new TTS_ID3(Examples, at, ds_diemTK[id], ds_diemTK[id + 1]);
                    DTID3.GetTree();
                    iData_AllTrees.Add(DTID3);
                    #endregion
                }

                #endregion

                #region Duyet tree lay gia tri

                DataTable Diem_SV = iData_DiemDieuKien;

                List<double> result = new List<double>();

                foreach (DATN.ID3.TTS_ID3 d in iData_AllTrees)
                {
                    result = d.SearchTree(d.Tree, Diem_SV, result, false);
                }
                double tong = 0;
                string ds_dudoan = "";
                foreach (double b in result)
                {
                    ds_dudoan = ds_dudoan + "\n" + b.ToString();
                    tong += b;
                }
                double diem_dudoan = (double)tong / result.Count;

                #endregion

                return diem_dudoan;

                #endregion
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        string ThucHienThuatToanID(GridView grdRow)
        {
            string ketqua = "";
            foreach (GridViewRow row in grdRow.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chon = (row.Cells[0].FindControl("chonmonhoc") as CheckBox);
                    if (chon.Checked)
                    {
                        int monhoc = Convert.ToInt32((row.Cells[1].FindControl("lblID_MONHOC") as Label).Text);
                        string tenmonhoc = (row.Cells[3].FindControl("lblTEN_MONHOC") as Label).Text;
                        DataTable iGridDataSource = dangkyhocphan.GetDiemMau(Session["MA_SINHVIEN"].ToString(), monhoc);
                        DataTable Diem_SV = dangkyhocphan.GetDiem_SV(Session["MA_SINHVIEN"].ToString(), monhoc);
                        double diem_dudoan = TuVanMHID(iGridDataSource, Diem_SV);

                        if (Double.IsNaN(diem_dudoan))
                        {
                            #region Không tìm được điểm chính xác

                            #region Tao lai du lieu mau

                            foreach (DataRow dr in iGridDataSource.Rows)
                            {
                                foreach (DataColumn xdc in iGridDataSource.Columns)
                                {
                                    if (!string.IsNullOrEmpty(dr[xdc.ColumnName].ToString()))
                                    {
                                        if (Convert.ToDouble(dr[xdc.ColumnName]) < (double)4)
                                        {
                                            dr[xdc.ColumnName] = 4; // 0 -> 3.9
                                        }
                                        else
                                        {
                                            if (Convert.ToDouble(dr[xdc.ColumnName]) < (double)5)
                                            {
                                                dr[xdc.ColumnName] = 5; // 4.0 -> 4.9
                                            }
                                            else
                                            {
                                                if (Convert.ToDouble(dr[xdc.ColumnName]) < (double)5.5)
                                                {
                                                    dr[xdc.ColumnName] = 5.5; // 5.0 -> 5.4
                                                }
                                                else
                                                {
                                                    if (Convert.ToDouble(dr[xdc.ColumnName]) < (double)6.5)
                                                    {
                                                        dr[xdc.ColumnName] = 6.5; // 5.5 -> 6.4
                                                    }
                                                    else
                                                    {
                                                        if (Convert.ToDouble(dr[xdc.ColumnName]) <
                                                            (double)7)
                                                        {
                                                            dr[xdc.ColumnName] = 7; // 6.5 -> 6.9
                                                        }
                                                        else
                                                        {
                                                            if (Convert.ToDouble(dr[xdc.ColumnName]) <
                                                                (double)8)
                                                            {
                                                                dr[xdc.ColumnName] = 8; // 7.0 -> 7.9
                                                            }
                                                            else
                                                            {
                                                                if (Convert.ToDouble(dr[xdc.ColumnName]) <
                                                                    (double)8.5)
                                                                {
                                                                    dr[xdc.ColumnName] = 8.5; // 8.0 -> 8.4
                                                                }
                                                                else
                                                                {
                                                                    dr[xdc.ColumnName] = 10; // 8.5 -> 10
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            #endregion

                            if (iGridDataSource.Rows.Count < 1)
                            {
                                ketqua = ketqua + "Dự đoán kết quả môn học " + tenmonhoc + " :" +
                                         "\n- Kết quả: Không thể dự đoán \n- Điểm dự đoán: Không có dữ liệu mẫu nên không thể dự đoán được\n\n";
                            }
                            else
                            {
                                //4-F 5-D 5.5-D+ 6.5-C 7-C+ 8-B 8.5-B+ 10-A
                                #region Tao lai diem DK

                                foreach (DataColumn xdc in Diem_SV.Columns)
                                {
                                    if (string.IsNullOrEmpty(Diem_SV.Rows[0][xdc.ColumnName].ToString()))
                                    {
                                        Diem_SV.Rows[0][xdc.ColumnName] = 4;
                                    }
                                    else
                                    {
                                        if (Convert.ToDouble(Diem_SV.Rows[0][xdc.ColumnName]) < (double)4)
                                        {
                                            Diem_SV.Rows[0][xdc.ColumnName] = 4;
                                        }
                                        else
                                        {
                                            if (Convert.ToDouble(Diem_SV.Rows[0][xdc.ColumnName]) < (double)5)
                                            {
                                                Diem_SV.Rows[0][xdc.ColumnName] = 5;
                                            }
                                            else
                                            {
                                                if (Convert.ToDouble(Diem_SV.Rows[0][xdc.ColumnName]) < (double)5.5)
                                                {
                                                    Diem_SV.Rows[0][xdc.ColumnName] = 5.5;
                                                }
                                                else
                                                {
                                                    if (Convert.ToDouble(Diem_SV.Rows[0][xdc.ColumnName]) <
                                                        (double)6.5)
                                                    {
                                                        Diem_SV.Rows[0][xdc.ColumnName] = 6.5;
                                                    }
                                                    else
                                                    {
                                                        if (Convert.ToDouble(Diem_SV.Rows[0][xdc.ColumnName]) <
                                                            (double)7)
                                                        {
                                                            Diem_SV.Rows[0][xdc.ColumnName] = 7;
                                                        }
                                                        else
                                                        {
                                                            if (Convert.ToDouble(Diem_SV.Rows[0][xdc.ColumnName]) <
                                                                (double)8)
                                                            {
                                                                Diem_SV.Rows[0][xdc.ColumnName] = 8;
                                                            }
                                                            else
                                                            {
                                                                if (
                                                                    Convert.ToDouble(Diem_SV.Rows[0][xdc.ColumnName]) <
                                                                    (double)8.5)
                                                                {
                                                                    Diem_SV.Rows[0][xdc.ColumnName] = 8.5;
                                                                }
                                                                else
                                                                {
                                                                    Diem_SV.Rows[0][xdc.ColumnName] = 10;
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }

                                #endregion

                                #region
                                diem_dudoan = TuVanMH(iGridDataSource, Diem_SV);
                                if (diem_dudoan == 4)
                                {
                                    ketqua = ketqua + "Dự đoán kết quả môn học " + tenmonhoc +
                                             " :" +
                                             "\n- Kết quả: Không đạt \n- Điểm dự đoán: 0 -> 3.9 (F)\n\n";
                                }
                                if (diem_dudoan == 5)
                                {
                                    ketqua = ketqua + "Dự đoán kết quả môn học " + tenmonhoc +
                                             " :" +
                                             "\n- Kết quả: Đạt \n- Điểm dự đoán: 4.0 -> 4.9 (D) \n\n";
                                }
                                if (diem_dudoan == 5.5)
                                {
                                    ketqua = ketqua + "Dự đoán kết quả môn học " + tenmonhoc +
                                             " :" +
                                             "\n- Kết quả: Đạt \n- Điểm dự đoán: 5.0 -> 5.4 (D+) \n\n";
                                }
                                if (diem_dudoan == 6.5)
                                {
                                    ketqua = ketqua + "Dự đoán kết quả môn học " + tenmonhoc +
                                             " :" +
                                             "\n- Kết quả: Đạt \n- Điểm dự đoán: 5.5 -> 6.4 (C) \n\n";
                                }
                                if (diem_dudoan == 7)
                                {
                                    ketqua = ketqua + "Dự đoán kết quả môn học " + tenmonhoc +
                                             " :" +
                                             "\n- Kết quả: Đạt \n- Điểm dự đoán: 6.5 -> 6.9 (C+) \n\n";
                                }
                                if (diem_dudoan == 8)
                                {
                                    ketqua = ketqua + "Dự đoán kết quả môn học " + tenmonhoc +
                                             " :" +
                                             "\n- Kết quả: Đạt \n- Điểm dự đoán: 7.0 -> 7.9 (B) \n\n";
                                }
                                if (diem_dudoan == 8.5)
                                {
                                    ketqua = ketqua + "Dự đoán kết quả môn học " + tenmonhoc +
                                             " :" +
                                             "\n- Kết quả: Đạt \n- Điểm dự đoán: 8.0 -> 8.4 (B+) \n\n";
                                }
                                if (diem_dudoan == 10)
                                {
                                    ketqua = ketqua + "Dự đoán kết quả môn học " + tenmonhoc +
                                             " :" +
                                             "\n- Kết quả: Đạt \n- Điểm dự đoán: 8.5 -> 10 (A) \n\n";
                                }
                                #endregion
                                if (Double.IsNaN(diem_dudoan))
                                {
                                    #region Xét đậu rớt khi không tìm được điểm

                                    foreach (DataRow dr in iGridDataSource.Rows)
                                    {
                                        if (Convert.ToDouble(dr[monhoc]) == (double)4)
                                        {
                                            dr[monhoc] = 0;
                                        }
                                        else
                                        {
                                            dr[monhoc] = 10;
                                        }
                                    }
                                    diem_dudoan = TuVanMH(iGridDataSource, Diem_SV);
                                    if (diem_dudoan == 10)
                                    {
                                        ketqua = ketqua + "Dự đoán kết quả môn học " + tenmonhoc +
                                                 " :" +
                                                 "\n- Kết quả: Đạt \n- Điểm dự đoán: Dữ liệu quá ít không thể dự đoán được\n\n";
                                    }
                                    else
                                    {
                                        ketqua = ketqua + "Dự đoán kết quả môn học " + tenmonhoc +
                                                 " :" +
                                                 "\n- Kết quả: Không đạt \n- Điểm dự đoán: Dữ liệu quá ít không thể dự đoán được\n\n";
                                    }

                                    #endregion
                                }
                            }
                            #endregion
                        }
                        else
                        {
                            #region
                            #region Diem chu

                            string diemchu = "";
                            if (diem_dudoan < (double)4)
                            {
                                diemchu = " 0 -> 3.9 (F)";
                            }
                            else
                            {
                                if (diem_dudoan < (double)5)
                                {
                                    diemchu = " 4.0 -> 4.9 (D)";
                                }
                                else
                                {
                                    if (diem_dudoan < (double)5.5)
                                    {
                                        diemchu = " 5.0 -> 5.4 (D+)";
                                    }
                                    else
                                    {
                                        if (diem_dudoan < (double)6.5)
                                        {
                                            diemchu = " 5.5 -> 6.4 (C)";
                                        }
                                        else
                                        {
                                            if (diem_dudoan < (double)7)
                                            {
                                                diemchu = " 6.5 -> 6.9 (C+)";
                                            }
                                            else
                                            {
                                                if (diem_dudoan < (double)8)
                                                {
                                                    diemchu = " 7.0 -> 7.9 (B)";
                                                }
                                                else
                                                {
                                                    if (diem_dudoan < (double)8.5)
                                                    {
                                                        diemchu = " 8.0 -> 8.4 (B+)";
                                                    }
                                                    else
                                                    {
                                                        diemchu = " 8.5 -> 10 (A)";
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            #endregion

                            if (diem_dudoan >= (double)4)
                            {
                                ketqua = ketqua + "Dự đoán kết quả môn học " + tenmonhoc + " :" +
                                         "\n- Kết quả: Đạt \n- Điểm dự đoán:" + diemchu + "\n\n";
                            }
                            else
                            {
                                ketqua = ketqua + "Dự đoán kết quả môn học " + tenmonhoc + " :" +
                                         "\n- Kết quả: Không đạt \n- Điểm dự đoán:" + diemchu +
                                         "\n\n";
                            }
                            #endregion
                        }
                    }
                }
            }
            return ketqua;
        }

        protected void btnHoTroID3_OnClick(object sender, EventArgs e)
        {
            DataTable DanhSachHocPhan = (DataTable)ViewState["DanhSachHocPhan"];
            DataRow[] xDataSearch = (from x in
                                         DanhSachHocPhan.AsEnumerable()
                                             .Where(d => d.Field<int>("ISBATBUOC") == 0)
                                     select x).ToArray();

            if (xDataSearch.Count() > 0)
            {
                DataTable dt = xDataSearch.CopyToDataTable();
                DataTable xdt = dt.AsEnumerable().GroupBy(t => t.Field<int>("ID_MONHOC")).Select(x => x.FirstOrDefault()).CopyToDataTable();
                ViewState["ThuatToan"] = xdt;
            }
            ThuatTuanID.DataSource = (DataTable)ViewState["ThuatToan"];
            ThuatTuanID.DataBind();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "thuatToanID();", true);
        }

        protected void btnDuDoanID_Click(object sender, EventArgs e)
        {
            DataTable ThuatToan = (DataTable)ViewState["ThuatToan"];
            if (ThuatToan != null && ThuatToan.Rows.Count > 0)
            {
                string ketqua = ThucHienThuatToanID(ThuatTuanID);
                txtThongbao.Text = ketqua;
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "thongBao();", true);
        }

        #endregion

        #region C45
        List<DATN.C45.DecisionTree_C45> iData_AllTree = new List<DATN.C45.DecisionTree_C45>(); // Danh sach tat ca cac tree sau khi thuc hien thuat toan

        string ThucHienThuatToan(GridView grdRow)
        {
            string ketqua = "";
            foreach (GridViewRow row in grdRow.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chon = (row.Cells[0].FindControl("chonmonhoc") as CheckBox);
                    if (chon.Checked)
                    {
                        int monhoc = Convert.ToInt32((row.Cells[1].FindControl("lblID_MONHOC") as Label).Text);
                        string tenmonhoc = (row.Cells[3].FindControl("lblTEN_MONHOC") as Label).Text;
                        #region Thuc hien thuat toan cho tung mon hoc

                        DataTable iGridDataSource = dangkyhocphan.GetDiemMau(Session["MA_SINHVIEN"].ToString(), monhoc);
                        DataTable Diem_SV = dangkyhocphan.GetDiem_SV(Session["MA_SINHVIEN"].ToString(), monhoc);
                        double diem_dudoan = TuVanMH(iGridDataSource, Diem_SV);

                        if (Double.IsNaN(diem_dudoan))
                        {
                            #region Không tìm được điểm chính xác

                            #region Tao lai du lieu mau

                            foreach (DataRow dr in iGridDataSource.Rows)
                            {
                                foreach (DataColumn xdc in iGridDataSource.Columns)
                                {
                                    if (!string.IsNullOrEmpty(dr[xdc.ColumnName].ToString()))
                                    {
                                        if (Convert.ToDouble(dr[xdc.ColumnName]) < (double)4)
                                        {
                                            dr[xdc.ColumnName] = 4; // 0 -> 3.9
                                        }
                                        else
                                        {
                                            if (Convert.ToDouble(dr[xdc.ColumnName]) < (double)5)
                                            {
                                                dr[xdc.ColumnName] = 5; // 4.0 -> 4.9
                                            }
                                            else
                                            {
                                                if (Convert.ToDouble(dr[xdc.ColumnName]) < (double)5.5)
                                                {
                                                    dr[xdc.ColumnName] = 5.5; // 5.0 -> 5.4
                                                }
                                                else
                                                {
                                                    if (Convert.ToDouble(dr[xdc.ColumnName]) < (double)6.5)
                                                    {
                                                        dr[xdc.ColumnName] = 6.5; // 5.5 -> 6.4
                                                    }
                                                    else
                                                    {
                                                        if (Convert.ToDouble(dr[xdc.ColumnName]) <
                                                            (double)7)
                                                        {
                                                            dr[xdc.ColumnName] = 7; // 6.5 -> 6.9
                                                        }
                                                        else
                                                        {
                                                            if (Convert.ToDouble(dr[xdc.ColumnName]) <
                                                                (double)8)
                                                            {
                                                                dr[xdc.ColumnName] = 8; // 7.0 -> 7.9
                                                            }
                                                            else
                                                            {
                                                                if (Convert.ToDouble(dr[xdc.ColumnName]) <
                                                                    (double)8.5)
                                                                {
                                                                    dr[xdc.ColumnName] = 8.5; // 8.0 -> 8.4
                                                                }
                                                                else
                                                                {
                                                                    dr[xdc.ColumnName] = 10; // 8.5 -> 10
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            #endregion

                            if (iGridDataSource.Rows.Count < 1)
                            {
                                ketqua = ketqua + "Dự đoán kết quả môn học " + tenmonhoc + " :" +
                                         "\n- Kết quả: Không thể dự đoán \n- Điểm dự đoán: Không có dữ liệu mẫu nên không thể dự đoán được\n\n";
                            }
                            else
                            {
                                //4-F 5-D 5.5-D+ 6.5-C 7-C+ 8-B 8.5-B+ 10-A
                                #region Tao lai diem DK

                                foreach (DataColumn xdc in Diem_SV.Columns)
                                {
                                    if (string.IsNullOrEmpty(Diem_SV.Rows[0][xdc.ColumnName].ToString()))
                                    {
                                        Diem_SV.Rows[0][xdc.ColumnName] = 4;
                                    }
                                    else
                                    {
                                        if (Convert.ToDouble(Diem_SV.Rows[0][xdc.ColumnName]) < (double)4)
                                        {
                                            Diem_SV.Rows[0][xdc.ColumnName] = 4;
                                        }
                                        else
                                        {
                                            if (Convert.ToDouble(Diem_SV.Rows[0][xdc.ColumnName]) < (double)5)
                                            {
                                                Diem_SV.Rows[0][xdc.ColumnName] = 5;
                                            }
                                            else
                                            {
                                                if (Convert.ToDouble(Diem_SV.Rows[0][xdc.ColumnName]) < (double)5.5)
                                                {
                                                    Diem_SV.Rows[0][xdc.ColumnName] = 5.5;
                                                }
                                                else
                                                {
                                                    if (Convert.ToDouble(Diem_SV.Rows[0][xdc.ColumnName]) <
                                                        (double)6.5)
                                                    {
                                                        Diem_SV.Rows[0][xdc.ColumnName] = 6.5;
                                                    }
                                                    else
                                                    {
                                                        if (Convert.ToDouble(Diem_SV.Rows[0][xdc.ColumnName]) <
                                                            (double)7)
                                                        {
                                                            Diem_SV.Rows[0][xdc.ColumnName] = 7;
                                                        }
                                                        else
                                                        {
                                                            if (Convert.ToDouble(Diem_SV.Rows[0][xdc.ColumnName]) <
                                                                (double)8)
                                                            {
                                                                Diem_SV.Rows[0][xdc.ColumnName] = 8;
                                                            }
                                                            else
                                                            {
                                                                if (
                                                                    Convert.ToDouble(Diem_SV.Rows[0][xdc.ColumnName]) <
                                                                    (double)8.5)
                                                                {
                                                                    Diem_SV.Rows[0][xdc.ColumnName] = 8.5;
                                                                }
                                                                else
                                                                {
                                                                    Diem_SV.Rows[0][xdc.ColumnName] = 10;
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }

                                #endregion

                                diem_dudoan = TuVanMH(iGridDataSource, Diem_SV);
                                if (diem_dudoan == 4)
                                {
                                    ketqua = ketqua + "Dự đoán kết quả môn học " + tenmonhoc +
                                             " :" +
                                             "\n- Kết quả: Không đạt \n- Điểm dự đoán: 0 -> 3.9 (F)\n\n";
                                }
                                if (diem_dudoan == 5)
                                {
                                    ketqua = ketqua + "Dự đoán kết quả môn học " + tenmonhoc +
                                             " :" +
                                             "\n- Kết quả: Đạt \n- Điểm dự đoán: 4.0 -> 4.9 (D) \n\n";
                                }
                                if (diem_dudoan == 5.5)
                                {
                                    ketqua = ketqua + "Dự đoán kết quả môn học " + tenmonhoc +
                                             " :" +
                                             "\n- Kết quả: Đạt \n- Điểm dự đoán: 5.0 -> 5.4 (D+) \n\n";
                                }
                                if (diem_dudoan == 6.5)
                                {
                                    ketqua = ketqua + "Dự đoán kết quả môn học " + tenmonhoc +
                                             " :" +
                                             "\n- Kết quả: Đạt \n- Điểm dự đoán: 5.5 -> 6.4 (C) \n\n";
                                }
                                if (diem_dudoan == 7)
                                {
                                    ketqua = ketqua + "Dự đoán kết quả môn học " + tenmonhoc +
                                             " :" +
                                             "\n- Kết quả: Đạt \n- Điểm dự đoán: 6.5 -> 6.9 (C+) \n\n";
                                }
                                if (diem_dudoan == 8)
                                {
                                    ketqua = ketqua + "Dự đoán kết quả môn học " + tenmonhoc +
                                             " :" +
                                             "\n- Kết quả: Đạt \n- Điểm dự đoán: 7.0 -> 7.9 (B) \n\n";
                                }
                                if (diem_dudoan == 8.5)
                                {
                                    ketqua = ketqua + "Dự đoán kết quả môn học " + tenmonhoc +
                                             " :" +
                                             "\n- Kết quả: Đạt \n- Điểm dự đoán: 8.0 -> 8.4 (B+) \n\n";
                                }
                                if (diem_dudoan == 10)
                                {
                                    ketqua = ketqua + "Dự đoán kết quả môn học " + tenmonhoc +
                                             " :" +
                                             "\n- Kết quả: Đạt \n- Điểm dự đoán: 8.5 -> 10 (A) \n\n";
                                }
                                if (Double.IsNaN(diem_dudoan))
                                {
                                    #region Xét đậu rớt khi không tìm được điểm

                                    foreach (DataRow dr in iGridDataSource.Rows)
                                    {
                                        if (Convert.ToDouble(dr[monhoc]) == (double)4)
                                        {
                                            dr[monhoc] = 0;
                                        }
                                        else
                                        {
                                            dr[monhoc] = 10;
                                        }
                                    }
                                    diem_dudoan = TuVanMH(iGridDataSource, Diem_SV);
                                    if (diem_dudoan == 10)
                                    {
                                        ketqua = ketqua + "Dự đoán kết quả môn học " + tenmonhoc +
                                                 " :" +
                                                 "\n- Kết quả: Đạt \n- Điểm dự đoán: Dữ liệu quá ít không thể dự đoán được\n\n";
                                    }
                                    else
                                    {
                                        ketqua = ketqua + "Dự đoán kết quả môn học " + tenmonhoc +
                                                 " :" +
                                                 "\n- Kết quả: Không đạt \n- Điểm dự đoán: Dữ liệu quá ít không thể dự đoán được\n\n";
                                    }

                                    #endregion
                                }
                            }

                            #endregion
                        }
                        else
                        {
                            #region Diem chu

                            string diemchu = "";
                            if (diem_dudoan < (double)4)
                            {
                                diemchu = " 0 -> 3.9 (F)";
                            }
                            else
                            {
                                if (diem_dudoan < (double)5)
                                {
                                    diemchu = " 4.0 -> 4.9 (D)";
                                }
                                else
                                {
                                    if (diem_dudoan < (double)5.5)
                                    {
                                        diemchu = " 5.0 -> 5.4 (D+)";
                                    }
                                    else
                                    {
                                        if (diem_dudoan < (double)6.5)
                                        {
                                            diemchu = " 5.5 -> 6.4 (C)";
                                        }
                                        else
                                        {
                                            if (diem_dudoan < (double)7)
                                            {
                                                diemchu = " 6.5 -> 6.9 (C+)";
                                            }
                                            else
                                            {
                                                if (diem_dudoan < (double)8)
                                                {
                                                    diemchu = " 7.0 -> 7.9 (B)";
                                                }
                                                else
                                                {
                                                    if (diem_dudoan < (double)8.5)
                                                    {
                                                        diemchu = " 8.0 -> 8.4 (B+)";
                                                    }
                                                    else
                                                    {
                                                        diemchu = " 8.5 -> 10 (A)";
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            #endregion

                            if (diem_dudoan >= (double)4)
                            {
                                ketqua = ketqua + "Dự đoán kết quả môn học " + tenmonhoc + " :" +
                                         "\n- Kết quả: Đạt \n- Điểm dự đoán:" + diemchu + "\n\n";
                            }
                            else
                            {
                                ketqua = ketqua + "Dự đoán kết quả môn học " + tenmonhoc + " :" +
                                         "\n- Kết quả: Không đạt \n- Điểm dự đoán:" + diemchu +
                                         "\n\n";
                            }
                        }
                        #endregion
                    }
                }
            }
            return ketqua;
        }

        private double TuVanMH(DataTable iData_DiemMau, DataTable iData_DiemDieuKien)
        {
            try
            {
                #region Thuc hien thuat toan cho tung mon hoc

                DataTable iGridDataSource = iData_DiemMau;

                List<double> ds_diemTK = new List<double>(); //Danh sach cac bo diem TK co trong bang du lieu

                #region Lay danh sach diem KET QUA MON DANG TU VAN

                foreach (DataRow drTK in iGridDataSource.Rows)
                {
                    int check = 0;
                    foreach (double kt in ds_diemTK)
                    {
                        if (kt == Convert.ToDouble(drTK[iGridDataSource.Columns.Count - 1]))
                        {
                            check = 1;
                        }
                    }
                    if (check == 0)
                    {
                        ds_diemTK.Add(Convert.ToDouble(drTK[iGridDataSource.Columns.Count - 1]));
                    }
                }
                if (ds_diemTK.Count % 2 != 0)
                {
                    ds_diemTK.Add(ds_diemTK[0]);
                }

                #endregion

                string xcolumn = iGridDataSource.Columns[iGridDataSource.Columns.Count - 1].ColumnName; // Ten cot diem TK mon dang xet
                iData_AllTree.Clear();

                #region Tao tree

                for (int id = 0; id < ds_diemTK.Count - 1; id += 2)
                {
                    #region Tao Attribute

                    List<DATN.C45.Attribute> Attributes = new List<DATN.C45.Attribute>(); // danh sach thuoc tinh
                    for (int i = 0; i < iGridDataSource.Columns.Count - 1; i++)
                    {
                        DATN.C45.Attribute x = new DATN.C45.Attribute();
                        x.Name = iGridDataSource.Columns[i].ColumnName;
                        Attributes.Add(x);
                    }

                    #endregion

                    #region Duyet 1 lan 2 gia tri diem TK de tao tree
                    DataTable iDataXet = null;
                    var filteredRows = from row in iGridDataSource.AsEnumerable()
                                       where row.Field<double>(xcolumn) == ds_diemTK[id] || row.Field<double>(xcolumn) == ds_diemTK[id + 1]
                                       select row;
                    DataRow[] xcheck = filteredRows.ToArray();
                    //DataRow[] xcheck = iGridDataSource.Select("Convert(" + xcolumn + ",'System.Double')= " + ds_diemTK[id].ToString() + " or Convert(" + xcolumn + ",'System.Double')= " + ds_diemTK[id + 1].ToString());
                    iDataXet = xcheck.CopyToDataTable();

                    #endregion

                    #region Tao tree

                    for (int i = 0; i < iDataXet.Rows.Count; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            int check = 0;
                            for (int z = 0; z < Attributes[j].Value.Count; z++)
                            {
                                if (Convert.ToDouble(Attributes[j].Value[z]) == Convert.ToDouble(iDataXet.Rows[i][j]))
                                {
                                    check = 1;
                                }
                            }
                            if (check == 0)
                            {
                                Attributes[j].Value.Add(Convert.ToDouble(iDataXet.Rows[i][j]));
                            }
                        }
                    }

                    #region Tao ds dieu kien mau

                    List<List<double>> Examples = new List<List<double>>();
                    foreach (DataRow xdr in iDataXet.Rows)
                    {
                        List<double> example = new List<double>();
                        for (int i = 0; i < iDataXet.Columns.Count; i++)
                        {
                            example.Add(Convert.ToDouble(xdr[i]));
                        }
                        Examples.Add(example);
                    }

                    #endregion

                    List<DATN.C45.Attribute> at = new List<DATN.C45.Attribute>();
                    for (int i = 0; i < Attributes.Count; i++)
                    {
                        at.Add(Attributes[i]);
                    }
                    List<double> ds_tmp = new List<double>();
                    for (int i = 0; i < Examples.Count; i++)
                    {
                        int check = 0;
                        foreach (double value in ds_tmp)
                        {
                            if (Examples[i][Examples[i].Count - 1] == value)
                            {
                                check = 1;
                            }
                        }
                        if (check == 0)
                            ds_tmp.Add(Examples[i][Examples[i].Count - 1]);
                    }
                    DATN.C45.DecisionTree_C45 DTID3 = new DecisionTree_C45(Examples, at, ds_diemTK[id], ds_diemTK[id + 1]);
                    DTID3.GetTree();
                    iData_AllTree.Add(DTID3);
                    #endregion
                }

                #endregion

                #region Duyet tree lay gia tri

                DataTable Diem_SV = iData_DiemDieuKien;

                List<double> result = new List<double>();

                foreach (DATN.C45.DecisionTree_C45 d in iData_AllTree)
                {
                    result = d.SearchTree(d.Tree, Diem_SV, result, false);
                }
                double tong = 0;
                string ds_dudoan = "";
                foreach (double b in result)
                {
                    ds_dudoan = ds_dudoan + "\n" + b.ToString();
                    tong += b;
                }
                double diem_dudoan = (double)tong / result.Count;

                #endregion

                return diem_dudoan;

                #endregion
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnHoTroDungC45_OnClick(object sender, EventArgs e)
        {
            DataTable DanhSachHocPhan = (DataTable)ViewState["DanhSachHocPhan"];
            DataRow[] xDataSearch = (from x in
                                         DanhSachHocPhan.AsEnumerable()
                                             .Where(d => d.Field<int>("ISBATBUOC") == 0)
                                     select x).ToArray();

            if (xDataSearch.Count() > 0)
            {
                DataTable dt = xDataSearch.CopyToDataTable();
                DataTable xdt = dt.AsEnumerable().GroupBy(t => t.Field<int>("ID_MONHOC")).Select(x => x.FirstOrDefault()).CopyToDataTable();
                ViewState["ThuatToan"] = xdt;
            } GridThuatToan.DataSource = (DataTable)ViewState["ThuatToan"];
            GridThuatToan.DataBind();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "popUpPage();", true);
        }

        protected void btnDuDoan_Click(object sender, EventArgs e)
        {
            DataTable ThuatToan = (DataTable)ViewState["ThuatToan"];
            if (ThuatToan != null && ThuatToan.Rows.Count > 0)
            {
                string ketqua = ThucHienThuatToan(GridThuatToan); txtThongbao.Text = ketqua;
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "thongBao();", true);
        }

        protected void btnLamMoi_Click(object sender, EventArgs e)
        {

        }
        #endregion
    }
}