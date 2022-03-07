using System.Threading.Tasks;
using TravelTogether2.Models;

namespace TravelTogether2.Services
{
    public interface IMailService
    {
        Task SendCodeEmailAsync(MailRequest mailRequest);
    }
}
