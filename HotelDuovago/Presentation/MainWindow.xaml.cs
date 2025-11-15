using System.Windows;

using Presentation.Views;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void HandleInventoryView(object sender, RoutedEventArgs e)
        {
            // DataContext = new InventoryView();
        }

        private void HandleProductsView(object sender, RoutedEventArgs e)
        {
            DataContext = new ProductView();
        }

        private void HandleManufacturersView(object sender, RoutedEventArgs e)
        {
            // DataContext = new ManufacturerView();
        }

        private void HandleInspectionsView(object sender, RoutedEventArgs e)
        {
            // DataContext = new InspectionsView();
        }
    }
}