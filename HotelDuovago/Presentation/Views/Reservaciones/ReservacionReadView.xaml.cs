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
            LoadReservaciones();
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
                dataSource.ItemsSource = Reservaciones.Where(r =>
                r.Id.Equals(Int32.Parse(searchValue)) ||
                r.ClienteId.Equals(Int32.Parse(searchValue)) ||
                r.HabitacionId.Equals(Int32.Parse(searchValue)) ||
                r.DiasEstancia.Equals(Int32.Parse(searchValue))).ToList();
            }
            catch (Exception)
            {
                try
                {
                    try
                    {
                        dataSource.ItemsSource = Reservaciones.Where(r =>
                        r.MontoTotal.Equals(decimal.Parse(searchValue))).ToList();
                    }
                    catch (Exception)
                    {
                        dataSource.ItemsSource = Reservaciones.Where(r =>
                        r.FechaEntrada.Equals(DateTime.Parse(searchValue)) ||
                        r.FechaSalida.Equals(DateTime.Parse(searchValue))).ToList();
                    }
                }
                catch (Exception)
                {
                    dataSource.ItemsSource = Reservaciones.Where(r =>
                    r.Estado.ToLower().Contains(searchValue)).ToList();
                }
            }
        }

        private void UpdateGrid()
        {
            dataSource.AutoGenerateColumns = false;
            dataSource.ItemsSource = Reservaciones;
            UpdateTotalRows();
        }

        private void LoadReservaciones() 
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
                new ExcelColumn { Name = "ClienteId", Type = ColumnType.Integer},
                new ExcelColumn { Name = "HabitacionId", Type = ColumnType.Integer},
                new ExcelColumn { Name = "FechaEntrada", Type = ColumnType.DateTime},
                new ExcelColumn { Name = "FechaSalida", Type = ColumnType.DateTime},
                new ExcelColumn { Name = "DiasEstancia", Type = ColumnType.Integer},
                new ExcelColumn { Name = "MontoTotal", Type = ColumnType.Decimal},
                new ExcelColumn { Name = "Estado", Type = ColumnType.String}
            };

            // Obtenemos los datos
            var reservacionesData = new ReservacionRepository().GetAll();

            // Convertimos la data acorde al modelo del reporte
            var data = new List<ReservacionReport>() { };
            foreach (var reservacion in reservacionesData)
            {
                data.Add(new ReservacionReport()
                {
                    ClienteId = reservacion.ClienteId,
                    HabitacionId = reservacion.HabitacionId,
                    FechaEntrada = reservacion.FechaEntrada,
                    FechaSalida = reservacion.FechaSalida,
                    DiasEstancia = reservacion.DiasEstancia,
                    MontoTotal = reservacion.MontoTotal,
                    Estado = reservacion.Estado
                });
            }

            // Mandamos a llamar al servicio
            ExcelExportService.Handle(columns, data, "ReservacionReport");
        }
    }
}
