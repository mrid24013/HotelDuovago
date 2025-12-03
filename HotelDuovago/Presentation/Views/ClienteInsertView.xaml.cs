using ApplicationLogic.Managers;
using DataAccess.Models;
using DataAccess.Repositories;
using Microsoft.IdentityModel.Tokens;
using Presentation.Enums;
using Presentation.Models;
using Presentation.Services;
using System.Buffers;
using System.Data.Common;
using System.Windows;
using System.Windows.Controls;

namespace Presentation.Views
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
            string companyName = txtCompanyName.Text.Trim();
            string phone = txtPhone.Text.Trim();
            SaveShippers(companyName, phone);

        }

        private void SaveShippers(string companyName, string phone)
        {
            try
            {
                Random random = new Random();

                ClienteManager shipper = new ClienteManager(
                    random.Next(0, 1000),
                    companyName,
                    phone
                );

                if (!string.IsNullOrEmpty(companyName) && !string.IsNullOrEmpty(phone))
                {
                    if (!shipper.FindCompany() && !shipper.FindPhone())
                    {
                        shipper.Insert();
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
