using Contact_App.Services.DbService;
using Prism.Mvvm;
using Prism.Navigation;
using Contact_App.Services.Settings;


namespace Contact_App.ViewModels
{
    public class ViewModelBase : BindableBase, IInitialize
    {
        protected INavigationService NavigationService { get; }
        protected IDbService DbService { get; }
        protected ISettinngsManager SettingsManager { get; }    
        public ViewModelBase(INavigationService navigationService, IDbService dbService, ISettinngsManager settingsManager = null)
        {
            NavigationService = navigationService;
            SettingsManager = settingsManager;
            DbService = dbService;
        }

        #region --- Private fields ---

         private string title;

        #endregion

        #region  --- Properties ---
        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }       
        public virtual void Initialize(INavigationParameters parameters) { }

        #endregion
    }
}