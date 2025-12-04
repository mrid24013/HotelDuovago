using DataAccess.Models;

namespace ApplicationLogic.Managers
{
    public class UsuarioManager
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
        public UsuarioManager(int id)
        {
            this.id = id;
            this.nombre = "";
            this.telefono = "";
            this.email = "";
            this.direccion = "";
            this.fechaRegistro = default(DateTime);
        }

        public UsuarioManager(int id, string nombre, string telefono, string email, string direccion, DateTime fechaRegistro)
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
        public static List<UsuarioManager> GetAll()
        {
            List<Usuario> clienteData = new DataAccess.Repositories.UsuarioRepository().GetAll();
            List<UsuarioManager> cliente = new List<UsuarioManager>() { };
            foreach (Usuario data in clienteData)
            {
                cliente.Add(new UsuarioManager(
                    data.Id,
                    data.Nombre,
                    data.Telefono,
                    data.Email,
                    data.Direccion,
                    data.FechaRegistro
                ));
            }
            return cliente;
        }

        //=====================================================================================================================

        public bool Find()
        {
            Usuario cliente = new Usuario()
            {
                Id = this.id,
                Nombre = this.nombre,
                Telefono = this.telefono,
                Email = this.email,
                Direccion = this.direccion,
                FechaRegistro = this.fechaRegistro
            };
            bool found = new DataAccess.Repositories.UsuarioRepository().Find(cliente);
            if (found)
            {
                this.nombre = cliente.Nombre;
                this.telefono = cliente.Telefono;
                this.email = cliente.Email;
                this.direccion = cliente.Direccion;
                this.fechaRegistro = cliente.FechaRegistro;
            }
            return found;
        }

        public bool FindID()
        {
            bool found = new DataAccess.Repositories.UsuarioRepository().FindID(this.id);
            return found;
        }

        public bool FindNombre()
        {
            bool found = new DataAccess.Repositories.UsuarioRepository().FindNombre(this.nombre);
            return found;
        }

        public bool FindTelefono()
        {
            bool found = new DataAccess.Repositories.UsuarioRepository().FindTelefono(this.telefono);
            return found;
        }

        public bool FindEmail()
        {
            bool found = new DataAccess.Repositories.UsuarioRepository().FindEmail(this.email);
            return found;
        }

        public bool FindDireccion()
        {
            bool found = new DataAccess.Repositories.UsuarioRepository().FindDireccion(this.direccion);
            return found;
        }

        public bool FindFechaRegistro()
        {
            bool found = new DataAccess.Repositories.UsuarioRepository().FindFechaRegistro(this.fechaRegistro);
            return found;
        }

        //=====================================================================================================================

        public bool Insert()
        {
            Usuario usuario = new Usuario()
            {
                Id = this.id,
                Nombre = this.nombre,
                Telefono = this.telefono,
                Email = this.email,
                Direccion = this.direccion,
                FechaRegistro = this.fechaRegistro
            };
            return new DataAccess.Repositories.UsuarioRepository().Insert(usuario);
        }

        public bool Update()
        {
            Usuario usuario = new Usuario()
            {
                Id = this.id,
                Nombre = this.nombre,
                Telefono = this.telefono,
                Email = this.email,
                Direccion = this.direccion,
                FechaRegistro = this.fechaRegistro
            };
            return new DataAccess.Repositories.UsuarioRepository().Update(usuario);
        }

        public bool Delete()
        {
            return new DataAccess.Repositories.UsuarioRepository().Delete(this.id);
        }
        #endregion
    }
}
