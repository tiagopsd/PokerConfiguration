using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerConfiguration.Dominio
{
    public class ConfiguracaoNivelModelo
    {
        public int Id { get; set; }
        public int Sequencia { get; set; }
        public string Level { get; set; }
        public string SmallBlind { get; set; }
        public string BigBlind { get; set; }
        public string Ante { get; set; }
        public string Duracao { get; set; }
    }
}
