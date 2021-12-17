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

namespace Contact_App.ViewModels
{
    public class SignInPageViewModel : ViewModelBase
    {
        INavigationService _navigationService;

        private ISettinngsManager _settinngsManager;
        public SignInPageViewModel(INavigationService navigationService, IDbService dbService, ISettinngsManager settinngsManager, IAuthorization authorization) : base(navigationService, dbService, settinngsManager)
        {
            _navigationService = navigationService;
            _settinngsManager = settinngsManager;
            
           
        }

        public DelegateCommand OnSignUpTapCommand => new DelegateCommand(GoSignUp);
        


        #region --- Public Properties ---

        private string _login;
        public string Login
        {
            get => _login;
            set => SetProperty(ref _login, value);
        }
        private string _password;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        private bool _IsButtonSignInEnabled = false;
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
        }

        #endregion


        #region --- Private Helpers ---

        private async void GoSignUp() => await _navigationService.NavigateAsync(nameof(SignUpPage));

        #endregion
    }
}
