//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
//using ChealCore.Data;
//using ChealCore.Models;
//using ChealCore.Enums;

//namespace ChealCore.Controllers
//{
//    public class LoanAccountsController : Controller
//    {
//        private readonly ApplicationDbContext _context;

//        public LoanAccountsController(ApplicationDbContext context)
//        {
//            _context = context;
//        }

//        // GET: LoanAccounts
//        public async Task<IActionResult> Index()
//        {
//            var applicationDbContext = _context.LoanAccount.Include(l => l.CustomerAccount).Include(l => l.branch);
//            return View(await applicationDbContext.ToListAsync());
//        }

//        // GET: LoanAccounts/Details/5
//        public async Task<IActionResult> Details(int? id)
//        {
//            if (id == null || _context.LoanAccount == null)
//            {
//                return NotFound();
//            }

//            var loanAccount = await _context.LoanAccount
//                .Include(l => l.CustomerAccount)
//                .Include(l => l.branch)
//                .FirstOrDefaultAsync(m => m.Id == id);
//            if (loanAccount == null)
//            {
//                return NotFound();
//            }

//            return View(loanAccount);
//        }

//        // GET: LoanAccounts/Create
//        public IActionResult Create()
//        {
//            ViewData["CustomerAccountId"] = new SelectList(_context.CustomerAccount.Where(a => a.Accounttype == AccountType.Loan).ToList(), "Id", "AccountName");
//            ViewData["BranchID"] = new SelectList(_context.Branch, "Id", "Address");
//            return View();
//        }

//        // POST: LoanAccounts/Create
//        // To protect from overposting attacks, enable the specific properties you want to bind to.
//        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Create([Bind("Id,CustomerAccountId,AccountName,LoanAmount,TermsOfLoan,NumberOfYears,InterestRate,ServicingAccountNumber,BranchID")] LoanAccount loanAccount)
//        {
//            if (!ModelState.IsValid)
//            {
//                loanAccount.InterestRate = _context.AccountConfiguration.First().LoanDebitInterestRate;
//                loanAccount.ServicingAccountNumber = _context.CustomerAccount.Find(loanAccount.CustomerAccountId).AccountNumber.ToString();

//                _context.Add(loanAccount);
//                await _context.SaveChangesAsync();
//                return RedirectToAction(nameof(Index));
//            }
//            ViewData["CustomerAccountId"] = new SelectList(_context.CustomerAccount, "Id", "AccountName", loanAccount.CustomerAccountId);
//            ViewData["BranchID"] = new SelectList(_context.Branch, "Id", "Address", loanAccount.BranchID);
//            return View(loanAccount);
//        }

//        // GET: LoanAccounts/Edit/5
//        public async Task<IActionResult> Edit(int? id)
//        {
//            if (id == null || _context.LoanAccount == null)
//            {
//                return NotFound();
//            }

//            var loanAccount = await _context.LoanAccount.FindAsync(id);
//            if (loanAccount == null)
//            {
//                return NotFound();
//            }
//            ViewData["CustomerAccountId"] = new SelectList(_context.CustomerAccount.Where(a => a.Accounttype == AccountType.Loan).ToList(), "Id", "AccountName", loanAccount.CustomerAccountId);
//            ViewData["BranchID"] = new SelectList(_context.Branch, "Id", "Address", loanAccount.BranchID);
//            return View(loanAccount);
//        }

//        // POST: LoanAccounts/Edit/5
//        // To protect from overposting attacks, enable the specific properties you want to bind to.
//        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(int id, [Bind("Id,CustomerAccountId,AccountName,LoanAmount,TermsOfLoan,NumberOfYears,InterestRate,ServicingAccountNumber,BranchID")] LoanAccount loanAccount)
//        {
//            if (id != loanAccount.Id)
//            {
//                return NotFound();
//            }

//            if (!ModelState.IsValid)
//            {
//                try
//                {
//                    _context.Update(loanAccount);
//                    await _context.SaveChangesAsync();
//                }
//                catch (DbUpdateConcurrencyException)
//                {
//                    if (!LoanAccountExists(loanAccount.Id))
//                    {
//                        return NotFound();
//                    }
//                    else
//                    {
//                        throw;
//                    }
//                }
//                return RedirectToAction(nameof(Index));
//            }
//            ViewData["CustomerAccountId"] = new SelectList(_context.CustomerAccount, "Id", "AccountName", loanAccount.CustomerAccountId);
//            ViewData["BranchID"] = new SelectList(_context.Branch, "Id", "Address", loanAccount.BranchID);
//            return View(loanAccount);
//        }

//        // GET: LoanAccounts/Delete/5
//        public async Task<IActionResult> Delete(int? id)
//        {
//            if (id == null || _context.LoanAccount == null)
//            {
//                return NotFound();
//            }

//            var loanAccount = await _context.LoanAccount
//                .Include(l => l.CustomerAccount)
//                .Include(l => l.branch)
//                .FirstOrDefaultAsync(m => m.Id == id);
//            if (loanAccount == null)
//            {
//                return NotFound();
//            }

//            return View(loanAccount);
//        }

//        // POST: LoanAccounts/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteConfirmed(int id)
//        {
//            if (_context.LoanAccount == null)
//            {
//                return Problem("Entity set 'ApplicationDbContext.LoanAccount'  is null.");
//            }
//            var loanAccount = await _context.LoanAccount.FindAsync(id);
//            if (loanAccount != null)
//            {
//                _context.LoanAccount.Remove(loanAccount);
//            }

//            await _context.SaveChangesAsync();
//            return RedirectToAction(nameof(Index));
//        }

//        private bool LoanAccountExists(int id)
//        {
//            return (_context.LoanAccount?.Any(e => e.Id == id)).GetValueOrDefault();
//        }
//    }
//}
