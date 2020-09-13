using System;

namespace PokerConfiguration.Dominio.Entidades
{
    public class ConfiguracaoTorneio : Entidade<int>
    {
        public string NomeTorneio { get; set; }
        public string BuyIn { get; set; }
        public string ReBuy { get; set; }
        public string Addon { get; set; }
    }
}
