using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_WPF.MVVM
{
    internal class ViewModelBase : INotifyPropertyChanged
    {
        #region Event properties
        public event PropertyChangedEventHandler? PropertyChanged;
        #endregion

        #region Event methods
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
