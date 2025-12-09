using ApplicationLogic.Managers;
using DataAccess.Models;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Presentation.Views.Reservaciones
{
    public partial class ReservacionInsertView : UserControl
    {
        public ReservacionInsertView()
        {
            InitializeComponent();
            DataContext = this;
        }

        private List<ReservacionManager> reservaciones = new List<ReservacionManager>() { };
        public List<ReservacionManager> Reservaciones { get { return reservaciones; } set { reservaciones = value; } }

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
                    int clienteid = int.Parse(clienteID);
                    int habitacionid = int.Parse(habitacionID);
                    DateOnly fEntrada = DateOnly.Parse(entrada);
                    DateOnly fSalida = DateOnly.Parse(salida);

                    ClienteManager cliente = new ClienteManager(
                        clienteid
                    );

                    HabitacionManager habitacion = new HabitacionManager(
                        habitacionid
                    );

                    if (cliente.FindID() && habitacion.FindID())
                    {
                        decimal price = habitacion.HabitacionPrice();
                        bool available = habitacion.HabitacionAvailable();
                        int dias = (fSalida.ToDateTime(default(TimeOnly)) - fEntrada.ToDateTime(default(TimeOnly))).Days;

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

                        if (available)
                        {
                            if (!reservacion.FindReservacionExistente())
                            {

                                reservacion.Insert();
                                var values = habitacion.FindValues();
                                HabitacionManager updateValue = new HabitacionManager(
                                    habitacionid,
                                    values.numero,
                                    values.tipo,
                                    values.precio,
                                    values.capacidad,
                                    values.descripcion,
                                    false
                                    );

                                updateValue.Update();
                                txbResultado.Text = $"Reservacion agregada.";
                            }
                            else
                            {
                                txbResultado.Text = $"Cliente ya tiene reservacion en estas fechas.";
                            }
                        }
                        else
                        {
                            txbResultado.Text = $"Habitacion no disponible.";
                        }
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
