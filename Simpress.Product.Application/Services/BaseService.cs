using FluentValidation;
using FluentValidation.Results;
using Simpress.Product.Domain.Entities;
using Simpress.Product.Domain.Entities.Notifications;
using Simpress.Product.Domain.Services;

namespace Simpress.Product.Application.Services
{
    public abstract class BaseService
    {
        private readonly INotificator _notificator;

        protected BaseService(INotificator notificator)
        {
            _notificator = notificator;
        }

        protected void Notificate(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors) Notificate(error.ErrorMessage);
            
        }

        protected void Notificate(string errorMessage)
        {
            _notificator.HandleNotificiation(new Notification(errorMessage));
        }

        protected bool ExecuteValidation<TV, TE>(TV validation, TE entity) where TV: AbstractValidator<TE> where TE: BaseEntity
        {
            var validator = validation.Validate(entity);

            if (validator.IsValid) return true;

            Notificate(validator);

            return false;
        }
    }
}
