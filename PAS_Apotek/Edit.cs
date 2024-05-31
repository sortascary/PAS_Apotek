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
    public partial class Edit : Form
    {
        public string Username;
        public string Password;

        int count;
        bool isLoaded = false;

        public Edit()
        {
            InitializeComponent();
            bind_data();
        }

        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-I47PK8E\\MSSQLSERVER01;Initial Catalog=Medicine_shop;Integrated Security=True");

        private void bind_data()
        {
            SqlCommand cmd1 = new SqlCommand("select id,Medicine As Medicine, Price As Price from Shop", conn);
            SqlDataAdapter da1 = new SqlDataAdapter();
            da1.SelectCommand = cmd1;
            DataTable dt1 = new DataTable();
            dt1.Clear();
            da1.Fill(dt1);
            dataGridView1.DataSource = dt1;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 11, FontStyle.Bold);
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label3_Click_1(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (comboBox2.Text == "ID")
            {
                SqlCommand cmd1 = new SqlCommand("select id, Medicine As Medicine,Price As Price from Shop where id Like @id+'%'", conn);
                cmd1.Parameters.AddWithValue("id", textBox4.Text);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd1;
                DataTable dt = new DataTable();
                dt.Clear();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            else if (comboBox2.Text == "Name")
            {
                SqlCommand cmd1 = new SqlCommand("select id,Medicine As Medicine,Price As Price from Shop where Medicine Like @Medicine+'%'", conn);
                cmd1.Parameters.AddWithValue("Medicine", textBox4.Text);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd1;
                DataTable dt = new DataTable();
                dt.Clear();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Animtimer.Enabled = false;
            Dashboard Dash = new Dashboard
            {
                Username = Username,
                Password = Password
            };
            Dash.Show();
            this.Hide();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (textBox1.Text != "" || textBox2.Text != "" || textBox5.Text != "")
            {
                DialogResult dialogResult = MessageBox.Show("are you sure you want to Add?", "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Yes)
                {
                    SqlCommand cmd2 = new SqlCommand("Insert into Shop(id,Medicine,Price)Values(@id,@Medicine,@Price)", conn);
                    cmd2.Parameters.AddWithValue("id", textBox5.Text);
                    cmd2.Parameters.AddWithValue("Medicine", textBox1.Text);
                    cmd2.Parameters.AddWithValue("Price", textBox2.Text);
                    conn.Open();
                    cmd2.ExecuteNonQuery();
                    conn.Close();
                    bind_data();

                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox5.Text = "";
                }
            } else
            {
                MessageBox.Show("please input the Data", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox3.Text))
            {
                MessageBox.Show("Please Input No ID You Want To Delete");
                return;
            }

            DialogResult dialogResult = MessageBox.Show("are you sure you want to Delete?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Shop WHERE id = @id", conn);
                cmd.Parameters.AddWithValue("@id", textBox3.Text);

                MessageBox.Show("Successfully Deleted");
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                bind_data();
                textBox3.Text = "";
            }
        }

        private void comboBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void Animtimer_Tick(object sender, EventArgs e)
        {

            count++;

            if (count <= 75)
            {
                pictureBox1.Top += 10;
            }
            else
            {
                pictureBox1.Top -= 750;
                count = 0; // Reset count for the next loop
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void Edit_Load(object sender, EventArgs e)
        {
            Animtimer.Enabled = true;
            isLoaded = true;
        }

        private void Edit_Enter(object sender, EventArgs e)
        {
            
        }

        private void Edit_FormClosing(object sender, FormClosingEventArgs e)
        {
            isLoaded = false; // Set to false to stop the animation loop
            Animtimer.Enabled = false; // Optionally disable the timer if desired
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("are you sure you want to Update?", "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                SqlCommand cmd3 = new SqlCommand("Update Shop Set Medicine=@Medicine,Price=@Price where id=@id", conn);
                cmd3.Parameters.AddWithValue("id", textBox5.Text);
                cmd3.Parameters.AddWithValue("Medicine", textBox1.Text);
                cmd3.Parameters.AddWithValue("Price", textBox2.Text);
                conn.Open();
                cmd3.ExecuteNonQuery();
                conn.Close();
                bind_data();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
                textBox1.Text = selectedRow.Cells["Medicine"].Value.ToString();
                textBox2.Text = selectedRow.Cells["Price"].Value.ToString();
                textBox5.Text = selectedRow.Cells["id"].Value.ToString();
            }
        }
    }
}
