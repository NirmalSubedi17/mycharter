using System;
using MvvmCross.Binding.BindingContext;
using MvvmCross.iOS.Views;
using MyCharter.Core.ViewModels;
using UIKit;

namespace MyCharter.iOS.Views
{
    [MvxFromStoryboard("MainStoryboard")]
    public partial class LoginView : MvxViewController
    {
        public LoginView(IntPtr handle) : base(handle)
        {
        }

		public override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);
            this.NavigationItem.SetHidesBackButton(true, false);
		}
		public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var set = this.CreateBindingSet<LoginView, LoginViewModel>();
            set.Bind(txtUserName).To(vm => vm.UserName);
            set.Bind(txtPassword).To(vm => vm.Password);
            set.Bind(cmdLogin).To(vm => vm.SubmitCommand);
            set.Bind(cmdGotoSignup).To(vm => vm.GotoSignupCommand);
            set.Bind(cmdForgotPassword).To(vm => vm.ForgotCommand);
            set.Apply();
        }
    }
}

