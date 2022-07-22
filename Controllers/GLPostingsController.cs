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
using ChealCore.Logic;

namespace ChealCore.Controllers
{
    public class GLPostingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        BusinessLogic busLogic = new BusinessLogic();

        public GLPostingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: GlPostings
        public async Task<IActionResult> Index()
        {

            var applicationDbContext = _context.GLPosting.Include(g => g.CrGlAccount).Include(g => g.DrGlAccount);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: GlPostings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.GLPosting == null)
            {
                return NotFound();
            }

            var glPosting = await _context.GLPosting
                .Include(g => g.CrGlAccount)
                .Include(g => g.DrGlAccount)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (glPosting == null)
            {
                return NotFound();
            }

            return View(glPosting);
        }

        // GET: GlPostings/Create
        public IActionResult Create()
        {
            ViewData["CrGlAccountID"] = new SelectList(_context.GLAccount, "ID", "AccountName");
            ViewData["DrGlAccountID"] = new SelectList(_context.GLAccount, "ID", "AccountName");
            return View();
        }

        // POST: GlPostings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,CreditAmount,DebitAmount,Narration,Date,DrGlAccountID,CrGlAccountID,PostInitiatorId,Status")] GLPosting glPosting)
        {
            if (!ModelState.IsValid)
            {
                var drAct = _context.GLAccount.Find(glPosting.DrGlAccountID);
                var crAct = _context.GLAccount.Find(glPosting.CrGlAccountID);


                if (crAct.AccountName.ToLower().Contains("till") || crAct.AccountName.ToLower().Contains("vault"))
                {
                    if ((decimal)crAct.AccountBalance < glPosting.CreditAmount)
                    {
                        Problem("Insufficient funds in asset account to be credited");
                        return View("NotFound");
                    }
                }
                glPosting.Date = DateTime.Now;
                glPosting.CreditAmount = glPosting.DebitAmount;


                if (!(drAct.AccountBalance > (float)glPosting.DebitAmount))
                {
                    Problem("There is not enough funds in the account to be debited");
                    return View("NotFound");
                }
                else
                {
                    crAct.AccountBalance += (float)glPosting.CreditAmount;
                    drAct.AccountBalance -= (float)glPosting.DebitAmount;
                }

                //var transaction = new Transaction
                //{
                //    Amount = glPosting.CreditAmount,
                //    Date = DateTime.Now,
                //    AccountName = drAct.AccountName,
                //    SubCategory = drAct.GLCategory.CategoryName.ToString(),
                //    mainAccountCategory = drAct.GLCategory.mainAccountCategory

                //};

                _context.Add(glPosting);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CrGlAccountID"] = new SelectList(_context.GLAccount, "ID", "AccountName", glPosting.CrGlAccountID);
            ViewData["DrGlAccountID"] = new SelectList(_context.GLAccount, "ID", "AccountName", glPosting.DrGlAccountID);
            return View(glPosting);
        }

        // GET: GlPostings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.GLPosting == null)
            {
                return NotFound();
            }

            var glPosting = await _context.GLPosting.FindAsync(id);
            if (glPosting == null)
            {
                return NotFound();
            }
            ViewData["CrGlAccountID"] = new SelectList(_context.GLAccount, "ID", "AccountName", glPosting.CrGlAccountID);
            ViewData["DrGlAccountID"] = new SelectList(_context.GLAccount, "ID", "AccountName", glPosting.DrGlAccountID);
            return View(glPosting);
        }

        // POST: GlPostings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,CreditAmount,DebitAmount,Narration,Date,DrGlAccountID,CrGlAccountID,PostInitiatorId,Status")] GLPosting glPosting)
        {
            if (id != glPosting.ID)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(glPosting);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GlPostingExists(glPosting.ID))
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
            ViewData["CrGlAccountID"] = new SelectList(_context.GLAccount, "ID", "AccountName", glPosting.CrGlAccountID);
            ViewData["DrGlAccountID"] = new SelectList(_context.GLAccount, "ID", "AccountName", glPosting.DrGlAccountID);
            return View(glPosting);
        }

        // GET: GlPostings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.GLPosting == null)
            {
                return NotFound();
            }

            var glPosting = await _context.GLPosting
                .Include(g => g.CrGlAccount)
                .Include(g => g.DrGlAccount)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (glPosting == null)
            {
                return NotFound();
            }

            return View(glPosting);
        }

        // POST: GlPostings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.GLPosting == null)
            {
                return Problem("Entity set 'ApplicationDbContext.GlPosting'  is null.");
            }
            var glPosting = await _context.GLPosting.FindAsync(id);
            if (glPosting != null)
            {
                _context.GLPosting.Remove(glPosting);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GlPostingExists(int id)
        {
            return (_context.GLPosting?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
