using Ninject;
using PokerConfiguration.Dominio.Entidades;
using PokerConfiguration.Dominio.InterfacesRepositorio;
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

namespace PokerConfiguration.InterfacesTela
{
    public partial class FrmTorneios : Form
    {
        [Inject]
        public IConfiguracaoTorneioRepositorio ConfiguracaoTorneioRepositorio { get; set; }

        [Inject]
        public IConfiguracaoAtualRepositorio ConfiguracaoAtualRepositorio { get; set; }

        [Inject]
        public IConfiguracaoBlindsRepositorio ConfiguracaoBlindsRepositorio { get; set; }

        [Inject]
        public IDbContexto Contexto { get; set; }

        [Inject]
        public FrmConfiguracao FrmConfiguracao { get; set; }

        public FrmTorneios()
        {
            InitializeComponent();
        }

        private void gridTorneios_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var coluna = e.RowIndex;
                var col = gridTorneios.Rows[coluna];
                var id = col.Cells[4].Value;
                CarregaConfiguracao(Convert.ToInt32(id));
            }
            catch
            {
                MessageBox.Show("Erro, tente novamente!");
            }
        }

        private void CarregaConfiguracao(int idTorneio)
        {
            var torneio = ConfiguracaoTorneioRepositorio.Filtrar(d => d.Id == idTorneio).FirstOrDefault();
            PreencheDadosTorneio(torneio);
            var configuracaoAplicar = ConfiguracaoAtualRepositorio.Filtrar(d => d.IdTorneio == idTorneio).FirstOrDefault();
            PreencheConfiguracaoAtual(configuracaoAplicar);
            var configuracaoBlinds = ConfiguracaoBlindsRepositorio.Filtrar(d => d.IdTorneio == idTorneio).ToList();
            FrmConfiguracao._blindsAplicacao.AplicaBlinds(configuracaoBlinds);
            FrmConfiguracao.CalculaMedia();
            this.Close();
        }

        private void PreencheConfiguracaoAtual(ConfiguracaoAtual configuracaoAtual)
        {
            if(configuracaoAtual != null)
            {
                Configuracao.Sequencia = configuracaoAtual.Sequencia;
                Configuracao.QtdAddon = configuracaoAtual.QtdAddon;
                Configuracao.QtdEntradas = configuracaoAtual.QtdEntradas;
                Configuracao.QtdJogadores = configuracaoAtual.QtdJogadores;
                Configuracao.QtdRebuy = configuracaoAtual.QtdRebuy;
                Configuracao.RelogioAtual = new TimeSpan(0, configuracaoAtual.Minuto, configuracaoAtual.Segundo);
                AtualizaDadosTela();
            }
        }

        private void AtualizaDadosTela()
        {
            FrmConfiguracao._frmTelaDadosEvento.lblJog.Text = Configuracao.QtdJogadores.ToString();
            FrmConfiguracao._frmTelaDadosEvento.lblBuyIn.Text = Configuracao.QtdEntradas.ToString();
            FrmConfiguracao._frmTelaDadosEvento.lblReBuy.Text = Configuracao.QtdRebuy.ToString();
            FrmConfiguracao._frmTelaDadosEvento.lblAddOn.Text = Configuracao.QtdAddon.ToString();
            FrmConfiguracao.lblJog.Text = Configuracao.QtdJogadores.ToString();
            FrmConfiguracao.lblBuyIn.Text = Configuracao.QtdEntradas.ToString();
            FrmConfiguracao.lblReBuy.Text = Configuracao.QtdRebuy.ToString();
            FrmConfiguracao.lblAddOn.Text = Configuracao.QtdAddon.ToString();
        }

        private void PreencheDadosTorneio(ConfiguracaoTorneio configuracaoTorneio)
        {
            if(configuracaoTorneio != null)
            {
                FrmConfiguracao.txNomeTorneio.Text = configuracaoTorneio.NomeTorneio;
                FrmConfiguracao.txtQtdAddon.Text = configuracaoTorneio.Addon;
                FrmConfiguracao.txtQtdBuyIn.Text = configuracaoTorneio.BuyIn;
                FrmConfiguracao.txtQdtReBuy.Text = configuracaoTorneio.ReBuy;
                FrmConfiguracao.txtTorneioControle.Text = configuracaoTorneio.NomeTorneio;
            }
        }
    }
}
