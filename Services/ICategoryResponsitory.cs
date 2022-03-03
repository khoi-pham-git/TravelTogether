using System.Collections.Generic;
using TravelTogether2.Models;

namespace TravelTogether2.Services
{
    public interface ICategoryResponsitory
    {
        List<Category> GetAll(string search, string sortby, int page = 1);
    }
}
