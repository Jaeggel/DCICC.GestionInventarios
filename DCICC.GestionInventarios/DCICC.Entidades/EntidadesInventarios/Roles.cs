namespace DCICC.Entidades.EntidadesInventarios
{
    public class Roles
    {
        public int IdRol { get; set; }

        public string NombreRol { get; set; }

        public string NombreRolAntiguo { get; set; }

        public string DescripcionRol { get; set; }

        public bool HabilitadoRol { get; set; }

        public bool PermisoActivos { get; set; }

        public bool PermisoMaqVirtuales { get; set; }

        public bool PermisoTickets { get; set; }

        public bool PermisoReportes { get; set; }

        public bool PermisoAdministracion { get; set; }
    }
}
