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

namespace PAS_Apotek
{
    public partial class Dashboard : Form
    {
        public string Username;
        public string Password;
        public Dashboard()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-I47PK8E\\MSSQLSERVER01;Initial Catalog=Medicine_shop;Integrated Security=True");


        private void Dashboard_Load(object sender, EventArgs e)
        {
            label2.Text = Username;
            if (Username != "Admin")
            {
                button3.Visible = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("are you sure you want to leave (this will also delete everything in your cart)?", "Leaving?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Cart", conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                Application.Exit();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("are you sure you want to log out (this will also delete everything in your cart)?", "Leaving?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Cart", conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                Form1 f1 = new Form1();
                f1.Show();
                this.Hide();
            }
        }

        private void button3_MouseHover(object sender, EventArgs e)
        {
            button3.ForeColor = Color.White;
            button3.BackColor = Color.Black;
        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {
            button3.ForeColor = Color.Black; // or the original text color
            button3.BackColor = Color.FromArgb(128, 255, 128); // or the original background color
        }

        private void button1_MouseHover(object sender, EventArgs e)
        {
            button1.ForeColor = Color.White;
            button1.BackColor = Color.Red;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.ForeColor = Color.Black;
            button1.BackColor = Color.FromArgb(128, 255, 128);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2
            {
                Username = Username,
                Password = Password
            };
            f2.Show();
            this.Hide();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Edit Edit = new Edit
            {
                Username = Username,
                Password = Password
            };
            Edit.Show();
            this.Hide();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Checkout f3 = new Checkout
            {
                Username = Username,
                Password = Password
            };
            f3.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
