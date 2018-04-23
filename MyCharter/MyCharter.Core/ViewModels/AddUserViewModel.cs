using System;
using System.Linq;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MyCharter.Core.Entities;
using MyCharter.Core.Helpers;
using MyCharter.Core.Services;

namespace MyCharter.Core.ViewModels
{
    public class AddUserViewModel:BaseViewModel
    {
        private readonly Lazy<ICredentialService> CredentialService = new Lazy<ICredentialService>(Mvx.Resolve<ICredentialService>);

        #region ICommands
        private IMvxCommand submitCommand;
        public IMvxCommand SubmitCommand
        {
            get { return submitCommand; }
            set
            {
                submitCommand = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region ViewModel Fields
        private string userName;
        public string UserName
        {
            get { return userName; }
            set
            {
                userName = value;
                RaisePropertyChanged();
            }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                RaisePropertyChanged();
            }
        }

        private string confirmPassword;
        public string ConfirmPassword
        {
            get { return confirmPassword; }
            set
            {
                confirmPassword = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        public AddUserViewModel()
        {
            SubmitCommand = new MvxCommand(OnSubmitClick);
        }

        #region Command Handlers
        private async void OnSubmitClick()
        {
            if (!AreInputValid())
            {
                DisplayErrorsDialog();
                return;
            }

            if (CredentialService.Value == null)
            {
                throw new NotImplementedException("Credential Service Implementation not found.");
            }

            var result = CredentialService.Value.UserAccountExists(this.UserName);

            if (result)
            {
                await UserDialogService.Value.AlertAsync("Email ID is already in use with other account.", "Already Exists!", "OK");
                return;
            }

            UserModel user = new UserModel() { UserName = this.UserName, Password = this.Password };

            if (CredentialService.Value.SaveCredential(user))
            {
                await UserDialogService.Value.AlertAsync("You have successfully created a New Account.", "Success", "Got it");
                await NavigationService.Value.Navigate<UserListViewModel>();
            }
            else
            {
                await UserDialogService.Value.AlertAsync("We are having trouble creating your Account. Please try again.", "Oops!", "OK");
            }
        }

        #endregion

        private bool AreInputValid()
        {
            Errors.Clear();
            if (string.IsNullOrEmpty(this.UserName) || string.IsNullOrWhiteSpace(this.UserName))
            {
                UpdateError(true, "Email", "Email ID is required");
            }

            if (string.IsNullOrEmpty(this.Password) || string.IsNullOrWhiteSpace(this.Password))
            {
                UpdateError(true, "Password", "Password is required");
            }

            if (Errors.Count > 0) return false; //We dont need to validate anymore if Username or Password is empty;

            if (!EmailValidator.IsValid(this.UserName))
            {
                UpdateError(true, "Email", "Enter the valid Email ID");
            }

            var passwordValidationResult = PasswordValidator.ValidateInput(this.Password);
            if (passwordValidationResult != null && passwordValidationResult.Count > 0)
            {
                foreach (var result in passwordValidationResult)
                {
                    UpdateError(true, result.Key, result.Value);
                }
            }

            if (Errors.Count() == 0)
            {
                //Validate Confirm Password MUST MATCH only when everything (Username, Password) are ready;
                if (!string.Equals(this.Password, this.ConfirmPassword, StringComparison.CurrentCulture))
                {
                    UpdateError(true, "Confirm Password", "Confirm Password must be same as Password");
                }
            }

            return Errors.Count() == 0;
        }
    }
}
