using System;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using StatueStore.BD;
using System.Drawing;

namespace StatueStore.FormsSistema {
    public partial class frmFirstTimeLogin : Form {
        int idFuncionario;
        string cpfFuncionario;
        public frmFirstTimeLogin(int id, string cpf) {
            InitializeComponent();
            idFuncionario = id;
            cpfFuncionario = cpf;
        }

        private void btnLogar_Click(object sender, EventArgs e) {
            if (txtPasswd.Text.Equals("") || txtConfirmedPasswd.Text.Equals("")) {
                MessageBox.Show("Por favor, preencha corretamente os dois campos.", "Preenchimento incorreto", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtPasswd.Focus();
                return;
            } else {
                if (!txtPasswd.Text.Equals(txtConfirmedPasswd.Text)) {
                    MessageBox.Show("As duas senhas precisam coincidir. Por favor, repita a senha novamente.", "Preenchimento incorreto", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtConfirmedPasswd.Clear();
                    txtConfirmedPasswd.Focus();
                    return;
                }

                else if (txtPasswd.Text.Equals(cpfFuncionario)) {
                    MessageBox.Show("Sua senha não pode ser igual ao seu CPF.", "Ops", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                else {
                    ConexaoStatue c = new ConexaoStatue();
                    try {
                        // Atualiza a senha;
                        c.connect("bdStatueStore");
                        c.command.CommandText = "UPDATE Funcionario SET senha = @PASS WHERE idFuncionario = @ID";
                        c.command.Parameters.Clear();
                        c.command.Parameters.Add("@PASS", SqlDbType.VarChar).Value = txtPasswd.Text;
                        c.command.Parameters.Add("@ID", SqlDbType.VarChar).Value = idFuncionario;
                        c.conexao.Open();
                        c.command.ExecuteNonQuery();
                        c.conexao.Close();

                        if (MessageBox.Show("Senha atualizada", "Sucesso!", MessageBoxButtons.OK, MessageBoxIcon.None) == DialogResult.OK) this.Close();
                    } 
                    
                    catch (Exception Ex) {
                        MessageBox.Show("Houve algo errado ao atualizar a sua senha. Por favor notifique o administrador do sistema com a seguinte mensagem: " + Ex.Message, "Ops", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtConfirmedPasswd.Enabled = false;
                        txtPasswd.Enabled = false;
                        btnLogar.Enabled = false;
                    }
                }
            }
        }



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
    }
}
