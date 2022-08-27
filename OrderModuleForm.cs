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
    public partial class OrderModuleForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=aserzamzam12;Integrated Security=True");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        int qty = 0;
        public OrderModuleForm()
        {
            InitializeComponent();
            LoadCustomer();
            LoadProduct();
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
           
        }

        public void LoadCustomer()
        {
            int i = 0;
            dgvCustomer.Rows.Clear();
            cm = new SqlCommand("SELECT cid, cname FROM tbCustomer WHERE CONCAT(cid,cname) LIKE '%"+txtSearchCust.Text+"%'", con);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvCustomer.Rows.Add(i, dr[0].ToString(), dr[1].ToString());
            }
            dr.Close();
            con.Close();


        }

        public void LoadProduct()
        {
            int i = 0;
            dgvProduct.Rows.Clear();
            cm = new SqlCommand("SELECT pid, pname, pprice, pdescription, pcategory FROM tbProduct WHERE CONCAT(pid, pname, pprice, pdescription, pcategory) LIKE '%" + txtSearchProd.Text + "%'", con);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvProduct.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString());
            }
            dr.Close();
            con.Close();
            int n = 0;
            double total = 0;
            dgvOrder.Rows.Clear();
            cm = new SqlCommand("SELECT idd , nam,qty, price, total FROM tbOrder1 ", con);

            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                n++;
                dgvOrder.Rows.Add(n, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(),dr[4].ToString());
                total += Convert.ToInt32(dr[4].ToString());
            }
            dr.Close();
            con.Close();
            label23.Text = total.ToString();
        }

        private void txtSearchCust_TextChanged(object sender, EventArgs e)
        {
            LoadCustomer();
        }

        private void txtSearchProd_TextChanged(object sender, EventArgs e)
        {
            LoadProduct();
        }

        
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            GetQty();
           
            if (Convert.ToInt16(UDQty.Value) > 0)
            {
                int total = Convert.ToInt16(txtPrice.Text) * Convert.ToInt16(UDQty.Value);
                txtTotal.Text = total.ToString();
            }
        }

        private void dgvCustomer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtCId.Text = "1";

                dgvCustomer.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtCName.Text = "زمزم";
                dgvCustomer.Rows[e.RowIndex].Cells[2].Value.ToString();
        }

        private void dgvProduct_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtPid.Text = dgvProduct.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtPName.Text = dgvProduct.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtPrice.Text = dgvProduct.Rows[e.RowIndex].Cells[3].Value.ToString();            
        }

      

        private void btnInsert_Click(object sender, EventArgs e)
        {
            try
            {

                if (label23.Text == "0")
                {
                    MessageBox.Show("من فضلك اختار الصنف او العصير", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (MessageBox.Show("هل انت متاكد من اجراء هذا الطلب؟", "جاري الحفظ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    MessageBox.Show("تم اجراء الاوردر بنجاح");
                    PrintPreviewDialog printPreviewDialog1 = new PrintPreviewDialog();
                    printPreviewDialog1.Document = this.printDocument1;
                    printPreviewDialog1.FormBorderStyle = FormBorderStyle.Fixed3D;
                    //printPreviewDialog1.SetBounds(20, 20, this.Width, this.Height);

                    printPreviewDialog1.ShowDialog();


                    cm = new SqlCommand("DELETE  FROM tbOrder1", con);
                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                    Clear();
                    LoadProduct();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Clear()
        {
            //txtCId.Clear();
            //txtCName.Clear();

            txtPid.Clear();
            txtPName.Clear();

            txtPrice.Clear();
            UDQty.Value = 0;
            txtTotal.Clear();
            dtOrder.Value = DateTime.Now;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();                        
        }

        public void GetQty()
        {
            cm = new SqlCommand("SELECT pqty FROM tbProduct WHERE pid='"+ txtPid.Text +"'", con);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                qty = Convert.ToInt32(dr[0].ToString());
            }
            dr.Close();
            con.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            float yPos = 8;
            int leftMargin = 15;
            // Pen pen = new Pen(Brushes.Black);

            //e.HasMorePages =true;
            // pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            //Font printFont4d = new System.Drawing.Font("Times New Roman", 12, FontStyle.Bold);
            //Font printFont = new System.Drawing.Font("Times New Roman", 14);
            //Font printFont11 = new System.Drawing.Font("Times New Roman", 16);
            //Font printFontFoorer = new System.Drawing.Font("Times New Roman", 12);
            //Font printFontheader_DRName = new System.Drawing.Font("Times New Roman", 20, FontStyle.Bold);
            //------
           

            Font PatientFontheader = new System.Drawing.Font("Times New Roman", 30, FontStyle.Bold);
            Font PatientFontheader1 = new System.Drawing.Font("Times New Roman",10, FontStyle.Bold);

            Font PatientNormal = new System.Drawing.Font("Arial", 8, FontStyle.Bold);
            //======================================                      
            string nam, price, qut, tot;
            //yPos += 152;
            e.Graphics.DrawString("عصير زمزم", PatientFontheader, Brushes.Black, leftMargin + 320 ,30, new StringFormat());
            e.Graphics.DrawString(label7.Text, PatientFontheader1, Brushes.Black, leftMargin + 350, 80, new StringFormat());
            e.Graphics.DrawString("اسم الصنف", PatientFontheader1, Brushes.Black, leftMargin + 290, 100, new StringFormat());
            //e.Graphics.DrawString(txtPName.Text, PatientFontheader1, Brushes.Black, leftMargin + 250, 100, new StringFormat());

            e.Graphics.DrawString("سعر الكوب", PatientFontheader1, Brushes.Black, leftMargin + 410,100 , new StringFormat());
           // e.Graphics.DrawString(txtPrice.Text, PatientFontheader1, Brushes.Black, leftMargin + 350,150 , new StringFormat());

            e.Graphics.DrawString("الكميه", PatientFontheader1, Brushes.Black, leftMargin + 470, 100, new StringFormat());
            e.Graphics.DrawString("الاجمالي", PatientFontheader1, Brushes.Black, leftMargin + 510, 100, new StringFormat());
            
            //e.Graphics.DrawString(UDQty.Text, PatientFontheader1, Brushes.Black, leftMargin + 350, 200, new StringFormat());
            yPos += 120;
            SizeF stringSize = new SizeF();
            for (int i = 0; i < dgvOrder.Rows.Count; i++)
            {
                nam  = dgvOrder.Rows[i].Cells[2].Value.ToString();
                price = dgvOrder.Rows[i].Cells[4].Value.ToString();
                qut = dgvOrder.Rows[i].Cells[3].Value.ToString();
                tot = dgvOrder.Rows[i].Cells[5].Value.ToString();
                e.Graphics.DrawString(nam.ToUpper(), PatientNormal, Brushes.Black, leftMargin +270, yPos, new StringFormat());
                yPos += 1;
                tot =  "          " + price + "            " + qut +"         " + tot ;
                stringSize = e.Graphics.MeasureString(tot, PatientNormal);
                e.Graphics.DrawString(tot, PatientNormal, Brushes.Black, 550 - stringSize.Width, yPos);
                yPos += 20;
            }
            yPos += 10;
            e.Graphics.DrawString(label7.Text, PatientFontheader1, Brushes.Black, leftMargin + 350, yPos, new StringFormat());
            yPos += 30;
            e.Graphics.DrawString(":المجموع", PatientFontheader1, Brushes.Black, leftMargin + 400, yPos, new StringFormat());
            e.Graphics.DrawString(label23.Text, PatientFontheader1, Brushes.Black, leftMargin + 350, yPos, new StringFormat());

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

        }

        private void btnUpdate_Click_1(object sender, EventArgs e)
        {
            PrintPreviewDialog printPreviewDialog1 = new PrintPreviewDialog();
            printPreviewDialog1.Document = this.printDocument1;
            printPreviewDialog1.FormBorderStyle = FormBorderStyle.Fixed3D;
            //printPreviewDialog1.SetBounds(20, 20, this.Width, this.Height);

            printPreviewDialog1.ShowDialog();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {

                if (txtPid.Text == "")
                {
                    MessageBox.Show("من فضلك اختار الصنف او العصير", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (MessageBox.Show("هل انت متاكد من اضافه العنصر؟", "جاري الحفظ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    cm = new SqlCommand("INSERT INTO tbOrder(odate, pid, cid, qty, price, total)VALUES(@odate, @pid, @cid, @qty, @price, @total)", con);
                    cm.Parameters.AddWithValue("@odate", dtOrder.Value);
                    cm.Parameters.AddWithValue("@pid", Convert.ToInt32(txtPid.Text));
                    cm.Parameters.AddWithValue("@cid", Convert.ToInt32(txtCId.Text));
                    cm.Parameters.AddWithValue("@qty", Convert.ToInt32(UDQty.Value));
                    cm.Parameters.AddWithValue("@price", Convert.ToInt32(txtPrice.Text));
                    cm.Parameters.AddWithValue("@total", Convert.ToInt32(txtTotal.Text));
                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();

                    cm = new SqlCommand("INSERT INTO tbOrder1(nam,qty, price, total)VALUES(@nam, @qty, @price, @total)", con);
                    cm.Parameters.AddWithValue("@nam", txtPName.Text);
                    cm.Parameters.AddWithValue("@qty", Convert.ToInt32(UDQty.Value));
                    cm.Parameters.AddWithValue("@price", Convert.ToInt32(txtPrice.Text));
                    cm.Parameters.AddWithValue("@total", Convert.ToInt32(txtTotal.Text));
                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();

                    cm = new SqlCommand("UPDATE tbProduct SET pqty=(pqty-@pqty) WHERE pid LIKE '" + txtPid.Text + "' ", con);
                    cm.Parameters.AddWithValue("@pqty", Convert.ToInt16(UDQty.Value));

                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                    Clear();
                    LoadProduct();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("هل انت متاكد من حذف المنتجات  ؟", "جاري الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                con.Open();
                cm = new SqlCommand("DELETE * FROM tbOrder1", con);
                cm.ExecuteNonQuery();
                con.Close();
                

            }
        }

        private void dgvOrder_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
             string colName = dgvOrder.Columns[e.ColumnIndex].Name;

            if (colName == "Delete")
            {
                
                if (MessageBox.Show("هل انت متاكد من حذف هذا الاوردر؟", "جاري الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    label14.Text = dgvOrder.Rows[e.RowIndex].Cells[1].Value.ToString();
                    cm = new SqlCommand("DELETE  FROM tbOrder WHERE iddd LIKE '" + label14.Text + "'", con);
                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close(); 
                    con.Open();
                    cm = new SqlCommand("DELETE FROM tbOrder1 where idd  LIKE '" + dgvOrder.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", con);

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
            LoadProduct();
        }

        private void dgvProduct_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
