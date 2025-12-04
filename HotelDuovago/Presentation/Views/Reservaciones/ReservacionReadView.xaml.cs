using ApplicationLogic.Managers;
using DataAccess.Repositories;
using Presentation.Enums;
using Presentation.Models;
using Presentation.Services;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace Presentation.Views.Reservaciones
{
    public partial class ReservacionReadView : UserControl
    {
        public ReservacionReadView()
        {
            InitializeComponent();
            LoadClientes();
            DataContext = this;
        }

        private List<ReservacionManager> reservaciones = new List<ReservacionManager>() { };
        public List<ReservacionManager> Reservaciones { get { return reservaciones; } set { reservaciones = value; } }

        public void OnClickBtnSearch(object sender, RoutedEventArgs e) 
        {
            string searchValue = txtSearch.Text.Trim().ToLower();
            //Filtra en base a la palabra clave
            try
            {
                dataSource.ItemsSource = Reservaciones.Where(c =>
                c.Id.Equals(Int32.Parse(searchValue)) ||
                c.Telefono.ToLower().Contains(searchValue)).ToList();
            }
            catch (Exception)
            {
                try
                {
                    dataSource.ItemsSource = Reservaciones.Where(c =>
                    c.FechaRegistro.Equals(DateTime.Parse(searchValue))).ToList();
                }
                catch (Exception)
                {
                    dataSource.ItemsSource = Reservaciones.Where(c =>
                    c.Nombre.ToLower().Contains(searchValue) ||
                    c.Telefono.ToLower().Contains(searchValue) ||
                    c.Email.ToLower().Contains(searchValue) ||
                    c.Direccion.ToLower().Contains(searchValue)).ToList();
                }
            }
        }

        private void UpdateGrid()
        {
            dataSource.AutoGenerateColumns = false;
            dataSource.ItemsSource = Reservaciones;
            UpdateTotalRows();
        }

        private void LoadClientes() 
        {
            Reservaciones = ReservacionManager.GetAll();
            UpdateGrid();
        }

        private void UpdateTotalRows()
        {
            int totalRows = Reservaciones.Count;
            txbTotalRows.Text = $"{totalRows.ToString()} Elements";
        }

        private void ExportToExcel(object sender, RoutedEventArgs e)
        {
            // Generar las columnas
            List<ExcelColumn> columns = new List<ExcelColumn>()
            {
                new ExcelColumn { Name = "Nombre", Type = ColumnType.String},
                new ExcelColumn { Name = "Telefono", Type = ColumnType.String},
                new ExcelColumn { Name = "Email", Type = ColumnType.String},
                new ExcelColumn { Name = "Direccion", Type = ColumnType.String},
                new ExcelColumn { Name = "FechaRegistro", Type = ColumnType.DateTime}
            };

            // Obtenemos los datos
            var reservacionesData = new ReservacionRepository().GetAll();

            // Convertimos la data acorde al modelo del reporte
            var data = new List<ReservacionReport>() { };
            foreach (var reservacion in reservacionesData)
            {
                data.Add(new ReservacionReport()
                {
                    Nombre = reservacion.Nombre,
                    Telefono = reservacion.Telefono,
                    Email = reservacion.Email,
                    Direccion = reservacion.Direccion,
                    FechaRegistro = reservacion.FechaRegistro
                });
            }

            // Mandamos a llamar al servicio
            ExcelExportService.Handle(columns, data, "ClienteReport");
        }
    }
}
