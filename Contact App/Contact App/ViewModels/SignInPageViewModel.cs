using Contact_App.Services.Settings;
using Contact_App.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using Contact_App.Resources;
using Contact_App.Services.DbService;
using Contact_App.Services.Authorization;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Xamarin.CommunityToolkit.Helpers;
using System.Globalization;

namespace Contact_App.ViewModels
{
    public class SignInPageViewModel : ViewModelBase
    {
        INavigationService _navigationService;

        ISettinngsManager _settinngsManager;
        public SignInPageViewModel(INavigationService navigationService, IDbService dbService, ISettinngsManager settinngsManager, IAuthorization authorization) : base(navigationService, dbService, settinngsManager)
        {
            Title = Resource.SignInTitlePage;
            _navigationService = navigationService;
            _settinngsManager = settinngsManager;
            _authorization = authorization;
        }
       

        #region --- Private fields ---

        private string _login;
        private string _password;
        private bool _IsButtonSignInEnabled;
        IAuthorization _authorization;

        #endregion

        #region --- Commands ---
        public DelegateCommand OnSignUpTapCommand => new DelegateCommand(GoSignUp);
        public DelegateCommand OnSignInTapCommand => new DelegateCommand(AuthorizationUser, CanExecute);

        #endregion

        #region --- Public Properties ---  

        public string Login
        {
            get => _login;
            set => SetProperty(ref _login, value);
        }       
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }       
        public bool IsButtonSignInEnabled
        {
            get => _IsButtonSignInEnabled;
            set => SetProperty(ref _IsButtonSignInEnabled, value);
        }
        public INavigation Navigation { get; set; }

        #endregion

        #region --- Overrides ---
        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);
            if (args.PropertyName == nameof(Login) || args.PropertyName == nameof(Password))
            {
                System.Console.WriteLine(args.PropertyName);

                if (!string.IsNullOrEmpty(Login) && !string.IsNullOrEmpty(Password))
                {
                    IsButtonSignInEnabled = true;
                }
                else
                {
                    IsButtonSignInEnabled = false;
                }
            }
        }


        public override void Initialize(INavigationParameters parameters)
        {
            Login = (string)parameters["login"];
            LocalizationResourceManager.Current.CurrentCulture = new CultureInfo(SettingsManager.Language.ToString());
        }

        #endregion

        #region --- Private Helpers ---

        private async void GoSignUp() => await _navigationService.NavigateAsync(nameof(SignUpPage));

        private async void AuthorizationUser()
        {
            bool result = await _authorization.IsAuthorization(Login, Password);

            if (result)
            {
                SettingsManager.LoggedUser = Login;

                await NavigationService.NavigateAsync(nameof(MainListView));
                
                NavigationPage page = (NavigationPage)App.Current.MainPage;
                while (page.Navigation.NavigationStack.Count > 1)
                    page.Navigation.RemovePage(page.Navigation.NavigationStack[page.Navigation.NavigationStack.Count - 2]);

                Login = "";
                Password = "";

            }
            else
            {                
                UserDialogs.Instance.Alert(Resource.InvalidLoginOrPassword);
                Password = "";
            }
        }
        private bool CanExecute() => _IsButtonSignInEnabled;
        #endregion
    }
}
