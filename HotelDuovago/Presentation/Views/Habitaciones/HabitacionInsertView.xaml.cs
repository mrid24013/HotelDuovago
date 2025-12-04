using ApplicationLogic.Managers;
using System.Windows;
using System.Windows.Controls;

namespace Presentation.Views.Habitaciones
{
    public partial class HabitacionInsertView : UserControl
    {
        public HabitacionInsertView()
        {
            InitializeComponent();
            DataContext = this;
        }

        public void OnClickBtnSave(object sender, RoutedEventArgs e) 
        {
            int numero = Int32.Parse(txtNumero.Text.Trim());
            string tipo = txtTipo.Text.Trim();
            decimal precio = decimal.Parse(txtPrecio.Text.Trim());
            int capacidad = Int32.Parse(txtCapacidad.Text.Trim());
            string descripcion = txtDescripcion.Text.Trim();
            Boolean disponible = checkDisponible.IsChecked == true;
        SaveHabitaciones(numero, tipo, precio, capacidad, descripcion, disponible);
        }

        private void SaveHabitaciones(int numero, string tipo, decimal precio, int capacidad, string descripcion, Boolean disponible)
        {
            try
            {
                Random random = new Random();

                HabitacionManager habitacion = new HabitacionManager(
                    random.Next(0, 1000),
                    numero, 
                    tipo, 
                    precio, 
                    capacidad, 
                    descripcion, 
                    disponible
                );

                if (!numero.Equals(null) && !string.IsNullOrEmpty(tipo) && !precio.Equals(null) && !capacidad.Equals(null) && !string.IsNullOrEmpty(descripcion) && !disponible.Equals(null))
                {
                    if (!habitacion.FindNumero() && !habitacion.FindDescripcion())
                    {
                        habitacion.Insert();
                        txbResultado.Text = $"Datos Guardado";
                    }
                    else
                    {
                        txbResultado.Text = $"Datos ya existentes";
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
