using Organic.Contracts.Email;

namespace Organic.Services.Abstracts
{
    public interface IEmailService
    {
        public void Send(MessageDto messageDto);
    }
}
