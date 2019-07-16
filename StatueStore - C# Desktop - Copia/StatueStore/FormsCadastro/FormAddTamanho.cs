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
using System.Data;
using System.Data.SqlClient;

namespace StatueStore.FormsCadastro {
    public partial class FormAddTamanho : Form {
        public FormAddTamanho() {
            InitializeComponent();
        }

        private void btnAddTamanho_Click(object sender, EventArgs e) {

            if (string.IsNullOrWhiteSpace(cbxTamanho.Text) || string.IsNullOrWhiteSpace(txtMinima.Text) || string.IsNullOrWhiteSpace(txtQuantidade.Text)) {
                MessageBox.Show("Preencha os campos de tamanho corretamente");
                return;
            }

            try {
                Convert.ToInt32(txtQuantidade.Text);
                Convert.ToInt32(txtMinima.Text);
            } catch {
                MessageBox.Show("Preencha corretamente os campos de quantidade");
                return;
            }

            ConexaoStatue conexao = new ConexaoStatue();
            conexao.connect();
            conexao.conexao.Open();
            conexao.command.CommandText = "SELECT TAMANHO FROM TAMANHO WHERE TAMANHO = @TAMANHO";
            conexao.command.Parameters.Add("@TAMANHO", SqlDbType.VarChar).Value = cbxTamanho.Text;

            int idTamanho = 0;


            if (conexao.command.ExecuteScalar() == null) {
                if (MessageBox.Show("Não há registro do tamanho " + cbxTamanho.Text + ". Deseja adiciona-lo ?", "Ops!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes) {
                    conexao.command.CommandText = "INSERT INTO TAMANHO OUTPUT INSERTED.IDTAMANHO VALUES (@TAMANHO, null)";
                    idTamanho = (int)conexao.command.ExecuteScalar();
                    MessageBox.Show("Tamanho cadastrado com sucesso!", "Sucesso!");
                }
                else {
                    MessageBox.Show("Tamanho inválido", "Ops!");
                    return;
                }
            }
            else {
                conexao.command.CommandText = "SELECT IDTAMANHO FROM TAMANHO WHERE TAMANHO = @TAMANHO";
                idTamanho = (int)conexao.command.ExecuteScalar();
            }
            conexao.conexao.Close();

            Tamanho tamanho = new Tamanho();
            tamanho.NomeTamanho = cbxTamanho.Text;
            tamanho.IdTamanho = idTamanho;
            tamanho.Quantidade = Convert.ToInt32(txtQuantidade.Text);
            tamanho.QuantidadeMinima = Convert.ToInt32(txtMinima.Text);

            FormCadProduto.tamanhos.Add(tamanho);
            Close();
        }

        private void FormAddTamanho_Load(object sender, EventArgs e) {
            ConexaoStatue conexao = new ConexaoStatue();
            conexao.connect();
            conexao.conexao.Open();
            conexao.command.CommandText = "SELECT TAMANHO FROM TAMANHO";

            SqlDataReader dr = conexao.command.ExecuteReader();

            if (dr.HasRows)
                while (dr.Read())
                    cbxTamanho.Items.Add(dr.GetString(0));

            conexao.conexao.Close();
        }
    }
}
