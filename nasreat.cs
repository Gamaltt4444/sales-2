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
    public partial class nasreat : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=aserzamzam12;Integrated Security=True");
        SqlCommand cm = new SqlCommand();
       
        public nasreat()
        {
            InitializeComponent();
        }

        private void customerButton1_Click(object sender, EventArgs e)
        {
           

        }

        private void customerButton1_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("هل انت متاكد من مسح جميع البيانات؟", "جاري المسح", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    cm = new SqlCommand("DELETE from nas", con);
                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("تم المسح بنجاح");
                    MessageBox.Show("ملحوظه مهم جدا يرجاء التاكد من مسح جميع بيانات الخوارج في نهاية اليوم حتي لا يتم حسبها في اليوم التالي");
                    this.Dispose();

                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
            //MainForm main = new MainForm();
            //main.Show();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("هل انت متاكد من حفظ البيانات ؟  ", "جاري الحفظ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    cm = new SqlCommand("INSERT INTO nas (date,price,dec)VALUES(@date, @price,@dec)", con);
                    cm.Parameters.AddWithValue("@date", dateTimePicker1.Value.Date);
                    cm.Parameters.AddWithValue("@price", txtCPhone.Text);
                    cm.Parameters.AddWithValue("@dec", textBox1.Text);
                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("تم حفظ اليبانات بنجاح ");
                    Clear();
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
        
        public void Clear()
        {
            //txtCName.Clear();
            txtCPhone.Clear();
            textBox1.Clear();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            
            try
            {
                if (MessageBox.Show("هل انت متاكد من تعديل البيانات  ", "جاري التعديل", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cm = new SqlCommand("update nas set price =@price,dec=@dec where nid like '" + lblCId.Text + "'  ", con);
                    //cm.Parameters.AddWithValue("@date", dateTimePicker1.Value.Date);
                    cm.Parameters.AddWithValue("@price", txtCPhone.Text);
                    cm.Parameters.AddWithValue("@dec", textBox1.Text);
                   
                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("تم تعديل اليبانات بنجاح ");
                    Clear();
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.Dispose();
            
            //allnaser nas = new allnaser();
            //nas.Show();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Clear();
            btnSave.Enabled = true;
        
        }

    }
}
