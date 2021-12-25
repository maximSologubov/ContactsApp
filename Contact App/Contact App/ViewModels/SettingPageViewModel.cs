using Contact_App.Resources;
using Contact_App.Services.DbService;
using Contact_App.Services.Settings;
using Contact_App.Themes;
using Contact_App.Views;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Contact_App.ViewModels
{
    public class SettingPageViewModel : ViewModelBase, INavigatedAware
    {
        public SettingPageViewModel(INavigationService navigationService, IDbService dbService, ISettinngsManager settinngsManager) : base(navigationService, dbService, settinngsManager) 
        {
            Title = Resource.SettingsTitlePage;
        }


        #region --- Private fields ---

        private object sortSelection;
        private string oldSortSelection;
        private bool isToogled;       

        #endregion
        #region  --- Properties ---

        public object SortSelection
        {
            get => sortSelection;
            set => SetProperty(ref sortSelection, value);
        }

        public bool IsToogled
        {
            get => isToogled;
            set => SetProperty(ref isToogled, value);
        }

        #endregion

        #region Commands

        public DelegateCommand SwitchTapCommand => new DelegateCommand(OnChangeTheme);

        #endregion


        #region Overrides

        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);

            string sortList = SettingsManager.SortListBy;
            if (!string.IsNullOrEmpty(sortList))
            {
                switch (sortList)
                {
                    case "Name":
                        SortSelection = "Name";
                        break;
                    case "NickName":
                        SortSelection = "NickName";
                        break;
                    case "Date creation":
                        SortSelection = "Date creation";
                        break;
                }
                oldSortSelection = sortList;
            }

            IsToogled = SettingsManager.DarkTheme;
           
        }

        protected async override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            if (args.PropertyName == nameof(IsToogled))
                OnChangeTheme();

            if (args.PropertyName == nameof(SortSelection) && SettingsManager.ChangeSort == true)
            {                
                await NavigationService.GoBackAsync();
            }

        }

        #endregion

        #region  --- Implement interface ---

        public async void OnNavigatedFrom(INavigationParameters parameters)
        {
            if (oldSortSelection != SortSelection.ToString())
            {
                SettingsManager.SortListBy = SortSelection.ToString();
            }
            SettingsManager.DarkTheme = IsToogled;
        }

        public void OnNavigatedTo(INavigationParameters parameters) 
        {
            SettingsManager.ChangeSort = true;
        }

        #endregion

        #region  --- Private helpers ---
        
        private void OnChangeTheme()
        {
            ICollection<ResourceDictionary> mergedDictionaries = Application.Current.Resources.MergedDictionaries;
            if (mergedDictionaries != null)
            {
                mergedDictionaries.Clear();

                switch (IsToogled)
                {
                    case true:
                        mergedDictionaries.Add(new DarkTheme());
                        break;
                    case false:
                        mergedDictionaries.Add(new LightTheme());
                        break;
                }
            }
        }

        #endregion
    }
}