using Simpress.Product.Domain.Entities.Notifications;

namespace Simpress.Product.Domain.Services
{
    public interface INotificator
    {
        bool HasNotification();
        List<Notification> GetAllNotifications();
        void HandleNotificiation(Notification notification);
    }
}
