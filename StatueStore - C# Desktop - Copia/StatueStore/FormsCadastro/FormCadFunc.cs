using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StatueStore.BD;
using StatueStore.BD.BD.CADASTROS;
using System.Data.SqlClient;

namespace StatueStore {
    public partial class FormCadFunc : Form {
        public FormCadFunc(int idfunc = 0) {
            InitializeComponent();
            idFuncionario = idfunc;
        }

        private void FormCadFunc_Load(object sender, EventArgs e) {
            //Limpa tudo e foca nome
            txtNome.Focus();
            Limpa();
            //Alimenta textbox NivelAcesso com os do banco de dados
            c = new ConexaoStatue();
            cbxNivelAcesso.Items.Clear();
            cbxNivelAcesso.Items.Add("");
            c.connect("bdStatueStore");
            c.command.CommandText = "SELECT idNivelAcesso, nomeNivel from NivelAcesso order by idNivelAcesso";
            c.conexao.Open();
            dr = c.command.ExecuteReader();

            //Obtém os niveis de acesso alocados no banco
            if (dr.HasRows) {
                while (dr.Read()) {
                    cbxNivelAcesso.Items.Add(dr.GetInt32(0) + " - " + dr.GetString(1));
                }
            }
            c.conexao.Close();
            dr.Close();

            //popula a combobox do estado (UF)
            cbxEstado.Items.Clear();
            string[] UF;

            UF = new string[] { "AC", "AL", "AM", "AP", "BA", "CE", "DF", "ES", "GO", "MA", "MT", "MS", "MG", "PA", "PB", "PR", "PE", "PI", "RJ", "RN", "RO", "RS", "RR", "SC", "SE", "SP", "TO" };

            foreach (string a in UF)
                cbxEstado.Items.Add(a);
        }

