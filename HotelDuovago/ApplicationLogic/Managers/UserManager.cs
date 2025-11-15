using DataAccess.Models;

namespace ApplicationLogic.Managers
{
    public class UserManager
    {
        #region attributes
        private Guid id;
        private int number;
        private string fullName;
        private string email;
        private string password;
        private UserRoleManager role;
        #endregion

        #region getters
        public Guid Id { get { return id; } }
        public int Number { get { return number; } }
        public string FullName { get { return fullName; } }
        public string Email { get { return email; } }
        public string Password { get { return password; } }
        public UserRoleManager Role { get { return role; } }
        #endregion

        #region constructors
        public UserManager(Guid id)
        {
            this.id = id;
            this.number = 0;
            this.fullName = "";
            this.email = "";
            this.password = "";
            this.role = new UserRoleManager(Guid.Empty);
        }

        public UserManager(Guid id, int number, string fullName, string email, string password, UserRoleManager role)
        {
            this.id = id;
            this.number = number;
            this.fullName = fullName;
            this.email = email;
            this.password = password;
            this.role = role;
        }
        #endregion

        #region methods
        public static List<UserManager> GetAll()
        {
            List<User> usersData = new DataAccess.Repositories.UserRepository().GetAll();
            List<UserManager> users = new List<UserManager>() { };
            foreach (User data in usersData)
            {
                UserRoleManager role = new UserRoleManager(
                    data.iUserRole,
                    data.UserRoleName ?? "",
                    data.UserRoleDescription ?? ""
                );

                users.Add(new UserManager(
                    data.iUser,
                    data.UserNumber ?? 0,
                    data.UserFullName,
                    data.UserEmail ?? "",
                    data.UserPassword ?? "",
                    role
                ));
            }
            return users;
        }

        public bool Find()
        {
            User user = new User()
            {
                iUser = this.id,
                UserNumber = this.number,
                UserFullName = this.fullName,
                UserEmail = this.email,
                UserPassword = this.password,
                iUserRole = this.role.Id
            };
            bool found = new DataAccess.Repositories.UserRepository().Find(user);
            if (found)
            {
                UserRoleManager role = new UserRoleManager(
                    user.iUserRole,
                    user.UserRoleName ?? "",
                    user.UserRoleDescription ?? ""
                );

                this.number = user.UserNumber ?? 0;
                this.fullName = user.UserFullName;
                this.email = user.UserEmail ?? "";
                this.password = user.UserPassword ?? "";
                this.role = role;
            }
            return found;
        }

        public bool Insert()
        {
            User user = new User()
            {
                iUser = this.id,
                UserNumber = this.number,
                UserFullName = this.fullName,
                UserEmail = this.email,
                UserPassword = this.password,
                iUserRole = this.role.Id
            };
            return new DataAccess.Repositories.UserRepository().Insert(user);
        }

        public bool Update()
        {
            User user = new User()
            {
                iUser = this.id,
                UserNumber = this.number,
                UserFullName = this.fullName,
                UserEmail = this.email,
                UserPassword = this.password,
                iUserRole = this.role.Id
            };
            return new DataAccess.Repositories.UserRepository().Update(user);
        }

        public bool Delete()
        {
            return new DataAccess.Repositories.UserRepository().Delete(this.id);
        }
        #endregion
    }
}
