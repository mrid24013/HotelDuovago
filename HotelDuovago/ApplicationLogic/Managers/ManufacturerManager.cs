using DataAccess.Models;

namespace ApplicationLogic.Managers
{
    public class ManufacturerManager
    {
        #region attributes
        private Guid id;
        private string code;
        private string name;
        #endregion

        #region getters
        public Guid Id { get { return id; } }
        public string Code { get { return code; } }
        public string Name { get { return name; } }
        #endregion

        #region constructors
        public ManufacturerManager(Guid id)
        {
            this.id = id;
            this.code = "";
            this.name = "";
        }

        public ManufacturerManager(Guid id, string code, string name)
        {
            this.id = id;
            this.code = code;
            this.name = name;
        }
        #endregion

        #region methods
        public static List<ManufacturerManager> GetAll()
        {
            List<Manufacturer> manufacturersData = new DataAccess.Repositories.ManufacturerRepository().GetAll();
            List<ManufacturerManager> manufacturers = new List<ManufacturerManager>() { };
            foreach (Manufacturer data in manufacturersData)
            {
                manufacturers.Add(new ManufacturerManager(data.iManufacturer, data.ManufacturerCode, data.ManufacturerName));
            }
            return manufacturers;
        }

        public bool Find()
        {
            Manufacturer manufacturer = new Manufacturer()
            {
                iManufacturer = this.id,
                ManufacturerCode = this.code,
                ManufacturerName = this.name
            };
            bool found = new DataAccess.Repositories.ManufacturerRepository().Find(manufacturer);
            if (found)
            {
                this.code = manufacturer.ManufacturerCode;
                this.name = manufacturer.ManufacturerName;
            }
            return found;
        }

        public bool Insert()
        {
            Manufacturer manufacturer = new Manufacturer()
            {
                iManufacturer = this.id,
                ManufacturerCode = this.code,
                ManufacturerName = this.name
            };
            return new DataAccess.Repositories.ManufacturerRepository().Insert(manufacturer);
        }

        public bool Update()
        {
            Manufacturer manufacturer = new Manufacturer()
            {
                iManufacturer = this.id,
                ManufacturerCode = this.code,
                ManufacturerName = this.name
            };
            return new DataAccess.Repositories.ManufacturerRepository().Update(manufacturer);
        }

        public bool Delete()
        {
            return new DataAccess.Repositories.ManufacturerRepository().Delete(this.id);
        }
        #endregion
    }
}
