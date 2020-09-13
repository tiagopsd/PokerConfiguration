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
    public class ConfiguracaoBlindsRepositorio : RepositorioBase<ConfiguracaoBlinds, int>, IConfiguracaoBlindsRepositorio
    {
        public ConfiguracaoBlindsRepositorio(DbContexto db) : base(db)
        {
        }

        public int Salvar()
        {
            try
            {
                return Db.SaveChanges();
            }
            catch
            {
                return 0;
            }
        }
    }
}
