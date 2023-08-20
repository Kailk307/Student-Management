using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using LyTanKhai_2121110103.DAL;

namespace LyTanKhai_2121110103.BAL
{
    class SVBAL
    {
        public static void Nhap_SV(string masv, string hosv, string tensv, int gt, string ngaysinh, string malop, string makhoa)
        {

            SqlConnection con = new SqlConnection(DBconnection.strcon);
            con.Open();
            SqlCommand cmd = new SqlCommand("insert into SINHVIEN values('" + masv + "',N'" + hosv + "',N'" + tensv + "','" + gt + "','" + ngaysinh + "','" + malop + "','" + makhoa + "')", con);
            cmd.ExecuteNonQuery();
            con.Close();

        }
        public static void Sua_SV(string masv, string hosv, string tensv, int gt, string ngaysinh, string malop, string makhoa)
        {
            using (SqlConnection con = new SqlConnection(DBconnection.strcon))
            {
                con.Open();
                string query = "UPDATE SINHVIEN SET HoSV = @HoSV, TenSV = @TenSV, GioiTinh = @GioiTinh, NgaySinh = @NgaySinh, MaLop = @MaLop,MaKhoa =@Makhoa WHERE MaSV = @MaSV";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@HoSV", hosv);
                    cmd.Parameters.AddWithValue("@TenSV", tensv);
                    cmd.Parameters.AddWithValue("@GioiTinh", gt);
                    cmd.Parameters.AddWithValue("@NgaySinh", ngaysinh);
                    cmd.Parameters.AddWithValue("@MaLop", malop);
                    cmd.Parameters.AddWithValue("@MaSV", masv);
                    cmd.Parameters.AddWithValue("@MaKhoa", makhoa);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static void Xoa_SV(string masv)
        {
            SqlConnection con = new SqlConnection(DBconnection.strcon);
            con.Open();
            SqlCommand cmd = new SqlCommand("delete from SINHVIEN  where MaSV='" + masv + "'", con);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public static DataTable timSV_Ma(string ma)
        {
            SqlConnection con = new SqlConnection(DBconnection.strcon);
            con.Open();
            SqlDataAdapter adap = new SqlDataAdapter("select row_number() over (order by MaSV) as STT, MaSV as [Mã Sinh Viên],HoSV as[Họ Sinh Viên],TenSV as[Tên Sinh Viên],case when GioiTinh = '1' then 'Nam' else N'Nữ' end as [Giới Tính],NgaySinh as[Ngày Sinh],MaLop as [Mã Lớp], MaKhoa as [Mã Khoa]  from SINHVIEN where MaSV like '%" + ma + "%'", con);
            DataTable ds = new DataTable();
            adap.Fill(ds);
            con.Close();
            return ds;

        }
        public static DataTable timSV_Ten(string ten)
        {
            SqlConnection con = new SqlConnection(DBconnection.strcon);
            con.Open();
            SqlDataAdapter adap = new SqlDataAdapter("select row_number() over (order by MaSV) as STT, MaSV as [Mã Sinh Viên],HoSV as [Họ Sinh Viên],TenSV as[Tên Sinh Viên],case when GioiTinh = '1' then 'Nam' else N'Nữ' end as [Giới Tính],NgaySinh as[Ngày Sinh],MaLop as [Mã Lớp], MaKhoa as [Mã Khoa]  from SINHVIEN where TenSV like N'%" + ten + "%'", con);
            DataTable ds = new DataTable();
            adap.Fill(ds);
            con.Close();
            return ds;

        }
    }
}
