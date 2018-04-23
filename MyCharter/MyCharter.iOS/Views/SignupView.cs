using System;
using MvvmCross.Binding.BindingContext;
using MvvmCross.iOS.Views;
using MyCharter.Core.ViewModels;
using UIKit;

namespace MyCharter.iOS.Views
{
    [MvxFromStoryboard("MainStoryboard")]
    public partial class SignupView : MvxViewController
    {
        public SignupView(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var set = this.CreateBindingSet<SignupView, SignupViewModel>();
            set.Bind(txtUserName).To(vm => vm.UserName);
            set.Bind(txtPassword).To(vm => vm.Password);
            set.Bind(txtConfirmPassword).To(vm => vm.ConfirmPassword);
            set.Bind(cmdSignUp).To(vm => vm.SubmitCommand);
            set.Bind(cmdGotoSignin).To(vm => vm.GotoLoginCommand);
            set.Apply();
        }
    }
}

