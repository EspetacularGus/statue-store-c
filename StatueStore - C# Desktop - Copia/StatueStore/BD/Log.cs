using System;
using System.Data;

namespace StatueStore.BD {
    public class Log {
        private string Detalhe { get; set; }
        private string DataAlt { get; set; }
        private string AreaAlt { get; set; }
        private int idFunc { get; set; }

        public Log() {
            //pega data agora;
            DataAlt = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" +
                DateTime.Now.Day + " " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second;
        }

        public void RegLog() {
            ConexaoStatue c = new ConexaoStatue();
            try {
                c.connect("bdStatueStore");
                c.command.CommandText = "INSERT LOGS VALUES(@DETALHE, @DATA, @AREA, @IDFUNC)";
                c.command.Parameters.Add("@DETALHE", SqlDbType.VarChar).Value = Detalhe;
                c.command.Parameters.Add("@DATA", SqlDbType.DateTime).Value = DataAlt;
                c.command.Parameters.Add("@AREA", SqlDbType.VarChar).Value = AreaAlt;
                c.command.Parameters.Add("@IDFUNC", SqlDbType.Int).Value = idFunc;
                c.conexao.Open();
                c.command.ExecuteNonQuery();
            } catch (Exception ex) {
                System.Windows.MessageBox.Show("Não foi possível criar um registro desta ação. Por favor notifique o administrador do sistema com a seguinte mensagem: " + ex.Message);
            } finally {
                c.conexao.Close();
            }
        }

        //sets
        public void setDatalhe(string det) {
            Detalhe = det;
        }
        public void setAreaAlt(string area){
            AreaAlt = area;
        }
        public void setIdFunc(int id) {
            idFunc = id;
        }

        public void setAll(string detalhe, string area, int id)
        {
            Detalhe = detalhe;
            AreaAlt = area.Trim();
            idFunc = id;
        }
    }
}
