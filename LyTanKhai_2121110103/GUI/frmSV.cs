using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Word;
using iTextSharp.text;
using System.IO;
using Document = Microsoft.Office.Interop.Word.Document;
using iTextSharp.text.pdf;
namespace LyTanKhai_2121110103.GUI
{
    public partial class frmSV : Form
    {
        public frmSV()
        {
            InitializeComponent();
        }
        int dem = 0;
        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMaSV.Text == "")
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin", "Quản lý sinh viên", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    int gt = 1;
                    if (radNam.Checked == true)
                    {
                        gt = 1;
                    }
                    else
                    {
                        gt = 0;
                    }
                   BAL.SVBAL.Nhap_SV(txtMaSV.Text, BAL.xulichuoi.VietHoa(txtHoSV.Text), BAL.xulichuoi.VietHoa(txtTenSV.Text), gt, dtpNgaySinh.Text, cmbMaLop.SelectedValue.ToString(), cbbKhoa.SelectedValue.ToString());
                    dgvSinhVien1.DataSource = Data.DS_SINHVIEN();
                    MessageBox.Show("Thêm thành công", "Quản lý sinh viên", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dem = dgvSinhVien1.RowCount;
                    label3.Text = "Tổng Số SV : " + dem.ToString();
                }

            }
            catch
            {
                MessageBox.Show("Thêm thất bại", "Quản lý sinh viên", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMaSV.Text == "")
                {
                    MessageBox.Show("Vui lòng chọn một sinh viên để sửa thông tin.", "Thông báo ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    int gt = radNam.Checked ? 1 : 0;

                    if (cmbMaLop.SelectedItem != null && cbbKhoa.SelectedItem != null)
                    {
                       BAL.SVBAL.Sua_SV(
                            txtMaSV.Text,
                            BAL.xulichuoi.VietHoa(txtHoSV.Text),
                            BAL.xulichuoi.VietHoa(txtTenSV.Text),
                            gt,
                            dtpNgaySinh.Text,
                            cmbMaLop.SelectedValue.ToString(),
                            cbbKhoa.SelectedValue.ToString()
                        );
                    }
                    else
                    {
                        // Display a message or handle the situation where one of the ComboBoxes does not have a selected value.
                    }

                    dgvSinhVien1.DataSource = Data.DS_SINHVIEN();
                    MessageBox.Show("Cập nhật thông tin thành công.", "Thông báo ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch
            {
                MessageBox.Show("Sửa thất bại", "Quản lý sinh viên", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMaSV.Text == "")
                {
                    MessageBox.Show("Vui lòng chọn một sinh viên để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa sinh viên này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                       BAL.SVBAL.Xoa_SV(txtMaSV.Text);
                        dgvSinhVien1.DataSource = Data.DS_SINHVIEN();
                        MessageBox.Show("Xóa sinh viên thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch
            {
                MessageBox.Show("Lỗi.", "Quản lý sinh viên", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnexcel_Click(object sender, EventArgs e)
        {
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string filePath = saveFileDialog.FileName;

                        Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
                        excelApp.Visible = false;
                        excelApp.Workbooks.Add();

                        Microsoft.Office.Interop.Excel._Worksheet worksheet = (Microsoft.Office.Interop.Excel._Worksheet)excelApp.ActiveSheet;

                        int rowIndex = 1;

                        // Header
                        for (int colIndex = 1; colIndex <= dgvSinhVien1.Columns.Count; colIndex++)
                        {
                            worksheet.Cells[rowIndex, colIndex] = dgvSinhVien1.Columns[colIndex - 1].HeaderText;
                        }
                        rowIndex++;

                        // Data
                        foreach (DataGridViewRow row in dgvSinhVien1.Rows)
                        {
                            for (int colIndex = 1; colIndex <= dgvSinhVien1.Columns.Count; colIndex++)
                            {
                                worksheet.Cells[rowIndex, colIndex] = row.Cells[colIndex - 1].Value;
                            }
                            rowIndex++;
                        }


                        worksheet.SaveAs(filePath);
                        excelApp.Quit();
                        MessageBox.Show("Export Excel thành công.", "Export PDF", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void btnpdf_Click(object sender, EventArgs e)
        {
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string filePath = saveFileDialog.FileName;

                        iTextSharp.text.Document doc = new iTextSharp.text.Document();
                        PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(filePath, FileMode.Create));
                        doc.Open();

                        PdfPTable pdfTable = new PdfPTable(dgvSinhVien1.Columns.Count);
                        pdfTable.DefaultCell.Padding = 3;
                        pdfTable.WidthPercentage = 100;
                        pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;

                        // Add Headers
                        foreach (DataGridViewColumn column in dgvSinhVien1.Columns)
                        {
                            PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
                            pdfTable.AddCell(cell);
                        }

                        // Add Rows
                        foreach (DataGridViewRow row in dgvSinhVien1.Rows)
                        {
                            foreach (DataGridViewCell cell in row.Cells)
                            {
                                pdfTable.AddCell(cell.Value.ToString());
                            }
                        }

                        doc.Add(pdfTable);
                        doc.Close();

                        MessageBox.Show("Export file PDF thành công.", "Export PDF", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void btnword_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Word documents (*.docx)|*.docx";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName;

                    Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                    Document doc = wordApp.Documents.Add();
                    Table table = doc.Tables.Add(doc.Range(), dgvSinhVien1.Rows.Count + 1, dgvSinhVien1.Columns.Count);

                    // Add Headers
                    for (int colIndex = 0; colIndex < dgvSinhVien1.Columns.Count; colIndex++)
                    {
                        table.Cell(1, colIndex + 1).Range.Text = dgvSinhVien1.Columns[colIndex].HeaderText;
                    }

                    // Add Rows
                    for (int rowIndex = 0; rowIndex < dgvSinhVien1.Rows.Count; rowIndex++)
                    {
                        for (int colIndex = 0; colIndex < dgvSinhVien1.Columns.Count; colIndex++)
                        {
                            table.Cell(rowIndex + 2, colIndex + 1).Range.Text = dgvSinhVien1.Rows[rowIndex].Cells[colIndex].Value.ToString();
                        }
                    }

                    // Save and Close
                    doc.SaveAs2(filePath);
                    doc.Close();
                    wordApp.Quit();

                    MessageBox.Show("Export file Word thành công.", "Export Word", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        int row;
        private void dgvSinhVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvSinhVien1.Rows.Count)
            {
                row = e.RowIndex;

                if (dgvSinhVien1.Rows[row].Cells[4].Value != null)
                {
                    if (dgvSinhVien1.Rows[row].Cells[4].Value.ToString() == "Nam")
                    {
                        radNam.Checked = true;
                    }
                    else
                    {
                        radNu.Checked = true;
                    }
                }

                txtMaSV.Text = dgvSinhVien1.Rows[row].Cells[1].Value.ToString();
                txtHoSV.Text = dgvSinhVien1.Rows[row].Cells[2].Value.ToString();
                txtTenSV.Text = dgvSinhVien1.Rows[row].Cells[3].Value.ToString();

                if (dgvSinhVien1.Rows[row].Cells[5].Value != null)
                {
                    dtpNgaySinh.Text = dgvSinhVien1.Rows[row].Cells[5].Value.ToString();
                }

                if (dgvSinhVien1.Rows[row].Cells[6].Value != null)
                {
                    cmbMaLop.Text = dgvSinhVien1.Rows[row].Cells[6].Value.ToString();
                }

                if (dgvSinhVien1.Rows[row].Cells[7].Value != null)
                {
                    cbbKhoa.Text = dgvSinhVien1.Rows[row].Cells[7].Value.ToString();
                }
            }
        }

        private void frmSV_Load(object sender, EventArgs e)
        {
            dgvSinhVien1.DataSource = Data.DS_SINHVIEN();
            dem = dgvSinhVien1.RowCount;
            label3.Text = "Tổng Số SV : " + dem.ToString();

            cmbMaLop.DataSource = Data.DS_LOP();
            cmbMaLop.DisplayMember = "MaLop";
            cmbMaLop.ValueMember = "MaLop";

            cbbKhoa.DataSource =Data.DS_KHOA();
            cbbKhoa.DisplayMember = "MaKhoa";
            cbbKhoa.ValueMember = "MaKhoa";
        }
        private void dgvSinhVien1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvSinhVien1.Rows.Count)
            {
                row = e.RowIndex;

                if (dgvSinhVien1.Rows[row].Cells[4].Value != null)
                {
                    if (dgvSinhVien1.Rows[row].Cells[4].Value.ToString() == "Nam")
                    {
                        radNam.Checked = true;
                    }
                    else
                    {
                        radNu.Checked = true;
                    }
                }

                txtMaSV.Text = dgvSinhVien1.Rows[row].Cells[1].Value.ToString();
                txtHoSV.Text = dgvSinhVien1.Rows[row].Cells[2].Value.ToString();
                txtTenSV.Text = dgvSinhVien1.Rows[row].Cells[3].Value.ToString();

                if (dgvSinhVien1.Rows[row].Cells[5].Value != null)
                {
                    dtpNgaySinh.Text = dgvSinhVien1.Rows[row].Cells[5].Value.ToString();
                }

                if (dgvSinhVien1.Rows[row].Cells[6].Value != null)
                {
                    cmbMaLop.Text = dgvSinhVien1.Rows[row].Cells[6].Value.ToString();
                }

                if (dgvSinhVien1.Rows[row].Cells[7].Value != null)
                {
                    cbbKhoa.Text = dgvSinhVien1.Rows[row].Cells[7].Value.ToString();
                }
            }
        }

       
    }
}
