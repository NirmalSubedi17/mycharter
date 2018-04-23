using System;
using System.Text;
using Acr.UserDialogs;
using artm.MvxPlugins.Logger.Services;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MyCharter.Core.Utils;

namespace MyCharter.Core.ViewModels
{
    public class BaseViewModel:MvxViewModel
    {
        private ObservableDictionary<string, string> _errors = new ObservableDictionary<string, string>();
        public ObservableDictionary<string, string> Errors
        {
            get { return _errors; }
            set { _errors = value; RaisePropertyChanged(() => Errors); }
        }

        /// <summary>
        /// Updates the error.
        /// </summary>
        /// <param name="isInError">If set to <c>true</c> is in error.</param>
        /// <param name="propertyName">Property name.</param>
        /// <param name="errorMessage">Error message.</param>
        public void UpdateError(bool isInError, string propertyName, string errorMessage)
        {
            if (isInError)
            {
                Errors[propertyName] = errorMessage;
            }
            else
            {
                if (Errors.ContainsKey(propertyName))
                    Errors.Remove(propertyName);
            }
        }

        /// <summary>
        /// The navigation service.
        /// </summary>
        public readonly Lazy<IMvxNavigationService> NavigationService = new Lazy<IMvxNavigationService>(Mvx.Resolve<IMvxNavigationService>);

        /// <summary>
        /// The Logging service.
        /// </summary>
        public readonly Lazy<ILoggerService> LoggingService = new Lazy<ILoggerService>(Mvx.Resolve<ILoggerService>);


        /// <summary>
        /// The User Dialog service.
        /// </summary>
        public readonly Lazy<IUserDialogs> UserDialogService = new Lazy<IUserDialogs>(Mvx.Resolve<IUserDialogs>);

        public BaseViewModel()
        {
        }

        public async void DisplayErrorsDialog()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach(var item in Errors)
            {
                if(!string.IsNullOrEmpty(item.Value))
                    stringBuilder.AppendLine("\u2022 "+item.Value);
            }

            await UserDialogService.Value.AlertAsync(stringBuilder.ToString(), "Please Correct the Following:", "OK");
        }
    }
}
