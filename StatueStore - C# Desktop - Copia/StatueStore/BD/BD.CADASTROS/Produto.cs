using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace StatueStore.BD.BD.CADASTROS {
    public class Produto {

        public string nome { get; set; }
        public string imagem { get; set; }
        public string descricao { get; set; }
        public string descricaoRed { get; set; }
        public decimal custo { get; set; }
        public decimal preco { get; set; }
        public string modelo { get; set; }
        public string marca { get; set; }
        public char sexo { get; set; }
        public string date { get; set; }
        public int idsubgrupo { get; set; }
        public int idfunc { get; set; }
        public List<Tamanho> Tamanhos { get; set; }

        //Para outra tabela
        public string fornecedor { get; set; }
        private int idprod { get; set; }

        public void cadastrarBanco() {
            try {
                //usando classe de conexao
                ConexaoStatue c = new ConexaoStatue();
                c.connect("bdStatueStore");
                c.conexao.Open();

                //inserindo dados no banco
                c.command.CommandText = "INSERT PRODUTO output INSERTED.idProduto " +
                    "VALUES(@NOME, @IMAGEM, @CUSTO, @PRECO, @DESCRICAO, @DESCRICAORED, @MODELO, @MARCA, @SEXO, @DATACAD, @IDSUBGRUPO, @IDFUNC)";

                c.command.Parameters.Clear();
                c.command.Parameters.AddWithValue("@NOME", nome);
                c.command.Parameters.AddWithValue("@IMAGEM", imagem);
                c.command.Parameters.Add("@CUSTO", System.Data.SqlDbType.Money).Value = custo;
                c.command.Parameters.Add("@PRECO", System.Data.SqlDbType.Money).Value = preco;
                c.command.Parameters.AddWithValue("@DESCRICAO", descricao);
                c.command.Parameters.AddWithValue("@DESCRICAORED", descricaoRed);
                c.command.Parameters.AddWithValue("@MODELO", modelo);
                c.command.Parameters.AddWithValue("@MARCA", marca);
                c.command.Parameters.Add("@SEXO", System.Data.SqlDbType.Char).Value = sexo;
                c.command.Parameters.AddWithValue("@DATACAD", date);
                c.command.Parameters.AddWithValue("@IDSUBGRUPO", idsubgrupo);
                c.command.Parameters.AddWithValue("@IDFUNC", idfunc);

                idprod = (int)c.command.ExecuteScalar();
                c.command.Parameters.Clear();

                c.command.CommandText = "INSERT INTO DETALHE_TAMANHO VALUES (@QUANTIDADE, @QUANTIDADEMIN, @IDTAMANHO, @IDPRODUTO)";
                foreach(var item in Tamanhos) {
                    c.command.Parameters.Clear();
                    c.command.Parameters.Add("@QUANTIDADE", SqlDbType.Int).Value = item.Quantidade;
                    c.command.Parameters.Add("@QUANTIDADEMIN", SqlDbType.Int).Value = item.QuantidadeMinima;
                    c.command.Parameters.Add("@IDTAMANHO", SqlDbType.Int).Value = item.IdTamanho;
                    c.command.Parameters.Add("@IDPRODUTO", SqlDbType.Int).Value = idprod;
                    c.command.ExecuteNonQuery();
                }
                c.conexao.Close();
                System.Windows.MessageBox.Show("Produto cadastrado com sucesso!", "Sucesso!");

            } catch (Exception Ex) {
                System.Windows.MessageBox.Show("Não foi possível realizar o cadastro de produto. Por favor notifique o administrador do sistema com a seguinte mensagem: " + Ex.Message);
            }
            // fim da função
        }

        public void CadastraFornProd() {
            try {
                int idFornecedor = 0;
                ConexaoStatue c = new ConexaoStatue();
                c.connect("bdStatueStore");
                c.command.CommandText = "Select idFornecedor FROM FORNECEDOR WHERE razaoSocial LIKE @RAZAO";
                c.command.Parameters.Clear();
                c.command.Parameters.Add("@RAZAO", SqlDbType.VarChar).Value = fornecedor;
                c.conexao.Open();

                idFornecedor = (int)c.command.ExecuteScalar();

                c.command.CommandText = "INSERT INTO Fornecedor_Produto Values(@DATAHOJE, @IDFORNECEDOR, @IDPRODUTO, null)";

                c.command.Parameters.Add("@DATAHOJE", SqlDbType.Date).Value = date;
                c.command.Parameters.Add("@IDFORNECEDOR", SqlDbType.Int).Value = idFornecedor;
                c.command.Parameters.Add("@IDPRODUTO", SqlDbType.Int).Value = idprod;

                c.command.ExecuteNonQuery();

                c.conexao.Close();

            } catch (Exception Ex) {
                System.Windows.MessageBox.Show("Não foi possível realizar o cadastro de relação: Fornecedor_produto. Por favor notifique o administrador do sistema com a seguinte mensagem: " + Ex.Message);
            }
        }

        public void GetProdutoById(int id) {
            ConexaoStatue c = new ConexaoStatue();
            c.connect();
            c.command.CommandText = "SELECT * FROM PRODUTO WHERE IDPRODUTO = @IDPRODUTO";
            c.command.Parameters.Clear();
            c.command.Parameters.Add("@IDPRODUTO", SqlDbType.Int).Value = id;
            c.conexao.Open();

            SqlDataReader dr = c.command.ExecuteReader();

            if (dr.HasRows) {
                while (dr.Read()) {
                    nome = dr.GetString(1);
                    imagem = dr.GetString(2);
                    custo = dr.GetDecimal(3);
                    preco = dr.GetDecimal(4);
                    descricao = dr.GetString(5);
                    descricaoRed = dr.GetString(6);
                    modelo = dr.GetString(7);
                    marca = dr.GetString(8);
                    sexo = Convert.ToChar(dr.GetString(9));
                    idsubgrupo = dr.GetInt32(11);
                }
            }

            dr.Close();
            c.command.ExecuteNonQuery();
            c.conexao.Close();
        }
    }
}
