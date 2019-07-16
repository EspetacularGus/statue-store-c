using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatueStore.BD {
    public class Subgrupo {

        public string nome { get; set; }
        public string descricao { get; set; }
        public string observacao { get; set; }
        public int idGrupo { get; set; }

        public void cadastrarBanco() {
            ConexaoStatue c = new ConexaoStatue();
            c.connect("bdStatueStore");
            c.command.CommandText = "INSERT SUBGRUPO VALUES(@NOME, @DESCRICAO, @OBS, @IDGRUPO)";
            c.command.Parameters.Add("@NOME", System.Data.SqlDbType.VarChar).Value = nome;
            c.command.Parameters.Add("@DESCRICAO", System.Data.SqlDbType.VarChar).Value = descricao;
            c.command.Parameters.Add("@OBS", System.Data.SqlDbType.VarChar).Value = observacao;
            c.command.Parameters.Add("@IDGRUPO", System.Data.SqlDbType.Int).Value = idGrupo;
            try {
                c.conexao.Open();
                c.command.ExecuteNonQuery();
                c.conexao.Close();
            } catch (Exception) {
                System.Windows.MessageBox.Show("Cadastro de subgrupo não realizado devido a um problema. Contate o Administrador do Sistema.");
            }
        }

        public static int GetID(String sgrupo) {
            try {  
                ConexaoStatue c = new ConexaoStatue();
                c.connect("bdStatueStore");
                c.conexao.Open();
                c.command.CommandText = "SELECT idSubgrupo FROM SUBGRUPO " +
                    "WHERE nomeSubgrupo LIKE '" + sgrupo + "'";
                System.Data.SqlClient.SqlDataReader dr = c.command.ExecuteReader();
                if (dr.HasRows) {
                    dr.Read();
                    return dr.GetInt32(0);
                } else {
                    return 1;
                }
            } 
            catch {
                System.Windows.MessageBox.Show("Não foi possível retornar o ID do subgrupo. Contate o Administrador do Sistema.");
                return 1;
            }
        }
    }
}
