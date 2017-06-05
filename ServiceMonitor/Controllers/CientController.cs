using ServiceMonitor.Models;
using ServiceMonitor.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServiceMonitor.Models.Client;

namespace ServiceMonitor.Controllers
{
    public class CientController : Controller
    {
        private readonly IClientRepositories _repositories;

        public CientController(IClientRepositories repositories)
        {
            _repositories = repositories;
        }

        // GET: Cient
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(ClientGridViewModel model )
        {
          //  ClientRepositories clientRepositories = new ClientRepositories();

            if (ModelState.IsValid)
            {
                if (_repositories.CheckClient(model))
                {
                  return  RedirectToAction("ListOfServices", model);
                }
            }
            ModelState.AddModelError("Błędne dane", "Wprowadz poprawne dane");
            return View(model);
        }

        
        public ActionResult ListOfServices(ClientGridViewModel model)
        {
            var items = _repositories.GetListServices(model);

            return View(items);
        }

        public ActionResult Details(int id)
        {
            var items = _repositories.GetDetails(id);
            var info = _repositories.GetGeneralInfo(id);

            var model = new ClientServicesDetailsModel
            {
                State = items,
                Info = info
            };

            return View(model);
        }
    }
}