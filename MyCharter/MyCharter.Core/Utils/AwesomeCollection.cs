using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace MyCharter.Core.Utils
{
    public class AwesomeCollection<T> : ObservableCollection<T>
    {
        public AwesomeCollection()
            : base()
        {
        }

        public AwesomeCollection(List<T> list)
            : base(list)
        {
        }

        public void Reset(IEnumerable<T> range)
        {
            this.Items.Clear();

            if (range != null)
            {
                AddRange(range);
            }

        }

        public void AddRange(IEnumerable<T> range)
        {
            foreach (var item in range)
            {

                Items.Add(item);
            }

            //Raise the property change!
            this.OnPropertyChanged(new PropertyChangedEventArgs("Count"));
            this.OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }


        public AwesomeCollection(IEnumerable<T> collection)
            : base(collection)
        {
        }

    }
}
