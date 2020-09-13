using PokerConfiguration.Dominio.Entidades;
using PokerConfiguration.Dominio.InterfacesRepositorio;
using PokerConfiguration.Infra.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerConfiguration.Infra.Repositorios
{
    public class ConfiguracaoTorneioRepositorio : RepositorioBase<ConfiguracaoTorneio, int>, IConfiguracaoTorneioRepositorio
    {
        public ConfiguracaoTorneioRepositorio(DbContexto db) : base(db)
        {
        }
    }
}
