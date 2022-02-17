using book_store_for_developers.DAL;
using book_store_for_developers.Infrastructure;
using book_store_for_developers.Models;
using book_store_for_developers.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using NLog;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using static book_store_for_developers.App_Start.IdentityConfig;
using static book_store_for_developers.Models.IdentityModels;

namespace book_store_for_developers.Controllers
{
    [Authorize]

    [Authorize]
    public class ManageController : Controller
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private BooksContext db = new BooksContext();
         private IMailService mailService;
        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            Error
        }
        //public ManageController()
        //{
            
        //}
        public ManageController(BooksContext context, IMailService mailService)
        {
            this.db = context;
            this.mailService = mailService;
        }

        public ManageController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
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

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        // GET: Manage
        public async Task<ActionResult> Index(ManageMessageId? message)
        {
            var name = User.Identity.Name;
            logger.Info("Admin główna | " + name);

            if (TempData["ViewData"] != null)
            {
                ViewData = (ViewDataDictionary)TempData["ViewData"];
            }

            if (User.IsInRole("Admin"))
                ViewBag.UserIsAdmin = true;
            else
                ViewBag.UserIsAdmin = false;

            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return View("Error");
            }

            var model = new ManageCredentialsViewModel
            {
                Message = message,
                UserData = user.UserData
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangeProfile([Bind(Prefix = "UserData")] UserData userData)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                user.UserData = userData;
                var result = await UserManager.UpdateAsync(user);

                AddErrors(result);
            }

            if (!ModelState.IsValid)
            {
                TempData["ViewData"] = ViewData;
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword([Bind(Prefix = "ChangePasswordViewModel")] ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["ViewData"] = ViewData;
                return RedirectToAction("Index");
            }

            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInAsync(user, isPersistent: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
            }
            AddErrors(result);

            if (!ModelState.IsValid)
            {
                TempData["ViewData"] = ViewData;
                return RedirectToAction("Index");
            }

            var message = ManageMessageId.ChangePasswordSuccess;
            return RedirectToAction("Index", new { Message = message });
        }


        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("password-error", error);
            }
        }

        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie, DefaultAuthenticationTypes.TwoFactorCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = isPersistent }, await user.GenerateUserIdentityAsync(UserManager));
        }

        public ActionResult ListOrders()
        {
            var name = User.Identity.Name;
            logger.Info("Admin zamowienia | " + name);

            bool isAdmin = User.IsInRole("Admin");
            ViewBag.UserIsAdmin = isAdmin;

            IEnumerable<Order> userOrders;

          
            if (isAdmin)
            {
                userOrders = db.Orders.Include("OrderItems").OrderByDescending(o => o.DateAdded).ToArray();
            }
            else
            {
                var userId = User.Identity.GetUserId();
                userOrders = db.Orders.Where(o => o.UserId == userId).Include("OrderItems").OrderByDescending(o => o.DateAdded).ToArray();
            }

            return View(userOrders);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public OrderStatus OrderStatusChange(Order order)
        {
            Order OrderToModification = db.Orders.Find(order.OrderId);
            OrderToModification.OrderStatus = order.OrderStatus;
            db.SaveChanges();

            if (OrderToModification.OrderStatus == OrderStatus.Realized)
            {
                this.mailService.SendingOrderCompleteEmail(OrderToModification);
            }

            return order.OrderStatus;
        }

        [Authorize(Roles = "Admin")]
        public ActionResult AddBook(int? bookId, bool? confirmation)
        {
            Book book;
            if (bookId.HasValue)
            {
                ViewBag.EditMode = true;
                book = db.Books.Find(bookId);
            }
            else
            {
                ViewBag.EditMode = false;
                book = new Book();
            }

            var result = new EditKursViewModel();
            result.Categories = db.Categories.ToList();
            result.Book = book;
            result.Confirmation = confirmation;

            return View(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AddBook(EditKursViewModel model, HttpPostedFileBase file)
        {
            if (model.Book.BookId > 0)
            {
                // modyfikacja kursu
                db.Entry(model.Book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("AddBook", new { confirmation = true });
            }
            else
            {
                // Sprawdzenie, czy użytkownik wybrał plik
                if (file != null && file.ContentLength > 0)
                {
                    if (ModelState.IsValid)
                    {
                        // Generowanie pliku
                        var fileExt = Path.GetExtension(file.FileName);
                        var filename = Guid.NewGuid() + fileExt;

                        var path = Path.Combine(Server.MapPath(AppConfig.__RelativeBooksFolder), filename);
                        file.SaveAs(path);

                        model.Book.ImageFileName = filename;
                        model.Book.DateAdded = DateTime.Now;

                        db.Entry(model.Book).State = EntityState.Added;
                        db.SaveChanges();

                        return RedirectToAction("AddBook", new { confirmation = true });
                    }
                    else
                    {
                        var categories = db.Categories.ToList();
                        model.Categories = categories;
                        return View(model);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "File not specified");
                    var categories = db.Categories.ToList();
                    model.Categories = categories;
                    return View(model);
                }
            }

        }

        [Authorize(Roles = "Admin")]
        public ActionResult HideBook(int bookId)
        {
            var book = db.Books.Find(bookId);
            book.Hidden = true;
            db.SaveChanges();

            return RedirectToAction("AddBook", new { confirmation = true });
        }

        [Authorize(Roles = "Admin")]
        public ActionResult ShowBook(int bookId)
        {
            var book = db.Books.Find(bookId);
            book.Hidden = false;
            db.SaveChanges();

            return RedirectToAction("AddBook", new { confirmation = true });
        }

        [AllowAnonymous]
        public ActionResult SendingOrderConfirmationEmail(int orderId, string surname)
        {
            var order = db.Orders.Include("OrderItems").Include("OrderItems.Book")
                               .SingleOrDefault(o => o.OrderId == orderId && o.Surname == surname);

            if (order == null) return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            OrderConfirmationEmail email = new OrderConfirmationEmail();
            email.To = order.Email;
            email.From = "andrzejmroz1999@gmail.com.";
            email.Value = order.OrderValue;
            email.OrderNumber = order.OrderId;
            email.OrderItems = order.OrderItems;
            email.ImagePath = AppConfig.__RelativeBooksFolder;
            email.Send();

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [AllowAnonymous]
        public ActionResult SendingOrderCompleteEmail(int orderId, string surname)
        {
            var order = db.Orders.Include("OrderItems").Include("OrderItems.Book")
                                  .SingleOrDefault(o => o.OrderId == orderId && o.Surname == surname);

            if (order == null) return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            OrderCompletedEmail email = new OrderCompletedEmail();
            email.To = order.Email;
            email.From = "andrzejmroz1999@gmail.com";
            email.OrderNumber = order.OrderId;
            email.Send();

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

    }
   
}