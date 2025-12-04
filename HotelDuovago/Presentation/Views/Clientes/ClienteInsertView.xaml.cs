using ApplicationLogic.Managers;
using System.Windows;
using System.Windows.Controls;

namespace Presentation.Views.Clientes
{
    public partial class ClienteInsertView : UserControl
    {
        public ClienteInsertView()
        {
            InitializeComponent();
            DataContext = this;
        }

        public void OnClickBtnSave(object sender, RoutedEventArgs e) 
        {
            string nombre = txtNombre.Text.Trim();
            string telefono = txtTelefono.Text.Trim();
            string email = txtEmail.Text.Trim();
            string direccion = txtDireccion.Text.Trim();
            DateTime fechaRegistro = DateTime.Now;
            SaveClientes(nombre, telefono, email, direccion, fechaRegistro);
        }

        private void SaveClientes(string nombre, string telefono, string email, string direccion, DateTime fechaRegistro)
        {
            try
            {
                Random random = new Random();

                ClienteManager cliente = new ClienteManager(
                    random.Next(0, 1000),
                    nombre,
                    telefono,
                    email,
                    direccion,
                    fechaRegistro
                );

                if (!string.IsNullOrEmpty(nombre) && !string.IsNullOrEmpty(telefono) && !string.IsNullOrEmpty(email))
                {
                    if (!cliente.FindTelefono() && !cliente.FindEmail())
                    {
                        cliente.Insert();
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
