using System.Windows;
using Presentation.Views.Clientes;
using Presentation.Views.Habitaciones;
using Presentation.Views.Reservaciones;

namespace Presentation
{
    public partial class MainWindow : Window
    {
        #region attributes
        private Boolean Clientes = false;
        private Boolean Habitaciones = false;
        private Boolean Reservaciones = false;
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
            HandleHotelReadView(sender, e);
        }

        private void HandleHabitaciones(object sender, RoutedEventArgs e)
        {
            Title = "Proyecto: Habitaciones";
            Clientes = false;
            Habitaciones = true;
            Reservaciones = false;
            HandleHotelReadView(sender, e);
        }

        private void HandleReservaciones(object sender, RoutedEventArgs e)
        {
            Title = "Proyecto: Reservaciones";
            Clientes = false;
            Habitaciones = false;
            Reservaciones = true;
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
        }
    }
}