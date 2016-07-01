using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DATN.DATA;

namespace DATN.BUS
{
    public class bus_dangnhap
    {
        db_ttsDataContext db = new db_ttsDataContext();

        public int CheckLogin(string account, string pass)
        {
            var xcheck = from nd in db.tbL_SINHVIENs
                         where nd.MA_SINHVIEN.Equals(account) && nd.PASSWORD.Equals(pass)
                         select nd;
            DataTable xdt = TableUtil.LinqToDataTable(xcheck);
            if (xdt != null && xdt.Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(xdt.Rows[0]["ID_SINHVIEN"].ToString()))
                {
                    UserCommon.IdSinhVien = Convert.ToInt32(xdt.Rows[0]["ID_SINHVIEN"].ToString());
                    UserCommon.HoTenSinhVien =
                        (from d in db.tbL_SINHVIENs
                         where d.ID_SINHVIEN == Convert.ToInt32(xdt.Rows[0]["ID_SINHVIEN"].ToString())
                         select d).First().TEN_SINHVIEN;
                    UserCommon.UserName = account;
                }
                return 1; // dang nhap thanh cong
            }
            return 0; // tai khoan khong dung
        }

        public DataTable GetThongTinSinhVien(int id_sinhvien)
        {
            try
            {
                DataTable dt = null;
                var thongtinsinhvien = from sv in db.tbL_SINHVIENs
                                       join l in db.tbl_LOPHOCs on new { ID_LOPHOC = Convert.ToInt32(sv.ID_LOPHOC) } equals
                                           new { ID_LOPHOC = l.ID_LOPHOC }
                                       join khngang in db.tbl_KHOAHOC_NGANHs on
                                           new { ID_KHOAHOC_NGANH = Convert.ToInt32(l.ID_KHOAHOC_NGANH) } equals
                                           new { ID_KHOAHOC_NGANH = khngang.ID_KHOAHOC_NGANH }
                                       join kh in db.tbl_KHOAHOCs on new { ID_KHOAHOC = Convert.ToInt32(khngang.ID_KHOAHOC) } equals
                                           new { ID_KHOAHOC = kh.ID_KHOAHOC }
                                       join ng in db.tbl_NGANHs on new { ID_NGANH = Convert.ToInt32(khngang.ID_NGANH) } equals
                                           new { ID_NGANH = ng.ID_NGANH }
                                       join hdt in db.tbl_HEDAOTAOs on new { ID_HE_DAOTAO = Convert.ToInt32(kh.ID_HE_DAOTAO) } equals
                                           new { ID_HE_DAOTAO = hdt.ID_HE_DAOTAO }
                                       where
                                           (sv.IS_DELETE != 1 ||
                                            sv.IS_DELETE == null) &&
                                           (l.IS_DELETE != 1 ||
                                            l.IS_DELETE == null) &&
                                           (khngang.IS_DELETE != 1 ||
                                            khngang.IS_DELETE == null) &&
                                           (kh.IS_DELETE != 1 ||
                                            kh.IS_DELETE == null) &&
                                           (ng.IS_DELETE != 1 ||
                                            ng.IS_DELETE == null) &&
                                           (hdt.IS_DELETE != 1 ||
                                            hdt.IS_DELETE == null) &&
                                           sv.ID_SINHVIEN == id_sinhvien
                                       select new
                                       {
                                           sv.ID_SINHVIEN,
                                           sv.MA_SINHVIEN,
                                           sv.TEN_SINHVIEN,
                                           sv.NGAYSINH,
                                           sv.NOISINH,
                                           sv.DIACHI,
                                           sv.CMND,
                                           sv.NGAYCAP,
                                           sv.NOICAP,
                                           sv.GIOITINH,
                                           sv.DIENTHOAI,
                                           sv.EMAIL,
                                           sv.THONGTIN_NGOAITRU,
                                           hdt.ID_HE_DAOTAO,
                                           l.ID_LOPHOC,
                                           l.TEN_LOP,
                                           ng.ID_NGANH,
                                           ng.TEN_NGANH,
                                           kh.TEN_KHOAHOC,
                                           hdt.TEN_HE_DAOTAO,
                                           kh.ID_KHOAHOC,
                                           KHOAHOC = kh.NAM_BD + "-" + kh.NAM_KT
                                       };
                dt = TableUtil.LinqToDataTable(thongtinsinhvien);
                return dt;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

    }
}
