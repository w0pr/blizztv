using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibBlizzTV.Utils;

namespace BlizzTV.Utils
{
    public class DependencyChecker:IDisposable
    {
        private bool disposed = false;

        public DependencyChecker()
        {
            if (!this.CheckShockwaveFlash())
            {
                System.Windows.Forms.DialogResult result = System.Windows.Forms.MessageBox.Show("BlizzTV requires Abode Flash Player for internal video & stream playing and your system does not satisfy it. Do you want to install latest Adobe Flash Player now?", "Adobe Flash Player Missing!", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Error);
                if (result == System.Windows.Forms.DialogResult.Yes) System.Diagnostics.Process.Start("IExplore.exe", "http://get.adobe.com/flashplayer/");
            }
        }

        private bool CheckShockwaveFlash()
        {
            object flash_object;
            bool satisfied = true;            
            try
            {
                flash_object = Activator.CreateInstance(Type.GetTypeFromProgID("ShockwaveFlash.ShockwaveFlash"));
            }
            catch (Exception e)
            {
                satisfied = false;
                Log.Instance.Write(LogMessageTypes.ERROR, string.Format("CheckShockwaveFlash Exception: {0}", e.ToString()));
            }
            finally
            {
                flash_object = null;
            }
            return satisfied;
        }

        ~DependencyChecker() { Dispose(false); }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing) { }
                disposed = true;
            }
        }
    }       
}
