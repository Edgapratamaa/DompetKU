using kantong.Model;
using System;

namespace kantong.Controller
{
    public class PendapatanController
    {
        private readonly PendapatanModel _model = new PendapatanModel();

        public bool SimpanPendapatan(decimal jumlah, string kategori, string deskripsi, DateTime tanggal)
        {
            bool berhasil = _model.TambahPendapatan(jumlah, kategori, deskripsi, tanggal);
            if (berhasil)
            {
                _model.UpdateSaldo();
            }
            return berhasil;
        }
    }
}
