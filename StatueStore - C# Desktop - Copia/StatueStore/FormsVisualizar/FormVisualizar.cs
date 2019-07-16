using System;
using System.Windows.Forms;
using StatueStore.BD;
using System.Data;
using System.Data.SqlClient;


namespace StatueStore.FormsVisualizar {
    public partial class FormVisualizar : Form {
        int NivelAcessoFunc = 0;
        public FormVisualizar(int nivelAcesso, int id) {
            InitializeComponent();
            NivelAcessoFunc = nivelAcesso;
            FuncId = id;
        }

        private void btnLimpar_Click(object sender, EventArgs e) {
            cbxFiltro.ResetText();
            cbxTipo.ResetText();
            txtFiltro.Clear();

            dgvVisualizacao.DataSource = null;
            dgvVisualizacao.Rows.Clear();
            dgvVisualizacao.Refresh();
        }

        private void FormVisualizar_Load(object sender, EventArgs e) {

            cbxFiltro.Enabled = false;
            txtFiltro.Enabled = false;

            cbxTipo.Items.Clear();
            c = new ConexaoStatue();
            c.connect("bdStatueStore");
            c.command.CommandText = "SELECT name FROM sysobjects WHERE xtype = 'U'";
            c.conexao.Open();
            SqlDataReader dr = c.command.ExecuteReader();

            if (dr.HasRows) {
                while (dr.Read()) {
                    switch (NivelAcessoFunc) {
                        //NIVEIS DE ACESSO

                        //Basico
                        case 2:
                            if (dr.GetString(0) == "Pedido" || dr.GetString(0) == "Fornecedor" || dr.GetString(0) == "Produto" || dr.GetString(0) == "Cliente") {
                                cbxTipo.Items.Add(dr.GetString(0));
                            }
                            break;

                        //ADMINISTRADOR
                        case 3:
                            if (dr.GetString(0) == "Pedido" || dr.GetString(0) == "Fornecedor" || dr.GetString(0) == "Produto" || dr.GetString(0) == "Cliente") {
                                cbxTipo.Items.Add(dr.GetString(0));
                            }
                            break;

                        //GERENTE
                        case 4:
                            cbxTipo.Items.Add(dr.GetString(0));
                            break;
                    }
                }
            }

            c.conexao.Close();
        }

        private void cbxTipo_SelectedIndexChanged(object sender, EventArgs e) {
            cbxFiltro.Items.Clear();
            cbxFiltro.Items.Add("Nenhum");

            if (!cbxTipo.Text.Equals("")) {
                cbxFiltro.Enabled = true;
                txtFiltro.Enabled = true;

                c = new ConexaoStatue();
                c.connect("bdStatueStore");
                c.command.CommandText = "select COLUMN_NAME from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME = @TABELA";
                c.command.Parameters.Clear();
                c.command.Parameters.Add("@TABELA", SqlDbType.VarChar).Value = cbxTipo.Text.Trim();
                c.conexao.Open();
                SqlDataReader dr = c.command.ExecuteReader();
                if (dr.HasRows) {
                    while (dr.Read()) {
                        if (!dr.GetString(0).Equals("senha")){
                            cbxFiltro.Items.Add(dr.GetString(0));
                        }
                    }
                }
                c.conexao.Close();
                dr.Close();
            }
        }
        private void cbxFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxFiltro.Text.Equals("Nenhum")) txtFiltro.Enabled = false;
            else txtFiltro.Enabled = true;
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            if (cbxTipo.Text.Equals(""))
            {
                MessageBox.Show("A seleção de 'Procurar Em:' é obrigatória", "Preenchimento incorreto", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else if (cbxFiltro.Text.Equals(""))
            {
                MessageBox.Show("A seleção do filtro é obrigatória", "Preenchimento incorreto", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (cbxFiltro.Text.Equals("Nenhum"))
            {
                ConexaoStatue c = new ConexaoStatue();
                c.connect("bdStatueStore");
                c.command.CommandText = "SELECT * FROM " + cbxTipo.Text.Trim();
                SqlDataAdapter da = new SqlDataAdapter(c.command);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dgvVisualizacao.DataSource = ds.Tables[0];
                dgvVisualizacao.Refresh();
                c.conexao.Close();
                return;
            }
            else
            {
                ConexaoStatue c = new ConexaoStatue();
                c.connect("bdStatueStore");
                c.command.Parameters.Clear();
                c.command.CommandText = "SELECT * FROM " + cbxTipo.Text.Trim() + " WHERE " + cbxFiltro.Text.Trim() + " LIKE '" + txtFiltro.Text.Trim() + "'";
                //c.command.Parameters.Add("@TAB", SqlDbType.VarChar).Value = cbxTipo.Text.Trim();
                //c.command.Parameters.Add("@COL", SqlDbType.VarChar).Value = cbxFiltro.Text.Trim();
                //c.command.Parameters.Add("@FILTRO", SqlDbType.VarChar).Value = cbxTipo.Text.Trim();
                SqlDataAdapter da = new SqlDataAdapter(c.command);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dgvVisualizacao.DataSource = ds.Tables[0];
                dgvVisualizacao.Refresh();
                c.conexao.Close();
                return;
            }
        }

        //GLOBAIS
        ConexaoStatue c;
        int FuncId = 0;
    }
}
