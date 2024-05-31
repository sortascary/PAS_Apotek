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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PAS_Apotek
{
    public partial class Form2 : Form
    {
        private PictureBox[] pictureBoxes;
        private bool[] isExpanding;

        public string Username;
        public string Password;
        public Form2()
        {
            InitializeComponent();
            bind_data();

            pictureBoxes = new PictureBox[] { pictureBox2, pictureBox3, pictureBox6, pictureBox7, pictureBox1, pictureBox5, pictureBox4 };

            isExpanding = new bool[pictureBoxes.Length];
            for (int i = 0; i < isExpanding.Length; i++)
            {
                isExpanding[i] = false;
            }
            
        }

        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-I47PK8E\\MSSQLSERVER01;Initial Catalog=Medicine_shop;Integrated Security=True");


        private void bind_data()
        {
            // First query
            SqlCommand cmd1 = new SqlCommand("select id, Medicine As Medicine, Price As Price from Shop", conn);
            SqlDataAdapter da1 = new SqlDataAdapter();
            da1.SelectCommand = cmd1;
            DataTable dt1 = new DataTable();
            dt1.Clear();
            da1.Fill(dt1);
            dataGridView1.DataSource = dt1;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 11, FontStyle.Bold);

            // Second query
            SqlCommand cmd2 = new SqlCommand("select Medicine As Medicine, Ammount as Ammount from Cart", conn);
            SqlDataAdapter da2 = new SqlDataAdapter();
            da2.SelectCommand = cmd2;
            DataTable dt2 = new DataTable();
            dt2.Clear();
            da2.Fill(dt2);
            dataGridView2.DataSource = dt2;
            dataGridView2.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 11, FontStyle.Bold);
        }



        private void timer1_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < pictureBoxes.Length; i++)
            {
                if (isExpanding[i])
                {
                    if (pictureBoxes[i].Width < 172)
                    {
                        pictureBoxes[i].Width += 40; // Increase width
                        pictureBoxes[i].Left -= 20;  // Move left side to the left               
                    }
                    else
                    {
                        isExpanding[i] = false; // Change direction
                    }
                }
                else
                {
                    if (pictureBoxes[i].Width > 52)
                    {
                        pictureBoxes[i].Width -= 40; // Decrease width
                        pictureBoxes[i].Left += 20;  // Move left side to the right
                    }
                    else
                    {
                        isExpanding[i] = true; // Change direction
                    }
                }
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            Animtime.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "ID")
            {
                SqlCommand cmd1 = new SqlCommand("select id,Medicine As Medicine,Price As Price from Shop where id Like @id+'%'", conn);
                cmd1.Parameters.AddWithValue("id", textBox1.Text);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd1;
                DataTable dt = new DataTable();
                dt.Clear();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            else if (comboBox1.Text == "Name")
            {
                SqlCommand cmd1 = new SqlCommand("select id,Medicine As Medicine,Price As Price from Shop where Medicine Like @Medicine+'%'", conn);
                cmd1.Parameters.AddWithValue("Medicine", textBox1.Text);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd1;
                DataTable dt = new DataTable();
                dt.Clear();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
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
                bind_data();
                conn.Close();

                Application.Exit();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Animtime.Enabled = false;
            Checkout f3 = new Checkout
            {
                Username = Username,
                Password = Password
            };
            f3.Show();
            this.Hide();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (Username == "Admin")
            {
                Animtime.Enabled = false;
                Edit Edit = new Edit
                {
                    Username = Username,
                    Password = Password
                };
                Edit.Show();
                this.Hide();
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("Only the Admin can edit this", "No Access", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Dashboard Dash = new Dashboard {
                Username = Username,
                Password = Password
        };
            Dash.Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox3.Text))
            {
                MessageBox.Show("Please Input No ID You Want To Delete");
                return;
            }

            DialogResult dialogResult = MessageBox.Show("are you sure you want to Delete?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Cart WHERE Medicine = @Medicine", conn);
                cmd.Parameters.AddWithValue("@Medicine", textBox3.Text);

                MessageBox.Show("Successfully Deleted");
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                bind_data();

                textBox3.Text = "";
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(textBox2.Text != "")
            {
                DialogResult dialogResult = MessageBox.Show("are you sure you want to Add?", "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Yes)
                {
                    SqlCommand cmd2 = new SqlCommand("Insert into Cart(Medicine,Ammount)Values(@Medicine,@Ammount)", conn);
                    cmd2.Parameters.AddWithValue("Medicine", textBox2.Text);
                    cmd2.Parameters.AddWithValue("Ammount", numericUpDown1.Text);
                    conn.Open();
                    cmd2.ExecuteNonQuery();
                    conn.Close();
                    bind_data();

                    textBox2.Text = "";
                    numericUpDown1.Value = 1;
                }
                
            }
            else
            {
                MessageBox.Show("please input the correct Data", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
