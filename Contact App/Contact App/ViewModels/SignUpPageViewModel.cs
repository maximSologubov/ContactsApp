//using Acr.UserDialogs;
using Contact_App.Registration;
using Contact_App.ServiceData;
using Contact_App.Services.DbService;
using Prism.Commands;
using Prism.Navigation;
using Contact_App.Views;
using Contact_App.Resources;
using Contact_App.Models;
using Contact_App.ViewModels;
using Prism.Mvvm;

namespace Contact_App.ViewModels
{
    public class SignUpPageViewModel : BindableBase //ViewModelBase
    {
        INavigationService _navigationService;
        public SignUpPageViewModel(INavigationService navigationService/*, IDbService _dbService, IRegistration registration*/) //: base(navigationService, _dbService)
         {
            _navigationService = navigationService;
            //registrationService = registration;
        }


        #region Private fields

        private string _login;
        private string _password;
        private string _confirmPassword;
        private bool _IsButtonSignInEnabled;
        private IRegistration registrationService;


        #endregion


        //INavigationService _navigationService;

        //public ICommand SignUpCommand { protected set; get; }
        //public INavigation Navigation { get; set; }



        #region Commands

        public DelegateCommand SignUpButtonTapCommand => new DelegateCommand(RegistrationNewUser, CanExecute);

        #endregion

        #region Properties

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
        public string ConfirmPassword
        {
            get => _confirmPassword;
            set => SetProperty(ref _confirmPassword, value);
        }
        public bool IsButtonSignInEnabled
        {
            get => _IsButtonSignInEnabled;
            set => SetProperty(ref _IsButtonSignInEnabled, value);
        }

        #endregion


        #region Private helpers

        private async void RegistrationNewUser()
        {
            CodeUserAuthResult result = await registrationService.IsRegistration(Login, Password, ConfirmPassword);

            switch (result)
            {
                case CodeUserAuthResult.InvalidLogin:
                    //UserDialogs.Instance.Alert(Resource.INVALID_LOGIN);
                    Login = "";
                    Password = "";
                    ConfirmPassword = "";
                    break;
                case CodeUserAuthResult.InvalidPassword:
                    //UserDialogs.Instance.Alert(Resource.INVALID_PASSWORD);
                    Password = "";
                    ConfirmPassword = "";
                    break;
                case CodeUserAuthResult.PasswordMismatch:
                    //UserDialogs.Instance.Alert(Resource.PASSWORD_MISMATCH);
                    Password = "";
                    ConfirmPassword = "";
                    break;
                case CodeUserAuthResult.LoginTaken:
                    //UserDialogs.Instance.Alert(Resource.LOGIN_TAKEN);
                    Login = "";
                    Password = "";
                    ConfirmPassword = "";
                    break;
                case CodeUserAuthResult.Passed:
                    //UserDialogs.Instance.Alert(Resource.AUTHETICATION_SUCCESS);
                    //await DbService.InsertDataAsync(new UserModel
                    //{
                    //    Login = Login,
                    //    Password = Password
                    //});

                    //NavigationParameters parameter = new NavigationParameters
                    //{
                    //    { "login", Login }
                    //};
                    //await NavigationService.NavigateAsync(nameof(SignInPageViewModel), parameter);
                    break;
            }
        }

        private bool CanExecute() => IsButtonSignInEnabled;

        #endregion

    }
}
