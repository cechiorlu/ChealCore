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

        BusinessLogic businessLogic = new BusinessLogic();

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
                        return View("InsufficientFunds");
                    }
                }
                DateTime postingDate = glPosting.Date.ToUniversalTime();
                glPosting.Date = postingDate;
                glPosting.CreditAmount = glPosting.DebitAmount;


                if (!(drAct.AccountBalance > (float)glPosting.DebitAmount))
                {
                    Problem("There is not enough funds in the account to be debited");
                    return View("InsufficientFunds");
                }
                else
                {
                    crAct.AccountBalance += (float)glPosting.CreditAmount;
                    drAct.AccountBalance -= (float)glPosting.DebitAmount;
                }

                _context.Add(glPosting);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CrGlAccountID"] = new SelectList(_context.GLAccount, "ID", "AccountName", glPosting.CrGlAccountID);
            ViewData["DrGlAccountID"] = new SelectList(_context.GLAccount, "ID", "AccountName", glPosting.DrGlAccountID);
            return View(glPosting);
        }

        // GET: GlPostings/Manage/id
        public async Task<IActionResult> Manage(int? id)
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

        // POST: GlPostings/Manage/id
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Manage(int id, [Bind("ID,CreditAmount,DebitAmount,Narration,Date,DrGlAccountID,CrGlAccountID,PostInitiatorId,Status")] GLPosting glPosting)
        {
            if (id != glPosting.ID)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    DateTime postingDate = glPosting.Date.ToUniversalTime();
                    glPosting.Date = postingDate;
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

        private bool GlPostingExists(int id)
        {
            return (_context.GLPosting?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
