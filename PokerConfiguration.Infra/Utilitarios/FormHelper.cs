using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PokerConfiguration.Infra
{
    public class FormHelper
    {
        public List<T> RetornaFormsParent<T>(Control parent) where T : class
        {
            var myCtrls = new List<T>();

            foreach (Control ctrl in parent.Controls)
            {
                if (ctrl.GetType() == typeof(T) || ctrl.GetType().IsInstanceOfType(typeof(T)))
                {
                    myCtrls.Add(ctrl as T);
                }
                else if (ctrl.HasChildren)
                {
                    var childs = RetornaFormsParent<T>(ctrl);
                    if (childs.Any()) myCtrls.AddRange(childs);
                }
            }
            return myCtrls;
        }
    }
}
