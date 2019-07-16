/*
 * SISTEMA STATUE STORE
 * ETESP @2º SEMESTRE 2018
*/

using System;
using System.Data;

namespace StatueStore.BD.BD.CADASTROS {
    public class Endereco {
        public string cep { get; set; }
        public string pais { get; set; }
        public string estado { get; set; }
        public string cidade { get; set; }
        public string bairro { get; set; }
        public string logradouro { get; set; }
        public string tipoLogradouro { get; set; }
        public int numero { get; set; }
        public string complementoEnd { get; set; }

        public int cadastrarBanco() {
            ConexaoStatue c = new ConexaoStatue();
            try {
                c.connect("bdStatueStore");
                c.command.CommandText = "INSERT Endereco output INSERTED.idEndereco VALUES(@CEP, @PAIS, @ESTADO, @CIDADE, @BAIRRO, @LOGRADOURO, @TIPOLOGRADOURO, @NUMERO, @COMPLEMENTOEND)";
                c.command.Parameters.Clear();
                c.command.Parameters.Add("@CEP", SqlDbType.VarChar).Value = cep;
                c.command.Parameters.Add("@PAIS", SqlDbType.VarChar).Value = pais;
                c.command.Parameters.Add("@ESTADO", SqlDbType.VarChar).Value = estado;
                c.command.Parameters.Add("@CIDADE", SqlDbType.VarChar).Value = cidade;
                c.command.Parameters.Add("@BAIRRO", SqlDbType.VarChar).Value = bairro;
                c.command.Parameters.Add("@LOGRADOURO", SqlDbType.VarChar).Value = logradouro;
                c.command.Parameters.Add("@TIPOLOGRADOURO", SqlDbType.VarChar).Value = tipoLogradouro;
                c.command.Parameters.Add("@NUMERO", SqlDbType.Int).Value = numero;
                c.command.Parameters.Add("@COMPLEMENTOEND", SqlDbType.VarChar).Value = complementoEnd;
                c.conexao.Open();
                return (int)c.command.ExecuteScalar();
            }
            catch (Exception Ex) {
                System.Windows.MessageBox.Show("Não foi possivel cadastrar o enderço. Por favor, notifique o administrador do sistema com a seguinte mensagem: " + Ex.Message);
                return 0;
            } 
            finally {
                c.conexao.Close();
            }
        }
    }
}