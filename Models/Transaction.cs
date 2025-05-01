using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DompetKU.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Tanggal wajib diisi")]
        public DateTime Date { get; set; } = DateTime.Now;
        
        [Required(ErrorMessage = "Kategori wajib diisi")]
        public string Category { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Jumlah wajib diisi")]
        [Range(1, double.MaxValue, ErrorMessage = "Jumlah harus lebih dari 0")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Amount { get; set; }
        
        [Required(ErrorMessage = "Keterangan wajib diisi")]
        public string Description { get; set; } = string.Empty;
        
        [Required]
        public string Type { get; set; } = string.Empty; // "Income" atau "Expense"
    }
}