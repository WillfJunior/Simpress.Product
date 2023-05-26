namespace Simpress.Product.Domain.Entities.Notifications
{
    public class Notification
    {
        public string? Message { get; }

        public Notification(string? message)
        {
            Message = message;
        }
    }
}
