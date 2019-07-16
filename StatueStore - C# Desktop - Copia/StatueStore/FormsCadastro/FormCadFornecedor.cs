using System;
using System.Drawing;
using System.Windows.Forms;
using StatueStore.BD.BD.CADASTROS;
using StatueStore.BD;
using System.ComponentModel;
using System.Collections.Generic;

namespace StatueStore
{
    public partial class FormCadFornecedor : Form
    {
        public FormCadFornecedor(int idfunc = 0)
        {
            InitializeComponent();
            idFuncionario = idfunc;
        }

        int idFuncionario;


        private void FormCadFornecedor_Load(object sender, EventArgs e)
        {
            txtRazaoSocial.Focus();
            btnLimpar.PerformClick();
            fornecedor = new Fornecedor();
            endereco = new Endereco();


            cbxEstado.Items.Clear();

            string[] UF;
            
            UF = new string[] { "AC", "AL", "AM", "AP", "BA", "CE", "DF", "ES", "GO", "MA", "MT", "MS", "MG", "PA", "PB", "PR", "PE", "PI", "RJ", "RN", "RO", "RS", "RR", "SC", "SE", "SP", "TO" };

            foreach (string a in UF)
                cbxEstado.Items.Add(a);

        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {

            // Cor Vermelho para preenchimento incorreto - Color.FromArgb(247, 212, 212);
            //Começo da validação
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

            if (chbIsento.Checked == false)
            {
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

            if (!txtCep.MaskCompleted)
            {
                preenchido = false;
                txtCep.BackColor = Color.FromArgb(247, 212, 212);
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

            if (cbxEstado.Text.Equals(""))
            {
                lblErrorUF.Visible = true;
                preenchido = false;
            }
            else
                lblErrorUF.Visible = false;

            if (cbxPais.Text.Equals(""))
            {
                lblErrorPais.Visible = true;
                preenchido = false;
            }
            else
                lblErrorPais.Visible = false;
            //FIM DA VALIDAÇÃO

            if (!txtEmail.Text.Contains("@"))
            {
                MessageBox.Show("Preencha o campo com um e-mail válido.", "Erro de validação", MessageBoxButtons.OK, MessageBoxIcon.None);
                preenchido = false;
            }

            //CADASTRANDO DADOS - SE PREENCHIDO...
            if (!preenchido)
            {
                MessageBox.Show("PREENCHA OS CAMPOS OBRIGATÓRIOS", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.None);
                return;
            }

            if (preenchido)
            {

                //Preenchendo atributos da classe
                fornecedor.razaoSocial = txtRazaoSocial.Text;
                fornecedor.nomeFantasia = txtNomeFantasia.Text;
                fornecedor.email = txtEmail.Text;
                fornecedor.cnpj = mtxCNPJ.Text.Replace(".", "").Replace("-", "").Replace("_", "").Replace("/", "").Replace(" ", "").Trim();
                fornecedor.ie = mtxIE.Text.Replace(".", "").Replace("-", "").Replace("_", "").Replace("/", "").Replace(" ", "").Trim();
                fornecedor.telefone = mtxTelefone1.Text.Replace("-", "").Trim().Replace("_", "").Replace(" ", "");
                fornecedor.telefone2 = mtxTelefone2.Text.Replace("-", "").Trim().Replace(" ", "");
                fornecedor.representante = txtRepresentante.Text.Replace(" ", "");
                fornecedor.obs = txtObs.Text;
                fornecedor.setDate();

                endereco.cep = txtCep.Text.Replace(".", "").Replace(" ", "").Replace("-", "").Replace("_", "").Trim();
                endereco.pais = cbxPais.Text;
                endereco.estado = cbxEstado.Text;
                endereco.cidade = txtCidade.Text;
                endereco.bairro = txtBairro.Text;
                endereco.logradouro = txtEndereco.Text;
                endereco.tipoLogradouro = txtTipoLogradouro.Text;
                endereco.numero = Convert.ToInt32(txtNum.Text);
                endereco.complementoEnd = txtComplemento.Text;

                int idEnd = endereco.cadastrarBanco();

                fornecedor.idEndereco = idEnd;
                fornecedor.idFunCad = idFuncionario;

                fornecedor.cadastraBanco();

                //Mensagem de concluído;
                MessageBox.Show("FORNECEDOR CADASTRADO COM SUCESSO ! ", "CONCLUÍDO", MessageBoxButtons.OK, MessageBoxIcon.None);

                Log lg = new Log();
                lg.setAll("CADASTRO", "FORNECEDOR", idFuncionario);
                lg.RegLog();
            }
        }

        private void chbIsento_CheckedChanged(object sender, EventArgs e)
        {
            if (chbIsento.Checked == true)
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

            txtCep.Clear();
            txtCidade.Clear();
            txtEndereco.Clear();
            txtComplemento.Clear();
            txtNum.Clear();
            txtBairro.Clear();
            txtObs.Clear();
            txtTipoLogradouro.Clear();

            chbIsento.Checked = false;

            txtRazaoSocial.Focus();
        }

        //globais
        Fornecedor fornecedor;
        Endereco endereco;

        private void txtNum_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && (Keys)e.KeyChar != Keys.Back) e.Handled = true;
        }

        private void mtxCNPJ_Enter(object sender, EventArgs e)
        {
            mtxCNPJ.SelectionStart = 0;
        }

        private void mtxIE_Enter(object sender, EventArgs e)
        {
            mtxIE.SelectionStart = 0;
        }

        private void mtxTelefone1_Enter(object sender, EventArgs e)
        {
            mtxTelefone1.SelectionStart = 0;
        }

        private void mtxTelefone2_Enter(object sender, EventArgs e)
        {
            mtxTelefone2.SelectionStart = 0;
        }

        private void txtNum_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && (Keys)e.KeyChar != Keys.Back) e.Handled = true;
        }
    }
}
