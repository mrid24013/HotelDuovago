using DataAccess.Models;

namespace ApplicationLogic.Managers
{
    public class MovementTypeManager
    {
        #region attributes
        private Guid id;
        private int number;
        private string name;
        private string description;
        #endregion

        #region getters
        public Guid Id { get { return id; } }
        public int Number { get { return number; } }
        public string Name { get { return name; } }
        public string Description { get { return description; } }
        #endregion

        #region constructors
        public MovementTypeManager(Guid id)
        {
            this.id = id;
            this.number = 0;
            this.name = "";
            this.description = "";
        }

        public MovementTypeManager(Guid id, int number, string name, string description)
        {
            this.id = id;
            this.number = number;
            this.name = name;
            this.description = description;
        }
        #endregion

        #region methods
        public static List<MovementTypeManager> GetAll()
        {
            List<MovementType> movementTypeData = new DataAccess.Repositories.MovementTypeRepository().GetAll();
            List<MovementTypeManager> movementTypes = new List<MovementTypeManager>() { };
            foreach (MovementType data in movementTypeData)
            {
                movementTypes.Add(new MovementTypeManager(
                    data.iMovementType,
                    data.MovementTypeNumber,
                    data.MovementTypeName,
                    data.MovementTypeDescription
                ));
            }
            return movementTypes;
        }

        public bool Find()
        {
            MovementType movementType = new MovementType()
            {
                iMovementType = this.id,
                MovementTypeNumber = this.number,
                MovementTypeName = this.name,
                MovementTypeDescription = this.description
            };
            bool found = new DataAccess.Repositories.MovementTypeRepository().Find(movementType);
            if (found)
            {
                this.id = movementType.iMovementType;
                this.number = movementType.MovementTypeNumber;
                this.name = movementType.MovementTypeName;
                this.description = movementType.MovementTypeDescription;
            }
            return found;
        }

        public bool Insert()
        {
            MovementType movementType = new MovementType()
            {
                iMovementType = this.id,
                MovementTypeNumber = this.number,
                MovementTypeName = this.name,
                MovementTypeDescription = this.description
            };
            return new DataAccess.Repositories.MovementTypeRepository().Insert(movementType);
        }

        public bool Update()
        {
            MovementType movementType = new MovementType()
            {
                iMovementType = this.id,
                MovementTypeNumber = this.number,
                MovementTypeName = this.name,
                MovementTypeDescription = this.description
            };
            return new DataAccess.Repositories.MovementTypeRepository().Update(movementType);
        }

        public bool Delete()
        {
            return new DataAccess.Repositories.MovementTypeRepository().Delete(this.id);
        }
        #endregion
    }
}
