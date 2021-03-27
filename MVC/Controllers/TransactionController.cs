using MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class TransactionController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();

        // GET Transaction/Create
        public ActionResult Create()
        {
            ViewData["Products"] = _db.Products.Select(p
                => new SelectListItem
                {
                    Text = p.Name,
                    Value = p.ProductId.ToString()
                });
            ViewData["Customers"] = _db.Customers.Select(c
                => new SelectListItem
                {
                    Text = c.FirstName + " " + c.LastName,
                    Value = c.CustomerId.ToString()
                });
            return View();
        }
        // POST Transaction/Create
        [HttpPost]
        public ActionResult Create(Transaction model)
        {
            model.DateOfTransaction = DateTimeOffset.Now;

            var createdObj =_db.Transactions.Add(model);

            if (_db.SaveChanges() == 1)
            {
                return RedirectToAction("Index");
                //return Redirect("/transaction/" + createdObj.TransactionId);
            }
            // ViewData["ErrMessage"]
            return View(model);
        }
        public ActionResult Index()
        {
            return View(_db.Transactions.ToArray());
        }
    }
}