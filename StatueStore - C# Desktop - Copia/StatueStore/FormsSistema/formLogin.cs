using System;
using System.Data;
using System.Windows.Forms;
using StatueStore.BD;
using System.Data.SqlClient;
using StatueStore.FormsSistema;
using System.Drawing;

namespace StatueStore
{
    public partial class formLogin : Form {
        public formLogin() {
            InitializeComponent();
        }


        private void formLogin_Load(object sender, EventArgs e) {
            c.connect("bdStatueStore");
            txtUsername.Focus();
        }

        private void btnLogar_Click(object sender, EventArgs e) {
            // Verifica se o campo usuario esta vazio
            if (!txtUsername.MaskCompleted || txtPasswd.Text.Equals("")) {
                MessageBox.Show("Preencha os campos corretamente", "Campos invalidos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtUsername.Focus();
                return;
            }

            else {
                try
                {
                    c.command.CommandText = "SELECT COUNT(*) FROM Funcionario WHERE cpf like @CPF and senha like @SENHA;";
                    c.conexao.Open();
                    c.command.Parameters.Clear();
                    c.command.Parameters.Add("@CPF", SqlDbType.VarChar).Value = txtUsername.Text.Replace(",", "").Replace(".", "").Replace("-", "").Replace("_", "").Replace(" ", "").Trim();
                    c.command.Parameters.Add("@SENHA", SqlDbType.VarChar).Value = txtPasswd.Text;
                    SqlDataReader dr = c.command.ExecuteReader();
                    dr.Read();

                    // verifica se o login E senha estao corretos
                    if (dr.GetInt32(0) == 1)
                    {
                        dr.Close();
                        c.command.CommandText = "SELECT idFuncionario,idNivelAcesso FROM Funcionario WHERE cpf like @CPF and senha like @SENHA;";
                        dr = c.command.ExecuteReader();

                        //Pega as informações do funcionário logado;
                        if (dr.HasRows)
                        {
                            dr.Read();
                            s.IdFuncionario = dr.GetInt32(0);
                            s.NivelAcesso = dr.GetInt32(1);
                            c.conexao.Close();
                            dr.Close();
                        }
                        else {
                            MessageBox.Show("Houve algo errado ao retornar as informações do usuario. Notifique o administrador do sistema");
                            txtUsername.Clear();
                            txtPasswd.Clear();
                            txtUsername.Focus();
                            return;
                        }

                        //mandando para o menu
                        if (s.NivelAcesso == 1)
                        {
                            MessageBox.Show("Você não possui as permissões necessárias para acessar o sistema.");
                            txtUsername.Clear();
                            txtPasswd.Clear();
                            txtUsername.Focus();
                            c.conexao.Close();
                            dr.Close();
                            return;
                        }

                        else
                        {
                            if(txtPasswd.Text.Equals(txtUsername.Text.Replace(",", "").Replace(".", "").Replace("-", "").Replace("_", "").Replace(" ", "").Trim())) {
                                var frmAtualizaSenha = new frmFirstTimeLogin(s.IdFuncionario, txtUsername.Text.Replace(",", "").Replace(".", "").Replace("-", "").Replace("_", "").Replace(" ", "").Trim());
                                frmAtualizaSenha.FormClosed += FrmAtualizaSenha_FormClosed;
                                frmAtualizaSenha.ShowDialog();
                                return;
                            }

                            formMenu Menu = new formMenu(s.NivelAcesso, s.IdFuncionario);
                            s.setSession();
                            Menu.Show();
                            c.conexao.Close();
                            dr.Close();
                            this.Hide();
                        }
                    }

                    //Não retornou nenhum funcionario com aquele cpf e aquela senha
                    else
                    {
                        // Caso senha e/ou usuario incorretos
                        MessageBox.Show("Login e/ou Senha incorretos", "Falha no login", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        txtUsername.Focus();
                        dr.Close();
                        c.conexao.Close();
                    }
                    c.command.Parameters.Clear();
                    c.conexao.Close();
                    //Fim do botao
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("Erro ao logar no sistema. Contate o administrador do Sistema com a seguinte mensagem: " + Ex.Message);
                }
            }
        }

        private void FrmAtualizaSenha_FormClosed(object sender, FormClosedEventArgs e) {
            txtUsername.Clear();
            txtPasswd.Clear();
            txtUsername.Focus();
            MessageBox.Show("Insira seus dados novamente");
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtUsername_Enter(object sender, EventArgs e) {

        }


        //Global
        ConexaoStatue c = new ConexaoStatue();
        Sessao s = new Sessao();

        private void panel1_MouseDown(object sender, MouseEventArgs e) {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e) {
            if (mouseDown) {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e) {
            mouseDown = false;
        }

        private bool mouseDown;
        private Point lastLocation;

        private void panel4_Paint(object sender, PaintEventArgs e) {

        }
    }
}
