using DataAccess.Models;

namespace ApplicationLogic.Managers
{
    public class UserRoleManager
    {
        #region attributes
        private Guid id;
        private string name;
        private string description;
        #endregion

        #region getters
        public Guid Id { get { return id; } }
        public string Name { get { return name; } }
        public string Description { get { return description; } }
        #endregion

        #region constructors
        public UserRoleManager(Guid id)
        {
            this.id = id;
            this.name = "";
            this.description = "";
        }

        public UserRoleManager(Guid id, string name, string description)
        {
            this.id = id;
            this.name = name;
            this.description = description;
        }
        #endregion

        #region methods
        public static List<UserRoleManager> GetAll()
        {
            List<UserRole> userRolesData = new DataAccess.Repositories.UserRoleRepository().GetAll();
            List<UserRoleManager> userRoles = new List<UserRoleManager>() { };
            foreach (UserRole data in userRolesData)
            {
                userRoles.Add(new UserRoleManager(
                    data.iUserRole,
                    data.UserRoleName,
                    data.UserRoleDescription ?? ""
                ));
            }
            return userRoles;
        }

        public bool Find()
        {
            UserRole userRole = new UserRole()
            {
                iUserRole = this.id,
                UserRoleName = this.name,
                UserRoleDescription = this.description
            };
            bool found = new DataAccess.Repositories.UserRoleRepository().Find(userRole);
            if (found)
            {
                this.name = userRole.UserRoleName;
                this.description = userRole.UserRoleDescription;
            }
            return found;
        }

        public bool Insert()
        {
            UserRole userRole = new UserRole()
            {
                iUserRole = this.id,
                UserRoleName = this.name,
                UserRoleDescription = this.description
            };
            return new DataAccess.Repositories.UserRoleRepository().Insert(userRole);
        }

        public bool Update()
        {
            UserRole userRole = new UserRole()
            {
                iUserRole = this.id,
                UserRoleName = this.name,
                UserRoleDescription = this.description
            };
            return new DataAccess.Repositories.UserRoleRepository().Update(userRole);
        }

        public bool Delete()
        {
            return new DataAccess.Repositories.UserRoleRepository().Delete(this.id);
        }
        #endregion
    }
}
