using Ninject;
using PokerConfiguration.Dominio.Entidades;
using PokerConfiguration.Dominio.InterfacesRepositorio;
using PokerConfiguration.Infra.Config;
using PokerConfiguration.Infra.Repositorios;
using PokerConfiguration.InterfacesTela;
using PokerConfiguration.Servicos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PokerConfiguration
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var kernel = new StandardKernel(new NinjectModule());
            var form = kernel.Get<FrmConfiguracao>();
            Application.Run(form);
        }
    }
}
