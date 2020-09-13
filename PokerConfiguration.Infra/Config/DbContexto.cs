using PokerConfiguration.Dominio.Entidades;
using PokerConfiguration.Infra.Mapeamentos;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerConfiguration.Infra.Config
{
    public class DbContexto : DbContext
    {
        public DbContexto() :
          base("Default")
        {
            Database.SetInitializer<DbContexto>(null);
        }

        protected override void OnModelCreating(DbModelBuilder dbModelBuilder)
        {
            Mapeamentos(dbModelBuilder);
            dbModelBuilder.HasDefaultSchema("dbo");
        }

        public int Salvar()
        {
            try
            {
                var linhasAfetadas = SaveChanges();
                return linhasAfetadas;
            }
            catch
            {
                return 0;
            }
        }

        public void FecharConexao()
        {
            Database.Connection.Close();
            Dispose();
        }

        private void Mapeamentos(DbModelBuilder dbModelBuilder)
        {
            dbModelBuilder.Configurations.Add(new ConfiguracaoTorneioMapeamento());
            dbModelBuilder.Configurations.Add(new ConfiguracaoAtualMapeamento());
            dbModelBuilder.Configurations.Add(new ConfiguracaoBlindsMapeamento());
        }
    }
}
