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
using ChealCore.Enums;
using ChealCore.Models.ViewModels;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace ChealCore.Controllers
{
    public class TellerPostingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly UserManager<ApplicationUser> userManager;

        public TellerPostingsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            this.userManager = userManager;
        }

        // GET: TellerPostings
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.TellerPosting.Include(t => t.CustomerAccount).Include(t => t.TillAccount).Include(t => t.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: TellerPostings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TellerPosting == null)
            {
                return NotFound();
            }

            var tellerPosting = await _context.TellerPosting
                .Include(t => t.CustomerAccount)
                .Include(t => t.TillAccount)
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (tellerPosting == null)
            {
                return NotFound();
            }

            return View(tellerPosting);
        }

        // GET: TellerPostings/Create
        public IActionResult Create()
        {
            ViewData["CustomerAccountID"] = new SelectList(_context.CustomerAccount, "Id", "AccountName");
            ViewData["GLAccountID"] = new SelectList(_context.GLAccount.Where(a => a.AccountName.ToLower() == "till").ToList(), "ID", "AccountName");
            ViewData["GLAccountID"] = new SelectList(_context.GLAccount, "ID", "AccountName");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName");
            return View();
        }

        // POST: TellerPostings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Amount,Narration,Date,PostingType,CustomerAccountID,UserId,GLAccountID,Status")] TellerPosting tellerPosting)
        {
            if (!ModelState.IsValid)
            {
                var user = await GetCurrentUserAsync();
                var userId = user?.Id;

                //string tellerId = tellerPosting.UserId;

                TellerPostingLogic telPostLogic = new TellerPostingLogic(_context);
                CustomerAccountLogic custActLogic = new CustomerAccountLogic(_context);

                bool tellerHasTill = _context.UserTill.Any(tu => tu.UserId.Equals(userId));
                if (!tellerHasTill)
                {
                    Problem("No till associated with logged in teller");
                    tellerPosting.Status = PostStatus.Declined;
                }
                // get user's till and do the necessary calculation
                tellerPosting.UserId = userId;

                int tillId = _context.UserTill.Where(tu => tu.UserId.Equals(userId)).First().GlAccountID;

                tellerPosting.GLAccountID = tillId;

                var tillAcct = _context.GLAccount.Find(tillId);

                var custAcct = _context.CustomerAccount.Find(tellerPosting.CustomerAccountID);

                //tellerPosting.UserId = tellerId;

                tellerPosting.Date = DateTime.Now;

                var amt = tellerPosting.Amount;

                if (tellerPosting.PostingType == TellerPostingType.Withdrawal)
                {
                    if (custActLogic.CustomerAccountHasSufficientBalance(custAcct, amt))
                    {
                        if (!((decimal)tillAcct.AccountBalance >= amt))
                        {
                            Problem("Insufficient funds in till account");
                            tellerPosting.Status = PostStatus.Declined;
                        }
                        tellerPosting.Status = PostStatus.Approved;

                        string result = telPostLogic.PostTeller(custAcct, tillAcct, (float)amt, TellerPostingType.Withdrawal);
                        if (!result.Equals("success"))
                        {
                            Problem(result);
                            tellerPosting.Status = PostStatus.Declined;
                        }

                        _context.Entry(custAcct).State = EntityState.Modified;
                        _context.Entry(tillAcct).State = EntityState.Modified;

                        _context.Add(tellerPosting);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        Problem("Insufficient funds in customer account");
                        tellerPosting.Status = PostStatus.Declined;
                    }

                }

                else
                {
                    tellerPosting.Status = PostStatus.Approved;

                    string result = telPostLogic.PostTeller(custAcct, tillAcct, (float)amt, TellerPostingType.Deposit);
                    if (!result.Equals("success"))
                    {
                        Problem(result);
                        return View("index");
                    }

                    _context.Entry(custAcct).State = EntityState.Modified;
                    _context.Entry(tillAcct).State = EntityState.Modified;

                    _context.Add(tellerPosting);
                    await _context.SaveChangesAsync();
                }


                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerAccountID"] = new SelectList(_context.CustomerAccount, "Id", "AccountNumber", tellerPosting.CustomerAccountID);
            //ViewData["GLAccountID"] = new SelectList(_context.GLAccount.Where(a => a.AccountName.ToLower() == "till").ToList(), "ID", "AccountName", tellerPosting.GLAccountID);
            //ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName", tellerPosting.UserId);
            return View(tellerPosting);
        }

        private bool TellerPostingExists(int id)
        {
            return (_context.TellerPosting?.Any(e => e.ID == id)).GetValueOrDefault();
        }

        [HttpGet]
        public IActionResult VerifyCustomer()
        {
            return View();
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => userManager.GetUserAsync(HttpContext.User);

    }
}
