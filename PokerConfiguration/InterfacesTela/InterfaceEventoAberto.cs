using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using PokerConfiguration.Infra;

namespace PokerConfiguration
{
    public partial class InterfaceEventoAberto : UserControl
    {
        private static FrmTelaDadosEvento _parentForm;
        ThreadHelper _threadHelper = new ThreadHelper();

        public InterfaceEventoAberto(FrmTelaDadosEvento parentForm)
        {
            InitializeComponent();
            _parentForm = parentForm;
        }
    }
}
