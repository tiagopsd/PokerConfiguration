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
    public class ContextoRepositorio : RepositorioBase<Entidade<int>, int>, IDbContexto
    {
        public ContextoRepositorio(DbContexto db) : base(db)
        {
        }

        public void Dispose() => Db.Dispose();

        public int Salvar() => Db.Salvar();

        public void FecharConexao() => Db.FecharConexao();
    }
}
