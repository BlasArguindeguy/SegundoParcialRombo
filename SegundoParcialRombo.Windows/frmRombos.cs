using SegundoParcialRombo.Datos;
using SegundoParcialRombo.Entidades;

namespace SegundoParcialRombo.Windows
{
    public partial class frmRombos : Form
    {
        private RepositorioDeRombos repo;
        private List<Rombo> lista;
        int valorFiltro;
        bool filterOn = false;
        public frmRombos()
        {
            InitializeComponent();
            repo = new RepositorioDeRombos();
            ActualizarCantidadDeRegistros();
            txtCantidad.Text = repo.GetCantidad().ToString();
        }
        public int ContarRombosMostrados()
        {
            return dgvDatos.Rows.Count;
        }

        private void ActualizarCantidadDeRegistros()
        {
            if (valorFiltro > 0)
            {
                txtCantidad.Text = repo.GetCantidad(valorFiltro).ToString();
            }
            else
            {
                txtCantidad.Text = repo.GetCantidad().ToString();
            }
        }
        private void ActualizarContador()
        {
            txtCantidad.Text = ContarRombosMostrados().ToString();
        }



        private void tsbEditar_Click(object sender, EventArgs e)
        {
        }


        private void CargarComboContornos(ref ToolStripComboBox tsCboBordes)
        {
            var listaBordes = Enum.GetValues(typeof(Contorno));
            foreach (var item in listaBordes)
            {
                tsCboBordes.Items.Add(item);
            }
            tsCboBordes.DropDownStyle = ComboBoxStyle.DropDownList;
            tsCboBordes.SelectedIndex = 0;

        }


        private void lado09ToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void lado90ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void tsbActualizar_Click(object sender, EventArgs e)
        {
        }

        private void tsbSalir_Click(object sender, EventArgs e)
        {
        }

        private void frmElipses_Load(object sender, EventArgs e)
        {
            CargarComboContornos(ref tsCboContornos);

        }

        private void tsbNuevo_Click_1(object sender, EventArgs e)
        {
            frmRomboAE Form = new frmRomboAE() { Text = "Agregar Rombo" };
            DialogResult dr = Form.ShowDialog(this);
            if (dr == DialogResult.Cancel) { return; }
            Rombo rombo = Form.GetRombo();
            if (!repo.Existe(rombo))
            {
                repo.Agregar(rombo);
                ActualizarCantidadDeRegistros();
                DataGridViewRow l = ConstruirFila();
                SetearFila(l, rombo);
                AgregarFila(l);

                MessageBox.Show("Fila añadida con éxito", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Registro existente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void AgregarFila(DataGridViewRow l)
        {
            dgvDatos.Rows.Add(l);
        }
        private void SetearFila(DataGridViewRow l, Rombo rombo)
        {
            l.Cells[colMayor.Index].Value = rombo.GetMayor();
            l.Cells[colMenor.Index].Value = rombo.GetMenor();
            l.Cells[colBorde.Index].Value = rombo.Contorno;
            l.Cells[colLado.Index].Value = rombo.GetLado().ToString(".000");
            l.Cells[colPerimetro.Index].Value = rombo.GetPerimetro().ToString(".000");
            l.Cells[colArea.Index].Value = rombo.GetArea().ToString(".000");
            l.Tag = rombo;
            l.Cells[colBorde.Index].Value = rombo.Contorno.ToString();
        }
        private DataGridViewRow ConstruirFila()
        {
            var l = new DataGridViewRow();
            l.CreateCells(dgvDatos);
            return l;
        }
        private void FormPrincipal_Load(object sender, EventArgs e)
        {

        }
        private void RecargarGrilla()
        {
            valorFiltro = 0;
            filterOn = false;
            tsbFiltrar.BackColor = SystemColors.Control;
            lista = repo.GetLista();
            MostrarDatosEnGrilla();
            ActualizarCantidadDeRegistros();
        }
        private void MostrarDatosEnGrilla()
        {
            dgvDatos.Rows.Clear();
            foreach (var hex in lista)
            {
                DataGridViewRow l = ConstruirFila();
                SetearFila(l, hex);
                AgregarFila(l);
            }
        }
        private void tsbBorrar_Click(object sender, EventArgs e)
        {
            if (dgvDatos.SelectedRows.Count == 0)
            {
                return;
            }
            DialogResult dr = MessageBox.Show("¿Eliminar la fila seleccionada?", "Confirmar", MessageBoxButtons.YesNo,
               MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (dr == DialogResult.No) { return; }
            else
            {
                var l = dgvDatos.SelectedRows[0];
                QuitarFila(l);
                var romboBorrar = (Rombo)l.Tag;
                repo.Borrar(romboBorrar);
                ActualizarCantidadDeRegistros();
                MessageBox.Show("Fila eliminada", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            RecargarGrilla();
        }
        private void QuitarFila(DataGridViewRow l)
        {
            dgvDatos.Rows.Remove(l);
        }

        private void tsbEditar_Click_1(object sender, EventArgs e)
        {
            if (dgvDatos.SelectedRows.Count == 0)
            {
                return;
            }

            var FilaSeleccionada = dgvDatos.SelectedRows[0];
            Rombo rombo = (Rombo)FilaSeleccionada.Tag;
            Rombo romboCopia = (Rombo)rombo.Clone();
            frmRomboAE frm = new frmRomboAE() { Text = "Editar rombo" };
            frm.SetRombo(rombo);
            DialogResult dr = frm.ShowDialog(this);
            if (dr == DialogResult.Cancel)
            {
                return;
            }
            rombo = frm.GetRombo();
            if (!repo.Existe(rombo))
            {
                repo.Editar(romboCopia, rombo);
                SetearFila(FilaSeleccionada, rombo);
                MessageBox.Show("Fila editada", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                SetearFila(FilaSeleccionada, romboCopia);
                MessageBox.Show("Registro existente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsbFiltrar_Click(object sender, EventArgs e)
        {
            
           
        }

        private void TsbRefrescar_Click(object sender, EventArgs e)
        {
            RecargarGrilla();

        }

        private void frmRombos_Load(object sender, EventArgs e)
        {
            if (repo.GetCantidad() > 0)
            {
                RecargarGrilla();
            }
        }
    }
}
