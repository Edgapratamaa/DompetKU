using Microsoft.AspNetCore.Mvc;
using DompetKU.Models;
using Microsoft.EntityFrameworkCore;

namespace DompetKU.Controllers
{
    public class LaporanController : Controller
    {
        private readonly AppDbContext _db;

        public LaporanController(AppDbContext db) => _db = db;

        public async Task<IActionResult> Index()
        {
            var totalIncome = await _db.Transactions
                .Where(t => t.Type == "Income")
                .SumAsync(t => (decimal?)t.Amount) ?? 0;

            var totalExpense = await _db.Transactions
                .Where(t => t.Type == "Expense")
                .SumAsync(t => (decimal?)t.Amount) ?? 0;

            ViewBag.TotalIncome = totalIncome;
            ViewBag.TotalExpense = totalExpense;
            ViewBag.Balance = totalIncome - totalExpense;

            return View(); // Pastikan return View() tanpa parameter
        }
    }
}