using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StatueStore.BD;
using StatueStore.BD.BD.CADASTROS;

namespace StatueStore.FormsVisualizar {
    public partial class FormEstoque : Form {
        public FormEstoque() {
            InitializeComponent();
        }


        private void btnBuscar_Click(object sender, EventArgs e) {
            ConexaoStatue conexao = new ConexaoStatue();
            conexao.connect();
            conexao.conexao.Open();
  

            String comando = "SELECT Produto.idProduto, nome as [Nome],precoVenda as [Preço],modelo,marca,sexo,quantidade,Tamanho FROM PRODUTO JOIN Detalhe_Tamanho " +
                "on Detalhe_Tamanho.idProduto = Produto.idProduto JOIN Tamanho on Detalhe_Tamanho.idTamanho = Tamanho.idTamanho ";

            if (!string.IsNullOrWhiteSpace(txtID.Text)) {
                comando += "WHERE PRODUTO.IDPRODUTO = @IDPRODUTO";
            }
            conexao.command.CommandText = comando;
            conexao.command.Parameters.Clear();
            conexao.command.Parameters.Add("@IDPRODUTO", SqlDbType.VarChar).Value = txtID.Text;

            SqlDataAdapter da = new SqlDataAdapter(conexao.command);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dgvVisualizacao.DataSource = ds.Tables[0];
            dgvVisualizacao.Refresh();

            conexao.conexao.Close();
        }

        private void FormEstoque_Load(object sender, EventArgs e) {
            ConexaoStatue conexao = new ConexaoStatue();
            conexao.connect();
            conexao.conexao.Open();


            String comando = "SELECT Produto.idProduto, nome as [Nome],precoVenda as [Preço],modelo,marca,sexo,quantidade,Tamanho FROM PRODUTO JOIN Detalhe_Tamanho " +
                "on Detalhe_Tamanho.idProduto = Produto.idProduto JOIN Tamanho on Detalhe_Tamanho.idTamanho = Tamanho.idTamanho ";

            conexao.command.CommandText = comando;
            conexao.command.Parameters.Clear();
            conexao.command.Parameters.Add("@IDPRODUTO", SqlDbType.VarChar).Value = txtID.Text;

            SqlDataAdapter da = new SqlDataAdapter(conexao.command);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dgvVisualizacao.DataSource = ds.Tables[0];
            dgvVisualizacao.Refresh();

            conexao.conexao.Close();
        }

        private void btnEditar_Click(object sender, EventArgs e) {

            if (id == 0) {
                MessageBox.Show("Selecione algum produto para edita-lo");
                return;
            }

            Produto produto = new Produto();
            produto.GetProdutoById(id);

            Form frmEditarProduto = new FormAtualizaProduto(produto, id);
            frmEditarProduto.WindowState = FormWindowState.Maximized;
            frmEditarProduto.FormClosed += FrmEditarProduto_FormClosed;
            frmEditarProduto.ShowDialog();
        }

        private void FrmEditarProduto_FormClosed(object sender, FormClosedEventArgs e) {
            dgvVisualizacao.Refresh();
        }

        int id = 0;

        private void dgvVisualizacao_CellClick(object sender, DataGridViewCellEventArgs e) {
            if (e.RowIndex == -1) return;

            id = Convert.ToInt32(dgvVisualizacao.Rows[e.RowIndex].Cells["idProduto"].Value);
        }
    }
}
