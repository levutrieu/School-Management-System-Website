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
    public partial class HomeDefault : System.Web.UI.MasterPage
    {
        bus_dangnhap dangnhap = new bus_dangnhap();protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["ID_SINHVIEN"] == null)
            {
                btnlogin.InnerText = "Đăng nhập";
            }
            else
            {
                btnlogin.InnerText = "Đăng xuất";
            }}
        protected void btnlogin_OnClick(object sender, EventArgs e)
        {

            if (btnlogin.InnerText == "Đăng xuất")
            {
                Session.Clear();
                ViewState.Clear();
                Response.Redirect("TrangChu.aspx");
            }
            if (btnlogin.InnerText == "Đăng nhập")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
        }

        protected void btnDangNhap_OnClick(object sender, EventArgs e)
        {
            int res = dangnhap.CheckLogin(txtMaSinhVien.Text, txtMaKhau.Text);
            if (res == 1)
            {
                Session["ID_SINHVIEN"] = UserCommon.IdSinhVien;
                Session["MA_SINHVIEN"] = txtMaSinhVien.Text;
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