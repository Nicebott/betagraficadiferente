using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GestionAcademica.Models;

namespace GestionAcademica.Data
{
    public class GestorDatos
    {
        private readonly string rutaArchivo = "registros_academicos.txt";

        public void GuardarAlumno(Alumno alumno)
        {
            string registro = $"{alumno.CodigoAlumno},{alumno.NombreCompleto},{alumno.Carrera}," +
                            $"{string.Join(";", alumno.NotasParciales)},{string.Join(";", alumno.NotasLaboratorio)}," +
                            $"{alumno.PromedioFinal},{alumno.Condicion}";
            File.AppendAllText(rutaArchivo, registro + Environment.NewLine);
        }

        public List<Alumno> CargarAlumnos()
        {
            List<Alumno> alumnos = new List<Alumno>();

            if (!File.Exists(rutaArchivo)) return alumnos;

            foreach (var linea in File.ReadAllLines(rutaArchivo))
            {
                var datos = linea.Split(',');
                if (datos.Length < 5) continue;

                var alumno = new Alumno
                {
                    CodigoAlumno = datos[0],
                    NombreCompleto = datos[1],
                    Carrera = datos[2],
                    NotasParciales = datos[3].Split(';').Select(double.Parse).ToArray(),
                    NotasLaboratorio = datos[4].Split(';').Select(double.Parse).ToArray()
                };

                alumno.CalcularPromedio();
                alumnos.Add(alumno);
            }

            return alumnos;
        }

        public void EliminarAlumno(string codigoAlumno)
        {
            var alumnos = CargarAlumnos().Where(a => a.CodigoAlumno != codigoAlumno).ToList();
            File.WriteAllText(rutaArchivo, string.Empty);
            alumnos.ForEach(GuardarAlumno);
        }
    }
}