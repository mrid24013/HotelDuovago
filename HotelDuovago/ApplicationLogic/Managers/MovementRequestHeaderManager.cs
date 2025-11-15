using ApplicationLogic.Enums;

using DataAccess.Models;

namespace ApplicationLogic.Managers
{
    public class MovementRequestHeaderManager
    {
        #region attributes
        private Guid id;
        private UserManager requestedBy;
        private UserManager authorizedBy;
        private int number;
        private string description;
        private RequestHeaderStatus status;
        private DateTime createdAt;
        private DateTime authorizedAt;
        #endregion

        #region getters
        public Guid Id { get { return id; } }
        public UserManager RequestedBy { get { return requestedBy; } }
        public UserManager AuthorizedBy { get { return authorizedBy; } }
        public int Number { get { return number; } }
        public string Description { get { return description; } }
        public RequestHeaderStatus Status { get { return status; } }
        public DateTime CreatedAt { get { return createdAt; } }
        public DateTime AuthorizedAt { get { return authorizedAt; } }
        #endregion

        #region constructors
        public MovementRequestHeaderManager(Guid id)
        {
            this.id = id;
            this.requestedBy = new UserManager(Guid.Empty);
            this.authorizedBy = new UserManager(Guid.Empty);
            this.number = 0;
            this.description = "";
            this.status = RequestHeaderStatus.Open;
            this.createdAt = DateTime.Now;
            this.authorizedAt = DateTime.MinValue;
        }

        public MovementRequestHeaderManager(
            Guid id,
            UserManager requestedBy,
            UserManager authorizedBy,
            int number,
            string description,
            RequestHeaderStatus status,
            DateTime createdAt,
            DateTime authorizedAt
        )
        {
            this.id = id;
            this.requestedBy = requestedBy ?? new UserManager(Guid.Empty);
            this.authorizedBy = authorizedBy ?? new UserManager(Guid.Empty);
            this.number = number;
            this.description = description;
            this.status = status;
            this.createdAt = createdAt;
            this.authorizedAt = authorizedAt;
        }
        #endregion

        #region methods
        public static List<MovementRequestHeaderManager> GetAll()
        {
            List<MovementRequestHeader> headersData = new DataAccess.Repositories.MovementRequestHeaderRepository().GetAll();
            List<MovementRequestHeaderManager> headers = new List<MovementRequestHeaderManager>();

            foreach (MovementRequestHeader data in headersData)
            {
                UserManager requester = new UserManager(
                    data.iRequestedBy,
                    data.CreatorNumber ?? 0,
                    data.CreatorFullName ?? "",
                    data.CreatorEmail ?? "",
                    "",
                    new UserRoleManager(Guid.Empty)
                );

                UserManager authorizer = new UserManager(
                    data.iAuthorizedBy ?? Guid.Empty,
                    data.AuthorizerNumber ?? 0,
                    data.AuthorizerFullName ?? "",
                    data.AuthorizerEmail ?? "",
                    "",
                    new UserRoleManager(Guid.Empty)
                );

                headers.Add(new MovementRequestHeaderManager(
                    data.iMovementRequestHeader,
                    requester,
                    authorizer,
                    data.MovementRequestHeaderNumber ?? 0,
                    data.MovementRequestHeaderDescription ?? "",
                    (RequestHeaderStatus)data.MovementRequestHeaderStatus,
                    data.MovementRequestHeaderCreatedAt,
                    data.MovementRequestHeaderAuthorizedAt ?? DateTime.MinValue
                ));
            }

            return headers;
        }

        public bool Find()
        {
            MovementRequestHeader header = new MovementRequestHeader()
            {
                iMovementRequestHeader = this.id,
                iRequestedBy = this.requestedBy.Id,
                MovementRequestHeaderStatus = 0,
                MovementRequestHeaderCreatedAt = this.createdAt
            };

            bool found = new DataAccess.Repositories.MovementRequestHeaderRepository().Find(header);

            if (found)
            {
                UserManager requester = new UserManager(
                    header.iRequestedBy,
                    header.CreatorNumber ?? 0,
                    header.CreatorFullName ?? "",
                    header.CreatorEmail ?? "",
                    "",
                    new UserRoleManager(Guid.Empty)
                );

                UserManager authorizer = new UserManager(
                    header.iAuthorizedBy ?? Guid.Empty,
                    header.AuthorizerNumber ?? 0,
                    header.AuthorizerFullName ?? "",
                    header.AuthorizerEmail ?? "",
                    "",
                    new UserRoleManager(Guid.Empty)
                );

                this.requestedBy = requester;
                this.authorizedBy = authorizer;
                this.number = header.MovementRequestHeaderNumber ?? 0;
                this.description = header.MovementRequestHeaderDescription ?? "";
                this.status = (RequestHeaderStatus)header.MovementRequestHeaderStatus;
                this.createdAt = header.MovementRequestHeaderCreatedAt;
                this.authorizedAt = header.MovementRequestHeaderAuthorizedAt ?? DateTime.MinValue;
            }

            return found;
        }

        public bool Insert()
        {
            MovementRequestHeader header = new MovementRequestHeader()
            {
                iMovementRequestHeader = this.id,
                iRequestedBy = this.requestedBy.Id,
                CreatorNumber = this.requestedBy.Number,
                CreatorFullName = this.requestedBy.FullName,
                CreatorEmail = this.requestedBy.Email,
                iAuthorizedBy = this.authorizedBy.Id != Guid.Empty ? this.authorizedBy.Id : null,
                AuthorizerNumber = this.authorizedBy.Number,
                AuthorizerFullName = this.authorizedBy.FullName,
                AuthorizerEmail = this.authorizedBy.Email,
                MovementRequestHeaderNumber = this.number,
                MovementRequestHeaderDescription = this.description,
                MovementRequestHeaderStatus = (int)this.status,
                MovementRequestHeaderCreatedAt = this.createdAt,
                MovementRequestHeaderAuthorizedAt = this.authorizedAt != DateTime.MinValue ? this.authorizedAt : null
            };

            return new DataAccess.Repositories.MovementRequestHeaderRepository().Insert(header);
        }

        public bool Update()
        {
            MovementRequestHeader header = new MovementRequestHeader()
            {
                iMovementRequestHeader = this.id,
                iRequestedBy = this.requestedBy.Id,
                CreatorNumber = this.requestedBy.Number,
                CreatorFullName = this.requestedBy.FullName,
                CreatorEmail = this.requestedBy.Email,
                iAuthorizedBy = this.authorizedBy.Id != Guid.Empty ? this.authorizedBy.Id : null,
                AuthorizerNumber = this.authorizedBy.Number,
                AuthorizerFullName = this.authorizedBy.FullName,
                AuthorizerEmail = this.authorizedBy.Email,
                MovementRequestHeaderNumber = this.number,
                MovementRequestHeaderDescription = this.description,
                MovementRequestHeaderStatus = (int)this.status,
                MovementRequestHeaderCreatedAt = this.createdAt,
                MovementRequestHeaderAuthorizedAt = this.authorizedAt != DateTime.MinValue ? this.authorizedAt : null
            };

            return new DataAccess.Repositories.MovementRequestHeaderRepository().Update(header);
        }

        public bool Delete()
        {
            return new DataAccess.Repositories.MovementRequestHeaderRepository().Delete(this.id);
        }
        #endregion
    }
}
