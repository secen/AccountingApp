using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AccountingAppEF2.DBA;

namespace AccountingAppEF2.Controllers
{
    public class StocksController : Controller
    {
        private AccountingDBEntities db = new AccountingDBEntities();

        // GET: Stocks
        public ActionResult Index()
        {
            HttpCookie myCookie = Request.Cookies["myCookie"];
            string tok = myCookie.Values["token"];
            if (tok == null)
                return RedirectToAction("Login", "Users");
            User usr = db.Users.Where(x => x.token == tok).FirstOrDefault();
            if (usr == null)
                return RedirectToAction("Login", "Users");

            int usrID = usr.id;
            return View(db.Stocks.Where(x=>x.UserId == usrID).ToList());
        }

        public ActionResult View()
        {
            HttpCookie myCookie = Request.Cookies["myCookie"];
            string tok = myCookie.Values["token"];
            if (tok == null)
                return RedirectToAction("Login", "Users");
            User usr = db.Users.Where(x => x.token == tok).FirstOrDefault();
            if (usr == null)
                return RedirectToAction("Login", "Users");

            int usrID = usr.id;
            return View(db.Stocks.Where(x => x.UserId == usrID).ToList());
        }

        // GET: Stocks/Details/5
        public ActionResult Details(int? id)
        {
            HttpCookie myCookie = Request.Cookies["myCookie"];
            string tok = myCookie.Values["token"];
            if (tok == null)
                return RedirectToAction("Login", "Users");
            User usr = db.Users.Where(x => x.token == tok).FirstOrDefault();
            if (usr == null)
                return RedirectToAction("Login", "Users");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stock stock = db.Stocks.Find(id);
            if (stock == null)
            {
                return HttpNotFound();
            }
            return View(stock);
        }

        // GET: Stocks/Create
        public ActionResult Create()
        {
            return View(new Stock());
        }

        // POST: Stocks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Acronym,Value,Category")] Stock stock)
        {
            HttpCookie myCookie = Request.Cookies["myCookie"];
            string tok = myCookie.Values["token"];
            if (tok == null)
                return RedirectToAction("Login", "Users");
            User usr = db.Users.Where(x => x.token == tok).FirstOrDefault();
            if (usr == null)
                return RedirectToAction("Login", "Users");

            if (ModelState.IsValid)
            {
                stock.UserId = usr.id;
                db.Stocks.Add(stock);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(stock);
        }

        // GET: Stocks/Edit/5
        public ActionResult Edit(int? id)
        {
            HttpCookie myCookie = Request.Cookies["myCookie"];
            string tok = myCookie.Values["token"];
            if (tok == null)
                return RedirectToAction("Login", "Users");
            User usr = db.Users.Where(x => x.token == tok).FirstOrDefault();
            if (usr == null)
                return RedirectToAction("Login", "Users");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stock stock = db.Stocks.Find(id);
            if (stock == null)
            {
                return HttpNotFound();
            }
            return View(stock);
        }

        // POST: Stocks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Acronym,Value,Category")] Stock stock)
        {
            HttpCookie myCookie = Request.Cookies["myCookie"];
            string tok = myCookie.Values["token"];
            if (tok == null)
                return RedirectToAction("Login", "Users");
            User usr = db.Users.Where(x => x.token == tok).FirstOrDefault();
            if (usr == null)
                return RedirectToAction("Login", "Users");

            if (ModelState.IsValid)
            {
                db.Entry(stock).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(stock);
        }

        // GET: Stocks/Delete/5
        public ActionResult Delete(int? id)
        {
            HttpCookie myCookie = Request.Cookies["myCookie"];
            string tok = myCookie.Values["token"];
            if (tok == null)
                return RedirectToAction("Login", "Users");
            User usr = db.Users.Where(x => x.token == tok).FirstOrDefault();
            if (usr == null)
                return RedirectToAction("Login", "Users");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stock stock = db.Stocks.Find(id);
            if (stock == null)
            {
                return HttpNotFound();
            }
            return View(stock);
        }

        // POST: Stocks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Stock stock = db.Stocks.Find(id);
            db.Stocks.Remove(stock);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
