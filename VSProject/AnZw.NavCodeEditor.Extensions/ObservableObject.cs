using System;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnZw.NavCodeEditor.Extensions
{
    public class ObservableObject : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableObject()
        {
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T propertyValue, T newValue, [CallerMemberName]string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(propertyValue, newValue))
                return false;
            propertyValue = newValue;
            OnPropertyChanged(propertyName);
            return true;
        }

    }
}
