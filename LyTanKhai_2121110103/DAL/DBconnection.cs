using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
namespace LyTanKhai_2121110103.DAL
{
    public class DBconnection
    {
        public static string strcon = @"Data Source=LAPTOP-6R1I7R57\LYTANKHAI;Initial Catalog=QLSV1;Integrated Security=True";
        SqlConnection con = new SqlConnection(strcon);
    }
}
