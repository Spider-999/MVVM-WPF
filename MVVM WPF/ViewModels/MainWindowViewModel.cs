using MVVM_WPF.Models;
using MVVM_WPF.MVVM;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace MVVM_WPF.ViewModels
{
    internal class MainWindowViewModel : ViewModelBase
    {
        #region Properties
        private ObservableCollection<Item> _items;
		private Item _item;
        #endregion

        #region Relay commands
        private RelayCommand? _addItemCommand => new RelayCommand(execute => AddItem());
        private RelayCommand? _deleteItemCommand => new RelayCommand(execute => DeleteItem(), canExecute => IsItemSelected());
        private RelayCommand? _updateItemCommand => new RelayCommand(execute => UpdateItem(), canExecute => IsItemSelected());  
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

        public RelayCommand? AddItemCommand
        {
            get => _addItemCommand;
        }

        public RelayCommand? DeleteItemCommand
        {
            get => _deleteItemCommand;
        }

        public RelayCommand? UpdateItemCommand
        {
            get => _updateItemCommand;
        }
        #endregion

        #region Constructor
        public MainWindowViewModel()
        {
            _items = new ObservableCollection<Item>();
        }
        #endregion

        #region Command methods
        public void AddItem()
        {
            _items.Add(new Item { Name="NONE", ID="-1", Quantity=-1});
        }

        public void DeleteItem()
        {
            _items.Remove(SelectedItem);
        }

        public void UpdateItem()
        {
            _item.Name = "UPDATED";
            _item.ID = "UPDATED";
            _item.Quantity = 0;
        }
        #endregion

        #region Utility methods
        public bool IsItemSelected()
        {
            return _item != null;
        }
        #endregion
    }
}
