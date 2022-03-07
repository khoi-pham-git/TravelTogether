using System.Collections.Generic;
using System.Linq;
using TravelTogether2.Models;

namespace TravelTogether2.Services
{
    public class CustomerResponsitory : ICustomerRespository
    {
        private readonly TourGuide_v2Context _context;
        public static int PAGE_SIZE { get; set; } = 5;

        public CustomerResponsitory(TourGuide_v2Context context)
        {
            _context = context;
        }

        public List<Customer> GetAll(string search, string sortby, int page = 1)
        {

            var allCusomer = _context.Customers.AsQueryable();


            #region Fillter (search)


            if (!string.IsNullOrEmpty(search))
            {
                allCusomer = allCusomer.Where(cus => cus.Name.Contains(search));
            }


            //allCusomer = allCusomer.OrderBy(cus => cus.Email);
            if (!string.IsNullOrEmpty(sortby))
            {
                switch (sortby)
                {
                    case "Name_desc": allCusomer = allCusomer.OrderByDescending(cus => cus.Name); break;
                    case "Id_desc": allCusomer = allCusomer.OrderByDescending(cus => cus.Id); break;
                    case "Phone_desc": allCusomer = allCusomer.OrderByDescending(cus => cus.Phone); break;
                    case "Email_desc": allCusomer = allCusomer.OrderByDescending(cus => cus.Email); break;
                    case "Address_desc": allCusomer = allCusomer.OrderByDescending(cus => cus.Address); break;
                }
            }

            allCusomer = allCusomer.Skip((page - 1) * PAGE_SIZE).Take(PAGE_SIZE);

            #endregion

            //sort by name 
            var result = allCusomer.Select(cus => new Customer
            {
                Id = cus.Id,
                Name = cus.Name,
                Phone = cus.Phone,
                Email = cus.Email,
                Address = cus.Address,
                Image = cus.Image
            });

            return result.ToList();
        }
    }
}
