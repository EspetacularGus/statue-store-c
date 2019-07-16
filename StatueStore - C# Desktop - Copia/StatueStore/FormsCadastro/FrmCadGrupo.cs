using StatueStore.BD.BD.CADASTROS;
using System;
using System.Drawing;
using System.Windows.Forms;
using StatueStore.BD;

namespace StatueStore
{
    public partial class FormCadGrupo : Form
    {
        public FormCadGrupo(bool second = false, int idfunc = 0)
        {
            InitializeComponent();
            if (second)
                btnVoltar.Visible = true;

            idFuncionario = idfunc;
        }

        private void FrmCadGrupo_Load(object sender, EventArgs e) {
            btnLimpar.PerformClick();
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            validado = true;

            testaTxt(ref txtNome);

            if (validado) {
                try {
                    Grupo grupo = new Grupo();
                    grupo.nome = txtNome.Text;
                    grupo.descricao = txtDescricao.Text;
                    grupo.observacão = txtObs.Text;
                    grupo.cadastrarBanco();
                    MessageBox.Show("Dados Enviados Com Êxito!", "Sucesso!", MessageBoxButtons.OK, MessageBoxIcon.None);

                    //LOG
                    Log lg = new Log();
                    lg.setAll("CADASTRO", "GRUPO", idFuncionario);
                    lg.RegLog();
                }
                catch(Exception Ex) {
                    MessageBox.Show("Houve um problema com o cadastro. Informe o administrador do sistema.");
                }
            }
            else {
                MessageBox.Show("Preencha os campos corretamente", "Campos Incorretos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLimpar_Click_1(object sender, EventArgs e)
        {
            txtNome.Clear();
            txtDescricao.Clear();
            txtObs.Clear();
            txtNome.Focus();
        }

        //Funções
        void testaTxt(ref TextBox campo)
        {
            if (campo.Text.Equals(""))
            {
                campo.BackColor = Color.FromArgb(255, 229, 229);
                validado = false;
            }
        }

        //Globais
        bool validado;
        int idFuncionario;
    }
}
