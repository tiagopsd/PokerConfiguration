using Ninject;
using Ninject.Modules;
using PokerConfiguration.Dominio.Entidades;
using PokerConfiguration.Dominio.InterfacesRepositorio;
using PokerConfiguration.Infra.Config;
using PokerConfiguration.Infra.Repositorios;
using PokerConfiguration.InterfacesTela;
using PokerConfiguration.Servicos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerConfiguration
{
    public class NinjectModule : INinjectModule
    {
        public string Name => "NinjectModule";

        public IKernel Kernel => this.Kernel;

        public void OnLoad(IKernel kernel)
        {
            kernel.Bind<IRepositorioBase<Entidade<int>, int>>().To<RepositorioBase<Entidade<int>, int>>().InSingletonScope();
            kernel.Bind<IDbContexto>().To<ContextoRepositorio>().InSingletonScope();
            kernel.Bind<IConfiguracaoTorneioRepositorio>().To<ConfiguracaoTorneioRepositorio>().InSingletonScope();
            kernel.Bind<IConfiguracaoBlindsRepositorio>().To<ConfiguracaoBlindsRepositorio>().InSingletonScope();
            kernel.Bind<IConfiguracaoAtualRepositorio>().To<ConfiguracaoAtualRepositorio>().InSingletonScope();
            kernel.Bind<TorneioAplicacao>().To<TorneioAplicacao>().InSingletonScope();
            kernel.Bind<DbContexto>().To<DbContexto>().InSingletonScope();
            kernel.Bind<FrmConfiguracao>().To<FrmConfiguracao>().InSingletonScope();
        }

        public void OnUnload(IKernel kernel)
        {
            kernel.Bind<IRepositorioBase<Entidade<int>, int>>().To<RepositorioBase<Entidade<int>, int>>().InSingletonScope();
            kernel.Bind<IDbContexto>().To<ContextoRepositorio>().InSingletonScope();
            kernel.Bind<IConfiguracaoTorneioRepositorio>().To<ConfiguracaoTorneioRepositorio>().InSingletonScope();
            kernel.Bind<IConfiguracaoBlindsRepositorio>().To<ConfiguracaoBlindsRepositorio>().InSingletonScope();
            kernel.Bind<IConfiguracaoAtualRepositorio>().To<ConfiguracaoAtualRepositorio>().InSingletonScope();
            kernel.Bind<TorneioAplicacao>().To<TorneioAplicacao>().InSingletonScope();
            kernel.Bind<DbContexto>().To<DbContexto>().InSingletonScope();
            kernel.Bind<FrmConfiguracao>().To<FrmConfiguracao>().InSingletonScope();
        }

        public void OnVerifyRequiredModules()
        {
        }
    }
}
