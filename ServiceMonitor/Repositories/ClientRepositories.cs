using ServiceMonitor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceMonitor.Models.Client;
using ServiceMonitor.Repositories.Interface;

namespace ServiceMonitor.Repositories
{
    public class ClientRepositories : IClientRepositories
    {
        //private readonly MVC4DataBaseEntities _dbContext;

        //public ClientRepositories(MVC4DataBaseEntities dbContext)
        //{
        //    _dbContext = dbContext;
        //}

        private readonly SDA_EndProjEntitiesLocalHp _dbContext;

        public ClientRepositories(SDA_EndProjEntitiesLocalHp dbContext)
        {
            _dbContext = dbContext;
        }

        public bool CheckClient(ClientGridViewModel model)
        {
            return _dbContext.Customers.Any(x =>
                x.Name == model.Name &&
                x.LastName == model.LastName &&
                x.PhoneNumber == model.PhoneNumber);
        }
        public ClientGridViewModel CheckClient2(ClientGridViewModel model)
        {
            

            var result = _dbContext.Customers
                .Where(x =>
                    x.Name == model.Name &&
                    x.LastName == model.LastName &&
                    x.PhoneNumber == model.PhoneNumber)
                .Select(c => new ClientGridViewModel
                {
                    Id = c.Id,
                    LastName = c.LastName,
                    Name = c.Name,
                    PhoneNumber = c.PhoneNumber
                }).FirstOrDefault();             

            return result;            
        }



        public IEnumerable<ClientServicesDetailsViewModel> GetDetails(int id)
        {
            var result = _dbContext.ChangeStageWorks
                .Where(x => x.IdService == id)
                .Select(x => new ClientServicesDetailsViewModel
                {
                    NameService = x.Servicee.TypeService.Name,
                    Status = x.WorkStage.Name,
                    SinceWhen = x.SinceWhen,
                    UntilWhen = x.UntilWhen,
                    Description = x.Descriptionn
                }).ToList();


            return result;
        }

        public IEnumerable<ClientListServicesViewModel> GetListServices(ClientGridViewModel model)
        {
            var result = _dbContext.Servicees
                .Where(x => x.Customer.Name == model.Name &&
                            x.Customer.LastName == model.LastName &&
                            x.Customer.PhoneNumber == model.PhoneNumber)
                .Select(x => new ClientListServicesViewModel
                {
                    NameServices = x.TypeService.Name,
                    CompanyName = x.Performer.Name,
                    CurrentStatus = 
                        x.ChangeStageWorks.OrderByDescending(d => d.Id).FirstOrDefault().WorkStage
                            .Name, //Single(a => a.UntilWhen == null).WorkStage.Name,
                    Price = (double) x.Price,
                    Id = x.Id
                }).ToList();

            return result;
        }
        public IEnumerable<ClientListServicesViewModel> GetListServices2(int id)
        {
            var result = _dbContext.Servicees
                .Where(x => x.Customer.Id == id)
                .Select(x => new ClientListServicesViewModel
                {
                    NameServices = x.TypeService.Name,
                    CompanyName = x.Performer.Name,
                    CurrentStatus =
                        x.ChangeStageWorks.OrderByDescending(d => d.Id).FirstOrDefault().WorkStage
                            .Name, //Single(a => a.UntilWhen == null).WorkStage.Name,
                    Price = (double)x.Price,
                    Id = x.Id
                }).ToList();

            return result;
        }


        public ClientServicesDetailsGeneralModel GetGeneralInfo(int id)
        {
            var result = _dbContext.Servicees
                .Where(x => x.Id == id)
                .Select(x => new ClientServicesDetailsGeneralModel
                {
                    PODGiK_Name = x.PODGiK.Name,
                    PhoneNumber = x.PODGiK.PhoneNumber,
                    WebSite = x.PODGiK.WebSite,
                    PerformerName = x.Performer.Name,
                    PerformerPhone = x.Performer.PhoneNumber
                }).SingleOrDefault();


            return result;
        }

  
    }
}