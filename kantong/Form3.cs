using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace kantong
{
    public partial class FormPendapatan : Form
    {
        private string connectionString = "Server=localhost;Database=dompet_keuangan;Uid=root;Pwd=;";
        private Form1 _form1;

        public FormPendapatan(Form1 form1)
        {
            InitializeComponent();

            _form1 = form1;
            LoadKategori();
            this.Text = "Tambah Pendapatan";
            button1.Text = "Simpan Pendapatan";
            label1.Text = "Jumlah Pendapatan";
        }

        private void LoadKategori()
        {
            comboBox1.Items.AddRange(new string[] {
                "Gaji",
                "Bonus",
                "Investasi",
                "Hadiah",
                "Lain-lain"
            });
            comboBox1.SelectedIndex = 0;
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

                    string query = "INSERT INTO pendapatan (jumlah, kategori, deskripsi, tanggal) " +
                                  "VALUES (@jumlah, @kategori, @deskripsi, @tanggal)";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@jumlah", jumlah);
                        command.Parameters.AddWithValue("@kategori", comboBox1.SelectedItem.ToString());
                        command.Parameters.AddWithValue("@deskripsi", textBox2.Text);
                        command.Parameters.AddWithValue("@tanggal", dateTimePicker1.Value);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Data pendapatan berhasil disimpan!");
                            UpdateSaldo();
                            _form1.RefreshData();
                            _form1.Show();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Gagal menyimpan data.");
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
            _form1.Show();
            this.Close();
        }
    }
}