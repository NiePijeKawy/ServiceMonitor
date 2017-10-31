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
    public class ClientController : Controller
    {
        private readonly IClientRepositories _repositories;

        ServiceReferenceSemVITICw.TechnologieInternetoweCwSoapClient webClient = new ServiceReferenceSemVITICw.TechnologieInternetoweCwSoapClient();

        public ClientController(IClientRepositories repositories)
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

            var modelWeb = new ServiceReferenceSemVITICw.ClientGridViewModel
            { 
                Id = model.Id,
                Name = model.Name,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber                
            };            

            if (ModelState.IsValid)
            {
                //if (_repositories.CheckClient(model))
                //{

                //  return  RedirectToAction("ListOfServices", model.Name);
                //}

                //     var model2 = _repositories.CheckClient2(model);
                var modelWeb2 = webClient.CheckClient2(modelWeb);
                           
                if (modelWeb2 != null)
                {
                    return RedirectToAction("ListOfServices2", new { id = modelWeb2.Id });
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

        public ActionResult ListOfServices2(int id)
        {
            var items = _repositories.GetListServices2(id);

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