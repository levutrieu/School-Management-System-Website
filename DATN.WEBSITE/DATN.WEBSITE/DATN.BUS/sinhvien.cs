using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.BUS
{
    public class sinhvien
    {
        private string _MaSinhVien;
        private string _HoTen;
        private string _TenLop;
        private string _TenNganh;
        private string _HeDaoTao;
        private string _KhoaHoc;
        private int _Id_HeDaoTao;
        private int _Id_SinhVien;
        private int _IdNganh;
        private string _NgaySinh;
        private string _NoiSinh;
        private string _DiaChi;
        private bool _GioiTinh;
        private string _DienThoai;
        private string _CMND;
        private string _NgayCap;
        private string _NoiCap;
        private string _Email;
        private string _ThongTinNgoaiTru;





        public sinhvien()
        {
            
        }

        public string MaSinhVien
        {
            get { return _MaSinhVien; }
            set { _MaSinhVien = value; }
        }

        public string HoTen
        {
            get { return _HoTen; }
            set { _HoTen = value; }
        }

        public string TenLop
        {
            get { return _TenLop; }
            set { _TenLop = value; }
        }

        public string TenNganh
        {
            get { return _TenNganh; }
            set { _TenNganh = value; }
        }

        public string HeDaoTao
        {
            get { return _HeDaoTao; }
            set { _HeDaoTao = value; }
        }

        public string KhoaHoc
        {
            get { return _KhoaHoc; }
            set { _KhoaHoc = value; }
        }

        public int IdHeDaoTao
        {
            get { return _Id_HeDaoTao; }
            set { _Id_HeDaoTao = value; }
        }

        public int IdSinhVien
        {
            get { return _Id_SinhVien; }
            set { _Id_SinhVien = value; }
        }

        public int IdNganh
        {
            get { return _IdNganh; }
            set { _IdNganh = value; }
        }

        public string NgaySinh
        {
            get { return _NgaySinh; }
            set { _NgaySinh = value; }
        }

        public string NoiSinh
        {
            get { return _NoiSinh; }
            set { _NoiSinh = value; }
        }

        public string DiaChi
        {
            get { return _DiaChi; }
            set { _DiaChi = value; }
        }

        public bool GioiTinh
        {
            get { return _GioiTinh; }
            set { _GioiTinh = value; }
        }

        public string DienThoai
        {
            get { return _DienThoai; }
            set { _DienThoai = value; }
        }

        public string Cmnd
        {
            get { return _CMND; }
            set { _CMND = value; }
        }

        public string NgayCap
        {
            get { return _NgayCap; }
            set { _NgayCap = value; }
        }

        public string NoiCap
        {
            get { return _NoiCap; }
            set { _NoiCap = value; }
        }

        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }

        public string ThongTinNgoaiTru
        {
            get { return _ThongTinNgoaiTru; }
            set { _ThongTinNgoaiTru = value; }
        }
    }
}
