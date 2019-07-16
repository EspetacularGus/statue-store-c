using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace StatueStore.BD.BD.CADASTROS {
    class Grupo {

        ConexaoStatue c;
        public string nome { get; set; }
        public string descricao { get; set; }
        public string observacão { get; set; }

        public void cadastrarBanco() {
            c = new ConexaoStatue();
            try {
                c.connect("bdStatueStore");
                c.command.CommandText = "INSERT GRUPO output INSERTED.idGrupo VALUES(@NOME, @DESCRICAO, @OBS)";
                c.command.Parameters.Add("@NOME", SqlDbType.VarChar).Value = nome;
                c.command.Parameters.Add("@DESCRICAO", SqlDbType.VarChar).Value = descricao;
                c.command.Parameters.Add("@OBS", SqlDbType.VarChar).Value = observacão;
                c.conexao.Open();
                c.command.ExecuteNonQuery();
            } catch (Exception ex) {
                System.Windows.MessageBox.Show("Houve algum problema ao cadastrar o grupo. Contate o Administrador do Sistema com a mensagem: " + ex.Message);
            } finally {
                c.conexao.Close();
            }
        }

        public static int GetID(string grupo) {
            try {
                ConexaoStatue c = new ConexaoStatue();
                c.connect("bdStatueStore");
                c.command.CommandText = "SELECT idGrupo FROM GRUPO WHERE nomeGrupo LIKE @grupo";
                c.command.Parameters.Add("@grupo", SqlDbType.VarChar).Value = grupo;
                c.conexao.Open();
                System.Data.SqlClient.SqlDataReader dr = c.command.ExecuteReader();
                //leitura dos id's;
                if (dr.HasRows) {
                    while (dr.Read()) {
                        return dr.GetInt32(0);
                    }
                }
                //numero padrao de catch = 1
                return 1;
            } catch {
                System.Windows.MessageBox.Show("Houve algo errado ao retornar o id do grupo. Informe o administrador do sistema.");
                //numero padrao de catch = 1
                return 1;
            }
        }
    }
}
