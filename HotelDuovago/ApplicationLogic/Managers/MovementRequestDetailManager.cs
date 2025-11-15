using ApplicationLogic.Enums;

using DataAccess.Models;

namespace ApplicationLogic.Managers
{
    public class MovementRequestDetailManager
    {
        #region attributes
        private Guid id;
        private MovementRequestHeaderManager header;
        private MovementTypeManager type;
        private InventoryManager inventory;
        private string description;
        private int quantity;
        private RequestDetailStatus status;
        #endregion

        #region getters
        public Guid Id { get { return id; } }
        public MovementRequestHeaderManager Header { get { return header; } }
        public MovementTypeManager Type { get { return type; } }
        public InventoryManager Inventory { get { return inventory; } }
        public string Description { get { return description; } }
        public int Quantity { get { return quantity; } }
        public RequestDetailStatus Status { get { return status; } }
        #endregion

        #region constructors
        public MovementRequestDetailManager(Guid id)
        {
            this.id = id;
            this.header = new MovementRequestHeaderManager(Guid.Empty);
            this.type = new MovementTypeManager(Guid.Empty);
            this.inventory = new InventoryManager(Guid.Empty);
            this.description = "";
            this.quantity = 0;
            this.status = RequestDetailStatus.Open;
        }

        public MovementRequestDetailManager(
            Guid id,
            MovementRequestHeaderManager header,
            MovementTypeManager type,
            InventoryManager inventory,
            string description,
            int quantity,
            RequestDetailStatus status
        )
        {
            this.id = id;
            this.header = header ?? new MovementRequestHeaderManager(Guid.Empty);
            this.type = type ?? new MovementTypeManager(Guid.Empty);
            this.inventory = inventory ?? new InventoryManager(Guid.Empty);
            this.description = description;
            this.quantity = quantity;
            this.status = status;
        }
        #endregion

        #region methods
        public static List<MovementRequestDetailManager> GetAll()
        {
            List<MovementRequestDetail> detailsData = new DataAccess.Repositories.MovementRequestDetailRepository().GetAll();
            List<MovementRequestDetailManager> details = new List<MovementRequestDetailManager>();

            foreach (MovementRequestDetail data in detailsData)
            {
                MovementRequestHeaderManager header = new MovementRequestHeaderManager(data.iMovementRequestHeader);
                MovementTypeManager type = new MovementTypeManager(data.iMovementType);
                InventoryManager inventory = new InventoryManager(data.iInventory);

                details.Add(new MovementRequestDetailManager(
                    data.iMovementRequestDetail,
                    header,
                    type,
                    inventory,
                    data.MovementRequestDetailDescription ?? "",
                    data.MovementRequestDetailQuantity,
                    (RequestDetailStatus)data.MovementRequestDetailStatus
                ));
            }

            return details;
        }

        public bool Find()
        {
            MovementRequestDetail detail = new MovementRequestDetail()
            {
                iMovementRequestDetail = this.id,
                iMovementRequestHeader = this.header.Id,
                iMovementType = this.type.Id,
                iInventory = this.inventory.Id,
                MovementRequestDetailQuantity = this.quantity,
                MovementRequestDetailStatus = 0
            };

            bool found = new DataAccess.Repositories.MovementRequestDetailRepository().Find(detail);

            if (found)
            {
                this.header = new MovementRequestHeaderManager(detail.iMovementRequestHeader);
                this.type = new MovementTypeManager(detail.iMovementType);
                this.inventory = new InventoryManager(detail.iInventory);
                this.description = detail.MovementRequestDetailDescription ?? "";
                this.quantity = detail.MovementRequestDetailQuantity;
                this.status = (RequestDetailStatus)detail.MovementRequestDetailStatus;
            }

            return found;
        }

        public bool Insert()
        {
            MovementRequestDetail detail = new MovementRequestDetail()
            {
                iMovementRequestDetail = this.id,
                iMovementRequestHeader = this.header.Id,
                iMovementType = this.type.Id,
                iInventory = this.inventory.Id,
                MovementRequestDetailDescription = this.description,
                MovementRequestDetailQuantity = this.quantity,
                MovementRequestDetailStatus = (int)this.status
            };

            return new DataAccess.Repositories.MovementRequestDetailRepository().Insert(detail);
        }

        public bool Update()
        {
            MovementRequestDetail detail = new MovementRequestDetail()
            {
                iMovementRequestDetail = this.id,
                iMovementRequestHeader = this.header.Id,
                iMovementType = this.type.Id,
                iInventory = this.inventory.Id,
                MovementRequestDetailDescription = this.description,
                MovementRequestDetailQuantity = this.quantity,
                MovementRequestDetailStatus = (int)this.status
            };

            return new DataAccess.Repositories.MovementRequestDetailRepository().Update(detail);
        }

        public bool Delete()
        {
            return new DataAccess.Repositories.MovementRequestDetailRepository().Delete(this.id);
        }
        #endregion
    }
}
