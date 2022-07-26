using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ChealCore.Data;
using ChealCore.Models;
using ChealCore.Enums;

namespace ChealCore.Controllers
{
    public class LoanAccountsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LoanAccountsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: LoanAccounts
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.LoanAccount.Include(l => l.CustomerAccount).Include(l => l.branch);
            return View(await applicationDbContext.ToListAsync());
        }


        // GET: LoanAccounts/Create
        public IActionResult Create()
        {
            var terms = new List<string> {
                LoanTerms.Fixed.ToString(),
                LoanTerms.Reducing.ToString()
            };

            ViewData["LoanTerms"] = new SelectList(terms);
            ViewData["CustomerAccountId"] = new SelectList(_context.CustomerAccount.Where(a => a.Accounttype == AccountType.Loan).ToList(), "Id", "AccountName");
            ViewData["BranchID"] = new SelectList(_context.Branch, "Id", "Address");
            return View();
        }

        // POST: LoanAccounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CustomerAccountId,AccountName,LoanAmount,LoanTerms,NumberOfYears,InterestRate,ServicingAccountNumber,BranchID")] LoanAccount loanAccount)
        {
            if (!ModelState.IsValid)
            {
                loanAccount.InterestRate = _context.AccountConfiguration.First().LoanDebitInterestRate;
                loanAccount.ServicingAccountNumber = _context.CustomerAccount.Find(loanAccount.CustomerAccountId).AccountNumber.ToString();

                _context.Add(loanAccount);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var terms = new List<string> {
                LoanTerms.Fixed.ToString(),
                LoanTerms.Reducing.ToString()
            };

            ViewData["CustomerAccountId"] = new SelectList(_context.CustomerAccount, "Id", "AccountName", loanAccount.CustomerAccountId);
            ViewData["BranchID"] = new SelectList(_context.Branch, "Id", "Address", loanAccount.BranchID);
            ViewData["LoanTerms"] = new SelectList(terms, loanAccount.LoanTerms.ToString());
            return View(loanAccount);
        }

        // GET: LoanAccounts/Manage/5
        public async Task<IActionResult> Manage(int? id)
        {
            if (id == null || _context.LoanAccount == null)
            {
                return NotFound();
            }

            var loanAccount = await _context.LoanAccount.FindAsync(id);
            if (loanAccount == null)
            {
                return NotFound();
            }

            var terms = new List<string> {
                LoanTerms.Fixed.ToString(),
                LoanTerms.Reducing.ToString()
            };

            ViewData["LoanTerms"] = new SelectList(terms);
            ViewData["CustomerAccountId"] = new SelectList(_context.CustomerAccount.Where(a => a.Accounttype == AccountType.Loan).ToList(), "Id", "AccountName", loanAccount.CustomerAccountId);
            ViewData["BranchID"] = new SelectList(_context.Branch, "Id", "Address", loanAccount.BranchID);
            return View(loanAccount);
        }

        // POST: LoanAccounts/Manage/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Manage(int id, [Bind("Id,CustomerAccountId,AccountName,LoanAmount,LoanTerms,NumberOfYears,InterestRate,ServicingAccountNumber,BranchID")] LoanAccount loanAccount)
        {
            if (id != loanAccount.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(loanAccount);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoanAccountExists(loanAccount.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            var terms = new List<string> {
                LoanTerms.Fixed.ToString(),
                LoanTerms.Reducing.ToString()
            };

            ViewData["CustomerAccountId"] = new SelectList(_context.CustomerAccount, "Id", "AccountName", loanAccount.CustomerAccountId);
            ViewData["BranchID"] = new SelectList(_context.Branch, "Id", "Address", loanAccount.BranchID);
            ViewData["LoanTerms"] = new SelectList(terms, loanAccount.LoanTerms.ToString());
            return View(loanAccount);
        }

        private bool LoanAccountExists(int id)
        {
            return (_context.LoanAccount?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
