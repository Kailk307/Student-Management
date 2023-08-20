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
    public static class KhoaBAL
    {
        public static void Nhap_Khoa(string makhoa, string tenkhoa)
        {
            SqlConnection con = new SqlConnection(DBconnection.strcon);
            con.Open();
            SqlCommand cmd = new SqlCommand("insert into KHOA values('" + makhoa + "',N'" + tenkhoa + "')", con);
            cmd.ExecuteNonQuery();
            con.Close();

        }
        public static void Sua_Khoa(string makhoa, string tenkhoa)
        {
            SqlConnection con = new SqlConnection(DBconnection.strcon);
            con.Open();
            SqlCommand cmd = new SqlCommand("UPDATE KHOA SET TenKhoa=N'" + tenkhoa + "' WHERE MaKhoa='" + makhoa + "'", con);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public static void Xoa_Khoa(string makhoa)
        {
            SqlConnection con = new SqlConnection(DBconnection.strcon);
            con.Open();
            SqlCommand cmd = new SqlCommand("delete  KHOA  where MaKhoa='" + makhoa + "'", con);
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}
