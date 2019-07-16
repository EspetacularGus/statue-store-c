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
using System.Data.SqlClient;

namespace StatueStore.FormsCadastro
{
    public partial class FormCadSubGrupo : Form
    {
        public FormCadSubGrupo(int idfunc = 0)
        {
            InitializeComponent();
            idFuncionario = idfunc;
        }
        private void FormCadSubGrupo_Load(object sender, EventArgs e)
        {
            atualizaCbx();
            btnLimpar.PerformClick();
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            txtNome.Clear();
            txtDescricao.Clear();
            txtObservacao.Clear();
            cbxGrupo.ResetText();
            txtNome.Focus();
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            validado = true;

            if (cbxGrupo.Text.Equals("")) {
                lblErrorGrupo.Visible = true;
                validado = false;
            }
            else {
                lblErrorGrupo.Visible = false;
            }

            testaTxt(ref txtNome);

            if (validado) {
                Subgrupo sgrupo = new Subgrupo();
                sgrupo.nome = txtNome.Text;
                sgrupo.descricao = txtDescricao.Text;
                sgrupo.observacao = txtObservacao.Text;
                sgrupo.idGrupo = BD.BD.CADASTROS.Grupo.GetID(cbxGrupo.Text);
                sgrupo.cadastrarBanco();
                MessageBox.Show("Dados Cadastrados com sucesso!");

                Log l = new Log();
                l.setAll("CADASTRO", "GRUPO", idFuncionario);
                l.RegLog();
            }
            else {
                MessageBox.Show("Preencha corretamente os campos!", "Preenchimento incorreto", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtNome.Focus();
                return;
            }
        }

        private void btnAddGrupo_Click(object sender, EventArgs e) {
            var displaySecForm = new FormCadGrupo(true, idFuncionario) {
                Dock = DockStyle.Fill,
                FormBorderStyle = FormBorderStyle.FixedSingle,
                WindowState = FormWindowState.Maximized,
                MaximizeBox = false,
                MinimizeBox = false
            };
            displaySecForm.Show();
            displaySecForm.FormClosed += new FormClosedEventHandler(DisplaySecForm_FormClosed);
        }

        private void DisplaySecForm_FormClosed(object sender, FormClosedEventArgs e) {
            atualizaCbx();
        }

        private void cbxGrupo_SelectedIndexChanged(object sender, EventArgs e) {
            lblErrorGrupo.Visible = false;
        }

        //funções
        public void atualizaCbx() {
            cbxGrupo.Items.Clear();
            ConexaoStatue c;
            SqlDataReader dr;
            try {
                c = new ConexaoStatue();
                c.connect("bdStatueStore");
                c.command.CommandText = "SELECT nomeGrupo FROM GRUPO";
                c.conexao.Open();
                dr = c.command.ExecuteReader();

                if (dr.HasRows)
                    while (dr.Read())
                        cbxGrupo.Items.Add(dr.GetString(0));

                dr.Close();
                c.conexao.Close();
            } catch {
                MessageBox.Show("Houve um problema ao preencher os grupos na caixa de seleção. Avise o administrador do Sistema.");
                btnEnviar.Enabled = false;
            } finally {
                c = null;
                dr = null;
            }
        }
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
