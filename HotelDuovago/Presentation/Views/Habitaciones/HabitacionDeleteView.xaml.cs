using ApplicationLogic.Managers;
using System.Windows;
using System.Windows.Controls;

namespace Presentation.Views.Habitaciones
{
    public partial class HabitacionDeleteView : UserControl
    {
        public HabitacionDeleteView()
        {
            InitializeComponent();
            DataContext = this;
        }

        public void OnClickBtnDelete(object sender, RoutedEventArgs e) 
        {
            string idValue = txtSearch.Text.Trim();
            DeleteCliente(idValue);
        }

        private void DeleteCliente(string id)
        {
            try
            {
                int Id = Int32.Parse(id);

                HabitacionManager habitacion = new HabitacionManager(
                    Id
                );

                if (habitacion.Find())
                {
                    habitacion.Delete();
                    txbResultado.Text = $"Eliminado";
                }
                else
                {
                    txbResultado.Text = $"No se encontro";
                }
            }
            catch (Exception)
            {
                txbResultado.Text = $"No se encontro";
            }
        }
    }
}
