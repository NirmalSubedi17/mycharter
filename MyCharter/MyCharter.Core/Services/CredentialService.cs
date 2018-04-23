using System;
using System.Collections.Generic;
using System.Linq;
using MyCharter.Core.Entities;
using Xamarin.Auth;

namespace MyCharter.Core.Services
{
    public class CredentialService:ICredentialService
    {
        private static CredentialService _instance;
        private CredentialService()
        {
            
        }

        public static CredentialService Instance{
            get{
                if (_instance == null)
                    _instance = new CredentialService();
                return _instance;
            }
        }

        public bool DeleteCredential(UserModel userCredential)
        {
            var account = AccountStore.Create().FindAccountsForService(App.AppName).FirstOrDefault(f=>f.Username.Trim().ToLower()==userCredential.UserName.Trim().ToLower() && f.Properties["Password"]==userCredential.Password);
            if (account == null) return false;

            AccountStore.Create().Delete(account, App.AppName);
            return true;
        }

        public ICollection<UserModel> GetAllCredential()
        {
            var account = AccountStore.Create().FindAccountsForService(App.AppName);
            if (account == null || account.Count() == 0) return null;

            var result = from a in account
                         select new UserModel { UserName = a.Username, Password = a.Properties["Password"] };

            return result.ToList();
        }

        public UserModel GetCredential(string userName, string password)
        {
            var account = AccountStore.Create().FindAccountsForService(App.AppName).FirstOrDefault(f => f.Username.Trim().ToLower() == userName.Trim().ToLower() && f.Properties["Password"] == password);
            if (account == null) return null;
            return new UserModel() { UserName = account.Username, Password = account.Properties["Password"] };
        }

        public bool SaveCredential(UserModel userCredential)
        {
            if (userCredential == null) return false;

            if (!string.IsNullOrWhiteSpace(userCredential.UserName) && !string.IsNullOrWhiteSpace(userCredential.Password))
            {
                Account account = new Account
                {
                    Username = userCredential.UserName
                };
                account.Properties.Add(nameof(userCredential.Password), userCredential.Password);
                AccountStore.Create().Save(account, App.AppName);
            }
            return true;
        }

        public bool UserAccountExists(string userName)
        {
            var account = AccountStore.Create().FindAccountsForService(App.AppName).FirstOrDefault(f => f.Username.Trim().ToLower() == userName.Trim().ToLower());
            if (account == null) return false;
            return true;
        }
    }
}
