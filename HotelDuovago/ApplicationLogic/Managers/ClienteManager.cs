using DataAccess.Models;

namespace ApplicationLogic.Managers
{
    public class ClienteManager
    {
        #region attributes
        private int id;
        private string companyName;
        private string phone;
        #endregion

        #region getters
        public int Id { get { return id; } }
        public string CompanyName { get { return companyName; } }
        public string Phone { get { return phone; } }
        #endregion

        #region constructors
        public ClienteManager(int id)
        {
            this.id = id;
            this.companyName = "";
            this.phone = "";
        }

        public ClienteManager(int id, string companyName, string phone)
        {
            this.id = id;
            this.companyName = companyName;
            this.phone = phone;
        }
        #endregion

        #region methods
        public static List<ClienteManager> GetAll()
        {
            List<Cliente> clienteData = new DataAccess.Repositories.ClienteRepository().GetAll();
            List<ClienteManager> cliente = new List<ClienteManager>() { };
            foreach (Cliente data in clienteData)
            {
                cliente.Add(new ClienteManager(
                    data.ShipperID,
                    data.CompanyName,
                    data.Phone
                ));
            }
            return cliente;
        }

        public bool Find()
        {
            Cliente cliente = new Cliente()
            {
                ShipperID = this.id,
                CompanyName = this.companyName,
                Phone = this.phone
            };
            bool found = new DataAccess.Repositories.ClienteRepository().Find(cliente);
            if (found)
            {
                this.companyName = cliente.CompanyName;
                this.phone = cliente.Phone;
            }
            return found;
        }

        public bool FindID()
        {
            bool found = new DataAccess.Repositories.ClienteRepository().FindID(this.id);
            return found;
        }

        public bool FindCompany()
        {
            bool found = new DataAccess.Repositories.ClienteRepository().FindCompany(this.companyName);
            return found;
        }

        public bool FindPhone()
        {
            bool found = new DataAccess.Repositories.ClienteRepository().FindPhone(this.phone);
            return found;
        }

        public bool Insert()
        {
            Cliente cliente = new Cliente()
            {
                ShipperID = this.id,
                CompanyName = this.companyName,
                Phone = this.phone
            };
            return new DataAccess.Repositories.ClienteRepository().Insert(cliente);
        }

        public bool Update()
        {
            Cliente cliente = new Cliente()
            {
                ShipperID = this.id,
                CompanyName = this.companyName,
                Phone = this.phone
            };
            return new DataAccess.Repositories.ClienteRepository().Update(cliente);
        }

        public bool Delete()
        {
            return new DataAccess.Repositories.ClienteRepository().Delete(this.id);
        }
        #endregion
    }
}
