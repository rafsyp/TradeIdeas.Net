using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using TradeIdeasNet.Models;

namespace TradeIdeas.Controllers.Api
{

    //Api endpoints controller.  GetTrades populates list to the view in Views/Trade/List  Create Trade adds a trade passed along as a json object

    [Authorize]
    public class TradeController : ApiController
    {
        private ApplicationDbContext _context;
        private UserManager<ApplicationUser> _UserManager;

        public TradeController()
        {
            _context = new ApplicationDbContext();
            _UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_context));
        }

        public IEnumerable<Trade> GetTrades()
        {

            string userid = User.Identity.GetUserId();
            Console.Write(userid);
            IEnumerable<Trade> trades = new List<Trade>();
            if (!string.IsNullOrEmpty(userid))
            {
                trades = _context.Trades.Where(x => x.ApplicationUserID == userid).ToList();
            }

            return trades;

        }

        public IHttpActionResult GetTradebyID(int id)
        {
            var trade = _context.Trades.SingleOrDefault(c => c.Id == id);

            if (trade == null)
                return NotFound();

            return Ok(trade);
        }

        [HttpPost]
        public IHttpActionResult CreateTrade(JObject jsonResult)
        {

            Trade trade = JsonConvert.DeserializeObject<Trade>(jsonResult.ToString());

            string userid = User.Identity.GetUserId();
            ApplicationUser user = _UserManager.FindById(userid);

            trade.ApplicationUser = user;
            trade.ApplicationUserID = userid;

            _context.Trades.Add(trade);
            _context.SaveChanges();

            return Created(new Uri(Request.RequestUri + "/" + trade.Id), trade);
        }

        [HttpDelete]
        public IHttpActionResult DeleteTrade(int id)
        {
            var trade = _context.Trades.SingleOrDefault(c => c.Id == id);

            if (trade == null)
                return NotFound();

            _context.Trades.Remove(trade);
            _context.SaveChanges();

            return Ok();
        }
    }
}
