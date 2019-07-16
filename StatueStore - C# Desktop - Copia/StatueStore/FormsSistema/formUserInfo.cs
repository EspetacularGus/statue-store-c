using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StatueStore.BD;
using System.Data.SqlClient;

namespace StatueStore.FormsSistema {
    public partial class formUserInfo : Form {
        public formUserInfo(int id, int nivelAcesso) {
            InitializeComponent();
            //construtor se precisar

            funcID = id;
            FuncNivelAcesso = nivelAcesso;
        }

        private void formHome_Load(object sender, EventArgs e) {
            String nome = string.Empty;
            String cpf = string.Empty;
            String nomeAcesso = string.Empty;

            ConexaoStatue c = new ConexaoStatue();
            
            try {
                c.connect("bdStatueStore");
                c.command.CommandText = "SELECT nome,sobrenome,cpf FROM Funcionario where idFuncionario = @IDFUNC";
                c.command.Parameters.Add("@IDFUNC", SqlDbType.Int).Value = funcID;
                c.conexao.Open();
                SqlDataReader dr = c.command.ExecuteReader();
                if (dr.HasRows) {
                    dr.Read();
                    nome = dr.GetString(0);
                    nome += " " + dr.GetString(1);
                    lblLoad.Text = "Seja bem-vindo, " + dr.GetString(0) + "!";
                    cpf = dr.GetString(2);
                }

                dr.Close();

                c.command.CommandText = "SELECT nomeNivel FROM NivelAcesso WHERE idNivelAcesso = @IDNIVEL";
                c.command.Parameters.Add("@IDNIVEL", SqlDbType.Int).Value = FuncNivelAcesso;
                dr = c.command.ExecuteReader();
                if (dr.HasRows) {
                    dr.Read();
                    nomeAcesso = dr.GetString(0);
                }
                
                dr.Close();
                c.conexao.Close();
            } catch (Exception Ex) {
                MessageBox.Show("Não foi possível resgatar as informações ao seu respeito. Por favor entre em contato com o " +
                    "administrador do sistema com a seguinte mensagem: " + Ex.Message, "Ops", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                return;
            }
          
            //Tratando o CPF
            string cpfTratado = cpf.Substring(0, 3) + "." + cpf.Substring(3, 3) + "." + cpf.Substring(6, 3) + "-" + cpf.Substring(9, 2);

            //Aplicando os dados

            lblID.Text = funcID.ToString();
            lblNomeFunc.Text = nome;
            lblCPF.Text = cpfTratado;
            lblNiveldeAcesso.Text = nomeAcesso;
        }


        //GLOBAIS
        int funcID = 0;
        int FuncNivelAcesso = 0;
    }
}
