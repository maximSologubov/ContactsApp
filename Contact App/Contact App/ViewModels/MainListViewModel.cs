using Contact_App.Models;
using Contact_App.Services.DbService;
using Contact_App.Services.Settings;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Contact_App.ViewModels
{
    public class MainListViewModel : ViewModelBase, IInitializeAsync//, INavigatedAware
    {
        public MainListViewModel(INavigationService navigationService, IDbService dbService, ISettinngsManager settinngsManager) : base(navigationService, dbService, settinngsManager)
        {
            //ProfileList = new ObservableCollection<ProfileModel>()
            //{
            //   new ProfileModel()
            //   {
            //       NickName = "Ivan",
            //       Name = "Ivan",
            //       ImagePath = "",
            //       Owner = "",
            //       Description = ""
            //   },
            //   new ProfileModel()
            //   {
            //       NickName = "Petro",
            //       Name = "Petro",
            //       ImagePath = "",
            //       Owner = "",
            //       Description = ""
            //   }
            //};
        }

        #region --- Private fields ---

        private ObservableCollection<ProfileModel> profileList;

        private bool isVisible = false;

        
        #endregion

        #region --- Implement interfaces ---
        public Task InitializeAsync(INavigationParameters parameters)
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {

            //if (SettingsManager.ChangeSort && parameters.GetNavigationMode() == NavigationMode.Back)
            //{
            //    List<ProfileModel> profiles = ProfileList.ToList();
            //    profiles.Sort(new ProfileModelComparer(SettingsManager.SortListBy));
            //    ProfileList = new ObservableCollection<ProfileModel>(profiles);
            //    SettingsManager.ChangeSort = false;
            //}
        }

        #endregion

        #region --- Public Properties ---  
        public ObservableCollection<ProfileModel> ProfileList
        {
            get => profileList;
            set => SetProperty(ref profileList, value);
        }
        public bool IsVisible
        {
            get => isVisible;
            set => SetProperty(ref isVisible, value);
        }

        #endregion



    }
}