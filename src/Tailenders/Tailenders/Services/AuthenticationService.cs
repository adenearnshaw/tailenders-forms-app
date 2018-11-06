﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using Tailenders.Common;

namespace Tailenders.Services
{
    public class AuthenticationService
    {
        private static AuthenticationService _instance;
        public static AuthenticationService Instance => _instance ?? (_instance = new AuthenticationService());

        private readonly PublicClientApplication _client;

        public AuthenticationService()
        {
            try
            {
                _client = new PublicClientApplication(ADB2CConstants.ClientId, ADB2CConstants.Authority)
                {
                    RedirectUri = ADB2CConstants.RedirectUrl,
                    ValidateAuthority = false
                };
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

        }

        public async Task<string> TryLogin()
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
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return authResult?.AccessToken ?? string.Empty;
        }
    }
}