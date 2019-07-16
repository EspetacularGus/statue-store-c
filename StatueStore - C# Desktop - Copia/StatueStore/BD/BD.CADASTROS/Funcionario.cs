using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatueStore.BD.BD.CADASTROS {
    class Funcionario {
        public string Cpf { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public char Sexo { get; set; }
        public string Email { get; set; }
        public string Funcao { get; set; }
        public string DataAdmissao { get; set; }
        public string DataDemissao { get; set; }
        public decimal ValorHora { get; set; }
        public string Regimento { get; set; }
        public string Observacao { get; set; }
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public int NumeroCasa { get; set; }
        public string Cep { get; set; }
        public string Complemento { get; set; }
        public int idNivelDeAcesso { get; set; }
        public int IdFuncionarioCad { get; set; }

        public void CadastrarBanco() {
            ConexaoStatue c = new ConexaoStatue();
            c.connect("bdStatueStore");
            c.command.CommandText = "INSERT FUNCIONARIO VALUES(" +
                "@CPF, @NOME, @SOBRENOME, @SEXO, @EMAIL, @CPF, @FUNCAO, @DATAADMISSAO, @DATADEMISSAO, @VALORHORA, @REGIMENTO, @OBSERVACAO, @LOGRADOURO, @BAIRRO," +
                "@CIDADE, @ESTADO, @NUMEROCASA, @CEP, @COMPLEMENTO, @IDNIVELACESSO, @IDFUNCIONARIOCAD" +
                ")";
            c.command.Parameters.AddWithValue("@CPF", Cpf);
            c.command.Parameters.AddWithValue("@NOME", Nome);
            c.command.Parameters.AddWithValue("@SOBRENOME", Sobrenome);
            c.command.Parameters.AddWithValue("@SEXO", Sexo);
            c.command.Parameters.AddWithValue("@EMAIL", Email);
            c.command.Parameters.AddWithValue("@FUNCAO", Funcao);
            c.command.Parameters.AddWithValue("@DATAADMISSAO", DataAdmissao);
            c.command.Parameters.AddWithValue("@DATADEMISSAO", DataDemissao);
            c.command.Parameters.AddWithValue("@VALORHORA", ValorHora);
            c.command.Parameters.AddWithValue("@REGIMENTO", Regimento);
            c.command.Parameters.AddWithValue("@OBSERVACAO", Observacao);
            c.command.Parameters.AddWithValue("@LOGRADOURO", Logradouro);
            c.command.Parameters.AddWithValue("@BAIRRO", Bairro);
            c.command.Parameters.AddWithValue("@CIDADE", Cidade);
            c.command.Parameters.AddWithValue("@ESTADO", Estado);
            c.command.Parameters.AddWithValue("@NUMEROCASA", NumeroCasa);
            c.command.Parameters.AddWithValue("@CEP", Cep);
            c.command.Parameters.AddWithValue("@COMPLEMENTO", Complemento);
            c.command.Parameters.AddWithValue("@IDNIVELACESSO", idNivelDeAcesso);
            c.command.Parameters.AddWithValue("@IDFUNCIONARIOCAD", IdFuncionarioCad);
            c.conexao.Open();
            c.command.ExecuteNonQuery();
            c.conexao.Close();
        }
    }
}
