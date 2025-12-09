using DataAccess.Models;

namespace ApplicationLogic.Managers
{
    public class ReservacionManager
    {
        #region attributes
        private int id;
        private int clienteId;
        private int habitacionId;
        private DateTime fechaEntrada;
        private DateTime fechaSalida;
        private int diasEstancia;
        private decimal montoTotal;
        private string estado;
        #endregion

        #region getters
        public int Id { get { return id; } }
        public int ClienteId { get { return clienteId; } }
        public int HabitacionId { get { return habitacionId; } }
        public DateTime FechaEntrada { get { return fechaEntrada; } }
        public DateTime FechaSalida { get { return fechaSalida; } }
        public int DiasEstancia { get { return diasEstancia; } }
        public decimal MontoTotal { get { return montoTotal; } }
        public string Estado { get { return estado; } }
        #endregion

        #region constructors
        public ReservacionManager(int id)
        {
            this.id = id;
            this.clienteId = 0;
            this.habitacionId = 0;
            this.fechaEntrada = default(DateTime);
            this.fechaSalida = default(DateTime);
            this.diasEstancia = 0;
            this.montoTotal = 0;
            this.estado = "";
        }

        public ReservacionManager(int id, int clienteId, int habitacionId, DateTime fechaEntrada, DateTime fechaSalida, int diasEstancia, decimal montoTotal, string estado)
        {
            this.id = id;
            this.clienteId = clienteId;
            this.habitacionId = habitacionId;
            this.fechaEntrada = fechaEntrada;
            this.fechaSalida = fechaSalida;
            this.diasEstancia = diasEstancia;
            this.montoTotal = montoTotal;
            this.estado = estado;
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
                    data.ClienteId,
                    data.HabitacionId,
                    data.FechaEntrada,
                    data.FechaSalida,
                    data.DiasEstancia,
                    data.MontoTotal,
                    data.Estado
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
                ClienteId = this.clienteId,
                HabitacionId = this.habitacionId,
                FechaEntrada = this.fechaEntrada,
                FechaSalida = this.fechaSalida,
                DiasEstancia = this.diasEstancia,
                MontoTotal = this.montoTotal,
                Estado = this.estado
            };
            bool found = new DataAccess.Repositories.ReservacionRepository().Find(reservacion);
            if (found)
            {
                this.id = reservacion.Id;
                this.clienteId = reservacion.ClienteId;
                this.habitacionId = reservacion.HabitacionId;
                this.fechaEntrada = reservacion.FechaEntrada;
                this.fechaSalida = reservacion.FechaSalida;
                this.diasEstancia = reservacion.DiasEstancia;
                this.montoTotal = reservacion.MontoTotal;
                this.estado = reservacion.Estado;
            }
            return found;
        }

        public bool FindID()
        {
            bool found = new DataAccess.Repositories.ReservacionRepository().FindID(this.id);
            return found;
        }
        public bool FindClienteID()
        {
            bool found = new DataAccess.Repositories.ReservacionRepository().FindClienteID(this.clienteId);
            return found;
        }
        public bool FindHabitacionID()
        {
            bool found = new DataAccess.Repositories.ReservacionRepository().FindHabitacionID(this.habitacionId);
            return found;
        }

        public bool FindFechaEntrada()
        {
            bool found = new DataAccess.Repositories.ReservacionRepository().FindFechaEntrada(this.fechaEntrada);
            return found;
        }

        public bool FindFechaSalida()
        {
            bool found = new DataAccess.Repositories.ReservacionRepository().FindFechaSalida(this.fechaSalida);
            return found;
        }

        public bool FindDiasEstancia()
        {
            bool found = new DataAccess.Repositories.ReservacionRepository().FindDiasEstancia(this.diasEstancia);
            return found;
        }

        public bool FindMontoTotal()
        {
            bool found = new DataAccess.Repositories.ReservacionRepository().FindMontoTotal(this.montoTotal);
            return found;
        }

        public bool FindEstado()
        {
            bool found = new DataAccess.Repositories.ReservacionRepository().FindEstado(this.estado);
            return found;
        }

        //=====================================================================================================================

        public bool Insert()
        {
            Reservacion reservacion = new Reservacion()
            {
                Id = this.id,
                ClienteId = this.clienteId,
                HabitacionId = this.habitacionId,
                FechaEntrada = this.fechaEntrada,
                FechaSalida = this.fechaSalida,
                DiasEstancia = this.diasEstancia,
                MontoTotal = this.montoTotal,
                Estado = this.estado
            };
            return new DataAccess.Repositories.ReservacionRepository().Insert(reservacion);
        }

        public bool Update()
        {
            Reservacion reservacion = new Reservacion()
            {
                Id = this.id,
                ClienteId = this.clienteId,
                HabitacionId = this.habitacionId,
                FechaEntrada = this.fechaEntrada,
                FechaSalida = this.fechaSalida,
                DiasEstancia = this.diasEstancia,
                MontoTotal = this.montoTotal,
                Estado = this.estado
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