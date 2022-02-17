using book_store_for_developers.DAL;
using book_store_for_developers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace book_store_for_developers.Infrastructure
{
    public class CartMenager
    {
        private BooksContext db;
        private ISessionMenager session;
        public CartMenager(ISessionMenager session, BooksContext db)
        {
            this.session = session;
            this.db = db;
        }
        public List<CartItem> DownloadCart()
        {
            List<CartItem> cart;
            if (session.Get<List<CartItem>>(Consts.CartSessionKey) == null)
            {
                cart = new List<CartItem>();
            }
            else
            {
                cart = session.Get<List<CartItem>>(Consts.CartSessionKey) as List<CartItem>;
            }

            return cart;
        }
        public void AddToCart(int bookId)
        {
            var cart = DownloadCart();
            var cartItem = cart.Find(k => k.book.BookId == bookId);

            if (cartItem != null)
            {
                cartItem.Quantity++;
            }
            else
            {
                var bookToAdded = db.Books.Where(k => k.BookId == bookId).SingleOrDefault();
                if (bookToAdded != null)
                {
                    var newCartItem = new CartItem()
                    {
                        book = bookToAdded,
                        Quantity = 1,
                        Value = bookToAdded.BookPrice
                    };
                    cart.Add(newCartItem);
                }
            }
            session.Set(Consts.CartSessionKey, cart);
        }
        public int RemoveFromCart(int kursId)
        {
            var cart = DownloadCart();
            var cartItem = cart.Find(k => k.book.BookId == kursId);
            if (cartItem != null)
            {
                if (cartItem.Quantity > 1)
                {
                    cartItem.Quantity--;
                    return cartItem.Quantity;
                }
                else
                {
                    cart.Remove(cartItem);
                }
            }
            return 0;
        }
        public decimal DownloadCartValue()
        {
            var cart = DownloadCart();
            return cart.Sum(k => (k.Quantity * k.book.BookPrice));
        }
        public int DownloadQuantityOfCartItems()
        {
            var cart = DownloadCart();
            int quantity = cart.Sum(k => k.Quantity);
            return quantity;
        }
        public Order CreateOrder(Order newOrder,string userId)
        {
            var koszyk = DownloadCart();
            newOrder.DateAdded = DateTime.Now;
            newOrder.UserId = userId;
            db.Orders.Add(newOrder);

            if (newOrder.OrderItems == null)
            {
                newOrder.OrderItems = new List<OrderItem>();
            }
            decimal cartvalue = 0;
            foreach (var cartItem in koszyk)
            {
                var newOrderItem = new OrderItem()
                {
                    BookId = cartItem.book.BookId,
                    Quantity = cartItem.Quantity,
                    PurchasePrice = cartItem.book.BookPrice,
                };
                cartvalue += (cartItem.Quantity * cartItem.book.BookPrice);
                newOrder.OrderItems.Add(newOrderItem);
            }
            newOrder.OrderValue = cartvalue;
            db.SaveChanges();

            return newOrder;
        }
        public void EmptyCart()
        {
            session.Set<List<CartItem>>(Consts.CartSessionKey, null);
        }
    }
}