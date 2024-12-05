using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using GestionAcademica.Models;
using GestionAcademica.Data;

namespace GestionAcademica.Forms
{
    public partial class FormularioPrincipal : Form
    {
        private readonly GestorDatos gestor;

        public FormularioPrincipal()
        {
            InitializeComponent();
            gestor = new GestorDatos();
            ConfigurarTablaAlumnos();
            CargarDatosAlumnos();
        }

        private void ConfigurarTablaAlumnos()
        {
            dgvAlumnos.AutoGenerateColumns = false;
            dgvAlumnos.Columns.Clear();

            dgvAlumnos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CodigoAlumno",
                HeaderText = "Código",
                DataPropertyName = "CodigoAlumno",
                Width = 100
            });

            dgvAlumnos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "NombreCompleto",
                HeaderText = "Nombre Completo",
                DataPropertyName = "NombreCompleto",
                Width = 200
            });

            dgvAlumnos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Carrera",
                HeaderText = "Carrera",
                DataPropertyName = "Carrera",
                Width = 150
            });

            dgvAlumnos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "PromedioFinal",
                HeaderText = "Promedio",
                DataPropertyName = "PromedioFinal",
                Width = 100
            });

            dgvAlumnos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Condicion",
                HeaderText = "Condición",
                DataPropertyName = "Condicion",
                Width = 100
            });
        }

        private void CargarDatosAlumnos()
        {
            try
            {
                var alumnos = gestor.CargarAlumnos();
                dgvAlumnos.DataSource = null;
                dgvAlumnos.DataSource = alumnos;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (ValidarDatos())
            {
                try
                {
                    var alumno = new Alumno
                    {
                        CodigoAlumno = txtCodigo.Text,
                        NombreCompleto = txtNombre.Text,
                        Carrera = txtCarrera.Text,
                        NotasParciales = new[] { 
                            double.Parse(txtParcial1.Text),
                            double.Parse(txtParcial2.Text)
                        },
                        NotasLaboratorio = new[] {
                            double.Parse(txtLab1.Text),
                            double.Parse(txtLab2.Text)
                        }
                    };

                    alumno.CalcularPromedio();
                    gestor.GuardarAlumno(alumno);
                    CargarDatosAlumnos();
                    LimpiarCampos();

                    MessageBox.Show("Alumno registrado exitosamente", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al guardar: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private bool ValidarDatos()
        {
            if (string.IsNullOrWhiteSpace(txtCodigo.Text) ||
                string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtCarrera.Text))
            {
                MessageBox.Show("Por favor complete todos los campos requeridos",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void LimpiarCampos()
        {
            txtCodigo.Clear();
            txtNombre.Clear();
            txtCarrera.Clear();
            txtParcial1.Clear();
            txtParcial2.Clear();
            txtLab1.Clear();
            txtLab2.Clear();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvAlumnos.SelectedRows.Count > 0)
            {
                var codigo = dgvAlumnos.SelectedRows[0].Cells["CodigoAlumno"].Value.ToString();
                if (MessageBox.Show("¿Está seguro de eliminar este registro?", "Confirmar",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    gestor.EliminarAlumno(codigo);
                    CargarDatosAlumnos();
                }
            }
        }

        private void dgvAlumnos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dgvAlumnos.Rows[e.RowIndex];
                txtCodigo.Text = row.Cells["CodigoAlumno"].Value?.ToString();
                txtNombre.Text = row.Cells["NombreCompleto"].Value?.ToString();
                txtCarrera.Text = row.Cells["Carrera"].Value?.ToString();
            }
        }
    }
}