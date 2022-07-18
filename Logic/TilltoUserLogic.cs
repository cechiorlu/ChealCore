using ChealCore.Data;
using ChealCore.Enums;
using ChealCore.Models;
using Microsoft.EntityFrameworkCore;

namespace App.Logic
{
    public class TilltoUserLogic
    {
        private readonly ApplicationDbContext _context;


        public TilltoUserLogic(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<GLAccount> GetAllTills()
        {


            return _context.GLAccount.Where(x => x.AccountName.ToLower().Contains("till") && x.GLCategory.CategoryName.ToLower() == "cash asset").ToList();
        }

        public List<GLAccount> TillsWithoutTeller()
        {
            var tills = GetAllTills();

            var output = new List<GLAccount>();

            var tillToUsers = _context.UserTill.ToList();


            foreach (var till in tills)
            {
                if (!tillToUsers.Any(tu => tu.GlAccountID == till.ID))
                {
                    output.Add(till);
                }
            }
            return output;
        }
        public GLAccount GetUserTill(ApplicationUser teller)
        {
            bool tellerHasTill = _context.UserTill.Any(tu => tu.UserId == teller.Id);

            if (tellerHasTill)
            {
                int tillId = _context.UserTill.Where(tu => tu.UserId == teller.Id).First().GlAccountID;

                return _context.GLAccount.Find(tillId);
            }
            return null;
        }

        public List<ApplicationUser> GetAllTellers()
        {
            return _context.Users.ToList(); //.Where(u => u.Role.RoleClaims.Any(r => r.Name.Equals("TellerPosting"))).ToList();
        }

        public List<ApplicationUser> TellersWithoutTill()
        {
            var tellers = GetAllTellers();
            var output = new List<ApplicationUser>();

            var tillToUsers = _context.UserTill.ToList();

            foreach (var teller in tellers)
            {
                if (!tillToUsers.Any(tu => tu.UserId == teller.Id))
                {
                    output.Add(teller);
                }
            }
            return output;
        }

    


        public List<UserTill> ExtractAllTellerInfo()
        {
            var output = new List<UserTill>();

            var tellersWithTill = _context.UserTill.ToList();

            var tellersWithoutTill = TellersWithoutTill();

            //adding all tellers without a till account 
            foreach (var teller in tellersWithoutTill)
            {
                output.Add(new UserTill { UserId = teller.Id, GlAccountID = 0 });
            }
            //adding all tellers with a till account
            output.AddRange(tellersWithTill);
            return output;
        }

        public List<ApplicationUser> ExtractTellersWithoutTill()
        {
            return TellersWithoutTill();
        }

        public List<GLAccount> ExtractTillsWithoutTeller()
        {
            return TillsWithoutTeller();
        }
    }
}
