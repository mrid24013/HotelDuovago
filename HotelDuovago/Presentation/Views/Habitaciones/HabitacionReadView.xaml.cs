using ApplicationLogic.Managers;
using DataAccess.Repositories;
using Presentation.Enums;
using Presentation.Models;
using Presentation.Services;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace Presentation.Views.Habitaciones
{
    public partial class HabitacionReadView : UserControl
    {
        public HabitacionReadView()
        {
            InitializeComponent();
            LoadHabitaciones();
            DataContext = this;
        }

        private List<HabitacionManager> habitaciones = new List<HabitacionManager>() { };
        public List<HabitacionManager> Habitaciones { get { return habitaciones; } set { habitaciones = value; } }

        public void OnClickBtnSearch(object sender, RoutedEventArgs e) 
        {
            string searchValue = txtSearch.Text.Trim().ToLower();
            //Filtra en base a la palabra clave
            try
            {
                dataSource.ItemsSource = Habitaciones.Where(c =>
                c.Id.Equals(Int32.Parse(searchValue)) ||
                c.Numero.Equals(Int32.Parse(searchValue)) ||
                c.Capacidad.Equals(Int32.Parse(searchValue)) ||
                c.Precio.Equals(decimal.Parse(searchValue))).ToList();
            }
            catch (Exception)
            {
                try
                {
                    dataSource.ItemsSource = Habitaciones.Where(c =>
                    c.Disponible.Equals(Boolean.Parse(searchValue))).ToList();
                }
                catch (Exception)
                {
                    try
                    {
                        dataSource.ItemsSource = Habitaciones.Where(c =>
                        c.Precio.Equals(decimal.Parse(searchValue))).ToList();
                    }
                    catch (Exception)
                    {
                        dataSource.ItemsSource = Habitaciones.Where(c =>
                        c.Tipo.ToLower().Contains(searchValue) ||
                        c.Descripcion.ToLower().Contains(searchValue)).ToList();
                    }
                }
            }
        }

        private void UpdateGrid()
        {
            dataSource.AutoGenerateColumns = false;
            dataSource.ItemsSource = Habitaciones;
            UpdateTotalRows();
        }

        private void LoadHabitaciones() 
        {
            Habitaciones = HabitacionManager.GetAll();
            UpdateGrid();
        }

        private void UpdateTotalRows()
        {
            int totalRows = Habitaciones.Count;
            txbTotalRows.Text = $"{totalRows.ToString()} Elements";
        }

        private void ExportToExcel(object sender, RoutedEventArgs e)
        {
            // Generar las columnas
            List<ExcelColumn> columns = new List<ExcelColumn>()
            {
                new ExcelColumn { Name = "Numero", Type = ColumnType.Integer},
                new ExcelColumn { Name = "Tipo", Type = ColumnType.String},
                new ExcelColumn { Name = "Precio", Type = ColumnType.Double},
                new ExcelColumn { Name = "Capacidad", Type = ColumnType.Integer},
                new ExcelColumn { Name = "Descripcion", Type = ColumnType.String},
                new ExcelColumn { Name = "Disponible", Type = ColumnType.Bool}
            };

            // Obtenemos los datos
            var habitacionesData = new HabitacionRepository().GetAll();

            // Convertimos la data acorde al modelo del reporte
            var data = new List<HabitacionReport>() { };
            foreach (var habitacion in habitacionesData)
            {
                data.Add(new HabitacionReport()
                {
                    Numero = habitacion.Numero,
                    Tipo = habitacion.Tipo,
                    Precio = habitacion.Precio,
                    Capacidad = habitacion.Capacidad,
                    Descripcion = habitacion.Descripcion,
                    Disponible = habitacion.Disponible
                });
            }

            // Mandamos a llamar al servicio
            ExcelExportService.Handle(columns, data, "HabitacionReport");
        }
    }
}
