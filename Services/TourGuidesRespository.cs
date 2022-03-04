using System.Collections.Generic;
using System.Linq;
using TravelTogether2.Models;

namespace TravelTogether2.Services
{
    public class TourGuidesRespository : ITourGuidesRespository
    {
        private readonly TourGuide_v2Context _context;
        public static int PAGE_SIZE { get; set; } = 5;

        public TourGuidesRespository(TourGuide_v2Context context)
        {
            _context = context;
        }
        public List<TourGuide> GetAll(string search, string sortby, int page = 1)
        {
            var allTourGuide = _context.TourGuides.AsQueryable();


            #region Fillter (search)


            if (!string.IsNullOrEmpty(search))
            {
                allTourGuide = allTourGuide.Where(cus => cus.Name.Contains(search));
            }


            //allCusomer = allCusomer.OrderBy(cus => cus.Email);
            if (!string.IsNullOrEmpty(sortby))
            {
                switch (sortby)
                {
                    case "Name_desc": allTourGuide = allTourGuide.OrderByDescending(cus => cus.Name); break;
                    case "Id_desc": allTourGuide = allTourGuide.OrderByDescending(cus => cus.Id); break;
                }
            }

            allTourGuide = allTourGuide.Skip((page - 1) * PAGE_SIZE).Take(PAGE_SIZE);

            #endregion

            //sort by name 
            var result = allTourGuide.Select(cus => new TourGuide
            {
                Id = cus.Id,
                Name = cus.Name,
                Gender = cus.Gender,
                Phone = cus.Phone,
                Address = cus.Address,
                Rank = cus.Rank,
                Image = cus.Image,
                Email = cus.Email
            });

            return result.ToList();

        }
    }
}
