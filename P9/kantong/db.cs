using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace kantong
{
    public class Pendapatan
    {
        private readonly string connectionString = "Server=localhost;Database=dompet_keuangan;Uid=root;Pwd=;";

        public bool TambahPendapatan(decimal jumlah, string kategori, string deskripsi, DateTime tanggal)
        {
            string query = "INSERT INTO pendapatan (jumlah, kategori, deskripsi, tanggal) " +
                           "VALUES (@jumlah, @kategori, @deskripsi, @tanggal)";

            var parameters = new Dictionary<string, object>
            {
                {"@jumlah", jumlah},
                {"@kategori", kategori},
                {"@deskripsi", deskripsi},
                {"@tanggal", tanggal}
            };

            return ExecuteQuery(query, parameters);
        }

        public bool UpdateSaldo()
        {
            string query = @"UPDATE saldo 
                SET 
                    total_pendapatan = COALESCE((SELECT SUM(jumlah) FROM pendapatan), 0),
                    total_pengeluaran = COALESCE((SELECT SUM(jumlah) FROM pengeluaran), 0),
                    saldo_akhir = COALESCE((SELECT SUM(jumlah) FROM pendapatan), 0) - 
                                 COALESCE((SELECT SUM(jumlah) FROM pengeluaran), 0),
                    tanggal_update = NOW()
                WHERE id = 1";

            return ExecuteQuery(query, new Dictionary<string, object>());
        }

        private bool ExecuteQuery(string query, Dictionary<string, object> parameters)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        foreach (var param in parameters)
                        {
                            command.Parameters.AddWithValue(param.Key, param.Value);
                        }
                        command.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Error: " + ex.Message);
                return false;
            }
        }
    }
}
