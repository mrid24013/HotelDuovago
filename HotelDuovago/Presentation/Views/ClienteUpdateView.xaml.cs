using ApplicationLogic.Managers;
using DataAccess.Models;
using Microsoft.IdentityModel.Tokens;
using System.Windows;
using System.Windows.Controls;

namespace Presentation.Views
{
    public partial class ClienteUpdateView : UserControl
    {
        public ClienteUpdateView()
        {
            InitializeComponent();
            DataContext = this;
        }

        public void OnClickBtnUpdate(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtID.Text.Trim()))
            {
                int id = Int32.Parse(txtID.Text.Trim());
                string companyName = txtCompanyName.Text.Trim();
                string phone = txtPhone.Text.Trim();
                UpdateShippers(id, companyName, phone);
            }
            else
            {
                txbResultado.Text = $"Insertar datos en los campos de texto";
            }

        }

        private void UpdateShippers(int id, string companyName, string phone)
        {
            try
            {
                ClienteManager shipper = new ClienteManager(
                    id,
                    companyName,
                    phone
                );

                if (!id.Equals(null) && !string.IsNullOrEmpty(companyName) && !string.IsNullOrEmpty(phone))
                {
                    if (shipper.FindID())
                    {
                        shipper.Update();
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
