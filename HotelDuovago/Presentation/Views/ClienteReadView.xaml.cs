using ApplicationLogic.Managers;
using DataAccess.Repositories;
using Presentation.Enums;
using Presentation.Models;
using Presentation.Services;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace Presentation.Views
{
    public partial class ClienteReadView : UserControl
    {
        public ClienteReadView()
        {
            InitializeComponent();
            LoadShippers();
            DataContext = this;
        }

        private List<ClienteManager> shippers = new List<ClienteManager>() { };
        public List<ClienteManager> Shippers { get { return shippers; } set { shippers = value; } }

        public void OnClickBtnSearch(object sender, RoutedEventArgs e) 
        {
            string searchValue = txtSearch.Text.Trim().ToLower();
            //Filtra en base a la palabra clave
            try
            {
                dataSource.ItemsSource = Shippers.Where(s =>
                s.Id.Equals(Int32.Parse(searchValue))).ToList();
            }
            catch (Exception)
            {
                dataSource.ItemsSource = Shippers.Where(s =>
                s.CompanyName.ToLower().Contains(searchValue) ||
                s.Phone.ToLower().Contains(searchValue)).ToList();
            }
        }

        private void UpdateGrid()
        {
            dataSource.AutoGenerateColumns = false;
            dataSource.ItemsSource = Shippers;
            UpdateTotalRows();
        }

        private void LoadShippers() 
        {
            Shippers = ClienteManager.GetAll();
            UpdateGrid();
        }

        private void UpdateTotalRows()
        {
            int totalRows = Shippers.Count;
            txbTotalRows.Text = $"{totalRows.ToString()} Elements";
        }

        private void ExportToExcel(object sender, RoutedEventArgs e)
        {
            // Generar las columnas
            List<ExcelColumn> columns = new List<ExcelColumn>()
            {
                new ExcelColumn { Name = "CompanyName", Type = ColumnType.String},
                new ExcelColumn { Name = "Phone", Type = ColumnType.String}
            };

            // Obtenemos los datos
            var shippersData = new ClienteRepository().GetAll();

            // Convertimos la data acorde al modelo del reporte
            var data = new List<ClienteReport>() { };
            foreach (var shipper in shippersData)
            {
                data.Add(new ClienteReport()
                {
                    CompanyName = shipper.CompanyName,
                    Phone = shipper.Phone
                });
            }

            // Mandamos a llamar al servicio
            ExcelExportService.Handle(columns, data, "ShipperReport");
        }
    }
}
