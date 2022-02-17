using book_store_for_developers.DAL;
using book_store_for_developers.Infrastructure;
using book_store_for_developers.Models;
using book_store_for_developers.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using static book_store_for_developers.App_Start.IdentityConfig;

namespace book_store_for_developers.Controllers
{
    public class CartController : Controller
    {
        private CartMenager cartMenager;
        private ISessionMenager sessionMenager { get; set; }
        private BooksContext db;
        private IMailService mailService;

        public CartController(IMailService mailService)
        {
            this.mailService = mailService;
            db = new BooksContext();
            sessionMenager = new SessionMenager();
            cartMenager = new CartMenager(sessionMenager, db);
        }
        // GET: Cart
        public ActionResult Index()
        {
            var cartItems = cartMenager.DownloadCart();
            var totalPrice = cartMenager.DownloadCartValue();
            CartViewModel cartVM = new CartViewModel()
            {
                CartItem = cartItems,
                TotalPrice = totalPrice
            };
            return View(cartVM);
        }
        public ActionResult AddToCart(int id)
        {
            cartMenager.AddToCart(id);

            return RedirectToAction("Index");
        }

        public int DownloadNumberOfCartItems()
        {
            return cartMenager.DownloadQuantityOfCartItems();
        }

        public ActionResult RemoveFromCart(int bookId)
        {
            int numberOfItems = cartMenager.RemoveFromCart(bookId);
            int numberOfCartItems = cartMenager.DownloadQuantityOfCartItems();
            decimal cartValue = cartMenager.DownloadCartValue();

            var result = new CartRemovalViewModel
            {
                IdItemsToRemove = bookId,
                NumberItemsToRemove = numberOfItems,
                CartTotalPrice = cartValue,
                CartQuantityItem = numberOfCartItems
            };
            return Json(result);
        }
        public async Task<ActionResult> Pay()
        {
            if (Request.IsAuthenticated)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());

                var order = new Order
                {
                    Name = user.UserData.Name,
                    Surname = user.UserData.Surname,
                    Address = user.UserData.Address,
                    City = user.UserData.City,
                    PostCode = user.UserData.PostCode,
                    Email = user.UserData.Email,
                    PhoneNumber = user.UserData.PhoneNumber,
                };
                return View(order);
            }
            else
            {
                return RedirectToAction("Login", "Account", new { retrnUrl = Url.Action("Pay", "Cart") });
            }
        }
        [HttpPost]
        public async Task<ActionResult> Pay(Order orderDetails)
        {
            if (ModelState.IsValid)
            {             
                var userId = User.Identity.GetUserId();
                var newOrder = cartMenager.CreateOrder(orderDetails, userId);
                var user = await UserManager.FindByIdAsync(userId);
                TryUpdateModel(user.UserData);
                await UserManager.UpdateAsync(user);
                cartMenager.EmptyCart();

                mailService.SendingOrderConfirmationEmail(newOrder);
                return RedirectToAction("OrderConfirmation");
            }
            else
                return View(orderDetails);
        }
        public ActionResult OrderConfirmation()
        {
            return View();
        }
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
    }

}
