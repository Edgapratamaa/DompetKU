using kantong.Model;
using System;

namespace kantong.Controller
{
    public class PengeluaranController
    {
        private readonly PengeluaranModel _model = new PengeluaranModel();

        public bool SimpanPengeluaran(decimal jumlah, string kategori, string deskripsi, DateTime tanggal)
        {
            bool berhasil = _model.TambahPengeluaran(jumlah, kategori, deskripsi, tanggal);
            if (berhasil)
            {
                _model.UpdateSaldo();
            }
            return berhasil;
        }
    }
}
