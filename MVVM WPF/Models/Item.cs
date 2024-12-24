namespace MVVM_WPF.Models
{
    internal class Item
    {
        #region Private properties
        private string _id;
        private string _name;
        private int _quantity;
        #endregion

        #region Getters & setters
        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public int Quantity
        {
            get { return _quantity; }
            set { _quantity = value; }
        }
        #endregion
    }
}
