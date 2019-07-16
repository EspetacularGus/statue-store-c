/*
 * SISTEMA STATUE STORE
 * ETESP @2º SEMESTRE 2018
*/

using System;
using System.Drawing;
using System.Windows.Forms;
using StatueStore.FormsCadastro;
using StatueStore.FormsVisualizar;
using StatueStore.FormsSistema;

namespace StatueStore
{
    public partial class formMenu : Form
    {
        public formMenu(int Acess = 0, int id=0)
        {
            InitializeComponent();

            //Seta o id do funcionario logado para registro de logs...
            if(id != 0) idFuncionario = id;

            idNivelAcessoFuncionario = Acess;

            //Form menu muda de acordo com o nivel de acesso.
            switch(Acess){
                case 2:
                    btnCad.Enabled = false;
                    btnVisualizar.Enabled = false;
                    break;

                case 3:
                    BtnCadFuncionario.Enabled = false;
                    break;

                case 4:
                    //Gerenciador possui todos os acessos
                    break;
            }
        }

        private void formMenu_Load(object sender, EventArgs e)
        {
            // Maximiza a janela (fullscreen)
            this.WindowState = FormWindowState.Maximized;
            leftPanel.Height = ClientSize.Height;
            btnUserInfo.PerformClick();
        }

        // ---- SIDE MENU BUTTONS ----- // 

        // DropDown buttons 
        private void btnCad_Click(object sender, EventArgs e)
        {

            if (pnlCad.Visible == true)
                pnlCad.Visible = false;
            else
                pnlCad.Visible = true;
        }

        private void btnVisualizar_Click(object sender, EventArgs e)
        {
            displayForm = new FormVisualizar(idNivelAcessoFuncionario, idFuncionario);
            display('v');
        }

        // Botão cadastro de Funcionário
        private void btnCadFunc_Click(object sender, EventArgs e)
        {
            displayForm = new FormCadFunc(idFuncionario);
            display('c');
        }

        // Botão Cadastro de fornecedor
        private void btnCadFornecedor_Click(object sender, EventArgs e)
        {
            displayForm = new FormCadFornecedor(idFuncionario);
            display('c');
        }

        // Botão Cadastro Produto
        private void btnCadProduto_Click(object sender, EventArgs e)
        {
            displayForm = new FormCadProduto(idFuncionario);
            display('c');
        }

        //Botão cadastro Subgrupo
        private void btnCadSubgrupo_Click(object sender, EventArgs e) {
            displayForm = new FormCadSubGrupo(idFuncionario);
            display('c');
        }

        //Botão cadastro grupo
        private void btnCadGrupo_Click(object sender, EventArgs e) {
            displayForm = new FormCadGrupo(false, idFuncionario);
            display('c');
        }
        private void btnVisualizarCadastros_Click(object sender, EventArgs e) {
            displayForm = new FormVisualizar(idNivelAcessoFuncionario, idFuncionario);
            display('v');
        }

        //BOTÃO DE INFO DO USUARIO

        private void btnUserInfo_Click(object sender, EventArgs e) {
            displayUserInfoForm = new formUserInfo(idFuncionario, idNivelAcessoFuncionario);
            pnlCad.Visible = false;
            displayUserInfoForm.MdiParent = this;
            displayUserInfoForm.Dock = DockStyle.Fill;
            displayUserInfoForm.FormClosed += new FormClosedEventHandler(DisplayForm_FormClosed);
            displayUserInfoForm.Show();

            pnlActFunc.Visible = true;
            actCad.Visible = false;
            actVisu.Visible = false;
            pnlActEstoque.Visible = false;
        }

        // handler do evento FormClosed do mdiChilds *forms abertos
        private void DisplayForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            displayForm = null;
            //throw new NotImplementedException();
        }

        private void button1_Click(object sender, EventArgs e) {
            pnlCad.Visible = false;
            //pnlVisu.Visible = false;
            tmrMenu.Start();
        }

        private void tmrMenu_Tick(object sender, EventArgs e) {
            //verifica se o menu esta aberto ou fechado
            if(menu == false) {
                //expandindo
                leftPanel.Width += 20;
                if(leftPanel.Width == 200) {
                    tmrMenu.Stop();
                    menu = true;
                    // mudando os dropdowns de posição
                    pnlCad.Location = new Point(200, 106);
                    //pnlVisu.Location = new Point(200, 146);
                    return;
                }
            }
            else {
                //Diminuindo
                leftPanel.Width -= 20;
                if(leftPanel.Width == 60) {
                    tmrMenu.Stop();
                    menu = false;
                    pnlCad.Location = new Point(60, 106);
                    //pnlVisu.Location = new Point(60, 146);
                    return;
                }
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //Funções
        public void displaySecondary() {

        }

        void display(char tipo) {
            switch (tipo) {
                case 'c':
                    pnlActFunc.Visible = false;
                    actCad.Visible = true;
                    actVisu.Visible = false;
                    pnlActEstoque.Visible = false;
                    ActPedidos.Visible = false;
                    break;

                case 'v':
                    pnlActFunc.Visible = false;
                    actCad.Visible = false;
                    actVisu.Visible = true;
                    pnlActEstoque.Visible = false;
                    ActPedidos.Visible = false;
                    break;

                case 'e':
                    pnlActFunc.Visible = false;
                    actCad.Visible = false;
                    actVisu.Visible = false;
                    pnlActEstoque.Visible = true;
                    ActPedidos.Visible = false;
                    break;

                case 'p':
                    pnlActFunc.Visible = false;
                    actCad.Visible = false;
                    actVisu.Visible = false;
                    pnlActEstoque.Visible = false;
                    ActPedidos.Visible = true;
                    break;

            }
            pnlActFunc.Visible = false;

            pnlCad.Visible = false;
            //pnlVisu.Visible = false;
            displayForm.MdiParent = this;
            displayForm.Dock = DockStyle.Fill;
            displayForm.FormClosed += new FormClosedEventHandler(DisplayForm_FormClosed);
            displayForm.Show();
        }

        // Declarações globais
        int idFuncionario;
        int idNivelAcessoFuncionario;
        Form displayForm;
        Form displayUserInfoForm;
        bool menu = false;

        private void button2_Click(object sender, EventArgs e) {
            displayForm = new FormEstoque();
            display('e');
        }

        private void btnPedidos_Click(object sender, EventArgs e) {
            displayForm = new FormVisualizarPedidos();
            display('p');
        }
    }
}
