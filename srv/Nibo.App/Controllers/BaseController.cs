using Microsoft.AspNetCore.Mvc;
using Nibo.Business.Interfaces;

namespace Nibo.App.Controllers
{
    public class BaseController : Controller
    {
        private readonly INotification _notifier;

        protected BaseController(INotification notifier)
        {
            _notifier = notifier;
        }

        protected bool OperacaoValida()
        {
            return !_notifier.HasNotification();
        }
    }
}
