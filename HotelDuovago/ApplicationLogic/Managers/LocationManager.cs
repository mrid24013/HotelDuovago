using DataAccess.Models;

namespace ApplicationLogic.Managers
{
    public class LocationManager
    {
        #region attributes
        private Guid id;
        private int number;
        private string name;
        private string description;
        private bool enabled;
        #endregion


        #region getters
        public Guid Id { get { return id; } }
        public int Number { get { return number; } }
        public string Name { get { return name; } }
        public string Description { get { return description; } }
        public bool Enabled { get { return enabled; } }
        #endregion

        #region constructors
        public LocationManager(Guid id)
        {
            this.id = id;
            this.number = 0;
            this.name = "";
            this.description = "";
            this.enabled = false;
        }

        public LocationManager(Guid id, int number, string name, string description, bool enabled)
        {
            this.id = id;
            this.number = number;
            this.name = name;
            this.description = description;
            this.enabled = enabled;
        }
        #endregion

        #region methods
        public static List<LocationManager> GetAll()
        {
            List<Location> locationsData = new DataAccess.Repositories.LocationRepository().GetAll();
            List<LocationManager> locations = new List<LocationManager>() { };
            foreach (Location data in locationsData)
            {
                locations.Add(new LocationManager(
                    data.iLocation,
                    data.LocationNumber ?? 0,
                    data.LocationName,
                    data.LocationDescription ?? "",
                    data.LocationEnabled ?? false
                ));
            }
            return locations;
        }

        public bool Find()
        {
            Location locations = new Location()
            {
                iLocation = this.id,
                LocationNumber = this.number,
                LocationName = this.name,
                LocationDescription = this.description,
                LocationEnabled = this.enabled
            };
            bool found = new DataAccess.Repositories.LocationRepository().Find(locations);
            if (found)
            {
                this.number = locations.LocationNumber ?? 0;
                this.name = locations.LocationName;
                this.description = locations.LocationDescription;
                this.enabled = locations.LocationEnabled ?? false;
            }
            return found;
        }

        public bool Insert()
        {
            Location locations = new Location()
            {
                iLocation = this.id,
                LocationNumber = this.number,
                LocationName = this.name,
                LocationDescription = this.description,
                LocationEnabled = this.enabled
            };
            return new DataAccess.Repositories.LocationRepository().Insert(locations);
        }

        public bool Update()
        {
            Location locations = new Location()
            {
                iLocation = this.id,
                LocationNumber = this.number,
                LocationName = this.name,
                LocationDescription = this.description,
                LocationEnabled = this.enabled
            };
            return new DataAccess.Repositories.LocationRepository().Update(locations);
        }

        public bool Delete()
        {
            return new DataAccess.Repositories.LocationRepository().Delete(this.id);
        }
        #endregion
    }
}
