using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BlizzTV
{
    public static class InvokeExtensions
    {
        public static void InvokeHandler(this Control control, MethodInvoker del) // Extension method for sync. invokes.
        {
            if (control.InvokeRequired)
            {
                control.Invoke(del);
                return; 
            }
            else del();
        }

        public static void AsyncInvokeHandler(this Control control, MethodInvoker del) // Extension method for asyc. invokes.
        {
            if (control.InvokeRequired)
            {
                control.BeginInvoke(del);
                return; 
            }
            else del(); 
        }
    }
}
