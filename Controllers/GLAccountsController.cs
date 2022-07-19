using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ChealCore.Data;
using ChealCore.Models;

namespace ChealCore.Controllers
{
    public class GLAccountsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GLAccountsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: GLAccounts
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.GLAccount.Include(g => g.Branch).Include(g => g.GLCategory);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: GLAccounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.GLAccount == null)
            {
                return NotFound();
            }

            var gLAccount = await _context.GLAccount
                .Include(g => g.Branch)
                .Include(g => g.GLCategory)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (gLAccount == null)
            {
                return NotFound();
            }

            return View(gLAccount);
        }

        // GET: GLAccounts/Create
        public IActionResult Create()
        {
            ViewData["BranchID"] = new SelectList(_context.Branch, "Id", "Address");
            ViewData["GLCategoryID"] = new SelectList(_context.GLCategory, "CategoryId", "CategoryDescription");
            return View();
        }

        // POST: GLAccounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,AccountName,CodeNumber,AccountBalance,GLCategoryID,BranchID,IsActivated")] GLAccount gLAccount)
        {
            GLCategory glCategory = await _context.GLCategory.FindAsync(gLAccount.GLCategoryID);
            Random random = new();

            if (!ModelState.IsValid)
            {

                gLAccount.CodeNumber = (long)(glCategory.CodeNumber + random.Next(10, 100));

                _context.Add(gLAccount);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BranchID"] = new SelectList(_context.Branch, "Id", "Address", gLAccount.BranchID);
            ViewData["GLCategoryID"] = new SelectList(_context.GLCategory, "CategoryId", "CategoryDescription", gLAccount.GLCategoryID);
            return View(gLAccount);
        }

        // GET: GLAccounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.GLAccount == null)
            {
                return NotFound();
            }

            var gLAccount = await _context.GLAccount.FindAsync(id);
            if (gLAccount == null)
            {
                return NotFound();
            }
            ViewData["BranchID"] = new SelectList(_context.Branch, "Id", "Address", gLAccount.BranchID);
            ViewData["GLCategoryID"] = new SelectList(_context.GLCategory, "CategoryId", "CategoryName", gLAccount.GLCategoryID);
            return View(gLAccount);
        }

        // POST: GLAccounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,AccountName,CodeNumber,AccountBalance,GLCategoryID,BranchID,IsActivated")] GLAccount gLAccount)
        {
            if (id != gLAccount.ID)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(gLAccount);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GLAccountExists(gLAccount.ID))
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
            ViewData["BranchID"] = new SelectList(_context.Branch, "Id", "Address", gLAccount.BranchID);
            ViewData["GLCategoryID"] = new SelectList(_context.GLCategory, "CategoryId", "CategoryDescription", gLAccount.GLCategoryID);
            return View(gLAccount);
        }

        // GET: GLAccounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.GLAccount == null)
            {
                return NotFound();
            }

            var gLAccount = await _context.GLAccount
                .Include(g => g.Branch)
                .Include(g => g.GLCategory)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (gLAccount == null)
            {
                return NotFound();
            }

            return View(gLAccount);
        }

        // POST: GLAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.GLAccount == null)
            {
                return Problem("Entity set 'ApplicationDbContext.GLAccount'  is null.");
            }
            var gLAccount = await _context.GLAccount.FindAsync(id);
            if (gLAccount != null)
            {
                _context.GLAccount.Remove(gLAccount);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GLAccountExists(int id)
        {
            return (_context.GLAccount?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}