using ApplicationLogic.Managers;
using DataAccess.Repositories;
using Presentation.Enums;
using Presentation.Models;
using Presentation.Services;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace Presentation.Views.Usuarios
{
    public partial class UsuarioReadView : UserControl
    {
        public UsuarioReadView()
        {
            InitializeComponent();
            LoadClientes();
            DataContext = this;
        }

        private List<UsuarioManager> clientes = new List<UsuarioManager>() { };
        public List<UsuarioManager> Clientes { get { return clientes; } set { clientes = value; } }

        public void OnClickBtnSearch(object sender, RoutedEventArgs e) 
        {
            string searchValue = txtSearch.Text.Trim().ToLower();
            //Filtra en base a la palabra clave
            try
            {
                dataSource.ItemsSource = Clientes.Where(c =>
                c.Id.Equals(Int32.Parse(searchValue)) ||
                c.Telefono.ToLower().Contains(searchValue)).ToList();
            }
            catch (Exception)
            {
                try
                {
                    dataSource.ItemsSource = Clientes.Where(c =>
                    c.FechaRegistro.Equals(DateTime.Parse(searchValue))).ToList();
                }
                catch (Exception)
                {
                    dataSource.ItemsSource = Clientes.Where(c =>
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
            dataSource.ItemsSource = Clientes;
            UpdateTotalRows();
        }

        private void LoadClientes() 
        {
            Clientes = UsuarioManager.GetAll();
            UpdateGrid();
        }

        private void UpdateTotalRows()
        {
            int totalRows = Clientes.Count;
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
            var usuariosData = new UsuarioRepository().GetAll();

            // Convertimos la data acorde al modelo del reporte
            var data = new List<UsuarioReport>() { };
            foreach (var usuario in usuariosData)
            {
                data.Add(new UsuarioReport()
                {
                    Nombre = usuario.Nombre,
                    Telefono = usuario.Telefono,
                    Email = usuario.Email,
                    Direccion = usuario.Direccion,
                    FechaRegistro = usuario.FechaRegistro
                });
            }

            // Mandamos a llamar al servicio
            ExcelExportService.Handle(columns, data, "UsuarioReport");
        }
    }
}
