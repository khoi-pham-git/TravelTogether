using System.Collections.Generic;
using System.Linq;
using TravelTogether2.Models;

namespace TravelTogether2.Services
{
    public class TourRespository : ITourRespository
    {
        private readonly TourGuide_v2Context _context;
        public static int PAGE_SIZE { get; set; } = 5;

        public TourRespository(TourGuide_v2Context context)
        {
            _context = context;
        }
        public List<Tour> GetAll(string search, string sortby, int page = 1)
        {
            var allTour = _context.Tours.AsQueryable();


            #region Fillter (search)


            if (!string.IsNullOrEmpty(search))
            {
                allTour = allTour.Where(cus => cus.Name.Contains(search));
            }


            //allCusomer = allCusomer.OrderBy(cus => cus.Email);
            if (!string.IsNullOrEmpty(sortby))
            {
                switch (sortby)
                {
                    case "Name_desc": allTour = allTour.OrderByDescending(cus => cus.Name); break;
                    case "Id_desc": allTour = allTour.OrderByDescending(cus => cus.Id); break;
                }
            }

            allTour = allTour.Skip((page - 1) * PAGE_SIZE).Take(PAGE_SIZE);

            #endregion

            //sort by name 
            var result = allTour.Select(cus => new Tour
            
            {
                Id = cus.Id,
                Name = cus.Name,
                QuatityTrip = cus.QuatityTrip,
                Price = cus.Price,
                Status = cus.Status,
                TourGuideId = cus.TourGuideId

            });

            return result.ToList();
        }
    }
}

