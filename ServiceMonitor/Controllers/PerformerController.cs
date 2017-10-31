using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;
using ServiceMonitor.Models.Performer;
using ServiceMonitor.Principal;
using ServiceMonitor.Repositories;
using ServiceMonitor.Repositories.Interface;

namespace ServiceMonitor.Controllers
{
 //   [Authorize]
    public class PerformerController : Controller
    {
        private readonly IPerformerRepositories _repositories;

        public PerformerController(IPerformerRepositories repositories)
        {
            _repositories = repositories;
        }

        [AllowAnonymous]
        // GET: Performer
        public ActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Index(PerformerWelcomeViewModel model)
        {
            // zmiana spssobu logowania Forms + ciastczko
            //if (ModelState.IsValid)
            //{
            //    if (_repositories.CheckPerformer(model))
            //    {
            //        return RedirectToAction("WorkList", new {login=model.Login});
            //    }
            //}

            //ModelState.AddModelError("Błędne dane", "Wprowadz poprawne dane");
            //return View(model);

            if (ModelState.IsValid)
            {
                var authenticatedUser = _repositories.AuthenticateUser(model);
                if (authenticatedUser != null)
                {
                    FormsAuthentication.SetAuthCookie(model.Login, true);
                    // FormsAuthentication.
                    // var aaa = Request.Cookies["login"].Value = model.Login;
                  
                    Session.Add("login", model.Login);
                    
                    return RedirectToAction("WorkList2");//, new { login = model.Login });  
                }                
            }
 
            ModelState.AddModelError("Błędne dane", "Wprowadz poprawne dane");
            return View(model);
        }

        public ActionResult LogOut()
        {
            if (Session != null)
            {
                FormsAuthentication.SignOut();
                Session.Clear();
            }

            return RedirectToAction("Index");
        }

        public void CreateAutenticationTicket(string userLogin)
        {
            var authUser = _repositories.FindUser(userLogin);

            CustomerPrincipalSerializedModel serializedModel = new CustomerPrincipalSerializedModel();

            serializedModel.Login = authUser;
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            string userData = serializer.Serialize(serializedModel);

            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                1, userLogin, DateTime.Now, DateTime.Now.AddMinutes(5), false, userData);

            string encTicket = FormsAuthentication.Encrypt(authTicket);

            HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);

            Response.Cookies.Add(faCookie);
        }       // nie dokonczone

        //public ActionResult ExternalLoginCallback(string returnUrl)
        //{            
        //}

        [AllowAnonymous]
        public ActionResult Registration()
        {
            var voivoderships = _repositories.GetVoivodeshipList();

            var model = new PerformerRejestractionViewModel
            {
               Voivoderships = voivoderships
            };

            return View(model);
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Registration(PerformerRejestractionViewModel model)// PerformerCreateNewPerformer
        {
            if (ModelState.IsValid)
            {
                if (_repositories.IsExist(model))
                {
                    ModelState.AddModelError("", "Wykonawca o takim loginie istnieje");
                    return View(model);
                }

                _repositories.SaveNewPerformer(model);
            
            // + zapis zwroci true
                return RedirectToAction("Index");
            }
            return View(model);
        }
        
        public ActionResult WorkList(string login)//PerformerWelcomeViewModel model)
        {
            var items = _repositories.GetWorkList(login);//model);
            return View(items);
        }

        public ActionResult WorkList2()//PerformerWelcomeViewModel model)
        {
            if (HttpContext.Session["login"] as string != null)
            {
                var items = _repositories.GetWorkList(HttpContext.Session["login"] as string);//Request.Cookies["login"].ToString());//model);         
                return View(items);
            }
            return null;
            
        }

        public ActionResult AddClient()
        {
           // User.Identity.Name
            var items = _repositories.GetVoivodeshipList();
            var model = new PerformerCreateNewCustomer
            {
                Voivoderships = items
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult AddClient(PerformerCreateNewCustomer model)
        {
            if (ModelState.IsValid)
            {
                if (_repositories.CustomerIsExist(model))
                {
                    ModelState.AddModelError("","Klient o podanych danych jest zapisany w bazie danych");
                    return View(model);
                }
                _repositories.SaveNewClient(model);
                return RedirectToAction("WorkList2");
            }

            ModelState.AddModelError("","Nie wszystkie dane zostały wprowadzone");
            return View(model);
        }

        public ActionResult AddServices()
        {
            var customers = _repositories.GetCustomerList();
            var types = _repositories.GetTypesList();
            var offices = _repositories.GetPODGiKs();
            var voivodeships = _repositories.GetVoivodeshipList();

            var model= new PerformerAddNewService
            {
                Podgiks = offices,
                Customers = customers,
                Types = types,
                Voivoderships = voivodeships        // zastanow się czy nie JS na podstawie PODGIK
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult AddServices(PerformerAddNewService model)
        {
            if (ModelState.IsValid)
            {
                model.LoginPerformer = HttpContext.Session["login"] as string;
                if (_repositories.ServiceIsExist(model))
                {
                    ModelState.AddModelError("", "Usługa o podanych danych jest już zapisana w bazie danych");
                    return View(model);
                }
                _repositories.SaveNewService(model);   
                return RedirectToAction("WorkList2"); // dopytać jak trzymać login 
            }
            return View(model);
        }        


        [HttpPost]
        public ActionResult Delete (int id)
        {
            _repositories.DeleteWork(id);
            return RedirectToAction("WorkList2");
        }

        public ActionResult Details (int id)
        {
            var model = new PerformerDetailsViewModel
            {
                Info = _repositories.Details(id),
                WorkState = _repositories.GetDetailsWorkList(id)
            };
            return View(model);
        }

        public ActionResult Edit (int id)
        {
            var model = _repositories.GetWorkState(id);
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(PerformerEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                _repositories.SaveChangeWorkState(model);
                return RedirectToAction("WorkList2");
            }

            ModelState.AddModelError("", "Wprowadz poprawne dane");
            return View(model);

        }



        public JsonResult GetCounty(int idVoivodeship)
        {
            var result = _repositories.GetCountyList(idVoivodeship);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCommune(int idCounty)
        {
            var items = _repositories.GetCommuneList(idCounty);
            return Json(items, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetLocality(int idCommune)
        {
            var items = _repositories.GetLocalityList(idCommune);
            return Json(items, JsonRequestBehavior.AllowGet);
        }

    }
}