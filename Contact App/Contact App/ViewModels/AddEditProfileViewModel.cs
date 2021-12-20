﻿using Contact_App.Models;
using Contact_App.Resources;
using Contact_App.ServiceData;
using Contact_App.Services.DbService;
using Contact_App.Services.Settings;
using Contact_App.Views;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Contact_App.ViewModels
{
    public class AddEditProfileViewModel : ViewModelBase
    {
        public AddEditProfileViewModel(INavigationService navigationService, IDbService dbService, ISettinngsManager settinngsManager) : base(navigationService, dbService, settinngsManager)
        {
            Title = Resource.AddProfileTitlePage;
        }

        #region --- Private fields ---

        private string pathToImageSourceProfile = Constants.PATH_TO_DEFAULT_IMAGE_PROFILE;
        private string nickName;
        private string name;
        private string description;
        private bool editMode = false;
        private ProfileModel profile;

        #endregion

        #region Commands

        public DelegateCommand SaveTapCommand => new DelegateCommand(SaveProfile);

        #endregion

        #region --- Properties ---

        public string PathToImageSourceProfile
        {
            get => pathToImageSourceProfile;
            set => SetProperty(ref pathToImageSourceProfile, value);
        }

        public string NickName
        {
            get => nickName;
            set => SetProperty(ref nickName, value);
        }

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        #endregion

        #region Override

        public override void Initialize(INavigationParameters parameters)
        {
            profile = parameters.GetValue<ProfileModel>("profile");
            if (profile != null)
            {
                PathToImageSourceProfile = profile.ImagePath;
                NickName = profile.NickName;
                Name = profile.Name;
                Description = profile.Description;
                editMode = true;
                Title = Resource.EditProfileTitlePage;
            }
        }

        #endregion

        #region --- Private helpers ---

        private async void SaveProfile()
        {
            if (string.IsNullOrWhiteSpace(NickName) || string.IsNullOrWhiteSpace(Name))
            {               
                App.Current.MainPage.DisplayAlert("", Resource.ALERT_ADD_EDIT_PROFILE, "ОK");
                return;
            }

            if (profile == null)
            {
                profile = new ProfileModel();
            }

            profile.NickName = NickName;
            profile.Name = Name;
            profile.ImagePath = PathToImageSourceProfile;
            profile.Owner = SettingsManager.LoggedUser;
            profile.CreationTime = DateTime.Now;
            profile.Description = Description;

            try
            {
                if (editMode)
                {
                    await DbService.UpdateDataAsync(profile);
                    editMode = false;
                }
                else
                {
                    int id = await DbService.InsertDataAsync(profile);
                }

                await NavigationService.NavigateAsync(nameof(MainListView));
                NavigationPage page = (NavigationPage)App.Current.MainPage;
                while (page.Navigation.NavigationStack.Count > 1)
                    page.Navigation.RemovePage(page.Navigation.NavigationStack[page.Navigation.NavigationStack.Count - 2]);
                profile = null;
            }
            catch (Exception ex)
            {
                App.Current.MainPage.DisplayAlert("Error", ex.Message, "ОK");
            }
        }

        #endregion

    }
}