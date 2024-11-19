using SegundoParcialRombo.Datos;
using SegundoParcialRombo.Entidades;

namespace SegundoParcialRombo.Windows
{
    public partial class frmRomboAE : Form
    {
        public frmRomboAE()
        {
            InitializeComponent();
        }
        private Rombo rombo;
        public Rombo GetRombo()
        { return rombo; }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (rombo != null)
            {
                txtDiagonalMayor.Text = rombo.DiagonalMayor.ToString();
                txtDiagonalMenor.Text = rombo.DiagonalMenor.ToString();

                 if (rombo.Contorno == Contorno.Solido)
                {
                    rbtSolido.Checked = true;
                }
                else if (rombo.Contorno == Contorno.Puntedo)
                {
                    rbtPunteado.Checked = true;
                }
                else if (rombo.Contorno == Contorno.Rayado)
                {
                    rbtRayado.Checked = true;
                }
                else
                {
                    rbtDoble.Checked = true;   
                }
            }
        }
        public void SetRombo(Rombo rombo)
        {
            this.rombo = rombo;
        }


        private void btnOK_Click_1(object sender, EventArgs e)
        {
            if (ValidarDatos())
            {
                if (rombo == null)
                {
                    rombo = new Rombo();
                }

                rombo.SetDiagonalMayor(int.Parse(txtDiagonalMayor.Text));
                rombo.SetDiagonalMenor(int.Parse(txtDiagonalMenor.Text));

                // Asignar el valor de Contorno basado en el radio button seleccionado
                if (rbtSolido.Checked)
                {
                    rombo.Contorno = Contorno.Solido;
                }
                else if (rbtPunteado.Checked)
                {
                    rombo.Contorno = Contorno.Puntedo;
                }
                else if (rbtRayado.Checked)
                {
                    rombo.Contorno = Contorno.Rayado;
                }
                else
                {
                    rombo.Contorno = Contorno.Doble;
                }

                DialogResult = DialogResult.OK;
            }
            DialogResult = DialogResult.OK;
            }
        
        private bool ValidarDatos()
        {
            bool valido = true;
            errorProvider1.Clear();

            if (!int.TryParse(txtDiagonalMayor.Text, out int lado) || lado <= 0)
            {
                valido = false;
                errorProvider1.SetError(txtDiagonalMayor, "Número no válido");
            }
            if (!int.TryParse(txtDiagonalMenor.Text, out int lado1) || lado1 <= 0)
            {
                valido = false;
                errorProvider1.SetError(txtDiagonalMenor, "Número no válido");
            }

            return valido;
        }

        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
