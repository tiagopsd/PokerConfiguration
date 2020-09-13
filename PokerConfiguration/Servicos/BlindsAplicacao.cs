using PokerConfiguration.Dominio;
using PokerConfiguration.Dominio.Entidades;
using PokerConfiguration.InterfacesTela;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PokerConfiguration.Servicos
{
    public class BlindsAplicacao
    {
        FrmConfiguracao _frmConfiguracao;
        public static List<ConfiguracaoNivelModelo> ListaBlinds;
        public BlindsAplicacao(FrmConfiguracao frmConfiguracao)
        {
            _frmConfiguracao = frmConfiguracao;
        }

        public List<ConfiguracaoNivelModelo> ObterBlinds()
        {
            int qtdBreak = 0;
            List<ConfiguracaoNivelModelo> listaBlinds = new List<ConfiguracaoNivelModelo>();
            for (int i = 1; i <= 30; i++)
            {
                ConfiguracaoNivelModelo configuracaoNivelModelo = new ConfiguracaoNivelModelo();

                var small = RetornarValorTextBox("txtsmall", i);
                configuracaoNivelModelo.SmallBlind = small;

                var txtBig = RetornarValorTextBox("txtbig", i);
                configuracaoNivelModelo.BigBlind = txtBig;

                var txtAnte = RetornarValorTextBox("txtante", i);
                configuracaoNivelModelo.Ante = txtAnte;

                var txttempo = RetornarValorTextBox("txttempo", i);
                configuracaoNivelModelo.Duracao = txttempo;

                var txtLevel = RetornarValorTextBox("txtlevel", i);
                configuracaoNivelModelo.Level = txtLevel;

                configuracaoNivelModelo.Sequencia = i;

                if (!DeveContinuar(configuracaoNivelModelo.SmallBlind, configuracaoNivelModelo.BigBlind,
                    configuracaoNivelModelo.Ante, configuracaoNivelModelo.Duracao, configuracaoNivelModelo.Level))
                    break;

                listaBlinds.Add(configuracaoNivelModelo);
            }
            return listaBlinds;
        }

        public void AplicaBlinds(List<ConfiguracaoBlinds> configuracaoBlinds)
        {
            foreach (var item in configuracaoBlinds)
            {
                AplicaValorTextBox("txtsmall", item.Sequencia, item.SmallBlind);

                AplicaValorTextBox("txtbig", item.Sequencia, item.BigBlind);

                AplicaValorTextBox("txtante", item.Sequencia, item.Ante);

                AplicaValorTextBox("txttempo", item.Sequencia, item.Duracao);

                AplicaValorTextBox("txtlevel", item.Sequencia, item.Level);
            }
        }

        public void AplicaValorTextBox(string txtName, int numero, string valor)
        {
            var nametxt = (txtName + numero.ToString()).Trim();
            var txt = _frmConfiguracao.tabBlinds.Controls[nametxt] as TextBox;
            if (txt != null)
                txt.Text = valor;
        }

        public string RetornarValorTextBox(string txtName, int numero)
        {
            var nametxt = (txtName + numero.ToString()).Trim();
            var txt = _frmConfiguracao.tabBlinds.Controls[nametxt] as TextBox;
            if (txt != null)
                return txt.Text;
            return "";
        }

        public bool RetornarValorCheckBox(string chkName, int numero)
        {
            var namechk = (chkName + numero.ToString()).Trim();
            var chk = _frmConfiguracao.tabBlinds.Controls[namechk] as CheckBox;
            if (chk != null)
                return chk.Checked;
            return false;
        }


        private bool DeveContinuar(string small, string big, string ante, string tempo, string level)
        {
            bool deve = true;
            if (small == "0" && big == "0" && ante == "0")
                deve = false;
            if (tempo != "0" && !string.IsNullOrWhiteSpace(level))
                deve = true;

            return deve;
        }


    }
}
