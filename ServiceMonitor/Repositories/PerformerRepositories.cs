using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceMonitor.Models;
using ServiceMonitor.Models.Performer;
using ServiceMonitor.Repositories.Interface;




namespace ServiceMonitor.Repositories
{
    public class PerformerRepositories : IPerformerRepositories
    {
        //private readonly MVC4DataBaseEntities _dbContext;

        //public PerformerRepositories(MVC4DataBaseEntities dbContext)
        //{
        //    _dbContext = dbContext;
        //}

        private readonly SDA_EndProjEntitiesLocalHp _dbContext;

        public PerformerRepositories(SDA_EndProjEntitiesLocalHp dbContext)
        {
            _dbContext = dbContext;
        }

        public bool CheckPerformer(PerformerWelcomeViewModel model)
        {
            return _dbContext.Performers.Any(x =>
                x.Login == model.Login &&
                x.Password == model.Password);
        }

        public IEnumerable<PerformerGenerelListServices> GetWorkList(string login) //PerformerWelcomeViewModel model)
        {
            var result = _dbContext.Servicees
                .Where(x => x.Performer.Login == login) //model.Login)
                .Select(x => new PerformerGenerelListServices
                {
                    Id = x.Id,
                    CustomerName = x.Customer.Name,
                    CustomerLastName = x.Customer.LastName,
                    CustomerPhoneNumber = x.Customer.PhoneNumber,
                    KindWork = x.TypeService.Name,
                    Price = x.Price,
                    CurrentStatus = x.ChangeStageWorks.OrderByDescending(d => d.Id).FirstOrDefault().WorkStage.Name
                }).ToList();

            return result;
        }

        public IEnumerable<IdNameViewModel> GetCountyList(int idVoivodeship)
        {
            var result = _dbContext.Counties.Where(x => x.VoivodeshipCode == idVoivodeship)
                .Select(x => new IdNameViewModel
                {
                    Id = x.Id,
                    DisplayName = x.Name
                });

            return result;
        }

        public IEnumerable<IdNameViewModel> GetVoivodeshipList()
        {
            return _dbContext.Voivodeships
                .Select(x => new IdNameViewModel()
                {
                    Id = x.Id,
                    DisplayName = x.Name
                });
        }

        public IEnumerable<IdNameViewModel> GetCommuneList(int idCounty)
        {
            return _dbContext.Communes
                .Where(x => x.CountyCode == idCounty)
                .Select(x => new IdNameViewModel
                {
                    Id = x.Id,
                    DisplayName = x.Name
                });
        }

        public IEnumerable<IdNameViewModel> GetLocalityList(int idCommune)
        {
            return _dbContext.Localities
                .Where(x => x.CommuneCode == idCommune)
                .Select(x => new IdNameViewModel
                {
                    Id = x.Id,
                    DisplayName = x.Name
                });
        }
        public PerformerEditViewModel GetWorkState(int id)
        {
            var model = new PerformerEditViewModel
            {
                WorksState = _dbContext.WorkStages.Select(x => new IdNameViewModel
                {
                    Id = x.Id,
                    DisplayName = x.Name
                }).ToList(),
                ServiceName = _dbContext.Servicees
                        .Where(x => x.Id == id)
                        .Select(x => x.TypeService.Name).FirstOrDefault(),
                CurentStatus = _dbContext.ChangeStageWorks
                             .Where(x=>x.IdService == id)
                             .OrderByDescending(x=>x.Id)
                             .Select(x=>x.WorkStage.Name)
                             .FirstOrDefault(),
                Id = id
            };
            return model;
        }

        public void SaveNewPerformer(PerformerRejestractionViewModel model)
        {
            if (!_dbContext.Addressses.Any(x =>
                x.IdLocality == model.LocalityId &&
                x.Street == model.Street &&
                x.HouseNumber == model.HouseNumber))
            {
                var address = new Addresss
                {
                    IdVoivodeship = model.VoivodershipId,
                    IdCounty = model.CountyId,
                    IdCommune = model.CommuneId,
                    IdLocality = model.LocalityId,
                    Street = model.Street,
                    HouseNumber = model.HouseNumber
                };

                var performer = new Performer
                {
                    Name = model.Name,
                    IdAddress = _dbContext.Addressses
                        .Where(x => x.IdLocality == model.LocalityId && x.Street == model.Street &&
                                    x.HouseNumber == model.HouseNumber)
                        .Select(x => x.Id).SingleOrDefault(),
                    PhoneNumber = model.PhoneNumber,
                    Password = model.Password,
                    Login = model.Login
                };

                _dbContext.Addressses.Add(address);
                _dbContext.Performers.Add(performer);
                _dbContext.SaveChanges();

            }
            else
            {
                var performer = new Performer
                {
                    Name = model.Name,
                    IdAddress = _dbContext.Addressses
                        .Where(x => x.IdLocality == model.LocalityId && x.Street == model.Street &&
                                    x.HouseNumber == model.HouseNumber)
                        .Select(x => x.Id).SingleOrDefault(),
                    PhoneNumber = model.PhoneNumber,
                    Password = model.Password,
                    Login = model.Login
                };
                _dbContext.Performers.Add(performer);
                _dbContext.SaveChanges();
            }
        }

