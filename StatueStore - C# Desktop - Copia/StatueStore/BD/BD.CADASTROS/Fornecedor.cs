/*
 * SISTEMA STATUE STORE
 * ETESP @2º SEMESTRE 2018
*/

using System;
using System.Data;

namespace StatueStore.BD.BD.CADASTROS {
    public class Fornecedor {
        public string razaoSocial { get; set; }
        public string nomeFantasia { get; set; }
        public string email { get; set; }
        public string cnpj { get; set; }
        public string ie { get; set; }
        public string telefone { get; set; }
        public string telefone2 { get; set; }
        public string representante { get; set; }
        public string obs { get; set; }
        private string dataCad { get; set; }
        public int idEndereco { get; set; }
        public int idFunCad { get; set; }

        public void cadastraBanco() {
            try {
                ConexaoStatue c = new ConexaoStatue();
                c.connect("bdStatueStore");
                //idendereço e idfuncad estão nulos até tais classes serem criadas;
                c.command.CommandText = "INSERT Fornecedor VALUES(@RAZAO, @NOMEFANTASIA, @EMAIL, @CNPJ, @IE, @TELEFONE, @TELEFONE2, @REPRESENTANTE, @OBS, @DATACAD, @IDENDERECO, @IDFUNCAD)";
                c.command.Parameters.Add("@RAZAO", SqlDbType.VarChar).Value = razaoSocial;
                c.command.Parameters.Add("@NOMEFANTASIA", SqlDbType.VarChar).Value = nomeFantasia;
                c.command.Parameters.Add("@EMAIL", SqlDbType.VarChar).Value = email;
                c.command.Parameters.Add("@CNPJ", SqlDbType.VarChar).Value = cnpj;
                c.command.Parameters.Add("@IE", SqlDbType.VarChar).Value = ie;
                c.command.Parameters.Add("@TELEFONE", SqlDbType.VarChar).Value = telefone;
                c.command.Parameters.Add("@TELEFONE2", SqlDbType.VarChar).Value = telefone2;
                c.command.Parameters.Add("@REPRESENTANTE", SqlDbType.VarChar).Value = representante;
                c.command.Parameters.Add("@OBS", SqlDbType.VarChar).Value = obs;
                c.command.Parameters.Add("@DATACAD", SqlDbType.VarChar).Value = dataCad;
                c.command.Parameters.Add("@IDENDERECO", SqlDbType.Int).Value = idEndereco;
                c.command.Parameters.Add("@IDFUNCAD", SqlDbType.Int).Value = idFunCad;

                //Executa comando e fecha conexao
                c.conexao.Open();
                c.command.ExecuteNonQuery();
                c.conexao.Close();
            } catch(Exception ex) {
                System.Windows.MessageBox.Show("Erro ao cadastrar fornecedor. Por favor, informe um administrador sobre este problema com a seguinte mensagem: " + ex.Message);
            }
        }

        public void setDate() {
            dataCad = DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day;
        }
    }
}
