﻿using Acr.UserDialogs;
using MpesaCross.ViewModels;
using MpesaCross.Views;
using MpesaSdk.Extensions;
using Prism;
using Prism.DryIoc;
using Prism.Ioc;
using Xamarin.Forms;

namespace MpesaCross
{
    public partial class App : PrismApplication
    {
        public App()
        {

        }

        protected override async void OnInitialized()
        {
            Xamarin.Forms.Device.SetFlags(new string[] { "Shapes_Experimental" });
            InitializeComponent();

            await NavigationService.NavigateAsync("/NavigationPage/MpesaPushStkPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MpesaPushStkPage, MpesaPushStkViewModel>();
            containerRegistry.RegisterForNavigation<MpesaResultsPage, MpesaResultsViewModel>();

            containerRegistry.RegisterServices(services =>
            {
                // Initializing mpesaclient httpclient instance using Dependency Injection
                services.AddMpesaService();
            });

            containerRegistry.RegisterInstance<IUserDialogs>(UserDialogs.Instance);
        }

        protected override void OnStart()
        {
            base.OnStart();
        }

        protected override void OnSleep()
        {
            base.OnSleep();
        }

        protected override void OnResume()
        {
            base.OnResume();
        }
    }
}