        public bool IsExist(PerformerRejestractionViewModel model)
        {
            return _dbContext.Performers.Any(x => x.Login == model.Login);
        }

        public bool CustomerIsExist(PerformerCreateNewCustomer model)
        {
            return _dbContext.Customers.Any(x => x.Name == model.Name && x.LastName == model.LastName &&
                                                x.PhoneNumber == model.PhoneNumber);
        }

        public void SaveNewClient(PerformerCreateNewCustomer model)
        {
            if (!_dbContext.Addressses.Any(x =>
                x.IdLocality == model.LocalityId &&
                x.Street == model.Street &&
                x.HouseNumber == model.HouseNumber))
            {
                var address = new Addresss
                {
                    IdVoivodeship = model.VoivodershipId,
                    IdCounty = model.CountyId,
                    IdCommune = model.CommuneId,
                    IdLocality = model.LocalityId,
                    Street = model.Street,
                    HouseNumber = model.HouseNumber
                };

                var customer = new Customer
                {
                    Name = model.Name,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    IdAddress = _dbContext.Addressses
                        .Where(x => x.IdLocality == model.LocalityId && x.Street == model.Street &&
                                    x.HouseNumber == model.HouseNumber)
                        .Select(x => x.Id).SingleOrDefault()
                };

                _dbContext.Addressses.Add(address);
                _dbContext.Customers.Add(customer);
                _dbContext.SaveChanges();
            }
            else
            {
                var customer = new Customer
                {
                    Name = model.Name,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    IdAddress = _dbContext.Addressses
                        .Where(x => x.IdLocality == model.LocalityId && x.Street == model.Street &&
                                    x.HouseNumber == model.HouseNumber)
                        .Select(x => x.Id).SingleOrDefault()
                };

                _dbContext.Customers.Add(customer);
                _dbContext.SaveChanges();
            }
        }

        public IEnumerable<IdNameViewModel> GetPODGiKs()
        {
            return _dbContext.PODGiKs
                .Select(x => new IdNameViewModel
                {
                    Id = x.Id,
                    DisplayName = x.Name
                });
        }

        public IEnumerable<IdNameViewModel> GetTypesList()
        {
            return _dbContext.TypeServices.Select(x => new IdNameViewModel
            {
                Id = x.Id,
                DisplayName = x.Name
            });
        }

        public IEnumerable<IdNameViewModel> GetCustomerList()
        {
            return _dbContext.Customers
                .Select(x => new IdNameViewModel
                {
                    Id = x.Id,
                    DisplayName = x.LastName + " " + x.Name
                });
        }

        public string FindUser(string userLogin)
        {
            return _dbContext.Performers.Where(x => x.Login == userLogin)
                .Select(x => x.Login).FirstOrDefault();
        }

        public PerformerWelcomeViewModel AuthenticateUser(PerformerWelcomeViewModel model)
        {            
            var result = _dbContext.Performers
                    .Where( x=> x.Login==model.Login && x.Password==model.Password)
                    .FirstOrDefault();


            if (result==null)  return null;

            var performerModel = new PerformerWelcomeViewModel {Login=result.Login, Password=result.Password };


            return performerModel;
        }

        public bool ServiceIsExist(PerformerAddNewService model)
        {
            int? address = _dbContext.Addressses.Where(x => x.IdVoivodeship == model.IdVoivodership &&
                                                    x.IdCounty == model.IdCounty &&
                                                    x.IdCommune == model.IdCommune &&
                                                    x.IdLocality == model.IdLocality &&
                                                    x.PlotNumber == model.PlotNumber)
                                                    .Select(p=>p.Id).FirstOrDefault();

            bool client= _dbContext.Servicees.Any(x =>
                                            x.IdAddress == address &&
                                            x.IdCustomer == model.IdCustomer &&
                                            x.IdPODGiK == model.IdPodgik &&
                                            x.IdType == model.IdType &&
                                            x.Price == model.Price &&
                                            x.Descriptionn == x.Descriptionn &&
                                            x.IdPerformer == model.IdPerformer );
            if (client) return true;
            return false;
        }

        public void SaveNewService(PerformerAddNewService model)
        {            
            var address = new Addresss
            {
                IdVoivodeship = model.IdVoivodership,
                IdCounty = model.IdCounty,
                IdCommune = model.IdCommune,
                IdLocality = model.IdLocality,
                PlotNumber = model.PlotNumber
            };
            _dbContext.Addressses.Add(address);
            _dbContext.SaveChanges();

         //   int Idaddres = address.Id;        pobieranie id po insercie

            var service = new Servicee
            {
                IdPerformer = _dbContext.Performers.Where(x=>x.Login==model.LoginPerformer)
                                                        .Select(p=>p.Id)
                                                        .FirstOrDefault(),
                IdCustomer = model.IdCustomer,
                IdType = model.IdType,
                Price = model.Price,
                Descriptionn = model.Description,
                IdAddress = address.Id,
                IdPODGiK = model.IdPodgik      
            };
            
            _dbContext.Servicees.Add(service);
            _dbContext.SaveChanges();

            var workState = new ChangeStageWork
            {
                Descriptionn = "Przyjącie zamówienie",
                IdService = service.Id,
                IdWorkStage = 1,
                SinceWhen = DateTime.Now
            };

            _dbContext.ChangeStageWorks.Add(workState);
            _dbContext.SaveChanges();
        }

