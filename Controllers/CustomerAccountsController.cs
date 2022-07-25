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

namespace App.Controllers
{
    public class CustomerAccountsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomerAccountsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CustomerAccounts
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.CustomerAccount.Include(c => c.Customer);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: CustomerAccounts/Create
        public IActionResult Create()
        {
            ViewData["CustomerID"] = new SelectList(_context.Customer, "CustomerID", "FullName");
            return View();
        }

        // POST: CustomerAccounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CustomerID,AccountName,AccountNumber,AccountBalance,Accounttype,DateOpened,IsActivated")] CustomerAccount customerAccount)
        {
            CustomerLogic customerLogic = new();

            CustomerAccountLogic customerAcctLogic = new(_context);


            customerAccount.AccountNumber = Int64.Parse(customerAcctLogic.GenerateCustomerAccountNumber(customerAccount.Accounttype) + customerLogic.GenerateCustomerId() + customerAccount.CustomerID.ToString());

            var customerData = await _context.Customer.FindAsync(customerAccount.CustomerID);

            if (!ModelState.IsValid)
            {
                customerAccount.DateOpened = DateTime.UtcNow;
                _context.Add(customerAccount);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerID"] = new SelectList(_context.Customer, "CustomerID", "FullName", customerAccount.CustomerID);
            return View(customerAccount);
        }

        // GET: CustomerAccounts/Manage/id
        public async Task<IActionResult> Manage(int? id)
        {
            if (id == null || _context.CustomerAccount == null)
            {
                return NotFound();
            }

            var customerAccount = await _context.CustomerAccount.FindAsync(id);
            if (customerAccount == null)
            {
                return NotFound();
            }
            ViewData["CustomerID"] = new SelectList(_context.Customer, "CustomerID", "FullName", customerAccount.CustomerID);
            return View(customerAccount);
        }

        // POST: CustomerAccounts/Manage/id
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Manage(int id, [Bind("Id,CustomerID,AccountName,AccountNumber,AccountBalance,Accounttype,DateOpened,IsActivated")] CustomerAccount customerAccount)
        {
            if (id != customerAccount.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    DateTime dateOpened = customerAccount.DateOpened.ToUniversalTime();
                    customerAccount.DateOpened = dateOpened;
                    _context.Update(customerAccount);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerAccountExists(customerAccount.Id))
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
            ViewData["CustomerID"] = new SelectList(_context.Customer, "CustomerID", "FullName", customerAccount.CustomerID);
            return View(customerAccount);
        }

        private bool CustomerAccountExists(int id)
        {
            return (_context.CustomerAccount?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
