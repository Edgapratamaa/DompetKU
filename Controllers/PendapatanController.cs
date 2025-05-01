using Microsoft.AspNetCore.Mvc;
using DompetKU.Models;
using Microsoft.EntityFrameworkCore;

namespace DompetKU.Controllers
{
    public class PendapatanController : Controller
    {
        private readonly AppDbContext _db;

        public PendapatanController(AppDbContext db) => _db = db;

        public async Task<IActionResult> Index()
        {
            var transactions = await _db.Transactions
                .Where(t => t.Type == "Income")
                .OrderByDescending(t => t.Date)
                .ToListAsync();

            ViewBag.Transactions = transactions;
            return View(new Transaction());
        }

        [HttpPost]
        public async Task<IActionResult> Create(Transaction transaction)
        {
            transaction.Type = "Income";
            
            if(ModelState.IsValid)
            {
                _db.Transactions.Add(transaction);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            
            ViewBag.Transactions = await _db.Transactions
                .Where(t => t.Type == "Income")
                .ToListAsync();
                
            return View("Index", transaction);
        }
    }
}