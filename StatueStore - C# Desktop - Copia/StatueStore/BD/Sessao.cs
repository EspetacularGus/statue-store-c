using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatueStore.BD {
    public class Sessao {
  
        public int IdFuncionario { get; set; }
        public int NivelAcesso { get; set; }

        public void setSession() {
            Log log = new Log();
            log.setAreaAlt(string.Empty);
            log.setDatalhe("LOGIN SISTEMA");
            log.setIdFunc(IdFuncionario);
            log.RegLog();
        }
    }
}
