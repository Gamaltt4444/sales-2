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
    public partial class OrderForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=aserzamzam12;Integrated Security=True");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;

        public OrderForm()
        {
            InitializeComponent();
            LoadOrder();
        }

        public void LoadOrder()
        {

            double total = 0;
            double tota2 = 0;
            //double total5 = 0;



            int i = 0;
            dgvOrder.Rows.Clear();
            cm = new SqlCommand("SELECT iddd, odate, O.pid, P.pname, O.cid, C.cname, qty, price, total  FROM tbOrder AS O JOIN tbCustomer AS C ON O.cid=C.cid JOIN tbProduct AS P ON O.pid=P.pid WHERE odate between @fd and @sd ", con);
            cm.Parameters.AddWithValue("@fd", dateTimePicker1.Value.Date);
            cm.Parameters.AddWithValue("@sd", dateTimePicker2.Value.Date);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvOrder.Rows.Add(i, dr[0].ToString(), Convert.ToDateTime(dr[1].ToString()).ToString("dd/MM/yyyy"), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString(), dr[7].ToString(), dr[8].ToString());
                total += Convert.ToInt32(dr[8].ToString());
                tota2 += Convert.ToInt32(dr[6].ToString());
                //double total3 += Convert.ToInt32(dr[8].ToString());
                //double total4 += Convert.ToInt32(dr[6].ToString());
                //total5 = total3 - total4;


            }
            dr.Close();
            con.Close();
            //double a = double.Parse(label7.Text);
            //double b = double.Parse(label9.Text);
            //double c = a - b;
            label8.Text = i.ToString();
            label7.Text = total.ToString();
            label9.Text = tota2.ToString();
            //label10.Text = total5.ToString();


            double total6 = 0;
            int n = 0;
            dataGridView2.Rows.Clear();
            cm = new SqlCommand("SELECT date,price,dec FROM nas where date between @fdn and @sdn", con);
            cm.Parameters.AddWithValue("@fdn", dateTimePicker1.Value.Date);
            cm.Parameters.AddWithValue("@sdn", dateTimePicker2.Value.Date);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                n++;
                dataGridView2.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString());
                total6 += Convert.ToInt32(dr[1].ToString());
            }
            dr.Close();
            con.Close();
            label4.Text = total6.ToString();
            double total5 = total - total6;
            label10.Text = total5.ToString();



        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            OrderModuleForm moduleForm = new OrderModuleForm();            
            moduleForm.ShowDialog();
            LoadOrder();
        }

        private void dgvUser_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvOrder.Columns[e.ColumnIndex].Name;
            
            if (colName == "Delete")
            {
                if (MessageBox.Show("هل انت متاكد من حذف هذا الاوردر؟", "جاري الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    cm = new SqlCommand("DELETE FROM tbOrder WHERE iddd LIKE '" + dgvOrder.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", con);
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("!تم الحذف بنجاح");

                    cm = new SqlCommand("UPDATE tbProduct SET pqty=(pqty+@pqty) WHERE pid LIKE '" + dgvOrder.Rows[e.RowIndex].Cells[3].Value.ToString() + "' ", con);
                    cm.Parameters.AddWithValue("@pqty", Convert.ToInt16(dgvOrder.Rows[e.RowIndex].Cells[5].Value.ToString()));

                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();

                }
            }
            LoadOrder();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadOrder();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            double total = 0;
            double tota2 = 0;
            //double total5 = 0;



            int i = 0;
            dgvOrder.Rows.Clear();
            cm = new SqlCommand("SELECT iddd, odate, O.pid, P.pname, O.cid, C.cname, qty, price, total  FROM tbOrder AS O JOIN tbCustomer AS C ON O.cid=C.cid JOIN tbProduct AS P ON O.pid=P.pid WHERE odate between @fd and @sd ", con);
            cm.Parameters.AddWithValue("@fd", dateTimePicker1.Value.Date);
            cm.Parameters.AddWithValue("@sd", dateTimePicker2.Value.Date);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvOrder.Rows.Add(i, dr[0].ToString(), Convert.ToDateTime(dr[1].ToString()).ToString("dd/MM/yyyy"), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString(), dr[7].ToString(), dr[8].ToString());
                total += Convert.ToInt32(dr[8].ToString());
                tota2 += Convert.ToInt32(dr[6].ToString());
                //double total3 += Convert.ToInt32(dr[8].ToString());
                //double total4 += Convert.ToInt32(dr[6].ToString());
                //total5 = total3 - total4;


            }
            dr.Close();
            con.Close();
            //double a = double.Parse(label7.Text);
            //double b = double.Parse(label9.Text);
            //double c = a - b;
            label8.Text = i.ToString();
            label7.Text = total.ToString();
            label9.Text = tota2.ToString();
            //label10.Text = total5.ToString();


            double total6 = 0;
            int n = 0;
            dataGridView2.Rows.Clear();
            cm = new SqlCommand("SELECT date,price,dec FROM nas where date between @fdn and @sdn", con);
            cm.Parameters.AddWithValue("@fdn", dateTimePicker1.Value.Date);
            cm.Parameters.AddWithValue("@sdn", dateTimePicker2.Value.Date);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                n++;
                dataGridView2.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString());
                total6 += Convert.ToInt32(dr[1].ToString());
            }
            dr.Close();
            con.Close();
            label4.Text = total6.ToString();
            double total5 = total - total6;
            label10.Text = total5.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            double total = 0;
            double tota2 = 0;
            //double total5 = 0;
           

           
            int i = 0;
            dgvOrder.Rows.Clear();
            cm = new SqlCommand("SELECT iddd, odate, O.pid, P.pname, O.cid, C.cname, qty, price, total  FROM tbOrder AS O JOIN tbCustomer AS C ON O.cid=C.cid JOIN tbProduct AS P ON O.pid=P.pid WHERE CONCAT (orderid, odate, O.pid, P.pname, O.cid, C.cname, qty, price) LIKE '%" + txtSearch.Text + "%'", con);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvOrder.Rows.Add(i, dr[0].ToString(),Convert.ToDateTime(dr[1].ToString()).ToString("dd/MM/yyyy"), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString(), dr[7].ToString(), dr[8].ToString());
                total += Convert.ToInt32(dr[8].ToString());
                tota2 += Convert.ToInt32(dr[6].ToString());
                //double total3 += Convert.ToInt32(dr[8].ToString());
                //double total4 += Convert.ToInt32(dr[6].ToString());
                 //total5 = total3 - total4;
                
               
            }
            dr.Close();
            con.Close();
            //double a = double.Parse(label7.Text);
            //double b = double.Parse(label9.Text);
            //double c = a - b;
            label8.Text = i.ToString();
            label7.Text = total.ToString();
            label9.Text = tota2.ToString();
            //label10.Text = total5.ToString();


            double total6 = 0;
            int n = 0;
            dataGridView2.Rows.Clear();
            cm = new SqlCommand("SELECT date,price,dec FROM nas", con);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                n++;
                dataGridView2.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString());
                total6+= Convert.ToInt32(dr[1].ToString());
            }
            dr.Close();
            con.Close();
            label4.Text = total6.ToString();
            double total5 = total - total6;
            label10.Text = total5.ToString();
            dataGridView2.Rows.Clear();
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void dgvOrder_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvOrder.Columns[e.ColumnIndex].Name;

            if (colName == "Delete")
            {
                if (MessageBox.Show("هل انت متاكد من حذف هذا الاوردر؟", "جاري الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    cm = new SqlCommand("DELETE FROM tbOrder WHERE iddd LIKE '" + dgvOrder.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", con);
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("!تم الحذف بنجاح");

                    cm = new SqlCommand("UPDATE tbProduct SET pqty=(pqty+@pqty) WHERE pid LIKE '" + dgvOrder.Rows[e.RowIndex].Cells[3].Value.ToString() + "' ", con);
                    cm.Parameters.AddWithValue("@pqty", Convert.ToInt16(dgvOrder.Rows[e.RowIndex].Cells[5].Value.ToString()));

                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();

                }
            }
            LoadOrder();
        }
    }
}
