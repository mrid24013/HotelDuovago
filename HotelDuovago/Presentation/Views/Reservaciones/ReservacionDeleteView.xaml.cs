using ApplicationLogic.Managers;
using System.Windows;
using System.Windows.Controls;

namespace Presentation.Views.Reservaciones
{
    public partial class ReservacionDeleteView : UserControl
    {
        public ReservacionDeleteView()
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

                ReservacionManager reservacion = new ReservacionManager(
                    Id
                );

                if (reservacion.Find())
                {
                    reservacion.Delete();
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
