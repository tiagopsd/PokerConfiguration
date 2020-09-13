using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PokerConfiguration.Infra
{
    public class ThreadHelper
    {
        delegate void SetTextCallback(Form f, Control ctrl, string text);

        public void SetText(Form form, Control ctrl, string text)
        {
            try
            {
                if (ctrl.InvokeRequired)
                {
                    SetTextCallback d = new SetTextCallback(SetText);
                    form.Invoke(d, new object[] { form, ctrl, text });
                }
                else
                {
                    ctrl.Text = text;
                }
            }
            catch
            {
                return;
            }
        }
    }
}
