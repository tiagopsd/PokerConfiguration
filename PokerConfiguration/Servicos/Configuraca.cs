using PokerConfiguration.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PokerConfiguration.Servicos
{
    public static class Configuracao
    {
        public static List<ConfiguracaoNivelModelo> ConfiguracaoNivelModelos { get; set; }
        public static ConfiguracaoTorneioModelo ConfiguracaoTorneioModelo { get; set; }
        public static TimeSpan RelogioAtual { get; set; }
        public static bool Processando { get; set; }
        public static Timer Relogio { get; set; }
        public static string BlindAtual { get; set; }
        public static int Sequencia { get; set; }
        public static bool Som { get; set; }

        public static short QtdEntradas { get; set; }
        public static short QtdJogadores { get; set; }
        public static short QtdRebuy { get; set; }
        public static short QtdAddon { get; set; }

        public static TimeSpan RetornaTempoBlindAtual()
        {
            try
            {
                if (RelogioAtual != null && RelogioAtual != new TimeSpan(0, 0, 0))
                    return RelogioAtual;

                if (ConfiguracaoNivelModelos.Any() && Sequencia <= ConfiguracaoNivelModelos.Count)
                    return new TimeSpan(0, int.Parse(Configuracao.ConfiguracaoNivelModelos
                        .Where(d => d.Sequencia == Sequencia)
                    .Select(d => d.Duracao).FirstOrDefault()), 0);
            }
            catch
            {
            }
            return new TimeSpan(0, 0, 0);
        }

        public static TimeSpan RetornaTempoBlindInicial()
        {
            try
            {
                if (!Configuracao.ConfiguracaoNivelModelos.Any())
                    return new TimeSpan(0, 0, 0);

                   return new TimeSpan(0, Convert.ToInt32(Configuracao.ConfiguracaoNivelModelos
                   .Where(d => d.Sequencia == Sequencia)
                   .FirstOrDefault()?.Duracao ?? "0"), 0);
            }
            catch (Exception erro)
            {
                MessageBox.Show("Favor configurar próximo blind!");
                return new TimeSpan(0, 0, 0);
            }

        }

        public static ConfiguracaoNivelModelo RetornaBlindAtual()
        {
            try
            {
                if (!Configuracao.ConfiguracaoNivelModelos.Any())
                    return new ConfiguracaoNivelModelo();

                return Configuracao.ConfiguracaoNivelModelos
                    .Where(d => d.Sequencia == Sequencia).FirstOrDefault();
            }
            catch (Exception erro)
            {
                return new ConfiguracaoNivelModelo();
            }
        }

    }
}
