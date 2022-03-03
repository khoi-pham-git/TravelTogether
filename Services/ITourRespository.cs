using System.Collections.Generic;
using TravelTogether2.Models;

namespace TravelTogether2.Services
{
    public interface ITourRespository
    {
        List<Tour> GetAll(string search, string sortby, int page = 1);

    }
}
