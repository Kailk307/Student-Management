using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LyTanKhai_2121110103.GUI
{
    public partial class frmkhoa : Form
    {
        public frmkhoa()
        {
            InitializeComponent();
        }
        int dem = 0;
        private void btnThem_Click(object sender, EventArgs e)
        {
           
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (txtMaKhoa.Text == "")
            {
                MessageBox.Show("Chọn hàng cần sửa", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                // Hàm sửa dữ liệu
                BAL.KhoaBAL.Sua_Khoa(txtMaKhoa.Text, BAL.xulichuoi.VietHoa(txtTenKhoa.Text));
                dgvKhoa.DataSource = Data.DS_MONHOC();

                MessageBox.Show("Sửa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void frmkhoa_Load(object sender, EventArgs e)
        {
            dgvKhoa.DataSource = Data.DS_KHOA();//chọn ngồn dữ liệu
            dgvKhoa.Columns[0].HeaderText = "Mã khoa";
            dgvKhoa.Columns[1].HeaderText = "Tên khoa";
            dgvKhoa.Columns[0].Width = 140;
            dgvKhoa.Columns[1].Width = 140;
            dem = dgvKhoa.RowCount - 1;
            label3.Text = "Tổng số khoa: " + dem.ToString();

        }
        int row;
        private void dgvKhoa_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvKhoa.Rows.Count)
            {
                row = e.RowIndex;
                txtMaKhoa.Text = dgvKhoa.Rows[row].Cells[0].Value.ToString();
                txtTenKhoa.Text = dgvKhoa.Rows[row].Cells[1].Value.ToString();
            }
        }
        private void dgvKhoa_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //chọn dữ liệu từ gridview sau đó hiện lên các textbox
            if (e.RowIndex >= 0 && e.RowIndex < dgvKhoa.Rows.Count)
            {
                row = e.RowIndex;
                txtMaKhoa.Text = dgvKhoa.Rows[row].Cells[0].Value.ToString();
                txtTenKhoa.Text = dgvKhoa.Rows[row].Cells[1].Value.ToString();
            }

        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                BAL.KhoaBAL.Nhap_Khoa(txtMaKhoa.Text, BAL.xulichuoi.VietHoa(txtTenKhoa.Text));
                dgvKhoa.DataSource = Data.DS_KHOA();
                MessageBox.Show("Thêm thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dem = dgvKhoa.RowCount - 1;
                label3.Text = "Tổng số khoa: " + dem.ToString();

            }
            catch
            {
                MessageBox.Show("Nhập thất bại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private bool allowMaKhoaSua = false;
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (!allowMaKhoaSua)
                {
                    MessageBox.Show("Không được phép sửa mã khoa", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                BAL.KhoaBAL.Sua_Khoa(txtMaKhoa.Text, BAL.xulichuoi.VietHoa(txtTenKhoa.Text));

                dgvKhoa.DataSource = Data.DS_KHOA();

                MessageBox.Show("Sửa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("Vui lòng chọn hàng để sửa", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.DialogResult rs;
            if (txtMaKhoa.Text == "")
            {
                MessageBox.Show("Vui lòng chọn hàng cần xóa", "Xóa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                rs = MessageBox.Show("Bạn chắc chắn muốn xóa", "Xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (rs == System.Windows.Forms.DialogResult.Yes)
                {
                    //hàm xóa dữ liệu
                    BAL.KhoaBAL.Xoa_Khoa(txtMaKhoa.Text);
                    dgvKhoa.DataSource = Data.DS_KHOA();//hiện lên gridview
                    MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
        }
    }
}
