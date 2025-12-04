using ApplicationLogic.Managers;
using System.Windows;
using System.Windows.Controls;

namespace Presentation.Views.Usuarios
{
    public partial class UsuarioUpdateView : UserControl
    {
        public UsuarioUpdateView()
        {
            InitializeComponent();
            DataContext = this;
        }

        public void OnClickBtnUpdate(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtID.Text.Trim()))
            {
                int id = Int32.Parse(txtID.Text.Trim());
                string nombre = txtNombre.Text.Trim();
                string telefono = txtTelefono.Text.Trim();
                string email = txtEmail.Text.Trim();
                string direccion = txtDireccion.Text.Trim();
                DateTime fechaRegistro = DateTime.Now;
                UpdateCliente(id, nombre, telefono, email, direccion, fechaRegistro);
            }
            else
            {
                txbResultado.Text = $"Insertar datos en los campos de texto";
            }

        }

        private void UpdateCliente(int id, string nombre, string telefono, string email, string direccion, DateTime fechaRegistro)
        {
            try
            {
                UsuarioManager cliente = new UsuarioManager(
                    id,
                    nombre,
                    telefono,
                    email,
                    direccion,
                    fechaRegistro
                );

                if (!id.Equals(null) && !string.IsNullOrEmpty(nombre) && !string.IsNullOrEmpty(telefono) && !string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(direccion) && !fechaRegistro.Equals(null))
                {
                    if (cliente.FindID())
                    {
                        cliente.Update();
                        txbResultado.Text = $"Datos Actualizados";
                    }
                    else
                    {
                        txbResultado.Text = $"Shipper no existente";
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
