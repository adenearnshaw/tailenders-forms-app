﻿using System;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.AppCenter.Crashes;
using Tailenders.Managers;
using Tailenders.Navigation;
using Tailenders.Services;
using Tailenders.Views;
using TailendersApi.Client.Exceptions;
using Xamarin.Forms;

namespace Tailenders.ViewModels
{
    public class LoginPageViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;

        public LoginPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            TryLoginCommand = new RelayCommand(async () => await TryLogin());
        }

        public ICommand TryLoginCommand { get; }

        public async Task TryLogin()
        {
            IsBusy = true;

            await AuthenticationService.Instance.TryLogin();
            try
            {
                var profileManager = SimpleIoc.Default.GetInstance<IProfileManager>();
                await profileManager.GetUserProfile();
                Application.Current.MainPage = App.CreateNavigationPage(new MasterPage());
 }
            catch (ProfileDoesntExistException)
            {
                Application.Current.MainPage = App.CreateNavigationPage(new NewProfilePage());
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                Application.Current.MainPage = App.CreateNavigationPage(new ErrorPage());
            }
            
            IsBusy = false;
        }
    }
}