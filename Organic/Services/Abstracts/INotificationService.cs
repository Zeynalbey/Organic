namespace Organic.Services.Concretes
{
    public interface INotificationService
    {
        Task SenOrderCreatedToAdmin(string trackingCode);
    }
}
