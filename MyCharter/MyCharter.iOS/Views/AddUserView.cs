using System;
using MvvmCross.Binding.BindingContext;
using MvvmCross.iOS.Views;
using MyCharter.Core.ViewModels;
using UIKit;

namespace MyCharter.iOS.Views
{
    [MvxFromStoryboard("MainStoryboard")]
    public partial class AddUserView : MvxViewController
    {
        public AddUserView(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var set = this.CreateBindingSet<AddUserView, AddUserViewModel>();
            set.Bind(txtUserName).To(vm => vm.UserName);
            set.Bind(txtPassword).To(vm => vm.Password);
            set.Bind(txtConfirmPassword).To(vm => vm.ConfirmPassword);
            set.Bind(cmdSubmit).To(vm => vm.SubmitCommand);
            set.Apply();
        }
    }
}

