using System;
using System.Collections.Generic;
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
        bus_dangnhap client = new bus_dangnhap();
        protected void btnDangNhap_OnClick(object sender, EventArgs e)
        {
            int res = client.CheckLogin(txtMaSinhVien.Text, txtMaKhau.Text);
            if (res == 1)
            {
                Session["ID_SINHVIEN"] = UserCommon.IdSinhVien;
                Response.Redirect("DangKyMonHoc.aspx");
            }
            else
            {
                Response.Write("Sai thông tin đăng nhập");
            }
        }
    }
}