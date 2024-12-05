using System;
using System.Linq;

namespace GestionAcademica.Models
{
    public class Alumno
    {
        public string CodigoAlumno { get; set; }
        public string NombreCompleto { get; set; }
        public string Carrera { get; set; }
        public double[] NotasParciales { get; set; }
        public double[] NotasLaboratorio { get; set; }
        public double PromedioFinal { get; private set; }
        public string Condicion { get; private set; }

        public void CalcularPromedio()
        {
            double promedioParciales = NotasParciales?.Average() ?? 0;
            double promedioLaboratorio = NotasLaboratorio?.Average() ?? 0;

            PromedioFinal = (promedioParciales * 0.7) + (promedioLaboratorio * 0.3);
            ActualizarCondicion();
        }

        private void ActualizarCondicion()
        {
            Condicion = PromedioFinal >= 65 ? "Aprobado" : "Reprobado";
        }
    }
}