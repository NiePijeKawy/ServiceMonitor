﻿using System.Collections.Generic;
using ServiceMonitor.Models;
using ServiceMonitor.Models.Performer;

namespace ServiceMonitor.Repositories.Interface
{
    public interface IPerformerRepositories
    {
        bool CheckPerformer(PerformerWelcomeViewModel model);
        IEnumerable<PerformerGenerelListServices> GetWorkList(string login); //PerformerWelcomeViewModel model);
        IEnumerable<IdNameViewModel> GetCountyList(int idVoivodeship);
        IEnumerable<IdNameViewModel> GetVoivodeshipList();
        IEnumerable<IdNameViewModel> GetCommuneList(int idCounty);
        IEnumerable<IdNameViewModel> GetLocalityList(int idCommune);
        void SaveNewPerformer(PerformerRejestractionViewModel model);  //PerformerCreateNewPerformer
        bool IsExist(PerformerRejestractionViewModel model); //PerformerCreateNewPerformer
        bool CustomerIsExist(PerformerCreateNewCustomer model);
        void SaveNewClient(PerformerCreateNewCustomer model);
        IEnumerable<IdNameViewModel> GetPODGiKs();
        IEnumerable<IdNameViewModel> GetTypesList();
        IEnumerable<IdNameViewModel> GetCustomerList();
        PerformerEditViewModel GetWorkState(int id);
        string FindUser(string userLogin);
        PerformerWelcomeViewModel AuthenticateUser(PerformerWelcomeViewModel model);
        bool ServiceIsExist(PerformerAddNewService model);
        void SaveNewService(PerformerAddNewService model);
        void DeleteWork(int id);
        PerformerDetailsInfo Details(int id);
        IEnumerable<PerformerDetailsWorkState> GetDetailsWorkList(int id);
        void SaveChangeWorkState(PerformerEditViewModel model);
    }
}