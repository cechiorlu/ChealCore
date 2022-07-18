using ChealCore.Data;
using ChealCore.Enums;
using ChealCore.Models;

namespace ChealCore.Logic
{
    public class BalanceSheetLogic
    {
        private readonly ApplicationDbContext _context;

        public BalanceSheetLogic(ApplicationDbContext context)
        {
            _context = context; 
        }
        public List<GLAccount> GetAssetAccounts()
        {
            var allAssets = _context.GLAccount.Where(a => a.GLCategory.mainAccountCategory == MainAccountCategory.Asset).ToList();
            return allAssets;

        }

        public List<GLAccount> GetCapitalAccounts()
        {
            var allCapital = _context.GLAccount.Where(a => a.GLCategory.mainAccountCategory == MainAccountCategory.Capital).ToList();
            return allCapital;
        }

        public List<GLAccount> GetIncomeAccounts()
        {
            var allIncome = _context.GLAccount.Where(a => a.GLCategory.mainAccountCategory == MainAccountCategory.Income).ToList();
            return allIncome;
        }

        public List<GLAccount> GetExpenseAccounts()
        {
            var allExpense = _context.GLAccount.Where(a => a.GLCategory.mainAccountCategory == MainAccountCategory.Expenses).ToList();
            return allExpense;
        }
        public List<GLAccount> GetLiabilityAccounts()
        {
            var allLiability = _context.GLAccount.Where(a => a.GLCategory.mainAccountCategory == MainAccountCategory.Liability).ToList();
            return allLiability;
        }
    }
}
