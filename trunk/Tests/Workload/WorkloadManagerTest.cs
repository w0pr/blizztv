using BlizzTV.Controls.Animations;
using BlizzTV.Workload;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Forms;
using System;

namespace Tests.Workload
{   
    [TestClass()]
    public class WorkloadManagerTest
    {
        [TestMethod()]
        public void WorkloadTest()
        {
            ToolStrip toolstrip = new ToolStrip();
            ToolStripProgressBar progressBar = new ToolStripProgressBar();
            toolstrip.Items.Add(progressBar);

            WorkloadManager.Instance.AttachControls(progressBar, new LoadingAnimation());
            
            WorkloadManager.Instance.Add(1);
            WorkloadManager.Instance.Step();

            Assert.IsTrue(WorkloadManager.Instance.CurrentWorkload == 0, "WorkloadManager add or step failed.");
        }
    }
}
