using LyTanKhai_2121110103.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyTanKhai_2121110103.BAL
{
    public class LoginBAL
    {
        private LoginDAL LoginDAL;
        public string ConnectionString { get; set; }

        public LoginBAL()
        {
            LoginDAL = new LoginDAL();
        }


    }
}