        private void btnLimpar_Click(object sender, EventArgs e) {
            Limpa();
        }
        private void btnEnviar_Click(object sender, EventArgs e) {
            foreach (Control i in Controls)
                if (i is TextBox)
                    i.BackColor = Color.WhiteSmoke;


            bool preenchido = true;

            if (txtNome.Text.Equals("")) {
                preenchido = false;
                txtNome.BackColor = Color.FromArgb(247, 212, 212);
            }

            if (txtSobrenome.Text.Equals("")) {
                preenchido = false;
                txtSobrenome.BackColor = Color.FromArgb(247, 212, 212);
            }

            if (txtEmail.Text.Equals("")) {
                preenchido = false;
                txtEmail.BackColor = Color.FromArgb(247, 212, 212);
            }

            if (!mtxCPF.MaskCompleted) {
                preenchido = false;
                mtxCPF.BackColor = Color.FromArgb(247, 212, 212);
            }

            if (cbxSexo.SelectedIndex == 0) {
                lblErrorSexo.Visible = true;
                preenchido = false;
            } else
                lblErrorSexo.Visible = false;

            if (txtFuncao.Text.Equals("")) {
                preenchido = false;
                txtFuncao.BackColor = Color.FromArgb(247, 212, 212);
            }

            if (cbxNivelAcesso.SelectedIndex == 0) {
                lblErrorNivelAcesso.Visible = true;
                preenchido = false;
            } else
                lblErrorNivelAcesso.Visible = false;

            if (!mtxAdmissao.MaskCompleted) {
                mtxAdmissao.BackColor = Color.FromArgb(247, 212, 212);
                preenchido = false;
            }

            if (cbxRegimento.SelectedIndex == 0) {
                lblErrorRegimento.Visible = true;
                preenchido = false;
            } else
                lblErrorRegimento.Visible = false;

            if (txtPrecoHr.Text.Equals("")) {
                txtPrecoHr.BackColor = Color.FromArgb(247, 212, 212);
                preenchido = false;
            }

            if (!txtCEP.MaskCompleted) {
                preenchido = false;
                txtCEP.BackColor = Color.FromArgb(247, 212, 212);
            }

            if (txtEndereco.Text.Equals("")) {
                preenchido = false;
                txtEndereco.BackColor = Color.FromArgb(247, 212, 212);
            }

            if (txtNum.Text.Equals("")) {
                preenchido = false;
                txtNum.BackColor = Color.FromArgb(247, 212, 212);
            }

            if (txtBairro.Text.Equals("")) {
                preenchido = false;
                txtBairro.BackColor = Color.FromArgb(247, 212, 212);
            }

            if (txtCidade.Text.Equals("")) {
                preenchido = false;
                txtCidade.BackColor = Color.FromArgb(247, 212, 212);
            }

            if (cbxEstado.SelectedIndex == 0) {
                lblErrorUF.Visible = true;
                preenchido = false;
            } else
                lblErrorUF.Visible = false;

            // verifica se os campos foram preenchidos corretamente
            if (preenchido == false) {
                MessageBox.Show("Preencha corretamente os campos!", "Preenchimento incorreto", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtNome.Focus();
            }


            if (!checkCPF(mtxCPF.Text.Replace(" ", "").Replace(".", "").Replace("-", "").Replace("_", "").Replace(",", "").Trim())) {
                MessageBox.Show("O CPF inserido ja esta sendo utilizado.", "Ops", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                mtxCPF.Focus();
                mtxCPF.Clear();
                return;
            }



            if (preenchido == true) {
                // Instancia e cadastra funcionario
                funcionario = new Funcionario();

                funcionario.Nome = txtNome.Text;
                funcionario.Sobrenome = txtSobrenome.Text;
                funcionario.Email = txtEmail.Text;
                funcionario.Cpf = mtxCPF.Text.Replace(".", "").Replace("-", "").Replace(",", "");

                switch (cbxSexo.Text) {
                    case "Masculino":
                        funcionario.Sexo = 'M';
                        break;
                    case "Feminino":
                        funcionario.Sexo = 'F';
                        break;
                }

                funcionario.Cep = txtCEP.Text.Replace(" ", "").Replace("-", "").Replace("_", "").Replace(".", "").Replace(",", "").Trim();
                funcionario.Logradouro = txtEndereco.Text;
                funcionario.Complemento = txtComplemento.Text;
                funcionario.Bairro = txtBairro.Text;
                funcionario.Cidade = txtCidade.Text;
                funcionario.Estado = cbxEstado.Text;
                funcionario.Regimento = cbxRegimento.Text;
                funcionario.ValorHora = Convert.ToDecimal(txtPrecoHr.Text.Replace(" ", "").Replace("R$", "").Replace(",00", "").Replace(".00", ""));
                funcionario.idNivelDeAcesso = Convert.ToInt32(cbxNivelAcesso.Text.Substring(0, 1));
                funcionario.Funcao = txtFuncao.Text;
                funcionario.DataAdmissao = ArrumaData(mtxAdmissao.Text);

                if (mtxDemissao.MaskCompleted)
                    funcionario.DataDemissao = ArrumaData(mtxDemissao.Text);
                else
                    funcionario.DataDemissao = "";

                funcionario.Observacao = txtObs.Text;
                funcionario.IdFuncionarioCad = idFuncionario;
                funcionario.CadastrarBanco();
                MessageBox.Show("Dados Enviados com sucesso!", "Sucesso!", MessageBoxButtons.OK, MessageBoxIcon.None);

                //LOG
                Log l = new Log();
                l.setAll("CADASTRO", "FUNCIONARIO", idFuncionario);
                l.RegLog();
            }
        }

        private void txtPrecoHr_Leave(object sender, EventArgs e) {
            var a = Convert.ToInt32(txtPrecoHr.Text);
            txtPrecoHr.Text = a.ToString("C");
        }

        private void txtPrecoHr_Enter(object sender, EventArgs e) {
            txtPrecoHr.Text = txtPrecoHr.Text.Replace(" ", "").Replace("R$", "").Replace(",00", "").Replace(".00", "");
        }

        private void txtPrecoHr_KeyPress(object sender, KeyPressEventArgs e) {
            if (char.IsLetter(e.KeyChar) || char.IsWhiteSpace(e.KeyChar) || char.IsSymbol(e.KeyChar))
                e.Handled = true;
        }

        private void txtNum_KeyPress(object sender, KeyPressEventArgs e) {
            if (!char.IsNumber(e.KeyChar) && (Keys)e.KeyChar != Keys.Back)
                e.Handled = true;
        }

        // Funções

        private bool checkCPF(string cpf) {
            c.conexao.Close();

            bool check = false;
            c = new ConexaoStatue();
            c.connect("bdStatueStore");
            c.command.CommandText = "SELECT COUNT(*) FROM Funcionario WHERE cpf LIKE @X";
            c.command.Parameters.Clear();
            c.command.Parameters.Add("@X", SqlDbType.VarChar).Value = cpf;
            c.conexao.Open();
            SqlDataReader dr = c.command.ExecuteReader();
            if (dr.HasRows) {
                while (dr.Read()) {
                    if (dr.GetInt32(0) == 0)
                        check = true;
                    else
                        check = false;
                }
            }

            dr.Close();
            c.conexao.Close();

            if (check)
                return check;

            else 
                return check;
        }

        public string ArrumaData(string data) => data.Substring(6, 4) + "-" + data.Substring(3, 2) + "-" + data.Substring(0, 2);

        void Limpa() {
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
            txtObs.Clear();
            txtNome.Focus();
            txtTipoLogradouro.Clear();
            cbxPais.ResetText();
        }

        //Globais
        ConexaoStatue c;
        Funcionario funcionario;
        SqlDataReader dr;
        int idFuncionario;

    }
}
