using MVVM_WPF.Models;
using MVVM_WPF.MVVM;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace MVVM_WPF.ViewModels
{
    internal class MainWindowViewModel : ViewModelBase
    {
        #region Properties
        private ObservableCollection<Item> _items;
		private Item _item;
        #endregion

        #region Getters & setters
        public ObservableCollection<Item> Items
        {
            get => _items;
            set => _items = value;
        }

        public Item SelectedItem
		{
			get => _item;
            set
            {
                _item = value;
                OnPropertyChanged();
            }
		}
        #endregion

        #region Constructor
        public MainWindowViewModel()
        {
            _items = new ObservableCollection<Item>();
            _items.Add(new Item { ID = "1", Name = "Item 1", Quantity = 10 });
            _items.Add(new Item { ID = "2", Name = "Item 2", Quantity = 5 });
        }
        #endregion
    }
}
