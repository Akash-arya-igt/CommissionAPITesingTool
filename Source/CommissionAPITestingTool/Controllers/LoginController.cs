using System;
using System.Web.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using CommissionAPITestingTool.Models;

namespace CommissionAPITestingTool.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        // POST: Login/Create
        [HttpPost]
        public ActionResult Index(LoginModel login)
        {
            try
            {
                string strSessionKey = string.Empty;
                string URLvalue = System.Configuration.ConfigurationManager.AppSettings["CommAPIUrl"];
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(URLvalue);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                    var resp = client.GetAsync(URLvalue + "Login?userID=" + login.UserId + "&password=" + login.Password).Result;
                    if (resp.IsSuccessStatusCode)
                    {
                        //var shoppingResp = resp.
                        strSessionKey = resp.Content.ReadAsAsync<string>().Result;
                        return RedirectToAction("Index", "Home", new { sessionkey = strSessionKey });
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid login attempt.");
                        return View(login);
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(login);
            }
        }
    }
}