using DataAccess.Models;

namespace ApplicationLogic.Managers
{
    public class InventoryManager
    {
        #region attributes
        private Guid id;
        private LocationManager location;
        private ProductManager product;
        private UserManager createdBy;
        private int stock;
        private int minStock;
        private int maxStock;
        #endregion

        #region getters
        public Guid Id { get { return id; } }
        public LocationManager Location { get { return location; } }
        public ProductManager Product { get { return product; } }
        public UserManager CreatedBy { get { return createdBy; } }
        public int Stock { get { return stock; } }
        public int MinStock { get { return minStock; } }
        public int MaxStock { get { return maxStock; } }
        #endregion

        #region constructors
        public InventoryManager(Guid id)
        {
            this.id = id;
            this.location = new LocationManager(Guid.Empty);
            this.product = new ProductManager(Guid.Empty);
            this.createdBy = new UserManager(Guid.Empty);
            this.stock = 0;
            this.minStock = 0;
            this.maxStock = 0;
        }

        public InventoryManager(
            Guid id,
            LocationManager location,
            ProductManager product,
            UserManager user,
            int stock,
            int minStock,
            int maxStock
        )
        {
            this.id = id;
            this.location = location;
            this.product = product;
            this.createdBy = user;
            this.stock = stock;
            this.minStock = minStock;
            this.maxStock = maxStock;
        }
        #endregion

        #region methods
        public static List<InventoryManager> GetAll()
        {
            List<Inventory> inventoriesData = new DataAccess.Repositories.InventoryRepository().GetAll();
            List<InventoryManager> inventories = new List<InventoryManager>() { };
            foreach (Inventory data in inventoriesData)
            {
                LocationManager location = new LocationManager(
                    data.iLocation,
                    data.LocationNumber ?? 0,
                    data.LocationName ?? "",
                    data.LocationDescription ?? "",
                    data.LocationEnabled ?? false
                );

                ManufacturerManager manufacturer = new ManufacturerManager(data.iManufacturer ?? Guid.Empty);
                ProductManager product = new ProductManager(
                    data.iProduct,
                    data.ProductCode ?? "",
                    data.ProductDescription ?? "",
                    data.ProductPrice ?? 0,
                    manufacturer
                );

                UserManager user = new UserManager(
                    data.iCreatedBy,
                    data.CreatorNumber ?? 0,
                    data.CreatorFullName ?? "",
                    data.CreatorEmail ?? "",
                    "",
                    new UserRoleManager(Guid.Empty)
                );

                inventories.Add(new InventoryManager(
                    data.iInventory,
                    location,
                    product,
                    user,
                    data.InventoryStock,
                    data.InventoryMinStock,
                    data.InventoryMaxStock
                ));
            }
            return inventories;
        }

        public bool Find()
        {
            Inventory inventory = new Inventory()
            {
                iInventory = this.id,
                iLocation = this.location.Id,
                iProduct = this.product.Id,
                iCreatedBy = this.createdBy.Id,
                InventoryStock = this.stock,
                InventoryMinStock = this.minStock,
                InventoryMaxStock = this.maxStock,
            };
            bool found = new DataAccess.Repositories.InventoryRepository().Find(inventory);
            if (found)
            {
                LocationManager location = new LocationManager(
                    inventory.iLocation,
                    inventory.LocationNumber ?? 0,
                    inventory.LocationName ?? "",
                    inventory.LocationDescription ?? "",
                    inventory.LocationEnabled ?? false
                );

                ManufacturerManager manufacturer = new ManufacturerManager(inventory.iManufacturer ?? Guid.Empty);
                ProductManager product = new ProductManager(
                    inventory.iProduct,
                    inventory.ProductCode ?? "",
                    inventory.ProductDescription ?? "",
                    inventory.ProductPrice ?? 0,
                    manufacturer
                );

                UserManager user = new UserManager(
                    inventory.iCreatedBy,
                    inventory.CreatorNumber ?? 0,
                    inventory.CreatorFullName ?? "",
                    inventory.CreatorEmail ?? "",
                    "",
                    new UserRoleManager(Guid.Empty)
                );

                this.id = inventory.iInventory;
                this.location = location;
                this.product = product;
                this.createdBy = user;
                this.stock = inventory.InventoryStock;
                this.minStock = inventory.InventoryMinStock;
                this.maxStock = inventory.InventoryMaxStock;
            }
            return found;
        }

        public bool Insert()
        {
            Inventory inventory = new Inventory()
            {
                iInventory = this.id,
                iLocation = this.location.Id,
                iProduct = this.product.Id,
                iCreatedBy = this.createdBy.Id,
                InventoryStock = this.stock,
                InventoryMinStock = this.minStock,
                InventoryMaxStock = this.maxStock,
            };
            return new DataAccess.Repositories.InventoryRepository().Insert(inventory);
        }

        public bool Update()
        {
            Inventory inventory = new Inventory()
            {
                iInventory = this.id,
                iLocation = this.location.Id,
                iProduct = this.product.Id,
                iCreatedBy = this.createdBy.Id,
                InventoryStock = this.stock,
                InventoryMinStock = this.minStock,
                InventoryMaxStock = this.maxStock,
            };
            return new DataAccess.Repositories.InventoryRepository().Update(inventory);
        }

        public bool Delete()
        {
            return new DataAccess.Repositories.InventoryRepository().Delete(this.id);
        }
        #endregion
    }
}
