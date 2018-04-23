using System;
using System.Windows.Input;

namespace MyCharter.Core.Contracts
{
    public interface IRemove
    {
        ICommand RemoveCommand { get; }
    }
}
