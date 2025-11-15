using ApplicationLogic.Managers;
using Presentation.Models;
using System.Windows;
using System.Windows.Controls;

namespace Presentation.Views
{
    public partial class ProductView : UserControl
    {
        public ProductView()
        {
            InitializeComponent();
            LoadProducts();
            LoadManufacturers();
            DataContext = this;
        }

        private static readonly List<ComboBoxOption> orderOptions = new List<ComboBoxOption>()
        {
            new ComboBoxOption() { Value = "none", Label = "None", Disabled = false },
            new ComboBoxOption() { Value = "asc_price", Label = "Price: low to high", Disabled = false },
            new ComboBoxOption() { Value = "desc_price", Label = "Price: high to low", Disabled = false }
        };

        private List<ProductManager> products = new List<ProductManager>() { };
        private List<ManufacturerManager> manufacturers = new List<ManufacturerManager>() { };

        public List<ComboBoxOption> OrderOptions { get { return orderOptions; } }
        public List<ProductManager> Products { get { return products; } set { products = value; } }
        public List<ManufacturerManager> Manufacturers { get { return manufacturers; } set { manufacturers = value; } }

        public void OnClickBtnSearch(object sender, RoutedEventArgs e) 
        {
            string searchValue = txtSearch.Text.Trim().ToLower();
            //Filtra en base a la palabra clave
            dataSource.ItemsSource = Products.Where(p => 
            p.Code.ToLower().Contains(searchValue) ||
            p.Description.ToLower().Contains(searchValue) ||
            p.Price.ToString().ToLower().Contains(searchValue) ||
            p.Manufacturer.Code.ToLower().Contains(searchValue) ||
            p.Manufacturer.Code.ToLower().Contains(searchValue)).ToList();
        }

        public void OnChangeCmbOrderOptions(object sender, RoutedEventArgs e)
        {
            // extrae la opcion seleccionada
            ComboBoxOption selectedOption = (ComboBoxOption)cmbOrderOptions.SelectedItem;

            // verifica el ordenamiento
            switch (selectedOption.Value)
            {
                case "asc_price":
                    dataSource.ItemsSource = Products.OrderBy(p => p.Price).ToList();
                    UpdateTotalRows();
                    UpdateTotalPrices();
                    break;
                case "desc_price":
                    dataSource.ItemsSource = Products.OrderByDescending(p => p.Price).ToList();
                    UpdateTotalRows();
                    UpdateTotalPrices();
                    break;
                default:
                    dataSource.ItemsSource = Products.OrderBy(p => p.Price).ToList();
                    UpdateGrid();
                    break;
            }
        }

        public void OnChangeCmbManufacturers(object sender, RoutedEventArgs e)
        {
            // extrae la opcion seleccionada
            ManufacturerManager selectedManufacturer = (ManufacturerManager)cmbManufacturers.SelectedItem;

            // filtra en base al fabricante
            dataSource.ItemsSource = Products.Where(p => p.Manufacturer.Id == selectedManufacturer.Id).ToList();

            // actualiza las cantidades visibles
            UpdateTotalRows();
            UpdateTotalPrices();
        }

        private void UpdateGrid()
        {
            dataSource.AutoGenerateColumns = false;
            dataSource.ItemsSource = Products;
            UpdateTotalRows();
            UpdateTotalPrices();
        }

        private void LoadProducts() 
        {
            Products = ProductManager.GetAll();
            UpdateGrid();
        }

        private void LoadManufacturers()
        {
            Manufacturers = ManufacturerManager.GetAll();
        }

        private void UpdateTotalRows()
        {
            int totalRows = Products.Count;
            txbTotalRows.Text = $"{totalRows.ToString()} Elements";
        }

        private void UpdateTotalPrices()
        {
            decimal totalPrices = Products.Sum(p => p.Price);
            txbTotalPrices.Text = $"{totalPrices.ToString(format: "C")} Acumulated";
        }
    }
}
