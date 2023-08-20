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
    public partial class frmlop : Form
    {
        public frmlop()
        {
            InitializeComponent();
        }

        private void frmlop_Load(object sender, EventArgs e)
        {
            cbbKhoa.DataSource = Data.DS_KHOA();
            cbbKhoa.DisplayMember = "TenKhoa";
            cbbKhoa.ValueMember = "MaKhoa";

            cbbGV.DataSource = Data.DS_GIANGVIEN();
            cbbGV.DisplayMember = "TenGV";
            cbbGV.ValueMember = "MaGV";

            dgvLop.DataSource = Data.DS_LOP();//chọn ngồn dữ liệu
            dgvLop.Columns[0].HeaderText = "Mã lớp";
            dgvLop.Columns[1].HeaderText = "Tên lớp";
            dgvLop.Columns[2].HeaderText = " Mã khoa";
            dgvLop.Columns[3].HeaderText = "GV";
            dgvLop.Columns[0].Width = 140;
            dgvLop.Columns[1].Width = 140;
            dgvLop.Columns[2].Width = 140;
            dgvLop.Columns[3].Width = 140;
            dem = dgvLop.RowCount - 1;//đếm số lượng
            lblThongbao.Text = "Tổng số lớp: " + dem.ToString();

        }     
        private void btnThem_Click(object sender, EventArgs e)
        {
           
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            
        }
        int row;
        private void btnSua_Click(object sender, EventArgs e)
        {
            
        }
        private void dgvLop_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvLop.Rows.Count)
            {
                row = e.RowIndex;
                txtMaLop.Text = dgvLop.Rows[row].Cells[0].Value.ToString();
                txtTenLop.Text = dgvLop.Rows[row].Cells[1].Value.ToString();
            }
        }
        private void dgvLop_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvLop.Rows.Count)
            {
                row = e.RowIndex;
                txtMaLop.Text = dgvLop.Rows[row].Cells[0].Value.ToString();
                txtTenLop.Text = dgvLop.Rows[row].Cells[1].Value.ToString();
            }
        }
        int dem = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                BAL.LopBAL.Them(txtMaLop.Text, BAL.xulichuoi.VietHoa(txtTenLop.Text), cbbKhoa.SelectedValue.ToString(), cbbGV.SelectedValue.ToString());
                dgvLop.DataSource = Data.DS_LOP();
                MessageBox.Show("Thêm thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dem = dgvLop.RowCount - 1;
                lblThongbao.Text = "Tổng : " + dem.ToString();
            }
            catch
            {
                MessageBox.Show("Thêm thất bại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.DialogResult rs;
            if (txtMaLop.Text == "")
            {
                MessageBox.Show("Vui lòng chọn hàng cần xóa", "Xóa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                rs = MessageBox.Show("Bạn chắc chắn muốn xóa", "Xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (rs == System.Windows.Forms.DialogResult.Yes)
                {
                    //hàm xóa dữ liệu
                    BAL.LopBAL.Xoa(txtMaLop.Text);
                    dgvLop.DataSource = Data.DS_LOP();//hiện lên gridview
                    MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
        }
        private bool allowMaLopSua = false;
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (!allowMaLopSua)
                {
                    MessageBox.Show("Không được phép sửa mã lớp", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                BAL.LopBAL.Sua(txtMaLop.Text, BAL.xulichuoi.VietHoa(txtTenLop.Text), cbbKhoa.SelectedValue.ToString(), cbbGV.SelectedValue.ToString());

                dgvLop.DataSource = Data.DS_LOP();

                MessageBox.Show("Sửa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("Vui lòng chọn hàng để sửa", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

    }
}
