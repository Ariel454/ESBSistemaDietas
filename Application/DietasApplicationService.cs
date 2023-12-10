using SistemaDietas.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDietas.Application
{
    public class DietasApplicationService
    {
        private readonly DietasService _dietasService;

        public DietasApplicationService(DietasService dietasService)
        {
            _dietasService = dietasService;
        }

        public void ProcesarRecetaEnDietas(RecetaMedica receta)
        {
            _dietasService.ProcesarRecetaEnDietas(receta);
        }
    }
}
