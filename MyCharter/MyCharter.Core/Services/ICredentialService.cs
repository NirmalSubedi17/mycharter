using System;
using System.Collections.Generic;
using MyCharter.Core.Entities;

namespace MyCharter.Core.Services
{
    public interface ICredentialService
    {
        bool SaveCredential(UserModel userCredential);
        bool DeleteCredential(UserModel userCredential);
        UserModel GetCredential(string userName, string password);
        ICollection<UserModel> GetAllCredential();
        bool UserAccountExists(string userName);
    }
}
