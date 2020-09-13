using PokerConfiguration.Dominio;
using PokerConfiguration.Dominio.Modelo;
using PokerConfiguration.Infra;
using PokerConfiguration.InterfacesTela;
using PokerConfiguration.Servicos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PokerConfiguration
{
    public partial class FrmTelaDadosEvento : Form
    {
        private EventoAplicacao _eventoAplicacao;
        private static DateTimePicker _timePicker;
        private FrmConfiguracao _frmConfiguracao;
        public FrmTelaDadosEvento(FrmConfiguracao frmConfiguracao)
        {
            InitializeComponent();
            _eventoAplicacao = new EventoAplicacao(this);
            _frmConfiguracao = frmConfiguracao;
            lblBuyIn.Text = Configuracao.QtdEntradas.ToString();
            lblReBuy.Text = Configuracao.QtdRebuy.ToString();
            lblJog.Text = Configuracao.QtdJogadores.ToString();
            lblAddOn.Text = Configuracao.QtdAddon.ToString();
            var blindAtual = Configuracao.RetornaBlindAtual();
            lblAnte.Text = blindAtual?.Ante ??"0";
            lblBig.Text = blindAtual?.BigBlind ?? "0";
            lblSmall.Text = blindAtual?.SmallBlind ?? "0";
        }

        private void FrmTelaDadosEvento_Load(object sender, EventArgs e)
        {
        }

        private void lblNivel_Click(object sender, EventArgs e)
        {

        }
    }
}
