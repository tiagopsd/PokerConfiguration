using Ninject;
using PokerConfiguration.Dominio;
using PokerConfiguration.Dominio.Entidades;
using PokerConfiguration.Dominio.InterfacesRepositorio;
using PokerConfiguration.Infra.Config;
using PokerConfiguration.InterfacesTela;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerConfiguration.Servicos
{
    public class TorneioAplicacao
    {
        private FrmConfiguracao _frmConfiguracao;
        private IConfiguracaoAtualRepositorio _configuracaoAtualRepositorio;
        private IConfiguracaoBlindsRepositorio _configuracaoBlindsRepositorio;
        private IConfiguracaoTorneioRepositorio _configuracaoTorneioRepositorio;
        private IDbContexto _contexto;

        public TorneioAplicacao(FrmConfiguracao frmConfiguracao,
            IConfiguracaoAtualRepositorio configuracaoAtualRepositorio,
            IConfiguracaoBlindsRepositorio configuracaoBlindsRepositorio,
            IConfiguracaoTorneioRepositorio configuracaoTorneioRepositorio,
            IDbContexto contexto)
        {
            _configuracaoAtualRepositorio = configuracaoAtualRepositorio;
            _configuracaoBlindsRepositorio = configuracaoBlindsRepositorio;
            _configuracaoTorneioRepositorio = configuracaoTorneioRepositorio;
            _contexto = contexto;
            _frmConfiguracao = frmConfiguracao;
        }

        public ConfiguracaoTorneioModelo ObterTorneio()
        {
            return new ConfiguracaoTorneioModelo
            {
                Addon = _frmConfiguracao.txtQtdAddon.Text,
                BuyIn = _frmConfiguracao.txtQdtReBuy.Text,
                ReBuy = _frmConfiguracao.txtQdtReBuy.Text,
                Nome = _frmConfiguracao.txNomeTorneio.Text
            };
        }

        public void GravaConfiguracao()
        {
            var torneioModel = ObterTorneio();

            var configuracaoTorneio = _configuracaoTorneioRepositorio
                .Filtrar(d => d.NomeTorneio == torneioModel.Nome).FirstOrDefault();
            GravaConfiguracaoTorneio(torneioModel, ref configuracaoTorneio);
            GravaConfiguracaoAtual(configuracaoTorneio);
            GravarConfiguracaoBlinds(configuracaoTorneio);
            var result = _contexto.Salvar();
        }

        private void GravarConfiguracaoBlinds(ConfiguracaoTorneio configuracaoTorneio)
        {
            if (configuracaoTorneio == null)
                return;
            var configuracaoBlinds = _configuracaoBlindsRepositorio.Filtrar(d => d.IdTorneio == configuracaoTorneio.Id).ToList();
            if (configuracaoBlinds.Any())
                configuracaoBlinds.ForEach(d => _configuracaoBlindsRepositorio.Excluir(d));

            var configuracoesBlinds = _frmConfiguracao._blindsAplicacao.ObterBlinds().Select(d => new ConfiguracaoBlinds
            {
                Ante = d.Ante,
                BigBlind = d.BigBlind,
                Duracao = d.Duracao,
                IdTorneio = configuracaoTorneio.Id,
                Level = d.Level,
                Sequencia = d.Sequencia,
                SmallBlind = d.SmallBlind
            }).ToList();
            configuracoesBlinds.ForEach(d => _configuracaoBlindsRepositorio.Cadastrar(d));
        }

        private void GravaConfiguracaoAtual(ConfiguracaoTorneio configuracaoTorneio)
        {
            if (configuracaoTorneio == null)
                return;
            var config = _configuracaoAtualRepositorio.Filtrar(d => d.IdTorneio == configuracaoTorneio.Id).FirstOrDefault();
            if (config != null)
                _configuracaoAtualRepositorio.Excluir(config);

            var configuracaoAtual = new ConfiguracaoAtual
            {
                IdTorneio = configuracaoTorneio.Id,
                Minuto = Configuracao.RelogioAtual.Minutes,
                Segundo = Configuracao.RelogioAtual.Seconds,
                QtdAddon = Configuracao.QtdAddon,
                QtdEntradas = Configuracao.QtdEntradas,
                QtdJogadores = Configuracao.QtdJogadores,
                QtdRebuy = Configuracao.QtdRebuy,
                Sequencia = Configuracao.Sequencia
            };
            _configuracaoAtualRepositorio.Cadastrar(configuracaoAtual);
        }

        public void GravaConfiguracaoTorneio(ConfiguracaoTorneioModelo torneioModel, ref ConfiguracaoTorneio configuracaoTorneio)
        {
            if (configuracaoTorneio == null)
            {
                configuracaoTorneio = new ConfiguracaoTorneio
                {
                    Addon = torneioModel.Addon,
                    BuyIn = torneioModel.BuyIn,
                    NomeTorneio = torneioModel.Nome,
                    ReBuy = torneioModel.ReBuy
                };
                _configuracaoTorneioRepositorio.Cadastrar(configuracaoTorneio);
            }
            else
            {
                configuracaoTorneio.Addon = torneioModel.Addon;
                configuracaoTorneio.BuyIn = torneioModel.BuyIn;
                configuracaoTorneio.NomeTorneio = torneioModel.Nome;
                configuracaoTorneio.ReBuy = torneioModel.ReBuy;
                _configuracaoTorneioRepositorio.Atualizar(configuracaoTorneio);
            }
            var retorno = _contexto.Salvar();
        }

        public List<ConfiguracaoTorneio> ObterTorneiosBanco()
        {
            return _configuracaoTorneioRepositorio.Query().ToList();
        }
        
    }
}
