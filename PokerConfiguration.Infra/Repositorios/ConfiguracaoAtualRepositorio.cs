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
    public class ConfiguracaoAtualRepositorio : RepositorioBase<ConfiguracaoAtual, int>, IConfiguracaoAtualRepositorio
    {
        public ConfiguracaoAtualRepositorio(DbContexto db) : base(db)
        {
        }
    }
}
