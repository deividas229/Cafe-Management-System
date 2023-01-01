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
    public partial class UserForm : Form
    {
        public UserForm()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\deivi\OneDrive\Documents\Cafedb.mdf;Integrated Security=True;Connect Timeout=30");
        void populate()
        {
            Con.Open();
            string query = "select * from UsersTb1";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            UsersGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void label4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1();
            form1.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            UserOrder userOrder = new UserOrder();
            userOrder.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            ItemForm itemForm = new ItemForm();
            itemForm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (UnameTb.Text == "" || UpasswordTb.Text == "" || UphoneTB.Text == "")
            {
                MessageBox.Show("Fill All The fields");
            }
            else
            {
                Con.Open();
                string query = "insert into UsersTb1 values('" + UnameTb.Text + "','" + UphoneTB.Text + "','" + UpasswordTb.Text + "')";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("User Successfully Created");
                Con.Close();
                populate();
            }
        }

        private void UserForm_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void UsersGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)  //to disable the row and column headers
            {
                UnameTb.Text = UsersGV.Rows[e.RowIndex].Cells[0].Value.ToString();
                UphoneTB.Text = UsersGV.Rows[e.RowIndex].Cells[1].Value.ToString();
                UpasswordTb.Text = UsersGV.Rows[e.RowIndex].Cells[2].Value.ToString();
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if(UphoneTB.Text == "")
            {
                MessageBox.Show("Select The User to be Deleted");
            }
            else
            {
                Con.Open();
                string query = "delete from UsersTb1 where Uphone = '"+UphoneTB.Text+"'";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("User successfully Deleted");
                Con.Close();
                populate();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (UpasswordTb.Text == "" || UphoneTB.Text == "" || UnameTb.Text == "")
            {
                MessageBox.Show("Fill All The fields");
            }
            else
            {
                Con.Open();
                string query = "update UsersTb1 set Uname='"+UnameTb.Text+"',Upassword='"+UpasswordTb.Text+"' where Uphone ='"+UphoneTB.Text+"'";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("User Successfully Updated");
                Con.Close();
                populate();
            }
        }
    }
}
