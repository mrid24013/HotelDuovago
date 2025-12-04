using DataAccess.Models;

namespace ApplicationLogic.Managers
{
    public class ReservacionManager
    {
        #region attributes
        private int id;
        private string nombre;
        private string telefono;
        private string email;
        private string direccion;
        private DateTime fechaRegistro;
        #endregion

        #region getters
        public int Id { get { return id; } }
        public string Nombre { get { return nombre; } }
        public string Telefono { get { return telefono; } }
        public string Email { get { return email; } }
        public string Direccion { get { return direccion; } }
        public DateTime FechaRegistro { get { return fechaRegistro; } }
        #endregion

        #region constructors
        public ReservacionManager(int id)
        {
            this.id = id;
            this.nombre = "";
            this.telefono = "";
            this.email = "";
            this.direccion = "";
            this.fechaRegistro = default(DateTime);
        }

        public ReservacionManager(int id, string nombre, string telefono, string email, string direccion, DateTime fechaRegistro)
        {
            this.id = id;
            this.nombre = nombre;
            this.telefono = telefono;
            this.email = email;
            this.direccion = direccion;
            this.fechaRegistro = fechaRegistro;
        }
        #endregion

        #region methods
        public static List<ReservacionManager> GetAll()
        {
            List<Reservacion> reservacionData = new DataAccess.Repositories.ReservacionRepository().GetAll();
            List<ReservacionManager> reservacion = new List<ReservacionManager>() { };
            foreach (Reservacion data in reservacionData)
            {
                reservacion.Add(new ReservacionManager(
                    data.Id,
                    data.Nombre,
                    data.Telefono,
                    data.Email,
                    data.Direccion,
                    data.FechaRegistro
                ));
            }
            return reservacion;
        }

        //=====================================================================================================================

        public bool Find()
        {
            Reservacion reservacion = new Reservacion()
            {
                Id = this.id,
                Nombre = this.nombre,
                Telefono = this.telefono,
                Email = this.email,
                Direccion = this.direccion,
                FechaRegistro = this.fechaRegistro
            };
            bool found = new DataAccess.Repositories.ReservacionRepository().Find(reservacion);
            if (found)
            {
                this.nombre = reservacion.Nombre;
                this.telefono = reservacion.Telefono;
                this.email = reservacion.Email;
                this.direccion = reservacion.Direccion;
                this.fechaRegistro = reservacion.FechaRegistro;
            }
            return found;
        }

        public bool FindID()
        {
            bool found = new DataAccess.Repositories.ReservacionRepository().FindID(this.id);
            return found;
        }

        public bool FindNombre()
        {
            bool found = new DataAccess.Repositories.ReservacionRepository().FindNombre(this.nombre);
            return found;
        }

        public bool FindTelefono()
        {
            bool found = new DataAccess.Repositories.ReservacionRepository().FindTelefono(this.telefono);
            return found;
        }

        public bool FindEmail()
        {
            bool found = new DataAccess.Repositories.ReservacionRepository().FindEmail(this.email);
            return found;
        }

        public bool FindDireccion()
        {
            bool found = new DataAccess.Repositories.ReservacionRepository().FindDireccion(this.direccion);
            return found;
        }

        public bool FindFechaRegistro()
        {
            bool found = new DataAccess.Repositories.ReservacionRepository().FindFechaRegistro(this.fechaRegistro);
            return found;
        }

        //=====================================================================================================================

        public bool Insert()
        {
            Reservacion reservacion = new Reservacion()
            {
                Id = this.id,
                Nombre = this.nombre,
                Telefono = this.telefono,
                Email = this.email,
                Direccion = this.direccion,
                FechaRegistro = this.fechaRegistro
            };
            return new DataAccess.Repositories.ReservacionRepository().Insert(reservacion);
        }

        public bool Update()
        {
            Reservacion reservacion = new Reservacion()
            {
                Id = this.id,
                Nombre = this.nombre,
                Telefono = this.telefono,
                Email = this.email,
                Direccion = this.direccion,
                FechaRegistro = this.fechaRegistro
            };
            return new DataAccess.Repositories.ReservacionRepository().Update(reservacion);
        }

        public bool Delete()
        {
            return new DataAccess.Repositories.ReservacionRepository().Delete(this.id);
        }
        #endregion
    }
}
