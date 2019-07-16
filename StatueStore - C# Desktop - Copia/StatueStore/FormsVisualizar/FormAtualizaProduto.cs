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
    public partial class FormAtualizaProduto : Form {

        int idProduto = 0;
        public FormAtualizaProduto(Produto produto, int id) {
            InitializeComponent();
            prod = produto;
            idProduto = id;
        }

        string imagemOriginal = "";
        private void FormAtualizaProduto_Load(object sender, EventArgs e) {

            atualizaGrupo();

            txtNome.Text = prod.nome;
            txtDescricao.Text = prod.descricao;
            txtDescrRed.Text = prod.descricaoRed;
            txtMarca.Text = prod.marca;
            txtModelo.Text = prod.modelo;
            txtPreco.Text = prod.preco.ToString("C");
            txtCusto.Text = prod.custo.ToString("C");
            imagemOriginal = prod.imagem.Substring(20);

            pbxImagem.Image = Image.FromFile(@"C:\Users\ninja\Desktop\Entrega 2019 (Statue Store)\APLICACAO_WEB -\StatueStoreWebApplic\StatueStoreWebApplic" + prod.imagem.Substring(2));

            ConexaoStatue c = new ConexaoStatue();
            c.connect("bdStatueStore");
            c.command.CommandText = "SELECT nomeGrupo,Nomesubgrupo FROM GRUPO JOIN SUBGRUPO ON GRUPO.idGrupo = Subgrupo.idGrupo WHERE IDSUBGRUPO = @IDSUBGRUPO";
            c.command.Parameters.Add("@IDSUBGRUPO", SqlDbType.Int).Value = prod.idsubgrupo;
            c.conexao.Open();
            SqlDataReader dr = c.command.ExecuteReader();

            if (dr.HasRows) {
                dr.Read();
                cbxGrupo.Text = dr.GetString(0);
                cbxSubgrupo.Text = dr.GetString(1);
            }

            switch (prod.sexo) {
                case 'M':
                    cbxSexo.Text = "Masculino";
                    break;
                case 'F':
                    cbxSexo.Text = "Feminino";
                    break;
                case 'U':
                    cbxSexo.Text = "Unissex";
                    break;

            }

            dr.Close();
            c.conexao.Close();
        }
        void atualizaGrupo() {
            try {
                cbxGrupo.Items.Clear();
                ConexaoStatue c = new ConexaoStatue();
                c.connect("bdStatueStore");
                c.command.CommandText = "SELECT nomeGrupo FROM grupo";
                c.conexao.Open();
                SqlDataReader dr = c.command.ExecuteReader();

                if (dr.HasRows)
                    while (dr.Read())
                        cbxGrupo.Items.Add(dr.GetString(0));

                dr.Close();
                c.conexao.Close();
            } catch (Exception Ex) {
                MessageBox.Show("Não foi possível carregar os Grupos possíveis. Por favor, notifique o administrador do sistema com a seguinte mensagem: " + Ex.Message);
            }
        }

        void atualizaSubGrupo(string grupo) {
            ConexaoStatue c;
            SqlDataReader dr;
            try {
                cbxSubgrupo.Items.Clear();
                c = new ConexaoStatue();
                c.connect();
                c.command.CommandText = "SELECT nomeSubgrupo FROM Subgrupo WHERE idGrupo IN (SELECT IDGRUPO FROM GRUPO where nomeGrupo = @GRUPO)";
                c.command.Parameters.Clear();
                c.command.Parameters.Add("@GRUPO", SqlDbType.VarChar).Value = grupo;
                c.conexao.Open();
                dr = c.command.ExecuteReader();

                if (dr.HasRows)
                    while (dr.Read())
                        cbxSubgrupo.Items.Add(dr.GetString(0));

                dr.Close();
                c.conexao.Close();
            } catch (Exception Ex) {
                MessageBox.Show("Não foi possível carregar os subgrupos possíveis. Por favor, notifique o administrador do sistema com a seguinte mensagem: " + Ex.Message);
            }
        }

        Produto prod;

        private void cbxGrupo_SelectedIndexChanged(object sender, EventArgs e) {

            if (string.IsNullOrWhiteSpace(cbxGrupo.Text) || cbxGrupo.Text.Equals("Selecione"))
                cbxSubgrupo.Items.Clear();

            atualizaSubGrupo(cbxGrupo.Text);
        }

        private void button1_Click(object sender, EventArgs e) {
            Close();
        }

        private void btnEnviar_Click(object sender, EventArgs e) {

            ConexaoStatue conexao = new ConexaoStatue();
            conexao.connect();
            conexao.conexao.Open();
            conexao.command.CommandText = "UPDATE PRODUTO SET NOME = @NOME, PRECOCUSTO = @CUSTO, PRECOVENDA = @PRECOVENDA, DESCRICAO = @DESCRICAO, " +
                "DESCRICAORED = @DESCRED, MODELO = @MODELO, MARCA = @MARCA, SEXO = @SEXO, IDSUBGRUPO = @IDSUBGRUPO";
            conexao.command.Parameters.Clear();
            conexao.command.Parameters.Add("@NOME", SqlDbType.VarChar).Value = txtNome.Text.Trim();

            if (mudou) {
                string imagemCaminhoFinal = txtNome.Text.Trim() + "_" + keyGen.GetRandomString(8) + imageExtension;
                string caminhoFisico = @"C:\Users\ninja\Desktop\Entrega 2019 (Statue Store)\APLICACAO_WEB -\StatueStoreWebApplic\StatueStoreWebApplic\dbProdutoImagens\" + imagemCaminhoFinal;
                System.IO.File.Copy(imagePath, caminhoFisico);
                conexao.command.CommandText += ", IMAGEM = @IMAGEM";
                conexao.command.Parameters.Add("@IMAGEM", SqlDbType.VarChar).Value = @"../dbProdutoImagens/" + imagemCaminhoFinal;
            }
            conexao.command.Parameters.Add("@CUSTO", SqlDbType.Decimal).Value = Convert.ToDecimal(txtCusto.Text.Replace(" ", "").Replace("R$", "").Replace(",00", "").Replace(".00", ""));
            conexao.command.Parameters.Add("@PRECOVENDA", SqlDbType.Decimal).Value = Convert.ToDecimal(txtPreco.Text.Replace(" ", "").Replace("R$", "").Replace(",00", "").Replace(".00", ""));
            conexao.command.Parameters.Add("@DESCRICAO", SqlDbType.VarChar).Value = txtDescricao.Text.Trim();
            conexao.command.Parameters.Add("@DESCRED", SqlDbType.VarChar).Value = txtDescrRed.Text.Trim();
            conexao.command.Parameters.Add("@MODELO", SqlDbType.VarChar).Value = txtModelo.Text.Trim();
            conexao.command.Parameters.Add("@MARCA", SqlDbType.VarChar).Value = txtMarca.Text.Trim();
            switch (cbxSexo.Text) {
                case "Masculino":
                    conexao.command.Parameters.Add("@SEXO", SqlDbType.Char).Value = 'M';
                    break;
                case "Feminino":
                    conexao.command.Parameters.Add("@SEXO", SqlDbType.Char).Value = 'F';
                    break;
                case "Unissex":
                    conexao.command.Parameters.Add("@SEXO", SqlDbType.Char).Value = 'U';
                    break;
            }
            conexao.command.Parameters.Add("@IDSUBGRUPO", SqlDbType.VarChar).Value = Subgrupo.GetID(cbxSubgrupo.Text);
            conexao.command.CommandText += " WHERE IDPRODUTO = @IDPRODUTO";
            conexao.command.Parameters.Add("@IDPRODUTO", SqlDbType.Int).Value = idProduto;
            conexao.command.ExecuteNonQuery();
            MessageBox.Show("Produto atualizado com sucesso!");
            Close();
        }

        private void btnCarregaImagem_Click(object sender, EventArgs e) {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = false;
            ofd.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            if (ofd.ShowDialog() == DialogResult.OK) {
                pbxImagem.Image = Image.FromFile(ofd.FileName);
                imagePath = ofd.FileName;
                imageExtension = System.IO.Path.GetExtension(ofd.FileName);
                mudou = true;
            }
        }
        bool mudou = false;
        String imagePath;
        String imageExtension;
    }
}
