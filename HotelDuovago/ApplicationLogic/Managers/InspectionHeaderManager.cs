using DataAccess.Models;

namespace ApplicationLogic.Managers
{
    public class InspectionHeaderManager
    {
        #region attributes
        private Guid id;
        private UserManager createdBy;
        private LocationManager location;
        private int number;
        private string description;
        private DateTime createdAt;
        #endregion

        #region getters
        public Guid Id { get { return id; } }
        public UserManager CreatedBy { get { return createdBy; } }
        public LocationManager Location { get { return location; } }
        public int Number { get { return number; } }
        public string Description { get { return description; } }
        public DateTime CreatedAt { get { return createdAt; } }
        #endregion

        #region constructors
        public InspectionHeaderManager(Guid id)
        {
            this.id = id;
            this.createdBy = new UserManager(Guid.Empty);
            this.location = new LocationManager(Guid.Empty);
            this.number = 0;
            this.description = "";
            this.createdAt = DateTime.Now;
        }

        public InspectionHeaderManager(
            Guid id,
            UserManager createdBy,
            LocationManager location,
            int number,
            string description,
            DateTime createdAt
        )
        {
            this.id = id;
            this.createdBy = createdBy ?? new UserManager(Guid.Empty);
            this.location = location ?? new LocationManager(Guid.Empty);
            this.number = number;
            this.description = description;
            this.createdAt = createdAt;
        }
        #endregion

        #region methods
        public static List<InspectionHeaderManager> GetAll()
        {
            List<InspectionHeader> headersData = new DataAccess.Repositories.InspectionHeaderRepository().GetAll();
            List<InspectionHeaderManager> headers = new List<InspectionHeaderManager>();
            foreach (InspectionHeader data in headersData)
            {
                UserManager user = new UserManager(
                    data.iCreatedBy,
                    data.CreatorNumber ?? 0,
                    data.CreatorFullName ?? "",
                    data.CreatorEmail ?? "",
                    "",
                    new UserRoleManager(Guid.Empty)
                );

                LocationManager location = new LocationManager(
                    data.iLocation,
                    data.LocationNumber ?? 0,
                    data.LocationName ?? "",
                    data.LocationDescription ?? "",
                    data.LocationEnabled ?? false
                );

                // Nota: Si tuvieras detalles relacionados, aquí iría su carga
                List<InspectionDetailManager> details = new List<InspectionDetailManager>();

                headers.Add(new InspectionHeaderManager(
                    data.iInspectionHeader,
                    user,
                    location,
                    data.InspectionHeaderNumber ?? 0,
                    data.InspectionHeaderDescription,
                    data.InspectionHeaderCreatedAt
                ));
            }

            return headers;
        }

        public bool Find()
        {
            InspectionHeader header = new InspectionHeader()
            {
                iInspectionHeader = this.id,
                iCreatedBy = this.createdBy.Id,
                iLocation = this.location.Id,
                InspectionHeaderNumber = this.number,
                InspectionHeaderDescription = this.description,
                InspectionHeaderCreatedAt = this.createdAt
            };

            bool found = new DataAccess.Repositories.InspectionHeaderRepository().Find(header);
            if (found)
            {
                UserManager user = new UserManager(
                    header.iCreatedBy,
                    header.CreatorNumber ?? 0,
                    header.CreatorFullName ?? "",
                    header.CreatorEmail ?? "",
                    "",
                    new UserRoleManager(Guid.Empty)
                );

                LocationManager location = new LocationManager(
                    header.iLocation,
                    header.LocationNumber ?? 0,
                    header.LocationName ?? "",
                    header.LocationDescription ?? "",
                    header.LocationEnabled ?? false
                );

                this.createdBy = user;
                this.location = location;
                this.number = header.InspectionHeaderNumber ?? 0;
                this.description = header.InspectionHeaderDescription;
                this.createdAt = header.InspectionHeaderCreatedAt;
            }

            return found;
        }

        public bool Insert()
        {
            InspectionHeader header = new InspectionHeader()
            {
                iInspectionHeader = this.id,
                iCreatedBy = this.createdBy.Id,
                CreatorNumber = this.createdBy.Number,
                CreatorFullName = this.createdBy.FullName,
                CreatorEmail = this.createdBy.Email,
                iLocation = this.location.Id,
                LocationNumber = this.location.Number,
                LocationName = this.location.Name,
                LocationDescription = this.location.Description,
                LocationEnabled = this.location.Enabled,
                InspectionHeaderNumber = this.number,
                InspectionHeaderDescription = this.description,
                InspectionHeaderCreatedAt = this.createdAt
            };
            return new DataAccess.Repositories.InspectionHeaderRepository().Insert(header);
        }

        public bool Update()
        {
            InspectionHeader header = new InspectionHeader()
            {
                iInspectionHeader = this.id,
                iCreatedBy = this.createdBy.Id,
                CreatorNumber = this.createdBy.Number,
                CreatorFullName = this.createdBy.FullName,
                CreatorEmail = this.createdBy.Email,
                iLocation = this.location.Id,
                LocationNumber = this.location.Number,
                LocationName = this.location.Name,
                LocationDescription = this.location.Description,
                LocationEnabled = this.location.Enabled,
                InspectionHeaderNumber = this.number,
                InspectionHeaderDescription = this.description,
                InspectionHeaderCreatedAt = this.createdAt
            };
            return new DataAccess.Repositories.InspectionHeaderRepository().Update(header);
        }

        public bool Delete()
        {
            return new DataAccess.Repositories.InspectionHeaderRepository().Delete(this.id);
        }
        #endregion
    }
}
