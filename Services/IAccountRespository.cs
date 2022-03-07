using System.Collections.Generic;

namespace TravelTogether2.Models
{
    public interface IAccountRespository
    {
        List<Account> GetAll(string search, string sortby, int page = 1);
    }
}
