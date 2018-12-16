namespace Tailenders.Common
{
    public static partial class ADB2CConstants
    {
        private const string _authorityBase = "https://login.microsoftonline.com/tfp";

        public static string Tenant => "tailenders.onmicrosoft.com";
        public static string ClientId => "02bc7fa6-e227-4061-b303-28f10fca5756";
        public static string RedirectUrl => $"msal{ClientId}://auth";

        public static string EmailSignUpAndInPolicy => "B2C_1_GenericSignInSignUp";
        public static string FacebookSignUpAndInPolicy => "B2C_1_FacebookOnlySignInSignUp";
        public static string ResetPasswordPolicy => "B2C_1_ResetPassword";

        public static string EmailSignUpAndInAuthority => $"{_authorityBase}/{Tenant}/{EmailSignUpAndInPolicy}";
        public static string FacebookSignUpAndInAuthority => $"{_authorityBase}/{Tenant}/{FacebookSignUpAndInPolicy}";
        public static string ResetPasswordAuthority => $"{_authorityBase}/{Tenant}/{ResetPasswordPolicy}";

        public static string[] Scopes => new string[]
            {
                $"https://{Tenant}/api/te.read.only",
                $"https://{Tenant}/api/te.write.only"
            };
    }
}

