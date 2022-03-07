using System.Collections.Generic;
using System.Linq;
using TravelTogether2.Models;

namespace TravelTogether2.Services
{
    public class AreasReponsitory : IAreasResponsitory
    {

        private readonly TourGuide_v2Context _context;
        public static int PAGE_SIZE { get; set; } = 5;

        public AreasReponsitory(TourGuide_v2Context context)
        {
            _context = context;
        }




        public List<Area> GetAll(string search, string sortby, int page=1)
        {

            var allArea = _context.Areas.AsQueryable();


            #region Fillter (search)


            if (!string.IsNullOrEmpty(search))
            {
                allArea = allArea.Where(area => area.Name.Contains(search));
            }


            //allCusomer = allCusomer.OrderBy(cus => cus.Email);
            if (!string.IsNullOrEmpty(sortby))
            {
                switch (sortby)
                {
                    case "Name_desc": allArea = allArea.OrderByDescending(cus => cus.Name); break;
                    case "Name_asc": allArea = allArea.OrderBy(cus => cus.Name); break;
                    case "Id_desc": allArea = allArea.OrderByDescending(cus => cus.Id); break;
                    case "Id_asc": allArea = allArea.OrderBy(cus => cus.Id); break;
                }
            }

            allArea = allArea.Skip((page - 1) * PAGE_SIZE).Take(PAGE_SIZE);

            #endregion

            //sort by name 
            var result = allArea.Select(cus => new Area
            {
                Id = cus.Id,
                Name = cus.Name,
                Description = cus.Description,
                Latitude = cus.Latitude,
                Longtitude = cus.Longtitude,
                TravelAgencyId  = cus.TravelAgencyId
                
            });

            return result.ToList();
        }
    }
}
