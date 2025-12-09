using ApplicationLogic.Managers;
using DataAccess.Models;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace Presentation.Views.Reservaciones
{
    public partial class ReservacionUpdateView : UserControl
    {
        public ReservacionUpdateView()
        {
            InitializeComponent();
            DataContext = this;
        }

        public void OnClickBtnUpdate(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtID.Text.Trim()))
            {
                string id = txtID.Text.Trim();
                string entrada = txtEntradaDate.Text;
                string salida = txtSalidaDate.Text;
                string estado = "";
                if (txtEstadoActivo.IsChecked == true)
                    estado = "Activa";
                else if (txtEstadoFinalizado.IsChecked == true)
                    estado = "Finalizada";
                else if (txtEstadoCancelado.IsChecked == true)
                    estado = "Cancelada";
                UpdateCliente(id, entrada, salida, estado);
            }
            else
            {
                txbResultado.Text = $"Insertar datos en los campos de texto";
            }

        }

        private void UpdateCliente(string id, string entrada, string salida, string estado)
        {
            try
            {
                if (!string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(entrada) && !string.IsNullOrEmpty(salida) && !string.IsNullOrEmpty(estado))
                {
                    int rId = int.Parse(id);
                    DateOnly fEntrada = DateOnly.Parse(entrada);
                    DateOnly fSalida = DateOnly.Parse(salida);

                    ReservacionManager reservacion = new ReservacionManager(
                        rId
                    );

                    var values = reservacion.FindValues();

                    HabitacionManager habitacion = new HabitacionManager(
                        values.habitacionId
                    );
                    decimal price = habitacion.HabitacionPrice();
                    int dias = (fSalida.ToDateTime(default(TimeOnly)) - fEntrada.ToDateTime(default(TimeOnly))).Days;

                    ReservacionManager reservacionUpdated = new ReservacionManager(
                            rId,
                            values.clienteId,
                            values.habitacionId,
                            fEntrada,
                            fSalida,
                            dias,
                            price * dias,
                            estado
                    );

                    if (reservacion.FindID())
                    {
                        reservacionUpdated.Update();
                        txbResultado.Text = $"Datos actualizados.";
                    }
                    else
                    {
                        txbResultado.Text = $"Reservacion no existente.";
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
