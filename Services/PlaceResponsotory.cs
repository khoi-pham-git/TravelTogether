using System.Collections.Generic;
using System.Linq;
using TravelTogether2.Models;

namespace TravelTogether2.Services
{

    public class PlaceResponsotory : IPlaceResponsotory
    {
        private readonly TourGuide_v2Context _context;
        public static int PAGE_SIZE { get; set; } = 5;

        public PlaceResponsotory(TourGuide_v2Context context)
        {
            _context = context;
        }
        public List<Place> GetAll(string search, string sortby, int page = 1)
        {
            var allPlace = _context.Places.AsQueryable();


            #region Fillter (search)


            if (!string.IsNullOrEmpty(search))
            {
                allPlace = allPlace.Where(place => place.Name.Contains(search));
            }


            //allCusomer = allCusomer.OrderBy(cus => cus.Email);
            if (!string.IsNullOrEmpty(sortby))
            {
                switch (sortby)
                {
                    case "Name_desc": allPlace = allPlace.OrderByDescending(place => place.Name); break;
                    case "Id_desc": allPlace = allPlace.OrderByDescending(place => place.Id); break;
                    case "Description_desc": allPlace = allPlace.OrderByDescending(place => place.Description); break;
                    case "AreaId_desc": allPlace = allPlace.OrderByDescending(place => place.AreaId); break;
                    case "Address_desc": allPlace = allPlace.OrderByDescending(place => place.Address); break;
                    case "CategoryId_desc": allPlace = allPlace.OrderByDescending(place => place.CategoryId); break;
                }
            }

            allPlace = allPlace.Skip((page - 1) * PAGE_SIZE).Take(PAGE_SIZE);

            #endregion

            //sort by name 
            var result = allPlace.Select(place => new Place
            {
                Id = place.Id,
                Name = place.Name,
                Address = place.Address,
                Description = place.Description,
                Longtitude = place.Longtitude,
                Latitude = place.Latitude,
                AreaId = place.AreaId,
                CategoryId = place.CategoryId
            });

            return result.ToList();
        }
    }
}

