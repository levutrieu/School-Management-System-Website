using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DATN.DATA;

namespace DATN.BUS
{
    public class bus_dangkyhocphan
    {
        db_ttsDataContext db = new db_ttsDataContext();

        #region
        public DataTable GetDanhSachDiem(int id_sinhvien)
        {
            try
            {
                DataTable dt = null;
                var xxxx = (
                    from hkht in db.tbl_NAMHOC_HKY_HTAIs
                    join nhht in db.tbl_NAMHOC_HIENTAIs on new { ID_NAMHOC_HIENTAI = Convert.ToInt32(hkht.ID_NAMHOC_HIENTAI) } equals new { ID_NAMHOC_HIENTAI = nhht.ID_NAMHOC_HIENTAI }
                    where
                        (hkht.IS_DELETE == null || hkht.IS_DELETE != 1) && (nhht.IS_DELETE != 1 || nhht.IS_DELETE == null) &&
                        (from hp in db.tbl_LOP_HOCPHANs
                         where (hp.IS_DELETE != 1 || hp.IS_DELETE == null) && hkht.ID_NAMHOC_HKY_HTAI == hp.ID_NAMHOC_HKY_HTAI &&
                             (from diem in db.tbl_DIEM_SINHVIENs
                              where diem.IS_DELETE != 1 || diem.IS_DELETE == null
                              select new
                              {
                                  diem.ID_LOPHOCPHAN
                              }).Contains(new { ID_LOPHOCPHAN = (System.Int32?)hp.ID_LOPHOCPHAN })
                         select new
                         {
                             hp.ID_NAMHOC_HKY_HTAI
                         }).Contains(new { ID_NAMHOC_HKY_HTAI = (System.Int32?)hkht.ID_NAMHOC_HKY_HTAI })
                    select new
                    {
                        ID = ("HK" + Convert.ToString(hkht.ID_NAMHOC_HKY_HTAI)),
                        NAME = ("Học kỳ" + " " + Convert.ToString(hkht.HOCKY) + " Năm " + Convert.ToString(nhht.NAMHOC_TU) + "-" + Convert.ToString(nhht.NAMHOC_DEN)),
                        MA_MONHOC = "",
                        SO_TC = 0,
                        DIEM_BT = (double?)0,
                        DIEM_GK = (double?)0,
                        DIEM_CK = (double?)0,
                        DIEM_TONG = (double?)0,
                        DIEM_HE4 = (double?)0,
                        DIEM_CHU = "",
                        CACH_TINHDIEM = "",
                        ID_PARENT = ""
                    }).Concat
                    (
                        from diem in db.tbl_DIEM_SINHVIENs
                        join hp in db.tbl_LOP_HOCPHANs on
                            new { ID_LOPHOCPHAN = Convert.ToInt32(diem.ID_LOPHOCPHAN) } equals new { ID_LOPHOCPHAN = hp.ID_LOPHOCPHAN }

                        join mh in db.tbl_MONHOCs on
                            new { ID_MONHOC = Convert.ToInt32(hp.ID_MONHOC) } equals new { ID_MONHOC = mh.ID_MONHOC }
                        join hkht in db.tbl_NAMHOC_HKY_HTAIs on
                            new { ID_NAMHOC_HKY_HTAI = Convert.ToInt32(hp.ID_NAMHOC_HKY_HTAI) } equals new { ID_NAMHOC_HKY_HTAI = hkht.ID_NAMHOC_HKY_HTAI }

                        join nhht in db.tbl_NAMHOC_HIENTAIs on
                        new { ID_NAMHOC_HIENTAI = Convert.ToInt32(hkht.ID_NAMHOC_HIENTAI) } equals new { ID_NAMHOC_HIENTAI = nhht.ID_NAMHOC_HIENTAI }

                        where
                            (diem.IS_DELETE != 1 || diem.IS_DELETE == null) && diem.ID_SINHVIEN == id_sinhvien
                        select new
                        {
                            ID = ("D" + Convert.ToString(diem.ID_KETQUA)),
                            NAME = mh.TEN_MONHOC,
                            MA_MONHOC = mh.MA_MONHOC,
                            SO_TC = (int)mh.SO_TC,
                            DIEM_BT = (double?)diem.DIEM_BT,
                            DIEM_GK = (double?)diem.DIEM_GK,
                            DIEM_CK = (double?)diem.DIEM_CK,
                            DIEM_TONG = (double?)diem.DIEM_TONG,
                            DIEM_HE4 = (double?)diem.DIEM_HE4,
                            DIEM_CHU = diem.DIEM_CHU,
                            CACH_TINHDIEM = mh.CACH_TINHDIEM,
                            ID_PARENT = ("HK" + Convert.ToString(hp.ID_NAMHOC_HKY_HTAI))
                        }
                    );

                dt = TableUtil.LinqToDataTable(xxxx);
                return dt;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int GetHeDaoTao(int idsinhvien)
        {
            try
            {
                int res = 0;
                var idhedaotao = (from l in db.tbl_LOPHOCs
                                  join sv in db.tbL_SINHVIENs on new { ID_LOPHOC = l.ID_LOPHOC } equals new { ID_LOPHOC = Convert.ToInt32(sv.ID_LOPHOC) }
                                  join kn in db.tbl_KHOAHOC_NGANHs on new { ID_KHOAHOC_NGANH = Convert.ToInt32(l.ID_KHOAHOC_NGANH) } equals new { ID_KHOAHOC_NGANH = kn.ID_KHOAHOC_NGANH }
                                  join kh in db.tbl_KHOAHOCs on new { ID_KHOAHOC = Convert.ToInt32(kn.ID_KHOAHOC) } equals new { ID_KHOAHOC = kh.ID_KHOAHOC }
                                  where
                                    (kn.IS_DELETE != 1 ||
                                    kn.IS_DELETE == null) &&
                                    (kh.IS_DELETE != 1 ||
                                    kh.IS_DELETE == null) &&
                                    (l.IS_DELETE != 1 ||
                                    l.IS_DELETE == null) &&
                                    (sv.IS_DELETE != 1 ||
                                    sv.IS_DELETE == null) &&
                                    sv.ID_SINHVIEN == idsinhvien
                                  select new
                                  {
                                      kh.ID_HE_DAOTAO
                                  }).Distinct().First().ID_HE_DAOTAO;
                res = Convert.ToInt32(idhedaotao.ToString());
                return res;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public DataTable GetAllKhoaHoc(int pID_HEDAOTAO)
        {
            try
            {
                DataTable dt = null;
                if (pID_HEDAOTAO > 0)
                {
                    var kh = from khoc in db.tbl_KHOAHOCs
                             join hdt in db.tbl_HEDAOTAOs on khoc.ID_HE_DAOTAO equals hdt.ID_HE_DAOTAO
                             where (khoc.IS_DELETE != 1 || khoc.IS_DELETE == null) && (hdt.IS_DELETE != 1 || hdt.IS_DELETE == null) && khoc.ID_HE_DAOTAO == pID_HEDAOTAO
                             select new
                             {
                                 khoc.ID_KHOAHOC,
                                 khoc.MA_KHOAHOC,
                                 khoc.ID_HE_DAOTAO,
                                 khoc.KYHIEU,
                                 khoc.NAM_BD,
                                 khoc.NAM_KT,
                                 khoc.SO_HKY_1NAM,
                                 khoc.SO_HKY,
                                 khoc.TEN_KHOAHOC,
                                 khoc.TRANGTHAI,
                                 hdt.TEN_HE_DAOTAO

                             };
                    dt = TableUtil.LinqToDataTable(kh);
                }
                else
                {
                    var kh = from khoc in db.tbl_KHOAHOCs
                             join hdt in db.tbl_HEDAOTAOs on khoc.ID_HE_DAOTAO equals hdt.ID_HE_DAOTAO
                             where (khoc.IS_DELETE != 1 || khoc.IS_DELETE == null) && (hdt.IS_DELETE != 1 || hdt.IS_DELETE == null)
                             select new
                             {
                                 khoc.ID_KHOAHOC,
                                 khoc.MA_KHOAHOC,
                                 khoc.ID_HE_DAOTAO,
                                 khoc.KYHIEU,
                                 khoc.NAM_BD,
                                 khoc.NAM_KT,
                                 khoc.SO_HKY_1NAM,
                                 khoc.SO_HKY,
                                 khoc.TEN_KHOAHOC,
                                 khoc.TRANGTHAI,
                                 hdt.TEN_HE_DAOTAO

                             };
                    dt = TableUtil.LinqToDataTable(kh);
                }

                return dt;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public DataTable GetLopHPDK(int id_sinhvien)
        {
            try
            {
                DataTable dt = null;
                var hpdk = from dkhp in db.tbl_HP_DANGKies
                           join hp in db.tbl_LOP_HOCPHANs on dkhp.ID_LOPHOCPHAN equals hp.ID_LOPHOCPHAN
                           join hk in db.tbl_NAMHOC_HKY_HTAIs on hp.ID_NAMHOC_HKY_HTAI equals hk.ID_NAMHOC_HKY_HTAI
                           join nh in db.tbl_NAMHOC_HIENTAIs on hk.ID_NAMHOC_HIENTAI equals nh.ID_NAMHOC_HIENTAI
                           join mh in db.tbl_MONHOCs on new { ID_MONHOC = Convert.ToInt32(hp.ID_MONHOC) } equals
                               new { ID_MONHOC = mh.ID_MONHOC }
                           where
                               (dkhp.IS_DELETE != 1 || dkhp.IS_DELETE == null) &&
                               (hp.IS_DELETE != 1 || hp.IS_DELETE == null) &&
                               (mh.IS_DELETE != 1 || mh.IS_DELETE == null) &&
                                (hk.IS_DELETE != 1 || hk.IS_DELETE == null) &&
                                (nh.IS_DELETE != 1 || nh.IS_DELETE == null) &&
                                hk.IS_HIENTAI == 1 &&
                               dkhp.ID_SINHVIEN == id_sinhvien
                           select new
                           {
                               dkhp.ID_DANGKY,
                               dkhp.ID_SINHVIEN,
                               dkhp.ID_LOPHOCPHAN,
                               dkhp.ID_THAMSO,
                               mh.MA_MONHOC,
                               mh.TEN_MONHOC,
                               NGAY_DANGKY = dkhp.NGAY_DANGKY.ToString(),
                               dkhp.GIO_DANGKY,
                               mh.SO_TC,
                               dkhp.DON_GIA,
                               dkhp.THANH_TIEN
                           };
                dt = TableUtil.LinqToDataTable(hpdk);
                dt.Columns.Add("TRANGTHAI");
                foreach (DataRow r in dt.Rows)
                {
                    r["TRANGTHAI"] = "Đã lưu vào cơ sở dữ liệu";
                }
                return dt;
            }
            catch (Exception err)
            {
                throw err;
            }
        }
        #endregion

        #region Thuc hien cac chuc nang
        public bool Insert_HocPhanDK(DataTable iDataSoure, string pUser)
        {
            try
            {
                int count = 0;
                foreach (DataRow r in iDataSoure.Rows)
                {
                    if (r["ID_DANGKY"].ToString().Equals("0"))
                    {
                        tbl_HP_DANGKY dkhp = new tbl_HP_DANGKY();
                        dkhp.ID_THAMSO = 1;
                        dkhp.ID_SINHVIEN = Convert.ToInt32(r["ID_SINHVIEN"].ToString());
                        dkhp.ID_LOPHOCPHAN = Convert.ToInt32(r["ID_LOPHOCPHAN"].ToString());
                        dkhp.NGAY_DANGKY = Convert.ToDateTime(r["NGAY_DANGKY"]);
                        dkhp.GIO_DANGKY = DateTime.Now.ToString("HH:mm:ss");
                        dkhp.DON_GIA = Convert.ToDouble(r["DON_GIA"].ToString());
                        dkhp.THANH_TIEN = Convert.ToDouble(r["THANH_TIEN"].ToString());
                        dkhp.CREATE_USER = pUser;
                        dkhp.SO_TC = Convert.ToInt32(r["SO_TC"].ToString());
                        dkhp.CREATE_TIME = DateTime.Now;
                        dkhp.IS_DELETE = 0;

                        db.tbl_HP_DANGKies.InsertOnSubmit(dkhp);
                        db.SubmitChanges();
                        count++;
                    }

                }

                if (count > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public bool Insert_DangKyHuy(DataTable iDataSoure, string pUser)
        {
            try
            {
                int count = 0;
                if (iDataSoure.Rows.Count > 0)
                {
                    foreach (DataRow r in iDataSoure.Rows)
                    {
                        if (Convert.ToInt32(r["ID_DANGKY"].ToString()) > 0)
                        {
                            try
                            {
                                tbl_HP_DANGKY_HUY huydk = new tbl_HP_DANGKY_HUY();
                                huydk.ID_LOPHOCPHAN = Convert.ToInt32(r["ID_LOPHOCPHAN"].ToString());
                                huydk.ID_SINHVIEN = Convert.ToInt32(r["ID_SINHVIEN"].ToString());
                                huydk.ID_THAMSO = Convert.ToInt32(r["ID_THAMSO"].ToString());
                                huydk.NGAY_HUY = DateTime.Now;
                                huydk.GIO_HUY = DateTime.Now.ToString("HH:mm:ss");
                                huydk.DON_GIA = Convert.ToDouble(r["DON_GIA"].ToString());
                                huydk.THANH_TIEN = Convert.ToDouble(r["THANH_TIEN"].ToString());
                                huydk.IS_DELETE = 0;
                                huydk.CREATE_USER = pUser;
                                huydk.CREATE_TIME = DateTime.Now;

                                db.tbl_HP_DANGKY_HUYs.InsertOnSubmit(huydk);
                                db.SubmitChanges();
                                count++;
                            }
                            catch
                            {
                                return false;
                            }
                            bool res = Delete_HocPhanDK(Convert.ToInt32(r["ID_DANGKY"].ToString()), pUser);
                            if (!res)
                            {
                                return false;
                            }
                        }
                    }
                }
                if (count > 0)
                    return true;
                return false;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public bool Delete_HocPhanDK(int Id_DangKy, string pUser)
        {
            try
            {
                bool res;

                tbl_HP_DANGKY dkhp = db.tbl_HP_DANGKies.Single(t => t.ID_DANGKY == Id_DangKy);
                dkhp.IS_DELETE = 1;
                dkhp.UPDATE_TIME = DateTime.Now;
                dkhp.CREATE_USER = pUser;
                db.SubmitChanges();
                if (dkhp.ID_DANGKY != 0)
                {
                    res = true;
                }
                else
                {
                    res = false;
                }
                return res;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public DataTable GetHocKyNamHoc()
        {
            try
            {
                DataTable dt = null;
                var hockynamhoc = from hkht in db.tbl_NAMHOC_HKY_HTAIs
                                  join nhht in db.tbl_NAMHOC_HIENTAIs on
                                      new { ID_NAMHOC_HIENTAI = Convert.ToInt32(hkht.ID_NAMHOC_HIENTAI) } equals
                                      new { ID_NAMHOC_HIENTAI = nhht.ID_NAMHOC_HIENTAI }
                                  where
                                      nhht.IS_HIENTAI == 1
                                  select new
                                  {
                                      hkht.ID_NAMHOC_HKY_HTAI,
                                      TEN_HOKY_NH =
                                          ("Học kỳ " + "" + Convert.ToString(hkht.HOCKY) + " " + "Năm " +
                                           Convert.ToString(nhht.NAMHOC_TU) + " - " + Convert.ToString(nhht.NAMHOC_DEN))
                                  };
                dt = TableUtil.LinqToDataTable(hockynamhoc);
                return dt;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public DataTable GetNgay_SoTuan()
        {
            try
            {
                DataTable dt = null;
                var NgayTuan = from nhht in db.tbl_NAMHOC_HIENTAIs
                               where
                                   (nhht.IS_DELETE != 1 ||
                                    nhht.IS_DELETE == null) &&
                                   nhht.IS_HIENTAI == 1
                               select new
                               {
                                   nhht.NGAY_BATDAU,
                                   nhht.SO_TUAN
                               };
                dt = TableUtil.LinqToDataTable(NgayTuan);

                return dt;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public int GetHocKy(int id_hocky_htai)
        {
            try
            {
                var hocky = (from hk in db.tbl_NAMHOC_HKY_HTAIs
                             where hk.ID_NAMHOC_HKY_HTAI == id_hocky_htai
                             select new { hk.HOCKY }).First().HOCKY.ToString();
                return Convert.ToInt32(hocky.ToString());
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public DataTable GetTKB(int id_hocky_hientai, int id_sinhvien, int tuan)
        {
            try
            {
                DataTable dt = null;
                var tkb = from dkhp in db.tbl_HP_DANGKies
                          join hp in db.tbl_LOP_HOCPHANs on new { ID_LOPHOCPHAN = Convert.ToInt32(dkhp.ID_LOPHOCPHAN) } equals new { ID_LOPHOCPHAN = hp.ID_LOPHOCPHAN }
                          join hpct in db.tbl_LOP_HOCPHAN_CTs on new { ID_LOPHOCPHAN = hp.ID_LOPHOCPHAN } equals new { ID_LOPHOCPHAN = Convert.ToInt32(hpct.ID_LOPHOCPHAN) }
                          join mh in db.tbl_MONHOCs on new { ID_MONHOC = Convert.ToInt32(hp.ID_MONHOC) } equals new { ID_MONHOC = mh.ID_MONHOC }
                          join p in db.tbl_PHONGHOCs on new { ID_PHONG = Convert.ToInt32(hpct.ID_PHONG) } equals new { ID_PHONG = p.ID_PHONG }
                          join gv in db.tbl_GIANGVIENs on new { ID_GIANGVIEN = Convert.ToInt32(hp.ID_GIANGVIEN) } equals new { ID_GIANGVIEN = gv.ID_GIANGVIEN }
                          where
                            (dkhp.IS_DELETE != 1 ||
                            dkhp.IS_DELETE == null) &&
                            (hp.IS_DELETE != 1 ||
                            hp.IS_DELETE == null) &&
                            (hpct.IS_DELETE != 1 ||
                            hpct.IS_DELETE == null) &&
                            (mh.IS_DELETE != 1 ||
                            mh.IS_DELETE == null) &&
                            (p.IS_DELETE != 1 ||
                            p.IS_DELETE == null) &&
                            (gv.IS_DELETE != 1 ||
                            p.IS_DELETE == null) &&
                            dkhp.ID_SINHVIEN == id_sinhvien &&
                            hp.ID_NAMHOC_HKY_HTAI == id_hocky_hientai &&
                            tuan >= hp.TUAN_BD && tuan <= hp.TUAN_KT
                          select new
                          {
                              hp.TEN_LOP_HOCPHAN,
                              hp.ID_LOPHOCPHAN,
                              SOTIET = hpct.SO_TIET,
                              hp.MA_LOP_HOCPHAN,
                              hp.TUAN_BD,
                              hp.TUAN_KT,
                              hpct.THU,
                              hpct.TIET_BD,
                              hpct.TIET_KT,
                              p.TEN_PHONG,
                              gv.TEN_GIANGVIEN,
                              mh.TEN_MONHOC
                          };
                dt = TableUtil.LinqToDataTable(tkb);
                return dt;
            }
            catch (Exception err)
            {
                throw err;
            }
        } 
        #endregion

        #region Danh cho thuat toan
        public DataTable Get_TTSV(string masinhvien)
        {
            try
            {
                DataTable dtRes = null;
                var query = from SV in db.tbL_SINHVIENs
                            join LH in db.tbl_LOPHOCs on new { ID_LOPHOC = Convert.ToInt32(SV.ID_LOPHOC) } equals new { ID_LOPHOC = LH.ID_LOPHOC } into LH_join
                            from LH in LH_join.DefaultIfEmpty()
                            join N in db.tbl_KHOAHOC_NGANHs on new { ID_KHOAHOC_NGANH = Convert.ToInt32(LH.ID_KHOAHOC_NGANH) } equals new { ID_KHOAHOC_NGANH = N.ID_KHOAHOC_NGANH } into N_join
                            from N in N_join.DefaultIfEmpty()
                            join KH in db.tbl_KHOAHOCs on new { ID_KHOAHOC = Convert.ToInt32(N.ID_KHOAHOC) } equals new { ID_KHOAHOC = KH.ID_KHOAHOC } into KH_join
                            from KH in KH_join.DefaultIfEmpty()
                            where
                              SV.MA_SINHVIEN == masinhvien && (SV.IS_DELETE != 1 || SV.IS_DELETE == null)
                            select new
                            {
                                SV.ID_SINHVIEN,
                                ID_KHOAHOC_NGANH = (int?)LH.ID_KHOAHOC_NGANH,
                                NAM_BD = (int?)KH.NAM_BD,
                                ID_HE_DAOTAO = (int?)KH.ID_HE_DAOTAO,
                                ID_NGANH = (int?)N.ID_NGANH,
                                ID_KHOAHOC = N.ID_KHOAHOC
                            };
                dtRes = TableUtil.LinqToDataTable(query);

                #region Lay hoc ky hien tai (1 -> 8) theo khoa hoc
                var query1 = from Tbl_KHOAHOC in db.tbl_KHOAHOCs
                             where
                                 Tbl_KHOAHOC.ID_KHOAHOC == Convert.ToInt32(dtRes.Rows[0]["ID_KHOAHOC"])
                             select new
                             {
                                 Tbl_KHOAHOC.NAM_BD
                             };
                DataTable xdt = null;
                xdt = TableUtil.LinqToDataTable(query1);
                DataTable iGridData_TTHK = null;
                iGridData_TTHK = GetAll_Tso_Hocky();
                int hkht = 0;
                if (xdt != null && xdt.Rows.Count > 0)
                {
                    int hk = Convert.ToInt32(iGridData_TTHK.Rows[0]["HOCKY"]);
                    if (hk == 3)
                    {
                        hk = 2;
                    }
                    hkht = (Convert.ToInt32(iGridData_TTHK.Rows[0]["NAMHOC_TU"]) -
                                Convert.ToInt32(xdt.Rows[0]["NAM_BD"])) * 2 + hk;
                    dtRes.Columns.Add("HOCKY", typeof(int));
                    dtRes.Rows[0]["HOCKY"] = hkht;
                }
                #endregion

                return dtRes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetAll_Tso_Hocky()
        {
            var query = from d in db.tbl_NAMHOC_HIENTAIs
                        where
                            (d.IS_DELETE != 1 ||
                             d.IS_DELETE == null) &&
                            d.IS_HIENTAI == 1
                        select new
                        {
                            d.ID_NAMHOC_HIENTAI,
                            d.NAMHOC_TU,
                            d.NAMHOC_DEN,
                            d.NGAY_BATDAU,
                            d.SO_TUAN,
                            d.SO_HKY_TRONGNAM,
                            d.IS_HIENTAI,
                            ID_NAMHOC_HKY_HTAI =
                                ((from m in db.tbl_NAMHOC_HKY_HTAIs
                                  where
                                      (m.IS_DELETE != 1 ||
                                       m.IS_DELETE == null) &&
                                      Convert.ToInt64(m.IS_HIENTAI) == 1 &&
                                      m.ID_NAMHOC_HIENTAI == d.ID_NAMHOC_HIENTAI
                                  select new
                                  {
                                      m.ID_NAMHOC_HKY_HTAI
                                  }).First().ID_NAMHOC_HKY_HTAI),
                            HOCKY =
                                ((from m in db.tbl_NAMHOC_HKY_HTAIs
                                  where
                                      (m.IS_DELETE != 1 ||
                                       m.IS_DELETE == null) &&
                                      Convert.ToInt64(m.IS_HIENTAI) == 1 &&
                                      m.ID_NAMHOC_HIENTAI == d.ID_NAMHOC_HIENTAI
                                  select new
                                  {
                                      m.HOCKY
                                  }).First().HOCKY)
                        };
            DataTable xdt = null;
            xdt = TableUtil.LinqToDataTable(query);
            return xdt;
        }

        public DataTable GetAll_SVKhoaTruoc(DataTable iDataSource)
        {
            try
            {
                DataTable dtRes = null;
                var query = from sv in db.tbL_SINHVIENs
                            join lh in db.tbl_LOPHOCs on new { ID_LOPHOC = Convert.ToInt32(sv.ID_LOPHOC) } equals new { ID_LOPHOC = lh.ID_LOPHOC } into lh_join
                            from lh in lh_join.DefaultIfEmpty()
                            where
                                (from n in db.tbl_KHOAHOC_NGANHs
                                 join kh in db.tbl_KHOAHOCs on new { ID_KHOAHOC = Convert.ToInt32(n.ID_KHOAHOC) } equals new { ID_KHOAHOC = kh.ID_KHOAHOC } into kh_join
                                 from kh in kh_join.DefaultIfEmpty()
                                 where
                                   kh.ID_HE_DAOTAO == Convert.ToInt32(iDataSource.Rows[0]["ID_HE_DAOTAO"]) &&
                                   kh.NAM_BD < Convert.ToInt32(iDataSource.Rows[0]["NAM_BD"]) &&
                                   n.ID_NGANH == Convert.ToInt32(iDataSource.Rows[0]["ID_NGANH"]) &&
                                   (n.IS_DELETE != 1 ||
                                   n.IS_DELETE == null) &&
                                   (kh.IS_DELETE != 1 ||
                                   kh.IS_DELETE == null)
                                 select new
                                 {
                                     n.ID_KHOAHOC_NGANH
                                 }).Contains(new { ID_KHOAHOC_NGANH = (System.Int32)lh.ID_KHOAHOC_NGANH })
                            select new
                            {
                                sv.ID_SINHVIEN
                            };
                dtRes = TableUtil.LinqToDataTable(query);
                return dtRes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetAll_MH_Mau(int pID_KHOAHOC_NGANH, int pHOCKY)
        {
            try
            {
                DataTable dtRes = null;
                var query = from Tbl_KHOAHOC_NGANH_CTIET in db.tbl_KHOAHOC_NGANH_CTIETs
                            where
                              Tbl_KHOAHOC_NGANH_CTIET.ID_KHOAHOC_NGANH == pID_KHOAHOC_NGANH &&
                              Convert.ToInt64(Tbl_KHOAHOC_NGANH_CTIET.HOCKY) == pHOCKY - 1
                            select new
                            {
                                Tbl_KHOAHOC_NGANH_CTIET.ID_MONHOC
                            };
                dtRes = TableUtil.LinqToDataTable(query);
                return dtRes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetDiemMau(string Masinhvien, int IDMonTuVan)
        {
            try
            {
                DataTable dt_result = new DataTable(); // Bang diem mau
                DataTable dtRes_sv = null; // Danh sach sinh vien nhung khoa truoc
                DataTable dtRes_mh = null; // Danh sach mon hoc cua hoc ky truoc
                DataTable dt_TTSV = null;  // Thong tin sinh vien
                dt_TTSV = Get_TTSV(Masinhvien);
                dtRes_sv = GetAll_SVKhoaTruoc(dt_TTSV);
                dtRes_mh = GetAll_MH_Mau(Convert.ToInt32(dt_TTSV.Rows[0]["ID_KHOAHOC_NGANH"]),
                    Convert.ToInt32(dt_TTSV.Rows[0]["HOCKY"]));

                #region Them cot diem mon dang xet

                DataRow idr = dtRes_mh.NewRow();
                idr["ID_MONHOC"] = IDMonTuVan;
                dtRes_mh.Rows.Add(idr);

                #endregion

                foreach (DataRow dr in dtRes_mh.Rows)
                {
                    dt_result.Columns.Add(dr[0].ToString(), typeof(double));
                }


                foreach (DataRow dr in dtRes_sv.Rows)
                {
                    #region Ket qua nhung mon hoc mau

                    var get_kq = from diem in db.tbl_DIEM_SINHVIENs
                                 join hp in db.tbl_LOP_HOCPHANs on new { ID_LOPHOCPHAN = Convert.ToInt32(diem.ID_LOPHOCPHAN) }
                                     equals
                                     new { ID_LOPHOCPHAN = hp.ID_LOPHOCPHAN } into hp_join
                                 from hp in hp_join.DefaultIfEmpty()
                                 join ct in db.tbl_KHOAHOC_NGANH_CTIETs on
                                     new { ID_KHOAHOC_NGANH_CTIET = Convert.ToInt32(hp.ID_KHOAHOC_NGANH_CTIET) } equals
                                     new { ID_KHOAHOC_NGANH_CTIET = ct.ID_KHOAHOC_NGANH_CTIET } into ct_join
                                 from ct in ct_join.DefaultIfEmpty()
                                 where
                                     diem.ID_SINHVIEN == Convert.ToInt32(dr["ID_SINHVIEN"]) &&
                                 #region Ds mon hoc cua ky truoc
 (
                                        (from Tbl_KHOAHOC_NGANH_CTIET in db.tbl_KHOAHOC_NGANH_CTIETs
                                         where
                                             Tbl_KHOAHOC_NGANH_CTIET.ID_KHOAHOC_NGANH ==
                                             Convert.ToInt32(dt_TTSV.Rows[0]["ID_KHOAHOC_NGANH"]) &&
                                             Tbl_KHOAHOC_NGANH_CTIET.HOCKY == (Convert.ToInt32(dt_TTSV.Rows[0]["HOCKY"]) - 1) &&
                                             (Tbl_KHOAHOC_NGANH_CTIET.IS_DELETE != 1 || Tbl_KHOAHOC_NGANH_CTIET.IS_DELETE == null)
                                         select new
                                         {
                                             Tbl_KHOAHOC_NGANH_CTIET.ID_MONHOC
                                         }).Distinct().Contains(new { ID_MONHOC = hp.ID_MONHOC }))
                                 #endregion
 && ct.ID_KHOAHOC_NGANH ==

                                 #region Khoa_nganh cua khoa truoc

 ((from kn in db.tbl_KHOAHOC_NGANHs
   join kh in db.tbl_KHOAHOCs on new { ID_KHOAHOC = Convert.ToInt32(kn.ID_KHOAHOC) } equals
       new { ID_KHOAHOC = kh.ID_KHOAHOC } into kh_join
   from kh in kh_join.DefaultIfEmpty()
   where
       kn.ID_NGANH == Convert.ToInt32(dt_TTSV.Rows[0]["ID_NGANH"]) &&
       kh.NAM_BD == (Convert.ToInt32(dt_TTSV.Rows[0]["NAM_BD"]) - 1)
   select new
   {
       kn.ID_KHOAHOC_NGANH
   }).First().ID_KHOAHOC_NGANH)

                                 #endregion

 && ct.HOCKY == (Convert.ToInt32(dt_TTSV.Rows[0]["HOCKY"]) - 1) &&
                                     (hp.IS_DELETE != 1 ||
                                      hp.IS_DELETE == null)
                                 select new
                                 {
                                     hp.ID_MONHOC,
                                     diem.DIEM_TONG
                                 };
                    DataTable ximport = TableUtil.LinqToDataTable(get_kq);
                    #endregion

                    #region Ket qua mon hoc can tu van

                    var get_kq_tuvan = from diem in db.tbl_DIEM_SINHVIENs
                                       join hp in db.tbl_LOP_HOCPHANs on new { ID_LOPHOCPHAN = Convert.ToInt32(diem.ID_LOPHOCPHAN) }
                                           equals
                                           new { ID_LOPHOCPHAN = hp.ID_LOPHOCPHAN } into hp_join
                                       from hp in hp_join.DefaultIfEmpty()
                                       join ct in db.tbl_KHOAHOC_NGANH_CTIETs on
                                           new { ID_KHOAHOC_NGANH_CTIET = Convert.ToInt32(hp.ID_KHOAHOC_NGANH_CTIET) } equals
                                           new { ID_KHOAHOC_NGANH_CTIET = ct.ID_KHOAHOC_NGANH_CTIET } into ct_join
                                       from ct in ct_join.DefaultIfEmpty()
                                       where
                                           diem.ID_SINHVIEN == Convert.ToInt32(dr["ID_SINHVIEN"]) && hp.ID_MONHOC == IDMonTuVan

                                           && ct.ID_KHOAHOC_NGANH ==

                                       #region Khoa_nganh cua khoa truoc

 ((from kn in db.tbl_KHOAHOC_NGANHs
   join kh in db.tbl_KHOAHOCs on new { ID_KHOAHOC = Convert.ToInt32(kn.ID_KHOAHOC) } equals
       new { ID_KHOAHOC = kh.ID_KHOAHOC } into kh_join
   from kh in kh_join.DefaultIfEmpty()
   where
       kn.ID_NGANH == Convert.ToInt32(dt_TTSV.Rows[0]["ID_NGANH"]) &&
       kh.NAM_BD == (Convert.ToInt32(dt_TTSV.Rows[0]["NAM_BD"]) - 1)
   select new
   {
       kn.ID_KHOAHOC_NGANH
   }).First().ID_KHOAHOC_NGANH)

                                       #endregion

 && ct.HOCKY == Convert.ToInt32(dt_TTSV.Rows[0]["HOCKY"]) &&
                                           (hp.IS_DELETE != 1 ||
                                            hp.IS_DELETE == null)
                                       select new
                                       {
                                           hp.ID_MONHOC,
                                           diem.DIEM_TONG
                                       };
                    DataTable ximport_tuvan = TableUtil.LinqToDataTable(get_kq_tuvan);
                    if (ximport_tuvan != null && ximport_tuvan.Rows.Count > 0)
                    {
                        DataRow ndr = ximport.NewRow();
                        ndr["ID_MONHOC"] = ximport_tuvan.Rows[0]["ID_MONHOC"];
                        ndr["DIEM_TONG"] = ximport_tuvan.Rows[0]["DIEM_TONG"];
                        ximport.Rows.Add(ndr);
                    }
                    #endregion

                    DataRow dtrow = dt_result.NewRow();

                    #region Chuyen ket qua diem cua 1 sinh vien thanh 1 dong trong bang du lieu mau

                    foreach (DataRow xdr in ximport.Rows)
                    {
                        for (int i = 0; i < dt_result.Columns.Count; i++)
                        {
                            if (xdr["ID_MONHOC"].ToString().Trim().Equals(dt_result.Columns[i].ColumnName))
                            {
                                dtrow[dt_result.Columns[i].ColumnName] = Convert.ToDouble(xdr["DIEM_TONG"]);
                            }
                        }
                    }

                    #endregion

                    #region Neu khong có diem => 0

                    for (int n = 0; n < dt_result.Columns.Count; n++)
                    {
                        if (string.IsNullOrEmpty(dtrow[n].ToString()))
                        {
                            dtrow[n] = 0;
                        }
                    }

                    #endregion

                    #region Nếu điểm môn cần tư vấn có tồn tại => import vào bảng dữ liệu mẫu

                    if (Convert.ToInt32(dtrow[IDMonTuVan.ToString()]) != 0)
                    {
                        dt_result.Rows.Add(dtrow);
                    }

                    #endregion
                }
                return dt_result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetDiem_SV(string masinhvien, int IDMonTuVan)
        {
            try
            {
                DataTable dt_result = new DataTable(); // Bang diem mau
                DataTable dtRes_sv = null; // Danh sach sinh vien nhung khoa truoc
                DataTable dtRes_mh = null; // Danh sach mon hoc cua hoc ky truoc
                DataTable dt_TTSV = null;  // Thong tin sinh vien
                dt_TTSV = Get_TTSV(masinhvien);
                dtRes_sv = GetAll_SVKhoaTruoc(dt_TTSV);
                dtRes_mh = GetAll_MH_Mau(Convert.ToInt32(dt_TTSV.Rows[0]["ID_KHOAHOC_NGANH"]),
                    Convert.ToInt32(dt_TTSV.Rows[0]["HOCKY"]));
                foreach (DataRow dr in dtRes_mh.Rows)
                {
                    dt_result.Columns.Add(dr[0].ToString(), typeof(double));
                }
                dt_result.Columns.Add(IDMonTuVan.ToString(), typeof(double));

                var get_kq = from diem in db.tbl_DIEM_SINHVIENs
                             join hp in db.tbl_LOP_HOCPHANs on new { ID_LOPHOCPHAN = Convert.ToInt32(diem.ID_LOPHOCPHAN) }
                                 equals
                                 new { ID_LOPHOCPHAN = hp.ID_LOPHOCPHAN } into hp_join
                             from hp in hp_join.DefaultIfEmpty()
                             join ct in db.tbl_KHOAHOC_NGANH_CTIETs on
                                 new { ID_KHOAHOC_NGANH_CTIET = Convert.ToInt32(hp.ID_KHOAHOC_NGANH_CTIET) } equals
                                 new { ID_KHOAHOC_NGANH_CTIET = ct.ID_KHOAHOC_NGANH_CTIET } into ct_join
                             from ct in ct_join.DefaultIfEmpty()
                             where
                                 diem.ID_SINHVIEN == Convert.ToInt32(dt_TTSV.Rows[0]["ID_SINHVIEN"]) &&
                                 (

                             #region Ds mon hoc cua ky truoc

from Tbl_KHOAHOC_NGANH_CTIET in db.tbl_KHOAHOC_NGANH_CTIETs
where
    Tbl_KHOAHOC_NGANH_CTIET.ID_KHOAHOC_NGANH ==
    Convert.ToInt32(dt_TTSV.Rows[0]["ID_KHOAHOC_NGANH"]) &&
    Tbl_KHOAHOC_NGANH_CTIET.HOCKY == (Convert.ToInt32(dt_TTSV.Rows[0]["HOCKY"]) - 1)
select new
{
    Tbl_KHOAHOC_NGANH_CTIET.ID_MONHOC
}

                             #endregion

).Contains(new { ID_MONHOC = hp.ID_MONHOC })
                            && ct.ID_KHOAHOC_NGANH == Convert.ToInt32(dt_TTSV.Rows[0]["ID_KHOAHOC_NGANH"])
                            && ct.HOCKY == (Convert.ToInt32(dt_TTSV.Rows[0]["HOCKY"]) - 1) &&
                                 (hp.IS_DELETE != 1 ||
                                  hp.IS_DELETE == null)
                             select new
                             {
                                 hp.ID_MONHOC,
                                 diem.DIEM_TONG
                             };
                DataTable ximport = TableUtil.LinqToDataTable(get_kq);

                DataRow dtrow = dt_result.NewRow();

                #region Chuyen ket qua diem cua 1 sinh vien thanh 1 dong trong bang du lieu mau

                foreach (DataRow xdr in ximport.Rows)
                {
                    for (int i = 0; i < dt_result.Columns.Count; i++)
                    {
                        if (xdr["ID_MONHOC"].ToString().Trim().Equals(dt_result.Columns[i].ColumnName))
                        {
                            dtrow[dt_result.Columns[i].ColumnName] = Convert.ToDouble(xdr["DIEM_TONG"]);
                        }
                    }
                }

                #endregion

                dt_result.Rows.Add(dtrow);
                return dt_result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        } 
        #endregion

        public double GetHocPhi_LT(int id_hedaotao, int LT_Or_TH)
        {
            double dongia = 0;
            if (LT_Or_TH == 1)// hoc phi lt
            {
                var dongiatc = (from gia in db.tbl_HP_CAUHINH_HOCPHIs
                                join hknh in db.tbl_NAMHOC_HKY_HTAIs on gia.ID_NAMHOC_HKY_HTAI equals hknh.ID_NAMHOC_HKY_HTAI
                                where (gia.IS_DELETE != 1 || gia.IS_DELETE == null) && (hknh.IS_DELETE != 1 || hknh.IS_DELETE == null)
                                && hknh.IS_HIENTAI == 1
                                && gia.IS_LYTHUYET == 1
                                && gia.ID_HE_DAOTAO ==id_hedaotao
                                select new
                                {
                                    gia.DON_GIA
                                }).First().DON_GIA;

                dongia = Convert.ToDouble(dongiatc);
            }
            if (LT_Or_TH == 0)// hoc phi lt
            {
                var dongiatc = (from gia in db.tbl_HP_CAUHINH_HOCPHIs
                                join hknh in db.tbl_NAMHOC_HKY_HTAIs on gia.ID_NAMHOC_HKY_HTAI equals hknh.ID_NAMHOC_HKY_HTAI
                                where (gia.IS_DELETE != 1 || gia.IS_DELETE == null) && (hknh.IS_DELETE != 1 || hknh.IS_DELETE == null)
                                && hknh.IS_HIENTAI == 1
                                && gia.IS_LYTHUYET == 0
                                && gia.ID_HE_DAOTAO == id_hedaotao
                                select new
                                {
                                    gia.DON_GIA
                                }).First().DON_GIA;

                dongia = Convert.ToDouble(dongiatc);
            }
            return dongia;
        }

        public DataTable GetThamSoDangDotDangKy(int id_hedaotao)
        {
            try
            {
                DataTable dt = null;
                var dotdangkyhocphan = from dotDKHP in db.tbl_HP_DOTDKs
                                       join hkht in db.tbl_NAMHOC_HKY_HTAIs on
                                           new { ID_NAMHOC_HKY_HTAI = Convert.ToInt32(dotDKHP.ID_NAMHOC_HKY_HTAI) } equals
                                           new { ID_NAMHOC_HKY_HTAI = hkht.ID_NAMHOC_HKY_HTAI }
                                       join nhht in db.tbl_NAMHOC_HIENTAIs on
                                           new { ID_NAMHOC_HIENTAI = Convert.ToInt32(hkht.ID_NAMHOC_HIENTAI) } equals
                                           new { ID_NAMHOC_HIENTAI = nhht.ID_NAMHOC_HIENTAI }
                                       where
                                           (dotDKHP.ISDELETE != 1 ||
                                            dotDKHP.ISDELETE == null) &&
                                           (hkht.IS_DELETE != 1 ||
                                            hkht.IS_DELETE == null) &&
                                           hkht.IS_HIENTAI == 1 &&
                                           (nhht.IS_DELETE != 1 ||
                                            nhht.IS_DELETE == null) &&
                                           nhht.IS_HIENTAI == 1 &&
                                           Convert.ToDateTime(DateTime.Now) >= Convert.ToDateTime(dotDKHP.NGAY_BDAU) &&
                                           Convert.ToDateTime(DateTime.Now) <= Convert.ToDateTime(dotDKHP.NGAY_KTHUC) &&
                                           dotDKHP.ID_HE_DAOTAO == id_hedaotao
                                       select new
                                       {
                                           dotDKHP.ID_DOTDK,
                                           dotDKHP.ID_NAMHOC_HKY_HTAI,
                                           dotDKHP.ID_HE_DAOTAO,
                                           dotDKHP.MA_DOT_DK,
                                           dotDKHP.TEN_DOT_DK,
                                           dotDKHP.NGAY_BDAU,
                                           dotDKHP.NGAY_KTHUC
                                       };
                dt = TableUtil.LinqToDataTable(dotdangkyhocphan);
                return dt;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        // các combobox + gridview
        public DataTable GetDanhSachLopHocPhan(int idkhoahoc, int idnganh)
        {
            DataTable dt = null;
            if (idkhoahoc == 0 && idnganh == 0)
            {
                dt = GetDanhSachLopHocPhan_3();
            }
            if (idkhoahoc == 0 && idnganh != 1)
            {
                dt = GetDanhSachLopHocPhan_2(idnganh);
            }
            if (idkhoahoc != 0 && idnganh == 0)
            {
                dt = GetDanhSachLopHocPhan_1(idkhoahoc);
            }
            if (idkhoahoc != 0 && idnganh != 0)
            {
                dt = GetDanhSachLopHocPhan_4(idkhoahoc, idnganh);
            }
            return dt;
        }

        DataTable GetDanhSachLopHocPhan_1(int idkhoahoc)
        {
            try
            {
                DataTable dt = null;
                var danhsachlophocphan = from hp in db.tbl_LOP_HOCPHANs
                                         join mh in db.tbl_MONHOCs on new { ID_MONHOC = Convert.ToInt32(hp.ID_MONHOC) } equals
                                             new { ID_MONHOC = mh.ID_MONHOC }
                                         join knct in db.tbl_KHOAHOC_NGANH_CTIETs on
                                             new { ID_KHOAHOC_NGANH_CTIET = Convert.ToInt32(hp.ID_KHOAHOC_NGANH_CTIET) } equals
                                             new { ID_KHOAHOC_NGANH_CTIET = knct.ID_KHOAHOC_NGANH_CTIET }
                                         join hkht in db.tbl_NAMHOC_HKY_HTAIs on
                                             new { ID_NAMHOC_HKY_HTAI = Convert.ToInt32(hp.ID_NAMHOC_HKY_HTAI) } equals
                                             new { ID_NAMHOC_HKY_HTAI = hkht.ID_NAMHOC_HKY_HTAI }
                                         join nhht in db.tbl_NAMHOC_HIENTAIs on
                                             new { ID_NAMHOC_HIENTAI = Convert.ToInt32(hkht.ID_NAMHOC_HIENTAI) } equals
                                             new { ID_NAMHOC_HIENTAI = nhht.ID_NAMHOC_HIENTAI }
                                         join kn in db.tbl_KHOAHOC_NGANHs on knct.ID_KHOAHOC_NGANH equals kn.ID_KHOAHOC_NGANH
                                         //join l in db.tbl_LOPHOCs on hp.ID_LOPHOC  equals l.ID_LOPHOC
                                         where
                                             kn.ID_KHOAHOC == idkhoahoc &&
                                             (hp.IS_DELETE != 1 || hp.IS_DELETE == null) &&
                                             (mh.IS_DELETE != 1 || mh.IS_DELETE == null) &&
                                             (knct.IS_DELETE != 1 || knct.IS_DELETE == null) &&
                                             (hkht.IS_DELETE != 1 || hkht.IS_DELETE == null) &&
                                             (nhht.IS_DELETE != 1 || nhht.IS_DELETE == null) &&
                                             hkht.IS_HIENTAI == 1 && nhht.IS_HIENTAI == 1 &&
                                             (kn.IS_DELETE != 1 || kn.IS_DELETE == null) &&
                                             //(l.IS_DELETE != 1 || l.IS_DELETE == null) &&
                                             (from hpct in db.tbl_LOP_HOCPHAN_CTs
                                              where hpct.IS_DELETE != 1 || hpct.IS_DELETE == null
                                              select new
                                              {
                                                  hpct.ID_LOPHOCPHAN
                                              }).Contains(new { ID_LOPHOCPHAN = (System.Int32?)hp.ID_LOPHOCPHAN })
                                         select new
                                         {
                                             hp.ID_LOPHOCPHAN,
                                             ID_KHOAHOC_NGANH_CTIET = (int?)hp.ID_KHOAHOC_NGANH_CTIET,
                                             ID_NAMHOC_HKY_HTAI = (int?)hp.ID_NAMHOC_HKY_HTAI,
                                             hp.ID_HEDAOTAO,
                                             ID_MONHOC = (int?)hp.ID_MONHOC,
                                             mh.ISBATBUOC,
                                             hp.ID_LOPHOC,
                                             //l.TEN_LOP,
                                             hp.ID_GIANGVIEN,
                                             hp.MA_LOP_HOCPHAN,
                                             hp.TEN_LOP_HOCPHAN,
                                             mh.MA_MONHOC,
                                             mh.TEN_MONHOC,
                                             mh.SO_TC,
                                             mh.IS_LYTHUYET,
                                             hp.SOLUONG,
                                             hp.TUAN_BD,
                                             hp.TUAN_KT,
                                             SOSVDKY = (int?)db.SoSVDaDangKy(hp.ID_LOPHOCPHAN),
                                             TEN_GIANGVIEN = (from gv in db.tbl_GIANGVIENs where gv.ID_GIANGVIEN == hp.ID_GIANGVIEN && (gv.IS_DELETE != 1 || gv.IS_DELETE == null) select new { gv.TEN_GIANGVIEN }).FirstOrDefault().TEN_GIANGVIEN.ToString()
                                         };
                dt = TableUtil.LinqToDataTable(danhsachlophocphan);
                dt.Columns.Add("THOIKHOABIEU");
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow r in dt.Rows)
                    {
                        int idlophocphan = Convert.ToInt32(r["ID_LOPHOCPHAN"].ToString());
                        r["THOIKHOABIEU"] = GetChiTietLopHocPhan(idlophocphan);
                    }
                }
                return dt;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        DataTable GetDanhSachLopHocPhan_2(int idnganh)
        {
            try
            {
                DataTable dt = null;
                var danhsachlophocphan = from hp in db.tbl_LOP_HOCPHANs
                                         join mh in db.tbl_MONHOCs on new { ID_MONHOC = Convert.ToInt32(hp.ID_MONHOC) } equals
                                             new { ID_MONHOC = mh.ID_MONHOC }
                                         join knct in db.tbl_KHOAHOC_NGANH_CTIETs on
                                             new { ID_KHOAHOC_NGANH_CTIET = Convert.ToInt32(hp.ID_KHOAHOC_NGANH_CTIET) } equals
                                             new { ID_KHOAHOC_NGANH_CTIET = knct.ID_KHOAHOC_NGANH_CTIET }
                                         join hkht in db.tbl_NAMHOC_HKY_HTAIs on
                                             new { ID_NAMHOC_HKY_HTAI = Convert.ToInt32(hp.ID_NAMHOC_HKY_HTAI) } equals
                                             new { ID_NAMHOC_HKY_HTAI = hkht.ID_NAMHOC_HKY_HTAI }
                                         join nhht in db.tbl_NAMHOC_HIENTAIs on
                                             new { ID_NAMHOC_HIENTAI = Convert.ToInt32(hkht.ID_NAMHOC_HIENTAI) } equals
                                             new { ID_NAMHOC_HIENTAI = nhht.ID_NAMHOC_HIENTAI }
                                         join kn in db.tbl_KHOAHOC_NGANHs on knct.ID_KHOAHOC_NGANH equals kn.ID_KHOAHOC_NGANH
                                         //join l in db.tbl_LOPHOCs on hp.ID_LOPHOC  equals l.ID_LOPHOC
                                         where
                                             kn.ID_NGANH == idnganh &&
                                             (hp.IS_DELETE != 1 || hp.IS_DELETE == null) &&
                                             (mh.IS_DELETE != 1 || mh.IS_DELETE == null) &&
                                             (knct.IS_DELETE != 1 || knct.IS_DELETE == null) &&
                                             (hkht.IS_DELETE != 1 || hkht.IS_DELETE == null) &&
                                             (nhht.IS_DELETE != 1 || nhht.IS_DELETE == null) &&
                                             hkht.IS_HIENTAI == 1 && nhht.IS_HIENTAI == 1 &&
                                             (kn.IS_DELETE != 1 || kn.IS_DELETE == null) &&
                                             //(l.IS_DELETE != 1 || l.IS_DELETE == null) &&
                                             (from hpct in db.tbl_LOP_HOCPHAN_CTs
                                              where hpct.IS_DELETE != 1 || hpct.IS_DELETE == null
                                              select new
                                              {
                                                  hpct.ID_LOPHOCPHAN
                                              }).Contains(new { ID_LOPHOCPHAN = (System.Int32?)hp.ID_LOPHOCPHAN })
                                         select new
                                         {
                                             hp.ID_LOPHOCPHAN,
                                             ID_KHOAHOC_NGANH_CTIET = (int?)hp.ID_KHOAHOC_NGANH_CTIET,
                                             ID_NAMHOC_HKY_HTAI = (int?)hp.ID_NAMHOC_HKY_HTAI,
                                             hp.ID_HEDAOTAO,
                                             ID_MONHOC = (int?)hp.ID_MONHOC,
                                             mh.ISBATBUOC,
                                             hp.ID_LOPHOC,
                                             //l.TEN_LOP,
                                             hp.ID_GIANGVIEN,
                                             hp.MA_LOP_HOCPHAN,
                                             hp.TEN_LOP_HOCPHAN,
                                             mh.MA_MONHOC,
                                             mh.TEN_MONHOC,
                                             mh.SO_TC,
                                             mh.IS_LYTHUYET,
                                             hp.SOLUONG,
                                             hp.TUAN_BD,
                                             hp.TUAN_KT,
                                             SOSVDKY = (int?)db.SoSVDaDangKy(hp.ID_LOPHOCPHAN),
                                             TEN_GIANGVIEN = (from gv in db.tbl_GIANGVIENs where gv.ID_GIANGVIEN == hp.ID_GIANGVIEN && (gv.IS_DELETE != 1 || gv.IS_DELETE == null) select new { gv.TEN_GIANGVIEN }).FirstOrDefault().TEN_GIANGVIEN.ToString()
                                         };
                dt = TableUtil.LinqToDataTable(danhsachlophocphan);
                dt.Columns.Add("THOIKHOABIEU");
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow r in dt.Rows)
                    {
                        int idlophocphan = Convert.ToInt32(r["ID_LOPHOCPHAN"].ToString());
                        r["THOIKHOABIEU"] = GetChiTietLopHocPhan(idlophocphan);
                    }
                }
                return dt;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        DataTable GetDanhSachLopHocPhan_3()
        {
            try
            {
                DataTable dt = null;
                var danhsachlophocphan = from hp in db.tbl_LOP_HOCPHANs
                                         join mh in db.tbl_MONHOCs on new { ID_MONHOC = Convert.ToInt32(hp.ID_MONHOC) } equals
                                             new { ID_MONHOC = mh.ID_MONHOC }
                                         join knct in db.tbl_KHOAHOC_NGANH_CTIETs on
                                             new { ID_KHOAHOC_NGANH_CTIET = Convert.ToInt32(hp.ID_KHOAHOC_NGANH_CTIET) } equals
                                             new { ID_KHOAHOC_NGANH_CTIET = knct.ID_KHOAHOC_NGANH_CTIET }
                                         join hkht in db.tbl_NAMHOC_HKY_HTAIs on
                                             new { ID_NAMHOC_HKY_HTAI = Convert.ToInt32(hp.ID_NAMHOC_HKY_HTAI) } equals
                                             new { ID_NAMHOC_HKY_HTAI = hkht.ID_NAMHOC_HKY_HTAI }
                                         join nhht in db.tbl_NAMHOC_HIENTAIs on
                                             new { ID_NAMHOC_HIENTAI = Convert.ToInt32(hkht.ID_NAMHOC_HIENTAI) } equals
                                             new { ID_NAMHOC_HIENTAI = nhht.ID_NAMHOC_HIENTAI }
                                         join kn in db.tbl_KHOAHOC_NGANHs on knct.ID_KHOAHOC_NGANH equals kn.ID_KHOAHOC_NGANH
                                         //join l in db.tbl_LOPHOCs on hp.ID_LOPHOC  equals l.ID_LOPHOC
                                         where
                                             (hp.IS_DELETE != 1 || hp.IS_DELETE == null) &&
                                             (mh.IS_DELETE != 1 || mh.IS_DELETE == null) &&
                                             (knct.IS_DELETE != 1 || knct.IS_DELETE == null) &&
                                             (hkht.IS_DELETE != 1 || hkht.IS_DELETE == null) &&
                                             (nhht.IS_DELETE != 1 || nhht.IS_DELETE == null) &&
                                             hkht.IS_HIENTAI == 1 && nhht.IS_HIENTAI == 1 &&
                                             (kn.IS_DELETE != 1 || kn.IS_DELETE == null) &&
                                             //(l.IS_DELETE != 1 || l.IS_DELETE == null) &&
                                             (from hpct in db.tbl_LOP_HOCPHAN_CTs
                                              where hpct.IS_DELETE != 1 || hpct.IS_DELETE == null
                                              select new
                                              {
                                                  hpct.ID_LOPHOCPHAN
                                              }).Contains(new { ID_LOPHOCPHAN = (System.Int32?)hp.ID_LOPHOCPHAN })
                                         select new
                                         {
                                             hp.ID_LOPHOCPHAN,
                                             ID_KHOAHOC_NGANH_CTIET = (int?)hp.ID_KHOAHOC_NGANH_CTIET,
                                             ID_NAMHOC_HKY_HTAI = (int?)hp.ID_NAMHOC_HKY_HTAI,
                                             hp.ID_HEDAOTAO,
                                             ID_MONHOC = (int?)hp.ID_MONHOC,
                                             mh.ISBATBUOC,
                                             hp.ID_LOPHOC,
                                             //l.TEN_LOP,
                                             hp.ID_GIANGVIEN,
                                             hp.MA_LOP_HOCPHAN,
                                             hp.TEN_LOP_HOCPHAN,
                                             mh.MA_MONHOC,
                                             mh.TEN_MONHOC,
                                             mh.SO_TC,
                                             mh.IS_LYTHUYET,
                                             hp.SOLUONG,
                                             hp.TUAN_BD,
                                             hp.TUAN_KT,
                                             SOSVDKY = (int?)db.SoSVDaDangKy(hp.ID_LOPHOCPHAN),
                                             TEN_GIANGVIEN = (from gv in db.tbl_GIANGVIENs where gv.ID_GIANGVIEN == hp.ID_GIANGVIEN && (gv.IS_DELETE != 1 || gv.IS_DELETE == null) select new { gv.TEN_GIANGVIEN }).FirstOrDefault().TEN_GIANGVIEN.ToString()
                                         };
                dt = TableUtil.LinqToDataTable(danhsachlophocphan);
                dt.Columns.Add("THOIKHOABIEU");
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow r in dt.Rows)
                    {
                        int idlophocphan = Convert.ToInt32(r["ID_LOPHOCPHAN"].ToString());
                        r["THOIKHOABIEU"] = GetChiTietLopHocPhan(idlophocphan);
                    }
                }
                return dt;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        DataTable GetDanhSachLopHocPhan_4(int idkhoahoc, int idnganh)
        {
            try
            {
                DataTable dt = null;
                var danhsachlophocphan = from hp in db.tbl_LOP_HOCPHANs
                                         join mh in db.tbl_MONHOCs on new { ID_MONHOC = Convert.ToInt32(hp.ID_MONHOC) } equals
                                             new { ID_MONHOC = mh.ID_MONHOC }
                                         join knct in db.tbl_KHOAHOC_NGANH_CTIETs on
                                             new { ID_KHOAHOC_NGANH_CTIET = Convert.ToInt32(hp.ID_KHOAHOC_NGANH_CTIET) } equals
                                             new { ID_KHOAHOC_NGANH_CTIET = knct.ID_KHOAHOC_NGANH_CTIET }
                                         join hkht in db.tbl_NAMHOC_HKY_HTAIs on
                                             new { ID_NAMHOC_HKY_HTAI = Convert.ToInt32(hp.ID_NAMHOC_HKY_HTAI) } equals
                                             new { ID_NAMHOC_HKY_HTAI = hkht.ID_NAMHOC_HKY_HTAI }
                                         join nhht in db.tbl_NAMHOC_HIENTAIs on
                                             new { ID_NAMHOC_HIENTAI = Convert.ToInt32(hkht.ID_NAMHOC_HIENTAI) } equals
                                             new { ID_NAMHOC_HIENTAI = nhht.ID_NAMHOC_HIENTAI }
                                         join kn in db.tbl_KHOAHOC_NGANHs on knct.ID_KHOAHOC_NGANH equals kn.ID_KHOAHOC_NGANH
                                         //join l in db.tbl_LOPHOCs on hp.ID_LOPHOC  equals l.ID_LOPHOC
                                         where
                                             kn.ID_KHOAHOC == idkhoahoc && kn.ID_NGANH == idnganh &&
                                             (hp.IS_DELETE != 1 || hp.IS_DELETE == null) &&
                                             (mh.IS_DELETE != 1 || mh.IS_DELETE == null) &&
                                             (knct.IS_DELETE != 1 || knct.IS_DELETE == null) &&
                                             (hkht.IS_DELETE != 1 || hkht.IS_DELETE == null) &&
                                             (nhht.IS_DELETE != 1 || nhht.IS_DELETE == null) &&
                                             hkht.IS_HIENTAI == 1 && nhht.IS_HIENTAI == 1 &&
                                             (kn.IS_DELETE != 1 || kn.IS_DELETE == null) &&
                                             //(l.IS_DELETE != 1 || l.IS_DELETE == null) &&
                                             (from hpct in db.tbl_LOP_HOCPHAN_CTs
                                              where hpct.IS_DELETE != 1 || hpct.IS_DELETE == null
                                              select new
                                              {
                                                  hpct.ID_LOPHOCPHAN
                                              }).Contains(new { ID_LOPHOCPHAN = (System.Int32?)hp.ID_LOPHOCPHAN })
                                         select new
                                         {
                                             hp.ID_LOPHOCPHAN,
                                             ID_KHOAHOC_NGANH_CTIET = (int?)hp.ID_KHOAHOC_NGANH_CTIET,
                                             ID_NAMHOC_HKY_HTAI = (int?)hp.ID_NAMHOC_HKY_HTAI,
                                             hp.ID_HEDAOTAO,
                                             ID_MONHOC = (int?)hp.ID_MONHOC,
                                             mh.ISBATBUOC,
                                             hp.ID_LOPHOC,
                                             //l.TEN_LOP,
                                             hp.ID_GIANGVIEN,
                                             hp.MA_LOP_HOCPHAN,
                                             hp.TEN_LOP_HOCPHAN,
                                             mh.MA_MONHOC,
                                             mh.TEN_MONHOC,
                                             mh.SO_TC,
                                             mh.IS_LYTHUYET,
                                             hp.SOLUONG,
                                             hp.TUAN_BD,
                                             hp.TUAN_KT,
                                             SOSVDKY = (int?)db.SoSVDaDangKy(hp.ID_LOPHOCPHAN),
                                             TEN_GIANGVIEN = (from gv in db.tbl_GIANGVIENs where gv.ID_GIANGVIEN == hp.ID_GIANGVIEN && (gv.IS_DELETE != 1 || gv.IS_DELETE == null) select new { gv.TEN_GIANGVIEN }).FirstOrDefault().TEN_GIANGVIEN.ToString()
                                         };
                dt = TableUtil.LinqToDataTable(danhsachlophocphan);
                dt.Columns.Add("THOIKHOABIEU");
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow r in dt.Rows)
                    {
                        int idlophocphan = Convert.ToInt32(r["ID_LOPHOCPHAN"].ToString());
                        r["THOIKHOABIEU"] = GetChiTietLopHocPhan(idlophocphan);
                    }
                }
                return dt;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        string GetChiTietLopHocPhan(int id_lophocphan)
        {
            string ketqua = "";
            try
            {
                DataTable dt = null;
                var dslophocphanchitiet = from hpct in db.tbl_LOP_HOCPHAN_CTs
                                          join p in db.tbl_PHONGHOCs on hpct.ID_PHONG equals p.ID_PHONG
                                          where (hpct.IS_DELETE != 1 || hpct.IS_DELETE == null) &&
                                                (p.IS_DELETE != 1 || p.IS_DELETE == null) &&
                                                hpct.ID_LOPHOCPHAN == id_lophocphan
                                          select new
                                          {
                                              THOIKHOABIEU = "Thứ:" + hpct.THU.ToString() + ",\tTiết: " + hpct.TIET_BD.ToString() + " đến " + hpct.TIET_KT.ToString() + "\tPhòng: " + p.MA_PHONG.ToString()
                                          };
                dt = TableUtil.LinqToDataTable(dslophocphanchitiet);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow r in dt.Rows)
                    {
                        ketqua = ketqua + "\n" + r["THOIKHOABIEU"].ToString() + "\n";
                    }
                }
                return ketqua;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public DataTable GetNganhWhereKhoaHoc(int idkhoahoc)
        {
            DataTable dt = null;
            var nganh = from kn in db.tbl_KHOAHOC_NGANHs
                        join kh in db.tbl_KHOAHOCs on new { ID_KHOAHOC = Convert.ToInt32(kn.ID_KHOAHOC) } equals
                            new { ID_KHOAHOC = kh.ID_KHOAHOC }
                        join ng in db.tbl_NGANHs on new { ID_NGANH = Convert.ToInt32(kn.ID_NGANH) } equals
                            new { ID_NGANH = ng.ID_NGANH }
                        where
                            (kn.IS_DELETE != 1 ||
                             kn.IS_DELETE == null) &&
                            (kh.IS_DELETE != 1 ||
                             kh.IS_DELETE == null) &&
                            (ng.IS_DELETE != 1 ||
                             ng.IS_DELETE == null) &&
                            kn.ID_KHOAHOC == idkhoahoc
                        select new
                        {
                            ID_NGANH = (int?)kn.ID_NGANH,
                            ng.TEN_NGANH
                        };
            dt = TableUtil.LinqToDataTable(nganh);
            return dt;
        }

        public DataTable GetMonHoc(int idkhoahoc, int idnganh)
        {
            try
            {
                DataTable dt = null;
                var dsmonhoc = (from knct in db.tbl_KHOAHOC_NGANH_CTIETs
                                join mh in db.tbl_MONHOCs on new { ID_MONHOC = Convert.ToInt32(knct.ID_MONHOC) } equals new { ID_MONHOC = mh.ID_MONHOC }
                                join kn in db.tbl_KHOAHOC_NGANHs on new { ID_KHOAHOC_NGANH = Convert.ToInt32(knct.ID_KHOAHOC_NGANH) } equals new { ID_KHOAHOC_NGANH = kn.ID_KHOAHOC_NGANH }
                                join hp in db.tbl_LOP_HOCPHANs on new { ID_KHOAHOC_NGANH_CTIET = knct.ID_KHOAHOC_NGANH_CTIET } equals new { ID_KHOAHOC_NGANH_CTIET = Convert.ToInt32(hp.ID_KHOAHOC_NGANH_CTIET) }
                                join hkht in db.tbl_NAMHOC_HKY_HTAIs on new { ID_NAMHOC_HKY_HTAI = Convert.ToInt32(hp.ID_NAMHOC_HKY_HTAI) } equals new { ID_NAMHOC_HKY_HTAI = hkht.ID_NAMHOC_HKY_HTAI }
                                where
                                  (knct.IS_DELETE != 1 ||
                                  knct.IS_DELETE == null) &&
                                  (mh.IS_DELETE != 1 ||
                                  mh.IS_DELETE == null) &&
                                  (hp.IS_DELETE != 1 ||
                                  hp.IS_DELETE == null) &&
                                  (hkht.IS_DELETE != 1 ||
                                  hkht.IS_DELETE == null) &&
                                  hkht.IS_HIENTAI == 1 &&
                                  kn.ID_KHOAHOC == idkhoahoc &&
                                  kn.ID_NGANH == idnganh
                                select new
                                {
                                    mh.ID_MONHOC,
                                    mh.TEN_MONHOC
                                }).Distinct();
                dt = TableUtil.LinqToDataTable(dsmonhoc);
                if (dt.Rows.Count > 0)
                {
                    DataRow r = dt.NewRow();
                    r["ID_MONHOC"] = 0;
                    r["TEN_MONHOC"] = "------------------------Tất cả----------------------";

                    dt.Rows.Add(r);DataTable xdt = dt.AsEnumerable().OrderBy(t => t.Field<int>("ID_MONHOC")).CopyToDataTable();
                    dt = xdt.Copy();
                }
                return dt;
            }
            catch (Exception err)
            {
                throw err;
            }
        }
    }
}
