using PokerConfiguration.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerConfiguration.Dominio.InterfacesRepositorio
{
    public interface IConfiguracaoBlindsRepositorio : IRepositorioBase<ConfiguracaoBlinds, int>
    {
        int Salvar();
    }
}