        public void DeleteWork(int id)
        {
            var workHistory = _dbContext.ChangeStageWorks.Where(p => p.IdService == id);
            var service = _dbContext.Servicees.Where(x => x.Id == id).First();

            //          _dbContext.Entry(workHistory).State = System.Data.Entity.EntityState.Deleted;

            //          _dbContext.Entry(service).State = System.Data.Entity.EntityState.Deleted;

            if (workHistory!=null)
            {
                _dbContext.ChangeStageWorks.RemoveRange(workHistory);

            }
            _dbContext.Servicees.Remove(service);


            _dbContext.SaveChanges();

        }

        public PerformerDetailsInfo Details(int id)
        {
            var result = _dbContext.Servicees
                    .Where(x => x.Id == id)
                    .Select(x => new PerformerDetailsInfo
                    {
                        CustomerFullName = x.Customer.Name + " " + x.Customer.LastName,
                        CustomerPhoneNumber = x.Customer.PhoneNumber,
                        CustomerVoivodeship = x.Customer.Addresss.Voivodeship.Name,
                        CustomerCounty = x.Customer.Addresss.County.Name,
                        CustomerCommune = x.Customer.Addresss.Commune.Name,
                        CustomerLocality = x.Customer.Addresss.Locality.Name,
                        CustomerFullStreet = x.Customer.Addresss.Street + " " + x.Customer.Addresss.HouseNumber,

                        PODGiK_Name = x.PODGiK.Name,
                        PODGiK_WebSite = x.PODGiK.WebSite,
                        PODGiK_PhoneNumber = x.PODGiK.PhoneNumber,
                        PODGiK_Voivodeship = x.PODGiK.Addresss.Voivodeship.Name,
                        PODGiK_County = x.PODGiK.Addresss.County.Name,
                        PODGiK_Commune = x.PODGiK.Addresss.Commune.Name,
                        PODGiK_Locality = x.PODGiK.Addresss.Locality.Name,
                        PODGiK_FullStreet = x.PODGiK.Addresss.Street + " " + x.PODGiK.Addresss.HouseNumber,

                        NameOfService = x.TypeService.Name,
                        CostService = x.Price,
                        PlotVoivodeship = _dbContext.Addressses.Where(p => p.Id == x.IdAddress)
                                                .Select(d => d.Voivodeship.Name).FirstOrDefault(),
                        PlotCounty = _dbContext.Addressses.Where(p => p.Id == x.IdAddress)
                                                .Select(d => d.County.Name).FirstOrDefault(),
                        PlotCommune = _dbContext.Addressses.Where(p => p.Id == x.IdAddress)
                                                .Select(d => d.Commune.Name).FirstOrDefault(),
                        PlotLocality = _dbContext.Addressses.Where(p => p.Id == x.IdAddress)
                                                .Select(d => d.Locality.Name).FirstOrDefault(),
                        PlotNumber = _dbContext.Addressses.Where(p => p.Id == x.IdAddress)
                                                .Select(d => d.PlotNumber).FirstOrDefault()
                    }).First();
            return result;
        }

        public IEnumerable<PerformerDetailsWorkState> GetDetailsWorkList(int id)
        {
            var result = _dbContext.ChangeStageWorks
                .Where(x => x.IdService == id)
                .Select(x => new PerformerDetailsWorkState
                {
                    NameService = x.Servicee.TypeService.Name,
                    Status = x.WorkStage.Name,
                    SinceWhen = x.SinceWhen,
                    UntilWhen = x.UntilWhen,
                    Description = x.Descriptionn
                }).ToList();

            return result;
        }

        public void SaveChangeWorkState(PerformerEditViewModel model)
        {
            int old = _dbContext.ChangeStageWorks
                .Where(x => x.IdService == model.Id && x.UntilWhen == null)
                .Select(x => x.Id)
                .FirstOrDefault();

            ChangeStageWork oldRow = _dbContext.ChangeStageWorks.Find(old);

            if (oldRow != null)
            {
                oldRow.UntilWhen = model.UntilWhen;
            }

      //      ChangeStageWork oldStateWork = _dbContext.ChangeStageWorks.Find(model.)
      //                        //  .Where(x => x.IdService == model.Id && x.UntilWhen == null);
            var newStateWork = new ChangeStageWork
            {
                IdService = model.Id,
                IdWorkStage = model.IdNewStatus,
                Descriptionn = model.Description,
                SinceWhen = model.UntilWhen
            };

            _dbContext.ChangeStageWorks.Add(newStateWork);
            _dbContext.SaveChanges();

        }
    }
}