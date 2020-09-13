using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerConfiguration.Dominio
{
    public class ConfiguracaoTorneioModelo
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string BuyIn { get; set; }
        public string ReBuy { get; set; }
        public string Addon { get; set; }
    }
}
