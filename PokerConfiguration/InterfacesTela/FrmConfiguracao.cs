using Ninject;
using PokerConfiguration.Dominio;
using PokerConfiguration.Dominio.InterfacesRepositorio;
using PokerConfiguration.Dominio.Modelo;
using PokerConfiguration.Infra;
using PokerConfiguration.Servicos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PokerConfiguration.InterfacesTela
{
    public partial class FrmConfiguracao : Form
    {
        public FrmTelaDadosEvento _frmTelaDadosEvento;
        public BlindsAplicacao _blindsAplicacao;
        private VoiceAplicacao _voice;
        public TorneioAplicacao _torneioAplicacao;

        [Inject]
        public IConfiguracaoTorneioRepositorio ConfiguracaoTorneioRepositorio { get; set; }

        [Inject]
        public IConfiguracaoAtualRepositorio ConfiguracaoAtualRepositorio { get; set; }

        [Inject]
        public IConfiguracaoBlindsRepositorio ConfiguracaoBlindsRepositorio { get; set; }

        [Inject]
        public IDbContexto Contexto { get; set; }

        [Inject]
        public FrmTorneios FrmTorneios { get; set; }


        public FrmConfiguracao()
        {
            InitializeComponent();
        }

        private void FrmConfiguracao_Load(object sender, EventArgs e)
        {
            _torneioAplicacao = new TorneioAplicacao(this, ConfiguracaoAtualRepositorio, ConfiguracaoBlindsRepositorio, ConfiguracaoTorneioRepositorio, Contexto);
            _blindsAplicacao = new BlindsAplicacao(this);
            _frmTelaDadosEvento = new FrmTelaDadosEvento(this);
            _voice = new VoiceAplicacao();
            Configuracao.Sequencia = 1;
            lblNivel.Text = "1";
            Configuracao.RelogioAtual = new TimeSpan(0, 20, 0);
            AtualizaConfiguracao();
            CriaRelogio();
        }

        private void btnAbreEvento_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTorneioControle.Text))
            {
                MessageBox.Show("Favor digite o nome do Torneio!");
                return;
            }


            AtualizaConfiguracao();
            if (Application.OpenForms.OfType<FrmTelaDadosEvento>().Count() > 0)
                MessageBox.Show("Já encontra-se aberto");
            else if (Configuracao.ConfiguracaoNivelModelos.Count <= 0)
                MessageBox.Show("Favor configurar os blinds antes de abrir!");
            var torneioModelo = _torneioAplicacao.ObterTorneio();
            if (string.IsNullOrWhiteSpace(torneioModelo.Addon) && string.IsNullOrWhiteSpace(torneioModelo.BuyIn) && string.IsNullOrWhiteSpace(torneioModelo.ReBuy))
                MessageBox.Show("Favor preencher os dados do torneio!");
            else if (torneioModelo.Addon == "0" && torneioModelo.BuyIn == "0" && torneioModelo.ReBuy == "0")
                MessageBox.Show("Favor preencher os dados do torneio!");
            else
            {
                _frmTelaDadosEvento = new FrmTelaDadosEvento(this);
                _frmTelaDadosEvento.lblTorneioEvento.Text = txtTorneioControle.Text;
                _frmTelaDadosEvento.lblNomeNivel.Text = lblNivel.Text;
                _frmTelaDadosEvento.Show();
                CalculaMedia();
            }
        }

        private void btnFechaEvento_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<FrmTelaDadosEvento>().Count() > 0)
                _frmTelaDadosEvento.Close();
        }

        private void AtualizaConfiguracao()
        {
            Configuracao.ConfiguracaoNivelModelos = _blindsAplicacao.ObterBlinds();
            Configuracao.ConfiguracaoTorneioModelo = _torneioAplicacao.ObterTorneio();

            Configuracao.RelogioAtual = Configuracao.RetornaTempoBlindAtual();
            if (Configuracao.RelogioAtual.Minutes <= 0 && Configuracao.RelogioAtual.Seconds <= 0)
            {
                MessageBox.Show("Tempo negativo sera reiniciado!");
                Configuracao.RelogioAtual = Configuracao.RetornaTempoBlindInicial();
            }
            Configuracao.Som = chkSom.Checked;

            if (Configuracao.ConfiguracaoNivelModelos.Count > 0 && Configuracao.ConfiguracaoNivelModelos.Count >= Configuracao.Sequencia)
            {
                var blindAtual = Configuracao.RetornaBlindAtual();
                Configuracao.BlindAtual = blindAtual.Level;
                Configuracao.Sequencia = blindAtual.Sequencia;
                _frmTelaDadosEvento.lblAnte.Text = blindAtual.Ante;
                _frmTelaDadosEvento.lblBig.Text = blindAtual.BigBlind;
                _frmTelaDadosEvento.lblSmall.Text = blindAtual.SmallBlind;
                lblNivel.Text = Configuracao.BlindAtual;
                _frmTelaDadosEvento.lblNomeNivel.Text = lblNivel.Text;
            }
        }



        private void btnEnviarSom_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txbTextoSom.Text))
                _voice.SintetizarVoz(txbTextoSom.Text);
        }

        private void btnZerarClock_Click(object sender, EventArgs e)
        {
            if(_blindsAplicacao.ObterBlinds().Count <= 0)
            {
                MessageBox.Show("Favor configurar blinds!");
                return;
            }
            var result = MessageBox.Show("Tem certeza que deseja reiniciar os blinds", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.No)
                return;


            Configuracao.Relogio.Stop();
            Configuracao.Sequencia = 1;
            Configuracao.RelogioAtual = Configuracao.RetornaTempoBlindInicial();
            var tempoAtualizado = Configuracao.RelogioAtual.ToString().Replace("00:", "");
            textBoxClockRetaguarda.Text = tempoAtualizado;
            _frmTelaDadosEvento.txbClock.Text = tempoAtualizado;
            AtualizaConfiguracao();
            if (Configuracao.RelogioAtual == new TimeSpan(0, 0, 0))
                MessageBox.Show("Favor configurar os blinds!");
            else
                Configuracao.Relogio.Start();
        }

        private void btnPausarClock_Click(object sender, EventArgs e)
        {
            Configuracao.Processando = false;
            Configuracao.Relogio.Stop();
        }

        private void CriaRelogio()
        {
            Configuracao.Relogio = new System.Windows.Forms.Timer();
            Configuracao.Relogio.Interval = 1000;
            TimeSpan segundo = new TimeSpan(0, 0, 1);
            Configuracao.Relogio.Tick += delegate
            {
                if (Configuracao.RelogioAtual.Seconds <= -3 && Configuracao.RelogioAtual.Minutes <= 0)
                    Configuracao.Relogio.Stop();

                Configuracao.RelogioAtual = Configuracao.RelogioAtual.Subtract(segundo);
                var tempoAtualizado = Configuracao.RelogioAtual.ToString().Replace("00:", "");
                textBoxClockRetaguarda.Text = tempoAtualizado;
                _frmTelaDadosEvento.txbClock.Text = tempoAtualizado;
                if (Configuracao.RelogioAtual.Seconds <= 3 && Configuracao.RelogioAtual.Minutes <= 0)
                    Console.Beep();
                if (Configuracao.RelogioAtual.Seconds == 0 && Configuracao.RelogioAtual.Minutes == 0)
                {
                    Configuracao.Sequencia++;
                    AtualizaConfiguracao();
                    Task.Run(() => SomSubindoNivel(Configuracao.Sequencia));
                }
            };
        }

        private void SomSubindoNivel(int nivel)
        {
            if (Configuracao.Som)
            {
                try
                {
                    var blindaAtual = Configuracao.RetornaBlindAtual();
                    if (!string.IsNullOrWhiteSpace(blindaAtual.Level))
                    {
                        if (blindaAtual.Level.ToLower().Contains("intervalo") || blindaAtual.Level.ToLower().Contains("break"))
                        {
                            _voice.SintetizarVoz($"Atenção, Mais três mãos para o Intervalo!");
                        }
                        else
                        {
                            var nivelAtual = Configuracao.ConfiguracaoNivelModelos.Where(d => d.Sequencia == nivel).FirstOrDefault();
                            if (nivelAtual.Ante != "0")
                                _voice.SintetizarVoz($"Atenção, na próxima mão, blinds {nivelAtual.SmallBlind} {nivelAtual.BigBlind}, com ante de {nivelAtual.Ante}");
                            else
                                _voice.SintetizarVoz($"Atenção, Na próxima mão, blinds {nivelAtual.SmallBlind} {nivelAtual.BigBlind}");
                        }
                    }
                    else
                    {
                        Configuracao.Relogio.Stop();
                        MessageBox.Show("Favor configurar mais Blinds!");
                    }
                }
                catch (Exception erro)
                {
                    Configuracao.Relogio.Stop();
                    MessageBox.Show("Favor configurar mais Blinds!");
                }
            }
        }

        private void btnContinuaClock_Click(object sender, EventArgs e)
        {
            AtualizaConfiguracao();
            if (Configuracao.RelogioAtual <= new TimeSpan(0, 0, 0))
                MessageBox.Show("Favor configurar os blinds!");
            else
            {
                Configuracao.Processando = true;
                Configuracao.Relogio.Start();
            }
        }

        private void btnSobeNivel_Click(object sender, EventArgs e)
        {
            var blindAtual = Configuracao.RetornaBlindAtual();
            Configuracao.Sequencia++;
            if (Configuracao.Sequencia > _blindsAplicacao.ObterBlinds().Count)
            {
                Configuracao.Sequencia--;
                MessageBox.Show("Favor configurar próximo blind!");
                Configuracao.Relogio.Stop();
                return;
            }
            Configuracao.RelogioAtual = Configuracao.RetornaTempoBlindInicial();
            AtualizaConfiguracao();
        }

        private void btnDesceNivel_Click(object sender, EventArgs e)
        {
            var blindAtual = Configuracao.RetornaBlindAtual();
            Configuracao.Sequencia--;
            if (Configuracao.Sequencia <= 0)
            {
                Configuracao.Sequencia = 1;
                MessageBox.Show("Blind mínimo é 1!");
                return;
            }
            Configuracao.RelogioAtual = Configuracao.RetornaTempoBlindInicial();
            AtualizaConfiguracao();
        }

        private void btnUmMin_Click(object sender, EventArgs e)
        {
            var span = new TimeSpan(0, 1, 0);
            Configuracao.RelogioAtual = Configuracao.RelogioAtual.Add(span);
            textBoxClockRetaguarda.Text = Configuracao.RelogioAtual.ToString().Replace("00:", "");
            _frmTelaDadosEvento.txbClock.Text = textBoxClockRetaguarda.Text;
        }

        private void btnMenosMin_Click(object sender, EventArgs e)
        {
            var span = new TimeSpan(0, 1, 0);
            Configuracao.RelogioAtual = Configuracao.RelogioAtual.Subtract(span);
            textBoxClockRetaguarda.Text = Configuracao.RelogioAtual.ToString().Replace("00:", "");
            _frmTelaDadosEvento.txbClock.Text = textBoxClockRetaguarda.Text;
        }

        private void btnDezMin_Click(object sender, EventArgs e)
        {
            var span = new TimeSpan(0, 10, 0);
            Configuracao.RelogioAtual = Configuracao.RelogioAtual.Add(span);
            textBoxClockRetaguarda.Text = Configuracao.RelogioAtual.ToString().Replace("00:", "");
            _frmTelaDadosEvento.txbClock.Text = textBoxClockRetaguarda.Text;
        }

        private void btnMenosDezMin_Click(object sender, EventArgs e)
        {
            var span = new TimeSpan(0, 10, 0);
            Configuracao.RelogioAtual = Configuracao.RelogioAtual.Subtract(span);
            textBoxClockRetaguarda.Text = Configuracao.RelogioAtual.ToString().Replace("00:", "");
            _frmTelaDadosEvento.txbClock.Text = textBoxClockRetaguarda.Text;
        }

        private void btnDezSeg_Click(object sender, EventArgs e)
        {
            var span = new TimeSpan(0, 0, 10);
            Configuracao.RelogioAtual = Configuracao.RelogioAtual.Add(span);
            textBoxClockRetaguarda.Text = Configuracao.RelogioAtual.ToString().Replace("00:", "");
            _frmTelaDadosEvento.txbClock.Text = textBoxClockRetaguarda.Text;
        }

        private void btnMenosDezSeg_Click(object sender, EventArgs e)
        {

            var span = new TimeSpan(0, 0, 10);
            Configuracao.RelogioAtual = Configuracao.RelogioAtual.Subtract(span);
            textBoxClockRetaguarda.Text = Configuracao.RelogioAtual.ToString().Replace("00:", "");
            _frmTelaDadosEvento.txbClock.Text = textBoxClockRetaguarda.Text;
        }

        private void btnTrintaSeg_Click(object sender, EventArgs e)
        {
            var span = new TimeSpan(0, 0, 30);
            Configuracao.RelogioAtual = Configuracao.RelogioAtual.Add(span);
            textBoxClockRetaguarda.Text = Configuracao.RelogioAtual.ToString().Replace("00:", "");
            _frmTelaDadosEvento.txbClock.Text = textBoxClockRetaguarda.Text;
        }

        private void btnMenosTrintaSeg_Click(object sender, EventArgs e)
        {
            var span = new TimeSpan(0, 0, 30);
            Configuracao.RelogioAtual = Configuracao.RelogioAtual.Subtract(span);
            textBoxClockRetaguarda.Text = Configuracao.RelogioAtual.ToString().Replace("00:", "");
            _frmTelaDadosEvento.txbClock.Text = textBoxClockRetaguarda.Text;
        }

        private void btnMaisBuyIn_Click(object sender, EventArgs e)
        {
            Configuracao.QtdEntradas++;
            lblBuyIn.Text = Configuracao.QtdEntradas.ToString();
            _frmTelaDadosEvento.lblBuyIn.Text = lblBuyIn.Text;
        }

        private void btnMenosBuyIn_Click(object sender, EventArgs e)
        {
            Configuracao.QtdEntradas--;
            lblBuyIn.Text = Configuracao.QtdEntradas.ToString();
            _frmTelaDadosEvento.lblBuyIn.Text = lblBuyIn.Text;
        }

        private void btnMaisEnt_Click(object sender, EventArgs e)
        {
        }

        private void btnMenosEnt_Click(object sender, EventArgs e)
        {

        }

        private void btnMaisReBuy_Click(object sender, EventArgs e)
        {
            Configuracao.QtdRebuy++;
            lblReBuy.Text = Configuracao.QtdRebuy.ToString();
            _frmTelaDadosEvento.lblReBuy.Text = lblReBuy.Text;
        }

        private void btnMenosReBuy_Click(object sender, EventArgs e)
        {
            Configuracao.QtdRebuy--;
            lblReBuy.Text = Configuracao.QtdRebuy.ToString();
            _frmTelaDadosEvento.lblReBuy.Text = lblReBuy.Text;
        }

        private void btnMaisJog_Click(object sender, EventArgs e)
        {
            Configuracao.QtdJogadores++;
            lblJog.Text = Configuracao.QtdJogadores.ToString();
            _frmTelaDadosEvento.lblJog.Text = lblJog.Text;
            CalculaMedia();
        }

        private void btnMenosJog_Click(object sender, EventArgs e)
        {
            Configuracao.QtdJogadores--;
            lblJog.Text = Configuracao.QtdJogadores.ToString();
            _frmTelaDadosEvento.lblJog.Text = lblJog.Text;
            CalculaMedia();
        }

        private void btnMaisAddon_Click(object sender, EventArgs e)
        {
            Configuracao.QtdAddon++;
            lblAddOn.Text = Configuracao.QtdAddon.ToString();
            _frmTelaDadosEvento.lblAddOn.Text = lblAddOn.Text;
        }

        private void btnMenosAddon_Click(object sender, EventArgs e)
        {
            Configuracao.QtdAddon--;
            lblAddOn.Text = Configuracao.QtdAddon.ToString();
            _frmTelaDadosEvento.lblAddOn.Text = lblAddOn.Text;
        }

        public void CalculaMedia()
        {
            try
            {
                var dadosTorneio = _torneioAplicacao.ObterTorneio();
                var qtdFichasBuyIN = Configuracao.QtdEntradas * decimal.Parse(dadosTorneio.BuyIn ?? "0");
                var qtdFichasReBuy = Configuracao.QtdRebuy * decimal.Parse(dadosTorneio.ReBuy ?? "0");
                var qtdFichasAddon = Configuracao.QtdAddon * decimal.Parse(dadosTorneio.Addon ?? "0");
                var totalFichas = qtdFichasBuyIN + qtdFichasReBuy + qtdFichasAddon;
                var media = totalFichas / Configuracao.QtdJogadores;
                _frmTelaDadosEvento.lblMedia.Text = (Convert.ToInt32(media)).ToString();
                _frmTelaDadosEvento.lblChip.Text = totalFichas.ToString();
            }
            catch
            {
            }

        }

        private void btnSalvarTorneio_Click(object sender, EventArgs e)
        {
            _torneioAplicacao.GravaConfiguracao();
        }

        private void btnBuscarTorneios_Click(object sender, EventArgs e)
        {
            try
            {
                FrmTorneios.gridTorneios.DataSource = _torneioAplicacao.ObterTorneiosBanco();
                FrmTorneios.ShowDialog();
            }
            catch
            {
            }
        }


    }
}
