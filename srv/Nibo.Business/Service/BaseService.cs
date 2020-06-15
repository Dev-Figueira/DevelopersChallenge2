using FluentValidation;
using FluentValidation.Results;
using Nibo.Business.Interfaces;
using Nibo.Business.Models;
using Nibo.Business.Notifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nibo.Business.Service
{
    public abstract class BaseService
    {
        private readonly INotification notifier;

        protected BaseService(INotification notificador)
        {
            notifier = notificador;
        }

        protected void Notify(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                Notify(error.ErrorMessage);
            }
        }

        protected void Notify(string mensagem)
        {
            notifier.Handle(new Notification(mensagem));
        }

        protected bool ExecutionValidations<TV, TE>(TV validacao, TE entidade) where TV : AbstractValidator<TE> where TE : Entity
        {
            var validator = validacao.Validate(entidade);

            if (validator.IsValid) return true;

            Notify(validator);

            return false;
        }

    }
}
