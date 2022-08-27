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
    public partial class allorders : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=aserzamzam12;Integrated Security=True");
        SqlCommand cm = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        SqlDataAdapter da1 = new SqlDataAdapter();
        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();
        public allorders()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dt.Clear();
            da = new SqlDataAdapter("select * from tbOrder", con);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            dt1.Clear();
            da1 = new SqlDataAdapter("select * from nas", con);
            da1.Fill(dt1);
            dataGridView2.DataSource = dt1;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            dt.Clear();
            da = new SqlDataAdapter(" select * from tbOrder where odate between @fd and @sd", con);
            da.SelectCommand.Parameters.AddWithValue("@fd", dateTimePicker1.Value.Date);
            da.SelectCommand.Parameters.AddWithValue("@sd", dateTimePicker2.Value.Date);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            dt1.Clear();
            da1 = new SqlDataAdapter(" select * from nas where date between @fdn and @sdn", con);
            da1.SelectCommand.Parameters.AddWithValue("@fdn", dateTimePicker1.Value.Date);
            da1.SelectCommand.Parameters.AddWithValue("@sdn", dateTimePicker2.Value.Date);

            da1.Fill(dt1);
            dataGridView2.DataSource = dt1;
           
        }
    }
}
