using System;
using System.Linq;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MyCharter.Core.Entities;
using MyCharter.Core.Helpers;
using MyCharter.Core.Services;

namespace MyCharter.Core.ViewModels
{
    public class LoginViewModel : BaseViewModel
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
        private IMvxCommand gotoSignupCommand;
        public IMvxCommand GotoSignupCommand
        {
            get { return gotoSignupCommand; }
            set
            {
                gotoSignupCommand = value;
                RaisePropertyChanged();
            }
        }
        private IMvxCommand forgotCommand;
        public IMvxCommand ForgotCommand
        {
            get { return forgotCommand; }
            set
            {
                forgotCommand = value;
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
        #endregion


        public LoginViewModel()
        {
            SubmitCommand = new MvxCommand(OnSubmitClick);
            GotoSignupCommand = new MvxCommand(OnGotoSignupClick);
            ForgotCommand = new MvxCommand(OnForgotClick);
        }

		#region Command Handlers
		private async void OnSubmitClick()
        {
            if (!AreInputValid())
            {
                DisplayErrorsDialog();
                return;
            }

            if(CredentialService.Value==null)
            {
                throw new NotImplementedException("Credential Service Implementation not found.");
            }

            var result = CredentialService.Value.GetCredential(this.UserName, this.Password);

            if(result==null)
            {
                await UserDialogService.Value.AlertAsync("Username or Password is invalid.", "Login Failed", "OK");
                return;
            }

            App.LoggedInUser = result;
            await NavigationService.Value.Navigate<UserListViewModel>();
        }

        private async void OnGotoSignupClick()
        {
            await NavigationService.Value.Navigate<SignupViewModel>();
        }

        private async void OnForgotClick()
        {
            var result =await UserDialogService.Value.PromptAsync("Enter the Email ID", "Forgot Password", "Submit", "Cancel", "Type your Email ID", Acr.UserDialogs.InputType.Email);

            if (!result.Ok) return;

            if(!EmailValidator.IsValid(result.Text))
            {
                await UserDialogService.Value.AlertAsync("Invalid Email ID", "Error", "OK");
                return;
            }

            var credentials = CredentialService.Value.GetAllCredential();
            if(credentials==null || credentials.Count==0 || credentials.Where(f=>f.UserName.Trim().ToLower()==result.Text.Trim().ToLower()).FirstOrDefault()==null)
            {
                await UserDialogService.Value.AlertAsync("Email ID does not exists in our System", "Not Found!", "OK");
                return;
            }

            string recoveredPassword = credentials.Where(f => f.UserName.Trim().ToLower() == result.Text.Trim().ToLower()).FirstOrDefault().Password;

            await UserDialogService.Value.AlertAsync("Your Password is : "+recoveredPassword, "Note your Password", "Got it!");

        }
        #endregion


        private bool AreInputValid()
        {
            Errors.Clear();
            if(string.IsNullOrEmpty(this.UserName) || string.IsNullOrWhiteSpace(this.UserName))
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
            if(passwordValidationResult!=null && passwordValidationResult.Count>0)
            {
                foreach(var result in passwordValidationResult)
                {
                    UpdateError(true, result.Key, result.Value);
                }
            }

            return Errors.Count() == 0;
        }

    }
}
