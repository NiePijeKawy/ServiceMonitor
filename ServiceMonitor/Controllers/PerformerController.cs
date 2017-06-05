using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServiceMonitor.Models.Performer;
using ServiceMonitor.Repositories;
using ServiceMonitor.Repositories.Interface;

namespace ServiceMonitor.Controllers
{
    public class PerformerController : Controller
    {
        private readonly IPerformerRepositories _repositories;

        public PerformerController(IPerformerRepositories repositories)
        {
            _repositories = repositories;
        }

        // GET: Performer
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(PerformerWelcomeViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (_repositories.CheckPerformer(model))
                {
                    return RedirectToAction("WorkList", new {login=model.Login});
                }
            }

            ModelState.AddModelError("Błędne dane", "Wprowadz poprawne dane");
            return View(model);
        }
        
        public ActionResult Registration()
        {
            var voivoderships = _repositories.GetVoivodeshipList();

            var model = new PerformerRejestractionViewModel
            {
               Voivoderships = voivoderships
            };

            return View(model);
        }

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

        public ActionResult AddClient()
        {
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
                return RedirectToAction("WorkList");
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
                Voivoderships = voivodeships
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult AddServices(PerformerAddNewService model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("WorkList"); // dopytać jak trzymać login 
            }
            return View(model);
        }

        

        public ActionResult GetCounty(int idVoivodeship)
        {
            var result = _repositories.GetCountyList(idVoivodeship);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCommune(int idCounty)
        {
            var items = _repositories.GetCommuneList(idCounty);
            return Json(items, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetLocality(int idCommune)
        {
            var items = _repositories.GetLocalityList(idCommune);
            return Json(items, JsonRequestBehavior.AllowGet);
        }


    }
}