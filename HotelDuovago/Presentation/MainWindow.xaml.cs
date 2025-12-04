using System.Windows;
using Presentation.Views.Clientes;
using Presentation.Views.Habitaciones;
using Presentation.Views.Reservaciones;
using Presentation.Views.Usuarios;

namespace Presentation
{
    public partial class MainWindow : Window
    {
        #region attributes
        private Boolean Clientes = false;
        private Boolean Habitaciones = false;
        private Boolean Reservaciones = false;
        private Boolean Usuarios = false;
        #endregion

        public MainWindow()
        {
            InitializeComponent();
        }

        private void HandleClientes(object sender, RoutedEventArgs e)
        {
            Title = "Proyecto: Clientes";
            Clientes = true;
            Habitaciones = false;
            Reservaciones = false;
            Usuarios = false;
            HandleHotelReadView(sender, e);
        }

        private void HandleHabitaciones(object sender, RoutedEventArgs e)
        {
            Title = "Proyecto: Habitaciones";
            Clientes = false;
            Habitaciones = true;
            Reservaciones = false;
            Usuarios = false;
            HandleHotelReadView(sender, e);
        }

        private void HandleReservaciones(object sender, RoutedEventArgs e)
        {
            Title = "Proyecto: Reservaciones";
            Clientes = false;
            Habitaciones = false;
            Reservaciones = true;
            Usuarios = false;
            HandleHotelReadView(sender, e);
        }

        private void HandleUsuarios(object sender, RoutedEventArgs e)
        {
            Title = "Proyecto: Usuarios";
            Clientes = false;
            Habitaciones = false;
            Reservaciones = false;
            Usuarios = true;
            HandleHotelReadView(sender, e);
        }

        private void HandleHotelReadView(object sender, RoutedEventArgs e)
        {
            if (Clientes)
            {
                DataContext = new ClienteReadView();
            }
            else if (Habitaciones)
            {
                DataContext = new HabitacionReadView();
            }
            else if (Reservaciones)
            {
                DataContext = new ReservacionReadView();
            }
            else if (Usuarios)
            {
                DataContext = new UsuarioReadView();
            }
        }

        private void HandleHotelUpdateView(object sender, RoutedEventArgs e)
        {
            if (Clientes)
            {
                DataContext = new ClienteUpdateView();
            }
            else if (Habitaciones)
            {
                DataContext = new HabitacionUpdateView();
            }
            else if (Reservaciones)
            {
                DataContext = new ReservacionUpdateView();
            }
            else if (Usuarios)
            {
                DataContext = new UsuarioUpdateView();
            }
        }

        private void HandleHotelInsertView(object sender, RoutedEventArgs e)
        {
            if (Clientes)
            {
                DataContext = new ClienteInsertView();
            }
            else if (Habitaciones)
            {
                DataContext = new HabitacionInsertView();
            }
            else if (Reservaciones)
            {
                DataContext = new ReservacionInsertView();
            }
            else if (Usuarios)
            {
                DataContext = new UsuarioInsertView();
            }
        }

        private void HandleHotelDeleteView(object sender, RoutedEventArgs e)
        {
            if (Clientes)
            {
                DataContext = new ClienteDeleteView();
            }
            else if (Habitaciones)
            {
                DataContext = new HabitacionDeleteView();
            }
            else if (Reservaciones)
            {
                DataContext = new ReservacionDeleteView();
            }
            else if (Usuarios)
            {
                DataContext = new UsuarioDeleteView();
            }
        }
    }
}