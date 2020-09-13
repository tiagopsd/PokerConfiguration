using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokerConfiguration.Dominio;

namespace PokerConfiguration
{
    public class VoiceFactory
    {
        internal static string ObterMensagem(TipoMensagemEnum tipoMensagem)
        {
            switch (tipoMensagem)
            {
                case TipoMensagemEnum.BemVindos:
                    return "Bem vindos!";
                default:
                    return "";
            }
        }
    }
}
