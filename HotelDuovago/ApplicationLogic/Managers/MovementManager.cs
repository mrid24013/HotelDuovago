using DataAccess.Models;

namespace ApplicationLogic.Managers
{
    public class MovementManager
    {
        #region attributes
        private Guid id;
        private ProductManager product;
        private InventoryManager fromInventory;
        private InventoryManager toInventory;
        private MovementTypeManager type;
        private MovementRequestDetailManager requestDetail;
        private int quantity;
        private DateTime createdAt;
        #endregion

        #region getters
        public Guid Id { get { return id; } }
        public ProductManager Product { get { return product; } }
        public InventoryManager FromInventory { get { return fromInventory; } }
        public InventoryManager ToInventory { get { return toInventory; } }
        public MovementTypeManager Type { get { return type; } }
        public MovementRequestDetailManager RequestDetail { get { return requestDetail; } }
        public int Quantity { get { return quantity; } }
        public DateTime CreatedAt { get { return createdAt; } }
        #endregion

        #region constructors
        public MovementManager(Guid id)
        {
            this.id = id;
            this.product = new ProductManager(Guid.Empty);
            this.fromInventory = new InventoryManager(Guid.Empty);
            this.toInventory = new InventoryManager(Guid.Empty);
            this.type = new MovementTypeManager(Guid.Empty);
            this.requestDetail = new MovementRequestDetailManager(Guid.Empty);
            this.quantity = 0;
            this.createdAt = DateTime.Now;
        }

        public MovementManager(
            Guid id,
            ProductManager product,
            InventoryManager fromInventory,
            InventoryManager toInventory,
            MovementTypeManager type,
            MovementRequestDetailManager requestDetail,
            int quantity,
            DateTime createdAt
        )
        {
            this.id = id;
            this.product = product ?? new ProductManager(Guid.Empty);
            this.fromInventory = fromInventory ?? new InventoryManager(Guid.Empty);
            this.toInventory = toInventory ?? new InventoryManager(Guid.Empty);
            this.type = type ?? new MovementTypeManager(Guid.Empty);
            this.requestDetail = requestDetail ?? new MovementRequestDetailManager(Guid.Empty);
            this.quantity = quantity;
            this.createdAt = createdAt;
        }
        #endregion

        #region methods
        public static List<MovementManager> GetAll()
        {
            List<Movement> movementsData = new DataAccess.Repositories.MovementRepository().GetAll();
            List<MovementManager> movements = new List<MovementManager>();

            foreach (Movement data in movementsData)
            {
                ManufacturerManager manufacturer = new ManufacturerManager(data.iManufacturer ?? Guid.Empty);
                ProductManager product = new ProductManager(
                    data.iProduct ?? Guid.Empty,
                    data.ProductCode ?? "",
                    data.ProductDescription ?? "",
                    data.ProductPrice ?? 0,
                    manufacturer
                );

                InventoryManager fromInventory = new InventoryManager(data.iFromInventory ?? Guid.Empty);
                InventoryManager toInventory = new InventoryManager(data.iToInventory ?? Guid.Empty);

                MovementTypeManager type = new MovementTypeManager(
                    data.iMovementType ?? Guid.Empty,
                    data.MovementTypeNumber ?? 0,
                    data.MovementTypeName ?? "",
                    ""
                );

                MovementRequestDetailManager requestDetail = new MovementRequestDetailManager(data.iMovementRequestDetail ?? Guid.Empty);

                movements.Add(new MovementManager(
                    data.iMovement,
                    product,
                    fromInventory,
                    toInventory,
                    type,
                    requestDetail,
                    data.MovementQuantity,
                    data.MovementCreatedAt
                ));
            }

            return movements;
        }

        public bool Find()
        {
            Movement movement = new Movement()
            {
                iMovement = this.id,
                MovementQuantity = this.quantity,
                MovementCreatedAt = this.createdAt,
            };

            bool found = new DataAccess.Repositories.MovementRepository().Find(movement);

            if (found)
            {
                ManufacturerManager manufacturer = new ManufacturerManager(movement.iManufacturer ?? Guid.Empty);
                this.product = new ProductManager(
                    movement.iProduct ?? Guid.Empty,
                    movement.ProductCode ?? "",
                    movement.ProductDescription ?? "",
                    movement.ProductPrice ?? 0,
                    manufacturer
                );

                this.fromInventory = new InventoryManager(movement.iFromInventory ?? Guid.Empty);
                this.toInventory = new InventoryManager(movement.iToInventory ?? Guid.Empty);

                this.type = new MovementTypeManager(
                    movement.iMovementType ?? Guid.Empty,
                    movement.MovementTypeNumber ?? 0,
                    movement.MovementTypeName ?? "",
                    ""
                );

                this.requestDetail = new MovementRequestDetailManager(movement.iMovementRequestDetail ?? Guid.Empty);

                this.quantity = movement.MovementQuantity;
                this.createdAt = movement.MovementCreatedAt;
            }

            return found;
        }

        public bool Insert()
        {
            Movement movement = new Movement()
            {
                iMovement = this.id,
                iProduct = this.product.Id,
                iFromInventory = this.fromInventory.Id,
                iToInventory = this.toInventory.Id,
                iMovementType = this.type.Id,
                iMovementRequestDetail = this.requestDetail.Id,
                MovementQuantity = this.quantity,
                MovementCreatedAt = this.createdAt
            };

            return new DataAccess.Repositories.MovementRepository().Insert(movement);
        }

        public bool Delete()
        {
            return new DataAccess.Repositories.MovementRepository().Delete(this.id);
        }
        #endregion
    }
}
