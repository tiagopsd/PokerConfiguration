using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerConfiguration.Dominio.Entidades
{
    public class ConfiguracaoBlinds : Entidade<int>
    {
        public int Sequencia { get; set; }
        public string Level { get; set; }
        public string SmallBlind { get; set; }
        public string BigBlind { get; set; }
        public string Ante { get; set; }
        public string Duracao { get; set; }
        public int IdTorneio { get; set; }
    }
}
