using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Cafe_Management_System
{
    public partial class ViewOrders : Form
    {
        public ViewOrders()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\deivi\OneDrive\Documents\Cafedb.mdf;Integrated Security=True;Connect Timeout=30");
        void populate()
        {
            Con.Open();
            string query = "select * from OrdersTb1";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            OrdersGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void ViewOrders_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void OrdersGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

            e.Graphics.DrawString("=====MyCafe SoftWare=====", new Font("Arial", 20, FontStyle.Bold), Brushes.Red, new Point(200, 40));
            e.Graphics.DrawString("=====Order Summary=====", new Font("Arial", 20, FontStyle.Bold), Brushes.Red, new Point(208, 70));
            e.Graphics.DrawString("Number:"+ OrdersGV.Rows[0].Cells[0].Value.ToString(), new Font("Arial", 15, FontStyle.Regular), Brushes.Black, new Point(120, 105));
            e.Graphics.DrawString("Date:" + OrdersGV.Rows[0].Cells[1].Value.ToString(), new Font("Arial", 15, FontStyle.Regular), Brushes.Black, new Point(120, 125));
            e.Graphics.DrawString("Seller:" + OrdersGV.Rows[0].Cells[2].Value.ToString(), new Font("Arial", 15, FontStyle.Regular), Brushes.Black, new Point(120, 145));
            e.Graphics.DrawString("Amount:" + OrdersGV.Rows[0].Cells[3].Value.ToString(), new Font("Arial", 15, FontStyle.Regular), Brushes.Black, new Point(120, 165));
            e.Graphics.DrawString("=====Order Summary=====", new Font("Arial", 20, FontStyle.Bold), Brushes.Red, new Point(208, 340));
        }
    }
}
