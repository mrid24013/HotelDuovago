using ApplicationLogic.Managers;
using System.Windows;
using System.Windows.Controls;

namespace Presentation.Views.Habitaciones
{
    public partial class HabitacionUpdateView : UserControl
    {
        public HabitacionUpdateView()
        {
            InitializeComponent();
            DataContext = this;
        }

        public void OnClickBtnUpdate(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtID.Text.Trim()))
            {
                int id = Int32.Parse(txtID.Text.Trim());
                int numero = Int32.Parse(txtNumero.Text.Trim());
                string tipo = txtTipo.Text.Trim();
                decimal precio = decimal.Parse(txtPrecio.Text.Trim());
                int capacidad = Int32.Parse(txtCapacidad.Text.Trim());
                string descripcion = txtDescripcion.Text.Trim();
                Boolean disponible = checkDisponible.IsChecked == true;
                UpdateHabitacion(id, numero, tipo, precio, capacidad, descripcion, disponible);
            }
            else
            {
                txbResultado.Text = $"Insertar datos en los campos de texto";
            }

        }

        private void UpdateHabitacion(int id, int numero, string tipo, decimal precio, int capacidad, string descripcion, Boolean disponible)
        {
            try
            {
                HabitacionManager habitacion = new HabitacionManager(
                    id,
                    numero,
                    tipo,
                    precio,
                    capacidad,
                    descripcion,
                    disponible
                );

                if (!id.Equals(null) && !numero.Equals(null) && !string.IsNullOrEmpty(tipo) && !precio.Equals(null) && !capacidad.Equals(null) && !string.IsNullOrEmpty(descripcion) && !disponible.Equals(null))
                {
                    if (habitacion.FindID())
                    {
                        habitacion.Update();
                        txbResultado.Text = $"Datos Actualizados";
                    }
                    else
                    {
                        txbResultado.Text = $"No se encontro habitacion";
                    }
                }
                else
                {
                    txbResultado.Text = $"Insertar datos en todos los campos de texto";
                }
            }
            catch (Exception)
            {
                txbResultado.Text = $"Ocurrio un error";
            }
        }
    }
}
