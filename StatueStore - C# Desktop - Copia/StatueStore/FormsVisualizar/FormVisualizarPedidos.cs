using StatueStore.BD;
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

namespace StatueStore.FormsVisualizar {
    public partial class FormVisualizarPedidos : Form {
        public FormVisualizarPedidos() {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, EventArgs e) {
            ConexaoStatue conexao = new ConexaoStatue();
            conexao.connect();
            conexao.conexao.Open();


            String comando = "SELECT Pedido.idPedido as [ID do Pedido],Envio.dataPrevisao as [Data de Previsão]," +
                "StatusPedido.nomeStatus as [Situação atual],email as [Email],CLIENTE.nome as [Nome],sobrenome as [Sobrenome],cpf as [CPF] FROM PEDIDO " +
                "JOIN ENVIO ON PEDIDO.idEnvio = ENVIO.idEnvio LEFT JOIN StatusPedido ON StatusPedido.idStatusPedido = PEDIDO.idStatusPedido JOIN CLIENTE ON PEDIDO.idCliente = CLIENTE.idCliente";
            conexao.command.Parameters.Clear();

            if (!string.IsNullOrWhiteSpace(txtID.Text)) {
                comando += " WHERE PEDIDO.idPedido = @IDPEDIDO";
                conexao.command.Parameters.Add("@IDPEDIDO", SqlDbType.VarChar).Value = txtID.Text;
            }
            conexao.command.CommandText = comando;

            SqlDataAdapter da = new SqlDataAdapter(conexao.command);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dgvVisualizacao.DataSource = ds.Tables[0];
            dgvVisualizacao.Refresh();

            conexao.conexao.Close();
        }

        private void FormVisualizarPedidos_Load(object sender, EventArgs e) {
            ConexaoStatue conexao = new ConexaoStatue();
            conexao.connect();
            conexao.conexao.Open();


            String comando = "SELECT Pedido.idPedido as [ID do Pedido],Envio.dataPrevisao as [Previsão],StatusPedido.nomeStatus as [Situação atual],email as [Email]" +
                ",CLIENTE.nome as [Nome],sobrenome as [Sobrenome],cpf as [CPF] FROM PEDIDO JOIN ENVIO " +
                "ON PEDIDO.idEnvio = ENVIO.idEnvio LEFT JOIN StatusPedido ON StatusPedido.idStatusPedido = PEDIDO.idStatusPedido JOIN CLIENTE ON PEDIDO.idCliente = CLIENTE.idCliente";

            conexao.command.CommandText = comando;

            SqlDataAdapter da = new SqlDataAdapter(conexao.command);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dgvVisualizacao.DataSource = ds.Tables[0];
            dgvVisualizacao.Refresh();

            conexao.conexao.Close();
        }

        int id = 0;
        string atualsitu = "";
        private void dgvVisualizacao_CellClick(object sender, DataGridViewCellEventArgs e) {
            if (e.RowIndex == -1) return;

            id = Convert.ToInt32(dgvVisualizacao.Rows[e.RowIndex].Cells["ID do Pedido"].Value);
            atualsitu = dgvVisualizacao.Rows[e.RowIndex].Cells["Situação atual"].Value.ToString();
        }

        private void btnEditar_Click(object sender, EventArgs e) {
            if (id == 0) {
                MessageBox.Show("Selecione algum produto para edita-lo");
                return;
            }

            Form frmAtualiza = new FormDetalhePedido(id, atualsitu);
            frmAtualiza.ShowDialog();

        }
    }
}
