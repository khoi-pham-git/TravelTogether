using System.Collections.Generic;
using System.Linq;
using TravelTogether2.Models;

namespace TravelTogether2.Services
{
    public class LanguageRespository : ILanguageRespository
    {
        private readonly TourGuide_v2Context _context;
        public static int PAGE_SIZE { get; set; } = 5;

        public LanguageRespository(TourGuide_v2Context context)
        {
            _context = context;
        }
        public List<Language> GetAll(string search, string sortby, int page = 1)
        {

            var alllanguage = _context.Languages.AsQueryable();


            #region Fillter (search)


            if (!string.IsNullOrEmpty(search))
            {
                alllanguage = alllanguage.Where(language => language.Language1.Contains(search));
            }


            //allCusomer = allCusomer.OrderBy(cus => cus.Email);
            if (!string.IsNullOrEmpty(sortby))
            {
                switch (sortby)
                {
                    case "Name_desc": alllanguage = alllanguage.OrderByDescending(language => language.Language1); break;
                    case "Id_desc": alllanguage = alllanguage.OrderByDescending(language => language.Id); break;
                    case "Level_desc": alllanguage = alllanguage.OrderByDescending(language => language.Level); break;
                }
            }

            alllanguage = alllanguage.Skip((page - 1) * PAGE_SIZE).Take(PAGE_SIZE);

            #endregion

            //sort by name 
            var result = alllanguage.Select(language => new Language
            {
              Id = language.Id,
              Language1 = language.Language1,
              Level = language.Level
            });

            return result.ToList();

        }
    }
}
