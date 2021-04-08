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
    public class ExpensesController : Controller
    {
        private AccountingDBEntities db = new AccountingDBEntities();

        // GET: Expenses
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
            return View(db.Expenses.Where(x=>x.UserId == usrID).ToList());
        }

        // GET: Expenses/Details/5
        public ActionResult Details(int? id)
        {
            HttpCookie myCookie = Request.Cookies["myCookie"];
            string tok = myCookie.Values["token"];
            if (tok == null)
                return RedirectToAction("Login", "Users");

            User usr = db.Users.Where(x => x.token == tok).FirstOrDefault();
            if (usr == null)
                return RedirectToAction("Login", "Users");
            int usrID = usr.id;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expense expense = db.Expenses.Find(id);
            if (expense == null)
            {
                return HttpNotFound();
            }
            return View(expense);
        }

        // GET: Expenses/Create
        public ActionResult Create()
        {
            HttpCookie myCookie = Request.Cookies["myCookie"];
            string tok = myCookie.Values["token"];
            if (tok == null)
                return RedirectToAction("Login", "Users");

            User usr = db.Users.Where(x => x.token == tok).FirstOrDefault();
            if (usr == null)
                return RedirectToAction("Login", "Users");
            int usrID = usr.id;
            return View();
        }

        // POST: Expenses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Value,Date")] Expense expense)
        {
            HttpCookie myCookie = Request.Cookies["myCookie"];
            string tok = myCookie.Values["token"];
            if (tok == null)
                return RedirectToAction("Login", "Users");
            User usr = db.Users.Where(x => x.token == tok).FirstOrDefault();
            if (usr == null)
                return RedirectToAction("Login", "Users");

            int usrID = usr.id;
            if (ModelState.IsValid)
            {
                expense.UserId = usrID;
                db.Expenses.Add(expense);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(expense);
        }

        // GET: Expenses/Edit/5
        public ActionResult Edit(int? id)
        {
            HttpCookie myCookie = Request.Cookies["myCookie"];
            string tok = myCookie.Values["token"];
            if (tok == null)
                return RedirectToAction("Login", "Users");
            User usr = db.Users.Where(x => x.token == tok).FirstOrDefault();
            if (usr == null)
                return RedirectToAction("Login", "Users");

            int usrID = usr.id;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expense expense = db.Expenses.Find(id);
            if (expense == null)
            {
                return HttpNotFound();
            }
            return View(expense);
        }

        // POST: Expenses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Value,Date")] Expense expense)
        {
            HttpCookie myCookie = Request.Cookies["myCookie"];
            string tok = myCookie.Values["token"];
            if (tok == null)
                return RedirectToAction("Login", "Users");
            User usr = db.Users.Where(x => x.token == tok).FirstOrDefault();
            if (usr == null)
                return RedirectToAction("Login", "Users");

            int usrID = usr.id;
            if (ModelState.IsValid)
            {
                db.Entry(expense).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(expense);
        }

        // GET: Expenses/Delete/5
        public ActionResult Delete(int? id)
        {
            HttpCookie myCookie = Request.Cookies["myCookie"];
            string tok = myCookie.Values["token"];
            if (tok == null)
                return RedirectToAction("Login", "Users");
            User usr = db.Users.Where(x => x.token == tok).FirstOrDefault();
            if (usr == null)
                return RedirectToAction("Login", "Users");

            int usrID = usr.id;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expense expense = db.Expenses.Find(id);
            if (expense == null)
            {
                return HttpNotFound();
            }
            return View(expense);
        }

        // POST: Expenses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Expense expense = db.Expenses.Find(id);
            db.Expenses.Remove(expense);
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
