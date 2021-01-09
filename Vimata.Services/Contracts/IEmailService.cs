namespace Vimata.Services.Contracts
{
    using System.Threading.Tasks;

    public interface IEmailService
    {
        Task SendEmail(string to, string subject, string body);
    }
}