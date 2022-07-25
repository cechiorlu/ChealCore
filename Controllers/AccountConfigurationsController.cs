using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ChealCore.Data;
using ChealCore.Models;
using ChealCore.Logic;
using Microsoft.AspNetCore.Authorization;

namespace App.Controllers
{
    // [Authorize(Policy = "CBA001")]
    public class AccountConfigurationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountConfigurationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AccountConfigurations
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.AccountConfiguration.Include(a => a.CurrentCotIncomeGl).Include(a => a.CurrentInterestExpenseGl).Include(a => a.CurrentInterestPayableGl).Include(a => a.LoanInterestExpenseGl).Include(a => a.LoanInterestIncomeGl).Include(a => a.LoanInterestReceivableGl).Include(a => a.SavingsInterestExpenseGl).Include(a => a.SavingsInterestPayableGl);
            return View(await applicationDbContext.FirstOrDefaultAsync());
        }


        // GET: AccountConfigurations/Create
        public IActionResult Create()
        {
            ViewData["CurrentCotIncomeGlID"] = new SelectList(_context.GLAccount, "ID", "AccountName");
            ViewData["CurrentInterestExpenseGlID"] = new SelectList(_context.GLAccount, "ID", "AccountName");
            ViewData["CurrentInterestPayableGlID"] = new SelectList(_context.GLAccount, "ID", "AccountName");
            ViewData["LoanInterestExpenseGLID"] = new SelectList(_context.GLAccount, "ID", "AccountName");
            ViewData["LoanInterestIncomeGlID"] = new SelectList(_context.GLAccount, "ID", "AccountName");
            ViewData["LoanInterestReceivableGlID"] = new SelectList(_context.GLAccount, "ID", "AccountName");
            ViewData["SavingsInterestExpenseGlID"] = new SelectList(_context.GLAccount, "ID", "AccountName");
            ViewData["SavingsInterestPayableGlID"] = new SelectList(_context.GLAccount, "ID", "AccountName");
            return View();
        }

        // POST: AccountConfigurations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,IsBusinessOpen,FinancialDate,SavingsCreditInterestRate,SavingsMinimumBalance,SavingsInterestExpenseGlID,SavingsInterestPayableGlID,CurrentCreditInterestRate,CurrentMinimumBalance,CurrentCot,CurrentInterestExpenseGlID,CurrentCotIncomeGlID,CurrentInterestPayableGlID,LoanDebitInterestRate,LoanInterestIncomeGlID,LoanInterestExpenseGLID,LoanInterestReceivableGlID")] AccountConfiguration accountConfiguration)
        {
            if (!ModelState.IsValid)
            {
                var financialDateUtc = DateTime.UtcNow;
                accountConfiguration.FinancialDate = financialDateUtc;
                _context.Add(accountConfiguration);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CurrentCotIncomeGlID"] = new SelectList(_context.GLAccount, "ID", "AccountName", accountConfiguration.CurrentCotIncomeGlID);
            ViewData["CurrentInterestExpenseGlID"] = new SelectList(_context.GLAccount, "ID", "AccountName", accountConfiguration.CurrentInterestExpenseGlID);
            ViewData["CurrentInterestPayableGlID"] = new SelectList(_context.GLAccount, "ID", "AccountName", accountConfiguration.CurrentInterestPayableGlID);
            ViewData["LoanInterestExpenseGLID"] = new SelectList(_context.GLAccount, "ID", "AccountName", accountConfiguration.LoanInterestExpenseGLID);
            ViewData["LoanInterestIncomeGlID"] = new SelectList(_context.GLAccount, "ID", "AccountName", accountConfiguration.LoanInterestIncomeGlID);
            ViewData["LoanInterestReceivableGlID"] = new SelectList(_context.GLAccount, "ID", "AccountName", accountConfiguration.LoanInterestReceivableGlID);
            ViewData["SavingsInterestExpenseGlID"] = new SelectList(_context.GLAccount, "ID", "AccountName", accountConfiguration.SavingsInterestExpenseGlID);
            ViewData["SavingsInterestPayableGlID"] = new SelectList(_context.GLAccount, "ID", "AccountName", accountConfiguration.SavingsInterestPayableGlID);
            return View(accountConfiguration);
        }

        // GET: AccountConfigurations/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            AccountConfiguration accountConfiguration = await _context.AccountConfiguration.FirstAsync();

            if (accountConfiguration == null)
            {
                return NotFound();
            }
            ViewData["CurrentCotIncomeGlID"] = new SelectList(_context.GLAccount, "ID", "AccountName", accountConfiguration.CurrentCotIncomeGlID);
            ViewData["CurrentInterestExpenseGlID"] = new SelectList(_context.GLAccount, "ID", "AccountName", accountConfiguration.CurrentInterestExpenseGlID);
            ViewData["CurrentInterestPayableGlID"] = new SelectList(_context.GLAccount, "ID", "AccountName", accountConfiguration.CurrentInterestPayableGlID);
            ViewData["LoanInterestExpenseGLID"] = new SelectList(_context.GLAccount, "ID", "AccountName", accountConfiguration.LoanInterestExpenseGLID);
            ViewData["LoanInterestIncomeGlID"] = new SelectList(_context.GLAccount, "ID", "AccountName", accountConfiguration.LoanInterestIncomeGlID);
            ViewData["LoanInterestReceivableGlID"] = new SelectList(_context.GLAccount, "ID", "AccountName", accountConfiguration.LoanInterestReceivableGlID);
            ViewData["SavingsInterestExpenseGlID"] = new SelectList(_context.GLAccount, "ID", "AccountName", accountConfiguration.SavingsInterestExpenseGlID);
            ViewData["SavingsInterestPayableGlID"] = new SelectList(_context.GLAccount, "ID", "AccountName", accountConfiguration.SavingsInterestPayableGlID);
            return View(accountConfiguration);
        }

        // POST: AccountConfigurations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("ID,IsBusinessOpen,FinancialDate,SavingsCreditInterestRate,SavingsMinimumBalance,SavingsInterestExpenseGlID,SavingsInterestPayableGlID,CurrentCreditInterestRate,CurrentMinimumBalance,CurrentCot,CurrentInterestExpenseGlID,CurrentCotIncomeGlID,CurrentInterestPayableGlID,LoanDebitInterestRate,LoanInterestIncomeGlID,LoanInterestExpenseGLID,LoanInterestReceivableGlID")] AccountConfiguration accountConfiguration)
        {
            if (accountConfiguration == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(accountConfiguration);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountConfigurationExists(accountConfiguration.ID))
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
            ViewData["CurrentCotIncomeGlID"] = new SelectList(_context.GLAccount, "ID", "AccountName", accountConfiguration.CurrentCotIncomeGlID);
            ViewData["CurrentInterestExpenseGlID"] = new SelectList(_context.GLAccount, "ID", "AccountName", accountConfiguration.CurrentInterestExpenseGlID);
            ViewData["CurrentInterestPayableGlID"] = new SelectList(_context.GLAccount, "ID", "AccountName", accountConfiguration.CurrentInterestPayableGlID);
            ViewData["LoanInterestExpenseGLID"] = new SelectList(_context.GLAccount, "ID", "AccountName", accountConfiguration.LoanInterestExpenseGLID);
            ViewData["LoanInterestIncomeGlID"] = new SelectList(_context.GLAccount, "ID", "AccountName", accountConfiguration.LoanInterestIncomeGlID);
            ViewData["LoanInterestReceivableGlID"] = new SelectList(_context.GLAccount, "ID", "AccountName", accountConfiguration.LoanInterestReceivableGlID);
            ViewData["SavingsInterestExpenseGlID"] = new SelectList(_context.GLAccount, "ID", "AccountName", accountConfiguration.SavingsInterestExpenseGlID);
            ViewData["SavingsInterestPayableGlID"] = new SelectList(_context.GLAccount, "ID", "AccountName", accountConfiguration.SavingsInterestPayableGlID);
            return View(accountConfiguration);
        }

        // GET: AccountConfigurations/Delete/5
        public async Task<IActionResult> Delete()
        {
            AccountConfiguration accountConfiguration = await _context.AccountConfiguration.FirstAsync();

            if (accountConfiguration == null)
            {
                return NotFound();
            }

            //var accountConfiguration = await _context.AccountConfiguration
            //    .Include(a => a.CurrentCotIncomeGl)
            //    .Include(a => a.CurrentInterestExpenseGl)
            //    .Include(a => a.CurrentInterestPayableGl)
            //    .Include(a => a.LoanInterestExpenseGl)
            //    .Include(a => a.LoanInterestIncomeGl)
            //    .Include(a => a.LoanInterestReceivableGl)
            //    .Include(a => a.SavingsInterestExpenseGl)
            //    .Include(a => a.SavingsInterestPayableGl)
            //    .FirstOrDefaultAsync(m => m.ID == id);
            //if (accountConfiguration == null)
            //{
            //    return NotFound();
            //}

            return View(accountConfiguration);
        }

        // POST: AccountConfigurations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed()
        {
            if (_context.AccountConfiguration == null)
            {
                return Problem("Entity set 'ApplicationDbContext.AccountConfiguration'  is null.");
            }
            var accountConfiguration = await _context.AccountConfiguration.FirstAsync();
            if (accountConfiguration != null)
            {
                _context.AccountConfiguration.Remove(accountConfiguration);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccountConfigurationExists(int id)
        {
            return (_context.AccountConfiguration?.Any(e => e.ID == id)).GetValueOrDefault();
        }



        private void SetGetGlActViewBags(AccountConfiguration accountConfiguration)
        {
            AccountConfigLogic accountConfigLogic = new(_context);

            var allassets = accountConfigLogic.ExtractAssetGLs();
            var allincome = accountConfigLogic.ExtractIncomeGLs();
            var allexpense = accountConfigLogic.EExtractExpenseGLs();
            var allliability = accountConfigLogic.ExtractLiabilityGLs();

            //var allCurrentLiability = allliability.Where(g => g.GlCategory.Name.ToLowerInvariant().Contains("current")).ToList();
            //var x = allliability.Where(g => g.GlCategory.Name.ToLowerInvariant().Contains("savings")).ToList();
            //var allSavingsLiability = allliability.Where(g => g.GlCategory.Name.ToLowerInvariant().Contains("savings")).ToList();

            ViewBag.CurrentCotIncomeGlID = new SelectList(allincome, "ID", "AccountName", accountConfiguration.CurrentCotIncomeGlID == null ? 0 : accountConfiguration.CurrentCotIncomeGlID);
            ViewBag.CurrentInterestExpenseGlID = new SelectList(allexpense, "ID", "AccountName", accountConfiguration.CurrentInterestExpenseGlID == null ? 0 : accountConfiguration.CurrentInterestExpenseGlID);
            ViewBag.CurrentInterestPayableGlID = new SelectList(allliability, "ID", "AccountName", accountConfiguration.CurrentInterestPayableGlID == null ? 0 : accountConfiguration.CurrentInterestPayableGlID);
            ViewBag.LoanInterestExpenseGLID = new SelectList(allexpense, "ID", "AccountName", accountConfiguration.LoanInterestExpenseGLID == null ? 0 : accountConfiguration.LoanInterestExpenseGLID);
            ViewBag.LoanInterestIncomeGlID = new SelectList(allincome, "ID", "AccountName", accountConfiguration.LoanInterestIncomeGlID == null ? 0 : accountConfiguration.LoanInterestIncomeGlID);
            ViewBag.LoanInterestReceivableGlID = new SelectList(allassets, "ID", "AccountName", accountConfiguration.LoanInterestReceivableGlID == null ? 0 : accountConfiguration.LoanInterestReceivableGlID);
            ViewBag.SavingsInterestExpenseGlID = new SelectList(allexpense, "ID", "AccountName", accountConfiguration.SavingsInterestExpenseGlID == null ? 0 : accountConfiguration.SavingsInterestExpenseGlID);
            ViewBag.SavingsInterestPayableGlID = new SelectList(allliability, "ID", "AccountName", accountConfiguration.SavingsInterestPayableGlID == null ? 0 : accountConfiguration.SavingsInterestPayableGlID);
        }

        private void SetPostGlActViewBags(AccountConfiguration accountConfiguration)
        {
            AccountConfigLogic accountConfigLogic = new(_context);

            var allassets = accountConfigLogic.ExtractAssetGLs();
            var allincome = accountConfigLogic.ExtractIncomeGLs();
            var allexpense = accountConfigLogic.EExtractExpenseGLs();
            var allliability = accountConfigLogic.ExtractLiabilityGLs();

            //var allCurrentLiability = allliability.Where(g => g.GlCategory.Name.ToLowerInvariant().Contains("current")).ToList();
            //var allSavingsLiability = allliability.Where(g => g.GlCategory.Name.ToLowerInvariant().Contains("savings")).ToList();

            ViewBag.CurrentCotIncomeGlID = new SelectList(allincome, "ID", "AccountName", accountConfiguration.CurrentCotIncomeGlID);
            ViewBag.CurrentInterestExpenseGlID = new SelectList(allexpense, "ID", "AccountName", accountConfiguration.CurrentInterestExpenseGlID);
            ViewBag.CurrentInterestPayableGlID = new SelectList(allliability, "ID", "AccountName", accountConfiguration.CurrentInterestPayableGlID);
            ViewBag.LoanInterestExpenseGLID = new SelectList(allexpense, "ID", "AccountName", accountConfiguration.LoanInterestExpenseGLID);
            ViewBag.LoanInterestIncomeGlID = new SelectList(allincome, "ID", "AccountName", accountConfiguration.LoanInterestIncomeGlID);
            ViewBag.LoanInterestReceivableGlID = new SelectList(allassets, "ID", "AccountName", accountConfiguration.LoanInterestReceivableGlID);
            ViewBag.SavingsInterestExpenseGlID = new SelectList(allexpense, "ID", "AccountName", accountConfiguration.SavingsInterestExpenseGlID);
            ViewBag.SavingsInterestPayableGlID = new SelectList(allliability, "ID", "AccountName", accountConfiguration.SavingsInterestPayableGlID);
        }

        public async Task<IActionResult> RunEOD(AccountConfiguration accountConfiguration)
        {

            return View(accountConfiguration);
        }

    }
}
