using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StatueStore.BD.BD.CADASTROS;
using StatueStore.BD;
using System.IO;
using System.Data.SqlClient;
using StatueStore.FormsCadastro;

namespace StatueStore {
    public partial class FormCadProduto : Form {

        public FormCadProduto(int idfunc = 0) {
            InitializeComponent();
            idFuncionario = idfunc;
        }
        private void FormCadProduto_Load(object sender, EventArgs e) {

            limpa();
            atualizaGrupo();
            carregaFornecedor();
        }

        private void btnLimpar_Click(object sender, EventArgs e) {
            limpa();
        }

        private void btnCarregaImagem_Click(object sender, EventArgs e) {
            lblNomeImagem.Visible = false;
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = false;
            ofd.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            if (ofd.ShowDialog() == DialogResult.OK) {
                pbxImagem.Image = Image.FromFile(ofd.FileName);
                imagePath = ofd.FileName;
                lblNomeImagem.Text = ofd.SafeFileName.ToString();
                imageExtension = Path.GetExtension(ofd.FileName);
            }
        }

        String imagePath;
        String imageExtension;

        private void btnEnviar_Click(object sender, EventArgs e) {
            //Validação dos campos
            validado = true;
            validaTxt(ref txtNome);
            validaTxt(ref txtDescricao);
            validaTxt(ref txtCusto);
            validaTxt(ref txtPreco);
            validaTxt(ref txtMarca);
            validaTxt(ref txtModelo);

            if (cbxGrupo.SelectedIndex == -1) {
                lblErrorGrupo.Visible = true;
                validado = false;
            }
            if (cbxSubgrupo.SelectedIndex == -1) {
                lblErrorSubgrupo.Visible = true;
                validado = false;
            }
            if (cbxSexo.SelectedIndex == -1) {
                lblErrorSexo.Visible = true;
                validado = false;
            }
            if (cbxFornecedor.SelectedIndex == -1) {
                validado = false;
                lblErrorForn.Visible = true;
            }

            if (pbxImagem.Image == null) {
                lblErrorImage.Visible = true;
                validado = false;
            }

            if (tamanhos.Count == 0)
                validado = false;
            //fim da validação

            //Verifica validação
            if (validado == false) {
                MessageBox.Show("Preencha corretamente os campos!", "Preenchimento incorreto", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtNome.Focus();
                return;
            }

            if (validado == true) {
                string imagemCaminhoFinal = txtNome.Text.Trim() + "_" + keyGen.GetRandomString(8) + imageExtension;
                Produto produto = new Produto();
                Subgrupo subgrupo = new Subgrupo();

                //Preenche os atributos da classe
                produto.Tamanhos = tamanhos;
                produto.nome = txtNome.Text;
                produto.imagem = @"..\dbProdutoImagens\" + imagemCaminhoFinal;
                produto.custo = Convert.ToDecimal(txtCusto.Text.Replace(" ", "").Replace("R$", "").Replace(",00", "").Replace(".00", ""));
                produto.preco = Convert.ToDecimal(txtPreco.Text.Replace(" ", "").Replace("R$", "").Replace(",00", "").Replace(".00", ""));
                produto.descricao = txtDescricao.Text;
                produto.descricaoRed = txtDescrRed.Text;
                produto.modelo = txtModelo.Text;
                produto.marca = txtMarca.Text;
                switch (cbxSexo.Text) {
                    case "Masculino":
                        produto.sexo = 'M';
                        break;
                    case "Feminino":
                        produto.sexo = 'F';
                        break;
                    case "Unissex":
                        produto.sexo = 'U';
                        break;
                }


                produto.date = dataHoje();
                produto.idsubgrupo = Subgrupo.GetID(cbxSubgrupo.Text);
                produto.idfunc = idFuncionario;
                produto.fornecedor = cbxFornecedor.Text;

                //Executa cadastro
                produto.cadastrarBanco();
                produto.CadastraFornProd();
                string caminhoFisico = @"C:\Users\ninja\Desktop\Entrega 2019 (Statue Store)\APLICACAO_WEB -\StatueStoreWebApplic\StatueStoreWebApplic\dbProdutoImagens\" + imagemCaminhoFinal;
                File.Copy(imagePath, caminhoFisico);

                MessageBox.Show("Dados Enviados com sucesso!", "Sucesso!", MessageBoxButtons.OK, MessageBoxIcon.None);

                Log l = new Log();
                l.setAll("CADASTRO", "PRODUTO", idFuncionario);
                l.RegLog();

                limpa();
            }
        }

        // Tirando os vermelhos;
        private void cbxGrupo_SelectedIndexChanged(object sender, EventArgs e) {
            lblErrorGrupo.Visible = false;
            validado = true;

            if (string.IsNullOrWhiteSpace(cbxGrupo.Text) || cbxGrupo.Text.Equals("Selecione"))
                cbxSubgrupo.Items.Clear();

            atualizaSubGrupo(cbxGrupo.Text);
        }

        private void cbxSubgrupo_SelectedIndexChanged(object sender, EventArgs e) {
            lblErrorSubgrupo.Visible = false;
            validado = true;
        }

        private void cbxSexo_SelectedIndexChanged(object sender, EventArgs e) {
            lblErrorSexo.Visible = false;
            validado = true;
        }

        private void chbP_CheckedChanged(object sender, EventArgs e) {
        }

        private void chbM_CheckedChanged(object sender, EventArgs e) {
        }


        private void chbGG_CheckedChanged(object sender, EventArgs e) {

        }
        private void cbxFornecedor_SelectedIndexChanged(object sender, EventArgs e) {
            lblErrorForn.Visible = false;
        }

        private void chbOutros_CheckedChanged(object sender, EventArgs e) {
        }

        //Precavendo letras em txt de numeros;
        private void txtCusto_KeyPress(object sender, KeyPressEventArgs e) {
            if ((Keys)e.KeyChar != Keys.Back && !char.IsNumber(e.KeyChar) && !char.IsPunctuation(e.KeyChar))
                e.Handled = true;
        }

        //Transformando em currency format
        private void txtCusto_Enter(object sender, EventArgs e) {
            txtCusto.Text = txtCusto.Text.Replace(" ", "").Replace("R$", "").Replace(",00", "").Replace(".00", "");
        }
        private void txtCusto_Leave(object sender, EventArgs e) {
            if (!string.IsNullOrEmpty(txtCusto.Text))
                txtCusto.Text = (Convert.ToDecimal(txtCusto.Text)).ToString("C");
        }

        private void txtPreco_Enter(object sender, EventArgs e) {
            txtPreco.Text = txtPreco.Text.Replace(" ", "").Replace("R$", "").Replace(",00", "").Replace(".00", "");
        }

        private void txtPreco_Leave(object sender, EventArgs e) {
            if (!string.IsNullOrEmpty(txtPreco.Text))
                txtPreco.Text = (Convert.ToDecimal(txtPreco.Text)).ToString("C");
        }

        //funções
        string dataHoje() {
            string data = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day;
            return data;
        }

        void carregaFornecedor() {
            try {
                cbxFornecedor.Items.Clear();
                ConexaoStatue c = new ConexaoStatue();
                c.connect("bdStatueStore");
                c.command.CommandText = "SELECT razaoSocial FROM FORNECEDOR";
                c.conexao.Open();
                SqlDataReader dr = c.command.ExecuteReader();
                if (dr.HasRows)
                    while (dr.Read())
                        cbxFornecedor.Items.Add(dr.GetString(0));

                dr.Close();
                c.conexao.Close();
            } catch (Exception Ex) {
                MessageBox.Show("Não foi possível carregar os Fornecedores possíveis. Por favor, notifique o administrador do sistema com a seguinte mensagem: " + Ex.Message);
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

        byte[] imgToByte() {
            try {
                if (pbxImagem.Image != null) {
                    MemoryStream ms = new MemoryStream();
                    pbxImagem.Image.Save(ms, pbxImagem.Image.RawFormat);
                    byte[] a = ms.GetBuffer();
                    return a;
                }
                return null;
            } catch {
                return null;
            }
        }
        void limpa() {
            lbxTamanhos.Items.Clear();
            tamanhos.Clear();
            txtNome.Clear();
            txtDescrRed.Clear();
            txtDescricao.Clear();
            txtCusto.Clear();
            txtPreco.Clear();
            txtModelo.Clear();
            txtMarca.Clear();
            
            cbxSexo.ResetText();
          
            lblErrorGrupo.Visible = false;
            lblErrorSexo.Visible = false;
          
            lblErrorSubgrupo.Visible = false;
            txtNome.Focus();
            pbxImagem.Image = null;
            lblNomeImagem.Text = "";
            
        }

        void validaTxt(ref TextBox txt) {
            if (txt.Text.Equals("")) {
                txt.BackColor = Color.FromArgb(255, 229, 229);
                validado = false;
            }
        }

        private void btnTamanhos_Click(object sender, EventArgs e) {
        }

        private void lbxTamanhos_SelectedIndexChanged(object sender, EventArgs e) {

        }

        private void btnAddTamanho_Click(object sender, EventArgs e) {



            //if (string.IsNullOrWhiteSpace(cbxTamanho.Text) || string.IsNullOrWhiteSpace(txtMinima.Text) || string.IsNullOrWhiteSpace(txtQuantidade.Text)) {
            //    MessageBox.Show("Preencha os campos de tamanho corretamente");
            //    return;
            //}

            //try {
            //    Convert.ToInt32(txtQuantidade.Text);
            //    Convert.ToInt32(txtMinima.Text);
            //} catch {
            //    MessageBox.Show("Preencha corretamente os campos de quantidade");
            //    return;
            //}

            //ConexaoStatue conexao = new ConexaoStatue();
            //conexao.connect();
            //conexao.conexao.Open();
            //conexao.command.CommandText = "SELECT TAMANHO FROM TAMANHO WHERE TAMANHO = @TAMANHO";
            //conexao.command.Parameters.Add("@TAMANHO", SqlDbType.VarChar).Value = cbxTamanho.Text;

            //int idTamanho = 0;


            //if(conexao.command.ExecuteScalar() == null) {
            //    if(MessageBox.Show("Não há registro do tamanho " + cbxTamanho.Text + ". Deseja adiciona-lo ?", "Ops!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes) {
            //        conexao.command.CommandText = "INSERT INTO TAMANHO OUTPUT INSERTED.IDTAMANHO VALUES (@TAMANHO)";
            //        idTamanho = (int)conexao.command.ExecuteScalar();
            //        MessageBox.Show("Tamanho cadastrado com sucesso!", "Sucesso!");
            //    }
            //    else {
            //        MessageBox.Show("Tamanho inválido", "Ops!");
            //        return;
            //    }
            //}
            //else {
            //    conexao.command.CommandText = "SELECT IDTAMANHO FROM TAMANHO WHERE TAMANHO = @TAMANHO";
            //    idTamanho = (int)conexao.command.ExecuteScalar();
            //}
            //conexao.conexao.Close();

            //Tamanho tamanho = new Tamanho();
            //tamanho.NomeTamanho = cbxTamanho.Text;
            //tamanho.IdTamanho = idTamanho;
            //tamanho.Quantidade = Convert.ToInt32(txtQuantidade.Text);
            //tamanho.QuantidadeMinima = Convert.ToInt32(txtMinima.Text);

            //tamanhos.Add(tamanho);

            //lbxTamanhos.Items.Clear();
            //foreach (var item in tamanhos)
            //    lbxTamanhos.Items.Add(item.NomeTamanho);
        }

        private void button1_Click(object sender, EventArgs e) {
            if (lbxTamanhos.SelectedIndex == -1)
                return;

            tamanhos.RemoveAt(lbxTamanhos.SelectedIndex);
            lbxTamanhos.Items.Remove(lbxTamanhos.Items[lbxTamanhos.SelectedIndex]);
        }

        //variaveis globais
        int idFuncionario;
        bool validado = true;
        public static List<Tamanho> tamanhos = new List<Tamanho>();

        private void button2_Click(object sender, EventArgs e) {

            Form frmaddTamanho = new FormAddTamanho();
            frmaddTamanho.ShowDialog();

            lbxTamanhos.Items.Clear();

            foreach (var item in tamanhos)
                lbxTamanhos.Items.Add(item.NomeTamanho + " - " + item.Quantidade);
        }
    }
}
