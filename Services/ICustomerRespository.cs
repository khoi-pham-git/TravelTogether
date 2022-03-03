using System.Collections.Generic;
using TravelTogether2.Models;

namespace TravelTogether2.Services
{
    public interface ICustomerRespository
    {
        List<Customer> GetAll(string search, string sortby, int page = 1);
    }
}
