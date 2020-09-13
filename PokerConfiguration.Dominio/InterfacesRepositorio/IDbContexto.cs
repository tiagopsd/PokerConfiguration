using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerConfiguration.Dominio.InterfacesRepositorio
{
    public interface IDbContexto : IDisposable
    {
        int Salvar();
    }
}
