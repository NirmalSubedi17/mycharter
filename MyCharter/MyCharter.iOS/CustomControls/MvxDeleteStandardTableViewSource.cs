using System;
using System.Collections.Generic;
using Foundation;
using MvvmCross.Binding.Bindings;
using MvvmCross.Binding.iOS.Views;
using MyCharter.Core.Contracts;
using UIKit;

namespace MyCharter.iOS.CustomControls
{
    /// <summary>
    /// Mvx delete standard table view source.
    /// Taken from : https://gist.github.com/jamesmontemagno/6985403
    /// </summary>
    public class MvxDeleteStandardTableViewSource : MvxStandardTableViewSource
    {

        private IRemove m_ViewModel;


        #region Constructors
        public MvxDeleteStandardTableViewSource(IRemove viewModel, UITableView tableView, UITableViewCellStyle style, NSString cellIdentifier, IEnumerable<MvxBindingDescription> descriptions, UITableViewCellAccessory tableViewCellAccessory = 0)
            : base(tableView, style, cellIdentifier, descriptions, tableViewCellAccessory)
        {
            m_ViewModel = viewModel;
        }


        public MvxDeleteStandardTableViewSource(IRemove viewModel, UITableView tableView, string bindingText) : base(tableView, bindingText)
        {
            m_ViewModel = viewModel;
        }

        public MvxDeleteStandardTableViewSource(IRemove viewModel, UITableView tableView, NSString cellIdentifier) : base(tableView, cellIdentifier)
        {
            m_ViewModel = viewModel;
        }

        public MvxDeleteStandardTableViewSource(IRemove viewModel, UITableView tableView) : base(tableView)
        {
            m_ViewModel = viewModel;
        }


        public MvxDeleteStandardTableViewSource(IRemove viewModel, UITableView tableView, UITableViewCellStyle style, NSString cellId, string binding, UITableViewCellAccessory accessory)
            : base(tableView, style, cellId, binding, accessory)
        {
            m_ViewModel = viewModel;
        }
        #endregion

        public override bool CanEditRow(UITableView tableView, NSIndexPath indexPath)
        {
            return true;
        }


        public override void CommitEditingStyle(UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath)
        {
            switch (editingStyle)
            {
                case UITableViewCellEditingStyle.Delete:
                    m_ViewModel.RemoveCommand.Execute(indexPath.Row);
                    break;
                case UITableViewCellEditingStyle.None:
                    break;
            }
        }

        public override UITableViewCellEditingStyle EditingStyleForRow(UITableView tableView, NSIndexPath indexPath)
        {
            return UITableViewCellEditingStyle.Delete;
        }

        public override bool CanMoveRow(UITableView tableView, NSIndexPath indexPath)
        {
            return false;
        }
    }
}