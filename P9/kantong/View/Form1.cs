using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient; // Pastikan telah menginstall package MySql.Data via NuGet

namespace kantong
{
    public partial class Form1 : Form
    {
        private string connectionString = "Server=localhost;Database=dompet_keuangan;Uid=root;Pwd=;";

        public Form1()
        {
            InitializeComponent();
            LoadData();
        }



        private void LoadData()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    // Query untuk mengambil data saldo
                    string query = "SELECT saldo_akhir, total_pengeluaran, total_pendapatan FROM saldo WHERE id = 1";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Menampilkan data ke textbox
                                textBox1.Text = reader["saldo_akhir"].ToString();
                                textBox3.Text = reader["total_pengeluaran"].ToString();
                                textBox2.Text = reader["total_pendapatan"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            // Kosongkan jika tidak digunakan
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Data sudah dimuat di constructor
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Kosongkan jika tidak digunakan
            Form2 formPengeluaran = new Form2(this);
            // <-- ganti constructor agar bawa referensi Form1
            formPengeluaran.Show();
            this.Hide();


        }

        private void label5_Click(object sender, EventArgs e)
        {
            // Kosongkan jika tidak digunakan
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            // Kosongkan jika tidak digunakan
        }

        public void RefreshData()
        {
            LoadData(); // gunakan ini setelah simpan pendapatan/pengeluaran
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Kosongkan jika tidak digunakan
            FormPendapatan formPendapatan = new FormPendapatan(this); // <-- bawa referensi Form1
            formPendapatan.Show();
            this.Hide();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // Kosongkan jika tidak digunakan
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            // Kosongkan jika tidak digunakan
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            // Kosongkan jika tidak digunakan
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kosongkan jika tidak digunakan
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DialogResult result = MessageBox.Show("Apakah Anda yakin ingin keluar?", "Konfirmasi",
                                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit(); // atau Application.ExitThread();
            }
        }
    }
}