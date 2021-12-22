using Acr.UserDialogs;
using Contact_App.Models;
using Contact_App.Resources;
using Contact_App.Services.DbService;
using Contact_App.Services.Settings;
using Contact_App.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Contact_App.ViewModels
{
    public class MainListViewModel : ViewModelBase, IInitializeAsync//, INavigatedAware
    {
        public MainListViewModel(INavigationService navigationService, IDbService dbService, ISettinngsManager settinngsManager) : base(navigationService, dbService, settinngsManager)
        {
           
        }

        #region Private fields

        private ObservableCollection<ProfileModel> profileList;
        private bool isVisible = false;

        #endregion

        #region Implement interfaces

        public async Task InitializeAsync(INavigationParameters parameters)
        {
            List<ProfileModel> profiles = await DbService.GetOwnersProfilesAsync(SettingsManager.LoggedUser);
           // profiles.Sort(new ProfileModelComparer(SettingsManager.SortListBy));
            ProfileList = new ObservableCollection<ProfileModel>(profiles);

            IsVisible = ProfileList.Count > 0;
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            //if (SettingsManager.ChangeSort && parameters.GetNavigationMode() == NavigationMode.Back)
            //{
                List<ProfileModel> profiles = ProfileList.ToList();
                //profiles.Sort(new ProfileModelComparer(SettingsManager.SortListBy));
                ProfileList = new ObservableCollection<ProfileModel>(profiles);
                //SettingsManager.ChangeSort = false;ты
            //}
        }

       // public void OnNavigatedFrom(INavigationParameters parameters) { }

        #endregion

        #region Properties

        public ObservableCollection<ProfileModel> ProfileList
        {
            get => profileList;
            set => SetProperty(ref profileList, value);
        }

        public IDialogService DialogService { get; }

        public bool IsVisible
        {
            get => isVisible;
            set => SetProperty(ref isVisible, value);
        }

        #endregion

        #region Commands

        public DelegateCommand LogOutTapCommand => new DelegateCommand(GoLogOutAsync);
        public DelegateCommand AddEditProfileTapCommand => new DelegateCommand(GoAddEditProfileAsync);
        public DelegateCommand SettingsTapCommand => new DelegateCommand(GoSettingsPageAsync);

        public ICommand DeleteTapCommand => new Command(OnDeleteAsync);
        public ICommand UpdateTapCommand => new Command(GoUpdateProfileAsync);
        public ICommand ItemTapCommand => new Command(GoShowItemTapped);

        #endregion

        #region --- Private helpers ---

        private async void GoLogOutAsync()
        {
            SettingsManager.LoggedUser = "";
            await NavigationService.NavigateAsync(nameof(SignInPage));

            NavigationPage page = (NavigationPage)App.Current.MainPage;
            while (page.Navigation.NavigationStack.Count > 1)
                page.Navigation.RemovePage(page.Navigation.NavigationStack[page.Navigation.NavigationStack.Count - 2]);
        }

        private async void GoAddEditProfileAsync() => await NavigationService.NavigateAsync(nameof(AddEditProfilePage));

        private async void GoUpdateProfileAsync(object selectedProfile)
        {
            ProfileModel profileModel = selectedProfile as ProfileModel;

            if (profileModel != null)
            {
                NavigationParameters parameter = new NavigationParameters
                {
                    {"profile", profileModel }
                };

                await NavigationService.NavigateAsync(nameof(AddEditProfilePage), parameter);
            }
        }


        private async void OnDeleteAsync(object selectedProfile)
        {
            ProfileModel profileModel = selectedProfile as ProfileModel;

            if (profileModel != null)
            {
                ConfirmConfig confirmConfig = new ConfirmConfig
                {
                    Message = Resource.DeleteProfileQuestion,
                    OkText = Resource.ConfirmOK,
                    CancelText = Resource.ConfirmCancel
                };

                if (await UserDialogs.Instance.ConfirmAsync(confirmConfig))
                {
                    ProfileList.Remove(ProfileList.First(p => p.Id == profileModel.Id));
                    IsVisible = ProfileList.Count > 0;
                    await DbService.DeleteDataAsync(profileModel);
                }
            }
        }


        private void GoShowItemTapped(object profile)
        {
            ProfileModel profileModel = profile as ProfileModel;

            if (profileModel != null)
                DialogService.ShowDialog("ItemTappedDialog", new DialogParameters
                {
                    {"source", profileModel.ImagePath }
                });
        }


        private async void GoSettingsPageAsync() => await NavigationService.NavigateAsync(nameof(SettingPage));

        #endregion

    }
}