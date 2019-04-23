
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using TradeIdeasNet.Models;

namespace Vidly.Controllers
{

    //Controller which controls the displaying of trade views to the user for viewing and manual crud operations

    [Authorize]
    public class TradeController : Controller
    {
        private ApplicationDbContext _context;
        private UserManager<ApplicationUser> UserManager;

        public TradeController()
        {
            _context = new ApplicationDbContext();
            UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_context));
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ViewResult Index()
        {
            return View("List"); 
        }

        public ActionResult New()
        {
            var viewModel = new TradeFormViewModel();

            return View("TradeForm", viewModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Trade trade)
        {
            string userid = User.Identity.GetUserId();
            ApplicationUser user = UserManager.FindById(userid);

            if (!ModelState.IsValid)
              {
                var viewModel = new TradeFormViewModel(trade);
                return View("TradeForm", viewModel);
              }

            if (trade.Id == 0)
            {
                trade.DateAdded = DateTime.Now;
                trade.ApplicationUser = user;
                trade.ApplicationUserID = userid;
                _context.Trades.Add(trade);
            }
            else
            {
                var editTrade = _context.Trades.Single(m => m.Id == trade.Id);
                editTrade.Ticker = trade.Ticker;
                editTrade.Tradetype = trade.Tradetype;
                editTrade.Comment = trade.Comment;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Trade");
        }

        public ActionResult Edit(int id)
        {
            var trade = _context.Trades.SingleOrDefault(c => c.Id == id);
            var viewModel = new TradeFormViewModel(trade);

            if (trade == null)
                return HttpNotFound();

            return View("TradeForm", viewModel);
        }


        public ActionResult Details(int id)
        {
            var trade = _context.Trades.SingleOrDefault(m => m.Id == id);

            if (trade == null)
                return HttpNotFound();

            return View(trade);

        }

    }
}