using Contact_App.Services.DbService;
using Contact_App.Services.Settings;
using Contact_App.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Contact_App.ViewModels
{
    public class MainListViewModel : ViewModelBase
    {
        public MainListViewModel(INavigationService navigationService, IDbService dbService, ISettinngsManager settinngsManager) : base(navigationService, dbService, settinngsManager)
        {
           
        }



        #region Commands

        public DelegateCommand LogOutTapCommand => new DelegateCommand(GoLogOutAsync);
        public DelegateCommand AddEditProfileTapCommand => new DelegateCommand(GoAddEditProfileAsync);
        public DelegateCommand SettingsTapCommand => new DelegateCommand(GoSettingsPageAsync);

        #endregion

        #region Private helpers

        private async void GoLogOutAsync()
        {
            SettingsManager.LoggedUser = "";
            await NavigationService.NavigateAsync(nameof(SignInPage));

            NavigationPage page = (NavigationPage)App.Current.MainPage;
            while (page.Navigation.NavigationStack.Count > 1)
                page.Navigation.RemovePage(page.Navigation.NavigationStack[page.Navigation.NavigationStack.Count - 2]);
        }

        private async void GoAddEditProfileAsync() => await NavigationService.NavigateAsync(nameof(AddEditProfilePage));

        private async void GoSettingsPageAsync() => await NavigationService.NavigateAsync(nameof(SettingPage));

        #endregion

    }
}