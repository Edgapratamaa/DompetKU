using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace kantong
{
    public partial class Form2 : Form
    {
        private Form1 _form1;
        private Form2()
        {
            throw new InvalidOperationException("Gunakan constructor Form2(Form1 form1)");
        }
        // Koneksi database - sesuaikan dengan setting Anda
        private string connectionString = "Server=localhost;Database=dompet_keuangan;Uid=root;Pwd=;";

        public Form2(Form1 form1)
        {
            InitializeComponent();
            _form1 = form1;
            LoadKategori();// Memuat kategori saat form dimulai
        }
        private void LoadKategori()
        {
            // Isi combobox dengan kategori pengeluaran
            comboBox1.Items.AddRange(new string[] {
                "Makanan",
                "Transportasi",
                "Belanja",
                "Hiburan",
                "Kesehatan",
                "Pendidikan",
                "Lain-lain"
            });
            comboBox1.SelectedIndex = 0; // Pilih kategori pertama sebagai default
        }




        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!decimal.TryParse(textBox1.Text, out decimal jumlah) || jumlah <= 0)
            {
                MessageBox.Show("Masukkan jumlah yang valid!");
                return;
            }

            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Deskripsi tidak boleh kosong!");
                return;
            }

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "INSERT INTO pengeluaran (jumlah, kategori, deskripsi, tanggal) " +
                                  "VALUES (@jumlah, @kategori, @deskripsi, @tanggal)";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@jumlah", jumlah);
                        command.Parameters.AddWithValue("@kategori", comboBox1.SelectedItem?.ToString() ?? "Lain-lain");
                        command.Parameters.AddWithValue("@deskripsi", textBox2.Text);
                        command.Parameters.AddWithValue("@tanggal", dateTimePicker1.Value);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Data pengeluaran berhasil disimpan!");
                            UpdateSaldo();
                            _form1.RefreshData();
                            _form1.Show();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Data gagal disimpan.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void UpdateSaldo()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"UPDATE saldo 
                    SET 
                        total_pendapatan = COALESCE((SELECT SUM(jumlah) FROM pendapatan), 0),
                        total_pengeluaran = COALESCE((SELECT SUM(jumlah) FROM pengeluaran), 0),
                        saldo_akhir = COALESCE((SELECT SUM(jumlah) FROM pendapatan), 0) - 
                                     COALESCE((SELECT SUM(jumlah) FROM pengeluaran), 0),
                        tanggal_update = NOW()
                    WHERE id = 1";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating saldo: " + ex.Message);
            }
        }
       

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}
