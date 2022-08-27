using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventoryManagementSystem
{
    public partial class allnas : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=aserzamzam12;Integrated Security=True");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        public allnas()
        {
            InitializeComponent();
            LoadCustomer();
        }

        public void LoadCustomer()
        {
            double total = 0;
            int i = 0;
            dgvCustomer.Rows.Clear();
            cm = new SqlCommand("SELECT nid, date , price , dec FROM nas", con);
             //cm.Parameters.AddWithValue("@fdn", dateTimePicker1.Value.Date);
             //cm.Parameters.AddWithValue("@sdn", dateTimePicker2.Value.Date);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvCustomer.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString());
                total += Convert.ToInt32(dr[2].ToString());
            }
            dr.Close();
            con.Close();
            label4.Text = total.ToString();
        }



        private void btnAdd_Click_1(object sender, EventArgs e)
        {
            nasreat moduleForm = new  nasreat();
            moduleForm.btnSave.Enabled = true;
            //moduleForm.btnUpdate.Enabled = false;
            moduleForm.ShowDialog();
            LoadCustomer();
        }

        private void dgvCustomer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dgvCustomer_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
             string colName = dgvCustomer.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                nasreat customerModule = new nasreat();
                //customerModule.lblCId.text = dgvCustomer.Rows[e.RowIndex].Cells[1].Value.ToString();
                customerModule.txtCPhone.Text = dgvCustomer.Rows[e.RowIndex].Cells[3].Value.ToString();
                customerModule.textBox1.Text = dgvCustomer.Rows[e.RowIndex].Cells[4].Value.ToString();
                //customerModule.txtCName.Text = dgvCustomer.Rows[e.RowIndex].Cells[0].Value.ToString();
                //customerModule.txtCPhone.Text = dgvCustomer.Rows[e.RowIndex].Cells[1].Value.ToString();

                customerModule.btnSave.Enabled = false;
                customerModule.btnSave.Enabled = true;
                customerModule.ShowDialog();
            }
            else if (colName == "Delete")
            {
                if (MessageBox.Show("هل انت متاكد من حذف البيانات", "جاري الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    cm = new SqlCommand("DELETE FROM nas WHERE nid LIKE '" + dgvCustomer.Rows[e.RowIndex].Cells[1].Value.ToString() +"'",con);
                        //dgvCustomer.Rows[e.RowIndex].Cells[3].Value.ToString() + "'", con);
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("تم الحذف بنجاح");
                   // MessageBox.Show("ملحوظه مهم جدا يرجاء التاكد من مسح جميع بيانات الخوارج في نهاية اليوم حتي لا يتم حسبها في اليوم التالي");
                }
            }

            LoadCustomer();
        }
        

        private void label1_Click(object sender, EventArgs e)
        {

           //nasreat nas = new nasreat();
            //nas.Show();
        }

        private void customerButton1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
           nasreat nas = new nasreat();
            nas.Show();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            double total = 0;
            int i = 0;
            dgvCustomer.Rows.Clear();
            cm = new SqlCommand("SELECT   nid, date , price , dec FROM nas where date between @fdn and @sdn", con);
            cm.Parameters.AddWithValue("@fdn", dateTimePicker1.Value.Date);
            cm.Parameters.AddWithValue("@sdn", dateTimePicker2.Value.Date);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvCustomer.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString());
                total += Convert.ToInt32(dr[2].ToString());
            }
            dr.Close();
            con.Close();
            label4.Text = total.ToString();
        }
        
    }
}
