using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DATN.BUS
{
    public static class UserCommon
    {
        private static string User_Name;

        public static string UserName
        {
            get { return User_Name; }
            set { User_Name = value; }
        }

        private static int _Id_SinhVien;

        private static string _HoTen_SinhVien;

        private static int _Id_NamHocHienTai;

        private static int _idNamHoc_HocKy_Htai;

        public static int IdSinhVien
        {
            get { return _Id_SinhVien; }
            set { _Id_SinhVien = value; }
        }

        public static string HoTenSinhVien
        {
            get { return _HoTen_SinhVien; }
            set { _HoTen_SinhVien = value; }
        }

        public static int IdNamHocHocKyHtai
        {
            get { return _idNamHoc_HocKy_Htai; }
            set { _idNamHoc_HocKy_Htai = value; }
        }

        public static int IdNamHocHienTai
        {
            get { return _Id_NamHocHienTai; }
            set { _Id_NamHocHienTai = value; }
        }
    }
}