using ChealCore.Models;
using ChealCore.Enums;
using Microsoft.EntityFrameworkCore;
using ChealCore.Data;

namespace ChealCore.Logic
{
    public class AccountConfigLogic
    {
        private readonly ApplicationDbContext _context;

        public AccountConfigLogic(ApplicationDbContext context)
        {
            this._context = context;
        }

        public List<GLAccount> ExtractIncomeGLs()
        {
            var output = _context.GLAccount.Where(a => a.GLCategory.mainAccountCategory == MainAccountCategory.Income).ToList();

            output.Insert(0, new GLAccount { ID = 0, AccountName = "--select--" });

            return output;
        }

        public List<GLAccount> EExtractExpenseGLs()
        {
            var output = _context.GLAccount.Where(a => a.GLCategory.mainAccountCategory == MainAccountCategory.Expenses).ToList();

            output.Insert(0, new GLAccount { ID = 0, AccountName = "--select--" });

            return output;
        }
        public List<GLAccount> ExtractAssetGLs()
        {
            var output = _context.GLAccount.Where(a => a.GLCategory.mainAccountCategory == MainAccountCategory.Asset).ToList();

            output.Insert(0, new GLAccount { ID = 0, AccountName = "--select--" });

            return output;
        }
        public List<GLAccount> ExtractLiabilityGLs()
        {
            var output = _context.GLAccount.Where(a => a.GLCategory.mainAccountCategory == MainAccountCategory.Liability).ToList();

            output.Insert(0, new GLAccount { ID = 0, AccountName = "--select--" });

            return output;
        }

    }
}
