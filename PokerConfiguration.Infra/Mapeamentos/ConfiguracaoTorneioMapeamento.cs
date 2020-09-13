using PokerConfiguration.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerConfiguration.Infra.Mapeamentos
{
    public class ConfiguracaoTorneioMapeamento : EntityTypeConfiguration<ConfiguracaoTorneio>
    {
        public ConfiguracaoTorneioMapeamento()
        {
            ToTable("ConfiguracaoTorneio");
            HasKey(d => d.Id);
        }
    }
}
