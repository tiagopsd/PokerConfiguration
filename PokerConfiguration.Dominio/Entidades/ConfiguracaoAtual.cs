using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerConfiguration.Dominio.Entidades
{
    public class ConfiguracaoAtual : Entidade<int>
    {
        public short QtdEntradas { get; set; }
        public short QtdJogadores { get; set; }
        public short QtdRebuy { get; set; }
        public short QtdAddon { get; set; }
        public int Minuto { get; set; }
        public int Segundo { get; set; }
        public int Sequencia { get; set; }
        public int IdTorneio { get; set; }
    }
}
