using ApplicationLogic.Managers;
using System.Windows;
using System.Windows.Controls;

namespace Presentation.Views.Usuarios
{
    public partial class UsuarioDeleteView : UserControl
    {
        public UsuarioDeleteView()
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

                UsuarioManager cliente = new UsuarioManager(
                    Id
                );

                if (cliente.Find())
                {
                    cliente.Delete();
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
