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
using System.Globalization;
using System.Linq;
using System.Text;
using Xamarin.CommunityToolkit.Helpers;
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
        private object languageSelection;
        private bool isDark;
        private bool refresh = false;

        #endregion
        #region  --- Properties ---

        public object SortSelection
        {
            get => sortSelection;
            set => SetProperty(ref sortSelection, value);
        } 
        public object LanguageSelection
        {
            get => languageSelection;
            set => SetProperty(ref languageSelection, value);
        }

        public bool IsDark
        {
            get => isDark;
            set => SetProperty(ref isDark, value);
        }
        public bool Refresh
        {
            get => refresh;
            set => SetProperty(ref refresh, value);
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
            }
            string language = SettingsManager.Language;
            LocalizationResourceManager.Current.CurrentCulture = new CultureInfo(language);
            if (!string.IsNullOrEmpty(language))
            {
                switch (language)
                {
                    case "en":
                        LanguageSelection = "en";
                        Refresh = true;
                        break;
                    case "ru":
                        LanguageSelection = "ru";
                        Refresh = true;
                        break;
                }               
            }

            IsDark = SettingsManager.DarkTheme;
            OnChangeTheme();

        }

        protected async override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            if (args.PropertyName == nameof(IsDark))
            {
                SettingsManager.DarkTheme = IsDark;
                OnChangeTheme();
            }
                

            if (args.PropertyName == nameof(SortSelection) && SettingsManager.ChangeSort == true)
            {                
                await NavigationService.GoBackAsync();
            }
            if (args.PropertyName == nameof(LanguageSelection))
            {

                LocalizationResourceManager.Current.CurrentCulture = new CultureInfo(LanguageSelection.ToString());
                if (Refresh) {
                    SettingsManager.Language = LanguageSelection.ToString();
                    await NavigationService.NavigateAsync(nameof(SettingPage));
                    NavigationPage page = (NavigationPage)App.Current.MainPage;
                    page.Navigation.RemovePage(page.Navigation.NavigationStack[page.Navigation.NavigationStack.Count - 2]);                    
                    Refresh = false;
                }
            }

        }

        #endregion

        #region  --- Implement interface ---

        public async void OnNavigatedFrom(INavigationParameters parameters)
        {            
            SettingsManager.SortListBy = SortSelection.ToString();
            SettingsManager.DarkTheme = IsDark;
            SettingsManager.Language = LanguageSelection.ToString();

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

                switch (IsDark)
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