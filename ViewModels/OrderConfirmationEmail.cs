using book_store_for_developers.Models;
using Postal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace book_store_for_developers.ViewModels
{
    public class OrderConfirmationEmail : Email
    {
        public string To { get; set; }
        public string From { get; set; }
        public decimal Value { get; set; }
        public int OrderNumber { get; set; }
        public string ImagePath { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
    public class OrderCompletedEmail : Email
    {
        public string To { get; set; }
        public string From { get; set; }
        public int OrderNumber { get; set; }
    }
}