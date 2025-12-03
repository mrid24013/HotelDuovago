using System.Windows;

using Presentation.Views;

namespace Presentation
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void HandleClienteReadView(object sender, RoutedEventArgs e)
        {
            DataContext = new ClienteReadView();
        }

        private void HandleClienteUpdateView(object sender, RoutedEventArgs e)
        {
            DataContext = new ClienteUpdateView();
        }

        private void HandleClienteInsertView(object sender, RoutedEventArgs e)
        {
            DataContext = new ClienteInsertView();
        }

        private void HandleClienteDeleteView(object sender, RoutedEventArgs e)
        {
            DataContext = new ClienteDeleteView();
        }
    }
}