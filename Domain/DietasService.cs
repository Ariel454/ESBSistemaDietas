using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDietas.Domain
{
    public class DietasService
    {
        public void ProcesarRecetaEnDietas(RecetaMedica receta)
        {


            Console.WriteLine($"Procesando receta en Dietas para el paciente: {receta.Paciente}");
        }
    }
}
