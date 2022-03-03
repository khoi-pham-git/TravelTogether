using System.Collections.Generic;
using System.Linq;
using TravelTogether2.Models;

namespace TravelTogether2.Services
{
    public class CategoryResponsitory : ICategoryResponsitory
    {
        private readonly TourGuide_v2Context _context;
        public static int PAGE_SIZE { get; set; } = 5;

        public CategoryResponsitory(TourGuide_v2Context context)
        {
            _context = context;
        }
        public List<Category> GetAll(string search, string sortby, int page = 1)
        {

            var allCategory = _context.Categories.AsQueryable();


            #region Fillter (search)


            if (!string.IsNullOrEmpty(search))
            {
                allCategory = allCategory.Where(Cate => Cate.Name.Contains(search));
            }


            //allCusomer = allCusomer.OrderBy(cus => cus.Email);
            if (!string.IsNullOrEmpty(sortby))
            {
                switch (sortby)
                {
                    case "Name_desc": allCategory = allCategory.OrderByDescending(Cate => Cate.Name); break;
                    case "Id_desc": allCategory = allCategory.OrderByDescending(Cate => Cate.Id); break;
                }
            }

            allCategory = allCategory.Skip((page - 1) * PAGE_SIZE).Take(PAGE_SIZE);

            #endregion

            //sort by name 
            var result = allCategory.Select(Cate => new Category
            {
               Id = Cate.Id,
               Name = Cate.Name
            });

            return result.ToList();



        }
    }
}
