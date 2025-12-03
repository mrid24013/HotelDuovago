using ApplicationLogic.Managers;
using DataAccess.Models;
using DataAccess.Repositories;
using System;
using System.Data.Common;
using System.Windows;
using System.Windows.Controls;
namespace Presentation.Views
{
    public partial class ClienteDeleteView : UserControl
    {
        public ClienteDeleteView()
        {
            InitializeComponent();
            DataContext = this;
        }

        public void OnClickBtnDelete(object sender, RoutedEventArgs e) 
        {
            string idValue = txtSearch.Text.Trim();
            DeleteShippers(idValue);

        }

        private void DeleteShippers(string id)
        {
            try
            {
                int shipperId = Int32.Parse(id);

                ClienteManager shipper = new ClienteManager(
                    shipperId
                );

                if (shipper.Find())
                {
                    shipper.Delete();
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
