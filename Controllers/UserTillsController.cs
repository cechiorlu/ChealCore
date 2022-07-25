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
using ChealCore.Models.ViewModels;

namespace App.Controllers
{
    public class UserTillsController : Controller
    {
        private readonly ApplicationDbContext _context;


        public UserTillsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: UserTills
        public async Task<IActionResult> Index()
        {
            TilltoUserLogic tilltoUserLogic = new(_context);

            //var applicationDbContext = _context.UserTill.Include(u => u.GLAccount).Include(u => u.User);
            //return View(await applicationDbContext.ToListAsync());

            List<UserTill> allInfo = tilltoUserLogic.ExtractAllTellerInfo();

            List<TillToUserViewModel> model = new();

            foreach (var info in allInfo)
            {
                TillToUserViewModel entry;

                if (info.GlAccountID == 0)
                {
                    entry = new TillToUserViewModel { Username = _context.Users.Find(info.UserId).UserName, GLAccountName = "NIL", AccountBalance = "NIL", HasDetails = false, IsDeletable = false };
                }
                else
                {
                    var users = _context.Users.Find(info.UserId);
                    entry = new TillToUserViewModel();
                    entry.Id = info.ID;
                    entry.Username = _context.Users.Find(info.UserId).UserName;
                    var getAcct = _context.GLAccount.Find(info.GlAccountID);
                    entry.GLAccountName = getAcct.AccountName;
                    entry.AccountBalance = info.GLAccount.AccountBalance.ToString();
                }

                model.Add(entry);
            }
            return View(model);
        }

        // GET: UserTills/Create
        public IActionResult Create()

        {
            TilltoUserLogic tilltoUserLogic = new(_context);

            //ViewData["GlAccountID"] = new SelectList(_context.GLAccount, "ID", "AccountName");
            //ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName");

            ViewData["GlAccountID"] = new SelectList(tilltoUserLogic.ExtractTillsWithoutTeller(), "ID", "AccountName");
            ViewData["UserId"] = new SelectList(tilltoUserLogic.ExtractTellersWithoutTill(), "Id", "UserName");
            return View();
        }

        // POST: UserTills/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,UserId,GlAccountID")] UserTill userTill)
        {
            TilltoUserLogic tilltoUserLogic = new(_context);
            var glAcct = await _context.GLAccount.FindAsync(userTill.GlAccountID);



            if (!ModelState.IsValid)
            {
                if (glAcct.IsActivated == true)
                {
                    _context.Add(userTill);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View("NotFound");
                }
            }
            // ViewData["GlAccountID"] = new SelectList(_context.GLAccount.Where(x => x.AccountName.ToLower().Contains("till")), "ID", "AccountName", userTill.GlAccountID);

            ViewData["GlAccountID"] = new SelectList(tilltoUserLogic.ExtractTillsWithoutTeller(), "ID", "AccountName");
            ViewData["UserId"] = new SelectList(tilltoUserLogic.ExtractTellersWithoutTill(), "Id", "UserName", userTill.UserId);


            return View(userTill);
        }

        // GET: UserTills/Manage/id
        public async Task<IActionResult> Manage(int? id)
        {
            if (id == null || _context.UserTill == null)
            {
                return NotFound();
            }

            var userTill = await _context.UserTill.FindAsync(id);
            if (userTill == null)
            {
                return NotFound();
            }
            ViewData["GlAccountID"] = new SelectList(_context.GLAccount, "ID", "AccountName", userTill.GlAccountID);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", userTill.UserId);
            return View(userTill);
        }

        // POST: UserTills/Manage/id
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Manage(int id, [Bind("ID,UserId,GlAccountID")] UserTill userTill)
        {
            if (id != userTill.ID)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(userTill);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserTillExists(userTill.ID))
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
            ViewData["GlAccountID"] = new SelectList(_context.GLAccount, "ID", "AccountName", userTill.GlAccountID);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", userTill.UserId);
            return View(userTill);
        }

       
        private bool UserTillExists(int id)
        {
            return (_context.UserTill?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
