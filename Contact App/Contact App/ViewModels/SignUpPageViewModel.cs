//using Acr.UserDialogs;
using Contact_App.Services.Registration;
using Contact_App.ServiceData;
using Contact_App.Services.DbService;
using Prism.Commands;
using Prism.Navigation;
using Contact_App.Views;
using Contact_App.Resources;
using Contact_App.Models;
using Contact_App.ViewModels;
using Prism.Mvvm;
using System.ComponentModel;
using System;
using Acr.UserDialogs;

namespace Contact_App.ViewModels
{
    public class SignUpPageViewModel : ViewModelBase
    {
        INavigationService _navigationService;
        public SignUpPageViewModel(INavigationService navigationService, IRegistration registration, IDbService _dbService) : base(navigationService, _dbService)
         {
            _navigationService = navigationService;
            _registration = registration;
        }


        #region --- Private fields ---

        private string _login;
        private string _password;
        private string _confirmPassword;
        private bool _IsButtonSignUpEnabled = false;
        private IRegistration _registration;

        #endregion

        #region --- Commands ---

        public DelegateCommand SingUpTapCommand => new DelegateCommand(RegistrationNewUser);

        #endregion

        #region  --- Properties ---

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
        public bool IsButtonSignUpEnabled 
        {
            get => _IsButtonSignUpEnabled;
            set => SetProperty(ref _IsButtonSignUpEnabled, value);
        }

        #endregion

        #region  --- Private helpers ---

        private async void RegistrationNewUser()
        {
            try
            {               
                CodeUserAuthResult result = await _registration.IsRegistration(Login, Password, ConfirmPassword);

                switch (result)
                {
                    case CodeUserAuthResult.InvalidLogin:                       
                        UserDialogs.Instance.Alert(Resource.InvalidLogin);
                        Login = "";
                        Password = "";
                        ConfirmPassword = "";
                        break;
                    case CodeUserAuthResult.InvalidPassword:
                        UserDialogs.Instance.Alert(Resource.InvalidPassword);
                        Password = "";
                        ConfirmPassword = "";
                        break;
                    case CodeUserAuthResult.PasswordMismatch:
                        UserDialogs.Instance.Alert(Resource.PasswordMissMatch);
                        Password = "";
                        ConfirmPassword = "";
                        break;
                    case CodeUserAuthResult.LoginTaken:
                        UserDialogs.Instance.Alert(Resource.LoginTaken);
                        Login = "";
                        Password = "";
                        ConfirmPassword = "";
                        break;
                    case CodeUserAuthResult.Passed:
                        UserDialogs.Instance.Alert(Resource.AuthenticationSuccess);
                        await DbService.InsertDataAsync(new UserModel
                        {
                            Login = Login,
                            Password = Password
                        });

                        NavigationParameters parameter = new NavigationParameters
                        {
                            { "login", Login }
                        };
                        await NavigationService.NavigateAsync(nameof(SignInPage), parameter);
                        break;
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e);
            }
        }

        #endregion

        #region --- Overrides ---
        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);
            if (args.PropertyName == nameof(Login) || args.PropertyName == nameof(Password) || args.PropertyName == nameof(ConfirmPassword))
            {
                if (!string.IsNullOrEmpty(Login) && !string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(ConfirmPassword))
                {
                    IsButtonSignUpEnabled = true;
                }
                else
                {
                    IsButtonSignUpEnabled = false;
                }
            }
        }
        #endregion

    }
}
