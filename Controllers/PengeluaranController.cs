using Microsoft.AspNetCore.Mvc;
using DompetKU.Models;
using Microsoft.EntityFrameworkCore;

namespace DompetKU.Controllers
{
    public class PengeluaranController : Controller
    {
        private readonly AppDbContext _db;

        public PengeluaranController(AppDbContext db) => _db = db;

        public async Task<IActionResult> Index()
        {
            var transactions = await _db.Transactions
                .Where(t => t.Type == "Expense")
                .OrderByDescending(t => t.Date)
                .ToListAsync();

            ViewBag.Transactions = transactions; // Kirim data ke ViewBag
            return View(new Transaction()); // Gunakan model Transaction untuk form
        }

        [HttpPost]
        public async Task<IActionResult> Create(Transaction transaction)
        {
            transaction.Type = "Expense";
            
            if (ModelState.IsValid)
            {
                _db.Transactions.Add(transaction);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            
            // Jika validasi gagal, kirim ulang data
            ViewBag.Transactions = await _db.Transactions
                .Where(t => t.Type == "Expense")
                .ToListAsync();
                
            return View("Index", transaction);
        }
    }
}