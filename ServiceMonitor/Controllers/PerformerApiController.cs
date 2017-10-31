using ServiceMonitor.Models;
using ServiceMonitor.Repositories;
using ServiceMonitor.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace ServiceMonitor.Controllers
{
    public class PerformerApiController : ApiController
    {
        private readonly IPerformerRepositories _repositories;

        public PerformerApiController(IPerformerRepositories repositories)
        {
            _repositories = repositories;
        }

        //IPerformerRepositories _repositories = new PerformerRepositories( new SDA_EndProjEntitiesLocalHp());

        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //public IQueryable<IdNameViewModel> Get()
        //{
        //    return (IQueryable<IdNameViewModel>)_repositories.GetCountyList(2);
        //}

      //  [ResponseType(typeof(void))]
        public bool Get(string name, string password)//int id)
        {
            var aa = IsPerformerExist(name, password);
            return aa;
        //    return StatusCode(HttpStatusCode.NoContent);
        }

        private bool IsPerformerExist (string name, string password)
        {            
            var result = _repositories.AuthenticateUser(new Models.Performer.PerformerWelcomeViewModel
            {
                Login = name,
                Password = password
            });

            return result == null ? false: true;
        }
    }
}
