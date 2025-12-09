using DataAccess.Models;
using System.Runtime.CompilerServices;

namespace ApplicationLogic.Managers
{
    public class HabitacionManager
    {
        #region attributes
        private int id;
        private int numero;
        private string tipo;
        private decimal precio;
        private int capacidad;
        private string descripcion;
        private Boolean disponible;
        #endregion

        #region getters
        public int Id { get { return id; } }
        public int Numero { get { return numero; } }
        public string Tipo { get { return tipo; } }
        public decimal Precio { get { return precio; } }
        public int Capacidad { get { return capacidad; } }
        public string Descripcion { get { return descripcion; } }
        public Boolean Disponible { get { return disponible; } }
        #endregion

        #region constructors
        public HabitacionManager(int id)
        {
            this.id = id;
            this.numero = 0;
            this.tipo = "";
            this.precio = 0;
            this.capacidad = 0;
            this.descripcion = "";
            this.disponible = false;
        }

        public HabitacionManager(int id, int numero, string tipo, decimal precio, int capacidad, string descripcion, Boolean disponible)
        {
            this.id = id;
            this.numero = numero;
            this.tipo = tipo;
            this.precio = precio;
            this.capacidad = capacidad;
            this.descripcion = descripcion;
            this.disponible = disponible;
        }
        #endregion

        #region methods
        public static List<HabitacionManager> GetAll()
        {
            List<Habitacion> habitacionData = new DataAccess.Repositories.HabitacionRepository().GetAll();
            List<HabitacionManager> habitacion = new List<HabitacionManager>() { };
            foreach (Habitacion data in habitacionData)
            {
                habitacion.Add(new HabitacionManager(
                    data.Id,
                    data.Numero,
                    data.Tipo,
                    data.Precio,
                    data.Capacidad,
                    data.Descripcion,
                    data.Disponible
                ));
            }
            return habitacion;
        }

        //=====================================================================================================================

        public bool Find()
        {
            Habitacion habitacion = new Habitacion()
            {
                Id = this.id,
                Numero = this.numero,
                Tipo = this.tipo,
                Precio = this.precio,
                Capacidad = this.capacidad,
                Descripcion = this.descripcion,
                Disponible = this.disponible
            };
            bool found = new DataAccess.Repositories.HabitacionRepository().Find(habitacion);
            if (found)
            {
                this.id = habitacion.Id;
                this.numero = habitacion.Numero;
                this.tipo = habitacion.Tipo;
                this.precio = habitacion.Precio;
                this.capacidad = habitacion.Capacidad;
                this.descripcion = habitacion.Descripcion;
                this.disponible = habitacion.Disponible;
            }
            return found;
        }

        public bool FindID()
        {
            bool found = new DataAccess.Repositories.HabitacionRepository().FindID(this.id);
            return found;
        }

        public bool FindNumero()
        {
            bool found = new DataAccess.Repositories.HabitacionRepository().FindNumero(this.numero);
            return found;
        }

        public bool FindTipo()
        {
            bool found = new DataAccess.Repositories.HabitacionRepository().FindTipo(this.tipo);
            return found;
        }

        public bool FindPrecio()
        {
            bool found = new DataAccess.Repositories.HabitacionRepository().FindPrecio(this.precio);
            return found;
        }

        public bool FindCapacidad()
        {
            bool found = new DataAccess.Repositories.HabitacionRepository().FindCapacidad(this.capacidad);
            return found;
        }

        public bool FindDescripcion()
        {
            bool found = new DataAccess.Repositories.HabitacionRepository().FindDescripcion(this.descripcion);
            return found;
        }

        public bool FindDisponible()
        {
            bool found = new DataAccess.Repositories.HabitacionRepository().FindDisponible(this.disponible);
            return found;
        }

        public bool HabitacionAvailable()
        {
            return new DataAccess.Repositories.HabitacionRepository().FindHabitacionAvailable(this.id);
        }

        public (int numero, string tipo, decimal precio, int capacidad, string descripcion) FindValues()
        {
            return new DataAccess.Repositories.HabitacionRepository().FindValues(this.id);
        }

        //=====================================================================================================================

        public bool Insert()
        {
            Habitacion habitacion = new Habitacion()
            {
                Id = this.id,
                Numero = this.numero,
                Tipo = this.tipo,
                Precio = this.precio,
                Capacidad = this.capacidad,
                Descripcion = this.descripcion,
                Disponible = this.disponible
            };
            return new DataAccess.Repositories.HabitacionRepository().Insert(habitacion);
        }

        public bool Update()
        {
            Habitacion habitacion = new Habitacion()
            {
                Id = this.id,
                Numero = this.numero,
                Tipo = this.tipo,
                Precio = this.precio,
                Capacidad = this.capacidad,
                Descripcion = this.descripcion,
                Disponible = this.disponible
            };
            return new DataAccess.Repositories.HabitacionRepository().Update(habitacion);
        }

        public bool Delete()
        {
            return new DataAccess.Repositories.HabitacionRepository().Delete(this.id);
        }

        public decimal HabitacionPrice()
        {
            return new DataAccess.Repositories.HabitacionRepository().FindHabitacionPrice(this.id);
        }
        #endregion
    }
}
