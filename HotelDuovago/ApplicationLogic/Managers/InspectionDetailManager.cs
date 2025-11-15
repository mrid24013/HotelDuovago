using DataAccess.Models;

namespace ApplicationLogic.Managers
{
    public class InspectionDetailManager
    {
        #region attributes
        private Guid id;
        private InspectionHeaderManager header;
        private InventoryManager inventory;
        private int expectedQuantity;
        private int realQuantity;
        #endregion

        #region getters
        public Guid Id { get { return id; } }
        public InspectionHeaderManager Header { get { return header; } }
        public InventoryManager Inventory { get { return inventory; } }
        public int ExpectedQuantity { get { return expectedQuantity; } }
        public int RealQuantity { get { return realQuantity; } }
        #endregion

        #region constructors
        public InspectionDetailManager(Guid id)
        {
            this.id = id;
            this.header = new InspectionHeaderManager(Guid.Empty);
            this.inventory = new InventoryManager(Guid.Empty);
            this.expectedQuantity = 0;
            this.realQuantity = 0;
        }

        public InspectionDetailManager(
            Guid id,
            InspectionHeaderManager header,
            InventoryManager inventory,
            int expectedQuantity,
            int realQuantity
        )
        {
            this.id = id;
            this.header = header;
            this.inventory = inventory;
            this.expectedQuantity = expectedQuantity;
            this.realQuantity = realQuantity;
        }
        #endregion

        #region methods
        public static List<InspectionDetailManager> GetAll()
        {
            List<InspectionDetail> inspectionDetailData = new DataAccess.Repositories.InspectionDetailRepository().GetAll();
            List<InspectionDetailManager> inspectionDetails = new List<InspectionDetailManager>() { };
            foreach (InspectionDetail data in inspectionDetailData)
            {
                InspectionHeaderManager header = new InspectionHeaderManager(data.iInspectionHeader);
                InventoryManager inventory = new InventoryManager(data.iInventory);
                inspectionDetails.Add(new InspectionDetailManager(data.iInspectionDetail, header, inventory, data.InspectionDetailExpectedQuantity, data.InspectionDetailRealQuantity ?? 0));
            }
            return inspectionDetails;
        }

        public bool Find()
        {
            InspectionDetail inspectionDetail = new InspectionDetail()
            {
                iInspectionDetail = this.id,
                iInspectionHeader = this.header.Id,
                iInventory = this.inventory.Id,
                InspectionDetailExpectedQuantity = this.expectedQuantity,
                InspectionDetailRealQuantity = this.realQuantity,
            };
            bool found = new DataAccess.Repositories.InspectionDetailRepository().Find(inspectionDetail);
            if (found)
            {
                this.header = new InspectionHeaderManager(inspectionDetail.iInspectionHeader);
                this.inventory = new InventoryManager(inspectionDetail.iInventory);
                this.expectedQuantity = inspectionDetail.InspectionDetailExpectedQuantity;
                this.realQuantity = inspectionDetail.InspectionDetailRealQuantity ?? 0;
            }
            return found;
        }

        public bool Insert()
        {
            InspectionDetail inspectionDetail = new InspectionDetail()
            {
                iInspectionDetail = this.id,
                iInspectionHeader = this.header.Id,
                iInventory = this.inventory.Id,
                InspectionDetailExpectedQuantity = this.expectedQuantity,
                InspectionDetailRealQuantity = this.realQuantity,
            };
            return new DataAccess.Repositories.InspectionDetailRepository().Insert(inspectionDetail);
        }

        public bool Update()
        {
            InspectionDetail inspectionDetail = new InspectionDetail()
            {
                iInspectionDetail = this.id,
                iInspectionHeader = this.header.Id,
                iInventory = this.inventory.Id,
                InspectionDetailExpectedQuantity = this.expectedQuantity,
                InspectionDetailRealQuantity = this.realQuantity,
            };
            return new DataAccess.Repositories.InspectionDetailRepository().Update(inspectionDetail);
        }

        public bool Delete()
        {
            return new DataAccess.Repositories.InspectionDetailRepository().Delete(this.id);
        }
        #endregion
    }
}
