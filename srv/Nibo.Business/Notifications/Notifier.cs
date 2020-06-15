using Nibo.Business.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Nibo.Business.Notifications
{
    public class Notifier : INotification
    {
        private List<Notification> _notify;

        public Notifier()
        {
            _notify = new List<Notification>();
        }

        public List<Notification> GetNotification()
        {
            return _notify;
        }

        public void Handle(Notification notificacao)
        {
            _notify.Add(notificacao);
        }

        public bool HasNotification()
        {
            return _notify.Any();
        }
    }
}
