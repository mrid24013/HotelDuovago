using DataAccess.Models;

namespace ApplicationLogic.Managers
{
    public class ProductManager
    {
        #region attributes
        private Guid id;
        private string code;
        private string description;
        private decimal price;
        private ManufacturerManager manufacturer;
        #endregion

        #region getters
        public Guid Id { get { return id; } }
        public string Code { get { return code; } }
        public string Description { get { return description; } }
        public decimal Price { get { return price; } }
        public ManufacturerManager Manufacturer { get { return manufacturer; } }
        #endregion

        #region constructors
        public ProductManager(Guid id)
        {
            this.id = id;
            this.code = "";
            this.description = "";
            this.price = 0;
            this.manufacturer = new ManufacturerManager(Guid.Empty);
        }

        public ProductManager(Guid id, string code, string description, decimal price, ManufacturerManager manufacturer)
        {
            this.id = id;
            this.code = code;
            this.description = description;
            this.price = price;
            this.manufacturer = manufacturer;
        }
        #endregion

        #region methods
        public static List<ProductManager> GetAll()
        {
            List<Product> productData = new DataAccess.Repositories.ProductRepository().GetAll();
            List<ProductManager> products = new List<ProductManager>() { };
            foreach (Product data in productData)
            {
                ManufacturerManager manufacturer = new ManufacturerManager(
                    data.iManufacturer,
                    data.ManufacturerCode ?? "",
                    data.ManufacturerName ?? ""
                );

                products.Add(new ProductManager(
                    data.iProduct,
                    data.ProductCode,
                    data.ProductDescription,
                    data.ProductPrice,
                    manufacturer
                ));
            }
            return products;
        }

        public bool Find()
        {
            Product product = new Product()
            {
                iProduct = this.id,
                ProductCode = this.code,
                ProductDescription = this.description,
                ProductPrice = this.price,
                iManufacturer = this.manufacturer.Id
            };
            bool found = new DataAccess.Repositories.ProductRepository().Find(product);
            if (found)
            {
                ManufacturerManager manufacturer = new ManufacturerManager(
                    product.iManufacturer,
                    product.ManufacturerCode ?? "",
                    product.ManufacturerName ?? ""
                );

                this.code = product.ProductCode;
                this.description = product.ProductDescription;
                this.price = product.ProductPrice;
                this.manufacturer = manufacturer;
            }
            return found;
        }

        public bool Insert()
        {
            Product product = new Product()
            {
                iProduct = this.id,
                ProductCode = this.code,
                ProductDescription = this.description,
                ProductPrice = this.price,
                iManufacturer = this.manufacturer.Id
            };
            return new DataAccess.Repositories.ProductRepository().Insert(product);
        }

        public bool Update()
        {
            Product product = new Product()
            {
                iProduct = this.id,
                ProductCode = this.code,
                ProductDescription = this.description,
                ProductPrice = this.price,
                iManufacturer = this.manufacturer.Id
            };
            return new DataAccess.Repositories.ProductRepository().Update(product);
        }

        public bool Delete()
        {
            return new DataAccess.Repositories.ProductRepository().Delete(this.id);
        }
        #endregion
    }
}
