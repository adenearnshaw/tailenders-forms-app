﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.AppCenter.Crashes;
using Microsoft.Identity.Client;
using Tailenders.Common;
using Tailenders.Data;
using TailendersApi.Client;

namespace Tailenders.Services
{
    public class AuthenticationService
    {
        private static AuthenticationService _instance;
        public static AuthenticationService Instance => _instance ?? (_instance = new AuthenticationService());

        private readonly PublicClientApplication _client;
        private readonly CredentialsProvider _credentialsProvider;

        public AuthenticationService()
        {
            try
            {
                _client = new PublicClientApplication(ADB2CConstants.ClientId, ADB2CConstants.SignUpAndInAuthority)
                {
                    RedirectUri = ADB2CConstants.RedirectUrl,
                    ValidateAuthority = false
                };
                _credentialsProvider = SimpleIoc.Default.GetInstance<ICredentialsProvider>() as CredentialsProvider;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Crashes.TrackError(ex);
            }

        }

        public async Task<bool> TrySilentLogin()
        {
            AuthenticationResult authResult = null;
            IEnumerable<IAccount> accounts = await _client.GetAccountsAsync();

            try
            {
                IAccount firstAccount = accounts.FirstOrDefault();
                authResult = await _client.AcquireTokenSilentAsync(ADB2CConstants.Scopes, firstAccount);

                if (authResult != null)
                {
                    _credentialsProvider.UpdateCredentials(authResult.UniqueId, authResult.AccessToken);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Crashes.TrackError(e);
            }

            return authResult != null;
        }

        public async Task<bool> TryLogin()
        {
            AuthenticationResult authResult = null;
            IEnumerable<IAccount> accounts = await _client.GetAccountsAsync();

            try
            {
                IAccount firstAccount = accounts.FirstOrDefault();
                authResult = await _client.AcquireTokenSilentAsync(ADB2CConstants.Scopes, firstAccount);
            }
            catch (MsalUiRequiredException ex)
            {
                Debug.WriteLine(ex.Message);

                try
                {
                    authResult = await _client.AcquireTokenAsync(ADB2CConstants.Scopes, App.UiParent);
                }
                catch (MsalException e)
                {
                    Debug.WriteLine(e.Message);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Crashes.TrackError(ex);
                return false;
            }

            if (authResult != null)
            {
                _credentialsProvider.UpdateCredentials(authResult.UniqueId, authResult.AccessToken);
            }
            return true;
        }

        public async Task TryLogout()
        {
            IEnumerable<IAccount> accounts = await _client.GetAccountsAsync();

            foreach (var account in accounts)
            {
                await _client.RemoveAsync(account);
            }
        }

        public async Task TryResetPassword()
        {
            try
            {
                var result = await _client.AcquireTokenAsync(ADB2CConstants.Scopes,
                    (IAccount) null,
                    UIBehavior.SelectAccount,
                    null,
                    null,
                    ADB2CConstants.ResetPasswordAuthority,
                    App.UiParent);
            }
            catch (MsalClientException)
            {
                return;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Crashes.TrackError(e);
                throw;
            }
        }
    }
}
