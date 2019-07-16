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
    public partial class FormDetalhePedido : Form {
        public FormDetalhePedido(int id, string situ) {
            InitializeComponent();
            idPedido = id;
            Situacao = situ;
        }

        string Situacao = "";
        int idPedido = 0;

        private void FormDetalhePedido_Load(object sender, EventArgs e) {
            try {
                cbxSituacao.Items.Clear();
                ConexaoStatue c = new ConexaoStatue();
                c.connect("bdStatueStore");
                c.command.CommandText = "SELECT NOMESTATUS FROM STATUSPEDIDO";
                c.conexao.Open();
                SqlDataReader dr = c.command.ExecuteReader();

                if (dr.HasRows)
                    while (dr.Read())
                        cbxSituacao.Items.Add(dr.GetString(0));

                dr.Close();
                c.conexao.Close();
                cbxSituacao.Text = Situacao;
            } catch (Exception Ex) {
                MessageBox.Show("Não foi possível carregar as situações possíveis. Por favor, notifique o administrador do sistema com a seguinte mensagem: " + Ex.Message);
            }
        }

        private void btnLimpar_Click(object sender, EventArgs e) {
            Close();
        }

        private void btnEnviar_Click(object sender, EventArgs e) {
            try {
                ConexaoStatue c = new ConexaoStatue();
                c.connect("bdStatueStore");
                c.command.CommandText = "UPDATE PEDIDO SET idStatusPedido = (SELECT idStatusPedido FROM StatusPedido WHERE nomeStatus = @NOMESTATUS) WHERE idPedido = @IDPEDIDO";
                c.command.Parameters.Clear();
                c.command.Parameters.Add("@NOMESTATUS", SqlDbType.VarChar).Value = cbxSituacao.Text;
                c.command.Parameters.Add("@IDPEDIDO", SqlDbType.Int).Value = idPedido;
                c.conexao.Open();
                c.command.ExecuteNonQuery();
                c.conexao.Close();
                MessageBox.Show("Situação atualizada com sucesso!");
                Close();

            } catch (Exception Ex) {
                MessageBox.Show("Não foi possível atualizar o estado do pedido. Por favor, notifique o administrador do sistema com a seguinte mensagem: " + Ex.Message);
            }
        }
    }
}
