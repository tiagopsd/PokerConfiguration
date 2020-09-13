using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerConfiguration.Infra
{
    public class RelogioCentral
    {
        public static DateTime RetornaHoras(string MinutosInicial, string SegundosInicial)
        {
            int _minutosInicial = Convert.ToInt32(MinutosInicial);
            int _segundosInicial = Convert.ToInt32(SegundosInicial);
            var timeSpan = new TimeSpan(0, _minutosInicial, _segundosInicial);
            return DateTime.Now.Add(timeSpan);
        }
    }
}
