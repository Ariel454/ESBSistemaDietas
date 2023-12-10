using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDietas.Domain
{
    public class RecetaMedica
    {
        public string Paciente { get; set; }
        public string Medicamento { get; set; }
        public string Dosis { get; set; }
        public string FechaEmision { get; set; }
    }
}
