using Acr.UserDialogs;
using artm.MvxPlugins.Logger.Services;
using MvvmCross.Platform;
using MvvmCross.Platform.IoC;
using MyCharter.Core.Entities;
using MyCharter.Core.Services;

namespace MyCharter.Core
{
    public class App : MvvmCross.Core.ViewModels.MvxApplication
    {
        public static readonly string AppName = "MyCharter";
        public static UserModel LoggedInUser { get; set; }

        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();
            
            RegisterNavigationServiceAppStart<ViewModels.LoginViewModel>();
            Mvx.RegisterSingleton<IUserDialogs>(() => UserDialogs.Instance);
            Mvx.RegisterSingleton<ILoggerService>(() =>LoggerService.Instance);
            Mvx.RegisterSingleton<ICredentialService>(() => CredentialService.Instance);
        }


    }
}
