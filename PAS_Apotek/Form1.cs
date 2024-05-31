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
    public partial class Form1 : Form
    {
        public static string textuser;
        public static string textpassword;
        int count = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            count ++;
            if (count <= 16)
            {
                pictureBox2.Left -= 20;
                pictureBox3.Left -= 20;
                pictureBox5.Left += 20;
            }
            else
            {
                pictureBox2.Left -= 20;
                Animationtimer.Enabled = false;
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-I47PK8E\\MSSQLSERVER01;Initial Catalog=loginapp;Integrated Security=True;TrustServerCertificate=True");
            con.Open();
            String query = "SELECT COUNT(*) FROM loginapp WHERE username=@username AND password=@password";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@username", textBox1.Text);
            cmd.Parameters.AddWithValue("@password", textBox2.Text);
            int count = (int)cmd.ExecuteScalar();
            con.Close();
            if (count > 0)
            {
                //MessageBox.Show("login success", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textuser = textBox1.Text;
                textpassword = textBox2.Text;
                Dashboard dash = new Dashboard
                {
                    Username = textuser,
                    Password = textpassword
                };
                dash.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("error login");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("are you sure you want to leave?", "Leaving?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
