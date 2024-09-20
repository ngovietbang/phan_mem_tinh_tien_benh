using Phan_mem_tinh_toan_tien_benh.custom;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Phan_mem_tinh_toan_tien_benh
{
    public partial class Form1 : Form
    {
        //set thông báo lỗi
        private ErrorProvider errorProvider;
        //set lỗi First click cho textbox tuổi
        private bool click1 = true;
        //set lỗi First click cho textbox tuổi
        private int demClick = 0;

        public Form1()
        {
            InitializeComponent();
            errorProvider = new ErrorProvider();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //khởi tạo mặc định check nam
            cb_nam.Checked = true;
            //place holder
            tb_tuoi.Text = "Nhập số tuổi của bệnh nhân";
            tb_tuoi.ForeColor = Color.Gray;
            tb_thanhtien.Text = "Số tiền cần thanh toán";
            tb_thanhtien.ForeColor = Color.Gray;

        }

        //Clear lỗi
        private void ClearAllErrors(Control.ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                if (control is System.Windows.Forms.TextBox || control is System.Windows.Forms.ComboBox || control is System.Windows.Forms.CheckBox)
                {
                    errorProvider.SetError(control, string.Empty);
                }

                // Nếu control là container, lặp đệ quy qua các điều khiển con
                if (control.Controls.Count > 0)
                {
                    ClearAllErrors(control.Controls);
                }
            }
        }

        //check box nam -> check
        private void cb_nam_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                //bỏ check nữ và trẻ em
                if (cb_nam.Checked == true)
                {
                    ClearAllErrors(this.Controls);
                    cb_nu.Checked = false;
                    cb_treem.Checked = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //check box nữ -> check
        private void cb_nu_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                //bỏ check nam và trẻ em
                if (cb_nu.Checked == true)
                {
                    ClearAllErrors(this.Controls);
                    cb_nam.Checked = false;
                    cb_treem.Checked = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //check box trẻ em -> check
        private void cb_treem_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                ClearAllErrors(this.Controls);
                //bỏ check nam và nữ
                if (cb_treem.Checked == true)
                {
                    ClearAllErrors(this.Controls);
                    cb_nam.Checked = false;
                    cb_nu.Checked = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Click textbox tuổi để xóa placeholder
        private void tb_tuoi_Enter(object sender, EventArgs e)
        {
            tb_tuoi.Clear() ;
            tb_tuoi.ForeColor = Color.Black;
            demClick = 1;
        }

        //nút tính tiền
        private void bt_tinh_Click(object sender, EventArgs e)
        {
            string tuoi = tb_tuoi.Text.ToString().Trim();
            //định dạng tuổi là dạng số
            bool kiemtratuoi = decimal.TryParse(tuoi, out decimal sotuoi);

            try
            {
                //Lỗi không nhập loại đối tượng
                if (cb_nam.Checked == false && cb_nu.Checked == false && cb_treem.Checked == false)
                {
                    ClearAllErrors(this.Controls);
                    errorProvider.SetError(cb_nam, "Vui lòng chọn loại đối tượng");
                    errorProvider.SetError(cb_nu, "Vui lòng chọn loại đối tượng");
                    errorProvider.SetError(cb_treem, "Vui lòng chọn loại đối tượng");
                    MessageBox.Show("Error: " + "Chưa chọn loại đối tượng!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                //tránh lỗi placeholder chiếm text lần đầu
                else if (click1 && demClick == 0)
                {
                    tb_tuoi.Clear() ;
                    ClearAllErrors(this.Controls);
                    errorProvider.SetError(tb_tuoi, "Giá trị không được để trống");
                    MessageBox.Show("Error: " + "Giá trị không được để trống", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    click1 = false;
                }
                //Lỗi null giá trị
                else if (string.IsNullOrWhiteSpace(tuoi))
                {
                    ClearAllErrors(this.Controls);
                    errorProvider.SetError(tb_tuoi, "Giá trị không được để trống");
                    MessageBox.Show("Error: " + "Giá trị không được để trống", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                //lỗi sai định dạng tuổi
                else if (!kiemtratuoi)
                {
                    ClearAllErrors(this.Controls);
                    errorProvider.SetError(tb_tuoi, "Tuổi phải là giá trị số");
                    MessageBox.Show("Error: " + "Nhập sai định dạng", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                // sai tuổi
                else if (sotuoi < 0 || sotuoi > 145)
                {
                    ClearAllErrors(this.Controls);
                    errorProvider.SetError(tb_tuoi, "Vui lòng nhập chính xác độ tuổi");
                    MessageBox.Show("Error: " + "Vui lòng nhập chính xác độ tuổi", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                //Đối tượng nam
                else if (cb_nam.Checked && sotuoi >= 18 && sotuoi <= 35 )
                {
                    ClearAllErrors(this.Controls);
                    tb_thanhtien.Text = "200000";
                    tb_thanhtien.ForeColor = Color.Red;
                }
                else if (cb_nam.Checked && sotuoi >= 36 && sotuoi <= 50)
                {
                    ClearAllErrors(this.Controls);
                    tb_thanhtien.Text = "300000";
                    tb_thanhtien.ForeColor = Color.Red;
                }
                else if (cb_nam.Checked && sotuoi >= 51 && sotuoi <= 145)
                {
                    ClearAllErrors(this.Controls);
                    tb_thanhtien.Text = "500000";
                    tb_thanhtien.ForeColor = Color.Red;
                }
                else if (cb_nam.Checked && sotuoi < 18) //Lỗi Nam < 18
                {
                    ClearAllErrors(this.Controls);
                    tb_thanhtien.Clear();
                    errorProvider.SetError(tb_tuoi, "Đối tượng Nam có các độ tuổi từ 18 - 145");
                    MessageBox.Show("Error: " + "Vui lòng nhập chính xác độ tuổi (Đối tượng Nam có các độ tuổi từ 18 - 145)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                //Đối tượng nữ
                else if (cb_nu.Checked && sotuoi >= 18 && sotuoi <= 35)
                {
                    ClearAllErrors(this.Controls);
                    tb_thanhtien.Text = "180000";
                    tb_thanhtien.ForeColor = Color.Red;
                }
                else if (cb_nu.Checked && sotuoi >= 36 && sotuoi <= 50)
                {
                    ClearAllErrors(this.Controls);
                    tb_thanhtien.Text = "250000";
                    tb_thanhtien.ForeColor = Color.Red;
                }
                else if (cb_nu.Checked && sotuoi >= 51 && sotuoi <= 145)
                {
                    ClearAllErrors(this.Controls);
                    tb_thanhtien.Text = "450000";
                    tb_thanhtien.ForeColor = Color.Red;
                }
                else if (cb_nu.Checked && sotuoi < 18) //Lỗi Nữ < 18
                {
                    ClearAllErrors(this.Controls);
                    tb_thanhtien.Clear();
                    errorProvider.SetError(tb_tuoi, "Đối tượng Nữ có các độ tuổi từ 18 - 145");
                    MessageBox.Show("Error: " + "Vui lòng nhập chính xác độ tuổi (Đối tượng Nữ có các độ tuổi từ 18 - 145)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                //Đối tượng trẻ em
                else if (cb_treem.Checked && sotuoi >= 0 && sotuoi <= 17)
                {
                    ClearAllErrors(this.Controls);
                    tb_thanhtien.Text = "100000";
                    tb_thanhtien.ForeColor = Color.Red;
                }
                else if (cb_treem.Checked && sotuoi > 17) //Lỗi trẻ em > 17
                {
                    ClearAllErrors(this.Controls);
                    tb_thanhtien.Clear();
                    errorProvider.SetError(tb_tuoi, "Đối tượng Trẻ em có các độ tuổi từ 0 - 17");
                    MessageBox.Show("Error: " + "Vui lòng nhập chính xác độ tuổi (Đối tượng Trẻ em có các độ tuổi từ 0 - 17)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    ClearAllErrors(this.Controls);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.Message, "Error: ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //nút làm mới
        private void bt_lammoi_Click(object sender, EventArgs e)
        {
            ClearAllErrors(this.Controls);
            tb_tuoi.Text = "Nhập số tuổi của bệnh nhân";
            tb_tuoi.ForeColor = Color.Gray;  
            tb_thanhtien.Text = "Số tiền cần thanh toán";
            tb_thanhtien.ForeColor = Color.Gray;
            demClick = 0;
            click1 = true;
        }
    }
}
