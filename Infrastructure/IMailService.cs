using book_store_for_developers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace book_store_for_developers.Infrastructure
{
    public interface IMailService
    {
        void SendingOrderConfirmationEmail(Order order);
        void SendingOrderCompleteEmail(Order order);
    }
}