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
    public partial class Checkout : Form
    {
        public string Username;
        public string Password;
        public Checkout()
        {
            InitializeComponent();
            bind_data();
        }

        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-I47PK8E\\MSSQLSERVER01;Initial Catalog=Medicine_shop;Integrated Security=True");

        private void bind_data()
        {
            SqlCommand cmd1 = new SqlCommand("select Medicine As Medicine, Ammount as Ammount from Cart", conn);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd1;
            DataTable dt = new DataTable();
            dt.Clear();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 11, FontStyle.Bold);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("are you sure you want to leave (this will also delete everything in your cart)?", "Leaving?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Cart", conn);
                conn.Open();
                cmd.ExecuteNonQuery();;
                bind_data();
                conn.Close();

                Application.Exit();
            }
        }

        private void Checkout_Load(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Dashboard Dash = new Dashboard
            {
                Username = Username,
                Password = Password
            };
            Dash.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.PrintPreviewControl.Zoom = 1;
            printPreviewDialog1.ShowDialog();
        }
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Bitmap imagebmp = new Bitmap(dataGridView1.Width, dataGridView1.Height);
            dataGridView1.DrawToBitmap(imagebmp, new Rectangle(0, 0, imagebmp.Width, imagebmp.Height));
            e.Graphics.DrawImage(imagebmp, 120, 120);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
           
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
