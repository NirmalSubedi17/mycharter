// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace MyCharter.iOS.Views
{
    [Register ("UserListView")]
    partial class UserListView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableView tblUsers { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (tblUsers != null) {
                tblUsers.Dispose ();
                tblUsers = null;
            }
        }
    }
}