using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using WarehouseApp.ViewModels;
using WarehouseLibrary.Data;
using WarehouseLibrary.Services;

namespace WarehouseApp
{
    public partial class MainWindow : Window
    {
        private GridViewColumnHeader _lastHeaderClicked = null;
        private ListSortDirection _lastDirection = ListSortDirection.Ascending;

        public MainWindow()
        {
            InitializeComponent();
            var dataManager = new DataManager("products.json");
            var searchService = new SearchService();
            var warehouseService = new WarehouseService(dataManager, searchService);
            DataContext = new MainViewModel(warehouseService);
        }

        private void GridViewColumnHeader_Click(object sender, RoutedEventArgs e)
        {
            var headerClicked = e.OriginalSource as GridViewColumnHeader;
            if (headerClicked?.Tag == null)
                return;

            ListSortDirection direction = ListSortDirection.Ascending;
            if (_lastHeaderClicked == headerClicked && _lastDirection == ListSortDirection.Ascending)
                direction = ListSortDirection.Descending;

            var binding = headerClicked.Tag.ToString();

            var collectionView = CollectionViewSource.GetDefaultView(ProductListView.ItemsSource);
            collectionView.SortDescriptions.Clear();
            collectionView.SortDescriptions.Add(new SortDescription(binding, direction));
            collectionView.Refresh();

            _lastHeaderClicked = headerClicked;
            _lastDirection = direction;
        }
    }
}