using Contact_App.Services.Settings;
using Contact_App.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace Contact_App.ViewModels
{
    public class SignInPageViewModel : BindableBase
    {
        INavigationService _navigationService;

        private ISettinngsManager _settinngsManager;
        public SignInPageViewModel(INavigationService navigationService, ISettinngsManager settinngsManager)
        {
            _navigationService = navigationService;
            _settinngsManager = settinngsManager;
            
           
        }

        //public ICommand SingInTapCommand => new DelegateCommand(OnSignInAsync);
        public DelegateCommand SignUpButtonTapCommand => new DelegateCommand(OnSignInAsync);


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



        public ICommand SignInCommand { protected set; get; }
        public INavigation Navigation { get; set; }

        #endregion



        #region --- Private Helpers ---

        private async void OnSignInAsync() => await _navigationService.NavigateAsync(nameof(SignUpPage));

        #endregion


        #region --- Overrides ---
        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);
            if(args.PropertyName == nameof(Login) || args.PropertyName == nameof(Password))
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

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            //var element = parameters["MyParam"];
            //throw new NotImplementedException();
        }

        #endregion
    }
}
