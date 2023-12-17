using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppMovilProyecto.Models
{
    public class Cita
    {
        public int IdCita { get; set; }

        public string Fecha { get; set; }

        public string Hora { get; set; }

        public string Descripcion { get; set; }

        // ID del Medico
        public string IdMedico { get; set; }

        // ID del Usuario
        public string IdUsuario { get; set; }
    }
}
