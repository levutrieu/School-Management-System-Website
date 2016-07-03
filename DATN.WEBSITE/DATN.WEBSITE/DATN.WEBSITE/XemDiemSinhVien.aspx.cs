using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using DATN.BUS;
using DevExpress.Web;
using DevExpress.Web.ASPxTreeList;

namespace DATN.WEBSITE
{
    public partial class XemDiemSinhVien : System.Web.UI.Page
    {
        bus_dangkyhocphan client = new bus_dangkyhocphan();

        bus_dangnhap dangnhap = new bus_dangnhap();

        private DataTable iDataSoure = null;

        private DataTable iGridDataSoure = null;

        double TinhDiemHe10(DataTable xdt)
        {
            try
            {
                double res = 0;
                int stc = 0;
                foreach (DataRow r in xdt.Rows)
                {
                    res += Convert.ToDouble(r["DIEM_TONG"].ToString()) * Convert.ToInt32(r["SO_TC"].ToString());
                    stc += Convert.ToInt32(r["SO_TC"].ToString());
                }
                return Math.Round(((double)res / stc), 2);
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        double TinhDiemTong(DataTable xdt)
        {
            double res = 0;
            int stc = 0;
            foreach (DataRow r in xdt.Rows)
            {
                string temp = r["DIEM_CHU"].ToString().ToUpper().Trim();
                if (temp.Equals("A"))
                {
                    res += Convert.ToDouble(r["SO_TC"].ToString()) * 4;
                    stc += Convert.ToInt32(r["SO_TC"].ToString());
                }
                if (temp.Equals("B+"))
                {
                    res += Convert.ToDouble(r["SO_TC"].ToString()) * 3.5;
                    stc += Convert.ToInt32(r["SO_TC"].ToString());
                }
                if (temp.Equals("B"))
                {
                    res += Convert.ToDouble(r["SO_TC"].ToString()) * 3;
                    stc += Convert.ToInt32(r["SO_TC"].ToString());
                }
                if (temp.Equals("C+"))
                {
                    res += Convert.ToDouble(r["SO_TC"].ToString()) * 2.5;
                    stc += Convert.ToInt32(r["SO_TC"].ToString());
                }
                if (temp.Equals("C"))
                {
                    res += Convert.ToDouble(r["SO_TC"].ToString()) * 2;
                    stc += Convert.ToInt32(r["SO_TC"].ToString());
                }
                if (temp.Equals("D+"))
                {
                    res += Convert.ToDouble(r["SO_TC"].ToString()) * 1.5;
                    stc += Convert.ToInt32(r["SO_TC"].ToString());
                }
                if (temp.Equals("D"))
                {
                    res += Convert.ToDouble(r["SO_TC"].ToString()) * 1;
                    stc += Convert.ToInt32(r["SO_TC"].ToString());
                }
                if (temp.Equals("F"))
                {
                    res += Convert.ToDouble(r["SO_TC"].ToString()) * 0;
                    stc += Convert.ToInt32(r["SO_TC"].ToString());
                }
            }
            return Math.Round(((double)res / stc), 2);
        }

        int TinhSOTCDat(DataTable xdt)
        {
            try
            {
                int stc = 0;
                foreach (DataRow r in xdt.Rows)
                {
                    string temp = r["DIEM_CHU"].ToString().ToUpper().Trim();
                    if (!temp.Equals("F"))
                    {
                        stc += Convert.ToInt32(r["SO_TC"].ToString());
                    }

                }
                return stc;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        void LoadDiemToGrid(int idsinhvien)
        {
            try
            {
                DataTable dt = client.GetDanhSachDiem(idsinhvien);
                DataTable xdt = dt.Clone();
                #region
                foreach (DataRow r in dt.Rows)
                {
                    if (r["ID_PARENT"].ToString() == "")
                    {
                        r["MA_MONHOC"] = "";
                        r["SO_TC"] = DBNull.Value;
                        r["DIEM_BT"] = DBNull.Value;
                        r["DIEM_GK"] = DBNull.Value;
                        r["DIEM_CK"] = DBNull.Value;
                        r["DIEM_TONG"] = DBNull.Value;
                        r["DIEM_HE4"] = DBNull.Value;
                        r["DIEM_CHU"] = DBNull.Value;
                        r["ID_PARENT"] = DBNull.Value;

                        xdt.ImportRow(r);
                    }
                    else
                    {
                        xdt.ImportRow(r);
                    }
                }
                #endregion
                DataTable dtcopy = xdt.Copy();
                int count = 1;
                foreach (DataRow xdr in xdt.Rows)
                {
                    if (xdr["ID_PARENT"].ToString() == "")
                    {
                        DataTable zdt = new DataTable();
                        string x = (xdr["ID"].ToString());
                        DataRow[] row = (from temp in xdt.AsEnumerable()
                                    .Where(t => t.Field<string>("ID_PARENT") == x)
                                         select temp).ToArray();
                        if (row.Count() > 0)
                        {
                            zdt = row.CopyToDataTable();
                            double res = TinhDiemTong(zdt);
                            double diemtong = TinhDiemHe10(zdt);

                            DataRow r = null;

                            #region
                            r = dtcopy.NewRow();
                            r["ID"] = x + r["ID"].ToString() + (count + 12121);
                            r["MA_MONHOC"] = "";
                            r["SO_TC"] = DBNull.Value;
                            r["DIEM_BT"] = DBNull.Value;
                            r["DIEM_GK"] = DBNull.Value;
                            r["DIEM_CK"] = DBNull.Value;
                            r["DIEM_TONG"] = DBNull.Value;
                            r["DIEM_HE4"] = DBNull.Value;
                            r["DIEM_CHU"] = DBNull.Value;
                            r["ID_PARENT"] = x;
                            r["NAME"] = "";
                            dtcopy.Rows.Add(r);

                            r = dtcopy.NewRow();
                            r["ID"] = x + r["ID"].ToString() + (count + 11222);
                            r["MA_MONHOC"] = "";
                            r["SO_TC"] = DBNull.Value;
                            r["DIEM_BT"] = DBNull.Value;
                            r["DIEM_GK"] = DBNull.Value;
                            r["DIEM_CK"] = DBNull.Value;
                            r["DIEM_TONG"] = DBNull.Value;
                            r["DIEM_HE4"] = DBNull.Value;
                            r["DIEM_CHU"] = DBNull.Value;
                            r["ID_PARENT"] = x;
                            r["NAME"] = "";
                            dtcopy.Rows.Add(r);
                            #endregion

                            r = dtcopy.NewRow();
                            r["ID"] = x + r["ID"].ToString() + (count + 9999);
                            r["MA_MONHOC"] = "";
                            r["SO_TC"] = DBNull.Value;
                            r["DIEM_BT"] = DBNull.Value;
                            r["DIEM_GK"] = DBNull.Value;
                            r["DIEM_CK"] = DBNull.Value;
                            r["DIEM_TONG"] = DBNull.Value;
                            r["DIEM_HE4"] = DBNull.Value;
                            r["DIEM_CHU"] = DBNull.Value;
                            r["ID_PARENT"] = x;
                            r["NAME"] = "Điểm tổng hệ 4:   " + res;
                            dtcopy.Rows.Add(r);

                            r = dtcopy.NewRow();
                            r["ID"] = x + r["ID"].ToString() + (count + 7173183);
                            r["MA_MONHOC"] = "";
                            r["SO_TC"] = DBNull.Value;
                            r["DIEM_BT"] = DBNull.Value;
                            r["DIEM_GK"] = DBNull.Value;
                            r["DIEM_CK"] = DBNull.Value;
                            r["DIEM_TONG"] = DBNull.Value;
                            r["DIEM_HE4"] = DBNull.Value;
                            r["DIEM_CHU"] = DBNull.Value;
                            r["ID_PARENT"] = x;
                            r["NAME"] = "Điểm tổng hệ 10:   " + diemtong;
                            dtcopy.Rows.Add(r);

                            r = dtcopy.NewRow();
                            r["ID"] = x + r["ID"].ToString() + (count + 453453);
                            r["MA_MONHOC"] = "";
                            r["SO_TC"] = DBNull.Value;
                            r["DIEM_BT"] = DBNull.Value;
                            r["DIEM_GK"] = DBNull.Value;
                            r["DIEM_CK"] = DBNull.Value;
                            r["DIEM_TONG"] = DBNull.Value;
                            r["DIEM_HE4"] = DBNull.Value;
                            r["DIEM_CHU"] = DBNull.Value;
                            r["ID_PARENT"] = x;
                            r["NAME"] = "Số TC đã đạt:   " + TinhSOTCDat(zdt);
                            dtcopy.Rows.Add(r);

                            r = dtcopy.NewRow();
                            #region
                            r["ID"] = x + r["ID"].ToString() + (count + 1000);
                            r["MA_MONHOC"] = "";
                            r["SO_TC"] = DBNull.Value;
                            r["DIEM_BT"] = DBNull.Value;
                            r["DIEM_GK"] = DBNull.Value;
                            r["DIEM_CK"] = DBNull.Value;
                            r["DIEM_TONG"] = DBNull.Value;
                            r["DIEM_HE4"] = DBNull.Value;
                            r["DIEM_CHU"] = DBNull.Value;
                            r["ID_PARENT"] = x;
                            r["NAME"] = "";
                            dtcopy.Rows.Add(r);
                            #endregion
                        }


                    }
                    count++;
                }
                iGridDataSoure = dtcopy.Copy();
                treegrd.DataSource = iGridDataSoure;
                treegrd.KeyFieldName = "ID";
                treegrd.ParentFieldName = "ID_PARENT";
                treegrd.DataBind();
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["ID_SINHVIEN"] == null)
            {
                groupinfo.Visible = false;
            }
            else
            {
                LoadDiemToGrid(Convert.ToInt32(Session["ID_SINHVIEN"]));
                iDataSoure = dangnhap.GetThongTinSinhVien(Convert.ToInt32(Session["ID_SINHVIEN"]));
                ViewState["iDataSoure"] = iDataSoure;
                txtMaSinhVien.Text = iDataSoure.Rows[0]["MA_SINHVIEN"].ToString();
                txtTenSinhVien.Text = iDataSoure.Rows[0]["TEN_SINHVIEN"].ToString();
                txtLop.Text = iDataSoure.Rows[0]["TEN_LOP"].ToString();
                txtNganh.Text = iDataSoure.Rows[0]["TEN_NGANH"].ToString();
                txtHeDaoTao.Text = iDataSoure.Rows[0]["TEN_HE_DAOTAO"].ToString();
                txtNienKhoa.Text = iDataSoure.Rows[0]["KHOAHOC"].ToString();

                groupnhapthongtin.Visible = false;
                groupinfo.Visible = true;
            }
        }

        protected void btnXemDiem_OnClick_OnClick(object sender, EventArgs e){
            bus_dangnhap dangnhap = new bus_dangnhap();
            if (string.IsNullOrEmpty(textmasv.Text))
            {
                
            }
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
                LoadDiemToGrid(Convert.ToInt32(Session["ID_SINHVIEN"]));groupinfo.Visible = true;
                groupinfo.Visible = true;
                groupnhapthongtin.Visible = false;
            }
        }
        
    }
}