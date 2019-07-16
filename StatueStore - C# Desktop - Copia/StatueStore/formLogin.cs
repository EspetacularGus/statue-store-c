using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StatueStore
{
    public partial class formLogin : Form
    {
        public formLogin()
        {
            InitializeComponent();
        }

        private void btnLogar_Click(object sender, EventArgs e)
        {
            // Verifica se o campo usuario esta vazio
            if (txtUsername.Text.Equals("")) {
                MessageBox.Show("Preencha o campo usuário corretamente", "Campos invalidos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtUsername.Focus();
            }
            
            // Verifica se o campo senha esta vazio
            else if (txtPasswd.Text.Equals("")) {
                MessageBox.Show("Preencha o campo senha corretamente", "Campos invalidos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtPasswd.Focus();
            }

            else {
                // verifica se o login E senha estao corretos
                if (txtUsername.Text.Equals("teste") && txtPasswd.Text.Equals("teste123456")) {
                    formMenu Menu = new formMenu();
                    Menu.Show();
                    this.Hide();           
                }
                else {  
                    // Caso senha e/ou usuario incorretos, 
                    MessageBox.Show("Login e/ou Senha incorretos", "Falha no login", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    txtUsername.Focus();
                } 
            }
        }
  
        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
            txtUsername.BackColor = Color.FromArgb(64, 64, 64);
        }

        private void txtPasswd_TextChanged(object sender, EventArgs e)
        {
            txtPasswd.BackColor = Color.FromArgb(64, 64, 64);
        }

        private void formLogin_Load(object sender, EventArgs e)
        {
            txtUsername.Focus();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
