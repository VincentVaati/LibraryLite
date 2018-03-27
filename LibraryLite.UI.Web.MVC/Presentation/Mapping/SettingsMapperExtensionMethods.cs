using LibraryLite.Core.Entities;
using LibraryLite.UI.Web.MVC.Presentation.ViewModels;
using System.Collections.Generic;

namespace LibraryLite.UI.Web.MVC.Presentation.Mapping
{
    public static class SettingsMapperExtensionMethods
    {
        public static ApplicationSettings ConvertToApplicationSettings(ApplicationSettingsViewModel applicationSettingsViewModel)
        {
            var appSettings = new ApplicationSettings
            {
                Id = applicationSettingsViewModel.Id,
                InstitutionName = applicationSettingsViewModel.InstitutionName,
                MaxBooksLoanable = applicationSettingsViewModel.MaxBooksLoanable,
                LoanDays = applicationSettingsViewModel.LoanDays,
                Penalty = applicationSettingsViewModel.Penalty
            };
            return appSettings;
        }
        public static ApplicationSettingsViewModel ConvertToAppSettingsViewModel(ApplicationSettings appSettings)
        {
            var appSettingsViewModel = new ApplicationSettingsViewModel
            {
                Id = appSettings.Id,
                InstitutionName = appSettings.InstitutionName,
                MaxBooksLoanable = appSettings.MaxBooksLoanable,
                LoanDays = appSettings.LoanDays,
                Penalty = appSettings.Penalty
            };
            return appSettingsViewModel;
        }
        public static IList<ApplicationSettingsViewModel> ConvertToAppSettingsList(IList<ApplicationSettings> settings)
        {
            IList<ApplicationSettingsViewModel> settingsViewModelList = new List<ApplicationSettingsViewModel>();
            foreach (var setting in settings)
            {
                settingsViewModelList.Add(ConvertToAppSettingsViewModel(setting));
            }
            return settingsViewModelList;
        }
    }
}