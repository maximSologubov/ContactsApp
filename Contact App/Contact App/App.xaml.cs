using Contact_App.Services.Settings;
using Contact_App.ViewModels;
using Contact_App.Views;
using Prism;
using Prism.Ioc;
using Prism.Unity;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
namespace Contact_App
{
    public partial class App : PrismApplication
    {
        
        public App(IPlatformInitializer initializer = null) : base(initializer) 
        {

        }
        #region --- Private fields
        private ISettinngsManager _settinngsManager;

        #endregion       

        #region --- Overrides ---
        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //Services

            containerRegistry.RegisterInstance<ISettinngsManager>(Container.Resolve<SettingsManager>());
            //Navigation
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<SignInPage, SignInPageViewModel>();
            containerRegistry.RegisterForNavigation<SignUpPage, SignUpPageViewModel>();
            //containerRegistry.RegisterForNavigation<MainListView, MainListViewModel>();
        }

        protected override void OnInitialized()
        {
            InitializeComponent();

            NavigationService.NavigateAsync($"{nameof(SignUpPage)}");
          
            

        }
        #endregion

    }
}
