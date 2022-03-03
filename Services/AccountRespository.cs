using System.Collections.Generic;
using System.Linq;
using TravelTogether2.Models;

namespace TravelTogether2.Services
{
    public class AccountRespository : IAccountRespository
    {
        private readonly TourGuide_v2Context _context;
        public static int PAGE_SIZE { get; set; } = 5;

        public AccountRespository(TourGuide_v2Context context)
        {
            _context = context;
        }

        public List<Account> GetAll(string search, string sortby, int page = 1)
        {
            var allAccount = _context.Accounts.AsQueryable();


            #region Fillter (search)


            if (!string.IsNullOrEmpty(search))
            {
                allAccount = allAccount.Where(acc => acc.Email.Contains(search));
            }
           

            #endregion  

            #region Sort
            //sort by name 
            allAccount = allAccount.OrderBy(acc => acc.Email);
            if (!string.IsNullOrEmpty(sortby))
            {
                switch (sortby)
                {
                    case "Email_desc": allAccount = allAccount.OrderByDescending(acc => acc.RoleId); break;
                }
            }

            #endregion

            #region Page
            allAccount = allAccount.Skip((page - 1) * PAGE_SIZE).Take(PAGE_SIZE);

            var result = allAccount.Select(acc => new Account
            {
                Email = acc.Email,
                Password = acc.Password,
                RoleId = acc.RoleId
            });
            #endregion

            return result.ToList();
        }
    }

}
