/*
 * SISTEMA STATUE STORE
 * ETESP @2º SEMESTRE 2018
*/

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
    public partial class formMenu : Form
    {
        public formMenu()
        {
            InitializeComponent();
        }

        private void formMenu_Load(object sender, EventArgs e)
        {
            // Maximiza a janela (fullscreen)
            this.WindowState = FormWindowState.Maximized;
            leftPanel.Height = ClientSize.Height;
        }

        // ---- SIDE MENU BUTTONS ----- // 

        // DropDown buttons 
        private void btnCad_Click(object sender, EventArgs e)
        {
            pnlVisu.Visible = false;

            if (pnlCad.Visible == true)
                pnlCad.Visible = false;
            else
                pnlCad.Visible = true;
        }

        private void btnVisualizar_Click(object sender, EventArgs e)
        {
            pnlCad.Visible = false;

            if (pnlVisu.Visible == false)
                pnlVisu.Visible = true;
            else
                pnlVisu.Visible = false;
        }

        // Botão cadastro de Funcionário
        private void btnCadFunc_Click(object sender, EventArgs e)
        {

            actCad.Visible = true;

            pnlCad.Visible = false;
            pnlVisu.Visible = false;

            displayForm = new FormCadFunc();
            displayForm.MdiParent = this;
            displayForm.Dock = DockStyle.Fill;
            displayForm.FormClosed += new FormClosedEventHandler(DisplayForm_FormClosed);
            displayForm.Show();
        }

        // Botão Cadastro de fornecedor
        private void btnCadFornecedor_Click(object sender, EventArgs e)
        {
            pnlCad.Visible = false;
            pnlVisu.Visible = false;

            displayForm = new FormCadFornecedor();
            displayForm.MdiParent = this;
            displayForm.Dock = DockStyle.Fill;
            displayForm.FormClosed += new FormClosedEventHandler(DisplayForm_FormClosed);
            displayForm.Show();
        }

        // Botão Cadastro Produto
        private void btnCadProduto_Click(object sender, EventArgs e)
        {
            pnlCad.Visible = false;
            pnlVisu.Visible = false;

            displayForm = new FormCadProduto();
            displayForm.MdiParent = this;
            displayForm.Dock = DockStyle.Fill;
            displayForm.FormClosed += new FormClosedEventHandler(DisplayForm_FormClosed);
            displayForm.Show();

        }

        //Botão cadastro grupo
        private void btnCadGrupo_Click(object sender, EventArgs e) {
            pnlCad.Visible = false;
            pnlVisu.Visible = false;

            displayForm.MdiParent = this.
        }

        // handler do evento FormClosed do mdiChilds *forms abertos
        private void DisplayForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            displayForm = null;
            //throw new NotImplementedException();
        }

        private void button1_Click(object sender, EventArgs e) {
            pnlCad.Visible = false;
            pnlVisu.Visible = false;
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
                    pnlVisu.Location = new Point(200, 146);
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
                    pnlVisu.Location = new Point(60, 146);
                    return;
                }
            }
        }

        // Fechando o app
        private void formMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Declarações globais 
        Form displayForm;
        bool menu = false;
    }
}
