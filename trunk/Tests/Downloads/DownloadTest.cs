using BlizzTV.Downloads;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Security.Cryptography;
using System.Threading;

namespace Tests.Downloads
{   
    [TestClass()]
    public class DownloadTest
    {
        private const string Uri = "http://blizztv.googlecode.com/files/blizztv-alpha-0.1.zip";
        private const string ExpectedChecksum = "bd5182ff926fa440e34078f57b050a716f805f81";
        private const string Filename = "downloadtest.zip";
        private readonly ManualResetEvent _waitHandler = new ManualResetEvent(false); // our custom-waiter for download complete event.

        [TestMethod()]
        public void DownloadFileTest()
        {
            Download download=new Download(Uri,Filename); 
            download.Complete += DownloadComplete;            
            download.Start();            
            this._waitHandler.WaitOne(); // wait until download complete event is handled.
            Assert.IsTrue(download.Success, string.Format("Failed downloading the test file: {0}.", Uri));
            string calculatedChecksum = CalculateFileSha1Checksum(Filename);
            Assert.IsTrue(ExpectedChecksum == calculatedChecksum, string.Format("Downloaded file's SHA1 checksum ({0}) is not equal to expected checksum ({1}).", ExpectedChecksum, calculatedChecksum));
        }

        private void DownloadComplete(bool success)
        {            
            this._waitHandler.Set(); // let the waiter finish.
        }

        private static string CalculateFileSha1Checksum(string filename)
        {
            string calculated = string.Empty;
            FileStream fs = new FileStream(filename, FileMode.Open);

            using(SHA1Managed sha1=new SHA1Managed())
            {
                byte[] hash = sha1.ComputeHash(fs);                
                foreach (byte b in hash) calculated += b.ToString("x2");
            }

            return calculated.ToString();
        }
    }
}
