using ApplicationLogic.Managers;
using DataAccess.Models;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Presentation.Views.Reservaciones
{
    public partial class ReservacionInsertView : UserControl
    {
        public ReservacionInsertView()
        {
            InitializeComponent();
            DataContext = this;
        }

        private List<ClienteManager> clientes = new List<ClienteManager>() { };
        public List<ClienteManager> Clientes { get { return clientes; } set { clientes = value; } }

        private List<HabitacionManager> habitaciones = new List<HabitacionManager>() { };
        public List<HabitacionManager> Habitaciones { get { return habitaciones; } set { habitaciones = value; } }

        public void OnClickBtnSave(object sender, RoutedEventArgs e) 
        {
            string clienteID = txtCliente.Text.Trim();
            string habitacionID = txtHabitacion.Text.Trim();
            string entrada = txtEntradaDate.Text;
            string salida = txtSalidaDate.Text;
            SaveReservaciones(clienteID, habitacionID, entrada, salida);
        }

        private void SaveReservaciones(string clienteID, string habitacionID, string entrada, string salida)
        {
            try
            {
                if (!string.IsNullOrEmpty(clienteID) && !string.IsNullOrEmpty(habitacionID))
                {
                    int clienteid = Int32.Parse(clienteID);
                    int habitacionid = Int32.Parse(habitacionID);
                    DateTime fEntrada = DateTime.Parse(txtEntradaDate.Text);
                    DateTime fSalida = DateTime.Parse(txtSalidaDate.Text);

                    ClienteManager cliente = new ClienteManager(
                        clienteid
                    );

                    HabitacionManager habitacion = new HabitacionManager(
                        habitacionid
                    );

                    if (cliente.FindID() && habitacion.FindID())
                    {
                        decimal price = habitacion.HabitacionPrice();
                        int dias = (fSalida - fEntrada).Days;

                        Random random = new Random();

                        ReservacionManager reservacion = new ReservacionManager(
                            random.Next(0, 1000),
                            clienteid,
                            habitacionid,
                            fEntrada,
                            fSalida,
                            dias,
                            price * dias,
                            "Activa"
                        );
                    }
                    else
                    {
                        txbResultado.Text = $"Cliente / Habitacion no existente.";
                    }
                }
                else
                {
                    txbResultado.Text = $"Insertar datos en los campos de texto";
                }
            }
            catch (Exception)
            {
                txbResultado.Text = $"Ocurrio un error";
            }
        }
    }
}
