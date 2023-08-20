using LyTanKhai_2121110103.DAL;
using LyTanKhai_2121110103.GUI;
using LyTanKhai_2121110103.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LyTanKhai_2121110103
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
        private int invalidLoginAttempts = 0; // Biến đếm số lần nhập sai
        private void button1_Click(object sender, EventArgs e)
        {
            string username = tbName.Text;
            string password = tbPass.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin đăng nhập!");
                return; // Dừng thực hiện nếu thông tin không đủ
            }

            LoginBEL user = new LoginBEL
            {
                TaiKhoan = username,
                Matkhau = password
            };

            LoginDAL loginDAL = new LoginDAL(); // Create an instance of LoginDAL

            bool isValid = loginDAL.ValidateUser(user); // Call ValidateUser on the instance

            if (isValid)
            {
                MessageBox.Show("Đăng nhập thành công!");

                main form1 = new main();
                form1.Show();

                // Close the current login form
                this.Hide(); // Thực hiện hành động sau khi đăng nhập thành công, ví dụ: Mở form chính, v.v.
            }
            else
            {
                invalidLoginAttempts++; // Tăng số lần nhập sai

                if (invalidLoginAttempts >= 3)
                {
                    MessageBox.Show("Bạn đã nhập sai mật khẩu 3 lần. Ứng dụng sẽ thoát!");
                    Application.Exit(); // Thoát khỏi ứng dụng
                }
                else
                {
                    MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng!");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult S = MessageBox.Show("Bạn có chắc muốn thoát  không", "Thông báo", MessageBoxButtons.OKCancel);
            if (S == DialogResult.OK)
                Application.Exit();
        }

        private void tbName_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
