﻿using CountryParse.Models;
using CountryParse.Models.Adapter;
using CountryParseNetCore.Commands;
using CountryParseNetCore.Services;
using CountryParseNetCore.Services.Utils;
using CountryParseNetCore.ViewModels.Base;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace CountryParseNetCore.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        private readonly ApiService apiService;
        private readonly DatabaseContext dataContext;

        #region Свойства
        private string countryToFind;
        public string CountryToFind
        {
            get => countryToFind;
            set => Set(ref countryToFind, value);
        }

        private DataNode foundCountry;
        public DataNode FoundCountry
        {
            get => foundCountry;
            set => Set(ref foundCountry, value);
        }

        private BindingList<Country> databaseCountryList;
        public BindingList<Country> DatabaseCountryList
        {
            get => databaseCountryList;
            set => Set(ref databaseCountryList, value);
        }
        #endregion

        #region Команды
        public ICommand FindCountryCommand { get; }
        private bool CanFindCountryCommandExecute(object parameter) => true;
        private void OnFindCountryCommandExecute(object parameter)
        {
            try
            {
                FoundCountry = apiService.GetCityByName(CountryToFind);
            }
            catch
            {
                CountryToFind = "Not found";
            }
        }

        public ICommand SaveCountryCommand { get; }
        private bool CanSaveCountryCommand(object parameter)
        {
            if (FoundCountry == null)
            {
                return false;
            }

            Country country = dataContext.Countries
                .Where(c => c.Name == FoundCountry.Name)
                .FirstOrDefault();
            return country == null;
        }
        private void OnSaveCountryCommand(object parameter)
        {
            Capital capital = dataContext.Capitals
                .Where(c => c.Name == FoundCountry.Capital)
                .FirstOrDefault();
            if (capital == null)
            {
                capital = new Capital() { Name = FoundCountry.Capital };
            }

            Region region = dataContext.Regions
                .Where(r => r.Name == FoundCountry.Region)
                .FirstOrDefault();
            if (region == null)
            {
                region = new Region() { Name = FoundCountry.Region };
            }

            Country country = dataContext.Countries
                .Where(c => c.Name == FoundCountry.Capital)
                .FirstOrDefault();
            if (country == null)
            {
                country = new Country()
                {
                    Name = FoundCountry.Name,
                    CountryCapital = capital,
                    CountryRegion = region,
                    Area = FoundCountry.Area,
                    Code = FoundCountry.Code,
                    Population = FoundCountry.Population
                };
                dataContext.Countries.Add(country);
                dataContext.SaveChanges();
            }
            DatabaseCountryList.Add(country);
        }
        #endregion

        public MainWindowViewModel(ApiService apiService, DatabaseContext dataContext)
        {
            this.apiService = apiService;
            this.dataContext = dataContext;
            DatabaseCountryList = new BindingList<Country>(dataContext.Countries.Include("CountryCapital").Include("CountryRegion").ToList());

            #region Команды
            FindCountryCommand = new LambdaCommand(OnFindCountryCommandExecute, CanFindCountryCommandExecute);
            SaveCountryCommand = new LambdaCommand(OnSaveCountryCommand, CanSaveCountryCommand);
            #endregion
        }
    }
}
