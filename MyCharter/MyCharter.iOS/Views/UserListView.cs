using System;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.iOS.Views;
using MyCharter.Core.ViewModels;
using MyCharter.iOS.CustomControls;
using UIKit;

namespace MyCharter.iOS.Views
{
    [MvxFromStoryboard("MainStoryboard")]
    public partial class UserListView : MvxTableViewController
    {
        public UserListView(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            this.NavigationItem.SetHidesBackButton(true, false);

            MvxDeleteStandardTableViewSource source = new MvxDeleteStandardTableViewSource(ViewModel,tblUsers);
            tblUsers.Source = source;

            tblUsers.SeparatorInset = new UIEdgeInsets(0, 0, 0, 0);

            var addUserButton = new UIBarButtonItem();
            addUserButton.Title = "Add User";
            addUserButton.TintColor = UIColor.White;


            var signOffButton = new UIBarButtonItem();
            signOffButton.Title = "Logout";
            signOffButton.TintColor = UIColor.White;

            var set = this.CreateBindingSet<UserListView, UserListViewModel>();

            set.Bind(source).To(vm => vm.Users);
            set.Bind(addUserButton).To(vm => vm.AddCommand);
            set.Bind(signOffButton).To(vm => vm.LogoutCommand);
            set.Apply();

            this.NavigationItem.SetRightBarButtonItem(addUserButton, true);
            this.NavigationItem.SetLeftBarButtonItem(signOffButton, true);
        }

		public override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);
            NavigationController.SetNavigationBarHidden(false, true);
           
		}

        private UserListViewModel m_ViewModel;
        public new UserListViewModel ViewModel
        {
            get { return m_ViewModel ?? (m_ViewModel = base.ViewModel as UserListViewModel); }
        }

    }
}

