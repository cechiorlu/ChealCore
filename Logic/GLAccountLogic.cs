using ChealCore.Data;
using ChealCore.Enums;
using ChealCore.Models;

namespace ChealCore.Logic
{
    public class GLAccountLogic
    {
        private readonly ApplicationDbContext _context;

        public GLAccountLogic(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<GLAccount> GetByMainCategory(MainAccountCategory mainCategory)
        {
            return _context.GLAccount.Where(a => a.GLCategory.mainAccountCategory == mainCategory).ToList();
        }
    }
}
