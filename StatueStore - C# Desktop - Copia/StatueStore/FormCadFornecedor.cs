using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StatueStore
{
    public partial class FormCadFornecedor : Form
    {
        public FormCadFornecedor()
        {
            InitializeComponent();
        }

        private void FormCadFornecedor_Load(object sender, EventArgs e)
        {

            txtRazaoSocial.Focus();

            cbxEstado.SelectedIndex = 0;
            cbxPais.SelectedIndex = 0;

        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {

            // Cor Vermelho para preenchimento incorreto - Color.FromArgb(247, 212, 212);
            bool preenchido = true;

            if (txtRazaoSocial.Text.Equals(""))
            {
                preenchido = false;
                txtRazaoSocial.BackColor = Color.FromArgb(247, 212, 212);
            }

            if (txtEmail.Text.Equals(""))
            {
                preenchido = false;
                txtEmail.BackColor = Color.FromArgb(247, 212, 212);
            }

            if (!mtxCNPJ.MaskCompleted)
            {
                preenchido = false;
                mtxCNPJ.BackColor = Color.FromArgb(247, 212, 212);
            }

            if (chbIsento.Checked == false) { 
                if (!mtxIE.MaskCompleted)
                {
                    preenchido = false;
                    mtxIE.BackColor = Color.FromArgb(247, 212, 212);
                }
            }

            if (txtRepresentante.Text.Equals(""))
            {
                preenchido = false;
                txtRepresentante.BackColor = Color.FromArgb(247, 212, 212);
            }
            if (!mtxTelefone1.MaskCompleted)
            {
                preenchido = false;
                mtxTelefone1.BackColor = Color.FromArgb(247, 212, 212);
            }

            if (txtCEP.Text.Equals(""))
            {
                preenchido = false;
                txtCEP.BackColor = Color.FromArgb(247, 212, 212);
            }

            if (txtEndereco.Text.Equals(""))
            {
                preenchido = false;
                txtEndereco.BackColor = Color.FromArgb(247, 212, 212);
            }

            if (txtNum.Text.Equals(""))
            {
                preenchido = false;
                txtNum.BackColor = Color.FromArgb(247, 212, 212);
            }

            if (txtBairro.Text.Equals(""))
            {
                preenchido = false;
                txtBairro.BackColor = Color.FromArgb(247, 212, 212);
            }

            if (txtCidade.Text.Equals(""))
            {
                preenchido = false;
                txtCidade.BackColor = Color.FromArgb(247, 212, 212);
            }

            if (cbxEstado.SelectedIndex == 0)
            {
                lblErrorUF.Visible = true;
                preenchido = false;
            }
            else
                lblErrorUF.Visible = false;

            if (cbxPais.SelectedIndex == 0)
            {
                lblErrorPais.Visible = true;
                preenchido = false;
            }
            else
                lblErrorPais.Visible = false;

            if (preenchido)
                MessageBox.Show("FORNECEDOR CADASTRADO COM SUCESSO ! ", "CONCLUÍDO", MessageBoxButtons.OK, MessageBoxIcon.None);
            else 
                MessageBox.Show("PREENCHA OS CAMPOS OBRIGATÓRIOS", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.None);
        }

        private void chbIsento_CheckedChanged(object sender, EventArgs e)
        {
            if(chbIsento.Checked == true)
            {
                mtxIE.Enabled = false;
                mtxIE.BackColor = Color.White;
                mtxIE.Clear();
            }
            else
            {
                mtxIE.Enabled = true;
                mtxIE.BackColor = Color.White;
            }
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            txtRazaoSocial.Clear();
            txtNomeFantasia.Clear();
            txtEmail.Clear();

            mtxCNPJ.Clear();
            mtxIE.Clear();

            txtRepresentante.Clear();
            mtxTelefone1.Clear();
            mtxTelefone2.Clear();

            cbxEstado.SelectedIndex = 0;
            cbxPais.SelectedIndex = 0;

            txtCEP.Clear();
            txtCidade.Clear();
            txtEndereco.Clear();
            txtComplemento.Clear();
            txtNum.Clear();
            txtBairro.Clear();

            chbIsento.Checked = false;

            txtRazaoSocial.Focus();
        }
    }
}
