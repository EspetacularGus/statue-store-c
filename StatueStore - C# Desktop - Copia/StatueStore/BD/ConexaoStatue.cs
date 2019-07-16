using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;

namespace StatueStore.BD
{
    public class ConexaoStatue
    {

        public SqlConnection conexao;
        public SqlCommand command;


        public void connect(string name = "bdStatueStore")
        {
            try {
                String sqlString = ConfigurationManager.ConnectionStrings[name].ConnectionString;
                conexao = new SqlConnection(sqlString);
                command = new SqlCommand();
                command.Connection = conexao;

            } catch (Exception Ex) {
                System.Windows.MessageBox.Show("ERRO AO TENTAR SE CONECTAR COM O BANCO DE DADOS. NOTIFIQUE O ADMINISTRADOR DO SISTEMA.");
            }
        }


    }
}
