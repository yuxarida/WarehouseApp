using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using WarehouseLibrary.Models;
using WarehouseLibrary.Services;

namespace WarehouseApp.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly IWarehouseService _warehouseService;
        private string _searchQuery;

        public ObservableCollection<Product> Products { get; }
        public Product SelectedProduct { get; set; }

        public string SearchQuery
        {
            get => _searchQuery;
            set
            {
                if (_searchQuery != value)
                {
                    _searchQuery = value;
                    OnPropertyChanged(nameof(SearchQuery));
                    Search();
                }
            }
        }

        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }

        public MainViewModel(IWarehouseService warehouseService)
        {
            _warehouseService = warehouseService;
            Products = new ObservableCollection<Product>(_warehouseService.GetAll());

            AddCommand = new RelayCommand(_ => AddProduct());
            EditCommand = new RelayCommand(_ => EditProduct(), _ => SelectedProduct != null);
            DeleteCommand = new RelayCommand(_ => DeleteProduct(), _ => SelectedProduct != null);
        }

        private void AddProduct()
        {
            var product = new Product();
            var dialog = new EditProductWindow(product);
            if (dialog.ShowDialog() == true)
            {
                _warehouseService.Add(product);
                Products.Add(product);
            }
        }

        private void EditProduct()
        {
            if (SelectedProduct == null) return;
            var clone = new Product
            {
                Name = SelectedProduct.Name,
                Category = SelectedProduct.Category,
                Quantity = SelectedProduct.Quantity,
                Price = SelectedProduct.Price
            };
            var dialog = new EditProductWindow(clone);
            if (dialog.ShowDialog() == true)
            {
                _warehouseService.Edit(SelectedProduct, clone);
                var idx = Products.IndexOf(SelectedProduct);
                Products[idx] = clone;
            }
        }

        private void DeleteProduct()
        {
            if (SelectedProduct == null) return;
            _warehouseService.Delete(SelectedProduct);
            Products.Remove(SelectedProduct);
        }

        private void Search()
        {
            Products.Clear();
            foreach (var p in _warehouseService.Search(SearchQuery))
                Products.Add(p);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}