using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MyCharter.Core.Contracts;
using MyCharter.Core.Entities;
using MyCharter.Core.Services;
using MyCharter.Core.Utils;

namespace MyCharter.Core.ViewModels
{
    public class UserListViewModel:BaseViewModel, IRemove
    {
        private readonly Lazy<ICredentialService> CredentialService = new Lazy<ICredentialService>(Mvx.Resolve<ICredentialService>);

        #region ICommands
        private ICommand removeCommand;
        public ICommand RemoveCommand
        {
            get { return removeCommand; }
            set
            {
                removeCommand = value;
                RaisePropertyChanged();
            }
        }

        private IMvxCommand addCommand;
        public IMvxCommand AddCommand
        {
            get { return addCommand; }
            set
            {
                addCommand = value;
                RaisePropertyChanged();
            }
        }

        private IMvxCommand logoutCommand;
        public IMvxCommand LogoutCommand
        {
            get { return logoutCommand; }
            set
            {
                logoutCommand = value;
                RaisePropertyChanged();
            }
        }
        #endregion


        #region ViewModel Fields
        private AwesomeCollection<UserModel>  users;
        public AwesomeCollection<UserModel>  Users
        {
            get { return users; }
            set
            {
                users = value;
                RaisePropertyChanged();
            }
        }


        #endregion

        public UserListViewModel()
        {
            AddCommand = new MvxCommand(OnAddClick);
            LogoutCommand = new MvxCommand(OnLogOffClick);
            RemoveCommand=new MvxCommand<int>(OnDeleteClick);
        }

		public override async Task Initialize()
		{
            UserDialogService.Value.ShowLoading();
            try
            {
                Users = await GetAllUsers();
            }
            catch(Exception ex)
            {
                LoggingService.Value.Log(ex.Message);
            }
            UserDialogService.Value.HideLoading();
		}

        #region Command Handlers
        private async void OnAddClick()
        {
            await NavigationService.Value.Navigate<AddUserViewModel>();
        }
        private async void OnLogOffClick()
        {
            App.LoggedInUser = null;
            await NavigationService.Value.Navigate<LoginViewModel>();
        }

        private async void OnDeleteClick(int item)
        {
            if (Users == null || Users.Count() == 0) return;

            UserModel userToDelete = Users[item];
            if(App.LoggedInUser.UserName.ToLower()==userToDelete.UserName.ToLower())
            {
                await UserDialogService.Value.AlertAsync("We can't let you Delete yourself.", "Oops!", "Got It!");
                return;
            }

            bool result = await UserDialogService.Value.ConfirmAsync("Are you sure to delete an Account?", "Confirm Delete?", "Delete", "Cancel");
            if (!result) return;

            bool deleted= CredentialService.Value.DeleteCredential(userToDelete);

            if(deleted)
            {
                Users.RemoveAt(item);
                await UserDialogService.Value.AlertAsync("You have successfully deleted a Account.", "Deleted", "Got it");
            }
            else
            {
                await UserDialogService.Value.AlertAsync("We are having trouble delete the Account. Please try again.", "Oops!", "OK");
            }

        }

        #endregion

        private async Task<AwesomeCollection<UserModel>> GetAllUsers()
        {
            var result= CredentialService.Value.GetAllCredential();
            await Task.Delay(2000);  //This is just to show the Busy indicator :) 
            return new AwesomeCollection<UserModel>(result);
        }


	}
}
