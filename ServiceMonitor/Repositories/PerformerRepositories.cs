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
        private readonly SDA_EndProjContext _dbContext;

        public PerformerRepositories(SDA_EndProjContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool CheckPerformer(PerformerWelcomeViewModel model)
        {
            return _dbContext.Performer.Any(x =>
                x.Login == model.Login &&
                x.Password == model.Password);
        }

        public IEnumerable<PerformerGenerelListServices> GetWorkList(string login)//PerformerWelcomeViewModel model)
        {
            var result = _dbContext.Servicee
                .Where(x => x.Performer.Login ==login )//model.Login)
                .Select(x => new PerformerGenerelListServices
                {
                    Id = x.Id,
                    CustomerName = x.Customer.Name,
                    CustomerLastName = x.Customer.LastName,
                    CustomerPhoneNumber = x.Customer.PhoneNumber,
                    KindWork = x.TypeService.Name,
                    Price = x.Price,
                    CurrentStatus = x.ChangeStageWork.OrderByDescending(d=>d.Id).FirstOrDefault().WorkStage.Name
                }).ToList();

            return result;
        }
        
        public IEnumerable<IdNameViewModel> GetCountyList(int idVoivodeship)
        {
            var result = _dbContext.County.Where(x => x.VoivodeshipCode == idVoivodeship)
                                .Select(x=>new IdNameViewModel
                {
                    Id = x.Id,
                    DisplayName = x.Name
                });

            return result;
        }

        public IEnumerable<IdNameViewModel> GetVoivodeshipList()
        {
            return _dbContext.Voivodeship
                .Select(x => new IdNameViewModel()
                {
                    Id = x.Id,
                    DisplayName = x.Name
                });
        }

        public IEnumerable<IdNameViewModel> GetCommuneList(int idCounty)
        {
            return _dbContext.Commune
                .Where(x => x.CountyCode == idCounty)
                .Select(x => new IdNameViewModel
                {
                    Id = x.Id,
                    DisplayName = x.Name
                });
        }

        public IEnumerable<IdNameViewModel> GetLocalityList(int idCommune)
        {
            return _dbContext.Locality
                .Where(x => x.CommuneCode == idCommune)
                .Select(x => new IdNameViewModel
                {
                    Id = x.Id,
                    DisplayName = x.Name
                });
        }

        public void SaveNewPerformer(PerformerRejestractionViewModel model)
        {
            if (!_dbContext.Addresss.Any(x =>
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
                    IdAddress = _dbContext.Addresss
                        .Where(x => x.IdLocality == model.LocalityId && x.Street == model.Street &&
                                    x.HouseNumber == model.HouseNumber)
                        .Select(x => x.Id).SingleOrDefault(),
                    PhoneNumber = model.PhoneNumber,
                    Password = model.Password,
                    Login = model.Login
                };

                _dbContext.Addresss.Add(address);
                _dbContext.Performer.Add(performer);
                _dbContext.SaveChanges();

            }
            else
            {
                var performer = new Performer
                {
                    Name = model.Name,
                    IdAddress = _dbContext.Addresss
                        .Where(x => x.IdLocality == model.LocalityId && x.Street == model.Street &&
                                    x.HouseNumber == model.HouseNumber)
                        .Select(x => x.Id).SingleOrDefault(),
                    PhoneNumber = model.PhoneNumber,
                    Password = model.Password,
                    Login = model.Login
                };
                _dbContext.Performer.Add(performer);
                _dbContext.SaveChanges();
            }
        }

        public bool IsExist(PerformerRejestractionViewModel model)
        {
            return _dbContext.Performer.Any(x => x.Login == model.Login);
        }

        public bool CustomerIsExist(PerformerCreateNewCustomer model)
        {
            return _dbContext.Customer.Any(x => x.Name == model.Name && x.LastName == model.LastName &&
                                                x.PhoneNumber == model.PhoneNumber);
        }

        public void SaveNewClient(PerformerCreateNewCustomer model)
        {
            if (!_dbContext.Addresss.Any(x =>
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
                    IdAddress = _dbContext.Addresss
                        .Where(x => x.IdLocality == model.LocalityId && x.Street == model.Street &&
                                    x.HouseNumber == model.HouseNumber)
                        .Select(x => x.Id).SingleOrDefault()
                };

                _dbContext.Addresss.Add(address);
                _dbContext.Customer.Add(customer);
                _dbContext.SaveChanges();
            }
            else
            {
                var customer = new Customer
                {
                    Name = model.Name,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    IdAddress = _dbContext.Addresss
                        .Where(x => x.IdLocality == model.LocalityId && x.Street == model.Street &&
                                    x.HouseNumber == model.HouseNumber)
                        .Select(x => x.Id).SingleOrDefault()
                };

                _dbContext.Customer.Add(customer);
                _dbContext.SaveChanges();
            }
        }

        public IEnumerable<IdNameViewModel> GetPODGiKs()
        {
            return _dbContext.PODGiK
                .Select(x => new IdNameViewModel
                {
                    Id = x.Id,
                    DisplayName = x.Name
                });
        }

        public IEnumerable<IdNameViewModel> GetTypesList()
        {
            return _dbContext.TypeService.Select(x => new IdNameViewModel
            {
                Id = x.Id,
                DisplayName = x.Name
            });
        }

        public IEnumerable<IdNameViewModel> GetCustomerList()
        {
            return _dbContext.Customer
                .Select(x => new IdNameViewModel
            {
                   Id = x.Id,
                   DisplayName = x.LastName +" "+ x.Name
            });
        }
    }
}