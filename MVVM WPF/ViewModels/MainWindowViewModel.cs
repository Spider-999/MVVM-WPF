using MVVM_WPF.Models;
using MVVM_WPF.MVVM;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows;

namespace MVVM_WPF.ViewModels
{
    internal class MainWindowViewModel : ViewModelBase
    {
        #region Private properties
        private ObservableCollection<Item> _items;
		private Item _selectedItem;
        private string _tbName;
        private string _tbID;
        private int _tbQuantity;
        #endregion

        #region Relay commands
        private RelayCommand? _addItemCommand => new RelayCommand(execute => AddItem());
        // Can only delete if an item is selected
        private RelayCommand? _deleteItemCommand => new RelayCommand(execute => DeleteItem(), canExecute => IsItemSelected());
        // Can only save if there are items in the collection
        private RelayCommand? _saveItemsCommand => new RelayCommand(execute => SaveItems(), canExecute => _items.Count() > 0); 
        private RelayCommand? _loadItemsCommand => new RelayCommand(execute => LoadItems());
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

        public RelayCommand? SaveItemsCommand
        {
            get => _saveItemsCommand;
        }

        public RelayCommand? LoadItemsCommand
        {
            get => _loadItemsCommand;
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


        /// <summary>
        /// Save the items to a text file
        /// </summary>
        public void SaveItems()
        {
            string workingDirectory = Environment.CurrentDirectory;
            if (workingDirectory != null)
            {
                string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
                if (projectDirectory != null)
                {
                    string filePath = Path.Combine(projectDirectory, "Items.txt");
                    using (StreamWriter writer = new StreamWriter(filePath))
                    {
                        foreach (Item item in _items)
                        {
                            writer.WriteLine(item.Name + " " + item.ID + " " + item.Quantity);
                        }
                        MessageBox.Show("Items saved");
                    }
                }
            }
        }

        /// <summary>
        /// Load the items from a text file
        /// </summary>
        public void LoadItems()
        {
            string workingDirectory = Environment.CurrentDirectory;
            if (workingDirectory != null)
            {
                string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
                if (projectDirectory != null)
                {
                    string filePath = Path.Combine(projectDirectory, "Items.txt");
                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            string[] itemData = line.Split(' ');
                            Item item = new Item { Name = itemData[0], ID = itemData[1], Quantity = int.Parse(itemData[2]) };
                            _items.Add(item);
                        }
                        MessageBox.Show("Items loaded");
                    }
                }
            }
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
