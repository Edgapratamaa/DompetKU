using System;
using System.Windows.Forms;

namespace kantong
{
    public partial class FormPendapatan : Form
    {
        private Form1 _form1;
        private Pendapatan _repository;

        public FormPendapatan(Form1 form1)
        {
            InitializeComponent();

            _form1 = form1;
            _repository = new Pendapatan();

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

            bool berhasil = _repository.TambahPendapatan(
                jumlah,
                comboBox1.SelectedItem.ToString(),
                textBox2.Text,
                dateTimePicker1.Value
            );

            if (berhasil)
            {
                MessageBox.Show("Data pendapatan berhasil disimpan!");
                _repository.UpdateSaldo();
                _form1.RefreshData();
                _form1.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Gagal menyimpan data.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _form1.Show();
            this.Close();
        }
    }
}
