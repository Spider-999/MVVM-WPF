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
		private Item _selectedItem;
        private string _tbName;
        private string _tbID;
        private int _tbQuantity;
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
			get => _selectedItem;
            set
            {
                _selectedItem = value;
                OnPropertyChanged();
            }
		}
        public string TbName
        {
            get => _tbName;
            set
            {
                _tbName = value;
                OnPropertyChanged();
            }
        }

        public string TbID
        {
            get => _tbID;
            set
            {
                _tbID = value;
                OnPropertyChanged();
            }
        }

        public int TbQuantity
        {
            get => _tbQuantity;
            set
            {
                _tbQuantity = value;
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
            if (CheckTextBoxes())
            {
                Item itemToAdd = new Item { Name = TbName, ID = TbID, Quantity = TbQuantity };

                if (itemToAdd != null)
                    _items.Add(itemToAdd);
            }
            else
                InvalidTextBoxMessages();

            ClearTextBoxes();
        }

        public void DeleteItem()
        {
            _items.Remove(SelectedItem);
        }

        public void UpdateItem()
        {
            if (CheckTextBoxes())
            {
                _selectedItem.Name = TbName;
                _selectedItem.ID = TbID;
                _selectedItem.Quantity = TbQuantity;

                var index = _items.IndexOf(_selectedItem);
                if (index >= 0)
                {
                    _items.RemoveAt(index);
                    _items.Insert(index, _selectedItem);
                }
            }
            else
                InvalidTextBoxMessages();

            ClearTextBoxes();
        }
        #endregion

        #region Utility methods
        public bool IsItemSelected()
        {
            return SelectedItem != null;
        }

        public bool CheckTextBoxes()
        {
            return !string.IsNullOrEmpty(TbName) && !string.IsNullOrEmpty(TbID) && TbQuantity > 0;
        }

        public void InvalidTextBoxMessages()
        {
            if (string.IsNullOrEmpty(TbName))
                MessageBox.Show("Name is empty");
            else if (string.IsNullOrEmpty(TbID))
                MessageBox.Show("ID is empty");
            else if (TbQuantity <= 0)
                MessageBox.Show("Quantity is less than 1");
        }

        public void ClearTextBoxes()
        {
            TbName = "";
            TbID = "";
            TbQuantity = 0;
        }
        #endregion
    }
}
