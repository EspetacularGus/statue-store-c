using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Mail;
using System.Windows;

namespace StatueStore.BD {
    public class statueEmailSender {


        public int idFuncionario { get; set; }
        public string nomeFuncionario { get; set; }


        public string nomeProduto { get; set; }
        public int idProduto { get; set; }
        public int qtdAtual { get; set; }
        public int qtdMinima { get; set; }



        private string Body { get; set; }
        public string Assunto { get; set; }

        MailMessage mm = new MailMessage();

        public void AdicionarEmail(string adr) {
            mm.To.Add(new MailAddress(adr));
        }

        public void Enviar() {
            try {
                //Servidor smtp
                SmtpClient client = new SmtpClient();
                client.Port = 587;
                client.Host = "smtp.gmail.com";
                client.EnableSsl = true;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential("statuestoreoficial@gmail.com", "!statue456");
                //Mensagem
                mm.From = new MailAddress("StatueStore@gmail.com", "StatueStore", Encoding.UTF8);
                mm.Body = Body;
                mm.IsBodyHtml = true;
                mm.Subject = Assunto;
                mm.BodyEncoding = UTF8Encoding.UTF8;
                mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                client.Send(mm);
            } catch (Exception ex) {
                MessageBox.Show("Erro ao comunicar os responsáveis pela resposição de estoque. \n " +
                   "Por favor notificar o administrador do sistema com a seguinte mensagem: " + ex.Message, "Erro de preenchimento");
            }

        }

        public void setBody() {
            /*
            string corpo = @"
                <head>
                    <style>
                        @import url('https://fonts.googleapis.com/css?family=Roboto:700i,500');

                        .titulo {
                            height: 200px;
                            width: 500px;
                            font-family: 'Roboto', sans-serif;
                            font-size: 60px;
                            padding-top: 3%;
                        }

                        #title2 {
                            font-size: 30;
                        }

                        #message {
                            padding-top: 5%;
                            height: 200px;
                            width: 500px;
                            padding-left: %;
                            font-size: 20px;
                            font-family: 'Roboto', sans-serif;
                            font-weight: 500;
                            font-style: medium;
                        }

                        #inner {
                            font-size: 15px;
                            font-family: 'Roboto', sans-serif;
                            font-weight: 500;
                            font-style: medium;
                        }
                    </style>


                </head>

                    <body>
                        <table>
                            <center>
                                <div style='background-color: rgb(233, 29, 99)' class='titulo'>
                                    < span id = 'title' > Statue Store </ span > < br >
     
                                         < span id = 'title2' > < i > Software </ i ></ span >
      
                                      </ div >
      
                                  </ center >
      
                                  < div id = 'message' >
                                       DE: @FUNCNOME<br>
                                       ID: @FUNCID

                                       <br> < br >< br >< br >
       
                                       < div id = 'inner' >

                                            O PRODUTO NOMEPRODUTO, DO ID IDPRODUTO, PRECISA DE RESPOSIÇÃO: <BR>
                                    QUANTIDADE ATUAL: QTDATUAL
                                    QUANTIDADE MINIMA: QTDMINIMA

                                </ div >

                            </ div >
                        </ table >
                    </body>
                ";*/

            string corpo = "<H1> DE: @FUNCNOME <BR> " +
                            "ID: @FUNCID </H1> <br><br>" +
                            "<h3>Foi requisitado a reposição do produto: 'NOMEPRODUTO', de identificação: IDPRODUTO <br> " +
                                    "QUANTIDADE ATUAL: QTDATUAL <BR>" +
                                    "QUANTIDADE MINIMA: QTDMINIMA </h3>";

            corpo = corpo.Replace("@FUNCNOME", nomeFuncionario);
            corpo = corpo.Replace("@FUNCID", idFuncionario.ToString());
            corpo = corpo.Replace("NOMEPRODUTO", nomeProduto);
            corpo = corpo.Replace("IDPRODUTO", idProduto.ToString());
            corpo = corpo.Replace("QTDATUAL", qtdAtual.ToString());
            corpo = corpo.Replace("QTDMINIMA", qtdMinima.ToString());

            Body = corpo;
        }
    }
}
