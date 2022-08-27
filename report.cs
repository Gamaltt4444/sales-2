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
    public partial class report : Form
    {

        public report()
        {
            InitializeComponent();
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            OrderForm ord = new OrderForm();
            ord.Show();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            CustomerForm ns =new CustomerForm();
            ns.Show();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OrderForm ns =new  OrderForm();
            ns.Show();
        } 
        }

        
    }

