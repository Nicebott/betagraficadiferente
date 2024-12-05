namespace GestionAcademica.Forms
{
    partial class FormularioPrincipal
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panelSuperior = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.groupBoxDatos = new System.Windows.Forms.GroupBox();
            this.txtCarrera = new System.Windows.Forms.TextBox();
            this.lblCarrera = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.lblNombre = new System.Windows.Forms.Label();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.lblCodigo = new System.Windows.Forms.Label();
            this.txtParcial1 = new System.Windows.Forms.TextBox();
            this.lblParcial1 = new System.Windows.Forms.Label();
            this.txtParcial2 = new System.Windows.Forms.TextBox();
            this.lblParcial2 = new System.Windows.Forms.Label();
            this.txtLab1 = new System.Windows.Forms.TextBox();
            this.lblLab1 = new System.Windows.Forms.Label();
            this.txtLab2 = new System.Windows.Forms.TextBox();
            this.lblLab2 = new System.Windows.Forms.Label();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.dgvAlumnos = new System.Windows.Forms.DataGridView();

            // Panel Superior
            this.panelSuperior.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.panelSuperior.Controls.Add(this.lblTitulo);
            this.panelSuperior.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSuperior.Height = 60;

            // Título
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Text = "Sistema de Gestión Académica";
            this.lblTitulo.Location = new System.Drawing.Point(20, 15);

            // GroupBox Datos
            this.groupBoxDatos.Text = "Datos del Alumno";
            this.groupBoxDatos.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.groupBoxDatos.Location = new System.Drawing.Point(20, 80);
            this.groupBoxDatos.Size = new System.Drawing.Size(960, 150);

            // Controles dentro del GroupBox
            int y = 30;
            ConfigurarControl(lblCodigo, txtCodigo, "Código:", 20, ref y);
            ConfigurarControl(lblNombre, txtNombre, "Nombre:", 300, ref y);
            ConfigurarControl(lblCarrera, txtCarrera, "Carrera:", 580, ref y);

            y = 80;
            ConfigurarControl(lblParcial1, txtParcial1, "Parcial 1:", 20, ref y);
            ConfigurarControl(lblParcial2, txtParcial2, "Parcial 2:", 200, ref y);
            ConfigurarControl(lblLab1, txtLab1, "Lab 1:", 380, ref y);
            ConfigurarControl(lblLab2, txtLab2, "Lab 2:", 560, ref y);

            // Botones
            ConfigurarBoton(btnGuardar, "Guardar", 740, 100);
            ConfigurarBoton(btnEliminar, "Eliminar", 860, 100);

            // DataGridView
            this.dgvAlumnos.Location = new System.Drawing.Point(20, 250);
            this.dgvAlumnos.Size = new System.Drawing.Size(960, 300);
            this.dgvAlumnos.BackgroundColor = System.Drawing.Color.White;
            this.dgvAlumnos.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvAlumnos.AllowUserToAddRows = false;
            this.dgvAlumnos.AllowUserToDeleteRows = false;
            this.dgvAlumnos.ReadOnly = true;
            this.dgvAlumnos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAlumnos.RowHeadersVisible = false;
            this.dgvAlumnos.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAlumnos_CellClick);

            // Formulario Principal
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.panelSuperior,
                this.groupBoxDatos,
                this.dgvAlumnos
            });
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sistema de Gestión Académica";
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
        }

        private void ConfigurarControl(Label lbl, TextBox txt, string texto, int x, ref int y)
        {
            lbl.Text = texto;
            lbl.Location = new System.Drawing.Point(x, y);
            lbl.AutoSize = true;

            txt.Location = new System.Drawing.Point(x + 80, y - 3);
            txt.Size = new System.Drawing.Size(150, 25);
            txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            this.groupBoxDatos.Controls.Add(lbl);
            this.groupBoxDatos.Controls.Add(txt);
        }

        private void ConfigurarBoton(Button btn, string texto, int x, int y)
        {
            btn.Text = texto;
            btn.Location = new System.Drawing.Point(x, y);
            btn.Size = new System.Drawing.Size(100, 35);
            btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            btn.ForeColor = System.Drawing.Color.White;
            btn.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            if (texto == "Guardar")
                btn.Click += new System.EventHandler(this.btnGuardar_Click);
            else
                btn.Click += new System.EventHandler(this.btnEliminar_Click);

            this.groupBoxDatos.Controls.Add(btn);
        }

        private System.Windows.Forms.Panel panelSuperior;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.GroupBox groupBoxDatos;
        private System.Windows.Forms.TextBox txtCarrera;
        private System.Windows.Forms.Label lblCarrera;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.Label lblCodigo;
        private System.Windows.Forms.TextBox txtParcial1;
        private System.Windows.Forms.Label lblParcial1;
        private System.Windows.Forms.TextBox txtParcial2;
        private System.Windows.Forms.Label lblParcial2;
        private System.Windows.Forms.TextBox txtLab1;
        private System.Windows.Forms.Label lblLab1;
        private System.Windows.Forms.TextBox txtLab2;
        private System.Windows.Forms.Label lblLab2;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.DataGridView dgvAlumnos;
    }
}