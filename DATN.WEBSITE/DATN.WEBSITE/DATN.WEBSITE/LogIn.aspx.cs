using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DATN.BUS;

namespace DATN.WEBSITE
{
    public partial class LogIn : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && !IsCallback)
            {
                if (Session["ID_SINHVIEN"] == null)
                {
                   // Response.Redirect("LogIn.aspx");
                }
            }
        }
        bus_dangnhap dangnhap = new bus_dangnhap();
        protected void btnDangNhap_OnClick(object sender, EventArgs e)
        {
            int res = dangnhap.CheckLogin(txtMaSinhVien.Text, txtMaKhau.Text);
            if (res == 1)
            {
                Session["ID_SINHVIEN"] = UserCommon.IdSinhVien;
                Response.Redirect("TrangChu.aspx");
                DataTable iDataSoure = dangnhap.GetThongTinSinhVien(Convert.ToInt32(Session["ID_SINHVIEN"]));
                ViewState["iDataSoure"] = iDataSoure;
            }
            else
            {
                Response.Write("Sai thông tin đăng nhập");
            }
        }
    }
}