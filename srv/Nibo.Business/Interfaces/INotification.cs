using Nibo.Business.Notifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nibo.Business.Interfaces
{
    public interface  INotification
    {
        bool HasNotification();
        List<Notification> GetNotification();
        void Handle(Notification notificacao);
    }
}
