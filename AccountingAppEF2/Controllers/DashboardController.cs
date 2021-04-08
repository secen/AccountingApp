using AccountingAppEF2.DBA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AccountingAppEF2.Controllers
{
    public class DashboardController : Controller
    {
        private AccountingDBEntities db = new AccountingDBEntities();
        // GET: Dashboard
        public ActionResult Index()
        {
            HttpCookie myCookie = Request.Cookies["myCookie"];
            if (myCookie == null)
            {
                return RedirectToAction("Login", "Users");
                //No cookie found or cookie expired.
                //Handle the situation here, Redirect the user or simply return;
            }
            //ok - cookie is found.
            //Gracefully check if the cookie has the key-value as expected.
            if (!string.IsNullOrEmpty(myCookie.Values["token"]))
            {
                string tokenToFind = myCookie.Values["token"];
                User usr = db.Users.Where(x => x.token == tokenToFind).FirstOrDefault();
                if(usr!=null)
                    return View(usr);
                //Yes userId is found. Mission accomplished.
            }
            return View();
        }
    }
}