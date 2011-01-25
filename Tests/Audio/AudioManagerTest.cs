using BlizzTV.Audio;
using BlizzTV.Assets.Sounds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Audio
{       
    [TestClass()]
    public class AudioManagerTest
    {
        [TestMethod()]
        public void AudioManagerInitTest()
        {
            AudioManager manager = AudioManager.Instance;
            Assert.IsTrue(manager.EngineStatus == AudioManager.AudioEngineStatus.Ready, "AudioManager initialization test failed.");
        }       
    }
}
