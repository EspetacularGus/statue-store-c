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
    public partial class FormCadProduto : Form {
        public FormCadProduto() {
            InitializeComponent();
        }
        private void FormCadProduto_Load(object sender, EventArgs e) {
            limpa();
        }

        private void btnLimpar_Click(object sender, EventArgs e) {
            limpa();
        }

        private void btnEnviar_Click(object sender, EventArgs e) {

            validado = true;

            validaTxt(ref txtNome);
            validaTxt(ref txtDescricao);
            validaTxt(ref txtDescrRed);
            validaTxt(ref txtCusto);
            validaTxt(ref txtPreco);
            validaTxt(ref txtMarca);
            validaTxt(ref txtModelo);

            if (!chbP.Checked && !chbM.Checked && !chbG.Checked && !chbGG.Checked && !chbOutros.Checked) {
                validado = false;
                lblErrorTamanho.Visible = true;
            }
            else {
                lblErrorTamanho.Visible = false;
            }

            if (chbP.Checked)
                validaTxt(ref txtQtdP);
            if (chbM.Checked)
                validaTxt(ref txtQtdM);
            if (chbG.Checked)
                validaTxt(ref txtQtdG);
            if (chbGG.Checked)
                validaTxt(ref txtQtdGG);

            if (chbOutros.Checked) {
                validaTxt(ref txtOutrosQuantidade);
                validaTxt(ref txtOutrosTamanho);
            }

            if (cbxGrupo.SelectedIndex == 0) {
                lblErrorGrupo.Visible = true;
                validado = false;
            }

            if (cbxSubgrupo.SelectedIndex == 0) {
                lblErrorSubgrupo.Visible = true;
                validado = false;
            }

            if (cbxSexo.SelectedIndex == 0) {
                lblErrorSexo.Visible = true;
                validado = false;
            }

            // 
            if (validado == false) {
                MessageBox.Show("Preencha corretamente os campos!", "Preenchimento incorreto", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtNome.Focus();
            }
            if (validado == true) {
                MessageBox.Show("Dados Enviados com sucesso!", "Sucesso!", MessageBoxButtons.OK, MessageBoxIcon.None);
                this.Refresh();
            }
        }


        private void cbxGrupo_SelectedIndexChanged(object sender, EventArgs e) {
            lblErrorGrupo.Visible = false;
            validado = true;
        }

        private void cbxSubgrupo_SelectedIndexChanged(object sender, EventArgs e) {
            lblErrorSubgrupo.Visible = false;
            validado = true;
        }

        private void cbxSexo_SelectedIndexChanged(object sender, EventArgs e) {
            lblErrorSexo.Visible = false;
            validado = true;
        }

        private void chbP_CheckedChanged(object sender, EventArgs e) {
            if (chbP.Checked) {
                txtQtdP.Enabled = true;
                txtQtdP.BackColor = Color.WhiteSmoke;
            } else {
                txtQtdP.Enabled = false;
                txtQtdP.BackColor = Color.DarkGray;
            }
        }

        private void chbM_CheckedChanged(object sender, EventArgs e) {
            if (chbM.Checked) {
                txtQtdM.Enabled = true;
                txtQtdM.BackColor = Color.WhiteSmoke;
            } else {
                txtQtdM.Enabled = false;
                txtQtdM.BackColor = Color.DarkGray;
            }
        }

        private void chbG_CheckedChanged(object sender, EventArgs e) {
            if (chbG.Checked) {
                txtQtdG.Enabled = true;
                txtQtdG.BackColor = Color.WhiteSmoke;
            } else {
                txtQtdG.Enabled = false;
                txtQtdG.BackColor = Color.DarkGray;
            }
        }

        private void chbGG_CheckedChanged(object sender, EventArgs e) {
            if (chbGG.Checked) {
                txtQtdGG.Enabled = true;
                txtQtdGG.BackColor = Color.WhiteSmoke;
            } else {
                txtQtdGG.Enabled = false;
                txtQtdGG.BackColor = Color.DarkGray;
            }
        }

        private void chbOutros_CheckedChanged(object sender, EventArgs e) {
            if (chbOutros.Checked) {
                lblOutrosTamanho.Visible = true;
                lblOutrosQtd.Visible = true;
                txtOutrosTamanho.Visible = true;
                lblOutrosQtd.Visible = true;
                txtOutrosQuantidade.Visible = true;
            } else {
                lblOutrosTamanho.Visible = false;
                lblOutrosQtd.Visible = false;
                txtOutrosTamanho.Visible = false;
                lblOutrosQtd.Visible = false;
                txtOutrosQuantidade.Visible = false;
            }
        }

        private void txtCusto_KeyPress(object sender, KeyPressEventArgs e) {
            if ((Keys)e.KeyChar != Keys.Back && !char.IsNumber(e.KeyChar) && !char.IsPunctuation(e.KeyChar))
                e.Handled = true;
        }

        //funcoes
        void limpa() {
            txtNome.Clear();
            txtDescrRed.Clear();
            txtDescricao.Clear();
            txtCusto.Clear();
            txtPreco.Clear();
            txtModelo.Clear();
            txtMarca.Clear();
            nudMinQtd.Value = 1;
            nudQtd.Value = 1;
            cbxSexo.SelectedIndex = 0;
            cbxGrupo.SelectedIndex = 0;
            cbxSubgrupo.SelectedIndex = 0;
            chbP.Checked = false;
            chbM.Checked = false;
            chbG.Checked = false;
            chbGG.Checked = false;
            chbOutros.Checked = false;
            txtQtdG.Clear();
            txtQtdGG.Clear();
            txtQtdM.Clear();
            txtQtdP.Clear();
            txtOutrosQuantidade.Clear();
            txtOutrosTamanho.Clear();
            lblErrorGrupo.Visible = false;
            lblErrorSexo.Visible = false;
            lblErrorTamanho.Visible = false;
            lblErrorSubgrupo.Visible = false;
            txtNome.Focus();
        }

        void validaTxt(ref TextBox txt) {
            if (txt.Text.Equals("")) {
                txt.BackColor = Color.FromArgb(255, 229, 229);
                validado = false;
            }
        }


        //variaveis globais
        bool validado = true;
    }
}
