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
    public partial class FormCadFunc : Form
    {
        public FormCadFunc()
        {
            InitializeComponent();
        }

        private void FormCadFunc_Load(object sender, EventArgs e)
        {
            txtNome.Focus();

            cbxRegimento.SelectedIndex = 0;
            cbxEstado.SelectedIndex = 0;
            cbxNivelAcesso.SelectedIndex = 0;
            cbxSexo.SelectedIndex = 0;
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            Limpa();
        }
        private void btnEnviar_Click(object sender, EventArgs e)
        {
            bool preenchido = true;


            if (txtNome.Text.Equals("")) {
                preenchido = false;
                txtNome.BackColor = Color.FromArgb(247, 212, 212);
            }

            if (txtSobrenome.Text.Equals("")) {
                preenchido = false;
                txtSobrenome.BackColor = Color.FromArgb(247, 212, 212);
            }

            if (txtEmail.Text.Equals(""))
            {
                preenchido = false;
                txtEmail.BackColor = Color.FromArgb(247, 212, 212);
            }

            if (!mtxCPF.MaskCompleted)
            {
                preenchido = false;
                mtxCPF.BackColor = Color.FromArgb(247, 212, 212);
            }

            if (cbxSexo.SelectedIndex == 0)
            {
                lblErrorSexo.Visible = true;
                preenchido = false;
            }
            else
                lblErrorSexo.Visible = false;

            if (txtFuncao.Text.Equals(""))
            {
                preenchido = false;
                txtFuncao.BackColor = Color.FromArgb(247, 212, 212);
            }

            if (cbxNivelAcesso.SelectedIndex == 0)
            {
                lblErrorNivelAcesso.Visible = true;
                preenchido = false;
            }
            else
                lblErrorNivelAcesso.Visible = false;

            if (!mtxAdmissao.MaskCompleted)
            {
                mtxAdmissao.BackColor = Color.FromArgb(247, 212, 212);
                preenchido = false;
            }

            if (cbxRegimento.SelectedIndex == 0)
            {
                lblErrorRegimento.Visible = true;
                preenchido = false;
            }
            else
                lblErrorRegimento.Visible = false;

            if (txtPrecoHr.Text.Equals(""))
            {
                txtPrecoHr.BackColor = Color.FromArgb(247, 212, 212);
                preenchido = false;
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

            if (txtBairro.Text.Equals("")) {
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

            // 
            if (preenchido == false)
            {
                MessageBox.Show("Preencha corretamente os campos!", "Preenchimento incorreto", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtNome.Focus();
            }
            if (preenchido == true)
            {
                MessageBox.Show("Dados Enviados com sucesso!", "Sucesso!", MessageBoxButtons.OK, MessageBoxIcon.None);
            }
        }

        // Funções
        void Limpa()
        {
            txtNome.Clear();
            txtSobrenome.Clear();
            txtEmail.Clear();
            mtxCPF.Clear();

            cbxSexo.SelectedIndex = 0;

            txtFuncao.Clear();
            cbxNivelAcesso.SelectedIndex = 0;

            mtxAdmissao.Clear();
            mtxDemissao.Clear();

            cbxRegimento.SelectedIndex = 0;
            txtPrecoHr.Clear();
            txtCEP.Clear();
            txtEndereco.Clear();
            txtNum.Clear();
            txtComplemento.Clear();
            txtBairro.Clear();
            txtCidade.Clear();

            cbxEstado.SelectedIndex = 0;
            cbxPais.SelectedIndex = 0;

            txtObs.Clear();

            txtNome.Focus();
        }

        private void txtNum_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) || (Keys)e.KeyChar == Keys.Back)
                e.Handled = true; 
        }
    }
}
